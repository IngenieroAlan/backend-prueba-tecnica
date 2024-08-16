using System.ComponentModel.DataAnnotations;

namespace BackendPruebaTecnica.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        [MaxLength(90)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string PhoneNumber {  get; set; }
        [MaxLength(180)]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
