using System.ComponentModel.DataAnnotations;

namespace BackendPruebaTecnica.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(90)]
        public required string UserName { get; set; }
        [MaxLength(180)]
        public required string Email { get; set; }
        public required DateTime RegisterDate {get;set;}
    }
}
