using CardosoResort.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardosoResort.Web.ViewModels
{
    public class VillaFracaoVM
    {
        public VillaFracao? VillaFracao { get; set; } //Propriedade que armazena a villa fracao que será exibida na view

        [ValidateNever] //Validação nunca é executada para essa propriedade
        public IEnumerable<SelectListItem>? VillaLista { get; set; } //
    }

    /*O código selecionado representa uma classe chamada VillaFracaoVM no namespace CardosoResort.Web.ViewModels. Essa classe é usada como um modelo de visualização (view model) em um aplicativo web.
A classe VillaFracaoVM possui duas propriedades:
1.	VillaFracao: Essa propriedade é do tipo VillaFracao?, o que significa que ela pode armazenar uma instância nula da classe VillaFracao. Ela é usada para armazenar a villa fracao que será exibida na visualização (view). O ? indica que a propriedade pode ser nula.
2.	VillaLista: Essa propriedade é do tipo IEnumerable<SelectListItem>?, o que significa que ela pode armazenar uma coleção nula de objetos SelectListItem. Ela é usada para armazenar uma lista de villas que podem ser selecionadas na visualização. A classe SelectListItem é normalmente usada para representar um item em uma lista suspensa (dropdown list) ou uma lista de seleção (select list).
No geral, essa classe é projetada para armazenar dados que serão passados entre o controlador (controller) e a visualização (view) no aplicativo web. Ela encapsula as propriedades necessárias para exibir e interagir com os dados de villa fracao na interface do usuário.*/
}