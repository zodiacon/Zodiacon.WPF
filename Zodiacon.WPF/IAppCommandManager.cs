using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
	public interface IAppCommandManager {
		void Undo();
		void Redo();
		bool CanUndo { get; }
		bool CanRedo { get; }
		void Clear();
		string UndoDescription { get; }
		string RedoDescription { get; }
		void AddCommand(IAppCommand command, bool execute = true);

		event Action<IAppCommand, bool> CommandAdded;
	}
}
