using System.ComponentModel.DataAnnotations;
using Registration.Extensions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        //[Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Summary is required")]
        [MaxLength(250, ErrorMessage = "Should not exceed 250 characters")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Photo is required")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Only JPG and PNG files are allowed")]
        [MaxFileSize(250 * 1024, ErrorMessage = "Photo should be exceed 250KB")]

        [NotMapped]
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; } = "";

        public string PhotoType { get; set; } = "";
        public string PhotoData { get; set; } = "";
    }
}
