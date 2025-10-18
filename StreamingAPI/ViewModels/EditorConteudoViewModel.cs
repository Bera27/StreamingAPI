using System.ComponentModel.DataAnnotations;
using StreamingAPI.Models;

namespace StreamingAPI.ViewModels
{
    public class EditorConteudoViewModel
    {
        [Required(ErrorMessage = "O titulo é obrigatório")]
        [StringLength(40, MinimumLength = 10, ErrorMessage = "Este campo dever conter entre 3 e 40 caracteres")]
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string FileUrl { get; set; }
        public int CriadorId { get; set; }
    }
}