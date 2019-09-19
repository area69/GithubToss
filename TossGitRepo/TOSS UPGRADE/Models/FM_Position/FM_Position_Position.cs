using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Position
{
    public class FM_Position_Position
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Position_Position()
        {
            getPosition = new List<Signatory_PositionTable>();
            getPositioncolumns = new Signatory_PositionTable();
            getPositionList = new List<PositionList>();
        }
        public List<PositionList> getPositionList { get; set; }
        public Signatory_PositionTable getPositioncolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> PositionList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Signatory_PositionTable> getPosition { get; set; }
    }

    public class PositionList
    {
        public int PositionID { get; set; }

        public string PositionName { get; set; }

        public string PositionCode { get; set; }
    }
}