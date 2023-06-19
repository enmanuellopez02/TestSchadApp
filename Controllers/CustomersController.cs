using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evospike.PaginatedList.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestSchadApp.Interfaces;
using TestSchadApp.Models;
using TestSchadApp.Services;

namespace TestSchadApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;
        private readonly ICustomerTypeService _customerTypeService;

        public CustomersController(ILogger<CustomersController> logger,
            ICustomerService customerService,
            ICustomerTypeService customerTypeService)
        {
            _logger = logger;
            _customerService = customerService;
            _customerTypeService = customerTypeService;
        }

        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var customers = await _customerService.GetAllCustomersAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                try
                {
                    customers = customers.Where(c => c.CustomerName.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
                catch (Exception e)
                {
                    _logger.LogError("Error in filter data {message}", e.Message);
                    customers = new List<Customer>();
                }
            }

            if (pageNumber < 1 || pageNumber == null)
            {
                pageNumber = 1;
            }

            return View(customers.GetPaged((int)pageNumber, 10));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public async Task<IActionResult> Create()
        {
            await PoblarCustomerTypes();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var existCustomer = await _customerService.GetCustomerAsync(c => c.CustomerName == customer.CustomerName
                    && c.CustomerTypeId == customer.CustomerTypeId);

                if (existCustomer == null)
                {
                    await _customerService.CreateCustomerAsync(customer);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "the resource already exists");
                    return View(customer);
                }
            }

            await PoblarCustomerTypes();
            return View(customer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            await PoblarCustomerTypes();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var customerToUpdate = await _customerService.GetCustomerByIdAsync(id);
                if (customerToUpdate == null)
                {
                    return NotFound();
                }

                customerToUpdate.CustomerName = customer.CustomerName;
                customerToUpdate.Address = customer.Address;
                customerToUpdate.CustomerType = customer.CustomerType;
                customerToUpdate.Status = customer.Status;

                await _customerService.UpdateCustomerAsync(customerToUpdate);
                return RedirectToAction(nameof(Index));
            }

            await PoblarCustomerTypes();
            return View(customer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerToDelete = await _customerService.GetCustomerByIdAsync(id);
            if (customerToDelete == null)
            {
                return NotFound();
            }

            await _customerService.DeleteCustomerAsync(customerToDelete);
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public async Task PoblarCustomerTypes()
        {
            var customerTypes = await _customerTypeService.GetAllCustomerTypesAsync();
            ViewData["CustomerTypes"] = new SelectList(customerTypes, "Id", "Description");
        }
    }
}