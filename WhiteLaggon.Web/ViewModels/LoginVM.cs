using System.ComponentModel.DataAnnotations;

namespace CardosoResort.Web.ViewModels
{
    public class LoginVM
    {
        [Required, EmailAddress]//Atributo que indica que o campo é do tipo email
        public string Email { get; set; }

        [Required, DataType(DataType.Password)] //Atributo que indica que o campo é do tipo password
        public string Password { get; set; }

        public bool LembrarMe { get; set; }

        public string? RedireccionarURL { get; set; }//Propriedade que guarda a URL para onde o utilizador vai ser redirecionado após o login
    }
}