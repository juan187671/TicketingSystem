using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketingSystem.DALTicketingSystem;

namespace TicketingSystem.Models
{
    public class User
    {
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Must be valid email address.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        public bool enable { get; set; }

        public User()
        {
        }

        public User(string pEmail, string pPassword, string pName, bool pEnable)
        {
            this.email = pEmail;
            this.password = pPassword;
            this.name = pName;
            this.enable = pEnable;
        }

        public override bool Equals(object obj)
        {
            User other = (User)obj;
            return this.email.ToUpper().Equals(other.email.ToUpper());
        }

        public static explicit operator User(TUSER u)
        {
            return new User(u.email, u.password, u.name, u.enable);
        }
    }
}