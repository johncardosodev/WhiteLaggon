using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardosoResort.Domain.Entities
{
    public class Extra
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string? Descricao { get; set; }

        [ForeignKey("Villa")]
        public int VillaId { get; set; }

        [ValidateNever] //Validatenever é um atributo que diz ao ASP.NET Core para não validar a propriedade
        public Villa Villa { get; set; }//É uma propriedade de navegação, que é uma propriedade que nos permite navegar de uma entidade para outra
    }
}