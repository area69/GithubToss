//Accountable Form
app.controller('FMAccountableFormController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.AccountableFormTab = function () {
        F.tab1 = null;
        P({
            url: '/FileMaintenanceAccountableForm/AccountableFormTab',
        }).then(function (response) {
            F.tab1 = D(response.data);
            F.Get_AccountableFormTable();
        });
    }
    F.InventoryofAccountableFormTab = function () {
        F.tab2 = null;
        P({
            url: '/FileMaintenanceAccountableForm/InventoryofAccountableFormTab',
        }).then(function (response) {
            F.tab2 = D(response.data)
        });
    }
    F.AssignmentofAccountableFormTab = function () {
        F.tab3 = null;
        P({
            url: '/FileMaintenanceAccountableForm/AssignmentofAccountableFormTab',
        }).then(function (response) {
            F.tab3 = D(response.data);
        });
    }
    //Accountable Form
    F.Get_AccountableFormTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AccountableFormTable',
        }).then(function (response) {
            $("#_AccountableFormTableID").html(response.data);
        });
        F.Get_AddAccountableForm();
    }
    F.Get_AddAccountableForm = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAccountableForm',
        }).then(function (response) {
            $("#_AccountableFormID").html(response.data);
            F.LoadDynamicDDDescriptionName();
        });
    }
    F.LoadDynamicDDDescriptionName = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAccountableFormDescription',
        }).then(function (response) {
            $("#_DescriptionID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDDescriptionName = function () {
        var DescriptionTempID = $("#DescriptionTempID").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetSelectedDynamicAccountableFormDescription',
            params: { DescriptionTempID: DescriptionTempID }
        }).then(function (response) {
            $("#_DescriptionID").html(response.data);
        });
    }
    F.Get_UpdateAccountableForm = function (AccountFormID) {
        P({
            url: '/FileMaintenanceAccountableForm/Get_UpdateAccountableForm',
            params: { AccountFormID: AccountFormID }
        }).then(function (response) {
            $("#_AccountableFormID").html(response.data);
            F.LoadSelectedDynamicDDDescriptionName();
        });
    }
    F.Get_DeleteAccountableForm = function (AccountFormID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteAccountableForm',
            params: { AccountFormID: AccountFormID }
        }).then(function (response) {
            F.Get_AccountableFormTable();
        });
    }

    F.Get_AFDescriptionTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AFDescriptionTable',
        }).then(function (response) {
            $("#_DescriptionTableID").html(response.data);
        });
        F.Get_AddDescription();
        F.LoadDynamicDDDescriptionName();
    }
    F.Get_AddDescription = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddDescription',
        }).then(function (response) {
            $("#_DescriptionxID").html(response.data);
        });
    }
    F.Get_UpdateDescription = function (AFDescriptionID) {
        P({
            url: '/FileMaintenanceAccountableForm/Get_UpdateDescription',
            params: { AFDescriptionID: AFDescriptionID }
        }).then(function (response) {
            $("#_DescriptionxID").html(response.data);
        });
    }
    F.Get_DeleteDescription = function (AFDescriptionID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteDescription',
            params: { AFDescriptionID: AFDescriptionID }
        }).then(function (response) {
            F.Get_AFDescriptionTable();
            F.LoadDynamicDDDescriptionName();
        });
    }

    //Inventory of Accountable Forms
    //OR
    F.Get_InvtAccountableFormTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AccountableFormInvtTable',
        }).then(function (response) {
            $("#_ORDetailsTableID").html(response.data);
        });
        F.Get_AddInvtAccountableForm();
    }
    F.Get_AddInvtAccountableForm = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddInvntAccountableForm',
        }).then(function (response) {
            $("#_ORDetailsID").html(response.data);
            F.LoadDynamicDDAccountableForm();
        });
    }
    F.LoadDynamicDDAccountableForm = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAccountableform',
        }).then(function (response) {
            $("#_ORAFID").html(response.data);
        });
    }
    F.Get_UpdateAccountableFormInventory = function (AFORID) {
        P({
            url: '/FileMaintenanceAccountableForm/Get_UpdateAccountableFormInventory',
            params: { AFORID: AFORID }
        }).then(function (response) {
            $("#_ORDetailsID").html(response.data);
            F.LoadSelectedDynamicDDAF();
        });
    }
    F.LoadSelectedDynamicDDAF = function () {
        var AFIDTempID = $("#AFIDTempID").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetSelectedDynamicAccountableform',
            params: { AFIDTempID: AFIDTempID }
        }).then(function (response) {
            $("#_ORAFID").html(response.data);
        });
    }
    F.Get_DeleteAccountableFormInventory = function (AFORID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteAccountableFormInventory',
            params: { AFORID: AFORID }
        }).then(function (response) {
            F.Get_InvtAccountableFormTable();
        });
    }
    //CT
    F.Get_AddInvtAccountableFormCT = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddInvntAccountableFormCT',
        }).then(function (response) {
            $("#_CTDetailsID").html(response.data);
            F.LoadDynamicDDAccountableFormCT();
            //F.LoadDynamicDDASFAF();
        });
    }
    F.LoadDynamicDDAccountableFormCT = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAccountableformCT',
        }).then(function (response) {
            $("#_CTAFID").html(response.data);
        });
    }
    F.Get_InvtAccountableFormTableCT = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AccountableFormInvtTableCT',
        }).then(function (response) {
            $("#_CTDetailsTableID").html(response.data);
        });
        F.Get_AddInvtAccountableFormCT();
    }
    F.LoadSelectedDynamicDDAFCT = function () {
        var AFIDTempIDCT = $("#AFIDTempIDCT").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetSelectedDynamicAccountableformCT',
            params: { AFIDTempIDCT: AFIDTempIDCT }
        }).then(function (response) {
            $("#_CTAFID").html(response.data);
        });
    }
    F.Get_UpdateAccountableFormInventoryCT = function (AFORID) {
        P({
            url: '/FileMaintenanceAccountableForm/Get_UpdateAccountableFormInventoryCT',
            params: { AFORID: AFORID }
        }).then(function (response) {
            $("#_CTDetailsID").html(response.data);
            F.LoadSelectedDynamicDDAFCT();
        });
    }
    F.Get_DeleteAccountableFormInventoryCT = function (AFORID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteAccountableFormInventoryCT',
            params: { AFORID: AFORID }
        }).then(function (response) {
            F.Get_InvtAccountableFormTableCT();
        });
    }
    //CTC
    F.Get_InvtAccountableFormTableCTC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AccountableFormInvtTableCTC',
        }).then(function (response) {
            $("#_CTCDetailsTableID").html(response.data);
        });
        F.Get_AddInvtAccountableFormCTC();
    }
    F.Get_AddInvtAccountableFormCTC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddInvntAccountableFormCTC',
        }).then(function (response) {
            $("#_CTCDetailsID").html(response.data);
            F.LoadDynamicDDAccountableFormCTC();
        });
    }
    F.LoadDynamicDDAccountableFormCTC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAccountableformCTC',
        }).then(function (response) {
            $("#_CTCAFID").html(response.data);
        });
    }
    F.Get_UpdateAccountableFormInventoryCTC = function (AFORID) {
        P({
            url: '/FileMaintenanceAccountableForm/Get_UpdateAccountableFormInventoryCTC',
            params: { AFORID: AFORID }
        }).then(function (response) {
            $("#_CTCDetailsID").html(response.data);
            F.LoadSelectedDynamicDDAFCTC();
        });
    }
    F.LoadSelectedDynamicDDAFCTC = function () {
        var AFIDTempIDCTC = $("#AFIDTempIDCTC").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetSelectedDynamicAccountableformCTC',
            params: { AFIDTempIDCTC: AFIDTempIDCTC }
        }).then(function (response) {
            $("#_CTCAFID").html(response.data);
        });
    }
    F.Get_DeleteAccountableFormInventoryCTC = function (AFORID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteAccountableFormInventoryCTC',
            params: { AFORID: AFORID }
        }).then(function (response) {
            F.Get_InvtAccountableFormTableCTC();
        });
    }

    //Assignment of Accountable Forms
    //Treasurer Collector
    F.Get_AddAssignTreasurerCollector = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignTreasurerCollector',
        }).then(function (response) {
            $("#_TreasurerCollectionID").html(response.data);
            F.LoadDynamicAFCollector();
            F.LoadDynamicDDASFFundType();
            F.LoadDynamicDDASFAF();
            F.Get_InvtAccountableFormTable();
        });
    }
    F.LoadDynamicDDASFFundType = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicASFFundType',
        }).then(function (response) {
            $("#_DynamicFundTypeASF").html(response.data);
        });
    }
    F.LoadDynamicAFCollector = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFCollector',
        }).then(function (response) {
            $("#_DynamicCollectorTable").html(response.data);
        });
    }
    F.LoadDynamicDDASFAF = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFASF',
        }).then(function (response) {
            $("#_DynamicAFASF").html(response.data);
            F.LoadDynamicASFStartOR();
        });
    }
    F.LoadDynamicASFStartOR = function () {
        //var AFORID = $("#AFIDDDASF").val()
        //P({
        //    url: '/FileMaintenanceAccountableForm/GetAccountFormID',
        //    params: { AFORID: AFORID}
        //}).then(function (response) {
        //    var AccountFormIDT = response.data.AccountFormID;
        //    P({
        //        url: '/FileMaintenanceAccountableForm/GetDynamicAFStartOR',
        //        params: { AccountFormID: AccountFormIDT }
        //    }).then(function (response) {
        //        $("#_DynamicAFStartOR").html(response.data);
        //        F.LoadDynamicDDASFInvt();
        //    });
        //});
        var AccountFormID = $("#AFIDDDASF").val();

        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFStartOR',
            params: { AccountFormID: AccountFormID }
        }).then(function (response) {
            $("#_DynamicAFStartOR").html(response.data);
            F.LoadDynamicDDASFInvt();
        });
    }
    F.LoadDynamicDDASFInvt = function () {
        var AFORID = $("#StartIDDDD option:selected").val();
        if (AFORID == null || AFORID == "") {
            AFORID = 0;
        }
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignTC',
            params: { AFORID: AFORID }
        }).then(function (response) {
            $("#_DynamicASF").html(response.data);
        });
    }
    F.Get_AccountableFormAssignmentTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AccountableFormAssignmentTable',
        }).then(function (response) {
            $("#_TreasurerCollectionTableID").html(response.data);
        });
        F.Get_AddAssignTreasurerCollector();
    }

    F.Get_TransferReturnORTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_TransferReturnORTable',
        }).then(function (response) {
            $("#_TransferReturnORTableID").html(response.data);
        });
    }
    F.Get_DataTransferReturnOR = function (AssignAFID) {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddTransferReturnOR',
            params: { AssignAFID: AssignAFID },
        }).then(function (response) {
            $("#_TransferReturnORID").html(response.data);
            F.Get_TransferReturnORTable();
            F.LoadDynamicMainCollector();
        });
    }
    F.LoadDynamicMainCollector = function () {
        var AssignAFID = $("#AssignAFIDDDD").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicMainCollector',
            params: { AssignAFID: AssignAFID },
        }).then(function (response) {
            $("#_DynamicMainCollectorID").html(response.data);
            F.LoadDynamicSubCollector();
            F.LoadDynamicTCTRORStubNo();
        });
    }
    F.LoadDynamicSubCollector = function () {
        var CollectorID = $("#MainCOIDDD").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicSubCollector',
            params: { CollectorID: CollectorID }
        }).then(function (response) {
            $("#_DynamicSubCollectorID").html(response.data);
        });
    }
    F.LoadDynamicTCTRORStubNo = function () {
        var CollectorID = $("#MainCOIDDD").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicTCTRORStubNo',
            params: { CollectorID: CollectorID }
        }).then(function (response) {
            $("#_DynamicStubNoID").html(response.data);
            F.LoadDynamicDDTRPVOR();
        });
    }
    F.LoadDynamicDDTRPVOR = function () {
        var AssignAFID = $("#AssignAFIDDDD").val();
        if (AssignAFID == null || AssignAFID == "") {
            AssignAFID = 0;
        }
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicTCTRORPV',
            params: { AssignAFID: AssignAFID }
        }).then(function (response) {
            $("#_TransferReturnORIDPV").html(response.data);
        });
    }
    F.Get_UpdateTransferReturnOR = function (AssignAFID) {
        P({
            url: '/FileMaintenanceAccountableForm/Get_UpdateAFTransferReturnOR',
            params: { AssignAFID: AssignAFID },
        }).then(function (response) {
            F.Get_TransferReturnORTable();
            F.Get_AccountableFormAssignmentTable();
        });
    }
    F.Get_DeleteAccountableFormAssignmentTC = function (AssignAFID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteAccountableFormAssignmentTC',
            params: { AssignAFID: AssignAFID }
        }).then(function (response) {
            F.Get_AccountableFormAssignmentTable();
        });
    }

    //Field Collector
    F.Get_AddAssignFieldCollector = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignFieldCollector',
        }).then(function (response) {
            $("#_FieldCollectionID").html(response.data);
            F.LoadDynamicAFCollectorFC();
            F.LoadDynamicDDASFFundTypeFC();
            F.LoadDynamicDDASFAFFC();
            F.Get_InvtAccountableFormTable();
            F.Get_AddAssignTreasurerCollector();
        });
    }
    F.LoadDynamicDDASFFundTypeFC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicASFFundTypeFC',
        }).then(function (response) {
            $("#_FCFundID").html(response.data);
        });
    }
    F.LoadDynamicAFCollectorFC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFCollectorFC',
        }).then(function (response) {
            $("#_FCCollectorID").html(response.data);
        });
    }
    F.LoadDynamicDDASFAFFC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFASFFC',
        }).then(function (response) {
            $("#_FCAFID").html(response.data);
            F.LoadDynamicASFStartORFC();
        });
    }
    F.LoadDynamicASFStartORFC = function () {
        //var AFORID = $("#AFIDDDASF").val()
        //P({
        //    url: '/FileMaintenanceAccountableForm/GetAccountFormID',
        //    params: { AFORID: AFORID}
        //}).then(function (response) {
        //    var AccountFormIDT = response.data.AccountFormID;
        //    P({
        //        url: '/FileMaintenanceAccountableForm/GetDynamicAFStartOR',
        //        params: { AccountFormID: AccountFormIDT }
        //    }).then(function (response) {
        //        $("#_DynamicAFStartOR").html(response.data);
        //        F.LoadDynamicDDASFInvt();
        //    });
        //});
        var AccountFormID = $("#AFIDDDASFFC").val();

        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFStartORFC',
            params: { AccountFormID: AccountFormID }
        }).then(function (response) {
            $("#_FCStartORID").html(response.data);
            F.LoadDynamicDDASFInvtFC();
        });
    }
    F.LoadDynamicDDASFInvtFC = function () {
        var AFORID = $("#StartIDDDDFC option:selected").val();
        if (AFORID == null || AFORID == "") {
            AFORID = 0;
        }
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignAFFC',
            params: { AFORID: AFORID }
        }).then(function (response) {
            $("#_DynamicASFFC").html(response.data);
        });
    }
    F.Get_AccountableFormAssignmentFCTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AFAssignmentFieldCollectorTable',
        }).then(function (response) {
            $("#_FieldCollectionTableID").html(response.data);
        });
        F.Get_AddAssignFieldCollector();
    }
    F.Get_UpdateAFSetAsDefault = function (AssignAFID) {
        P({
            url: '/FileMaintenanceAccountableForm/Get_UpdateAFAssignFieldCollector',
            params: { AssignAFID: AssignAFID },
        }).then(function (response) {
            F.Get_AccountableFormAssignmentFCTable();
        });
    }
    F.Get_DeleteAccountableFormAssignmentFC = function (AssignAFID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteAccountableFormAssignmentFC',
            params: { AssignAFID: AssignAFID }
        }).then(function (response) {
            F.Get_AccountableFormAssignmentFCTable();
        });
    }

    //Cash Ticket
    F.Get_AddAssignCashTicket = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignCashTicket',
        }).then(function (response) {
            $("#_CashTicketID").html(response.data);
            F.LoadDynamicAFCollectorCT();
            F.LoadDynamicDDASFFundTypeCT();
            F.LoadDynamicDDASFAFCT();

            //F.Get_InvtAccountableFormTable();
            //F.Get_AddAssignTreasurerCollector();
        });
    }
    F.LoadDynamicAFCollectorCT = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFCollectorCT',
        }).then(function (response) {
            $("#_CTCollectorID").html(response.data);
        });
    }
    F.LoadDynamicDDASFFundTypeCT = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicASFFundTypeCT',
        }).then(function (response) {
            $("#_CTFundID").html(response.data);
            F.LoadDynamicTypeofFee();
        });
    }
    F.LoadDynamicTypeofFee = function () {
        var SubFundID = $("#FundTypeIDDDCT").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicTypeofFee',
            params: { SubFundID: SubFundID }
        }).then(function (response) {
            $("#_CTTypeofFeeID").html(response.data);
        });
    }
    F.LoadDynamicDDASFAFCT = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFASFCT',
        }).then(function (response) {
            $("#_CTAFDDID").html(response.data);
            F.LoadDynamicASFStartCTNo();
        });
    }
    F.LoadDynamicASFStartCTNo = function () {
        var AccountFormID = $("#AFIDDDASFCT").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFStartCTNo',
            params: { AccountFormID: AccountFormID }
        }).then(function (response) {
            $("#_CTStartCTNoIDDD").html(response.data);
            F.LoadDynamicDDASFInvtCT();
        });
    }
    F.LoadDynamicDDASFInvtCT = function () {
        var AFORID = $("#StartIDDDDCT option:selected").val();
        if (AFORID == null || AFORID == "") {
            AFORID = 0;
        }
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignAFCT',
            params: { AFORID: AFORID }
        }).then(function (response) {
            $("#_CTInventorAFIDD").html(response.data);
        });
    }
    F.Get_AFAssignmentCashTicketTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AFAssignmentCashTicketTable',
        }).then(function (response) {
            $("#_CastTicketTableID").html(response.data);
        });
        F.Get_AddAssignCashTicket();
    }
    F.DeleteAccountableFormAssignCashTicket = function (AssignAFID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteAccountableFormAssignCashTicket',
            params: { AssignAFID: AssignAFID }
        }).then(function (response) {
            F.Get_AFAssignmentCashTicketTable();
        });
    }

    //Barangay Collector
    F.Get_AFAssignmentBrgyCollectorTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AFAssignmentBarangayCollectorTable',
        }).then(function (response) {
            $("#_BrgyCollectorTableID").html(response.data);
        });
        F.Get_AddAssignBrgyCollector();
    }
    F.Get_AddAssignBrgyCollector = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignBarangayCollector',
        }).then(function (response) {
            $("#_BrgyCollectorID").html(response.data);
            F.LoadDynamicAFBarangay();
            F.LoadDynamicAFCollectorBC();
            F.LoadDynamicDDASFFundTypeBC();
            F.LoadDynamicDDASFAFBC();
        });
    }
    F.LoadDynamicAFBarangay = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFBarangayName',
        }).then(function (response) {
            $("#_BarangayNameIDBC").html(response.data);
        });
    }
    F.LoadDynamicAFCollectorBC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFCollectorBC',
        }).then(function (response) {
            $("#_CollectorIDBC").html(response.data);
        });
    }
    F.LoadDynamicDDASFFundTypeBC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicASFFundTypeBC',
        }).then(function (response) {
            $("#_FundTypeIDBC").html(response.data);
            F.LoadDynamicTypeofFeeBC();
        });
    }
    F.LoadDynamicTypeofFeeBC = function () {
        var SubFundID = $("#FundTypeIDDDBC").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicTypeofFeeBC',
            params: { SubFundID: SubFundID }
        }).then(function (response) {
            $("#_TypeofFeeIDBC").html(response.data);
        });
    }
    F.LoadDynamicDDASFAFBC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFASFBC',
        }).then(function (response) {
            $("#_AccountFormIDBC").html(response.data);
            F.LoadDynamicASFStartORBC();
        });
    }
    F.LoadDynamicASFStartORBC = function () {
        var AccountFormID = $("#AFIDDDASFBC").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFStartORNo',
            params: { AccountFormID: AccountFormID }
        }).then(function (response) {
            $("#_StartORIDBC").html(response.data);
            F.LoadDynamicDDASFInvtBC();
        });
    }
    F.LoadDynamicDDASFInvtBC = function () {
        var AFORID = $("#StartIDDDDBC option:selected").val();
        if (AFORID == null || AFORID == "") {
            AFORID = 0;
        }
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignAFBC',
            params: { AFORID: AFORID }
        }).then(function (response) {
            $("#_AFBrgyInvIDBC").html(response.data);
        });
    }
    F.DeleteAccountableFormAssignBrgyCollector = function (AssignAFID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteAccountableFormAssignBrgyCollectors',
            params: { AssignAFID: AssignAFID }
        }).then(function (response) {
            F.Get_AFAssignmentBrgyCollectorTable();
        });
    }

    //Community Tax Certificate Collectors
    F.Get_AFAssignmentCTCCollectorTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AFAssignmentCTCCollectorTable',
        }).then(function (response) {
            $("#_CTCCollectorTableID").html(response.data);
        });
        F.Get_AddAssignCTCCollector();
    }
    F.Get_AddAssignCTCCollector = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignCTCCollector',
        }).then(function (response) {
            $("#_CTCCollectorID").html(response.data);
            F.LoadDynamicAFCollectorCTC();
            F.LoadDynamicDDASFFundTypeCTC();
            F.LoadDynamicDDASFAFCTC();
        });
    }
    F.LoadDynamicAFCollectorCTC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFCollectorCTC',
        }).then(function (response) {
            $("#_CollectorIDCTC").html(response.data);
        });
    }
    F.LoadDynamicDDASFFundTypeCTC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicASFFundTypeCTC',
        }).then(function (response) {
            $("#_FundTypeIDCTC").html(response.data);
            F.LoadDynamicTypeofFeeCTC();
        });
    }
    F.LoadDynamicTypeofFeeCTC = function () {
        var SubFundID = $("#FundTypeIDDDASFCTC").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicTypeofFeeCTC',
            params: { SubFundID: SubFundID }
        }).then(function (response) {
            $("#_TypeofFeeIDCTC").html(response.data);
        });
    }
    F.LoadDynamicDDASFAFCTC = function () {
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFASFCTC',
        }).then(function (response) {
            $("#_AFIDCTC").html(response.data);
            F.LoadDynamicASFStartORCTC();
        });
    }
    F.LoadDynamicASFStartORCTC = function () {
        var AccountFormID = $("#AFIDDDASFCTC").val();
        P({
            url: '/FileMaintenanceAccountableForm/GetDynamicAFStartORNoCTC',
            params: { AccountFormID: AccountFormID }
        }).then(function (response) {
            $("#_StartORIDCTC").html(response.data);
            F.LoadDynamicDDASFInvtCTC();
        });
    }
    F.LoadDynamicDDASFInvtCTC = function () {
        var AFORID = $("#StartIDDDDCTC option:selected").val();
        if (AFORID == null || AFORID == "") {
            AFORID = 0;
        }
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignAFCTC',
            params: { AFORID: AFORID }
        }).then(function (response) {
            $("#_AFCTCInvIDCTC").html(response.data);
        });
    }
    F.DeleteAccountableFormAssignCTCCollector = function (AssignAFID) {
        P({
            url: '/FileMaintenanceAccountableForm/DeleteAccountableFormAssignCTCCollectors',
            params: { AssignAFID: AssignAFID }
        }).then(function (response) {
            F.Get_AFAssignmentCTCCollectorTable();
        });
    }

    //BPLS
    F.Get_AFAssignmentBPLSTable = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AFAssignmentBPLSTable',
        }).then(function (response) {
            $("#_BPLSTableID").html(response.data);
        });
        F.Get_AddAssignBPLS();
    }
    F.Get_AddAssignBPLS = function () {
        P({
            url: '/FileMaintenanceAccountableForm/Get_AddAssignBPLS',
        }).then(function (response) {
            $("#_BPLSID").html(response.data);
            //F.LoadDynamicAFCollectorCTC();
            //F.LoadDynamicDDASFFundTypeCTC();
            //F.LoadDynamicDDASFAFCTC();
        });
    }
});

