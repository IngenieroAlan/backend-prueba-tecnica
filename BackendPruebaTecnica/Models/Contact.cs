using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPruebaTecnica.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public int UserId {  get; set; }
        //Asignamos UserId como una llave foranea
        [ForeignKey("UserId")]
        public User User { get; set; }
        [MaxLength(90)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string PhoneNumber {  get; set; }
        [MaxLength(180)]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
