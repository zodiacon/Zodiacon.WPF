using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zodiacon.WPF.Behaviors;

namespace Zodiacon.WPF {
    public interface ITreeViewItem {
        string Text { get; }
        bool IsExpanded { get; set; }
        IList<ITreeViewItem> SubItems { get; }
        ITreeViewItem Parent { get; }
        bool IsSelected { get; set; }
    }

    public interface ITreeViewItemMatch : ITreeViewItem {
        Task<bool> IsMatchAsync(string searchText, int level, CancellationToken ct, SearchTextOptions options);
        Task BuildSubItemsAsync(bool build, CancellationToken ct);
        bool IsVisible { get; set; }
    }
}
