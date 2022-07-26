using System.ComponentModel.DataAnnotations;

namespace SP.Model.RequestModels
{
    public class RequestUserCredentials
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}