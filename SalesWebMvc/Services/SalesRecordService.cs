using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace SalesWebMvc.Services
{
	public class SalesRecordService
	{
		private readonly SalesWebMvcContext _context;
		public SalesRecordService(SalesWebMvcContext context)
		{
			_context = context;
		}
		public async Task<List<SalesRecord>> FindByDateasync(DateTime? minDate, DateTime? maxDate)
		{
			var result = from obj in _context.SalesRecord select obj;
			if (minDate.HasValue)
			{
				result = result.Where(x => x.Date >= minDate.Value);
			}
			if (maxDate.HasValue)
			{
				result = result.Where(x => x.Date <= maxDate.Value);
			}
			return await result
				.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderBy(x => x.Id)
				.ToListAsync();
		}
		public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingasync(DateTime? minDate, DateTime? maxDate)
		{
			var result = from obj in _context.SalesRecord select obj;
			if (minDate.HasValue)
			{
				result = result.Where(x => x.Date >= minDate.Value);
			}
			if (maxDate.HasValue)
			{
				result = result.Where(x => x.Date <= maxDate.Value);
			}
			return await result
				.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderBy(x => x.Id)
				.GroupBy(x => x.Seller.Department)
				.ToListAsync();
		}
	}
}
