using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFLTestTask.Models
{
    public class FileSystemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
        public FileSystemModel? Parent { get; set; }

        public IEnumerable<FileSystemModel> Subfolders { get; set; }
    }
}
