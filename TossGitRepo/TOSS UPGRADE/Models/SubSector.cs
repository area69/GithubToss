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
    
    public partial class SubSector
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubSector()
        {
            this.Signatory_DepartmentTable = new HashSet<Signatory_DepartmentTable>();
        }
    
        public int SubSectorID { get; set; }
        public int SectorID { get; set; }
        public string SubSectorName { get; set; }
        public string SubSectorCode { get; set; }
    
        public virtual Sector Sector { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Signatory_DepartmentTable> Signatory_DepartmentTable { get; set; }
    }
}
