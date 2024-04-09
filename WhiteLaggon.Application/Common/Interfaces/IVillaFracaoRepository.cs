using CardosoResort.Domain.Entities;

namespace CardosoResort.Application.Common.Interfaces
{
    public interface IVillaFracaoRepository : IRepository<VillaFracao>

    {
        void Update(VillaFracao entidade); //Atualiza a entidade no DbSet

        void Save(); //Salva as alterações no banco de dados
    }
}