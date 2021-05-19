using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
	public class SellersController : Controller
	{ 
		private readonly SellerService SellerService;
		private readonly DepartmentService DepartmentService;

		public SellersController(SellerService sellerService,DepartmentService departmentService)
		{
			SellerService = sellerService;
			DepartmentService = departmentService;
		}
		public async Task<IActionResult> Index()
		{
			var list = await SellerService.FindAllasync();
			return View(list);
		}
		
		public async Task<IActionResult> Create()
		{
			var department = await DepartmentService.FindAllasync();
			var ViewModel = new SellerFormViewModel { Departments = department };
			return View(ViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Seller Seller)
		{
			if (!ModelState.IsValid)
			{
				var departments = await DepartmentService.FindAllasync();
				var ViewModel = new SellerFormViewModel { Seller = Seller, Departments = departments };
				return View(ViewModel);
			}
			await SellerService.Insert(Seller);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int? id)
		{
			if(id == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id does not exist" });
			}
			var obj = SellerService.FindByIdasync(id.Value);
			if(obj == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id not found" });
			}
			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
		    await SellerService.Removeasync(id);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id does not exist" });
			}
			var obj = await SellerService.FindByIdasync(id.Value);
			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id not found" });
			}
			return View(obj);
		}
		public async Task<IActionResult> Edit(int? id)
		{
		
			if(id == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id does not exist" });
			}
			var obj = await SellerService.FindByIdasync(id.Value);
			if(obj == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id not found" });
			}
			var departments = await DepartmentService.FindAllasync();
			var ViewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
			return View(ViewModel);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Seller seller)
		{
			if (!ModelState.IsValid)
			{
				var departments = await DepartmentService.FindAllasync();
				var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
				return View(viewModel);
			}
			if(id != seller.Id)
			{
				return NotFound();
			}
			try
			{
				await SellerService.Update(seller);
				return RedirectToAction(nameof(Index));
			}
			catch(ApplicationException Ex)
			{
				return RedirectToAction(nameof(Error), new { message = Ex.Message });
			}
			
		}
		public IActionResult Error(string message)
		{
			var error = new ErrorViewModel
			{
				Message = message,
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			};
			return View(error);
		}
	}
}
