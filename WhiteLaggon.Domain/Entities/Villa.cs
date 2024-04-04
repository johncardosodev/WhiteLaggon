using System.ComponentModel.DataAnnotations;

namespace CardosoResort.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [MaxLength(1000)]
        public string? Descricao { get; set; }

        [Display(Name = "Preço por noite")]
        [Range(0, 10000)]
        public double Preco { get; set; }

        [Display(Name = "Metros Quadrados"), Range(10, 300)]
        public int Metros_Quadrados { get; set; }

        [Display(Name = "Ocupação")]
        [Range(1, 6)]
        public int Ocupacao { get; set; }

        [Display(Name = "Imagem Url")]
        public string? ImagemUrl { get; set; }

        public DateTime? Data_Criacao { get; set; } //Propriedade que armazena a data de criação do registro
        public DateTime? Data_Atualizacao { get; set; } //Propriedade que armazena a data de atualização do registro

        public bool Equals(Villa villa)
        {
            if (villa == null)
            {
                return false;
            }
            return this.Nome == villa.Nome && this.Descricao == villa.Descricao && this.Preco == villa.Preco && this.Metros_Quadrados == villa.Metros_Quadrados && this.Ocupacao == villa.Ocupacao && this.ImagemUrl == villa.ImagemUrl;
        }
    }
}