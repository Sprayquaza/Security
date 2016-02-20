using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace MvcLoginApp.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=(.*\d){2})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$" , ErrorMessage = "Password must have atleast 1 number, 1 special character and 1 uppercase character!")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public byte[] Salt { get; set; }

        public string Permission = "Default";
    }
}