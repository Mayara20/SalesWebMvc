using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
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
		public IActionResult Index()
		{
			var list = SellerService.FindAll();
			return View(list);
		}
		
		public IActionResult Create()
		{
			var department = DepartmentService.FindAll();
			var ViewModel = new SellerFormViewModel { Departments = department };
			return View(ViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Seller Seller)
		{
			SellerService.Insert(Seller);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}
			var obj = SellerService.FindById(id.Value);
			if(obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			SellerService.Remove(id);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var obj = SellerService.FindById(id.Value);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}
		public IActionResult Edit(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}
			var obj = SellerService.FindById(id.Value);
			if(obj == null)
			{
				return NotFound();
			}
			var departments = DepartmentService.FindAll();
			var ViewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
			return View(ViewModel);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, Seller seller)
		{
			if(id != seller.Id)
			{
				return NotFound();
			}
			SellerService.Update(seller);
			return RedirectToAction(nameof(Index));
		}
	}
}
