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

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceSignatoriesController : Controller
    {
        // GET: FileMaintenanceSignatories
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            return View();
        }
        //Position in Dropdown
        public ActionResult GetDynamicSignatories()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            model.DepartmentList = new SelectList((from s in TOSSDB.Signatory_PositionTable.ToList() orderby s.PositionName ascending select new { PositionID = s.PositionID, PositionName = s.PositionName }), "PositionID", "PositionName");
            return PartialView("_DynamicDDPositionName", model);
        }
        public ActionResult GetSelectedDynamicSignatories(int PositionIDTempID)
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            model.DepartmentList = new SelectList((from s in TOSSDB.Signatory_PositionTable.ToList() orderby s.PositionName ascending select new { PositionID = s.PositionID, PositionName = s.PositionName }), "PositionID", "PositionName");
            model.PositionID = PositionIDTempID;
            return PartialView("_DynamicDDPositionName", model);
        }
        //Department in Dropdown
        public ActionResult GetDynamicDepartment()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            model.DepartmentList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, Department = s.DepartmentName }), "DepartmentID", "Department");
            return PartialView("_DynamicDDDepartment", model);
        }
        public ActionResult GetSelectedDynamicDepartment(int DepartmentIDTempID)
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            model.DepartmentList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, Department = s.DepartmentName }), "DepartmentID", "Department");
            model.DepartmentID = DepartmentIDTempID;
            return PartialView("_DynamicDDDepartment", model);
        }
        public ActionResult GetDynamicFunction(int DepartmentID)
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            model.DepartmentList = new SelectList((from s in TOSSDB.Functions.Where(a => a.DepartmentID == DepartmentID).ToList() select new { FunctionID = s.FunctionID, FunctionName = s.FunctionName }), "FunctionID", "FunctionName");
            return PartialView("_DynamicDDFunctionName", model);
        }
        //Signatories Table Partial View
        public ActionResult GetListOfSignatories()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            List<SignatoriesList> TblsignatoriesLists = new List<SignatoriesList>();

            var SQLQuery = "SELECT SignatoriesID,SignatoriesName,PreferredName,PositionName,DepartmentName,IsDeptHead,FunctionName,DivisionName,isActive FROM DB_TOSS.dbo.SignatoriesTable,DB_TOSS.dbo.Signatory_PositionTable,Functions,DB_TOSS.dbo.Signatory_DepartmentTable where DB_TOSS.dbo.Signatory_PositionTable.PositionID = DB_TOSS.dbo.SignatoriesTable.PositionID and DB_TOSS.dbo.Signatory_DepartmentTable.DepartmentID = DB_TOSS.dbo.SignatoriesTable.DepartmentID AND Functions.FunctionID = SignatoriesTable.FunctionID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_SignatoryTable]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        TblsignatoriesLists.Add(new SignatoriesList()
                        {
                            SignatoriesID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            SignatoriesName = GlobalFunction.ReturnEmptyString(dr[1]),
                            PreferredName = GlobalFunction.ReturnEmptyString(dr[2]),
                            PositionName = GlobalFunction.ReturnEmptyString(dr[3]),
                            DepartmentName = GlobalFunction.ReturnEmptyString(dr[4]),
                            isDeptHead = GlobalFunction.ReturnEmptyBool(dr[5]),
                            FunctionName = GlobalFunction.ReturnEmptyString(dr[6]),
                            DivisionName = GlobalFunction.ReturnEmptyString(dr[7]),
                            isActive = GlobalFunction.ReturnEmptyBool(dr[8]),
                        });
                    }
                }
                Connection.Close();
            }

            model.getSignatoriesList = TblsignatoriesLists.ToList();


            return PartialView("_SignatoriesTable", model.getSignatoriesList);
        }
        //Add Signatories
        public JsonResult AddSignatories(FM_SignatoriesModel model)
        {
            SignatoriesTable tblSignatories = new SignatoriesTable();
            tblSignatories.SignatoriesName = GlobalFunction.ReturnEmptyString(model.getSignatoriesColumns.SignatoriesName);
            tblSignatories.PreferredName = GlobalFunction.ReturnEmptyString(model.getSignatoriesColumns.PreferredName);
            tblSignatories.PositionID = GlobalFunction.ReturnEmptyInt(model.PositionID);
            tblSignatories.DepartmentID = GlobalFunction.ReturnEmptyInt(model.DepartmentID);
            tblSignatories.IsDeptHead = GlobalFunction.ReturnEmptyBool(model.isDeptHeads);
            tblSignatories.FunctionID = GlobalFunction.ReturnEmptyInt(model.FunctionID);
            tblSignatories.DivisionName = GlobalFunction.ReturnEmptyString(model.getSignatoriesColumns.DivisionName);
            tblSignatories.isActive = GlobalFunction.ReturnEmptyBool(model.isActive);
            TOSSDB.SignatoriesTables.Add(tblSignatories);
            TOSSDB.SaveChanges();
            return Json(tblSignatories);
        }
        //Get Signatories Add Partial View
        public ActionResult Get_SignatoriesAdd()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            return PartialView("_SignatoriesAdd", model);
        }
        
        //Get Signature Data
        public ActionResult Get_UpdateSignatories(FM_SignatoriesModel model, int SignatoriesID)
        {
            SignatoriesTable tblSignatories = (from e in TOSSDB.SignatoriesTables where e.SignatoriesID == SignatoriesID select e).FirstOrDefault();
            model.getSignatoriesColumns.SignatoriesID = tblSignatories.SignatoriesID;
            model.getSignatoriesColumns.SignatoriesName = tblSignatories.SignatoriesName;
            model.getSignatoriesColumns.PreferredName = tblSignatories.PreferredName;
            model.PositionTempID = tblSignatories.PositionID;
            model.DepartmentTempID = tblSignatories.DepartmentID;
            model.isDeptHeads = tblSignatories.IsDeptHead;
            model.FunctionID = tblSignatories.FunctionID;
            model.getSignatoriesColumns.DivisionName = tblSignatories.DivisionName;
            model.isActive = tblSignatories.isActive;
            return PartialView("_SignatoriesUpdate", model);
        }

        //Update Signature Data
        public ActionResult UpdateSignatories(FM_SignatoriesModel model)
        {
            SignatoriesTable tblSignatories = (from e in TOSSDB.SignatoriesTables where e.SignatoriesID == model.getSignatoriesColumns.SignatoriesID select e).FirstOrDefault();
            tblSignatories.SignatoriesName = GlobalFunction.ReturnEmptyString(model.getSignatoriesColumns.SignatoriesName);
            tblSignatories.PreferredName = GlobalFunction.ReturnEmptyString(model.getSignatoriesColumns.PreferredName);
            tblSignatories.PositionID = GlobalFunction.ReturnEmptyInt(model.PositionID);
            tblSignatories.DepartmentID = GlobalFunction.ReturnEmptyInt(model.DepartmentID);
            tblSignatories.IsDeptHead = GlobalFunction.ReturnEmptyBool(model.isDeptHeads);
            tblSignatories.FunctionID = GlobalFunction.ReturnEmptyInt(model.FunctionID);
            tblSignatories.DivisionName = GlobalFunction.ReturnEmptyString(model.getSignatoriesColumns.DivisionName);
            tblSignatories.isActive = GlobalFunction.ReturnEmptyBool(model.isActive);
            TOSSDB.Entry(tblSignatories);
            TOSSDB.SaveChanges();
            return PartialView("_SignatoriesUpdate", model);
        }

        //Delete Signature Table
        public ActionResult DeleteSignature(FM_SignatoriesModel model, int SignatoriesID)
        {
            SignatoriesTable tblSignatories = (from e in TOSSDB.SignatoriesTables where e.SignatoriesID == SignatoriesID select e).FirstOrDefault();
            TOSSDB.SignatoriesTables.Remove(tblSignatories);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}