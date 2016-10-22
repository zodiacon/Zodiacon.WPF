using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMaster.Common {
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ModuleAttribute : Attribute {
		public string Name { get; }
		public ModuleAttribute(string name) {
			Name = name;
		}

		public string Author { get; set; }
		public string Version { get; set; }
		public string Description { get; set; }
	}

}
