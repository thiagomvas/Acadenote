using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadenode.Core.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required Role Role { get; set; }
        public bool IsActive { get; set; }
        public string? Token { get; set; }
        public required string Password { get; set; }

    }
}
