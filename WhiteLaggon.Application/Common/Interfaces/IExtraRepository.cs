using CardosoResort.Domain.Entities;

namespace CardosoResort.Application.Common.Interfaces
{
    public interface IExtraRepository : IRepository<Extra>
    {
        void Update(Extra entidade);

        void Save();
    }
}