using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using TestSchadApp.Interfaces;
using TestSchadApp.Models;
using TestSchadApp.Repositories;

namespace TestSchadApp.Services
{
	public class CustomerTypeService : ICustomerTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerTypeService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
        }

        public async Task<CustomerType> CreateCustomerTypeAsync(CustomerType customerType)
        {
            await _unitOfWork.CustomerTypes.CreateAsync(customerType);
            await _unitOfWork.CommitAsync();

            return customerType;
        }

        public async Task DeleteCustomerTypeAsync(int id)
        {
            await _unitOfWork.CustomerTypes.RemoveAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCustomerTypeAsync(CustomerType customerType)
        {
            await _unitOfWork.CustomerTypes.RemoveAsync(customerType);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<CustomerType>> GetAllCustomerTypesAsync()
        {
            return await _unitOfWork.CustomerTypes.GetAllAsync();
        }

        public async Task<List<CustomerType>> GetAllCustomerTypesAsync(Expression<Func<CustomerType, bool>> filter)
        {
            return await _unitOfWork.CustomerTypes.GetAllAsync(filter);
        }

        public async Task<CustomerType> GetCustomerTypeAsync(Expression<Func<CustomerType, bool>> filter)
        {
            return await _unitOfWork.CustomerTypes.GetAsync(filter);
        }

        public async Task<CustomerType> GetCustomerTypeByIdAsync(int id)
        {
            return await _unitOfWork.CustomerTypes.GetAsync(id);
        }

        public async Task UpdateCustomerTypeAsync(CustomerType customerType)
        {
            await _unitOfWork.CustomerTypes.UpdateAsync(customerType);
            await _unitOfWork.CommitAsync();
        }
    }
}

