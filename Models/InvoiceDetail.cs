using System;
using System.ComponentModel.DataAnnotations;

namespace TestSchadApp.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required, Display(Name = "Product")]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal TotalItbis { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
    }
}

