using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadenode.Core.Models
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        public static readonly Note NotFound = new Note { Id = "NotFound", Title = "Not Found", Content = "Note not found" };
    }
}
