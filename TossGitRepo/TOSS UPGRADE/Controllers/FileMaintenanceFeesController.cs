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
using TOSS_UPGRADE.Models.FM_Fees;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceFeesController : Controller
    {
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        // GET: FileMaintenanceFees
        public ActionResult Index()
        {
            return View();
        }

        #region Field Fees
        //Table Field Fees
        public ActionResult Get_FieldFeeTable()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            List<FieldFeeList> tbl_Fees = new List<FieldFeeList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.FieldFee";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_PayeeList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Fees.Add(new FieldFeeList()
                        {
                            FieldFeeID = Convert.ToInt32(dr[0]),
                            FieldFeeDescription = GlobalFunction.ReturnEmptyString(dr[1]),
                            Rate = GlobalFunction.ReturnEmptyString(dr[2]),
                            AccountCode = GlobalFunction.ReturnEmptyString(dr[3]),
                            FundType = GlobalFunction.ReturnEmptyString(dr[4]),
                            FeeCategory = GlobalFunction.ReturnEmptyString(dr[5]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getFieldFeeList = tbl_Fees.ToList();
            return PartialView("_FeesTable", model.getFieldFeeList);
        }
        //Get Add Field Fees Partial View
        public ActionResult Get_AddFieldFees()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            return PartialView("_AddFees", model);
        }
        public ActionResult GetDynamicFeeCategory()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FieldFeeList = new SelectList((from s in TOSSDB.FeeCategories.ToList() select new { FeeCategoryID = s.FeeCategoryID, FeeCategoryName = s.FeeCategoryName }), "FeeCategoryID", "FeeCategoryName");
            return PartialView("_DynamicDDFeeCategoryName", model);
        }
        #endregion
        #region Fee Category
        public ActionResult Get_AddFeeCategory()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            return PartialView("FeeCategory/_AddFeeCategory", model);
        }

        public ActionResult Get_FeeCategoryTable()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            List<FeeCategoryList> tbl_Fees = new List<FeeCategoryList>();

            var SQLQuery = "SELECT FeeCategoryID, FeeCategoryName, AccountableFormTable.Description FROM DB_TOSS.dbo.FeeCategory,dbo.AccountableFormTable where dbo.AccountableFormTable.AccountFormID = dbo.FeeCategory.AccountFormID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_PayeeList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Fees.Add(new FeeCategoryList()
                        {
                            FeeCategoryID = Convert.ToInt32(dr[0]),
                            FeeCategoryName = GlobalFunction.ReturnEmptyString(dr[1]),
                            AccountForm = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getFeeCategoryList = tbl_Fees.ToList();
            return PartialView("FeeCategory/_FeeCategoryTable", model.getFeeCategoryList);
        }

        public JsonResult AddFeeCategory(FM_Fees_Fee model)
        {
            FeeCategory tblFeeCategory = new FeeCategory();
            tblFeeCategory.FeeCategoryName = model.getFeeCategorycolumns.FeeCategoryName;
            tblFeeCategory.AccountFormID = model.AccountFormID;
            TOSSDB.FeeCategories.Add(tblFeeCategory);
            TOSSDB.SaveChanges();
            return Json(tblFeeCategory);
        }

        //Dropdown Fund
        public ActionResult GetDynamicAccountableFormDescription()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FeeCategoryList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() select new { AccountFormID = s.AccountFormID, Description = s.Description }), "AccountFormID", "Description");
            return PartialView("FeeCategory/_DynamicDDAccountableFormDescription", model);
        }

        public ActionResult GetSelectedDynamicAccountableFormDescription(int AccountFormTempID)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FeeCategoryList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() select new { AccountFormID = s.AccountFormID, Description = s.Description }), "AccountFormID", "Description");
            model.AccountFormID = AccountFormTempID;
            return PartialView("FeeCategory/_DynamicDDAccountableFormDescription", model);
        }


        //Get Update Internal Revenue Allotment
        public ActionResult Get_UpdateFeeCategory(FM_Fees_Fee model, int FeeCategoryID)
        {
            FeeCategory tblFeeCategory = (from e in TOSSDB.FeeCategories where e.FeeCategoryID == FeeCategoryID select e).FirstOrDefault();
            model.getFeeCategorycolumns.FeeCategoryID = tblFeeCategory.FeeCategoryID;
            model.getFeeCategorycolumns.FeeCategoryName = tblFeeCategory.FeeCategoryName;
            model.AccountFormTempID = tblFeeCategory.AccountFormID;
            return PartialView("FeeCategory/_UpdateFeeCategory", model);
        }

        //Update Internal Revenue Allotment
        public ActionResult UpdateFeeCategory(FM_Fees_Fee model)
        {
            FeeCategory tblFeeCategory = (from e in TOSSDB.FeeCategories where e.FeeCategoryID == model.getFeeCategorycolumns.FeeCategoryID select e).FirstOrDefault();
            tblFeeCategory.FeeCategoryName = model.getFeeCategorycolumns.FeeCategoryName;
            tblFeeCategory.AccountFormID = model.AccountFormID;
            TOSSDB.Entry(tblFeeCategory);
            TOSSDB.SaveChanges();
            return PartialView("FeeCategory/_UpdateFeeCategory", model);
        }

        //Delete Internal Revenue Allotment
        public ActionResult DeleteFeeCategory(FM_Fees_Fee model, int FeeCategoryID)
        {
            FeeCategory tblFeeCategory = (from e in TOSSDB.FeeCategories where e.FeeCategoryID == FeeCategoryID select e).FirstOrDefault();
            TOSSDB.FeeCategories.Remove(tblFeeCategory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}