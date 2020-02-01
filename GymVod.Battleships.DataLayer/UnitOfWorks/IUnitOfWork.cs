using System.Threading.Tasks;

namespace GymVod.Battleships.DataLayer.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void AddForDelete<TEntity>(TEntity entity) where TEntity : class;
        void AddForInsert<TEntity>(TEntity entity) where TEntity : class;
        void AddForUpdate<TEntity>(TEntity entity) where TEntity : class;
        Task CommitAsync();
    }
}