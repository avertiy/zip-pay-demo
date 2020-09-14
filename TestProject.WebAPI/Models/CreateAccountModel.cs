using System.ComponentModel.DataAnnotations;

namespace TestProject.WebAPI.Models
{
    public class CreateAccountModel
    {
        [Required]
        public int UserId { get; set; }
    }
}