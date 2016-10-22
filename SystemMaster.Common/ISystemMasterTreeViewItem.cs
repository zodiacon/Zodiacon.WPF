using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zodiacon.WPF;

namespace SystemMaster.Common {
	public interface ISystemMasterTreeViewItem : ITreeViewItem, ITreeViewItemMatch {
		IEnumerable<MenuItemViewModelBase> ContextMenuItems { get; }
	}
}
