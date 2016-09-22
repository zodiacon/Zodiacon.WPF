using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.ViewModels {
    class FileViewModel {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime Modified { get; set; }
    }
}
