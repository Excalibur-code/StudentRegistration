using System.ComponentModel.DataAnnotations;

namespace Registration.Extensions
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                var fileExtension = System.IO.Path.GetExtension(file.FileName);
                if(!_extensions.Contains(fileExtension.ToLower()))
                {
                    return new ValidationResult(ErrorMessage);
                }
                return ValidationResult.Success;
            }
            throw new ArgumentException("Attribute only applies to IFormFile Propertied");
        }
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                if(file.Length > _maxFileSize)
                {
                    return new ValidationResult(ErrorMessage);
                }
                return ValidationResult.Success;
            }

            throw new ArgumentException("Attribute only applies to IFormFile properties");
        }
    }
}
