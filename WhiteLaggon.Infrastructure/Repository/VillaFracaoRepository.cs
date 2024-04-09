using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Domain.Entities;
using CardosoResort.Infrastructure.Data;

namespace CardosoResort.Infrastructure.Repository
{
    public class VillaFracaoRepository : Repository<VillaFracao>, IVillaFracaoRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaFracaoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges(); //Salvamos as alterações no banco de dados
        }

        public void Update(VillaFracao entidade)
        {
            _db.VillaFracoes.Update(entidade); //Atualizamos a entidade no DbSet
        }
    }
}