using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOSS_UPGRADE.GlobalFunctions;
using TOSS_UPGRADE.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TOSS_UPGRADE.Models.FM_Barangay;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceBarangayController : Controller
    {
        // GET: FileMaintenanceBarangay
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #region Barangay
        //Barangay

        //Table Barangay Name
        public ActionResult Get_BarangayNameTable()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            List<BarangayNameList> tbl_AccountableForm = new List<BarangayNameList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Barangay_BarangayName";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BarangayNameList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableForm.Add(new BarangayNameList()
                        {
                            BarangayID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            BarangayName = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBarangayNameList = tbl_AccountableForm.ToList();
            return PartialView("BarangayName/_BarangayNameTable", model.getBarangayNameList);
        }
        //Get Add Barangay Name Partial View
        public ActionResult Get_AddBarangayName()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            return PartialView("BarangayName/_AddBarangayName", model);
        }
        //Add Barangay Name
        public JsonResult AddBarangayName(FM_Barangay_BarangayName model)
        {
            Barangay_BarangayName tblBarangayName = new Barangay_BarangayName();
            tblBarangayName.BarangayName = model.getBarangayNamecolumns.BarangayName;
            TOSSDB.Barangay_BarangayName.Add(tblBarangayName);
            TOSSDB.SaveChanges();
            return Json(tblBarangayName);
        }
        //Get Barangay Name
        public ActionResult Get_UpdateBarangayName(FM_Barangay_BarangayName model, int BarangayID)
        {
            Barangay_BarangayName tblBarangayName = (from e in TOSSDB.Barangay_BarangayName where e.BarangayID == BarangayID select e).FirstOrDefault();
            model.getBarangayNamecolumns.BarangayID = tblBarangayName.BarangayID;
            model.getBarangayNamecolumns.BarangayName = tblBarangayName.BarangayName;
            return PartialView("BarangayName/_UpdateBarangayName", model);
        }
        public ActionResult UpdateBarangayName(FM_Barangay_BarangayName model)
        {
            Barangay_BarangayName tblBarangayName = (from e in TOSSDB.Barangay_BarangayName where e.BarangayID == model.getBarangayNamecolumns.BarangayID select e).FirstOrDefault();
            tblBarangayName.BarangayName = model.getBarangayNamecolumns.BarangayName;
            TOSSDB.Entry(tblBarangayName);
            TOSSDB.SaveChanges();
            return PartialView("BarangayName/_UpdateBarangayName", model);
        }
        //Delete Barangay Name
        public ActionResult DeleteBarangayName(FM_Barangay_BarangayName model, int BarangayID)
        {
            Barangay_BarangayName tblBarangayName = (from e in TOSSDB.Barangay_BarangayName where e.BarangayID == BarangayID select e).FirstOrDefault();
            TOSSDB.Barangay_BarangayName.Remove(tblBarangayName);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion
        #region Barangay Collector Name

        //Table Barangay Collector Name
        public ActionResult Get_BarangayCollectorTable()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            List<BarangayCollectorList> tbl_AccountableForm = new List<BarangayCollectorList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Barangay_CollectorName";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BarangayCollectorNameList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableForm.Add(new BarangayCollectorList()
                        {
                            BarangayCollectorID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            FirstName = GlobalFunction.ReturnEmptyString(dr[1]),
                            MiddleName = GlobalFunction.ReturnEmptyString(dr[2]),
                            LastName = GlobalFunction.ReturnEmptyString(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBarangayCollectorList = tbl_AccountableForm.ToList();
            return PartialView("BarangayCollector/_BarangayCollectorTable", model.getBarangayCollectorList);
        }
        
        //Get Add Barangay Collector Partial View
        public ActionResult Get_AddCollectorName()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            return PartialView("BarangayCollector/_AddBarangayCollector", model);
        }

        //Add Collector Name
        public JsonResult AddBarangayCollectorName(FM_Barangay_BarangayName model)
        {
            Barangay_CollectorName tblBarangayName = new Barangay_CollectorName();
            tblBarangayName.FirstName = model.getBarangayCollectorcolumns.FirstName;
            tblBarangayName.MiddleName = model.getBarangayCollectorcolumns.MiddleName;
            tblBarangayName.LastName = model.getBarangayCollectorcolumns.LastName;
            TOSSDB.Barangay_CollectorName.Add(tblBarangayName);
            TOSSDB.SaveChanges();
            return Json(tblBarangayName);
        }

        //Get Collector Name
        public ActionResult Get_UpdateCollectorName(FM_Barangay_BarangayName model, int BarangayCollectorID)
        {
            Barangay_CollectorName tblBarangayName = (from e in TOSSDB.Barangay_CollectorName where e.BarangayCollectorID == BarangayCollectorID select e).FirstOrDefault();
            model.getBarangayCollectorcolumns.BarangayCollectorID = tblBarangayName.BarangayCollectorID;
            model.getBarangayCollectorcolumns.FirstName = tblBarangayName.FirstName;
            model.getBarangayCollectorcolumns.MiddleName = tblBarangayName.MiddleName;
            model.getBarangayCollectorcolumns.LastName = tblBarangayName.LastName;
            return PartialView("BarangayCollector/_UpdateBarangayCollector", model);
        }
        public ActionResult UpdateCollectorName(FM_Barangay_BarangayName model)
        {
            Barangay_CollectorName tblBarangayName = (from e in TOSSDB.Barangay_CollectorName where e.BarangayCollectorID == model.getBarangayCollectorcolumns.BarangayCollectorID select e).FirstOrDefault();
            tblBarangayName.FirstName = model.getBarangayCollectorcolumns.FirstName;
            tblBarangayName.MiddleName = model.getBarangayCollectorcolumns.MiddleName;
            tblBarangayName.LastName = model.getBarangayCollectorcolumns.LastName;
            TOSSDB.Entry(tblBarangayName);
            TOSSDB.SaveChanges();
            return PartialView("BarangayCollector/_UpdateBarangayCollector", model);
        }
        //Delete Collector Name
        public ActionResult DeleteCollectorName(FM_Barangay_BarangayName model, int BarangayCollectorID)
        {
            Barangay_CollectorName tblBarangayName = (from e in TOSSDB.Barangay_CollectorName where e.BarangayCollectorID == BarangayCollectorID select e).FirstOrDefault();
            TOSSDB.Barangay_CollectorName.Remove(tblBarangayName);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}