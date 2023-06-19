using System;
using Microsoft.EntityFrameworkCore;
using TestSchadApp.Interfaces;
using TestSchadApp.Models;
using TestSchadApp.Persistances;

namespace TestSchadApp.Repositories
{
	public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IRepository<Customer> _customers;
        private IRepository<CustomerType> _customerTypes;
        private IRepository<Invoice> _invoices;
        private IRepository<InvoiceDetail> _invoiceDetails;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Customer> Customers => _customers ??= new SqlRepository<Customer>(_context);
        public IRepository<CustomerType> CustomerTypes => _customerTypes ??= new SqlRepository<CustomerType>(_context);
        public IRepository<Invoice> Invoices => _invoices ??= new SqlRepository<Invoice>(_context);
        public IRepository<InvoiceDetail> InvoiceDetails => _invoiceDetails ??= new SqlRepository<InvoiceDetail>(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

