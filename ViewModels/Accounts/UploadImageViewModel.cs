using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Accounts
{
    public class UploadImageViewModel
    {
        [Required(ErrorMessage = "A imagem é obrigatória")]
        public string Base64Image { get; set; }

        
    }
}