using System;
using System.Linq.Expressions;
using TestSchadApp.Models;

namespace TestSchadApp.Interfaces
{
	public interface ICustomerService
	{
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<List<Customer>> GetAllCustomersAsync(Expression<Func<Customer, bool>> filter);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> GetCustomerAsync(Expression<Func<Customer, bool>> filter);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task DeleteCustomerAsync(Customer customer);
    }
}

