using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Railway_OnlineTicket.Models
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*$", ErrorMessage = ("Email Not Valid"))]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = ("Password Must Be 8 Caracter"))]
        public string Password { get; set; }
        [Required(ErrorMessage ="Select Gender")]
        public string Gender { get; set; }
        public string role { get; set; }
    }
}