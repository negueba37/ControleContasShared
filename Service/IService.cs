using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleData.Service
{
	internal interface IService<T>
	{
		public void Save(T dto);
		public T Update(T dto);
		public void Delete(T dto);
		public T Get(T dto);
		
	}
}
