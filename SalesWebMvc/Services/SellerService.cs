using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
	public class SellerService
	{
		private readonly SalesWebMvcContext _context;

		public SellerService(SalesWebMvcContext context)
		{
			_context = context;
		}
		public List<Seller> FindAll()
		{
			return _context.Seller.ToList();
		}
		public void Insert(Seller Seller)
		{
			Seller.Department = _context.Department.First();
			_context.Add(Seller);
			_context.SaveChanges();
		}

	}
}
