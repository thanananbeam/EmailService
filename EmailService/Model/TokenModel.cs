using System.ComponentModel.DataAnnotations;

namespace EmailService.Model
{
    public class TokenModel
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


    public class TokenResponseModel
    {
        public string Token { get; set; }
    }
}
