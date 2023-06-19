using System;
using System.Linq.Expressions;
using TestSchadApp.Interfaces;
using TestSchadApp.Models;

namespace TestSchadApp.Services
{
	public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            await _unitOfWork.Invoices.CreateAsync(invoice);
            await _unitOfWork.CommitAsync();

            return invoice;
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            await _unitOfWork.Invoices.RemoveAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteInvoiceAsync(Invoice invoice)
        {
            await _unitOfWork.Invoices.RemoveAsync(invoice);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            var invoices = await _unitOfWork.Invoices.GetAllAsync();
            invoices.ForEach(async invoice =>
            {
                invoice.Customer = await _unitOfWork.Customers.GetAsync(customer => customer.Id == invoice.CustomerId);
                invoice.InvoiceDetails = await _unitOfWork.InvoiceDetails.GetAllAsync(invoiceDetail => invoiceDetail.InvoiceId == invoice.Id);
            });

            return invoices;
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync(Expression<Func<Invoice, bool>> filter)
        {
            return await _unitOfWork.Invoices.GetAllAsync(filter);
        }

        public async Task<Invoice> GetInvoiceAsync(Expression<Func<Invoice, bool>> filter)
        {
            return await _unitOfWork.Invoices.GetAsync(filter);
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _unitOfWork.Invoices.GetAsync(id);
            invoice.Customer = await _unitOfWork.Customers.GetAsync(customer => customer.Id == invoice.CustomerId);
            invoice.InvoiceDetails = await _unitOfWork.InvoiceDetails.GetAllAsync(invoiceDetail => invoiceDetail.InvoiceId == invoice.Id);

            return invoice;
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            await _unitOfWork.Invoices.UpdateAsync(invoice);
            await _unitOfWork.CommitAsync();
        }
    }
}

