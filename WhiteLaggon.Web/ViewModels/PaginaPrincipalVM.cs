using CardosoResort.Domain.Entities;

namespace CardosoResort.Web.ViewModels
{
    public class PaginaPrincipalVM
    {
        public IEnumerable<Villa>? VillaLista { get; set; } //Lista de villas

        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int Noites { get; set; }
    }
}