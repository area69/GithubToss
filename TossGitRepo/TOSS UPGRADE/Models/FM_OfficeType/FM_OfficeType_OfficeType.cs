using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TOSS_UPGRADE.Models.FM_OfficeType
{
    public class FM_OfficeType_OfficeType
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_OfficeType_OfficeType()
        {
            getOfficeType = new List<OfficeType>();
            getOfficeTypecolumns = new OfficeType();
            getOfficeTypeList = new List<OfficeTypeList>();
        }
        public List<OfficeTypeList> getOfficeTypeList { get; set; }
        public OfficeType getOfficeTypecolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> OfficeTypeList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.OfficeType> getOfficeType { get; set; }
    }
    public class OfficeTypeList
    {
        public int OfficeTypeID { get; set; }
        public string OfficeTypeName { get; set; }
        public string OfficeTypeCode { get; set; }
    }
}