using Bonbonniere.Core.App.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bonbonniere.Website.Features.UserInfo
{
    public class UserViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public Gender Gender{get;set;}
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
