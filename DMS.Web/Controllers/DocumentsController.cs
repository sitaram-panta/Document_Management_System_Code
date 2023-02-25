using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMS.Web.Data;
using DMS.Web.Models;
using DMS.Web.Mapper;
using DMS.Web.viewmodel;
using Microsoft.AspNetCore.Authorization;

namespace DMS.Web.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private readonly DMSDbContext _context;

        public DocumentsController(DMSDbContext context)
        {
            _context = context;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var documentList = await _context.Documents.ToListAsync();
            var documentListVM = documentList.ToViewModel();
            if (documentListVM is not null)
            {
                return View(documentListVM);
            }
            return View(new List<DocumentViewModel>());

        }


        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

       
        // POST: Documents/Create
       
        
        
        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel documentViewModel)
        {
            if (ModelState.IsValid)
            {
                var profileRelativePath = SavefileName(documentViewModel.file);
                // Add employee record to db
                documentViewModel.FileName = profileRelativePath;

                var documentModel = documentViewModel.ToModel();
                _context.Add(documentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentViewModel);
        }



        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int id)
        {           
            var document = await _context.Documents.FindAsync(id);

            var documentViewModel = document.ToViewModel();
            
            return View(documentViewModel);
        }



        // POST: Documents/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DocumentViewModel documentvm)
        {
            try
            {
                var document = documentvm.ToModel();
                _context.Update(document);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int documentId)
        {
            throw new NotImplementedException();
        }



        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .FirstOrDefaultAsync(m => m.DocumentId == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Documents == null)
            {
                return Problem("Entity set 'DMSDbContext.Documents'  is null.");
            }
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public string SavefileName(IFormFile file)
        {
            // Save image to "profiles" folder        
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);  //Path.GetFileNameWithoutExtension(file.FileName)
            //var indexOfDot = fileName.LastIndexOf(".");
            //var fileExtenstion = fileName.Substring(indexOfDot);
            var fileExtenstion  = Path.GetExtension(file.FileName);
            var profileRelativePath = $"{fileName + Guid.NewGuid()}{fileExtenstion}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Documentupload/fileupload", profileRelativePath);

            using var stream = System.IO.File.Create(filePath);
            file.CopyTo(stream);

            return profileRelativePath;
        }

        public IActionResult Download(DocumentViewModel documentViewModel)
        {
            if (documentViewModel.FileName == null)
            {
                return Content("<script>alert('File name not present.');</script>");
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Documentupload/fileupload", documentViewModel.FileName);
            //var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] bytes = System.IO.File.ReadAllBytes(filePath); 
            return File(bytes, "application/octet-stream", documentViewModel.FileName);
        }



    }
}
