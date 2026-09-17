using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name{get; set;}

    
    public string Email {get; set;}

}