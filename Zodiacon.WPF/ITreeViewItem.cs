using System.Collections.Generic;

namespace Zodiacon.WPF {
    public interface ITreeViewItem {
        string Text { get; }
        bool IsExpanded { get; set; }
        IList<ITreeViewItem> SubItems { get; }
        ITreeViewItem Parent { get; }
        bool IsSelected { get; set; }
    }
}