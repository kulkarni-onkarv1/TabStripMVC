using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TabStripDemo.Models
{
    public partial class CandidateTransaction
    {
        public int Urn { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage ="Mention reciepant's bank")]
        public string ReciepientBank { get; set; }
        [Required]
        public string TransactionType { get; set; }
        [Required(ErrorMessage = "Mention UTR Number")]
        public string Utr { get; set; }
        [Required(ErrorMessage = "Mention Amount")]
        [Remote(action: "IsValidAmount", controller: "CandidateParticipation", HttpMethod = "POST")]
        public double Amount { get; set; }
        public string TransactionRecieptPath { get; set; }
        [Required]
        public int ApprovalStatus { get; set; }
        //[Required(ErrorMessage = "Remark is mandatory")]
        public string ApproverRemark { get; set; }
        public DateTime? RequestDateTime { get; set; }
        public DateTime? ResponseDateTime { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public DateTime TransactionDate { get; set; }

        public virtual User User { get; set; }
    }
}
