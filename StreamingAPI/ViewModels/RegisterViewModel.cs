using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StreamingAPI.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O E-mail é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,100}$",
        ErrorMessage = "A senha deve incluir maiúscula, minúscula, número e símbolo.")]
        public string SenhaHash { get; set; }

        [Compare("SenhaHash", ErrorMessage = "As senhas são diferentes")]
        public string ConfirmacaoSenha { get; set; }
    }
}