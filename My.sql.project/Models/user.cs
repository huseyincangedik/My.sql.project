using System.ComponentModel.DataAnnotations;

namespace My.sql.project.Models
{
    public class User
    {
        public int Id { get; set; } // Primary Key
        public string Username { get; set; } = string.Empty;
        //[EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
       //FluentValidation
        public string Role { get; set; } = "user";
    }
}