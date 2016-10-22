using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMaster.Common;

namespace SystemMaster.Core {
	[Module("Core")]
	[Export(typeof(ModuleBase))]
	sealed class Module : ModuleBase {
		public override void Initialize(ISystemMasterHost host) {
			
		}
	}
}
