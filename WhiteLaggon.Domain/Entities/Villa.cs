using System.ComponentModel.DataAnnotations;

namespace CardosoResort.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }

        [MaxLength(15)]
        public string Nome { get; set; }

        [MaxLength(1000)]
        public string? Descricao { get; set; }

        [Display(Name = "Preço por noite")]
        [Range(0, 10000)]
        public double Preco { get; set; }

        [Display(Name = "Número de Quartos")]
        [Range(1, 10)]
        public int Numero_Quartos { get; set; }

        [Display(Name = "Metros Quadrados"), Range(10, 300)]
        public int Metros_Quadrados { get; set; }

        public int Ocupacao { get; set; }

        [Display(Name = "Imagem Url")]
        public string? ImagemUrl { get; set; }

        public DateTime? Data_Criacao { get; set; } //Propriedade que armazena a data de criação do registro
        public DateTime? Data_Atualizacao { get; set; } //Propriedade que armazena a data de atualização do registro
    }
}