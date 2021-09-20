using System;

namespace CreditWebApp.Models
{
    public class CreditModel
    {
        public Guid Id { get; set; }
        public long ApplicationId { get; set; }
        public string Type { get; set; }
        public string Summary { get; set; }
        public decimal Amount { get; set; }
        public DateTime PostingDate { get; set; }
    }
}