//Fees
app.controller('FMFeeController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.InitAllFunction = function () {
        //Internal Revenue Allotment Table
        F.Get_FeeTable();
    }
    F.SuccessMessageBox = function () {
        var SuccessMess = F.P;
        swalSuccess("Successfully Added", SuccessMess);
    }
    F.UpdateMessageBox = function () {
        var SuccessMess = F.P;
        swalSuccess("Successfully Update", SuccessMess);
    }
    F.DeleteMessageBox = function () {
        var SuccessMess = F.P;
        swalSuccess("Successfully Deleted", SuccessMess);
    }

    F.Get_FeeTable = function () {
        P({
            url: '/FileMaintenanceFees/Get_FieldFeeTable',
        }).then(function (response) {
            $("#_FeeTableID").html(response.data);
        });
        F.Get_AddFees();
    }
    F.Get_AddFees = function () {
        P({
            url: '/FileMaintenanceFees/Get_AddFieldFees',
        }).then(function (response) {
            $("#_FeeID").html(response.data);
            F.GetDynamicRevisionYear();
            F.GetDynamicFeeCategory(); F.GetDynamicFundType()
            F.GetDynamicAccountCode()
        });
    }

    F.GetDynamicAccountCode = function () {
        P({
            url: '/FileMaintenanceFees/GetDynamicGeneralAccountCode',
        }).then(function (response) {
            $("#_DDAccountCodeID").html(response.data);
        });
    }
    F.GetDynamicRevisionYear = function () {
        P({
            url: '/FileMaintenanceFees/GetDynamicRevisionYear',
        }).then(function (response) {
            $("#_DDRevisionYrID").html(response.data);
        });
    }
    F.GetDynamicFundType = function () {
        P({
            url: '/FileMaintenanceFees/GetDynamicFundType',
        }).then(function (response) {
            $("#_DDFundTypeID").html(response.data);
        });
    }
    F.GetDynamicFeeCategory = function () {
        P({
            url: '/FileMaintenanceFees/GetDynamicFeeCategory',
        }).then(function (response) {
            $("#_DDFeeCategoryID").html(response.data);
        });
    }

    F.GetSelectedDynamicAccountCode = function () {
        var AccountCodeTempID = $("#AccountCodeTempID").val();
        P({
            url: '/FileMaintenanceFees/GetSelectedDynamicGeneralAccountCode',
            params: { AccountCodeTempID: AccountCodeTempID }
        }).then(function (response) {
            $("#_DDAccountCodeID").html(response.data);
        });
    }
    F.GetSelectedDynamicRevisionYear = function () {
        var RevisionYearTempID = $("#RevisionYearTempID").val();
        P({
            url: '/FileMaintenanceFees/GetSelectedDynamicRevisionYear',
            params: { RevisionYearTempID: RevisionYearTempID }
        }).then(function (response) {
            $("#_DDRevisionYrID").html(response.data);
        });
    }
    F.GetSelectedDynamicFundType = function () {
        var FundTypeTempID = $("#FundTypeTempID").val();
        P({
            url: '/FileMaintenanceFees/GetSelectedDynamicFundType',
            params: { FundTypeTempID: FundTypeTempID }
        }).then(function (response) {
            $("#_DDFundTypeID").html(response.data);
        });
    }
    F.GetSelectedDynamicFeeCategory = function () {
        var FeeCategoryTempID = $("#FeeCategoryTempID").val();
        P({
            url: '/FileMaintenanceFees/GetSelectedDynamicFeeCategory',
            params: { FeeCategoryTempID: FeeCategoryTempID }
        }).then(function (response) {
            $("#_DDFeeCategoryID").html(response.data);
        });
    }
    F.Get_UpdateFieldFee = function (FieldFeeID) {
        P({
            url: '/FileMaintenanceFees/Get_UpdateFieldFee',
            params: { FieldFeeID: FieldFeeID }
        }).then(function (response) {
            $("#_FeeID").html(response.data);
            F.GetSelectedDynamicAccountCode();
            F.GetSelectedDynamicFundType();
            F.GetSelectedDynamicFeeCategory();
            F.GetSelectedDynamicRevisionYear();
        });
    }
    F.Get_DeleteFieldFee = function (FieldFeeID) {
        P({
            url: '/FileMaintenanceFees/DeleteFieldFee',
            params: { FieldFeeID: FieldFeeID }
        }).then(function (response) {
            F.Get_FeeTable();
        });
    }

    F.Get_FeeCategoryTable = function () {
        P({
            url: '/FileMaintenanceFees/Get_FeeCategoryTable',
        }).then(function (response) {
            $("#_FeeCategoryTableID").html(response.data);
        });
        F.Get_AddFeeCategory();
        F.GetDynamicFeeCategory();
    }
    F.Get_AddFeeCategory = function () {
        P({
            url: '/FileMaintenanceFees/Get_AddFeeCategory',
        }).then(function (response) {
            $("#_FeeCategoryID").html(response.data);
            F.LoadDynamicDDAccountableFormDescription();
        });
    }
    F.LoadDynamicDDAccountableFormDescription = function () {
        P({
            url: '/FileMaintenanceFees/GetDynamicAccountableFormDescription',
        }).then(function (response) {
            $("#_AFTypeORID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDAccountableFormDescription = function () {
        var AccountFormTempID = $("#AccountFormTempID").val();
        alert(AccountFormTempID)
        P({
            url: '/FileMaintenanceFees/GetSelectedDynamicAccountableFormDescription',
            params: { AccountFormTempID: AccountFormTempID }
        }).then(function (response) {
            $("#_AFTypeORID").html(response.data);
        });
    }
    F.Get_UpdateFeeCategory = function (FeeCategoryID) {
        P({
            url: '/FileMaintenanceFees/Get_UpdateFeeCategory',
            params: { FeeCategoryID: FeeCategoryID }
        }).then(function (response) {
            $("#_FeeCategoryID").html(response.data);
            F.LoadSelectedDynamicDDAccountableFormDescription();
        });
    }
    F.Get_DeleteFeeCategory = function (FeeCategoryID) {
        P({
            url: '/FileMaintenanceFees/DeleteFeeCategory',
            params: { FeeCategoryID: FeeCategoryID }
        }).then(function (response) {
            F.Get_FeeCategoryTable();
        });
    }
});

//Bank
app.controller('FMBankController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.BankTab = function () {
        F.tab1 = null;
        P({
            url: '/FileMaintenanceBank/BankTab',
        }).then(function (response) {
            F.tab1 = D(response.data);
            F.Get_BankTable();
        });
    }
    F.AccountTypeTab = function () {
        F.tab2 = null;
        P({
            url: '/FileMaintenanceBank/AccountTypeTab',
        }).then(function (response) {
            F.tab2 = D(response.data);
            F.Get_AccountTypeTable();
        });
    }
    F.BankAccountTab = function () {
        F.tab3 = null;
        P({
            url: '/FileMaintenanceBank/BankAccountTab',
        }).then(function (response) {
            F.tab3 = D(response.data);
            F.Get_BankAccountTable();
        });
    }

    //Bank Name
    F.Get_BankTable = function () {
        P({
            url: '/FileMaintenanceBank/Get_BankTable',
        }).then(function (response) {
            $("#_BankTableID").html(response.data);
        });
        F.Get_AddBank();
    }
    F.Get_AddBank = function () {
        P({
            url: '/FileMaintenanceBank/Get_AddBankName',
        }).then(function (response) {
            $("#_BankID").html(response.data);
        });
    }
    F.Get_UpdateBank = function (BankID) {
        P({
            url: '/FileMaintenanceBank/Get_UpdateBankName',
            params: { BankID: BankID }
        }).then(function (response) {
            $("#_BankID").html(response.data);
        });
    }
    F.Get_DeleteBank = function (BankID) {
        P({
            url: '/FileMaintenanceBank/DeleteBankName',
            params: { BankID: BankID }
        }).then(function (response) {
            F.Get_BankTable();
        });
    }

    //Account Type

    F.Get_AccountTypeTable = function () {
        P({
            url: '/FileMaintenanceBank/Get_AccountTypeTable',
        }).then(function (response) {
            $("#AccountTypeTableID").html(response.data);
        });
        F.Get_AddAccountType();
    }
    F.Get_AddAccountType = function () {
        P({
            url: '/FileMaintenanceBank/Get_AddAccountType',
        }).then(function (response) {
            $("#_AccountTypeID").html(response.data);
        });
    }
    F.Get_UpdateAccountType = function (AccountTypeID) {
        P({
            url: '/FileMaintenanceBank/Get_UpdateAccountType',
            params: { AccountTypeID: AccountTypeID }
        }).then(function (response) {
            $("#_AccountTypeID").html(response.data);
        });
    }
    F.Get_DeleteAccountType = function (AccountTypeID) {
        P({
            url: '/FileMaintenanceBank/DeleteAccountType',
            params: { AccountTypeID: AccountTypeID }
        }).then(function (response) {
            F.Get_AccountTypeTable();
        });
    }

    //Bank Account

    F.Get_BankAccountTable = function () {
        P({
            url: '/FileMaintenanceBank/Get_BankAccountTable',
        }).then(function (response) {
            $("#_BankAccountTableID").html(response.data);
        });
        F.Get_AddBankAccount();
    }
    F.Get_AddBankAccount = function () {
        P({
            url: '/FileMaintenanceBank/Get_AddBankAccount',
        }).then(function (response) {
            $("#_BankAccountID").html(response.data);
            F.LoadDynamicDDBank();
            F.LoadDynamicDDAccountType();
            F.LoadDynamicDDFundType();
            F.LoadDynamicDDGeneralName();
        });
    }
    F.LoadDynamicDDBank = function () {
        P({
            url: '/FileMaintenanceBank/GetDynamicBank',
        }).then(function (response) {
            $("#_BankAccountBankID").html(response.data);
        });
    }
    F.LoadDynamicDDAccountType = function () {
        P({
            url: '/FileMaintenanceBank/GetDynamicAccountType',
        }).then(function (response) {
            $("#_DynamicDDAccountTypeID").html(response.data);
        });
    }
    F.LoadDynamicDDFundType = function () {
        P({
            url: '/FileMaintenanceBank/GetDynamicFundType',
        }).then(function (response) {
            $("#_DynamicDDFundTypeID").html(response.data);
        });
    }
    F.LoadDynamicDDGeneralName = function () {
        P({
            url: '/FileMaintenanceBank/GetDynamicGeneralAccount',
        }).then(function (response) {
            $("#_DynamicDDGeneralNameID").html(response.data);
            F.LoadDynamicDDGeneralCode();
        });
    }
    F.LoadDynamicDDGeneralCode = function () {
        var GeneralAccountID = $("#GeneralAccountNameIDDD").val();
        P({
            url: '/FileMaintenanceBank/GetDynamicGeneralAccountCode',
            params: { GeneralAccountID: GeneralAccountID },
        }).then(function (response) {
            $("#_DynamicCodeID").html(response.data);
        });
    }
    F.Get_UpdateBankAccount = function (BankAccountID) {
        P({
            url: '/FileMaintenanceBank/Get_UpdateBankAccount',
            params: { BankAccountID: BankAccountID }
        }).then(function (response) {
            $("#_BankAccountID").html(response.data);
            F.LoadSelectedDynamicDDBank();
            F.LoadSelectedSelectedDynamicDDAccountType();
            F.LoadSelectedDynamicDDFundType();
            F.LoadSelectedDynamicDDGeneralName();
        });
    }
    F.LoadSelectedDynamicDDBank = function () {
        var BankAccountBankTempID = $("#BankAccountBankTempID").val();
        P({
            url: '/FileMaintenanceBank/GetSelectedDynamicBank',
            params: { BankAccountBankTempID: BankAccountBankTempID }
        }).then(function (response) {
            $("#_BankAccountBankID").html(response.data);
        });
    }
    F.LoadSelectedSelectedDynamicDDAccountType = function () {
        var BankAccountAccountTypeTempID = $("#BankAccountAccountTypeTempID").val();
        P({
            url: '/FileMaintenanceBank/GetSelectedDynamicAccountType',
            params: { BankAccountAccountTypeTempID: BankAccountAccountTypeTempID }
        }).then(function (response) {
            $("#_DynamicDDAccountTypeID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDFundType = function () {
        var BankAccountFundTypeTempID = $("#BankAccountFundTypeTempID").val();
        P({
            url: '/FileMaintenanceBank/GetSelectedDynamicFundType',
            params: { BankAccountFundTypeTempID: BankAccountFundTypeTempID }
        }).then(function (response) {
            $("#_DynamicDDFundTypeID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDGeneralName = function () {
        var BankAccountGeneralAccountNameTempID = $("#BankAccountGeneralAccountNameTempID").val();
        P({
            url: '/FileMaintenanceBank/GetSelectedDynamicGeneralAccount',
            params: { BankAccountGeneralAccountNameTempID: BankAccountGeneralAccountNameTempID }
        }).then(function (response) {
            $("#_DynamicDDGeneralNameID").html(response.data);
            F.LoadDynamicDDGeneralCode();
        });
    }
    F.Get_DeleteBankAccount = function (BankAccountID) {
        P({
            url: '/FileMaintenanceBank/DeleteBankAccount',
            params: { BankAccountID: BankAccountID }
        }).then(function (response) {
            F.Get_BankAccountTable();
        });
    }
});

//Disbursement
app.controller('FMDisbursementController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.CheckInventoryTab = function () {
        F.tab1 = null;
        P({
            url: '/FileMaintenanceDisbursement/CheckInventoryTab',
        }).then(function (response) {
            F.tab1 = D(response.data);
            F.Get_CheckInventoryTable();
        });
    }
    F.CheckMaintenanceTab = function () {
        F.tab2 = null;
        P({
            url: '/FileMaintenanceDisbursement/CheckMaintenanceTab',
        }).then(function (response) {
            F.tab2 = D(response.data);
            F.Get_CheckMaintenanceTable();
        });
    }
    F.DVTypeTab = function () {
        F.tab3 = null;
        P({
            url: '/FileMaintenanceDisbursement/DVTypeTab',
        }).then(function (response) {
            F.tab3 = D(response.data);
            F.Get_DVTypeTable();
        });
    }

    //Check Inventory
    F.LoadDynamicDDBankName = function () {
        P({
            url: '/FileMaintenanceDisbursement/GetDynamicBankName',
        }).then(function (response) {
            $("#_DynamicDDBankID").html(response.data);
            F.LoadDynamicDDAccountName();
        });
    }
    F.LoadDynamicDDAccountName = function () {
        var BankID = $("#BankIDDDCheckInventor").val()
        P({
            url: '/FileMaintenanceDisbursement/GetDynamicAccountName',
            params: { BankID: BankID }
        }).then(function (response) {
            $("#_DynamicDDAccountNameID").html(response.data);
        });
    }
    F.LoadSelectDynamicDDBankName = function () {
        var CheckInventoryBankTempID = $("#CheckInventoryBankTempID").val();
        P({
            url: '/FileMaintenanceDisbursement/GetSelectedDynamicBankName',
            params: { CheckInventoryBankTempID: CheckInventoryBankTempID }
        }).then(function (response) {
            $("#_DynamicDDBankID").html(response.data);
            F.LoadSelectDynamicDDAccountName();
        });
    }
    F.LoadSelectDynamicDDAccountName = function () {
        var CheckInventoryAccountNameIDTempID = $("#CheckInventoryAccountNameIDTempID").val();
        var BankID = $("#BankIDDDCheckInventor").val();
        P({
            url: '/FileMaintenanceDisbursement/GetSelectedDynamicAccountName',
            params: { BankID: BankID, CheckInventoryAccountNameIDTempID: CheckInventoryAccountNameIDTempID }
        }).then(function (response) {
            $("#_DynamicDDAccountNameID").html(response.data);
        });
    }
    F.Get_CheckInventoryTable = function () {
        P({
            url: '/FileMaintenanceDisbursement/Get_CheckInventoryTable',
        }).then(function (response) {
            $("#_CheckInventoryTableID").html(response.data);
        });
        F.Get_AddCheckInventory();
    }
    F.Get_AddCheckInventory = function () {
        P({
            url: '/FileMaintenanceDisbursement/Get_AddCheckInventory',
        }).then(function (response) {
            $("#_CheckInventoryID").html(response.data);
            F.LoadDynamicDDBankName();
        });
    }
    F.Get_UpdateCheckInventory = function (CheckInvntID) {
        P({
            url: '/FileMaintenanceDisbursement/Get_UpdateCheckInventory',
            params: { CheckInvntID: CheckInvntID }
        }).then(function (response) {
            $("#_CheckInventoryID").html(response.data);
            F.LoadSelectDynamicDDBankName();
        });
    }
    F.Get_DeleteCheckInventory = function (CheckInvntID) {
        P({
            url: '/FileMaintenanceDisbursement/DeleteCheckInventory',
            params: { CheckInvntID: CheckInvntID }
        }).then(function (response) {
            F.Get_CheckInventoryTable();
        });
    }

    //Check Maintenance
    F.Get_CheckMaintenanceTable = function () {
        P({
            url: '/FileMaintenanceDisbursement/Get_CheckMaintenanceTable',
        }).then(function (response) {
            $("#_checkMaintenanceTableID").html(response.data);
        });
        F.Get_AddCheckMaintenance();
    }
    F.Get_AddCheckMaintenance = function () {
        P({
            url: '/FileMaintenanceDisbursement/Get_AddCheckMaitenance',
        }).then(function (response) {
            $("#_checkMaintenanceID").html(response.data);
            F.LoadDynamicDDBankNameCM();
        });
    }
    F.LoadDynamicDDBankNameCM = function () {
        P({
            url: '/FileMaintenanceDisbursement/GetDynamicBankNameCM',
        }).then(function (response) {
            $("#_DynamicDDBankCMID").html(response.data);
            F.LoadDynamicDDAccountNameCM();
        });
    }
    F.LoadDynamicDDAccountNameCM = function () {
        var BankID = $("#BankIDDDCM").val()
        P({
            url: '/FileMaintenanceDisbursement/GetDynamicAccountNameCM',
            params: { BankID: BankID }
        }).then(function (response) {
            $("#_DynamicDDAccountID").html(response.data);
            F.LoadDynamicStartingChkCM();
        });
    }
    F.LoadDynamicStartingChkCM = function () {
        var AccountNameIDDD2 = $("#AccountNameIDDD2").val()
        if (AccountNameIDDD2 == null) {
            AccountNameIDDD2 = 0;
        }
        P({
            url: '/FileMaintenanceDisbursement/LoadDynamicCheckMaintenanceInventory',
            params: { AccountNameIDDD2: AccountNameIDDD2 }
        }).then(function (response) {
            $("#_DynamicDDStartingNoID").html(response.data);
            F.Get_CheckMaintenanceQuantyNEnd();
        });
    }
    F.Get_CheckMaintenanceQuantyNEnd = function () {
        var StartingChckNo = $("#AccountNameInventoryIDDD2 option:selected").text();
        if (StartingChckNo == null || StartingChckNo == "") {
            StartingChckNo = 0;
        }
        P({
            url: '/FileMaintenanceDisbursement/Get_CheckMaintenanceQuantyNEnd',
            params: { StartingChckNo: StartingChckNo }
        }).then(function (response) {
            $("#_DynamicTBQuanEnding").html(response.data);
        });
    }

    F.Get_UpdateCheckMaintenance = function (CheckMainteID) {
        P({
            url: '/FileMaintenanceDisbursement/Get_UpdateCheckMaintenance',
            params: { CheckMainteID: CheckMainteID }
        }).then(function (response) {
            $("#_checkMaintenanceID").html(response.data);
            F.GetLoadDynamicDDAccountName();
        });
    }
    F.GetLoadDynamicDDAccountName = function () {
        var CheckMaintenanceBankIDTempID = $("#CheckMaintenanceBankIDTempID").val();
        P({
            url: '/FileMaintenanceDisbursement/GetSelectedDynamicMaitenance',
            params: { CheckMaintenanceBankIDTempID: CheckMaintenanceBankIDTempID }
        }).then(function (response) {
            $("#_DynamicDDAccountID").html(response.data);
        });
    }
    F.Get_DeleteCheckMaintenance = function (CheckMainteID) {
        P({
            url: '/FileMaintenanceDisbursement/DeleteCheckMaintenance',
            params: { CheckMainteID: CheckMainteID }
        }).then(function (response) {
            F.Get_CheckMaintenanceTable();
        });
    }

    //DV Type
    F.Get_DVTypeTable = function () {
        P({
            url: '/FileMaintenanceDisbursement/Get_DVTypeTable',
        }).then(function (response) {
            $("#DVTypeTableID").html(response.data);
        });
        F.Get_AddDVType();
    }
    F.Get_AddDVType = function () {
        P({
            url: '/FileMaintenanceDisbursement/Get_AddDVType',
        }).then(function (response) {
            $("#DVTypeID").html(response.data);
        });
    }
    F.Get_UpdateDVType = function (DVTypeID) {
        P({
            url: '/FileMaintenanceDisbursement/Get_UpdateDVType',
            params: { DVTypeID: DVTypeID }
        }).then(function (response) {
            $("#DVTypeID").html(response.data);
        });
    }
    F.Get_DeleteDVType = function (DVTypeID) {
        P({
            url: '/FileMaintenanceDisbursement/DeleteDVType',
            params: { DVTypeID: DVTypeID }
        }).then(function (response) {
            F.Get_DVTypeTable();
        });
    }
});

//IRA Share
app.controller('FMIRAController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.InitAllFunction = function () {
        //Internal Revenue Allotment Table
        F.Get_InternalRevenueAllotmentTable();
    }
    F.SuccessMessageBox = function () {
        var SuccessMess = F.P;
        swalSuccess("Successfully Added", SuccessMess);
    }
    F.UpdateMessageBox = function () {
        var SuccessMess = F.P;
        swalSuccess("Successfully Update", SuccessMess);
    }
    F.DeleteMessageBox = function () {
        var SuccessMess = F.P;
        swalSuccess("Successfully Deleted", SuccessMess);
    }
    //Internal Revenue Allotment Share
    F.Get_InternalRevenueAllotmentTable = function () {
        P({
            url: '/FileMaintenanceIRAShare/Get_InternalRevenueAllotmentTable',
        }).then(function (response) {
            $("#_IraTableID").html(response.data);
        });
        F.Get_AddInternalRevenueAllotment();
    }
    F.Get_AddInternalRevenueAllotment = function () {
        P({
            url: '/FileMaintenanceIRAShare/Get_AddInternalRevenueAllotmentTable',
        }).then(function (response) {
            $("#_IraID").html(response.data);
        });
    }
    F.Get_UpdateInternalRevenueAllotment = function (IRAID) {
        P({
            url: '/FileMaintenanceIRAShare/Get_UpdateInternalRevenueAllotment',
            params: { IRAID: IRAID }
        }).then(function (response) {
            $("#_IraID").html(response.data);
        });
    }
    F.Get_DeleteInternalRevenueAllotment = function (IRAID) {
        P({
            url: '/FileMaintenanceIRAShare/DeleteInternalRevenueAllotment',
            params: { IRAID: IRAID }
        }).then(function (response) {
            F.Get_InternalRevenueAllotmentTable();
        });
    }
});

//Barangay
app.controller('FMBarangayController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.BarangayNameTab = function () {
        F.tab1 = null;
        P({
            url: '/FileMaintenanceBarangay/BarangayNameTab',
        }).then(function (response) {
            F.tab1 = D(response.data);
            F.Get_BarangayNameTable();
        });
    }
    F.BarangayCollectorTab = function () {
        F.tab2 = null;
        P({
            url: '/FileMaintenanceBarangay/BarangayCollectorTab',
        }).then(function (response) {
            F.tab2 = D(response.data);
            F.Get_BarangayCollectorNameTable();
        });
    }
    F.BarangayBankAccountTab = function () {
        F.tab3 = null;
        P({
            url: '/FileMaintenanceBarangay/BarangayBankAccountTab',
        }).then(function (response) {
            F.tab3 = D(response.data);
            F.Get_BarangayBankAccountTable();
        });
    }

    //Barangay
    //Barangay Name
    F.Get_BarangayNameTable = function () {
        P({
            url: '/FileMaintenanceBarangay/Get_BarangayNameTable',
        }).then(function (response) {
            $("#_BarangayNameTableID").html(response.data);
        });
        F.Get_AddBarangayName();
    }
    F.Get_AddBarangayName = function () {
        P({
            url: '/FileMaintenanceBarangay/Get_AddBarangayName',
        }).then(function (response) {
            $("#_BarangayNameID").html(response.data);
        });
    }
    F.Get_UpdateBarangayName = function (BarangayID) {
        P({
            url: '/FileMaintenanceBarangay/Get_UpdateBarangayName',
            params: { BarangayID: BarangayID }
        }).then(function (response) {
            $("#_BarangayNameID").html(response.data);
        });
    }
    F.Get_DeleteBarangayName = function (BarangayID) {
        P({
            url: '/FileMaintenanceBarangay/DeleteBarangayName',
            params: { BarangayID: BarangayID }
        }).then(function (response) {
            F.Get_BarangayNameTable();
        });
    }


    //Barangay Collector
    F.Get_BarangayCollectorNameTable = function () {
        P({
            url: '/FileMaintenanceBarangay/Get_BarangayCollectorTable',
        }).then(function (response) {
            $("#_BarangayCollectorTableID").html(response.data);
        });
        F.Get_AddCollectorName();
    }
    F.Get_AddCollectorName = function () {
        P({
            url: '/FileMaintenanceBarangay/Get_AddCollectorName',
        }).then(function (response) {
            $("#_BarangayCollectorID").html(response.data);
        });
    }
    F.Get_UpdateCollectorName = function (BarangayCollectorID) {
        P({
            url: '/FileMaintenanceBarangay/Get_UpdateCollectorName',
            params: { BarangayCollectorID: BarangayCollectorID }
        }).then(function (response) {
            $("#_BarangayCollectorID").html(response.data);
        });
    }
    F.Get_DeleteCollectorName = function (BarangayCollectorID) {
        P({
            url: '/FileMaintenanceBarangay/DeleteCollectorName',
            params: { BarangayCollectorID: BarangayCollectorID }
        }).then(function (response) {
            F.Get_BarangayCollectorNameTable();
        });
    }

    //Barangay Bank Account

    F.Get_BarangayBankAccountTable = function () {
        P({
            url: '/FileMaintenanceBarangay/Get_BarangayBankAccountTable',
        }).then(function (response) {
            $("#_BarangayBankAccountTableID").html(response.data);
        });
        F.Get_AddBarangayBankAccount();
    }
    F.Get_AddBarangayBankAccount = function () {
        P({
            url: '/FileMaintenanceBarangay/Get_AddBarangayBankAccount',
        }).then(function (response) {
            $("#_BarangayBankAccountID").html(response.data);
            F.LoadDynamicDDBankName();
            F.LoadDynamicDDBarangayName();
        });
    }
    F.LoadDynamicDDBarangayName = function () {
        P({
            url: '/FileMaintenanceBarangay/GetDynamicBarangayName',
        }).then(function (response) {
            $("#_DynamicBarangayNameID").html(response.data);
        });
    }
    F.LoadDynamicDDBankName = function () {
        P({
            url: '/FileMaintenanceBarangay/GetDynamicBarangayBankName',
        }).then(function (response) {
            $("#_DynamicBankID").html(response.data);
            F.LoadDynamicDDBankAccountNumber();
        });
    }
    F.LoadDynamicDDBankAccountNumber = function () {
        var BankID = $("#BarangayBankIDD").val();
        P({
            url: '/FileMaintenanceBarangay/GetDynamicBarangayBankAccountNumber',
            params: { BankID: BankID }
        }).then(function (response) {
            $("#_DynamicAccountNo").html(response.data);
        });
    }
    F.Get_UpdateBarangayBankAccount = function (BarangayBankAccountID) {
        P({
            url: '/FileMaintenanceBarangay/Get_UpdateBarangayBankAccount',
            params: { BarangayBankAccountID: BarangayBankAccountID }
        }).then(function (response) {
            $("#_BarangayBankAccountID").html(response.data);
            F.LoadSelectedDynamicDDBarangayName();
            F.LoadSelectedDynamicDDBankName();
        });
    }
    F.LoadSelectedDynamicDDBarangayName = function () {
        var BarangayTempID = $("#BarangayTempID").val();
        P({
            url: '/FileMaintenanceBarangay/GetSelectedDynamicBarangayName',
            params: { BarangayTempID: BarangayTempID }
        }).then(function (response) {
            $("#_DynamicBarangayNameID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDBankName = function () {
        var BankTempID = $("#BankTempID").val();
        P({
            url: '/FileMaintenanceBarangay/GetSelectedDynamicBarangayBankName',
            params: { BankTempID: BankTempID }
        }).then(function (response) {
            $("#_DynamicBankID").html(response.data);
            F.LoadSelectedDynamicDDBankAccountNumber();
        });
    }
    F.LoadSelectedDynamicDDBankAccountNumber = function () {
        var BankID = $("#BarangayBankIDD").val();
        var AccountNumberTempID = $("#AccountNumberTempID").val();
        P({
            url: '/FileMaintenanceBarangay/GetSelectedDynamicBarangayBankAccountNumber',
            params: { BankID: BankID, AccountNumberTempID: AccountNumberTempID }
        }).then(function (response) {
            $("#_DynamicAccountNo").html(response.data);
        });
    }
    F.Get_DeleteBankAccount = function (BarangayBankAccountID) {
        P({
            url: '/FileMaintenanceBarangay/DeleteBarangayBankAccount',
            params: { BarangayBankAccountID: BarangayBankAccountID }
        }).then(function (response) {
            F.Get_BarangayBankAccountTable();
        });
    }
});
