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
    
    public partial class AccountableForm_Assignment
    {
        public int AssignAFID { get; set; }
        public int CollectorID { get; set; }
        public int SubFundID { get; set; }
        public int AFORID { get; set; }
        public System.DateTime DateIssued { get; set; }
        public Nullable<int> FieldFeeID { get; set; }
        public Nullable<int> TotalAmount { get; set; }
        public Nullable<System.DateTime> DateTransferred { get; set; }
        public Nullable<int> SubCollectorID { get; set; }
        public Nullable<int> BarangayID { get; set; }
        public Nullable<bool> IsTransferred { get; set; }
        public Nullable<bool> IsConsumed { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    
        public virtual AccountableForm_Inventory AccountableForm_Inventory { get; set; }
        public virtual Barangay_BarangayName Barangay_BarangayName { get; set; }
        public virtual CollectorTable CollectorTable { get; set; }
        public virtual FieldFee FieldFee { get; set; }
        public virtual SubFund SubFund { get; set; }
        public virtual SubCollectorTable SubCollectorTable { get; set; }
    }
}
