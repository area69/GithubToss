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
using TOSS_UPGRADE.Models.FM_Bank;


namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceBankController : Controller
    {
        // GET: FileMaintenanceBank    
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #region Bank
        //Table Accountable Form
        public ActionResult Get_BankTable()
        {
            FM_Bank model = new FM_Bank();
            List<BankList> tbl_Bank = new List<BankList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.BankTable";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BankTable]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Bank.Add(new BankList()
                        {
                            BankID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            BankName = GlobalFunction.ReturnEmptyString(dr[1]),
                           BankCode = GlobalFunction.ReturnEmptyString(dr[2]),
                            BankAddress = GlobalFunction.ReturnEmptyString(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBankList = tbl_Bank.ToList();
            return PartialView("Bank/_BankTable", model.getBankList);
        }
        #endregion
    }
}