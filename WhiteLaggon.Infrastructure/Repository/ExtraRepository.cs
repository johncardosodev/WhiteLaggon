using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Domain.Entities;
using CardosoResort.Infrastructure.Data;

namespace CardosoResort.Infrastructure.Repository
{
    public class ExtraRepository : Repository<Extra>, IExtraRepository
    {
        private readonly ApplicationDbContext _db;

        public ExtraRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges(); //Salvamos as alterações no banco de dados
        }

        public void Update(Extra entidade)
        {
            _db.Extras.Update(entidade); //Atualizamos a entidade no DbSet
        }
    }
}