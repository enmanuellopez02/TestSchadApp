using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using Evospike.PaginatedList.Extensions;
using Microsoft.AspNetCore.Mvc;
using TestSchadApp.Interfaces;
using TestSchadApp.Models;
using TestSchadApp.Services;

namespace TestSchadApp.Controllers
{
    public class CustomerTypesController : Controller
    {
        private readonly ILogger<CustomerTypesController> _logger;
        private readonly ICustomerTypeService _customerTypeService;

        public CustomerTypesController(ILogger<CustomerTypesController> logger,
            ICustomerTypeService customerTypeService)
        {
            _logger = logger;
            _customerTypeService = customerTypeService;
        }

        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var customerTypes = await _customerTypeService.GetAllCustomerTypesAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                try
                {
                    customerTypes = customerTypes.Where(c => c.Description.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
                catch (Exception e)
                {
                    _logger.LogError("Error in filter data {message}", e.Message);
                    customerTypes = new List<CustomerType>();
                }
            }

            if (pageNumber < 1 || pageNumber == null)
            {
                pageNumber = 1;
            }

            return View(customerTypes.GetPaged((int)pageNumber, 10));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerType = await _customerTypeService.GetCustomerTypeByIdAsync(id.Value);

            if (customerType == null)
            {
                return NotFound();
            }

            return View(customerType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                var existCustomerType = await _customerTypeService.GetCustomerTypeAsync(c => c.Description == customerType.Description);

                if (existCustomerType == null)
                {
                    await _customerTypeService.CreateCustomerTypeAsync(customerType);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "the resource already exists");
                    return View(customerType);
                }
            }

            return View(customerType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerType = await _customerTypeService.GetCustomerTypeByIdAsync(id.Value);
            if (customerType == null)
            {
                return NotFound();
            }

            return View(customerType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerType customerType)
        {
            if (id != customerType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var customerTypeToUpdate = await _customerTypeService.GetCustomerTypeByIdAsync(id);
                if (customerTypeToUpdate == null)
                {
                    return NotFound();
                }

                customerTypeToUpdate.Description = customerType.Description;

                await _customerTypeService.UpdateCustomerTypeAsync(customerTypeToUpdate);
                return RedirectToAction(nameof(Index));
            }

            return View(customerType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerType = await _customerTypeService.GetCustomerTypeByIdAsync(id.Value);

            if (customerType == null)
            {
                return NotFound();
            }

            return View(customerType);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerTypeToDelete = await _customerTypeService.GetCustomerTypeByIdAsync(id);
            if (customerTypeToDelete == null)
            {
                return NotFound();
            }

            await _customerTypeService.DeleteCustomerTypeAsync(customerTypeToDelete);
            return RedirectToAction(nameof(Index));
        }
    }
}

