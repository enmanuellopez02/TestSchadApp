using System;
using System.Linq.Expressions;
using TestSchadApp.Interfaces;
using TestSchadApp.Models;

namespace TestSchadApp.Services
{
	public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _unitOfWork.Customers.CreateAsync(customer);
            await _unitOfWork.CommitAsync();

            return customer;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _unitOfWork.Customers.RemoveAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            await _unitOfWork.Customers.RemoveAsync(customer);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            customers.ForEach(async customer =>
            {
                customer.CustomerType = await _unitOfWork.CustomerTypes.GetAsync(customerType => customerType.Id == customer.CustomerTypeId);
            });

            return customers;
        }

        public async Task<List<Customer>> GetAllCustomersAsync(Expression<Func<Customer, bool>> filter)
        {
            return await _unitOfWork.Customers.GetAllAsync(filter);
        }

        public async Task<Customer> GetCustomerAsync(Expression<Func<Customer, bool>> filter)
        {
            return await _unitOfWork.Customers.GetAsync(filter);
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetAsync(id);
            customer.CustomerType = await _unitOfWork.CustomerTypes.GetAsync(customerType => customerType.Id == customer.CustomerTypeId);

            return customer;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _unitOfWork.Customers.UpdateAsync(customer);
            await _unitOfWork.CommitAsync();
        }
    }
}

