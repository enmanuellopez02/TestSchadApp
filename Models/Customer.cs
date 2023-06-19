using System;
using System.ComponentModel.DataAnnotations;

namespace TestSchadApp.Models
{
	public class Customer
	{
		public int Id { get; set; }
        [Required, Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
		public bool Status { get; set; }

		public CustomerType CustomerType { get; set; }
        public int CustomerTypeId { get; set; }

        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
	}
}

