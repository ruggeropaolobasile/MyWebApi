using System.ComponentModel.DataAnnotations;

namespace MyWebApi.model
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
