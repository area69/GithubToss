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
    
    public partial class GeneralAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralAccount()
        {
            this.BankAccountTables = new HashSet<BankAccountTable>();
            this.FieldFees = new HashSet<FieldFee>();
            this.Particulars = new HashSet<Particular>();
            this.Taxes = new HashSet<Tax>();
        }
    
        public int GeneralAccountID { get; set; }
        public string GeneralAccountName { get; set; }
        public string GeneralAccountCode { get; set; }
        public int SubMajorAccountGroupID { get; set; }
        public bool isReserve { get; set; }
        public Nullable<int> ReservePercent { get; set; }
        public bool isFullReserve { get; set; }
        public bool isContinuing { get; set; }
        public bool isOBRCashAdvance { get; set; }
        public bool isNormalBalance { get; set; }
        public int isStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankAccountTable> BankAccountTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldFee> FieldFees { get; set; }
        public virtual SubMajorAccountGroup SubMajorAccountGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Particular> Particulars { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
