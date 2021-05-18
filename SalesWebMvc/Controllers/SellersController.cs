﻿using Microsoft.AspNetCore.Mvc;
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

		public SellersController(SellerService sellerService)
		{
			SellerService = sellerService;
		}
		public IActionResult Index()
		{
			var list = SellerService.FindAll();
			return View(list);
		}
	}
}
