using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evospike.PaginatedList.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestSchadApp.Interfaces;
using TestSchadApp.Models;
using TestSchadApp.Services;

namespace TestSchadApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ILogger<InvoicesController> _logger;
        private readonly IInvoiceService _invoiceService;
        private readonly ICustomerService _customerService;

        public InvoicesController(ILogger<InvoicesController> logger,
            IInvoiceService invoiceService,
            ICustomerService customerService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var invoices = await _invoiceService.GetAllInvoicesAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                try
                {
                    invoices = invoices.Where(c => c.Customer.CustomerName.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
                catch (Exception e)
                {
                    _logger.LogError("Error in filter data {message}", e.Message);
                    invoices = new List<Invoice>();
                }
            }

            if (pageNumber < 1 || pageNumber == null)
            {
                pageNumber = 1;
            }

            return View(invoices.GetPaged((int)pageNumber, 10));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceService.GetInvoiceByIdAsync(id.Value);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        public async Task<IActionResult> Create()
        {
            await PoblarCustomers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                // CALCULAR TOTALES
                PerformCalculations(invoice);
                invoice.InvoiceDetails.ForEach(invoiceDetail => invoiceDetail.CustomerId = invoice.CustomerId);

                await _invoiceService.CreateInvoiceAsync(invoice);
                return RedirectToAction(nameof(Index));
                
            }

            await PoblarCustomers();
            return View(invoice);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceService.GetInvoiceByIdAsync(id.Value);
            if (invoice == null)
            {
                return NotFound();
            }

            await PoblarCustomers();
            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var invoiceToUpdate = await _invoiceService.GetInvoiceByIdAsync(id);
                if (invoiceToUpdate == null)
                {
                    return NotFound();
                }

                invoiceToUpdate.Customer = invoice.Customer;
                invoiceToUpdate.InvoiceDetails = invoice.InvoiceDetails;

                // CALCULAR TOTALES
                PerformCalculations(invoiceToUpdate);

                await _invoiceService.UpdateInvoiceAsync(invoiceToUpdate);
                return RedirectToAction(nameof(Index));
            }

            await PoblarCustomers();
            return View(invoice);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceService.GetInvoiceByIdAsync(id.Value);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceToDelete = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoiceToDelete == null)
            {
                return NotFound();
            }

            await _invoiceService.DeleteInvoiceAsync(invoiceToDelete);
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PerformCalculations(Invoice invoice)
        {
            invoice.InvoiceDetails.ForEach(detail =>
            {
                detail.SubTotal = detail.Qty * detail.Price;
                detail.TotalItbis = detail.SubTotal * 0.18m;
                detail.Total = detail.SubTotal + detail.TotalItbis;
            });

            invoice.SubTotal = invoice.InvoiceDetails.Sum(detail => detail.SubTotal);
            invoice.TotalItbis = invoice.InvoiceDetails.Sum(detail => detail.TotalItbis);
            invoice.Total = invoice.InvoiceDetails.Sum(detail => detail.Total);
        }

        [NonAction]
        public async Task PoblarCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            ViewData["Customers"] = new SelectList(customers, "Id", "CustomerName");
        }
    }
}

