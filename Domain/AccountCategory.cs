using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Domain
{
	public class AccountCategory
	{
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }

    }
}
