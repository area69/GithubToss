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
using TOSS_UPGRADE.Models.FM_Payee;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenancePayeeController : Controller
    {
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        // GET: FileMaintenancePayee
        public ActionResult Index()
        {
            return View();
        }
        #region Payee
        //Table Internal Revenue Allotment
        public ActionResult Get_OfficeTypeTable()
        {
            FM_Payee_Payee model = new FM_Payee_Payee();
            List<PayeeList> tbl_Payee = new List<PayeeList>();

            var SQLQuery = "SELECT TOP (1000) PayeeID ,PayeeName ,PayeeAddress ,DepartmentName FROM DB_TOSS.dbo.Payee,Signatory_DepartmentTable where Signatory_DepartmentTable.DepartmentID = Payee.DepartmentID";
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
                        tbl_Payee.Add(new PayeeList()
                        {
                            PayeeID = Convert.ToInt32(dr[0]),
                            PayeeName = GlobalFunction.ReturnEmptyString(dr[1]),
                            PayeeAddress = GlobalFunction.ReturnEmptyString(dr[2]),
                            DepartmentName = GlobalFunction.ReturnEmptyString(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getPayeeList = tbl_Payee.ToList();
            return PartialView("_PayeeTable", model.getPayeeList);
        }

        //Get Add Internal Revenue Allotment Partial View
        public ActionResult Get_AddOfficeType()
        {
            FM_Payee_Payee model = new FM_Payee_Payee();
            return PartialView("_AddPayee", model);
        }

        //Add Internal Revenue Allotment
        public JsonResult AddPayee(FM_Payee_Payee model)
        {
            Payee tblPayee = new Payee();
            tblPayee.PayeeName = model.getPayeecolumns.PayeeName;
            tblPayee.PayeeAddress = model.getPayeecolumns.PayeeAddress;
            tblPayee.DepartmentID = model.DepartmentID;
            TOSSDB.Payees.Add(tblPayee);
            TOSSDB.SaveChanges();
            return Json(tblPayee);
        }

        //Dropdown Fund
        public ActionResult GetDynamicDepartment()
        {
            FM_Payee_Payee model = new FM_Payee_Payee();
            model.PayeeList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, DepartmentName = s.DepartmentName }), "DepartmentID", "DepartmentName");
            return PartialView("_DynamicDDDepartmentName", model);
        }

        public ActionResult GetSelectedDynamicDepartment(int DepartmentIDTempID)
        {
            FM_Payee_Payee model = new FM_Payee_Payee();
            model.PayeeList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, DepartmentName = s.DepartmentName }), "DepartmentID", "DepartmentName");
            model.DepartmentID = DepartmentIDTempID;
            return PartialView("_DynamicDDDepartmentName", model);
        }


        //Get Update Internal Revenue Allotment
        public ActionResult Get_UpdatePayee(FM_Payee_Payee model, int PayeeID)
        {
            Payee tblPayee = (from e in TOSSDB.Payees where e.PayeeID == PayeeID select e).FirstOrDefault();
            model.getPayeecolumns.PayeeID = tblPayee.PayeeID;
            model.getPayeecolumns.PayeeName = tblPayee.PayeeName;
            model.getPayeecolumns.PayeeAddress = tblPayee.PayeeAddress;
            model.DepartmentIDTempID = Convert.ToInt32(tblPayee.DepartmentID);
            return PartialView("_UpdatePayee", model);
        }

        //Update Internal Revenue Allotment
        public ActionResult UpdateOfficeType(FM_Payee_Payee model)
        {
            Payee tblPayee = (from e in TOSSDB.Payees where e.PayeeID == model.getPayeecolumns.PayeeID select e).FirstOrDefault();
            tblPayee.PayeeName = model.getPayeecolumns.PayeeName;
            tblPayee.PayeeAddress = model.getPayeecolumns.PayeeAddress;
            tblPayee.DepartmentID = model.DepartmentID;
            TOSSDB.Entry(tblPayee);
            TOSSDB.SaveChanges();
            return PartialView("_UpdatePayee", model);
        }

        //Delete Internal Revenue Allotment
        public ActionResult DeletePayee(FM_Payee_Payee model, int PayeeID)
        {
            Payee tblPayee = (from e in TOSSDB.Payees where e.PayeeID == PayeeID select e).FirstOrDefault();
            TOSSDB.Payees.Remove(tblPayee);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}