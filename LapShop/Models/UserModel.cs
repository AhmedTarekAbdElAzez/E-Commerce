using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShop.Models
{
    public class UserModel
    {
        [ValidateNever]
        public string FirstName { get; set; }
        [ValidateNever]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter email address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
