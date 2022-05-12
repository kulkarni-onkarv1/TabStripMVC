using System;
using System.Collections.Generic;

namespace TabStripDemo.Models
{
    public partial class CandidateTransaction
    {
        public int Urn { get; set; }
        public int? UserId { get; set; }
        public string ReciepientBank { get; set; }
        public string TransactionType { get; set; }
        public string Utr { get; set; }
        public double Amount { get; set; }
        public string TransactionRecieptPath { get; set; }
        public int ApprovalStatus { get; set; }
        public string ApproverRemark { get; set; }
        public DateTime? RequestDateTime { get; set; }
        public DateTime? ResponseDateTime { get; set; }
        public DateTime? TransactionDate { get; set; }

        public virtual User User { get; set; }
    }
}
