using System;
using TestSchadApp.Models;

namespace TestSchadApp.Interfaces
{
	public interface IUnitOfWork
	{
        IRepository<Customer> Customers { get; }
        IRepository<CustomerType> CustomerTypes { get; }
        IRepository<Invoice> Invoices { get; }
        IRepository<InvoiceDetail> InvoiceDetails { get; }

        Task CommitAsync();
    }
}

