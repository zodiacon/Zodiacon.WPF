using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zodiacon.WPF.Utilities {
    public sealed class VirtualizingStackPanelEx : VirtualizingStackPanel {
        /// <summary>
        /// Publically expose BringIndexIntoView.
        /// </summary>
        public void BringIntoView(int index) {

            base.BringIndexIntoView(index);
        }
    }
}
