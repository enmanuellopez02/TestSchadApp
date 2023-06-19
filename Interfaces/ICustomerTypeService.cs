using System;
using System.Linq.Expressions;
using TestSchadApp.Models;

namespace TestSchadApp.Interfaces
{
	public interface ICustomerTypeService
	{
        Task<CustomerType> CreateCustomerTypeAsync(CustomerType customerType);
        Task<List<CustomerType>> GetAllCustomerTypesAsync();
        Task<List<CustomerType>> GetAllCustomerTypesAsync(Expression<Func<CustomerType, bool>> filter);
        Task<CustomerType> GetCustomerTypeByIdAsync(int id);
        Task<CustomerType> GetCustomerTypeAsync(Expression<Func<CustomerType, bool>> filter);
        Task UpdateCustomerTypeAsync(CustomerType customerType);
        Task DeleteCustomerTypeAsync(int id);
        Task DeleteCustomerTypeAsync(CustomerType customerType);
    }
}