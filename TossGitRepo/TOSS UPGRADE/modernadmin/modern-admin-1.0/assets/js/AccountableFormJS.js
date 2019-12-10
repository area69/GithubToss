﻿app.controller('FMAccountableFormController', function ($scope, $http, $sce) {
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