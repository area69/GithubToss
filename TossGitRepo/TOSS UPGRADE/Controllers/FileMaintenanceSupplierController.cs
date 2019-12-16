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
using TOSS_UPGRADE.Models.FM_Supplier;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceSupplierController : Controller
    {
        // GET: FileMaintenanceSupplier
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        //Supplier Add Partial View
        public ActionResult Get_AddSupplier()
        {
            FM_Supplier model = new FM_Supplier();
            return PartialView("_AddSupplier", model);
        }
        //Tax Table Partial View
        public ActionResult Get_SupplierTable()
        {
            FM_Supplier model = new FM_Supplier();
            List<SupplierList> tbl_Supplier = new List<SupplierList>();

            var SQLQuery = "SELECT SupplierID,CompanyName,ProductServices,Address,TaxType,DTIRegNo,CDARegistry,FaxNo,TelNo,TIN,MFName,[MFAddress],[MFContactNo],[AccreNumber],[AccreDate],[AccreValidity],[AccreApproveBy],[AccreMOA] FROM [DB_TOSS].[dbo].[Supplier]";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_PositionList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Supplier.Add(new SupplierList()
                        {
                            SupplierID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            CompanyName = GlobalFunction.ReturnEmptyString(dr[1]),
                            ProductServices = GlobalFunction.ReturnEmptyString(dr[2]),
                            Address = GlobalFunction.ReturnEmptyString(dr[3]),
                            TaxType = GlobalFunction.ReturnEmptyString(dr[4]),
                            DTIRegNo = GlobalFunction.ReturnEmptyString(dr[5]),
                            CDARegistry = GlobalFunction.ReturnEmptyString(dr[6]),
                            FaxNo = GlobalFunction.ReturnEmptyString(dr[7]),
                            TelNo = GlobalFunction.ReturnEmptyString(dr[8]),
                            TIN = GlobalFunction.ReturnEmptyString(dr[9]),
                            MFName = GlobalFunction.ReturnEmptyString(dr[10]),
                            MFAddress = GlobalFunction.ReturnEmptyString(dr[11]),
                            MFContactNo = GlobalFunction.ReturnEmptyString(dr[12]),
                            AccreNumber = GlobalFunction.ReturnEmptyString(dr[13]),
                            AccreDate = GlobalFunction.ReturnEmptyString(dr[14]),
                            AccreValidity = GlobalFunction.ReturnEmptyString(dr[15]),
                            AccreApprovedBy = GlobalFunction.ReturnEmptyString(dr[16]),
                            AccreMOA = GlobalFunction.ReturnEmptyString(dr[17]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getSupplierList = tbl_Supplier.ToList();
            return PartialView("_SupplierTable", model.getSupplierList);

        }
        //Add Tax
        public JsonResult AddSupplier(FM_Supplier model)
        {
            Supplier tblSupplier = new Supplier();
            tblSupplier.CompanyName = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.CompanyName);
            tblSupplier.ProductServices = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.ProductServices);
            tblSupplier.Address = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.Address);
            tblSupplier.TaxType = GlobalFunction.ReturnEmptyString(model.TaxType);
            tblSupplier.FaxNo = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.FaxNo);
            tblSupplier.TelNo = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.TelNo);
            tblSupplier.TIN = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.TIN);
            tblSupplier.MFName = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.MFName);
            tblSupplier.MFAddress = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.MFAddress);
            tblSupplier.MFContactNo = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.MFContactNo);
            tblSupplier.AccreNumber = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreNumber);
            tblSupplier.AccreDate = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreDate);
            tblSupplier.AccreValidity = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreValidity);
            tblSupplier.AccreApproveBy = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreApproveBy);
            tblSupplier.AccreMOA = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreMOA);
            TOSSDB.Suppliers.Add(tblSupplier);
            TOSSDB.SaveChanges();
            return Json(tblSupplier);
        }
        public ActionResult Get_UpdateSupplier(FM_Supplier model, int SupplierID)
        {
            Supplier tblSupplier = (from e in TOSSDB.Suppliers where e.SupplierID == SupplierID select e).FirstOrDefault();
            model.getSuppliercolumns.SupplierID = tblSupplier.SupplierID;
            model.getSuppliercolumns.CompanyName = tblSupplier.CompanyName;
            model.getSuppliercolumns.ProductServices = tblSupplier.ProductServices;
            model.getSuppliercolumns.Address = tblSupplier.Address;
            model.TaxType = tblSupplier.TaxType;
            model.getSuppliercolumns.FaxNo = tblSupplier.FaxNo;
            model.getSuppliercolumns.TelNo = tblSupplier.TelNo;
            model.getSuppliercolumns.TIN = tblSupplier.TIN;
            model.getSuppliercolumns.MFName = tblSupplier.MFName;
            model.getSuppliercolumns.MFAddress = tblSupplier.MFAddress;
            model.getSuppliercolumns.MFContactNo = tblSupplier.MFContactNo;
            model.getSuppliercolumns.AccreNumber = tblSupplier.AccreNumber;
            model.getSuppliercolumns.AccreDate = tblSupplier.AccreDate;
            model.getSuppliercolumns.AccreValidity = tblSupplier.AccreValidity;
            model.getSuppliercolumns.AccreApproveBy = tblSupplier.AccreApproveBy;
            model.getSuppliercolumns.AccreMOA = tblSupplier.AccreMOA;
            return PartialView("_UpdateSupplier", model);
        }
        //Update Signature Data
        public ActionResult UpdateSupplier(FM_Supplier model)
        {
            Supplier tblSupplier = (from e in TOSSDB.Suppliers where e.SupplierID == model.getSuppliercolumns.SupplierID select e).FirstOrDefault();
            tblSupplier.CompanyName = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.CompanyName);
            tblSupplier.ProductServices = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.ProductServices);
            tblSupplier.Address = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.Address);
            tblSupplier.TaxType = GlobalFunction.ReturnEmptyString(model.TaxType);
            tblSupplier.FaxNo = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.FaxNo);
            tblSupplier.TelNo = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.TelNo);
            tblSupplier.TIN = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.TIN);
            tblSupplier.MFName = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.MFName);
            tblSupplier.MFAddress = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.MFAddress);
            tblSupplier.MFContactNo = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.MFContactNo);
            tblSupplier.AccreNumber = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreNumber);
            tblSupplier.AccreDate = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreDate);
            tblSupplier.AccreValidity = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreValidity);
            tblSupplier.AccreApproveBy = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreApproveBy);
            tblSupplier.AccreMOA = GlobalFunction.ReturnEmptyString(model.getSuppliercolumns.AccreMOA);
            TOSSDB.Entry(tblSupplier);
            TOSSDB.SaveChanges();
            return PartialView("_UpdateSupplier", model);
        }
        //Delete Signature Table
        public ActionResult DeleteSupplier(FM_Supplier model, int SupplierID)
        {
            Supplier tblSupplier = (from e in TOSSDB.Suppliers where e.SupplierID == SupplierID select e).FirstOrDefault();
            TOSSDB.Suppliers.Remove(tblSupplier);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}