using System;
using System.Linq.Expressions;
using TestSchadApp.Models;

namespace TestSchadApp.Interfaces
{
	public interface IInvoiceService
	{
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
        Task<List<Invoice>> GetAllInvoicesAsync();
        Task<List<Invoice>> GetAllInvoicesAsync(Expression<Func<Invoice, bool>> filter);
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task<Invoice> GetInvoiceAsync(Expression<Func<Invoice, bool>> filter);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(int id);
        Task DeleteInvoiceAsync(Invoice invoice);
    }
}

