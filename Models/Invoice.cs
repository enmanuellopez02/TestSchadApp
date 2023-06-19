using System;
using System.ComponentModel.DataAnnotations;

namespace TestSchadApp.Models
{
	public class Invoice
	{
		public int Id { get; set; }
		public decimal TotalItbis { get; set; }
		public decimal SubTotal { get; set; }
		public decimal Total { get; set; }

		public Customer Customer { get; set; }
        [Required]
        public int CustomerId { get; set; }

		public List<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
	}
}

