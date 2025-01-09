using ControleContas.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControleContasData.DAO
{
	internal class BaseDAO<T> where T : class
	{
		private readonly ApplicationContext _context;
		private readonly DbSet<T> _dbSet;
		public BaseDAO()
		{
			_context = new ApplicationContext();
			_dbSet = _context.Set<T>();
		}
		public void Save(T entity)
		{
			try
			{
				_dbSet.Add(entity);
				_context.SaveChanges();

			}
			catch (Exception)
			{
				throw;
			}
		}
		public T update(T entity)
		{
			try
			{

				_dbSet.Update(entity);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw;
			}
			return entity;
		}
		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
			_context.SaveChanges();
		}
		public List<T> Get()
		{
			
			return _dbSet.ToList<T>();

		}
		public T GetById(int id) {
			return _dbSet.Find(id);
		}
	}
}
