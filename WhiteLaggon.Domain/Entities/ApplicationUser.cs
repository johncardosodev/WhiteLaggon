using Microsoft.AspNetCore.Identity;

namespace CardosoResort.Domain.Entities
{
    //A classe ApplicationUser herda da classe IdentityUser, que representa um usuário no sistema de autenticação e autorização do ASP.NET Core Identity.
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public DateTime Data_Criacao { get; set; }
    }
}