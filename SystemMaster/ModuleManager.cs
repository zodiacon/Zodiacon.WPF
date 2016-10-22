using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMaster.Common;

namespace SystemMaster {
	[Export]
	class ModuleManager {
		[Import]
		public ObservableCollection<ModuleBase> Modules { get; private set; }

		public void Init(ISystemMasterHost host) {
			foreach(var module in Modules)
				module.Initialize(host);
		}

	}
}
