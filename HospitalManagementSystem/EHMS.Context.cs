﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalManagementSystem
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EHMSEntities : DbContext
    {
        public EHMSEntities()
            : base("name=EHMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdmissionDipositFee> AdmissionDipositFees { get; set; }
        public virtual DbSet<AgeRange> AgeRanges { get; set; }
        public virtual DbSet<AllowanceSetup> AllowanceSetups { get; set; }
        public virtual DbSet<BankReconcilation> BankReconcilations { get; set; }
        public virtual DbSet<BillCancelSummary> BillCancelSummaries { get; set; }
        public virtual DbSet<BloodDonate> BloodDonates { get; set; }
        public virtual DbSet<BloodStockManagement> BloodStockManagements { get; set; }
        public virtual DbSet<BloodStockManagementMaster> BloodStockManagementMasters { get; set; }
        public virtual DbSet<BloodTransectionDetail> BloodTransectionDetails { get; set; }
        public virtual DbSet<BloodTransectionMaster> BloodTransectionMasters { get; set; }
        public virtual DbSet<BoneMarrowReport> BoneMarrowReports { get; set; }
        public virtual DbSet<CbDeptRefTable> CbDeptRefTables { get; set; }
        public virtual DbSet<CentralizedBilling> CentralizedBillings { get; set; }
        public virtual DbSet<CentralizedBillingDetail> CentralizedBillingDetails { get; set; }
        public virtual DbSet<CentralizedBillingMaster> CentralizedBillingMasters { get; set; }
        public virtual DbSet<CentralizedBillingPaymentType> CentralizedBillingPaymentTypes { get; set; }
        public virtual DbSet<CommissionDetail> CommissionDetails { get; set; }
        public virtual DbSet<CommissionDetaislByType> CommissionDetaislByTypes { get; set; }
        public virtual DbSet<Consulation> Consulations { get; set; }
        public virtual DbSet<CytologyReport> CytologyReports { get; set; }
        public virtual DbSet<DailyJvDetail> DailyJvDetails { get; set; }
        public virtual DbSet<DeductionSetup> DeductionSetups { get; set; }
        public virtual DbSet<DemandPatientAssignment> DemandPatientAssignments { get; set; }
        public virtual DbSet<DepartmentWiseStock> DepartmentWiseStocks { get; set; }
        public virtual DbSet<DepositRefundDetail> DepositRefundDetails { get; set; }
        public virtual DbSet<DesignationSetup> DesignationSetups { get; set; }
        public virtual DbSet<DesignationWiseSalarySetup> DesignationWiseSalarySetups { get; set; }
        public virtual DbSet<DietTypeSetup> DietTypeSetups { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DoctorCommision> DoctorCommisions { get; set; }
        public virtual DbSet<DoctorsDisease> DoctorsDiseases { get; set; }
        public virtual DbSet<DoctorTypeSetup> DoctorTypeSetups { get; set; }
        public virtual DbSet<EmergencyFeeDetail> EmergencyFeeDetails { get; set; }
        public virtual DbSet<EmergencyInvestigationDetail> EmergencyInvestigationDetails { get; set; }
        public virtual DbSet<EmergencyInvestigationMaster> EmergencyInvestigationMasters { get; set; }
        public virtual DbSet<EmergencyMaster> EmergencyMasters { get; set; }
        public virtual DbSet<EmergencyOutcomeType> EmergencyOutcomeTypes { get; set; }
        public virtual DbSet<EmergencyPoliceCase> EmergencyPoliceCases { get; set; }
        public virtual DbSet<EmergencyTreatmentAllergy> EmergencyTreatmentAllergies { get; set; }
        public virtual DbSet<EmergencyTriage> EmergencyTriages { get; set; }
        public virtual DbSet<EmergencyTstCarid> EmergencyTstCarids { get; set; }
        public virtual DbSet<EmployeeAllowanceSetup> EmployeeAllowanceSetups { get; set; }
        public virtual DbSet<EmployeeDeductionSetup> EmployeeDeductionSetups { get; set; }
        public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public virtual DbSet<EmployeeEducationInfo> EmployeeEducationInfoes { get; set; }
        public virtual DbSet<EmployeePayrollMaster> EmployeePayrollMasters { get; set; }
        public virtual DbSet<EmployeeTrainingInfo> EmployeeTrainingInfoes { get; set; }
        public virtual DbSet<GL_AccGroups> GL_AccGroups { get; set; }
        public virtual DbSet<GL_AccountType> GL_AccountType { get; set; }
        public virtual DbSet<GL_AccSubGroups> GL_AccSubGroups { get; set; }
        public virtual DbSet<GL_LedgerMaster> GL_LedgerMaster { get; set; }
        public virtual DbSet<GL_Transaction> GL_Transaction { get; set; }
        public virtual DbSet<HandoverDetail> HandoverDetails { get; set; }
        public virtual DbSet<HospitalExtraTest> HospitalExtraTests { get; set; }
        public virtual DbSet<HospitalModuleMaster> HospitalModuleMasters { get; set; }
        public virtual DbSet<IpdDischarge> IpdDischarges { get; set; }
        public virtual DbSet<IpdMRCommonTest> IpdMRCommonTests { get; set; }
        public virtual DbSet<IpdMrMainTest> IpdMrMainTests { get; set; }
        public virtual DbSet<IpdMrMedicineRecord> IpdMrMedicineRecords { get; set; }
        public virtual DbSet<IpdPatientBedDetail> IpdPatientBedDetails { get; set; }
        public virtual DbSet<IpdPatientDiagnosisDetail> IpdPatientDiagnosisDetails { get; set; }
        public virtual DbSet<IpdPatientroomDetail> IpdPatientroomDetails { get; set; }
        public virtual DbSet<IpdRegistrationMaster> IpdRegistrationMasters { get; set; }
        public virtual DbSet<IpdRoomStatu> IpdRoomStatus { get; set; }
        public virtual DbSet<ItemAllotmentDetail> ItemAllotmentDetails { get; set; }
        public virtual DbSet<ItemAllotmentMaster> ItemAllotmentMasters { get; set; }
        public virtual DbSet<ItemBlockRecord> ItemBlockRecords { get; set; }
        public virtual DbSet<ItemBlockSetup> ItemBlockSetups { get; set; }
        public virtual DbSet<ItemDemandDetail> ItemDemandDetails { get; set; }
        public virtual DbSet<ItemDemandMaster> ItemDemandMasters { get; set; }
        public virtual DbSet<JornalVFormat> JornalVFormats { get; set; }
        public virtual DbSet<JVDetail> JVDetails { get; set; }
        public virtual DbSet<JVMaster> JVMasters { get; set; }
        public virtual DbSet<MedicalRecordDischarge> MedicalRecordDischarges { get; set; }
        public virtual DbSet<MonthlyAllowance> MonthlyAllowances { get; set; }
        public virtual DbSet<MonthlyDeduction> MonthlyDeductions { get; set; }
        public virtual DbSet<OpdFeeDetail> OpdFeeDetails { get; set; }
        public virtual DbSet<OpdMaster> OpdMasters { get; set; }
        public virtual DbSet<OpdMedicalDetail> OpdMedicalDetails { get; set; }
        public virtual DbSet<OpdMedicalRecordCommonTest> OpdMedicalRecordCommonTests { get; set; }
        public virtual DbSet<OpdMedicalRecordFurtherTest> OpdMedicalRecordFurtherTests { get; set; }
        public virtual DbSet<OpdMedicalRecordMaster> OpdMedicalRecordMasters { get; set; }
        public virtual DbSet<OpdMedicalRecordMedicineRefer> OpdMedicalRecordMedicineRefers { get; set; }
        public virtual DbSet<OpdPatientDoctorDetail> OpdPatientDoctorDetails { get; set; }
        public virtual DbSet<OpdPatientOtherDetail> OpdPatientOtherDetails { get; set; }
        public virtual DbSet<OpeningBalance> OpeningBalances { get; set; }
        public virtual DbSet<OperationRecord> OperationRecords { get; set; }
        public virtual DbSet<OperationTheatreDetail> OperationTheatreDetails { get; set; }
        public virtual DbSet<OperationTheatreMaster> OperationTheatreMasters { get; set; }
        public virtual DbSet<PackageDetail> PackageDetails { get; set; }
        public virtual DbSet<PackageMaster> PackageMasters { get; set; }
        public virtual DbSet<PackagePatientAssignment> PackagePatientAssignments { get; set; }
        public virtual DbSet<PathologyDynamicTestColumn> PathologyDynamicTestColumns { get; set; }
        public virtual DbSet<PatientDepositDetail> PatientDepositDetails { get; set; }
        public virtual DbSet<PatientDepositMaster> PatientDepositMasters { get; set; }
        public virtual DbSet<PatientDietDetail> PatientDietDetails { get; set; }
        public virtual DbSet<PatientLogMaster> PatientLogMasters { get; set; }
        public virtual DbSet<PatientTest> PatientTests { get; set; }
        public virtual DbSet<PatientTestDetail> PatientTestDetails { get; set; }
        public virtual DbSet<PatientTestResult> PatientTestResults { get; set; }
        public virtual DbSet<PayrollMaster> PayrollMasters { get; set; }
        public virtual DbSet<PreRegistration> PreRegistrations { get; set; }
        public virtual DbSet<PreRegistrationPreferDetail> PreRegistrationPreferDetails { get; set; }
        public virtual DbSet<ReferTable> ReferTables { get; set; }
        public virtual DbSet<ResignationDetail> ResignationDetails { get; set; }
        public virtual DbSet<SaveDymanicPathologyTest> SaveDymanicPathologyTests { get; set; }
        public virtual DbSet<ScanFileUpload> ScanFileUploads { get; set; }
        public virtual DbSet<SetupAgent> SetupAgents { get; set; }
        public virtual DbSet<SetupBank> SetupBanks { get; set; }
        public virtual DbSet<SetupBillingType> SetupBillingTypes { get; set; }
        public virtual DbSet<SetupBlock> SetupBlocks { get; set; }
        public virtual DbSet<SetupBloodGroup> SetupBloodGroups { get; set; }
        public virtual DbSet<SetupBloodType> SetupBloodTypes { get; set; }
        public virtual DbSet<SetupCountryName> SetupCountryNames { get; set; }
        public virtual DbSet<SetupDepartment> SetupDepartments { get; set; }
        public virtual DbSet<SetupDiagnosi> SetupDiagnosis { get; set; }
        public virtual DbSet<SetupDoctorAvailableTime> SetupDoctorAvailableTimes { get; set; }
        public virtual DbSet<SetupDoctorDepartment> SetupDoctorDepartments { get; set; }
        public virtual DbSet<SetupDoctorFee> SetupDoctorFees { get; set; }
        public virtual DbSet<SetupDoctorMaster> SetupDoctorMasters { get; set; }
        public virtual DbSet<SetupDoctorQualification> SetupDoctorQualifications { get; set; }
        public virtual DbSet<SetupDoctorSpecialist> SetupDoctorSpecialists { get; set; }
        public virtual DbSet<SetupDoctorTokenRegister> SetupDoctorTokenRegisters { get; set; }
        public virtual DbSet<SetupEmergencyCase> SetupEmergencyCases { get; set; }
        public virtual DbSet<SetupEmergencyFee> SetupEmergencyFees { get; set; }
        public virtual DbSet<SetupEmployeeDepartment> SetupEmployeeDepartments { get; set; }
        public virtual DbSet<SetupEmployeeMaster> SetupEmployeeMasters { get; set; }
        public virtual DbSet<SetupEmployeeShift> SetupEmployeeShifts { get; set; }
        public virtual DbSet<SetupEmployeeShiftMaster> SetupEmployeeShiftMasters { get; set; }
        public virtual DbSet<SetupEmployeeType> SetupEmployeeTypes { get; set; }
        public virtual DbSet<SetupFeeType> SetupFeeTypes { get; set; }
        public virtual DbSet<SetupFiscalYear> SetupFiscalYears { get; set; }
        public virtual DbSet<SetupFloor> SetupFloors { get; set; }
        public virtual DbSet<SetupGender> SetupGenders { get; set; }
        public virtual DbSet<SetupHospitalBillNumber> SetupHospitalBillNumbers { get; set; }
        public virtual DbSet<SetupHospitalDetail> SetupHospitalDetails { get; set; }
        public virtual DbSet<SetupHospitalFee> SetupHospitalFees { get; set; }
        public virtual DbSet<SetupHospitalFeeDiscount> SetupHospitalFeeDiscounts { get; set; }
        public virtual DbSet<SetupIcdCode> SetupIcdCodes { get; set; }
        public virtual DbSet<SetUpIpdRoom> SetUpIpdRooms { get; set; }
        public virtual DbSet<SetupIpdWard> SetupIpdWards { get; set; }
        public virtual DbSet<SetupManpower> SetupManpowers { get; set; }
        public virtual DbSet<SetupMedicalcharge> SetupMedicalcharges { get; set; }
        public virtual DbSet<SetupMemberDependent> SetupMemberDependents { get; set; }
        public virtual DbSet<SetupMemberDiscount> SetupMemberDiscounts { get; set; }
        public virtual DbSet<SetupMemberShip> SetupMemberShips { get; set; }
        public virtual DbSet<SetupMemberType> SetupMemberTypes { get; set; }
        public virtual DbSet<SetupOperationTheatreRoom> SetupOperationTheatreRooms { get; set; }
        public virtual DbSet<SetupOtherDiscount> SetupOtherDiscounts { get; set; }
        public virtual DbSet<SetupOtherTest> SetupOtherTests { get; set; }
        public virtual DbSet<SetupPathoTest> SetupPathoTests { get; set; }
        public virtual DbSet<SetupRelationship> SetupRelationships { get; set; }
        public virtual DbSet<SetupRoomType> SetupRoomTypes { get; set; }
        public virtual DbSet<SetupSection> SetupSections { get; set; }
        public virtual DbSet<SetupStockCategory> SetupStockCategories { get; set; }
        public virtual DbSet<SetupStockItemEntry> SetupStockItemEntries { get; set; }
        public virtual DbSet<SetupStockItemType> SetupStockItemTypes { get; set; }
        public virtual DbSet<SetupStockSubCategory> SetupStockSubCategories { get; set; }
        public virtual DbSet<SetupStockSupplier> SetupStockSuppliers { get; set; }
        public virtual DbSet<SetupStockUnit> SetupStockUnits { get; set; }
        public virtual DbSet<SetupTriageMedium> SetupTriageMediums { get; set; }
        public virtual DbSet<SetupUnit> SetupUnits { get; set; }
        public virtual DbSet<SetupVoucherNumber> SetupVoucherNumbers { get; set; }
        public virtual DbSet<ShiftFromIPDDepartment> ShiftFromIPDDepartments { get; set; }
        public virtual DbSet<StockBreakage> StockBreakages { get; set; }
        public virtual DbSet<StockDistributionDetail> StockDistributionDetails { get; set; }
        public virtual DbSet<StockDistributionMaster> StockDistributionMasters { get; set; }
        public virtual DbSet<StockGroupingItemDetail> StockGroupingItemDetails { get; set; }
        public virtual DbSet<StockGroupingItemMaster> StockGroupingItemMasters { get; set; }
        public virtual DbSet<StockItemGroupSetup> StockItemGroupSetups { get; set; }
        public virtual DbSet<StockItemGroupSubGroup> StockItemGroupSubGroups { get; set; }
        public virtual DbSet<StockItemLocationSetup> StockItemLocationSetups { get; set; }
        public virtual DbSet<StockItemMaster> StockItemMasters { get; set; }
        public virtual DbSet<StockItemPurchase> StockItemPurchases { get; set; }
        public virtual DbSet<StockItemPurchaseDetail> StockItemPurchaseDetails { get; set; }
        public virtual DbSet<StockItemRackSetup> StockItemRackSetups { get; set; }
        public virtual DbSet<StockPurchaseOrder> StockPurchaseOrders { get; set; }
        public virtual DbSet<StockPurchaseOrderDetail> StockPurchaseOrderDetails { get; set; }
        public virtual DbSet<StockReturnIn> StockReturnIns { get; set; }
        public virtual DbSet<StockReturnOut> StockReturnOuts { get; set; }
        public virtual DbSet<SurgeryCharge> SurgeryCharges { get; set; }
        public virtual DbSet<TaxOtherSetup> TaxOtherSetups { get; set; }
        public virtual DbSet<TaxRangeSetup> TaxRangeSetups { get; set; }
        public virtual DbSet<TBLConfigDetail> TBLConfigDetails { get; set; }
        public virtual DbSet<TblDistrict> TblDistricts { get; set; }
        public virtual DbSet<tblFundDeductionSetup> tblFundDeductionSetups { get; set; }
        public virtual DbSet<tblNepaliMonth> tblNepaliMonths { get; set; }
        public virtual DbSet<tblNepaliYear> tblNepaliYears { get; set; }
        public virtual DbSet<TblZone> TblZones { get; set; }
        public virtual DbSet<TrialAudit> TrialAudits { get; set; }
        public virtual DbSet<VDCMUN> VDCMUNs { get; set; }
        public virtual DbSet<VitalForOther> VitalForOthers { get; set; }
        public virtual DbSet<Vital> Vitals { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<Setup_NepCalender> Setup_NepCalender { get; set; }
    }
}
