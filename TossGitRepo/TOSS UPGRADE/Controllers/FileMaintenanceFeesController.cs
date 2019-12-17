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
using TOSS_UPGRADE.Models.GlobalClass;

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
        #region Field Type of Fees
        #region Field Fees
        //Table Field Fees
        public ActionResult Get_FieldFeeTable()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            List<FieldFeeList> tbl_Fees = new List<FieldFeeList>();

            var SQLQuery = "SELECT FieldFeeID,FieldFeeDescription,GeneralAccount.GeneralAccountID,Rate,FeeCategory.FeeCategoryID,SubFund.SubFundID FROM DB_TOSS.dbo.FieldFee,GeneralAccount,FeeCategory,SubFund where GeneralAccount.GeneralAccountID = FieldFee.GeneralAccountID AND FeeCategory.FeeCategoryID = FieldFee.FeeCategoryID AND SubFund.SubFundID = FieldFee.SubFundID";
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
                            FieldFeeID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            FieldFeeDescription = GlobalFunction.ReturnEmptyString(dr[1]),
                            Rate = GlobalFunction.ReturnEmptyInt(dr[3]),
                            AccountCode = GlobalFunction.ReturnEmptyInt(dr[2]),
                            FeeCategory = GlobalFunction.ReturnEmptyInt(dr[4]),
                            FundType = GlobalFunction.ReturnEmptyInt(dr[5]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getFieldFeeList = tbl_Fees.ToList();
            return PartialView("Fee/_FeesTable", model.getFieldFeeList);
        }
        //Get Add Field Fees Partial View
        public ActionResult Get_AddFieldFees()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            return PartialView("Fee/_AddFees", model);
        }
        public ActionResult GetDynamicFeeCategory()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FieldFeeList = new SelectList((from s in TOSSDB.FeeCategories.ToList() select new { FeeCategoryID = s.FeeCategoryID, FeeCategoryName = s.FeeCategoryName }), "FeeCategoryID", "FeeCategoryName");
            return PartialView("Fee/_DynamicDDFeeCategoryName", model);
        }
        public ActionResult GetDynamicRevisionYear()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FieldFeeList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            return PartialView("Fee/_DynamicDDRevisionYear", model);
        }
        public ActionResult GetDynamicFundType()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            var Acronym = "";
            foreach (var item in TOSSDB.SubFunds.ToList())
            {
                for (var i = 0; i < item.Fund.FundName.Length;)
                {
                    if (i == 0)
                    {
                        Acronym += item.Fund.FundName[i].ToString();
                    }
                    if (item.Fund.FundName[i] == ' ')
                    {
                        Acronym += item.Fund.FundName[i + 1].ToString();
                    }
                    i++;
                }
                model.globalClasses.fieldFeeDDs.Add(new FieldFeeDD
                {
                    SubFundID = item.SubFundID,
                    FundName = Acronym + " - " + item.SubFundName,
                });
                Acronym = "";

            }
            model.FieldFeeList = new SelectList(model.globalClasses.fieldFeeDDs.ToList(), "SubFundID", "FundName");

            return PartialView("Fee/_DynamicFundType", model);
        }
        public ActionResult GetDynamicGeneralAccountCode()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FieldFeeList = new SelectList((from s in TOSSDB.GeneralAccounts.ToList() select new { GeneralAccountID = s.GeneralAccountID, GeneralAccountCode = s.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AccountGroupCode + "-" + s.SubMajorAccountGroup.MajorAccountGroup.MajorAccountGroupCode + "-" + s.SubMajorAccountGroup.SubMajorAccountGroupCode + "-" + s.GeneralAccountCode + " - " + s.GeneralAccountName }), "GeneralAccountID", "GeneralAccountCode");
            return PartialView("Fee/_DynamicDDGeneralAccountCode", model);
        }
        //Get Add Field Fees
        public JsonResult AddFieldFee(FM_Fees_Fee model)
        {
            FieldFee tblFieldFee = new FieldFee();
            tblFieldFee.FieldFeeDescription = model.getFieldFeecolumns.FieldFeeDescription;
            tblFieldFee.Rate = model.getFieldFeecolumns.Rate;
            tblFieldFee.GeneralAccountID = model.AccountCodeID;
            tblFieldFee.SubFundID = model.FundTypeID;
            tblFieldFee.FeeCategoryID = model.FeeCategoryID;
            tblFieldFee.ORRequired = model.isRequired;
            TOSSDB.FieldFees.Add(tblFieldFee);
            TOSSDB.SaveChanges();
            return Json(tblFieldFee);
        }
        public ActionResult GetSelectedDynamicFeeCategory(int FeeCategoryTempID)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FieldFeeList = new SelectList((from s in TOSSDB.FeeCategories.ToList() select new { FeeCategoryID = s.FeeCategoryID, FeeCategoryName = s.FeeCategoryName }), "FeeCategoryID", "FeeCategoryName");
            model.FeeCategoryID = FeeCategoryTempID;
            return PartialView("Fee/_DynamicDDFeeCategoryName", model);
        }
        public ActionResult GetSelectedDynamicRevisionYear(int RevisionYearTempID)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FieldFeeList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            model.RevisionYearID = RevisionYearTempID;
            return PartialView("Fee/_DynamicDDRevisionYear", model);
        }
        public ActionResult GetSelectedDynamicFundType(int FundTypeTempID)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            var Acronym = "";
            foreach (var item in TOSSDB.SubFunds.ToList())
            {
                for (var i = 0; i < item.Fund.FundName.Length;)
                {
                    if (i == 0)
                    {
                        Acronym += item.Fund.FundName[i].ToString();
                    }
                    if (item.Fund.FundName[i] == ' ')
                    {
                        Acronym += item.Fund.FundName[i + 1].ToString();
                    }
                    i++;
                }
                model.globalClasses.fieldFeeDDs.Add(new FieldFeeDD
                {
                    SubFundID = item.SubFundID,
                    FundName = Acronym + " - " + item.SubFundName,
                });
                Acronym = "";

            }
            model.FieldFeeList = new SelectList(model.globalClasses.fieldFeeDDs.ToList(), "SubFundID", "FundName");
            model.FundTypeID = FundTypeTempID;
            return PartialView("Fee/_DynamicFundType", model);
        }
        public ActionResult GetSelectedDynamicGeneralAccountCode(int AccountCodeTempID)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FieldFeeList = new SelectList((from s in TOSSDB.GeneralAccounts.ToList() select new { GeneralAccountID = s.GeneralAccountID, GeneralAccountCode = s.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AccountGroupCode + "-" + s.SubMajorAccountGroup.MajorAccountGroup.MajorAccountGroupCode + "-" + s.SubMajorAccountGroup.SubMajorAccountGroupCode + "-" + s.GeneralAccountCode + " - " + s.GeneralAccountName }), "GeneralAccountID", "GeneralAccountCode");
            model.AccountCodeID = AccountCodeTempID;
            return PartialView("Fee/_DynamicDDGeneralAccountCode", model);
        }
        //Get Update Field Fees
        public ActionResult Get_UpdateFieldFee(FM_Fees_Fee model, int FieldFeeID)
        {
            FieldFee tblFieldFee = (from e in TOSSDB.FieldFees where e.FieldFeeID == FieldFeeID select e).FirstOrDefault();
            model.getFieldFeecolumns.FieldFeeID = tblFieldFee.FieldFeeID;
            model.getFieldFeecolumns.FieldFeeDescription = tblFieldFee.FieldFeeDescription;
            model.getFieldFeecolumns.Rate = tblFieldFee.Rate;
            model.AccountCodeTempID = tblFieldFee.GeneralAccountID;
            model.FundTypeTempID = tblFieldFee.SubFundID;
            model.FeeCategoryTempID = tblFieldFee.FeeCategoryID;
            model.RevisionYearTempID = tblFieldFee.GeneralAccount.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AllotmentClass.RevisionYear.RevisionYearID;
            model.isRequired = tblFieldFee.ORRequired;
            return PartialView("Fee/_UpdateFees", model);
        }
        public ActionResult UpdateFieldFee(FM_Fees_Fee model)
        {
            FieldFee tblFieldFee = (from e in TOSSDB.FieldFees where e.FieldFeeID == model.getFieldFeecolumns.FieldFeeID select e).FirstOrDefault();
            tblFieldFee.FieldFeeDescription = model.getFieldFeecolumns.FieldFeeDescription;
            tblFieldFee.Rate = model.getFieldFeecolumns.Rate;
            tblFieldFee.GeneralAccountID = model.AccountCodeID;
            tblFieldFee.SubFundID = model.FundTypeID;
            tblFieldFee.FeeCategoryID = model.FeeCategoryID;
            tblFieldFee.ORRequired = model.isRequired;
            TOSSDB.Entry(tblFieldFee);
            TOSSDB.SaveChanges();
            return PartialView("Fee/_UpdateFees", model);
        }
        //Delete Field Fees
        public ActionResult DeleteFieldFee(FM_Fees_Fee model, int FieldFeeID)
        {
            FieldFee tblFieldFees = (from e in TOSSDB.FieldFees where e.FieldFeeID == FieldFeeID select e).FirstOrDefault();
            TOSSDB.FieldFees.Remove(tblFieldFees);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Fee Category
        public ActionResult Get_AddFeeCategory()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            return PartialView("Fee/FeeCategory/_AddFeeCategory", model);
        }
        public ActionResult Get_FeeCategoryTable()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            List<FeeCategoryList> tbl_Fees = new List<FeeCategoryList>();

            var SQLQuery = "SELECT FeeCategoryID, FeeCategoryName,AF_Description.DescriptionName FROM DB_TOSS.dbo.FeeCategory,dbo.AF_Description where dbo.AF_Description.AFDescriptionID = dbo.FeeCategory.AFDescriptionID";
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
                            AFDescription = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getFeeCategoryList = tbl_Fees.ToList();
            return PartialView("Fee/FeeCategory/_FeeCategoryTable", model.getFeeCategoryList);
        }
        public JsonResult AddFeeCategory(FM_Fees_Fee model)
        {
            FeeCategory tblFeeCategory = new FeeCategory();
            tblFeeCategory.FeeCategoryName = model.getFeeCategorycolumns.FeeCategoryName;
            tblFeeCategory.AFDescriptionID = model.AFDescriptionID;
            TOSSDB.FeeCategories.Add(tblFeeCategory);
            TOSSDB.SaveChanges();
            return Json(tblFeeCategory);
        }
        //Dropdown Fund
        public ActionResult GetDynamicAccountableFormDescription()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FeeCategoryList = new SelectList((from s in TOSSDB.AF_Description.ToList() select new { AFDescriptionID = s.AFDescriptionID, DescriptionName = s.DescriptionName }), "AFDescriptionID", "DescriptionName");
            return PartialView("Fee/FeeCategory/_DynamicDDAccountableFormDescription", model);
        }
        public ActionResult GetSelectedDynamicAccountableFormDescription(int AccountFormTempID)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.FeeCategoryList = new SelectList((from s in TOSSDB.AF_Description.ToList() select new { AFDescriptionID = s.AFDescriptionID, DescriptionName = s.DescriptionName }), "AFDescriptionID", "DescriptionName");
            model.AFDescriptionID = AccountFormTempID;
            return PartialView("Fee/FeeCategory/_DynamicDDAccountableFormDescription", model);
        }
        //Get Update Internal Revenue Allotment
        public ActionResult Get_UpdateFeeCategory(FM_Fees_Fee model, int FeeCategoryID)
        {
            FeeCategory tblFeeCategory = (from e in TOSSDB.FeeCategories where e.FeeCategoryID == FeeCategoryID select e).FirstOrDefault();
            model.getFeeCategorycolumns.FeeCategoryID = tblFeeCategory.FeeCategoryID;
            model.getFeeCategorycolumns.FeeCategoryName = tblFeeCategory.FeeCategoryName;
            model.AccountFormTempID = tblFeeCategory.AFDescriptionID;
            return PartialView("Fee/FeeCategory/_UpdateFeeCategory", model);
        }
        //Update Internal Revenue Allotment
        public ActionResult UpdateFeeCategory(FM_Fees_Fee model)
        {
            FeeCategory tblFeeCategory = (from e in TOSSDB.FeeCategories where e.FeeCategoryID == model.getFeeCategorycolumns.FeeCategoryID select e).FirstOrDefault();
            tblFeeCategory.FeeCategoryName = model.getFeeCategorycolumns.FeeCategoryName;
            tblFeeCategory.AFDescriptionID = model.AFDescriptionID;
            TOSSDB.Entry(tblFeeCategory);
            TOSSDB.SaveChanges();
            return PartialView("Fee/FeeCategory/_UpdateFeeCategory", model);
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
        #endregion
        #region Particulars
        //Table Field Fees
        public ActionResult Get_ParticularTable()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            List<ParticularList> tbl_Fees = new List<ParticularList>();

            var SQLQuery = "SELECT ParticularID,GeneralAccount.GeneralAccountID,NatureOfParticular.NatureofParticularName FROM DB_TOSS.dbo.Particulars,GeneralAccount,NatureOfParticular where GeneralAccount.GeneralAccountID = Particulars.GeneralAccountID AND NatureOfParticular.NatureofParticularID = Particulars.NatureofParticularID";
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
                        tbl_Fees.Add(new ParticularList()
                        {
                            ParticularID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            GeneralAccount = GlobalFunction.ReturnEmptyInt(dr[1]),
                            ParticularName = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getParticularList = tbl_Fees.ToList();
            return PartialView("Particulars/_ParticularsTable", model.getParticularList);
        }
        //Get Add Field Fees Partial View
        public ActionResult Get_AddParticular()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            return PartialView("Particulars/_AddParticulars", model);
        }
        public ActionResult GetDynamicRevisionYearCT()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.ParticularList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            return PartialView("Particulars/_DynamicDDRevisionYear", model);
        }
        public ActionResult GetDynamicGeneralAccountCodeCT(int RevisionYearID)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.ParticularList = new SelectList((from s in TOSSDB.GeneralAccounts.ToList() where s.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AllotmentClass.RevisionYear.RevisionYearID == RevisionYearID select new { GeneralAccountID = s.GeneralAccountID, GeneralAccountCode = s.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AccountGroupCode + "-" + s.SubMajorAccountGroup.MajorAccountGroup.MajorAccountGroupCode + "-" + s.SubMajorAccountGroup.SubMajorAccountGroupCode + "-" + s.GeneralAccountCode + " - " + s.GeneralAccountName }), "GeneralAccountID", "GeneralAccountCode");
            return PartialView("Particulars/_DynamicDDGeneralAccountCode", model);
        }
        public ActionResult GetDynamicNatureOfParticularCT()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.ParticularList = new SelectList((from s in TOSSDB.NatureOfParticulars.ToList() select new { NatureofParticularID = s.NatureofParticularID, NatureofParticularName = s.NatureofParticularName }), "NatureofParticularID", "NatureofParticularName");
            return PartialView("Particulars/_DynamicDDNatureOfParticular", model);
        }
        public JsonResult AddParticular(FM_Fees_Fee model)
        {
            Particular tblParticular = new Particular();
            tblParticular.NatureofParticularID = model.NatureOfParticularIDCT;
            tblParticular.GeneralAccountID = model.GeneralAccountIDCT;
            TOSSDB.Particulars.Add(tblParticular);
            TOSSDB.SaveChanges();
            return Json(tblParticular);
        }
        public ActionResult GetSelectedDynamicRevisionYearCT(int RevisionYearTempIDCT)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.ParticularList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            model.RevisionYearTempID = RevisionYearTempIDCT;
            return PartialView("Particulars/_DynamicDDRevisionYear", model);
        }
        public ActionResult GetSelectedDynamicGeneralAccountCodeCT(int RevisionYearID, int GeneralAccountTempIDCT)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.ParticularList = new SelectList((from s in TOSSDB.GeneralAccounts.ToList() where s.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AllotmentClass.RevisionYear.RevisionYearID == RevisionYearID select new { GeneralAccountID = s.GeneralAccountID, GeneralAccountCode = s.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AccountGroupCode + "-" + s.SubMajorAccountGroup.MajorAccountGroup.MajorAccountGroupCode + "-" + s.SubMajorAccountGroup.SubMajorAccountGroupCode + "-" + s.GeneralAccountCode + " - " + s.GeneralAccountName }), "GeneralAccountID", "GeneralAccountCode");
            model.GeneralAccountIDCT = GeneralAccountTempIDCT;
            return PartialView("Particulars/_DynamicDDGeneralAccountCode", model);
        }
        public ActionResult GetSelectedDynamicNatureOfParticularCT(int NatureOfParticularTempIDCT)
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            model.ParticularList = new SelectList((from s in TOSSDB.NatureOfParticulars.ToList() select new { NatureofParticularID = s.NatureofParticularID, NatureofParticularName = s.NatureofParticularName }), "NatureofParticularID", "NatureofParticularName");
            model.NatureOfParticularIDCT = NatureOfParticularTempIDCT;
            return PartialView("Particulars/_DynamicDDNatureOfParticular", model);
        }
        //Get Update Field Fees
        public ActionResult Get_UpdateParticular(FM_Fees_Fee model, int ParticularID)
        {
            Particular tblParticular = (from e in TOSSDB.Particulars where e.ParticularID == ParticularID select e).FirstOrDefault();
            model.getParticularcolumns.ParticularID = tblParticular.ParticularID;
            model.NatureOfParticularTempIDCT = tblParticular.NatureofParticularID;
            model.GeneralAccountTempIDCT = tblParticular.GeneralAccountID;
            return PartialView("Particulars/_UpdateParticulars", model);
        }
        public ActionResult UpdateParticular(FM_Fees_Fee model)
        {
            Particular tblParticular = (from e in TOSSDB.Particulars where e.ParticularID == model.getParticularcolumns.ParticularID select e).FirstOrDefault();
            tblParticular.NatureofParticularID = model.NatureOfParticularIDCT;
            tblParticular.GeneralAccountID = model.GeneralAccountIDCT;
            TOSSDB.Entry(tblParticular);
            TOSSDB.SaveChanges();
            return PartialView("Particulars/_UpdateParticulars", model);
        }
        //Delete Field Fees
        public ActionResult DeleteParticular(FM_Fees_Fee model, int ParticularID)
        {
            Particular tblParticular = (from e in TOSSDB.Particulars where e.ParticularID == ParticularID select e).FirstOrDefault();
            TOSSDB.Particulars.Remove(tblParticular);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #region Nature Of Particulars
        //Table Nature Of Particulars
        public ActionResult Get_NatureOfParticularTable()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            List<NatureofParticularList> tbl_Fees = new List<NatureofParticularList>();

            var SQLQuery = "SELECT NatureofParticularID,NatureofParticularName FROM DB_TOSS.dbo.NatureOfParticular";
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
                        tbl_Fees.Add(new NatureofParticularList()
                        {
                            NatureofParticularID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            NatureofParticularName = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getNatureofParticularList = tbl_Fees.ToList();
            return PartialView("Particulars/NatureOfParticular/_NatureOfParticularTable", model.getNatureofParticularList);
        }
        //Get Add Nature Of Particulars Partial View
        public ActionResult Get_AddNatureOfParticular()
        {
            FM_Fees_Fee model = new FM_Fees_Fee();
            return PartialView("Particulars/NatureOfParticular/_AddNatureOfParticular", model);
        }
        public JsonResult AddNatureOfParticular(FM_Fees_Fee model)
        {
            NatureOfParticular tblNatureOfParticular = new NatureOfParticular();
            tblNatureOfParticular.NatureofParticularName = model.getNatureofParticularcolumns.NatureofParticularName;
            TOSSDB.NatureOfParticulars.Add(tblNatureOfParticular);
            TOSSDB.SaveChanges();
            return Json(tblNatureOfParticular);
        }
        //Get Update Field Fees
        public ActionResult Get_UpdateNatureOfParticular(FM_Fees_Fee model, int NatureofParticularID)
        {
            NatureOfParticular tblNatureOfParticulars = (from e in TOSSDB.NatureOfParticulars where e.NatureofParticularID == NatureofParticularID select e).FirstOrDefault();
            model.getNatureofParticularcolumns.NatureofParticularID = tblNatureOfParticulars.NatureofParticularID;
            model.getNatureofParticularcolumns.NatureofParticularName = tblNatureOfParticulars.NatureofParticularName;
            return PartialView("Particulars/NatureOfParticular/_UpdateNatureOfParticular", model);
        }
        public ActionResult UpdateNatureOfParticular(FM_Fees_Fee model)
        {
            NatureOfParticular tblNatureOfParticular = (from e in TOSSDB.NatureOfParticulars where e.NatureofParticularID == model.getNatureofParticularcolumns.NatureofParticularID select e).FirstOrDefault();
            tblNatureOfParticular.NatureofParticularName = model.getNatureofParticularcolumns.NatureofParticularName;
            TOSSDB.Entry(tblNatureOfParticular);
            TOSSDB.SaveChanges();
            return PartialView("Particulars/NatureOfParticular/_UpdateNatureOfParticular", model);
        }
        //Delete Field Fees
        public ActionResult DeleteNatureOfParticular(FM_Fees_Fee model, int NatureofParticularID)
        {
            NatureOfParticular tblNatureOfParticular = (from e in TOSSDB.NatureOfParticulars where e.NatureofParticularID == NatureofParticularID select e).FirstOrDefault();
            TOSSDB.NatureOfParticulars.Remove(tblNatureOfParticular);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #endregion
    }
}