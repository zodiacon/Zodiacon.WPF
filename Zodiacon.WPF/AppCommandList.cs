using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
	public sealed class AppCommandList : IAppCommand {
		List<IAppCommand> _commands;

		public AppCommandList(string name, string desc = null, params IAppCommand[] commands) {
			Name = name;
			Description = desc ?? name;
			_commands = new List<IAppCommand>(commands);
		}

		public string Name { get; }

		public string Description { get; }

		public void AddCommands(params IAppCommand[] commands) {
			_commands.AddRange(commands);
		}

		public void AddCommands(IEnumerable<IAppCommand> commands) {
			_commands.AddRange(commands);
		}

		public void Execute() {
			for(int i = 0; i < _commands.Count; i++)
				_commands[i].Execute();
		}

		public void Undo() {
			for(int i = _commands.Count - 1; i >= 0; --i)
				_commands[i].Undo();
		}
	}
}
