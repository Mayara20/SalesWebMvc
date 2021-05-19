using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
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
		public async Task<List<Seller>> FindAllasync()
		{
			return await _context.Seller.Include(obj => obj.Department).ToListAsync();
		}
		public async Task Insert(Seller Seller)
		{ 
			_context.Add(Seller);
			await _context.SaveChangesAsync();
		}
		public async Task<Seller> FindByIdasync(int id)
		{
			return await _context.Seller.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task Removeasync(int id)
		{
			var obj = _context.Seller.Find(id);
			_context.Seller.Remove(obj);
			await _context.SaveChangesAsync();
		}
		public async Task Update(Seller obj)
		{
			if(!_context.Seller.Any(x => x.Id == obj.Id))
			{
				throw new ApplicationException("id not found");
			}
			try
			{
				_context.Update(obj);
				await _context.SaveChangesAsync();
			}
			catch(DbUpdateConcurrencyException Ex)
			{
				throw new DbConcurrencyExceptions(Ex.Message);
			}
		}
	}
}
