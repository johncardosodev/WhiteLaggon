using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CardosoResort.Web.ViewModels
{
    public class RegistoVM
    {
        [Required, EmailAddress]//Atributo que indica que o campo é do tipo email
        public string Email { get; set; }

        [Required, DataType(DataType.Password)] //Atributo que indica que o campo é do tipo password
        public string Password { get; set; }

        [Required, DataType(DataType.Password)] //Atributo que indica que o campo é do tipo password
        [Compare(nameof(Password), ErrorMessage = "As passwords não coincidem")]
        [Display(Name = "Confirmar Password")]
        public string ConfirmarPassword { get; set; }

        [Required]
        public string Nome { get; set; }

        [Display(Name = "Número de Telemóvel")]
        //Numero a começar por 9, seguido de 8 digitos
        [RegularExpression(@"^9[1236]\d{7}$", ErrorMessage = "Número de telemóvel inválido. O número de telemóvel deve começar por 9 e ter 9 digitos.")]
        public string? Telemovel { get; set; }

        public string? RedireccionarURL { get; set; }//Propriedade que guarda a URL para onde o utilizador vai ser redirecionado após o login

        //Propriedades para a lista de roles disponíveis no sistema
        public IEnumerable<SelectListItem> RolesLista { get; set; }

        public string? Role { get; set; } //Propriedade que guarda o papel selecionado pelo utilizador
    }
}