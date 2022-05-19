using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace e_commerce_project.viewModels.editor
{
    public class AddEditorVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage= "Passwords must be at least 6 characters and have at least two non alphanumeric character\n and have at least one lowercase ('a'-'z') and have at least one uppercase ('A'-'Z').")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
