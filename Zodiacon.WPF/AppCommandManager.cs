using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
	public class AppCommandManager : BindableBase, IAppCommandManager {
		public int UndoLevel { get; }
		List<IAppCommand> _undoList;
		List<IAppCommand> _redoList;

		public AppCommandManager(int undoLevel = 0) {
			if(undoLevel < 0)
				throw new ArgumentException(nameof(undoLevel));
			UndoLevel = undoLevel;

			_undoList = new List<IAppCommand>(UndoLevel + 1);
			_redoList = new List<IAppCommand>(UndoLevel + 1);
		}

		public void AddCommand(IAppCommand command, bool execute = true) {
			if(execute)
				command.Execute();
			_undoList.Add(command);
			_redoList.Clear();
			if(UndoLevel > 0 && _undoList.Count > UndoLevel)
				_undoList.RemoveAt(0);
			UpdateChanges();
			OnCommandAdded(command, execute);
		}

		public event Action<IAppCommand, bool> CommandAdded;

		protected virtual void OnCommandAdded(IAppCommand command, bool executed) {
			CommandAdded?.Invoke(command, executed);
		}

		public bool CanUndo => _undoList.Count > 0;

		public bool CanRedo => _redoList.Count > 0;

		public string UndoDescription => CanUndo ? _undoList[_undoList.Count - 1].Description : null;

		public string RedoDescription => CanRedo ? _redoList[_redoList.Count - 1].Description : null;

		public virtual void Undo() {
			if(!CanUndo)
				throw new InvalidOperationException("can't undo");
			int index = _undoList.Count - 1;
			_undoList[index].Undo();
			_redoList.Add(_undoList[index]);
			_undoList.RemoveAt(index);
			UpdateChanges();
		}

		public void UpdateChanges() {
			RaisePropertyChanged(nameof(CanUndo));
			RaisePropertyChanged(nameof(CanRedo));
			RaisePropertyChanged(nameof(UndoDescription));
			RaisePropertyChanged(nameof(RedoDescription));
		}

		public virtual void Redo() {
			if(!CanRedo)
				throw new InvalidOperationException("Can't redo");
			var cmd = _redoList[_redoList.Count - 1];
			cmd.Execute();
			_redoList.RemoveAt(_redoList.Count - 1);
			_undoList.Add(cmd);
			UpdateChanges();
		}

		public void Clear() {
			_undoList.Clear();
			_redoList.Clear();
			UpdateChanges();
		}
	}
}
