﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TOSS_UPGRADE.Models.GlobalClass;

namespace TOSS_UPGRADE.Models.FM_AccountableForm
{
    public class FM_AccountableFormAssignment
    {
        public FM_AccountableFormAssignment()
        {
            getAccountableFormAssign = new List<AccountableForm_Assignment>();
            getAccountableFormAssigncolumns = new AccountableForm_Assignment();
            getAccountableFormAssList = new List<AccountableFormAssignmentList>();

            getAFTransferReturnORAssign = new List<AccountableForm_Assignment>();
            getAFTransferReturnORAssigncolumns = new AccountableForm_Assignment();
            getAFTransferReturnORList = new List<AFTransferReturnORList>();
            globalClasses = new GlobalClasses();
        }
        public AccountableForm_Assignment getAccountableFormAssigncolumns { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AccountableForm_Assignment> getAccountableFormAssign { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AccountableFormAssignmentList { get; set; }
        public List<AccountableFormAssignmentList> getAccountableFormAssList { get; set; }
        public GlobalClasses globalClasses { get; set;  }
        public AccountableForm_Assignment getAFTransferReturnORAssigncolumns { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AccountableForm_Assignment> getAFTransferReturnORAssign { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AFTransferReturnORList { get; set; }
        public List<AFTransferReturnORList> getAFTransferReturnORList { get; set; }
        public int AccountableFormAssignmentID { get; set; }
        public int AccountableFormAssignmentAFID { get; set; }
        public int AccountableTCTRORAssignAFID { get; set; }
        public int AccountableTCTRORID { get; set; }
        public int AccountableTCTRORMainCID { get; set; }
        public string AccountableTCTRORMainName { get; set; }

        public int AccountableTCTRORSubCID { get; set; }
        public int AccountableTCTRORStubNoID { get; set; }

        public string AccountableTCTROROrDescID { get; set; }
        public int AccountableTCTRORQuantityID { get; set; }
        public int AccountableTCTRORStartingORID { get; set; }
        public int AccountableTCTROREndingORID { get; set; }

        public int AccountableFACollectorID { get; set; }
        public int AccountableFormAssignmentFundID { get; set; }
        public int AccountableFormAssignmentBarangayID { get; set; }
        public int AccountableFormAssignmentTypeofFeeID { get; set; }
        public int AccountableFormAssignmentCashTicketValueID { get; set; }
        public int AccountableFormAssignmentEndingORID { get; set; }
        public int AccountableFormAssignmentQuantityID { get; set; }
        public int AccountableFormAssignmentStubNoID { get; set; }
        public int AccountableFormAssignmentTotalAmountID { get; set; }
        public int AFORID { get; set; }

    }
    public class AccountableFormAssignmentList
    {
        public int AssignAFID { get; set; }
        public string CollectorName { get; set; }
        public int SubFundID { get; set; }
        public string AF { get; set; }
        public int StubNo { get; set; }
        public int Quantity { get; set; }
        public int StratingOR { get; set; }
        public int EndingOR { get; set; }
        public int TotalAmount { get; set; }
        public string FieldFee { get; set; }
        public string BarangayName { get; set; }
        public string DateIssued { get; set; }
        public string DateTransfered { get; set; }
        public bool isConsumed { get; set; }
        public bool isDefault { get; set; }
    }
    public class AFTransferReturnORList
    {
        public int AssignAFID { get; set; }
        public string CollectorName { get; set; }
        public int SubFundID { get; set; }
        public string AF { get; set; }
        public int StubNo { get; set; }
        public int Quantity { get; set; }
        public int StratingOR { get; set; }
        public int EndingOR { get; set; }
        public string DateIssued { get; set; }
        public string DateTransferred { get; set; }
        public string SubCollector { get; set; }
        public bool IsTransferred { get; set; }
    }
}