using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GFLTestTask.Data;
using GFLTestTask.Models;
using Microsoft.Extensions.Logging;

namespace GFLTestTask.Controllers
{
    public class FileSystemController : Controller
    {
        private readonly ILogger<FileSystemController> _logger;
        private readonly GFLTestTaskContext _context;

        public FileSystemController(
            ILogger<FileSystemController> logger,
            GFLTestTaskContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("FileSystem/")]
        public async Task<IActionResult> RootFolder()
        {
            FoldersViewModel folderViewModel = new FoldersViewModel();
            folderViewModel.FolderName = "Root Folder";
            folderViewModel.Subfolders = await _context.FileSystemModel
                        .Where(model => model.ParentId == null)
                        .ToListAsync();
            return View("Folder", folderViewModel);
        }

        [Route("FileSystem/{**path}")]
        public async Task<IActionResult> Folder(string path)
        {
            string[] foldersPath = path.Split('/');
            string currentFolderName = foldersPath.LastOrDefault("");

            // the code below is required to select the correct folder from the database
            var subfoldersQuery = _context.FileSystemModel.Where(folder => folder.ParentId == null);

            foreach (string folderName in foldersPath)
            {
                subfoldersQuery = subfoldersQuery
                  .Where(folder => folder.Name == folderName)
                  .SelectMany(folder => folder.Subfolders);
            }

            IEnumerable<FileSystemModel> subfolders = await subfoldersQuery.ToListAsync();

            FoldersViewModel foldersViewModel = new FoldersViewModel();
            foldersViewModel.FolderName = currentFolderName;
            foldersViewModel.Subfolders = subfolders;

            // at first I was going to do this, but EF does not work like that,
            // my dear friend said that in this case it is better to make a SQL query,
            // but now the option above is normal

            //var query = _context.FileSystemModel.Where(folder => folder.Name == currentFolderName);

            //foreach (string folderName in foldersPath.Take(foldersPath.Length - 1).Reverse())
            //{
            //    query = query.Where(folder
            //        => folder.Parent!.Name == folderName);
            //}
            //query = query.Where(folder => folder.ParentId == null);
            //query = query.Include(f => f.Subfolders);

            //FileSystemModel? model = await query.FirstOrDefaultAsync();

            //if (model == null)
            //{
            //    return Problem($"Folder {currentFolderName} not found");
            //}

            //FoldersViewModel foldersViewModel = new FoldersViewModel();
            //foldersViewModel.FolderName = model.Name;
            //foldersViewModel.Subfolders = model.Subfolders;

            return View(foldersViewModel);
        }
    }
}
