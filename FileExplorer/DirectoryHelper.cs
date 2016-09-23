using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileExplorer {
    static class DirectoryHelper {
        public async static Task<string[]> EnumerateDirectoriesAsync(string path) {
            var task = Task.Run(() => {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;
                try {
                    Debug.WriteLine($"Enumerating {path}");
                    return Directory.GetDirectories(path);
                }
                catch {
                    return Enumerable.Empty<string>().ToArray();
                }
            });

            var directories = await task;
            return directories;
        }
    }
}
