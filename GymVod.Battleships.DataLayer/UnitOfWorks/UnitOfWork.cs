using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GymVod.Battleships.DataLayer.UnitOfWorks
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext dbContext;

		public UnitOfWork(DbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void AddForDelete<TEntity>(TEntity entity)
			where TEntity : class
		{
			dbContext.Remove(entity);
		}

		public void AddForInsert<TEntity>(TEntity entity)
			where TEntity : class
		{
			dbContext.Add(entity);
		}

		public void AddForUpdate<TEntity>(TEntity entity)
			where TEntity : class
		{
			dbContext.Update(entity);
		}

		public Task CommitAsync()
		{
			return dbContext.SaveChangesAsync();
		}
	}
}
