using System.ComponentModel.DataAnnotations;

namespace DMS.Web.viewmodel
{
    public class DocumentViewModel
    {

        [Required]
        public int DocumentId { get; set; }
        [Required(ErrorMessage = "Please enter Title.")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        [Required(ErrorMessage = "File is required")]
        public IFormFile file { get; set; }

        public string? FileName { get; set; }

    }
}
