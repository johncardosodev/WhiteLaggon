using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardosoResort.Domain.Entities
{
    public class VillaFracao
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] //So para testar Key em vez de ID. DatabaseGenerated(DatabaseGeneratedOption.None) para não ser auto incremento
        [Display(Name = "Numero")]
        public int Villa_Fracao { get; set; } //Em vez de ID, usamos Villa_Fracao que é a chave primária e tambem não é auto incremento

        [ForeignKey("Villa")] //So para testar ForeignKey
        [Display(Name = "Fracção da Villa")]
        public int VillaId { get; set; }

        [ValidateNever] //Validatenever é um atributo que diz ao ASP.NET Core para não validar a propriedade
        public Villa Villa { get; set; } //É uma propriedade de navegação, que é uma propriedade que nos permite navegar de uma entidade para outra

        [Display(Name = "Detalhes especiais")]
        public string? Detalhes_Especiais { get; set; }
    }
}