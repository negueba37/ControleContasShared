using AutoMapper;
using ConsoleData.Profiles;
using ControleContas.Data;
using ControleContas.Domain;
using ControleContasData.DTO;
using Shared.Data.Domain;
using Shared.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationContext _context;
		public CategoryRepository(ApplicationContext context)
        {
            _context = context;
		}
        void ICategoryRepository.Delete(int id)
		{
			var category = _context.Category.FirstOrDefault(c => c.Id == id);
			if (category != null) { 
				_context.Category.Remove(category);
				_context.SaveChanges();
			}
		}

        IEnumerable<Category> ICategoryRepository.GetAll()
        {
            return _context.Category.ToList();
        }

        Category ICategoryRepository.GetById(int id)
		{			
			return _context.Category.FirstOrDefault(x => x.Id == id); 
		}

		void ICategoryRepository.Save(Category category)
		{
			_context.Category.Add(category);
			_context.SaveChanges();
		}

		void ICategoryRepository.Update(Category category)
		{
			_context.Category.Update(category);
			_context.SaveChanges();
		}
	}
}
