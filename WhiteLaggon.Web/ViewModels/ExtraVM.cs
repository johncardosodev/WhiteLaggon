using CardosoResort.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardosoResort.Web.ViewModels
{
    public class ExtraVM
    {
        public Extra? Extra { get; set; } //Propriedade do tipo Extra que armazena o extra que será exibido na view

        [ValidateNever]
        public IEnumerable<Extra>? Extras { get; set; } //Propriedade do tipo IEnumerable<SelectListItem> que armazena uma lista de extras

        [ValidateNever] //Validação nunca é executada para essa propriedade
        public IEnumerable<SelectListItem>? VillaLista { get; set; } //Propriedade do tipo IEnumerable<SelectListItem> que armazena uma lista de villas
    }
}