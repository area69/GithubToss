//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOSS_UPGRADE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FieldFee
    {
        public int FieldFeeID { get; set; }
        public string FieldFeeDescription { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public int GeneralAccountID { get; set; }
        public int SubFundID { get; set; }
        public int FeeCategoryID { get; set; }
        public bool ORRequired { get; set; }
    
        public virtual FeeCategory FeeCategory { get; set; }
        public virtual GeneralAccount GeneralAccount { get; set; }
        public virtual SubFund SubFund { get; set; }
    }
}
