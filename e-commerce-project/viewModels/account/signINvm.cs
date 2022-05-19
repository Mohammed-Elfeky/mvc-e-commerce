using System.ComponentModel.DataAnnotations;
namespace e_commerce_project.viewModels.account
{
    public class signINvm
    {
        [Required]
        public string UserNAme { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
