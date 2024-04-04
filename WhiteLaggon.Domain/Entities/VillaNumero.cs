using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardosoResort.Domain.Entities
{
    public class VillaNumero
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] //So para testar Key em vez de ID
        [Display(Name = "Numero")]
        public int Villa_Numero { get; set; } //Em vez de ID, usamos Villa_Numero que é a chave primária e tambem não é auto incremento

        [ForeignKey("Villa")] //So para testar ForeignKey
        [Display(Name = "Villa")]
        public int VillaId { get; set; }

        public Villa Villa { get; set; } //É uma propriedade de navegação, que é uma propriedade que nos permite navegar de uma entidade para outra

        [Display(Name = "Detalhes especiais")]
        public string? Detalhes_Especiais { get; set; }
    }
}