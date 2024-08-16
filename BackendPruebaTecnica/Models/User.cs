namespace BackendPruebaTecnica.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required int Email { get; set; }
        public required DateTime FechaRegistro {get;set;}
    }
}
