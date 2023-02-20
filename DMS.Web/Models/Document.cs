
using System.ComponentModel.DataAnnotations;

namespace DMS.Web.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        




    }
}
