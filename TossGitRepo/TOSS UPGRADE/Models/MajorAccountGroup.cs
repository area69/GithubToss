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
    
    public partial class MajorAccountGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MajorAccountGroup()
        {
            this.SubMajorAccountGroups = new HashSet<SubMajorAccountGroup>();
        }
    
        public int MajorAccountGroupID { get; set; }
        public int AccountGroupID { get; set; }
        public string MajorAccountGroupName { get; set; }
        public string MajorAccountGroupCode { get; set; }
    
        public virtual AccountGroup AccountGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubMajorAccountGroup> SubMajorAccountGroups { get; set; }
    }
}
