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
    
    public partial class AccountableFormTable
    {
        public int AccountFormID { get; set; }
        public string AccountFormName { get; set; }
        public int AFDescriptionID { get; set; }
        public int QuantityValue { get; set; }
        public bool isCTC { get; set; }
    
        public virtual AF_Description AF_Description { get; set; }
    }
}
