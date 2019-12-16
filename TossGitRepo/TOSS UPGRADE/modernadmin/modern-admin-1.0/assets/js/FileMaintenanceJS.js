//Fund
app.controller('FMFundController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.FundTab = function () {
        F.tab1 = null;
        P({
            url: '/FileMaintenanceFund/FundTab',
        }).then(function (response) {
            F.tab1 = D(response.data);
            F.Get_FundTables();
        });
    }
    F.SubFundTab = function () {
        F.tab2 = null;
        P({
            url: '/FileMaintenanceFund/SubFundTab',
        }).then(function (response) {
            F.tab2 = D(response.data);
            F.Get_SubFundTables();
        });
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
    @* Fund *@

    F.Get_FundTables = function () {
        P({
            url: '/FileMaintenanceFund/Get_FundTable',
        }).then(function (response) {
            $("#FundTableID").html(response.data);
        });
        F.Get_AddFunds();
    }
    F.Get_AddFunds = function () {
        P({
            url: '/FileMaintenanceFund/Get_AddFund',
        }).then(function (response) {
            $("#FundID").html(response.data);
        });
    }
    F.Get_UpdateFunds = function (FundID) {
        P({
            url: '/FileMaintenanceFund/Get_UpdateFund',
            params: { FundID: FundID }
        }).then(function (response) {
            $("#FundID").html(response.data);
        });
    }
    F.Get_DeleteFunds = function (FundID) {
        P({
            url: '/FileMaintenanceFund/DeleteFunds',
            params: { FundID: FundID }
        }).then(function (response) {
            F.Get_FundTables();
        });
    }
    @* Sub Fund *@

    F.LoadDynamicDDFund = function () {
        P({
            url: '/FileMaintenanceFund/GetDynamicFund',
        }).then(function (response) {
            $("#_DynamicFund").html(response.data);
        });
    }
    F.Get_SubFundTables = function () {
        P({
            url: '/FileMaintenanceFund/Get_SubFundTable',
        }).then(function (response) {
            $("#SubFundTableID").html(response.data);
        });
        F.Get_AddSubFunds();
    }
    F.Get_AddSubFunds = function () {
        P({
            url: '/FileMaintenanceFund/Get_AddSubFund',
        }).then(function (response) {
            $("#SubFundID").html(response.data);
            F.LoadDynamicDDFund();
        });
    }
    F.Get_DeleteSubFunds = function (SubFundID) {
        P({
            url: '/FileMaintenanceFund/DeleteSubFunds',
            params: { SubFundID: SubFundID }
        }).then(function (response) {
            F.Get_SubFundTables();
        });
    }
    F.Get_UpdateSubFunds = function (SubFundID) {
        P({
            url: '/FileMaintenanceFund/Get_UpdateSubFund',
            params: { SubFundID: SubFundID }
        }).then(function (response) {
            $("#SubFundID").html(response.data);
            F.LoadSelectedDynamicDDFund();
        });
    }
    F.LoadSelectedDynamicDDFund = function () {
        var FundIDTempID = $("#FundIDTempID").val();
        P({
            url: '/FileMaintenanceFund/GetSelectedDynamicFund',
            params: { FundIDTempID: FundIDTempID }
        }).then(function (response) {
            $("#_DynamicFund").html(response.data);
        });
    }

});
//Sector
app.controller('FMSectorController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.SectorTab = function () {
        F.tab1 = null;
        P({
            url: '/FileMaintenanceSector/SectorTab',
        }).then(function (response) {
            F.tab1 = D(response.data);
            F.Get_SectorTables();
        });
    }
    F.SubSectorTab = function () {
        F.tab2 = null;
        P({
            url: '/FileMaintenanceSector/SubSectorTab',
        }).then(function (response) {
            F.tab2 = D(response.data);
            F.Get_SubSectorTables();
        });
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

    F.Get_SectorTables = function () {
        P({
            url: '/FileMaintenanceSector/Get_SectorTable',
        }).then(function (response) {
            $("#SectorTableID").html(response.data);
        });
        F.Get_AddSectors();
    }
    F.Get_AddSectors = function () {
        P({
            url: '/FileMaintenanceSector/Get_AddSector',
        }).then(function (response) {
            $("#SectorID").html(response.data);
            F.LoadDynamicDDSector();
        });
    }
    F.Get_UpdateSectors = function (SectorID) {
        P({
            url: '/FileMaintenanceSector/Get_UpdateSector',
            params: { SectorID: SectorID }
        }).then(function (response) {
            $("#SectorID").html(response.data);
        });
    }
    F.Get_DeleteSectors = function (SectorID) {
        P({
            url: '/FileMaintenanceSector/DeleteSectors',
            params: { SectorID: SectorID }
        }).then(function (response) {
            F.Get_SectorTables();
        });
    }

    F.LoadDynamicDDSector = function () {
        P({
            url: '/FileMaintenanceSector/GetDynamicSector',
        }).then(function (response) {
            $("#_DynamicSector").html(response.data);
            F.LoadDynamicLSectorCode();
        });
    }
    F.LoadDynamicLSectorCode = function () {
        var SectorID = $("#SectorNameIDD").val();
        P({
            url: '/FileMaintenanceSector/GetSectorCodeField',
            params: { SectorID: SectorID },
        }).then(function (response) {
            $("#_DynamicSectorCode").html(response.data);
        });
    }
    F.Get_SubSectorTables = function () {
        P({
            url: '/FileMaintenanceSector/Get_SubSectorTable',
        }).then(function (response) {
            $("#SubSectorTableID").html(response.data);
        });
        F.Get_AddSubSectors();
    }
    F.Get_AddSubSectors = function () {
        P({
            url: '/FileMaintenanceSector/Get_AddSubSector',
        }).then(function (response) {
            $("#SubSectorID").html(response.data);
            F.LoadDynamicDDSector();
        });
    }
    F.Get_DeleteSubSectors = function (SubSectorID) {
        P({
            url: '/FileMaintenanceSector/DeleteSubSectors',
            params: { SubSectorID: SubSectorID }
        }).then(function (response) {
            F.Get_SubSectorTables();
        });
    }
    F.Get_UpdateSubSectors = function (SubSectorID) {
        P({
            url: '/FileMaintenanceSector/Get_UpdateSubSector',
            params: { SubSectorID: SubSectorID }
        }).then(function (response) {
            $("#SubSectorID").html(response.data);
            F.LoadSelectedDynamicDDSector();
        });
    }
    F.LoadSelectedDynamicLSectorCode = function (SubSectorID) {
        P({
            url: '/FileMaintenanceSector/GetSelectedSectorCodeField',
            params: { SubSectorID: SubSectorID },
        }).then(function (response) {
            $("#_DynamicSectorCode").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDSector = function () {
        var SectorNameIDTempID = $("#SectorNameIDTempID").val();
        P({
            url: '/FileMaintenanceSector/GetSelectedDynamicSector',
            params: { SectorNameIDTempID: SectorNameIDTempID }
        }).then(function (response) {
            $("#_DynamicSector").html(response.data);
            F.LoadDynamicLSectorCode();
        });
    }
});
//OfficeType
app.controller('FMOfficeTypeController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.InitAllFunction = function () {
        F.Get_OfficeTypeTable();
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
    F.Get_OfficeTypeTable = function () {
        P({
            url: '/FileMaintenanceOfficeType/Get_OfficeTypeTable',
        }).then(function (response) {
            $("#_OfficeTypeTableID").html(response.data);
        });
        F.Get_AddOfficeType();
    }
    F.Get_AddOfficeType = function () {
        P({
            url: '/FileMaintenanceOfficeType/Get_AddOfficeType',
        }).then(function (response) {
            $("#_OfficeTypeID").html(response.data);
        });
    }
    F.Get_UpdateOfficeType = function (OfficeTypeID) {
        P({
            url: '/FileMaintenanceOfficeType/Get_UpdateOfficeType',
            params: { OfficeTypeID: OfficeTypeID }
        }).then(function (response) {
            $("#_OfficeTypeID").html(response.data);
        });
    }
    F.Get_DeleteOfficeType = function (OfficeTypeID) {
        P({
            url: '/FileMaintenanceOfficeType/DeleteOfficeType',
            params: { OfficeTypeID: OfficeTypeID }
        }).then(function (response) {
            F.Get_OfficeTypeTable();
        });
    }
});
//Responsibility Center
app.controller('FMDepartmentController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.DepartmentTab = function () {
        F.tab1 = null;
        P({
            url: '/FileMaintenanceResponsibilityCenter/DepartmentTab',
        }).then(function (response) {
            F.tab1 = D(response.data);
            F.Get_DepartmentTable();
        });
    }
    F.FunctionTab = function () {
        F.tab2 = null;
        P({
            url: '/FileMaintenanceResponsibilityCenter/FunctionTab',
        }).then(function (response) {
            F.tab2 = D(response.data);
            F.Get_FunctionTable();
        });
    }
    F.SectionTab = function () {
        F.tab3 = null;
        P({
            url: '/FileMaintenanceResponsibilityCenter/SectionTab',
        }).then(function (response) {
            F.tab3 = D(response.data);
            F.Get_SectionTable();
        });
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

    F.Get_DepartmentTable = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/Get_DepartmentTable',
        }).then(function (response) {
            $("#DepartmentTableID").html(response.data);
            F.Get_AddDepartment();
        });
    }
    F.Get_AddDepartment = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/Get_AddDepartment',
        }).then(function (response) {
            $("#DepartmentID").html(response.data);
            F.LoadDynamicDDFund();
            F.LoadDynamicDDSector();
            F.LoadDynamicDDOfficeType();
        });
    }
    F.LoadDynamicDDFund = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDynamicFund',
        }).then(function (response) {
            $("#_DynamicFundID").html(response.data);
        });
    }
    F.LoadDynamicDDSector = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDynamicSector',
        }).then(function (response) {
            $("#_DynamicSectorID").html(response.data);
            F.LoadDynamicDDSubSector();
        });
    }
    F.LoadDynamicDDSubSector = function () {
        var SectorID = $("#SectorNameIDD").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDynamicSubSector',
            params: { SectorID: SectorID },
        }).then(function (response) {
            $("#_DynamicSubSectorID").html(response.data);
        });
    }
    F.LoadDynamicDDOfficeType = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDynamicOfficeType',
        }).then(function (response) {
            $("#_DynamicOfficeTypeID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDFund = function () {
        var FundNameTempID = $("#FundNameTempID").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSelectedDynamicFund',
            params: { FundNameTempID: FundNameTempID }
        }).then(function (response) {
            $("#_DynamicFundID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDSector = function () {
        var SectorNameTempID = $("#SectorNameTempID").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSelectedDynamicSector',
            params: { SectorNameTempID: SectorNameTempID }
        }).then(function (response) {
            $("#_DynamicSectorID").html(response.data);
            F.LoadSelectedDynamicDDSubSector();
        });
    }
    F.LoadSelectedDynamicDDSubSector = function () {
        var SectorID = $("#SectorNameIDD").val();
        var SubSectorNameIDTempID = $("#SubSectorNameIDTempID").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSelectedDynamicSubSector',
            params: { SectorID: SectorID, SubSectorNameIDTempID: SubSectorNameIDTempID },
        }).then(function (response) {
            $("#_DynamicSubSectorID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDOfficeType = function () {
        var OfficeTypeNameTempID = $("#OfficeTypeNameTempID").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSelectedDynamicOfficeType',
            params: { OfficeTypeNameTempID: OfficeTypeNameTempID }
        }).then(function (response) {
            $("#_DynamicOfficeTypeID").html(response.data);
        });
    }
    F.Get_UpdateDepartment = function (DepartmentID) {
        P({
            url: '/FileMaintenanceResponsibilityCenter/Get_UpdateDepartment',
            params: { DepartmentID: DepartmentID }
        }).then(function (response) {
            $("#DepartmentID").html(response.data);
            F.LoadSelectedDynamicDDFund();
            F.LoadSelectedDynamicDDSector();
            F.LoadSelectedDynamicDDOfficeType();
        });
    }
    F.Get_DeleteDepartment = function (DepartmentID) {
        P({
            url: '/FileMaintenanceResponsibilityCenter/DeleteDepartment',
            params: { DepartmentID: DepartmentID }
        }).then(function (response) {
            F.Get_DepartmentTable();
        });
    }

    //Function
    F.Get_FunctionTable = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/Get_FunctionTable',
        }).then(function (response) {
            $("#_FunctionTableID").html(response.data);
            F.Get_AddFunction();
        });
    }
    F.Get_AddFunction = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/Get_AddFunction',
        }).then(function (response) {
            $("#_FunctionID").html(response.data);
            F.LoadDynamicDDDepartment();
            F.LoadDynamicDDFunctionSector();
            F.LoadDynamicDDFOfficeType();
        });
    }
    F.LoadDynamicDDDepartment = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDynamicDepartment',
        }).then(function (response) {
            $("#_DepartmentID").html(response.data);
            F.LoadDynamicLBFundName();
            F.LoadDynamicLBDepartmentCode();
        });
    }
    F.LoadSelectedDynamicDDDepartment = function () {
        var DepartmentTempID = $("#DepartmentTempID").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDynamicFunctionDepartment',
            params: { DepartmentTempID: DepartmentTempID }
        }).then(function (response) {
            $("#_DepartmentID").html(response.data);
            F.LoadDynamicLBFundName();
            F.LoadDynamicLBDepartmentCode();
        });
    }
    F.LoadDynamicLBFundName = function () {
        var DepartmentID = $("#DepartmentNameIDD").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetFundName',
            params: { DepartmentID: DepartmentID },
        }).then(function (response) {
            $("#_DynamicFundID1").html(response.data);
        });
    }
    F.LoadDynamicLBDepartmentCode = function () {
        var DepartmentID = $("#DepartmentNameIDD").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDeptOfficeCode',
            params: { DepartmentID: DepartmentID },
        }).then(function (response) {
            $("#_DynamicDepartmentCodeID").html(response.data);
        });
    }
    F.LoadDynamicDDFunctionSector = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDynamicFunctionSector',
        }).then(function (response) {
            $("#_FunctionSectorNameID").html(response.data);
            F.LoadDynamicDDFunctionSubSector();
        });
    }
    F.LoadSelectedDynamicDDFunctionSector = function () {
        var SectorNameTempID = $("#SectorNameTempID").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSelectedDynamicFunctionSector',
            params: { SectorNameTempID: SectorNameTempID }
        }).then(function (response) {
            $("#_FunctionSectorNameID").html(response.data);
            F.LoadSelectedDynamicDDFunctionSubSector();
        });
    }
    F.LoadSelectedDynamicDDFunctionSubSector = function () {
        var SectorID = $("#FunctionSectorNameIDD").val();
        var SubSectorNameIDTempID = $("#SubSectorNameIDTempID").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSelectedDynamicFunctionSubSector',
            params: { SectorID: SectorID, SubSectorNameIDTempID: SubSectorNameIDTempID },
        }).then(function (response) {
            $("#_FunctionSubSectorNameID").html(response.data);
        });
    }
    F.LoadDynamicDDFunctionSubSector = function () {
        var SectorID = $("#FunctionSectorNameIDD").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDynamicFunctionSubSector',
            params: { SectorID: SectorID },
        }).then(function (response) {
            $("#_FunctionSubSectorNameID").html(response.data);
        });
    }
    F.LoadDynamicDDFOfficeType = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetDynamicFunctionOfficeType',
        }).then(function (response) {
            $("#_FunctionOfficeTypeID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDFOfficeType = function () {
        var OfficeTypeNameTempID = $("#OfficeTypeNameTempID").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSelectedDynamicOfficeType',
            params: { OfficeTypeNameTempID: OfficeTypeNameTempID },
        }).then(function (response) {
            $("#_FunctionOfficeTypeID").html(response.data);
        });
    }
    F.Get_UpdateFunction = function (FunctionID) {
        P({
            url: '/FileMaintenanceResponsibilityCenter/Get_UpdateFunction',
            params: { FunctionID: FunctionID }
        }).then(function (response) {
            $("#_FunctionID").html(response.data);
            F.LoadSelectedDynamicDDDepartment();
            F.LoadSelectedDynamicDDFunctionSector();
            F.LoadSelectedDynamicDDFOfficeType();
        });
    }
    F.Get_DeleteFunction = function (FunctionID) {
        P({
            url: '/FileMaintenanceResponsibilityCenter/DeleteFunction',
            params: { FunctionID: FunctionID }
        }).then(function (response) {
            F.Get_FunctionTable();
        });
    }

    //Section
    F.Get_SectionTable = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/Get_SectionTable',
        }).then(function (response) {
            $("#_SectionTableID").html(response.data);
            F.Get_AddSection();
        });
    }
    F.Get_AddSection = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/Get_AddSection',
        }).then(function (response) {
            $("#_SectionID").html(response.data);
            F.LoadDynamicDDSectionDepartment();
        });
    }
    F.LoadDynamicDDSectionDepartment = function () {
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSectionDynamicDepartment',
        }).then(function (response) {
            $("#_SectionDepartmentID").html(response.data);

            F.LoadDynamicDDSectionFunction();
        });
    }
    F.LoadDynamicDDSectionFunction = function () {
        var DepartmentID = $("#SectionDepartmentNameIDD").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSectionDynamicFunction',
            params: { DepartmentID: DepartmentID },
        }).then(function (response) {
            $("#_SecctionFunctionID").html(response.data);
        });
    }
    F.Get_UpdateSection = function (SectionID) {
        P({
            url: '/FileMaintenanceResponsibilityCenter/Get_UpdateSection',
            params: { SectionID: SectionID }
        }).then(function (response) {
            $("#_SectionID").html(response.data);
            F.LoadSelectedDynamicDDSectionDepartment();
        });
    }
    F.LoadSelectedDynamicDDSectionDepartment = function () {
        var DepartmentTempID = $("#DepartmentTempID").val();
        P({
            url: '/FileMaintenanceResponsibilityCenter/GetSelectedSectionDynamicDepartment',
            params: { DepartmentTempID: DepartmentTempID },
        }).then(function (response) {
            $("#_SectionDepartmentID").html(response.data);
            F.LoadDynamicDDSectionFunction();
        });
    }
    F.Get_DeleteSection = function (SectionID) {
        P({
            url: '/FileMaintenanceResponsibilityCenter/DeleteSection',
            params: { SectionID: SectionID }
        }).then(function (response) {
            F.Get_SectionTable();
        });
    }
});
//Position
app.controller('FMPositionController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.InitAllFunction = function () {
        F.Get_PositionTable();
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

    F.Get_PositionTable = function () {
        P({
            url: '/FileMaintenancePosition/Get_PositionTable',
        }).then(function (response) {
            $("#_PositionTableID").html(response.data);
            F.Get_AddPosition();
        });
    }
    F.Get_AddPosition = function () {
        P({
            url: '/FileMaintenancePosition/Get_AddPosition',
        }).then(function (response) {
            $("#PositionID").html(response.data);
        });

    }
    F.Get_UpdatePosition = function (PositionID) {
        P({
            url: '/FileMaintenancePosition/Get_UpdatePosition',
            params: { PositionID: PositionID }
        }).then(function (response) {
            $("#PositionID").html(response.data);
        });
    }
    F.Get_DeletePosition = function (PositionID) {
        P({
            url: '/FileMaintenancePosition/DeletePosition',
            params: { PositionID: PositionID }
        }).then(function (response) {
            F.Get_PositionTable();
        });
    }
});
//Signatory
app.controller('FMSignatoriesController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.InitAllFunction = function () {
        F.GetListOfSignatories();
    }

    F.SuccessSave1 = function () {
        var SuccessMess = "Signatory Successfully Added";
        swalSuccess("Success", SuccessMess);
    }
    F.SuccessSave2 = function () {
        var SuccessMess = F.PositionNameID;
        swalSuccess("Success", SuccessMess);
    }
    F.SuccessSave4 = function () {
        var SuccessMess = "Department Successfully Added";
        swalSuccess("Success", SuccessMess);
    }
    F.SuccessSave3 = function () {
        var SuccessMess = F.PositionNameID;
        swalSuccess("Position Successfully Updated", SuccessMess);
    }
    F.SuccessSave5 = function () {
        var SuccessMess = "Department Successfully Update";
        swalSuccess("Success", SuccessMess);
    }

    F.GetListOfSignatories = function () {
        P({
            url: '/FileMaintenanceSignatories/GetListOfSignatories',
        }).then(function (response) {
            $("#SignatoriesTableID").html(response.data);
        });
        F.Get_SignatoriesAdd();
    }
    F.Get_SignatoriesAdd = function () {
        P({
            url: '/FileMaintenanceSignatories/Get_SignatoriesAdd',
        }).then(function (response) {
            $("#SignatoriesIDTemp").html(response.data);
        });
        F.LoadDynamicDDPositionName();
        F.LoadDynamicDDDepartment();
    }
    F.LoadDynamicDDPositionName = function () {
        P({
            url: '/FileMaintenanceSignatories/GetDynamicSignatories',
        }).then(function (response) {
            $("#_DynamicDDPositionNameID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDDepartment = function () {
        var DepartmentIDTempID = $("#DepartmentIDTempID").val();
        P({
            url: '/FileMaintenanceSignatories/GetSelectedDynamicDepartment',
            params: { DepartmentIDTempID: DepartmentIDTempID }
        }).then(function (response) {
            $("#_DynamicDDDepartmentID").html(response.data);
            F.LoadDynamicDDFunction();
        });
    }
    F.LoadSelectedDynamicDDPositionName = function () {
        var PositionIDTempID = $("#PositionIDTempID").val();
        P({
            url: '/FileMaintenanceSignatories/GetSelectedDynamicSignatories',
            params: { PositionIDTempID: PositionIDTempID }
        }).then(function (response) {
            $("#_DynamicDDPositionNameID").html(response.data);
        });
    }
    F.Get_UpdateSignatories = function (SignatoriesID) {
        P({
            url: '/FileMaintenanceSignatories/Get_UpdateSignatories',
            params: { SignatoriesID: SignatoriesID }
        }).then(function (response) {
            $("#SignatoriesIDTemp").html(response.data);
            F.LoadSelectedDynamicDDPositionName();
            F.LoadSelectedDynamicDDDepartment();
        });
    }
    F.Get_DeleteSignatories = function (SignatoriesID) {
        P({
            url: '/FileMaintenanceSignatories/DeleteSignature',
            params: { SignatoriesID: SignatoriesID }
        }).then(function (response) {
            F.GetListOfSignatories();
        });
    }
    F.LoadDynamicDDDepartment = function () {
        P({
            url: '/FileMaintenanceSignatories/GetDynamicDepartment',
        }).then(function (response) {
            $("#_DynamicDDDepartmentID").html(response.data);
            F.LoadDynamicDDFunction();
        });
    }
    F.LoadDynamicDDFunction = function () {
        var DepartmentID = $("#DepartmentIDD").val();
        P({
            url: '/FileMaintenanceSignatories/GetDynamicFunction',
            params: { DepartmentID: DepartmentID }
        }).then(function (response) {
            $("#_DynamicDDFunctionID").html(response.data);
        });
    }
});
//Chart of Accounts
app.controller('FMRevisonYearController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.RevisionYearTab = function () {
        F.tab1 = null;
        P({
            url: '/FileMaintenanceChartofAccount/RevisionYearTab',
        }).then(function (response) {
            F.tab1 = D(response.data);
            F.Get_RevisionYearTable();
        });
    }
    F.AllotmentClassTab = function () {
        F.tab2 = null;
        P({
            url: '/FileMaintenanceChartofAccount/AllotmentClassTab',
        }).then(function (response) {
            F.tab2 = D(response.data);
            F.Get_AllotmentClassTable();
        });
    }
    F.AccountGroupTab = function () {
        F.tab3 = null;
        P({
            url: '/FileMaintenanceChartofAccount/AccountGroupTab',
        }).then(function (response) {
            F.tab3 = D(response.data);
            F.Get_AccountGroupTable();
        });
    }
    F.MajorAccountGroupTab = function () {
        F.tab4 = null;
        P({
            url: '/FileMaintenanceChartofAccount/MajorAccountGroupTab',
        }).then(function (response) {
            F.tab4 = D(response.data);
            F.Get_MajorAccountGroupTable();
        });
    }
    F.SubMajorAccountGroupTab = function () {
        F.tab5 = null;
        P({
            url: '/FileMaintenanceChartofAccount/SubMajorAccountGroupTab',
        }).then(function (response) {
            F.tab5 = D(response.data);
            F.Get_SubMajorAccountGroupTable();
        });
    }
    F.GeneralAccountGroupTab = function () {
        F.tab6 = null;
        P({
            url: '/FileMaintenanceChartofAccount/GeneralAccountGroupTab',
        }).then(function (response) {
            F.tab6 = D(response.data);
            F.Get_GeneralAccountTable();
        });
    }
    F.SubsidiaryLedgerTab = function () {
        F.tab7 = null;
        P({
            url: '/FileMaintenanceChartofAccount/SubsidiaryLedgerTab',
        }).then(function (response) {
            F.tab7 = D(response.data);
            //F.Get_GeneralAccountTable();
        });
    }
    //Revision Year
    F.Get_RevisionYearTable = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_RevisionYearTable',
        }).then(function (response) {
            $("#_RevisionYearTableID").html(response.data);
        });
        F.Get_AddRevisionYear();
    }
    F.Get_AddRevisionYear = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_AddRevisionYear',
        }).then(function (response) {
            $("#_RevisionYearID").html(response.data);
        });
    }
    F.Get_UpdateRevisionYear = function (RevisionYearID) {
        P({
            url: '/FileMaintenanceChartofAccount/Get_UpdateRevisionYear',
            params: { RevisionYearID: RevisionYearID }
        }).then(function (response) {
            $("#_RevisionYearID").html(response.data);
        });
    }
    F.Get_DeleteRevisionYear = function (RevisionYearID) {
        P({
            url: '/FileMaintenanceChartofAccount/DeleteRevisionYear',
            params: { RevisionYearID: RevisionYearID }
        }).then(function (response) {
            F.Get_RevisionYearTable();
        });
    }

    //Allotment Class
    F.Get_AllotmentClassTable = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_AllotmentClassTable',
        }).then(function (response) {
            $("#_AllotmentClassTableID").html(response.data);
        });
        F.Get_AddAllotmentClass();
    }
    F.Get_AddAllotmentClass = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_AddAllotmentClass',
        }).then(function (response) {
            $("#_AllotmentClassID").html(response.data);
            F.LoadDynamicDDRevisionYear();
        });
    }
    F.LoadDynamicDDRevisionYear = function () {
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicRevisionYear',
        }).then(function (response) {
            $("#_DynamicDDRevisionYearID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDRevisionYear = function () {
        var RevisionYearDateTempID = $("#RevisionYearDateTempID").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicRevisionYear',
            params: { RevisionYearDateTempID: RevisionYearDateTempID }
        }).then(function (response) {
            $("#_DynamicDDRevisionYearID").html(response.data);
        });
    }
    F.Get_UpdateAllotmentClass = function (AllotmentClassID) {
        P({
            url: '/FileMaintenanceChartofAccount/Get_UpdateAllotmentClass',
            params: { AllotmentClassID: AllotmentClassID }
        }).then(function (response) {
            $("#_AllotmentClassID").html(response.data);
            F.LoadSelectedDynamicDDRevisionYear();
        });
    }
    F.Get_DeleteAllotmentClass = function (AllotmentClassID) {
        P({
            url: '/FileMaintenanceChartofAccount/DeleteAllotmentClass',
            params: { AllotmentClassID: AllotmentClassID }
        }).then(function (response) {
            F.Get_AllotmentClassTable();
        });
    }

    //Account Group
    F.Get_AccountGroupTable = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_AccountGroupTable',
        }).then(function (response) {
            $("#_AccountGroupTableID").html(response.data);
        });
        F.Get_AddAccountGroup();
    }
    F.Get_AddAccountGroup = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_AddAccountGroup',
        }).then(function (response) {
            $("#_AccountGroupID").html(response.data);
            F.LoadDynamicDDAccountGroupRevisionYear();
        });
    }
    F.LoadDynamicDDAccountGroupRevisionYear = function () {
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicAccountGroupRevisionYear',
        }).then(function (response) {
            $("#_AccountGroupRevisionYearID").html(response.data);
            F.LoadDynamicDDAccountGroupAllotmentClass();
        });
    }
    F.LoadDynamicDDAccountGroupAllotmentClass = function () {
        var RevisionYearID = $("#AccountGroupRevisionYearNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicAccountGroupAllotmentClass',
            params: { RevisionYearID: RevisionYearID }
        }).then(function (response) {
            $("#_AccountGroupAllotmentClassID").html(response.data);
        });
    }
    F.Get_UpdateAccountGroup = function (AccountGroupID) {
        P({
            url: '/FileMaintenanceChartofAccount/Get_UpdateAccountGroup',
            params: { AccountGroupID: AccountGroupID }
        }).then(function (response) {
            $("#_AccountGroupID").html(response.data);
            F.LoadSelectedDynamicDDAccountGroupRevisionYear();
        });
    }
    F.LoadSelectedDynamicDDAccountGroupRevisionYear = function () {
        var RevisionYearDateTempID = $("#RevisionYearDateTempID").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicAccountGroupRevisionYear',
            params: { RevisionYearDateTempID: RevisionYearDateTempID }
        }).then(function (response) {
            $("#_AccountGroupRevisionYearID").html(response.data);
            F.LoadSelectedDynamicDDAccountGroupAllotmentClass();
        });
    }
    F.LoadSelectedDynamicDDAccountGroupAllotmentClass = function () {
        var RevisionYearID = $("#RevisionYearDateTempID").val();
        var AllotmentClassIDTempID = $("#AllotmentClassIDTempID").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicAccountGroupAllotmentClass',
            params: { RevisionYearID: RevisionYearID, AllotmentClassIDTempID: AllotmentClassIDTempID }
        }).then(function (response) {
            $("#_AccountGroupAllotmentClassID").html(response.data);
        });
    }
    F.Get_DeleteAccountGroup = function (AccountGroupID) {
        P({
            url: '/FileMaintenanceChartofAccount/DeleteAccountGroup',
            params: { AccountGroupID: AccountGroupID }
        }).then(function (response) {
            F.Get_AccountGroupTable();
        });
    }

    //Major Account Group
    F.Get_MajorAccountGroupTable = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_MajorAccountGroupTable',
        }).then(function (response) {
            $("#MajorAccountGroupTableID").html(response.data);
        });
        F.Get_AddMajorAccountGroup();
    }
    F.Get_AddMajorAccountGroup = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_MajorAddAccountGroup',
        }).then(function (response) {
            $("#_MajorAccountGroupID").html(response.data);
            F.LoadDynamicDDMajorAccountGroupRevisionYear();
        });
    }
    F.LoadDynamicDDMajorAccountGroupRevisionYear = function () {
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicMajorAccountGroupRevisionYear',
        }).then(function (response) {
            $("#_MajorRevisionYearID").html(response.data);
            F.LoadDynamicDDMajorAccountGroupAllotmentClass();
        });
    }
    F.LoadDynamicDDMajorAccountGroupAllotmentClass = function () {
        var RevisionYearID = $("#MajorAccountGroupRevisionYearNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicMajorAccountGroupAllotmentClass',
            params: { RevisionYearID: RevisionYearID }
        }).then(function (response) {
            $("#_MajorAllotmentClassID").html(response.data);
            F.LoadDynamicDDMajorAccountGroupAccountGroupName();
        });
    }
    F.LoadDynamicDDMajorAccountGroupAccountGroupName = function () {
        var AllotmentClassID = $("#MajorAccountGroupAllotmentClassNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicMajorAccountGroupAccountGroupName',
            params: { AllotmentClassID: AllotmentClassID }
        }).then(function (response) {
            $("#_MajorAccountGroupNameID").html(response.data);
            F.LoadDynamicLMajorAccountCode();
        });
    }
    F.LoadDynamicLMajorAccountCode = function () {
        var AccountGroupID = $("#MajorAccountGroupAccountGroupNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetMajorAccountCodeField',
            params: { AccountGroupID: AccountGroupID },
        }).then(function (response) {
            $("#_DynamicMajorAccountCode").html(response.data);
        });
    }
    F.Get_UpdateMajorAccountGroup = function (MajorAccountGroupID) {
        P({
            url: '/FileMaintenanceChartofAccount/Get_UpdateMajorAccountGroup',
            params: { MajorAccountGroupID: MajorAccountGroupID }
        }).then(function (response) {
            $("#_MajorAccountGroupID").html(response.data);
            F.LoadSelectedDynamicDDMajorAccountGroupRevisionYear();
        });
    }
    F.LoadSelectedDynamicDDMajorAccountGroupRevisionYear = function () {
        var RevisionYearDateTempID = $("#RevisionYearDateTempID").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicMajorAccountGroupRevisionYear',
            params: { RevisionYearDateTempID: RevisionYearDateTempID }
        }).then(function (response) {
            $("#_MajorRevisionYearID").html(response.data);
            F.LoadSelectedDynamicDDMajorAccountGroupAllotmentClass();
        });
    }
    F.LoadSelectedDynamicDDMajorAccountGroupAllotmentClass = function () {
        var RevisionYearID = $("#MajorAccountGroupRevisionYearNameIDD").val();
        var AllotmentClassIDTempID = $("#AllotmentClassIDTempID").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicMajorAccountGroupAllotmentClass',
            params: { RevisionYearID: RevisionYearID, AllotmentClassIDTempID: AllotmentClassIDTempID }
        }).then(function (response) {
            $("#_MajorAllotmentClassID").html(response.data);
            F.LoadSelectedDynamicDDMajorAccountGroupAccountGroupName();
        });
    }
    F.LoadSelectedDynamicDDMajorAccountGroupAccountGroupName = function () {
        var AllotmentClassID = $("#MajorAccountGroupAllotmentClassNameIDD").val();
        var AccountGroupIDTempID = $("#AccountGroupIDTempID").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicMajorAccountGroupAccountGroupName',
            params: { AllotmentClassID: AllotmentClassID, AccountGroupIDTempID: AccountGroupIDTempID }
        }).then(function (response) {
            $("#_MajorAccountGroupNameID").html(response.data);
            F.LoadDynamicLMajorAccountCode();
        });
    }
    F.Get_DeleteMajorAccountGroup = function (MajorAccountGroupID) {
        P({
            url: '/FileMaintenanceChartofAccount/DeleteMajorAccountGroup',
            params: { MajorAccountGroupID: MajorAccountGroupID }
        }).then(function (response) {
            F.Get_MajorAccountGroupTable();
        });
    }

    //Sub Major Account Group
    F.Get_SubMajorAccountGroupTable = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_SubMajorAccountGroupTable',
        }).then(function (response) {
            $("#_SubMajorAccountGroupTableID").html(response.data);
        });
        F.Get_AddSubMajorAccountGroup();
    }
    F.Get_AddSubMajorAccountGroup = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_SubMajorAddAccountGroup',
        }).then(function (response) {
            $("#_SubMajorAccountGroupID").html(response.data);
            F.LoadDynamicDDSubMajorAccountGroupRevisionYear();
        });
    }
    F.LoadDynamicDDSubMajorAccountGroupRevisionYear = function () {
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicSubMajorAccountGroupRevisionYear',
        }).then(function (response) {
            $("#_SubMajorRevisionYearID").html(response.data);
            F.LoadDynamicDDSubMajorAccountGroupAllotmentClass();
        });
    }
    F.LoadDynamicDDSubMajorAccountGroupAllotmentClass = function () {
        var RevisionYearID = $("#SubMajorAccountGroupRevisionYearNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicSubMajorAccountGroupAllotmentClass',
            params: { RevisionYearID: RevisionYearID }
        }).then(function (response) {
            $("#_SubMajorAllotmentClassID").html(response.data);
            F.LoadDynamicDDSubMajorAccountGroupAccountGroupName();
        });
    }
    F.LoadDynamicDDSubMajorAccountGroupAccountGroupName = function () {
        var AllotmentClassID = $("#SubMajorAccountGroupAllotmentClassNameIDD").val();
        var AccountGroupIDTempID2 = $("#AccountGroupIDTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicSubMajorAccountGroupAccountGroupName',
            params: { AllotmentClassID: AllotmentClassID, AccountGroupIDTempID2: AccountGroupIDTempID2 }
        }).then(function (response) {
            $("#_SubMajorAccountGroupNameID").html(response.data);
            F.LoadDynamicLSubMajorAccountCode();
            F.LoadDynamicDDSubMajorAccountGroupMajorAccountGroupName();
        });
    }
    F.LoadDynamicDDSubMajorAccountGroupMajorAccountGroupName = function () {
        var AccountGroupID = $("#SubMajorAccountGroupAccountGroupNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicSubMajorAccountGroupMajorAccountGroupName',
            params: { AccountGroupID: AccountGroupID }
        }).then(function (response) {
            $("#_DynamicMajorAccountGroupNameID").html(response.data);
            F.LoadDynamicLSubMajorAccountCode2();
        });
    }
    F.LoadDynamicLSubMajorAccountCode = function () {
        var AccountGroupID = $("#SubMajorAccountGroupAccountGroupNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSubMajorAccountCodeField',
            params: { AccountGroupID: AccountGroupID },
        }).then(function (response) {
            $("#_DynamicSubMajorAccountCode1").html(response.data);
        });
    }
    F.LoadDynamicLSubMajorAccountCode2 = function () {
        var MajorAccountGroupID = $("#SubMajorAccountGroupMajorAccountGroupNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSubMajorAccountCodeField2',
            params: { MajorAccountGroupID: MajorAccountGroupID },
        }).then(function (response) {
            $("#_DynamicSubMajorAccountCode2").html(response.data);
        });
    }
    F.Get_UpdateSubMajorAccountGroup = function (SubMajorAccountGroupID) {
        P({
            url: '/FileMaintenanceChartofAccount/Get_UpdateSubMajorAccountGroup',
            params: { SubMajorAccountGroupID: SubMajorAccountGroupID }
        }).then(function (response) {
            $("#_SubMajorAccountGroupID").html(response.data);
            F.LoadSelectedDynamicDDSubMajorAccountGroupRevisionYear();
        });
    }
    F.LoadSelectedDynamicDDSubMajorAccountGroupRevisionYear = function () {
        var RevisionYearDateTempID2 = $("#RevisionYearDateTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicSubMajorAccountGroupRevisionYear',
            params: { RevisionYearDateTempID2: RevisionYearDateTempID2 }
        }).then(function (response) {
            $("#_SubMajorRevisionYearID").html(response.data);
            F.LoadSelectedDynamicDDSubMajorAccountGroupAllotmentClass();
        });
    }
    F.LoadSelectedDynamicDDSubMajorAccountGroupAllotmentClass = function () {
        var RevisionYearID = $("#RevisionYearDateTempID2").val();
        var AllotmentClassIDTempID2 = $("#AllotmentClassIDTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicSubMajorAccountGroupAllotmentClass',
            params: { RevisionYearID: RevisionYearID, AllotmentClassIDTempID2: AllotmentClassIDTempID2 }
        }).then(function (response) {
            $("#_SubMajorAllotmentClassID").html(response.data);
            F.LoadSelectedDynamicDDSubMajorAccountGroupAccountGroupName();
        });
    }
    F.LoadSelectedDynamicDDSubMajorAccountGroupAccountGroupName = function () {
        var AllotmentClassID = $("#SubMajorAccountGroupAllotmentClassNameIDD").val();
        var AccountGroupIDTempID2 = $("#AccountGroupIDTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicSubMajorAccountGroupAccountGroupName',
            params: { AllotmentClassID: AllotmentClassID, AccountGroupIDTempID2: AccountGroupIDTempID2 }
        }).then(function (response) {
            $("#_SubMajorAccountGroupNameID").html(response.data);
            F.LoadDynamicLSubMajorAccountCode();
            F.LoadSelectedDynamicDDSubMajorAccountGroupMajorAccountGroupName();
        });
    }
    F.LoadSelectedDynamicDDSubMajorAccountGroupMajorAccountGroupName = function () {
        var AccountGroupID = $("#SubMajorAccountGroupAccountGroupNameIDD").val();
        var MajorAccountGroupNameTempID2 = $("#MajorAccountGroupNameTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicSubMajorAccountGroupMajorAccountGroupName',
            params: { AccountGroupID: AccountGroupID, MajorAccountGroupNameTempID2: MajorAccountGroupNameTempID2 }
        }).then(function (response) {
            $("#_DynamicMajorAccountGroupNameID").html(response.data);
            F.LoadDynamicLSubMajorAccountCode2();
        });
    }
    F.Get_DeleteSubMajorAccountGroup = function (SubMajorAccountGroupID) {
        P({
            url: '/FileMaintenanceChartofAccount/DeleteSubMajorAccountGroup',
            params: { SubMajorAccountGroupID: SubMajorAccountGroupID }
        }).then(function (response) {
            F.Get_SubMajorAccountGroupTable();
        });
    }

    //General Account
    F.Get_GeneralAccountTable = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_GeneralAccountTable',
        }).then(function (response) {
            $("#_GeneralAccountTableID").html(response.data);
        });
        F.Get_AddGeneralAccount();
    }
    F.Get_AddGeneralAccount = function () {
        P({
            url: '/FileMaintenanceChartofAccount/Get_AddGeneralAccount',
        }).then(function (response) {
            $("#_GeneralAccountID").html(response.data);
            F.LoadDynamicDDGeneralAccountRevisionYear();
            F.LoadDynamicDDGeneralAccountName();
        });
    }
    F.LoadDynamicDDGeneralAccountRevisionYear = function () {
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicGeneralAccountRevisionYear',
        }).then(function (response) {
            $("#_GeneralRevisionYearID").html(response.data);
            F.LoadDynamicDDGeneralAccountAllotmentClass();
        });
    }
    F.LoadDynamicDDGeneralAccountAllotmentClass = function () {
        var RevisionYearID = $("#GeneralAccountRevisionYearNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicGeneralAccountAllotmentClass',
            params: { RevisionYearID: RevisionYearID }
        }).then(function (response) {
            $("#_GeneralAllotmentClassID").html(response.data);
            F.LoadDynamicDDGeneralAccountGroupName();
        });
    }
    F.LoadDynamicDDGeneralAccountGroupName = function () {
        var AllotmentClassID = $("#GeneralAccountAllotmentClassNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicGeneralAccountGroupName',
            params: { AllotmentClassID: AllotmentClassID }
        }).then(function (response) {
            $("#_GeneralAccountGroupID").html(response.data);
            F.LoadDynamicLGeneralAccountCode();
            F.LoadDynamicDDGeneralAccountMajorAccountGroupName();
        });
    }
    F.LoadDynamicDDGeneralAccountMajorAccountGroupName = function () {
        var AccountGroupID = $("#GeneralAccountAccountGroupNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicGeneralAccountMajorGroupName',
            params: { AccountGroupID: AccountGroupID }
        }).then(function (response) {
            $("#_GeneralMajorAccountID").html(response.data);
            F.LoadDynamicLGeneralAccountCode2();
            F.LoadDynamicDDGeneralAccountSubMajorAccountGroupName();
        });
    }
    F.LoadDynamicLGeneralAccountCode = function () {
        var AccountGroupID = $("#GeneralAccountAccountGroupNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetGeneralAccountCodeField',
            params: { AccountGroupID: AccountGroupID },
        }).then(function (response) {
            $("#_DynamicGeneralAccountCode1").html(response.data);
        });
    }
    F.LoadDynamicLGeneralAccountCode2 = function () {
        var MajorAccountGroupID = $("#GeneralAccountMajorAccountGroupNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetGeneralAccountCodeField2',
            params: { MajorAccountGroupID: MajorAccountGroupID },
        }).then(function (response) {
            $("#_DynamicGeneralAccountCode2").html(response.data);
        });
    }
    F.LoadDynamicLGeneralAccountCode3 = function () {
        var SubMajorAccountGroupID = $("#GeneralAccountSubMajorAccountGroupNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetGeneralAccountCodeField3',
            params: { SubMajorAccountGroupID: SubMajorAccountGroupID },
        }).then(function (response) {
            $("#_DynamicGeneralAccountCode3").html(response.data);
        });
    }
    F.LoadDynamicDDGeneralAccountSubMajorAccountGroupName = function () {
        var MajorAccountGroupID = $("#GeneralAccountMajorAccountGroupNameIDD").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicGeneralAccountSubMajorGroupName',
            params: { MajorAccountGroupID: MajorAccountGroupID }
        }).then(function (response) {
            $("#_GeneralSubMajorAccountID").html(response.data);
            F.LoadDynamicLGeneralAccountCode3();
        });
    }
    F.LoadDynamicDDGeneralAccountName = function () {
        P({
            url: '/FileMaintenanceChartofAccount/GetDynamicGeneralAccountName',
        }).then(function (response) {
            $("#_GeneralAccountNameID").html(response.data);
        });
    }

    F.Get_UpdateGeneralAccount = function (GeneralAccountID) {
        P({
            url: '/FileMaintenanceChartofAccount/Get_UpdateGeneralAccountGroup',
            params: { GeneralAccountID: GeneralAccountID }
        }).then(function (response) {
            $("#_GeneralAccountID").html(response.data);
            F.LoadSelectedDynamicDDGeneralAccountRevisionYear();
        });
    }
    F.LoadSelectedDynamicDDGeneralAccountRevisionYear = function () {
        var RevisionYearDateTempID2 = $("#RevisionYearDateTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicGeneralAccountRevisionYear',
            params: { RevisionYearDateTempID2: RevisionYearDateTempID2 }
        }).then(function (response) {
            $("#_GeneralRevisionYearID").html(response.data);
            F.LoadSelectedDynamicDDGeneralAccountAllotmentClass();
        });
    }
    F.LoadSelectedDynamicDDGeneralAccountAllotmentClass = function () {
        var RevisionYearID = $("#GeneralAccountRevisionYearNameIDD").val();
        var AllotmentClassIDTempID2 = $("#AllotmentClassIDTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicGeneralAccountAllotmentClass',
            params: { RevisionYearID: RevisionYearID, AllotmentClassIDTempID2: AllotmentClassIDTempID2 }
        }).then(function (response) {
            $("#_GeneralAllotmentClassID").html(response.data);
            F.LoadSelectedDynamicDDGeneralAccountGroupName();
        });
    }
    F.LoadSelectedDynamicDDGeneralAccountGroupName = function () {
        var AllotmentClassID = $("#GeneralAccountAllotmentClassNameIDD").val();
        var AccountGroupIDTempID2 = $("#AccountGroupIDTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicGeneralAccountGroupName',
            params: { AllotmentClassID: AllotmentClassID, AccountGroupIDTempID2: AccountGroupIDTempID2 }
        }).then(function (response) {
            $("#_GeneralAccountGroupID").html(response.data);
            F.LoadDynamicLGeneralAccountCode();
            F.LoadSelectedDynamicDDGeneralAccountMajorAccountGroupName();
        });
    }
    F.LoadSelectedDynamicDDGeneralAccountMajorAccountGroupName = function () {
        var AccountGroupID = $("#GeneralAccountAccountGroupNameIDD").val();
        var MajorAccountGroupNameTempID2 = $("#MajorAccountGroupNameTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicGeneralAccountMajorGroupName',
            params: { AccountGroupID: AccountGroupID, MajorAccountGroupNameTempID2: MajorAccountGroupNameTempID2 }
        }).then(function (response) {
            $("#_GeneralMajorAccountID").html(response.data);
            F.LoadDynamicLGeneralAccountCode2();
            F.LoadSelectedDynamicDDGeneralAccountSubMajorAccountGroupName();
        });
    }
    F.LoadSelectedDynamicDDGeneralAccountSubMajorAccountGroupName = function () {
        var MajorAccountGroupID = $("#GeneralAccountMajorAccountGroupNameIDD").val();
        var SubMajorAccountGroupNameTempID2 = $("#SubMajorAccountGroupNameTempID2").val();
        P({
            url: '/FileMaintenanceChartofAccount/GetSelectedDynamicGeneralAccountSubMajorGroupName',
            params: { MajorAccountGroupID: MajorAccountGroupID, SubMajorAccountGroupNameTempID2: SubMajorAccountGroupNameTempID2 }
        }).then(function (response) {
            $("#_GeneralSubMajorAccountID").html(response.data);
            F.LoadDynamicLGeneralAccountCode3();
        });
    }
});
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
//Payee
app.controller('FMPayeeController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.InitAllFunction = function () {
        F.Get_PayeeTable();
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
    F.Get_PayeeTable = function () {
        P({
            url: '/FileMaintenancePayee/Get_OfficeTypeTable',
        }).then(function (response) {
            $("#_PayeeTableID").html(response.data);
        });
        F.Get_AddPayee();
    }
    F.LoadDynamicDDDepartment = function () {
        P({
            url: '/FileMaintenancePayee/GetDynamicDepartment',
        }).then(function (response) {
            $("#_DepartmentNameID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDDepartment = function () {
        var DepartmentIDTempID = $("#DepartmentIDTempID").val();
        P({
            url: '/FileMaintenancePayee/GetSelectedDynamicDepartment',
            params: { DepartmentIDTempID: DepartmentIDTempID }
        }).then(function (response) {
            $("#_DepartmentNameID").html(response.data);
        });
    }
    F.Get_AddPayee = function () {
        P({
            url: '/FileMaintenancePayee/Get_AddOfficeType',
        }).then(function (response) {
            $("#_PayeeID").html(response.data);
            F.LoadDynamicDDDepartment();
        });
    }
    F.Get_UpdatePayee = function (PayeeID) {
        P({
            url: '/FileMaintenancePayee/Get_UpdatePayee',
            params: { PayeeID: PayeeID }
        }).then(function (response) {
            $("#_PayeeID").html(response.data);
            F.LoadSelectedDynamicDDDepartment();
        });
    }
    F.Get_DeletePayee = function (PayeeID) {
        P({
            url: '/FileMaintenancePayee/DeletePayee',
            params: { PayeeID: PayeeID }
        }).then(function (response) {
            F.Get_PayeeTable();
        });
    }
});
//Taxes
app.controller('FMTaxController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.InitAllFunction = function () {
        F.Get_TaxTable();
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

    F.Get_TaxTable = function () {
        P({
            url: '/FileMaintenanceTaxes/Get_TaxTable',
        }).then(function (response) {
            $("#_TaxesTableID").html(response.data);
            F.Get_AddTax();
        });
    }
    F.Get_AddTax = function () {
        P({
            url: '/FileMaintenanceTaxes/Get_AddTax',
        }).then(function (response) {
            $("#_TaxesID").html(response.data);
            F.LoadDynamicDDGeneralAccountName();
        });
    }
    F.LoadDynamicDDGeneralAccountName = function () {
        P({
            url: '/FileMaintenanceTaxes/GetDynamicGeneralAccount',
        }).then(function (response) {
            $("#_DDGeneralAccountID").html(response.data);
        });
    }
    F.LoadSelectedDynamicDDGeneralAccountName = function () {
        var GeneralAccountTempID = $("#GeneralAccountTempID").val();
        P({
            url: '/FileMaintenanceTaxes/GetDynamicSelectedGeneralAccount',
            params: { GeneralAccountTempID: GeneralAccountTempID }
        }).then(function (response) {
            $("#_DDGeneralAccountID").html(response.data);
        });
    }
    F.Get_UpdateTax = function (TaxID) {
        P({
            url: '/FileMaintenanceTaxes/Get_UpdateTax',
            params: { TaxID: TaxID }
        }).then(function (response) {
            $("#_TaxesID").html(response.data);
            F.LoadSelectedDynamicDDGeneralAccountName();
        });
    }
    F.Get_DeleteTax = function (TaxID) {
        P({
            url: '/FileMaintenanceTaxes/DeleteTax',
            params: { TaxID: TaxID }
        }).then(function (response) {
            F.Get_TaxTable();
        });
    }
});
//Supplier
app.controller('FMSupplierController', function ($scope, $http, $sce) {
    var F = $scope, P = $http, D = $sce.trustAsHtml;

    F.InitAllFunction = function () {
        F.Get_SupplierTable();
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

    F.Get_SupplierTable = function () {
        P({
            url: '/FileMaintenanceSupplier/Get_SupplierTable',
        }).then(function (response) {
            $("#_SupplierTableID").html(response.data);
            F.Get_AddSupplier();
        });
    }
    F.Get_AddSupplier = function () {
        P({
            url: '/FileMaintenanceSupplier/Get_AddSupplier',
        }).then(function (response) {
            $("#_SupplierID").html(response.data);
        });
    }
    F.Get_UpdateSupplier = function (SupplierID) {
        P({
            url: '/FileMaintenanceSupplier/Get_UpdateSupplier',
            params: { SupplierID: SupplierID }
        }).then(function (response) {
            $("#_SupplierID").html(response.data);
        });
    }
    F.Get_DeleteSupplier = function (SupplierID) {
        P({
            url: '/FileMaintenanceSupplier/DeleteSupplier',
            params: { SupplierID: SupplierID }
        }).then(function (response) {
            F.Get_SupplierTable();
        });
    }
});