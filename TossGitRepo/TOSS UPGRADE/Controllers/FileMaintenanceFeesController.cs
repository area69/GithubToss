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

        #endregion
        #region Fee Category
        public ActionResult Get_AddFeeCategory()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            return PartialView("FeeCategory/_AddFeeCategory", model);
        }
        #endregion
    }
}