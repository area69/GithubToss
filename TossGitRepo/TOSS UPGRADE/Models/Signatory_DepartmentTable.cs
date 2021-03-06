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
    
    public partial class Signatory_DepartmentTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Signatory_DepartmentTable()
        {
            this.Functions = new HashSet<Function>();
            this.Payees = new HashSet<Payee>();
            this.Sections = new HashSet<Section>();
            this.SignatoriesTables = new HashSet<SignatoriesTable>();
        }
    
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentAbbreviation { get; set; }
        public string ResponsibilityCode { get; set; }
        public Nullable<int> SectorID { get; set; }
        public Nullable<int> OfficeTypeID { get; set; }
        public Nullable<int> FundID { get; set; }
        public Nullable<int> SubSectorID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Function> Functions { get; set; }
        public virtual Fund Fund { get; set; }
        public virtual OfficeType OfficeType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payee> Payees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Section> Sections { get; set; }
        public virtual Sector Sector { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignatoriesTable> SignatoriesTables { get; set; }
        public virtual SubSector SubSector { get; set; }
    }
}
