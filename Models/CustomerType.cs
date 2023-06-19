using System;
using System.ComponentModel.DataAnnotations;

namespace TestSchadApp.Models
{
	public class CustomerType
	{
		public int Id { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;

		public List<Customer> Customers { get; set; }
	}
}

