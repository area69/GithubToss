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
    
    public partial class Function
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Function()
        {
            this.Sections = new HashSet<Section>();
            this.SignatoriesTables = new HashSet<SignatoriesTable>();
        }
    
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string FunctionAbbreviation { get; set; }
        public string FunctionCode { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> SectorID { get; set; }
        public Nullable<int> SubSectorID { get; set; }
        public Nullable<int> OfficeTypeID { get; set; }
    
        public virtual OfficeType OfficeType { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual Signatory_DepartmentTable Signatory_DepartmentTable { get; set; }
        public virtual SubSector SubSector { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Section> Sections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignatoriesTable> SignatoriesTables { get; set; }
    }
}
