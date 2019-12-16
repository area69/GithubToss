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
using TOSS_UPGRADE.Models.FM_Taxes;


namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceTaxesController : Controller
    {
        // GET: FileMaintenanceTaxes
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        //Tax Add Partial View
        public ActionResult Get_AddTax()
        {
            FM_Taxes model = new FM_Taxes();
            return PartialView("_AddTaxes", model);
        }
        //Tax Table Partial View
        public ActionResult Get_TaxTable()
        {
            FM_Taxes model = new FM_Taxes();
            List<TaxList> tbl_Tax = new List<TaxList>();

            var SQLQuery = "SELECT TaxID,TaxDescription,ShortDescription,TaxPercentage,TaxBase,GeneralAccount.GeneralAccountName,isUsed FROM DB_TOSS.dbo.Taxes,GeneralAccount where GeneralAccount.GeneralAccountID = Taxes.GeneralAccountID";
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
                        tbl_Tax.Add(new TaxList()
                        {
                            TaxID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            TaxDescription = GlobalFunction.ReturnEmptyString(dr[1]),
                            ShortDescription = GlobalFunction.ReturnEmptyString(dr[2]),
                            TaxPercentage = GlobalFunction.ReturnEmptyDecimal(dr[3]),
                            TaxBase = GlobalFunction.ReturnEmptyDecimal(dr[4]),
                            GeneralAccount = GlobalFunction.ReturnEmptyString(dr[5]),
                            isUsed = GlobalFunction.ReturnEmptyBool(dr[6]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getTaxList = tbl_Tax.ToList();
            return PartialView("_TaxesTable", model.getTaxList);

        }
        //Add Tax
        public JsonResult AddTax(FM_Taxes model)
        {
            Tax tblTax = new Tax();
            tblTax.TaxDescription = GlobalFunction.ReturnEmptyString(model.getTaxcolumns.TaxDescription);
            tblTax.ShortDescription = GlobalFunction.ReturnEmptyString(model.getTaxcolumns.ShortDescription);
            tblTax.TaxPercentage = GlobalFunction.ReturnEmptyString(model.getTaxcolumns.TaxPercentage);
            tblTax.TaxBase = GlobalFunction.ReturnEmptyString(model.getTaxcolumns.TaxBase);
            tblTax.GeneralAccountID = GlobalFunction.ReturnEmptyInt(model.GeneralAccountID);
            tblTax.isUsed = GlobalFunction.ReturnEmptyBool(model.isUsed);
            TOSSDB.Taxes.Add(tblTax);
            TOSSDB.SaveChanges();
            return Json(tblTax);
        }
        public ActionResult GetDynamicGeneralAccount()
        {
            FM_Taxes model = new FM_Taxes();
            model.TaxList = new SelectList((from s in TOSSDB.GeneralAccounts.ToList() orderby s.GeneralAccountName ascending select new { GeneralAccountID = s.GeneralAccountID, GeneralAccountName = s.GeneralAccountName }), "GeneralAccountID", "GeneralAccountName");
            return PartialView("_DynamicDDGeneralAccountName", model);
        }
        public ActionResult GetDynamicSelectedGeneralAccount(int GeneralAccountTempID)
        {
            FM_Taxes model = new FM_Taxes();
            model.TaxList = new SelectList((from s in TOSSDB.GeneralAccounts.ToList() orderby s.GeneralAccountName ascending select new { GeneralAccountID = s.GeneralAccountID, GeneralAccountName = s.GeneralAccountName }), "GeneralAccountID", "GeneralAccountName");
            model.GeneralAccountID = GeneralAccountTempID;
            return PartialView("_DynamicDDGeneralAccountName", model);
        }
        public ActionResult Get_UpdateTax(FM_Taxes model, int TaxID)
        {
            Tax tblTax = (from e in TOSSDB.Taxes where e.TaxID == TaxID select e).FirstOrDefault();
            model.getTaxcolumns.TaxID = tblTax.TaxID;
            model.getTaxcolumns.TaxDescription = tblTax.TaxDescription;
            model.getTaxcolumns.ShortDescription = tblTax.ShortDescription;
            model.getTaxcolumns.TaxPercentage = tblTax.TaxPercentage;
            model.getTaxcolumns.TaxBase = tblTax.TaxBase;
            model.GeneralAccountTempID = tblTax.GeneralAccountID;
            model.isUsed = tblTax.isUsed;
            return PartialView("_UpdateTaxes", model);
        }
        //Update Signature Data
        public ActionResult UpdateTax(FM_Taxes model)
        {
            Tax tblTax = (from e in TOSSDB.Taxes where e.TaxID == model.getTaxcolumns.TaxID select e).FirstOrDefault();
            tblTax.TaxDescription = GlobalFunction.ReturnEmptyString(model.getTaxcolumns.TaxDescription);
            tblTax.ShortDescription = GlobalFunction.ReturnEmptyString(model.getTaxcolumns.ShortDescription);
            tblTax.TaxPercentage = GlobalFunction.ReturnEmptyString(model.getTaxcolumns.TaxPercentage);
            tblTax.TaxBase = GlobalFunction.ReturnEmptyString(model.getTaxcolumns.TaxBase);
            tblTax.GeneralAccountID = GlobalFunction.ReturnEmptyInt(model.GeneralAccountID);
            tblTax.isUsed = GlobalFunction.ReturnEmptyBool(model.isUsed);
            TOSSDB.Entry(tblTax);
            TOSSDB.SaveChanges();
            return PartialView("_UpdateTaxes", model);
        }
        //Delete Signature Table
        public ActionResult DeleteTax(FM_Taxes model, int TaxID)
        {
            Tax tblTax = (from e in TOSSDB.Taxes where e.TaxID == TaxID select e).FirstOrDefault();
            TOSSDB.Taxes.Remove(tblTax);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}