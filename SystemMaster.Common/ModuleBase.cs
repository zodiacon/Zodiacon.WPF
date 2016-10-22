using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMaster.Common {
	public abstract class ModuleBase {
		public abstract void Initialize(ISystemMasterHost host);
	}
}
