using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
	public abstract class AppCommandBase : IAppCommand {
		public string Description { get; set; }

		public string Name { get; }

		public abstract void Execute();

		public virtual void Undo() {
			Execute();
		}

		protected AppCommandBase(string name, string desc = null) {
			Name = name;
			Description = desc ?? name;
		}
	}
}
