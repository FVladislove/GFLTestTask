using GFLTestTask.Models;

namespace GFLTestTask.ViewModels
{
    public class FoldersViewModel
    {
        public string FolderName { get; set; }
        public IEnumerable<FileSystemModel> Subfolders { get; set; }
    }
}
