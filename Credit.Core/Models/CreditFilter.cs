using System;

namespace Credit.Core.Models
{
    public class CreditFilter
    {
        public long ApplicationId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PostingDateFrom { get; set; }
        public DateTime? PostingDateTo { get; set; }
        
        public int Take{ get; set; } = 1000;
        public int Skip { get; set; }
    }
}