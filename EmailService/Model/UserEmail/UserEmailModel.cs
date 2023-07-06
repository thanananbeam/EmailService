using System;
using System.ComponentModel.DataAnnotations;

namespace EmailService.Model.UserEmail
{
    public class UserEmailModel
    {
        public Guid Id { get; set; }
        public string email { get; set; }
        public DateTime createdate { get; set; } = DateTime.Now;
        public DateTime updatedate { get; set; } = DateTime.Now;
    }

    // request from client
    public class CreateEmailModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }

        public void TrimData() 
        {
            if (!string.IsNullOrEmpty(this.email)) 
            {
                this.email = this.email.Trim();
            }
        }
    }

    // request from client update
    public class UpdateEmailModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }

        public void TrimData()
        {
            if (!string.IsNullOrEmpty(this.email))
            {
                this.email = this.email.Trim();
            }
        }
    }
}
