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
using TOSS_UPGRADE.Models.FM_ChartOfAccounts;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceChartofAccountController : Controller
    {
        // GET: FileMaintenanceChartofAccount   
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        #region RevisionYear
        //Barangay

        //Table Revision Year
        public ActionResult Get_RevisionYearTable()
        {
            FM_ChartOfAccounts_RevisionYear model = new FM_ChartOfAccounts_RevisionYear();
            List<RevisionYearList> tbl_RevisionYear = new List<RevisionYearList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.RevisionYear";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_RevisionYearList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_RevisionYear.Add(new RevisionYearList()
                        {
                            RevisionYearID = GlobalFunction.ReturnEmptyString(dr[0]),
                            RevisionYear = GlobalFunction.ReturnEmptyString(dr[1]),
                            IsUsed = GlobalFunction.ReturnEmptyBool(dr[2]),
                            Remarks = GlobalFunction.ReturnEmptyString(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getRevisionYearList = tbl_RevisionYear.ToList();
            return PartialView("RevisionYear/_RevisionYearTable", model.getRevisionYearList);
        }
        //Get Add Revision Year Partial View
        public ActionResult Get_AddRevisionYear()
        {
            FM_ChartOfAccounts_RevisionYear model = new FM_ChartOfAccounts_RevisionYear();
            return PartialView("RevisionYear/_AddRevisionYear", model);
        }
        //Add Revision Year
        public JsonResult AddRevisionYear(FM_ChartOfAccounts_RevisionYear model)
        {
            RevisionYear tblBarangayName = new RevisionYear();
            tblBarangayName.RevisionYearDate = model.getRevisionYearcolumns.RevisionYearDate;
            tblBarangayName.IsUsed = model.IsUsed;
            tblBarangayName.Remarks = model.getRevisionYearcolumns.Remarks;
            TOSSDB.RevisionYears.Add(tblBarangayName);
            TOSSDB.SaveChanges();
            return Json(tblBarangayName);
        }

        //Get Revision Year
        public ActionResult Get_UpdateRevisionYear(FM_ChartOfAccounts_RevisionYear model, int RevisionYearID)
        {
            RevisionYear tblBarangayName = (from e in TOSSDB.RevisionYears where e.RevisionYearID == RevisionYearID select e).FirstOrDefault();
            model.getRevisionYearcolumns.RevisionYearID = tblBarangayName.RevisionYearID;
            model.getRevisionYearcolumns.RevisionYearDate = tblBarangayName.RevisionYearDate;
            model.IsUsed = tblBarangayName.IsUsed;
            model.getRevisionYearcolumns.Remarks = tblBarangayName.Remarks;
            return PartialView("RevisionYear/_UpdateRevisionYear", model);
        }
        public ActionResult UpdateRevisionYear(FM_ChartOfAccounts_RevisionYear model)
        {
            RevisionYear tblBarangayName = (from e in TOSSDB.RevisionYears where e.RevisionYearID == model.getRevisionYearcolumns.RevisionYearID select e).FirstOrDefault();
            tblBarangayName.RevisionYearDate = model.getRevisionYearcolumns.RevisionYearDate;
            tblBarangayName.IsUsed = model.IsUsed;
            tblBarangayName.Remarks = model.getRevisionYearcolumns.Remarks;
            TOSSDB.Entry(tblBarangayName);
            TOSSDB.SaveChanges();
            return PartialView("RevisionYear/_UpdateRevisionYear", model);
        }
        //Delete Revision Year
        public ActionResult DeleteRevisionYear(FM_ChartOfAccounts_RevisionYear model, int RevisionYearID)
        {
            RevisionYear tblBarangayName = (from e in TOSSDB.RevisionYears where e.RevisionYearID == RevisionYearID select e).FirstOrDefault();
            TOSSDB.RevisionYears.Remove(tblBarangayName);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}