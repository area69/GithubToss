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
using TOSS_UPGRADE.Models.FM_IRA;
namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceIRAShareController : Controller
    {
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        // GET: FileMaintenanceIRAShare
        public ActionResult Index()
        {
            return View();
        }
        #region Internal Revenue Allotment
        //Table Internal Revenue Allotment
        public ActionResult Get_InternalRevenueAllotmentTable()
        {
            FM_IRA_IRA model = new FM_IRA_IRA();
            List<IRAList> tbl_IRA = new List<IRAList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.IRA_Table";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_IRAList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_IRA.Add(new IRAList()
                        {
                            IRAID = Convert.ToInt32(dr[0]),
                            IRAPercentageShare = GlobalFunction.ReturnEmptyDecimal(dr[1]),
                            IRAPercent = GlobalFunction.ReturnEmptyInt(dr[2]),
                            IRABase = GlobalFunction.ReturnEmptyDecimal(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getIRAList = tbl_IRA.ToList();
            return PartialView("_InternalRevenueAllotmentTable", model.getIRAList);
        }

        //Get Add Internal Revenue Allotment Partial View
        public ActionResult Get_AddInternalRevenueAllotmentTable()
        {
            FM_IRA_IRA model = new FM_IRA_IRA();
            return PartialView("_AddInternalRevenueAllotment", model);
        }

        //Add Internal Revenue Allotment
        public JsonResult AddInternalRevenueAllotmentTable(FM_IRA_IRA model)
        {
            IRA_Table tblIRA = new IRA_Table();
            tblIRA.IRAPercentageShare = model.getIRAcolumns.IRAPercentageShare;
            tblIRA.IRAPercent = model.getIRAcolumns.IRAPercent;
            tblIRA.IRABase = model.getIRAcolumns.IRABase;
            TOSSDB.IRA_Table.Add(tblIRA);
            TOSSDB.SaveChanges();
            return Json(tblIRA);
        }

        //Get Update Internal Revenue Allotment
        public ActionResult Get_UpdateInternalRevenueAllotment(FM_IRA_IRA model, int IRAID)
        {
            IRA_Table tblMemoAccount = (from e in TOSSDB.IRA_Table where e.IRAID == IRAID select e).FirstOrDefault();
            model.getIRAcolumns.IRAID = tblMemoAccount.IRAID;
            model.getIRAcolumns.IRAPercentageShare = tblMemoAccount.IRAPercentageShare;
            model.getIRAcolumns.IRAPercent = tblMemoAccount.IRAPercent;
            model.getIRAcolumns.IRABase = tblMemoAccount.IRABase;
            return PartialView("_UpdateInternalRevenueAllotment", model);
        }

        //Update Internal Revenue Allotment
        public ActionResult UpdateInternalRevenueAllotment(FM_IRA_IRA model)
        {
            IRA_Table tblMemoAccount = (from e in TOSSDB.IRA_Table where e.IRAID == model.getIRAcolumns.IRAID select e).FirstOrDefault();
            tblMemoAccount.IRAPercentageShare = model.getIRAcolumns.IRAPercentageShare;
            tblMemoAccount.IRAPercent = model.getIRAcolumns.IRAPercent;
            tblMemoAccount.IRABase = model.getIRAcolumns.IRABase;
            TOSSDB.Entry(tblMemoAccount);
            TOSSDB.SaveChanges();
            return PartialView("_UpdateInternalRevenueAllotment", model);
        }

        //Delete Internal Revenue Allotment
        public ActionResult DeleteInternalRevenueAllotment(FM_IRA_IRA model, int IRAID)
        {
            IRA_Table tblMemoAccount = (from e in TOSSDB.IRA_Table where e.IRAID == IRAID select e).FirstOrDefault();
            TOSSDB.IRA_Table.Remove(tblMemoAccount);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

    }
}