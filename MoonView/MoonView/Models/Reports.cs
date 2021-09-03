using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MoonView.Models
{
    public class Reports : Common
    {
        public string RegistryName { get; set; }
        public string BlockName { get; set; }
        public string SectorName { get; set; }
        public string SiteName { get; set; }
        public string SiteTypeID { get; set; }
        public List<Reports> lstPlotAvailabilityReport { get; set; }
        public string BookingNo { get; set; }
        public string MobileNo { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public List<Reports> lstRegistry { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string PK_BookingId { get; set; }
        public string BookingNumber { get; set; }
        public List<Reports> lstP { get; set; }
        public List<Reports> FullPaymentList { get; set; }
        public string PlotAmount { get; set; }
        public string CustomerID { get; set; }
        public string AssociateID { get; set; }
        public string ActualPlotRate { get; set; }
        public string PlotRate { get; set; }
        public string PayAmount { get; set; }
        public string BranchName { get; set; }
        public string BookingStatus { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLoginID { get; set; }
        public string AssociateName { get; set; }
        public string PK_PLCCharge { get; set; }
        public string NetPlotAmount { get; set; }
        public string AssociateLoginID { get; set; }
        public string BookingDate { get; set; }
        public string BookingAmount { get; set; }
        public string BranchID { get; set; }
        public string SelectedValue { get; set; }

        public string Discount { get; set; }
        public string PaymentPlanID { get; set; }
        public string PlanName { get; set; }
        public string TotalAllotmentAmount { get; set; }
        public string PaidAllotmentAmount { get; set; }
        public string BalanceAllotmentAmount { get; set; }
        public string TotalInstallment { get; set; }
     
        public string Balance { get; set; }
        public string PlotArea { get; set; }
        public string PK_BookingDetailsId { get; set; }
        public string InstallmentNo { get; set; }
        public string InstallmentDate { get; set; }
        public string PaymentDate { get; set; }
        public string PaidAmount { get; set; }
        public string InstallmentAmount { get; set; }
        public string PaymentMode { get; set; }
        public string DueAmount { get; set; }
        public string Remarks { get; set; }
        public string isRegistry { get; set; }
        public string PlotNumber { get; set; }
        public string SiteID { get; set; }
        public string PlotID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }

        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId )};
            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }

        public DataSet GetFullpaymentList()
        {
            SqlParameter[] para = {
                     new SqlParameter("@SiteID",SiteID),
                     new SqlParameter("@SectorID",SectorID),
                     new SqlParameter("@BlockID",BlockID),
                     new SqlParameter("@PlotNumber",PlotNumber),
                     new SqlParameter("@BookingNo",BookingNumber)
                  };
            DataSet ds = Connection.ExecuteQuery("GetFullpaymentList", para);
            return ds;
        }

        public DataSet UpdatingRegistryStatus()
        {
            SqlParameter[] para = { new SqlParameter("@BookingNo", BookingNo),
            new SqlParameter("@Fk_UserId", Fk_UserId),
            new SqlParameter("@Remarks", Remarks),
             new SqlParameter("@isRegister", isRegistry),
            new SqlParameter("@AddedBy", AddedBy),
           new SqlParameter("@RegistryName", RegistryName),
             };
            DataSet ds = Connection.ExecuteQuery("UpdateRegistryStatus", para);
            return ds;
        }


        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }

        public DataSet GetSectorList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetSectorList", para);
            return ds;
        }

        public DataSet GetBlockList()
        {
            SqlParameter[] para ={ new SqlParameter("@SiteID",SiteID),
                                     new SqlParameter("@SectorID",SectorID),
                                     new SqlParameter("@BlockID",BlockID),
                                 };
            DataSet ds = Connection.ExecuteQuery("GetBlockList", para);
            return ds;
        }

        public DataSet FillBookedPlotDetails()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber),
                                 new SqlParameter("@BookingNo",BookingNumber)

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForAllotment", para);
            return ds;
        }

        public DataSet GetRegistryList()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@LoginId",LoginId),
                                new SqlParameter("@FromDate",FromDate),
                                new SqlParameter("@ToDate",ToDate),
                                new SqlParameter("@BookingNo",BookingNo),
                                 new SqlParameter("@PlotNumber",PlotNumber)

                            };
            DataSet ds = Connection.ExecuteQuery("GetRegisteredPlot", para);
            return ds;
        }

        public DataSet GetSiteTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("SiteTypeList");
            return ds;
        }

        public DataSet GetPlotAvailabilityReport()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID),
                                  new SqlParameter("@SectorID", SectorID),
                                  new SqlParameter("@BlockID", BlockID),
                                  new SqlParameter("@PlotID", PlotID),
                                  new SqlParameter("@PlotNumber", PlotNumber),
                                   new SqlParameter("@Status", Status),
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotAvailabilityReport", para);
            return ds;
        }

    }
}


