using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
    public abstract class TreeViewItemBase : BindableBase, ITreeViewItem {
        protected virtual void OnExpanded(bool expanded) { }

        public ITreeViewItem Parent { get; }

        protected TreeViewItemBase(TreeViewItemBase parent = null) {
            Parent = parent;
        }

        private bool _isExpanded;

        public virtual bool IsExpanded {
            get { return _isExpanded; }
            set {
                if(SetProperty(ref _isExpanded, value))
                    OnExpanded(value);
            }
        }


        private string _text;

        public string Text {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public virtual IList<ITreeViewItem> SubItems => null;

        bool _isSelected;
        public bool IsSelected {
            get { return _isSelected; }
            set {
                if(SetProperty(ref _isSelected, value))
                    OnSelected(value);
            }
        }

        protected virtual void OnSelected(bool selected) { }
    }
}
