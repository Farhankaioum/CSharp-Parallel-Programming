﻿using EFCORE.Data;
using EFCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace EFCORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string search = "")
        {
            var employees = _context.Employees;
            var employeesList = new List<Employee>();
            employeesList = employees.ToList();

            if (!string.IsNullOrEmpty(search))
            {
                employeesList = employees.Where("name.Contains(@0)", search).ToList();
            }

            ViewBag.Employees = employeesList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Employee model)
        {
            if (ModelState.IsValid)
            {
                model.JoinDate = DateTime.Now;
                var newModel = new List<Employee>();
                newModel.Add(model);

                _context.BulkInsert(newModel);
                _context.BulkSaveChanges();

                return RedirectToAction(nameof(Index));
            }
                
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
