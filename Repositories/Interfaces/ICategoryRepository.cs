using Shared.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Repositories.Interfaces
{
	public interface ICategoryRepository
	{
		public IEnumerable<Category> GetAll();
		public Category GetById(int id);
		public void Save(Category category);
		public void Update(Category category);
		public void Delete(int id);
	}
}
