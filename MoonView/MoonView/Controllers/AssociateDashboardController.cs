using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoonView.Filter;
using MoonView.Models;

namespace MoonView.Controllers
{
    public class AssociateDashboardController : BaseController
    {

        public ActionResult ClosingWisePayoutDetails(string PK_PaidPayoutId)
        {
            AssociateBooking model = new AssociateBooking();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            model.PK_PaidPayoutId = PK_PaidPayoutId;
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetPayoutWiseIncomeDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.PK_PaidPayoutId = r["PK_PaidPayoutId"].ToString();
                    obj.DisplayName = r["Name"].ToString();
                    ViewBag.DisplayName = r["Name"].ToString();
                    ViewBag.ClosingDate = r["ClosingDate"].ToString();
                    ViewBag.PayoutNo = r["PayoutNo"].ToString();
                    ViewBag.UserId = r["LoginId"].ToString();
                    obj.CustomerID = r["CustomerId"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.CommPercentage = r["DifferencePerc"].ToString();
                    lst.Add(obj);
                }
                model.ClosingWisePayoutlist = lst;
            }
            return View(model);
        }

        public ActionResult AssociateDashBoard()
        {
            //ViewBag.Name = Session["FullName"].ToString();
            AssociateBooking newdata = new AssociateBooking();
            try
            {
                List<AssociateBooking> lstInst = new List<AssociateBooking>();
                newdata.AssociateID = Session["Pk_userId"].ToString();
                DataSet dsInst = newdata.GetDueInstallmentList();
                if (dsInst != null && dsInst.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsInst.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();

                        obj.CustomerID = r["PK_UserId"].ToString();
                        obj.CustomerLoginID = r["LoginId"].ToString();
                        obj.CustomerName = r["FirstName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        obj.InstallmentAmount = r["InstAmt"].ToString();


                        lstInst.Add(obj);
                    }

                    newdata.ListInstallment = lstInst;
                }
                List<AssociateBooking> lstNews = new List<AssociateBooking>();
                newdata.NewsFor = "Associate";
                DataSet dsAssociate = newdata.GetNews();
                if (dsAssociate != null && dsAssociate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsAssociate.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();
                        //   obj.PK_BookingId = r["PK_UserId"].ToString();

                        obj.PK_NewsID = r["PK_NewsID"].ToString();
                        obj.NewsHeading = r["NewsHeading"].ToString();
                        obj.NewsBody = r["NewsBody"].ToString();


                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lstNews.Add(obj);
                    }

                    newdata.ListNEWS = lstNews;
                }

                List<AssociateBooking> totallist = new List<AssociateBooking>();
                newdata.AssociateID = Session["Pk_userId"].ToString();
                DataSet ds = newdata.GetAssociateForDashboard();
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.TotalAssociate = ds.Tables[0].Rows[0]["TotalAssociate"].ToString();
                    ViewBag.TotalBusiness = ds.Tables[0].Rows[0]["TotalBusiness"].ToString();
                    ViewBag.TotalActiveId = ds.Tables[0].Rows[0]["TotalActiveId"].ToString();
                    ViewBag.SelfBusiness = ds.Tables[0].Rows[0]["SelfBusiness"].ToString();
                    ViewBag.TeamBusiness = ds.Tables[0].Rows[0]["TeamBusiness"].ToString();
                    ViewBag.TotalBooking = ds.Tables[0].Rows[0]["TotalBooking"].ToString();
                    ViewBag.TeamBooking = ds.Tables[0].Rows[0]["TeamBooking"].ToString();
                    ViewBag.SelfBooking = ds.Tables[0].Rows[0]["SelfBooking"].ToString();
                    ViewBag.Totalregistry = ds.Tables[0].Rows[0]["Totalregistry"].ToString();
                    ViewBag.SelfRegistry = ds.Tables[0].Rows[0]["SelfRegistry"].ToString();
                    ViewBag.TeamRegistry = ds.Tables[0].Rows[0]["TeamRegistry"].ToString();
                    ViewBag.VisitorFundpaid = ds.Tables[0].Rows[0]["VisitorFundpaid"].ToString();
                    ViewBag.TargetBusiness = ds.Tables[0].Rows[0]["TargetBusiness"].ToString();
                    ViewBag.TargetPercentage = ds.Tables[0].Rows[0]["TargetPercentage"].ToString();
                    ViewBag.RemainingTargetPercent = ds.Tables[0].Rows[0]["RemainingTargetPercent"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(newdata);
        }

        public ActionResult GetGraphDetailsOfPlot()
        {
            List<AssociateBooking> dataList = new List<AssociateBooking>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            AssociateBooking newdata = new AssociateBooking();
            newdata.LoginId = Session["LoginId"].ToString();
            Ds = newdata.BindGraphDetails();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    AssociateBooking details = new AssociateBooking();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPlotBookingOnGraph()
        {

            List<AssociateBooking> dataList3 = new List<AssociateBooking>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            AssociateBooking newdata = new AssociateBooking();
            newdata.AssociateID = Session["Pk_userId"].ToString();

            Ds = newdata.GetDetailsForBarGraph();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[0].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    AssociateBooking details = new AssociateBooking();


                    details.TotalBooking = (dr["TotalBooking"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList3.Add(details);

                    count++;
                }
            }
            return Json(dataList3, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Plot model = new Plot();
                model.SiteID = SiteID;

                #region GetSiteRate
                //DataSet dsSiteRate = model.GetSiteList();
                //if (dsSiteRate != null)
                //{
                //    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                //    model.Result = "yes";
                //}
                #endregion
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                model.Result = "yes";
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                model.ddlSector = ddlSector;
                #endregion
                //#region SitePLCCharge
                //List<Master> lstPlcCharge = new List<Master>();
                //DataSet dsPlcCharge = model.GetPLCChargeList();

                //if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                //{
                //    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                //    {
                //        Master obj = new Master();
                //        obj.SiteName = r["SiteName"].ToString();
                //        obj.PLCName = r["PLCName"].ToString();
                //        obj.PLCCharge = r["PLCCharge"].ToString();
                //        obj.PLCID = r["PK_PLCID"].ToString();

                //        lstPlcCharge.Add(obj);
                //    }
                //    model.lstPLC = lstPlcCharge;
                //}
                //#endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetBlockList(string SiteID, string SectorID)
        {
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Master model = new Master();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            DataSet dsblock = model.GetBlockList();

            #region ddlBlock
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

            model.lstBlock = lstBlock;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                Common model = new Common();
                model.Pincode = Pincode;

                #region GetStateCity
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State = model.City = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #region AssociateBookings


        public ActionResult AssociateBookingList(AssociateBooking model)
        {
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }

        [HttpPost]
        [ActionName("AssociateBookingList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult BookingList(AssociateBooking model)
        {
            model.LoginId = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.List();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.BookingStatus = r["BookingStatus"].ToString();
                    obj.PK_BookingId = r["PK_BookingId"].ToString();
                    obj.BranchID = r["BranchID"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.CustomerID = r["CustomerID"].ToString();
                    obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateID"].ToString();
                    obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.BookingAmount = r["BookingAmt"].ToString();
                    obj.PaymentPlanID = r["PlanName"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.NetPlotAmount = r["NetPlotAmount"].ToString();
                    obj.PK_PLCCharge = r["PLCCharge"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }


        #endregion

        #region Customer Ledger Report
        public ActionResult CustomerLedgerReport(string PK_BookingId)
        {


            AssociateBooking model = new AssociateBooking();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PK_BookingId = PK_BookingId;

            DataSet dsBookingDetails = model.GetBookingDetailsList();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }

        public ActionResult Details(string BookingNumber, string LoginId, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            AssociateBooking model = new AssociateBooking();
            model.LoginId = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();

            model.SiteID = SiteID == "0" ? null : SiteID;
            model.SectorID = SectorID == "0" ? null : SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
            DataSet dsblock = model.FillDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";
                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();


                }

            }
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[1].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.PK_BookingId = r["Fk_BookingId"].ToString();
                    obj.InstallmentNo = r["InstallmentNo"].ToString();
                    obj.InstallmentDate = r["InstallmentDate"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.InstallmentAmount = r["InstAmt"].ToString();
                    obj.PaymentMode = r["PaymentModeName"].ToString();
                    obj.DueAmount = r["DueAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            else
            {
                model.Result = "No record found !";

            }
            #region ddlSite
            int count1 = 0;
            Master objmaster = new Master();
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = objmaster.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = objmaster.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = objmaster.GetBlockList();


            if (dsblock1 != null && dsblock1.Tables.Count > 0 && dsblock1.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock1.Tables[0].Rows)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            #endregion
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region AssociateDownline

        public ActionResult AssociateDownlineDetail(AssociateBooking model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }


        #endregion 

        #region AssociateUpline

        public ActionResult AssociateUplineDetail(AssociateBooking model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }


        #endregion

        #region ChangePasswordAssociate


        public ActionResult ChangePasswordAssociate()
        {

            return View();
        }
        [HttpPost]
        [ActionName("ChangePasswordAssociate")]
        [OnAction(ButtonName = "Change")]
        public ActionResult UpdatePassword(AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.Password = Crypto.Encrypt(obj.Password);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                obj.UpdatedBy = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Login"] = "Password updated successfully..";
                        FormName = "Login";
                        Controller = "Home";
                    }
                    else
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangePasswordAssociate";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "ChangePasswordAssociate";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }

        
        #endregion

        #region EditProfile


        public ActionResult EditProfile(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            model.LoginId = Session["LoginId"].ToString();

            try
            {
                model.UserID = Session["Pk_userId"].ToString();
                model.LoginId = Session["LoginId"].ToString();
                DataSet dsPlotDetails = model.GetList();
                if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                {
                    model.UserID = Session["Pk_userId"].ToString();
                    model.SponsorID = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                    model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                    model.DesignationID = dsPlotDetails.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.DesignationName = dsPlotDetails.Tables[0].Rows[0]["DesignationName"].ToString();
                    model.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                    model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                    model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                    model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                    model.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                    model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.BranchName = dsPlotDetails.Tables[0].Rows[0]["BranchName"].ToString();
                    model.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();

                    model.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    model.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                    model.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                    model.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                }

                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        [ActionName("EditProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult Update(HttpPostedFileBase postedFile, AssociateBooking model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (postedFile != null)
                {
                    model.ProfilePic = "/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                    Session["ProfilePic"] = model.ProfilePic;
                }
                model.UpdatedBy = Session["Pk_userId"].ToString();

                DataSet ds = model.UpdatePersonalDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = " Updated successfully !";
                    }
                    else
                    {
                        TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "EditProfile";
            Controller = "AssociateDashboard";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region Tree
        public ActionResult Tree1(DashBoard model)
        {
            model.LoginId = Session["LoginId"].ToString();

            List<DashBoard> lst = new List<DashBoard>();

            DataSet ds = model.GetTreeDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DashBoard obj = new DashBoard();
                    obj.Fk_ParentId = r["Parentid"].ToString();
                    obj.DisplayName = r["MemberName"].ToString();
                    obj.Fk_UserId = r["PK_UserId"].ToString();
                    lst.Add(obj);
                }
                model.lsttree = lst;
            }

            return View(model);


        }

        public ActionResult Tree()
        {
            
            return View();
        }
        #endregion
        
        #region  associate tree
        public ActionResult AssociateTree(AssociateBooking model, string AssociateID)
        {
            if (AssociateID != null)
            {
                model.Fk_UserId = AssociateID;
            }
            else
            {
                model.Fk_UserId = Session["Pk_UserId"].ToString();
            }
            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineTree();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Fk_SponsorId = ds.Tables[0].Rows[0]["Fk_SponsorId"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();

                    obj.Status = r["Status"].ToString();
                    obj.ActiveStatus = r["ActiveStatus"].ToString();
                    lst.Add(obj);

                }
                model.lstPlot = lst;
            }


            return View(model);
        }

        //[HttpPost]
        //[ActionName("AssociateTree")]
        //[OnAction(ButtonName = "Search")]
        //public ActionResult GetDownlineList(AssociateBooking model)
        //{
        //    model.LoginId = Session["LoginId"].ToString();
        //    List<AssociateBooking> lst = new List<AssociateBooking>();

        //    DataSet ds = model.GetDownlineDetails();

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds.Tables[0].Rows)
        //        {
        //            AssociateBooking obj = new AssociateBooking();
        //            obj.AssociateID = r["AssociateId"].ToString();
        //            obj.AssociateName = r["AssociateName"].ToString();
        //            obj.DesignationName = r["DesignationName"].ToString();
        //            obj.Percentage = r["Percentage"].ToString();
        //            obj.BranchName = r["BranchName"].ToString();
        //            obj.Contact = r["Mobile"].ToString();
        //            lst.Add(obj);
        //        }
        //        model.lstPlot = lst;
        //    }

        //    return View(model);
        //}
        #endregion

        #region UserReward

        public ActionResult UserReward(AssociateBooking model)
        {

            model.UserID = Session["Pk_userId"].ToString();
            model.RewardID = "1";

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.RewardList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.Status = r["Status"].ToString();
                    obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Contact = r["BackColor"].ToString();
                    obj.PK_RewardItemId = r["PK_RewardItemId"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }

        public ActionResult ClaimReward(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.PK_RewardItemId = id;
                obj.Status = "Claim";
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.ClaimReward();
                if(ds!=null && ds.Tables[0].Rows.Count>0)
                {
                    if(ds.Tables[0].Rows[0]["Msg"].ToString()=="1")
                    {
                        TempData["Rewardmsg"] = "Reward Claimed";
                    }
                    else
                    {
                        TempData["Rewardmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch(Exception ex)
            {
                TempData["Rewardmsg"] = ex.Message;
            }
            return RedirectToAction("UserReward");
        }
        public ActionResult SkipReward(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.PK_RewardItemId = id;
                obj.Status = "Skip";
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Rewardmsg"] = "Reward Skipped";
                    }
                    else
                    {
                        TempData["Rewardmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Rewardmsg"] = ex.Message;
            }
            return RedirectToAction("UserReward");
        }
        #endregion

        #region UnpaidIncomeReport
        public ActionResult UnpaidIncome(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.FromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("UnpaidIncome")]
        [OnAction(ButtonName = "Search")]
        public ActionResult UnpaidIncomeBy(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.UnpaidIncomes();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.FromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        #endregion

        #region PayoutLedger
        public ActionResult PayoutRequest(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            DataSet Ds = model.GetPayoutBalance();
            ViewBag.Balance = Ds.Tables[0].Rows[0]["Balance"].ToString();
            
            return View(model);
        }

        [HttpPost]
        [ActionName("PayoutRequest")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePayoutRequest(AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                
                obj.AddedBy = Session["Pk_userId"].ToString();
                obj.UserID = Session["LoginId"].ToString();
                DataSet ds = obj.SavePayoutRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["PayoutReq"] = " Payout requested successfully !";
                        try
                        {
                            string mob = ds.Tables[0].Rows[0]["Column1"].ToString();
                            string name = ds.Tables[0].Rows[0]["Column3"].ToString();
                            BLSMS.SendSMS(mob, "Dear " + name + ", Your request of Payout Request of Rs " + ds.Tables[0].Rows[0]["Column2"].ToString() + " has been processed Successfully. Your amount will credited in your account within 1 to 5 working days.");
                            BLSMS.SendSMS("9115003366", "Payout request raised by " + name + " ( Login Id - "+ obj.UserID + "), of Rs " + ds.Tables[0].Rows[0]["Column2"].ToString() + " on "+ DateTime.Now);
                        }
                        catch { }

                    }
                    else
                    {
                        TempData["PayoutReq"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PayoutReq"] = ex.Message;
            }
            FormName = "PayoutRequest";
            Controller = "AssociateDashboard";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult PayoutDetails(AssociateBooking model )
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    obj.PK_PaidPayoutId = r["PK_PaidPayoutId"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("PayoutDetails")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutDetailsBy(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }

        public ActionResult PayoutLedger(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutLedger();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayoutWalletID = r["Pk_PayoutWalletId"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.Credit = r["Credit"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TType = r["TransactionType"].ToString();
                     obj.Debit = r["Debit"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        #endregion

        #region PayoutRequestReport
        public ActionResult PayoutRequestReport(AssociateBooking model)
        {
            model.Status = model.Status == "0" ? null : model.Status;
            model.UserID = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            #region ddl 
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion  

            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutRequestReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutRequestReportBy(AssociateBooking model)
        {
            model.UserID = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddl 
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion  
            return View(model);
        }
        #endregion

        #region KYC

        public ActionResult KYCDocuments()
        {
            AssociateBooking model = new AssociateBooking();

            model.UserID = Session["Pk_userId"].ToString();
            AssociateBooking obj = new AssociateBooking();
            DataSet ds = model.GetKYCDocuments();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.AdharNumber = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.AdharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.AdharStatus = "Status : " + ds.Tables[0].Rows[0]["AdharStatus"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                obj.PanStatus = "Status : " + ds.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.Status = "Status : " + ds.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.DocumentNumber = ds.Tables[0].Rows[0]["DocumentNumber"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                obj.DocumentStatus = "Status : " + ds.Tables[0].Rows[0]["DocumentStatus"].ToString();
                obj.AccountHolderName = ds.Tables[0].Rows[0]["BankHolderName"].ToString();
                obj.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
            }
            return View(obj);
        }

        [HttpPost]
        [ActionName("KYCDocuments")]
        [OnAction(ButtonName = "btnUpdateAdhar")]
        public ActionResult KYCDocuments(IEnumerable<HttpPostedFileBase> AdharFile, IEnumerable<HttpPostedFileBase> PanFile, IEnumerable<HttpPostedFileBase> DocumentFile, AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
             
            try
            {
                if (AdharFile != null)
                {
                    foreach (var file in AdharFile)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            //E:\BitBucket\DolphinZone\MoonView\files\assets\images\

                            obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));

                        }

                    }
                    obj.UserID = Session["Pk_userId"].ToString();
                    obj.ActionStatus = "Adhar";
                    DataSet ds = obj.UploadKYCDocuments();
                    if(ds!= null && ds.Tables[0].Rows[0]["Msg"].ToString()=="0")
                    {
                        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                }
                if(PanFile != null)
                {
                    foreach (var file in PanFile)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            //E:\BitBucket\DolphinZone\MoonView\files\assets\images\

                            obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));


                        }

                    }
                    obj.UserID = Session["Pk_userId"].ToString();
                    obj.ActionStatus = "Pan";
                    DataSet ds = obj.UploadKYCDocuments();

                }

                if (DocumentFile != null)
                {
                    foreach (var file in DocumentFile)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            //E:\BitBucket\DolphinZone\MoonView\files\assets\images\

                            obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));


                        }

                    }
                    obj.UserID = Session["Pk_userId"].ToString();
                    obj.ActionStatus = "Doc";
                    DataSet ds = obj.UploadKYCDocuments();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                        {
                            TempData["DocumentUpload"] = "Document uploaded successfully..";
                            FormName = "KYCDocuments";
                            Controller = "AssociateDashboard";
                            try
                            {
                                string KYCUploadedyMessage ="Dear " + ds.Tables[0].Rows[0]["FirstName"].ToString()+ ", your kyc uploaded and kyc will be approved in 4-5 working days.";
                                BLSMS.SendSMS(ds.Tables[0].Rows[0]["Mobile"].ToString(), KYCUploadedyMessage);
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "KYCDocuments";
                            Controller = "AssociateDashboard";
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                TempData["DocumentUpload"] = ex.Message;
                FormName = "KYCDocuments";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("KYCDocuments")]
        [OnAction(ButtonName = "btnUpdatePan")]
        public ActionResult KYCDocuments2(IEnumerable<HttpPostedFileBase> postedFile, AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
           
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        //E:\BitBucket\DolphinZone\MoonView\files\assets\images\
                        
                            obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));
                        

                    }
                     
                }

                obj.UserID = Session["Pk_userId"].ToString();
                obj.ActionStatus = "Pan";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DocumentUpload"] = "Document uploaded successfully..";
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                    else
                    {
                        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DocumentUpload"] = ex.Message;
                FormName = "KYCDocuments";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("KYCDocuments")]
        [OnAction(ButtonName = "btnUpdateDoc")]
        public ActionResult KYCDocuments3(IEnumerable<HttpPostedFileBase> postedFile, AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
            
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        //E:\BitBucket\DolphinZone\MoonView\files\assets\images\
                         
                            obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));
                        

                    }
                    
                }

                obj.UserID = Session["Pk_userId"].ToString();
                obj.ActionStatus = "Doc";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DocumentUpload"] = "Document uploaded successfully..";
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                    else
                    {
                        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DocumentUpload"] = ex.Message;
                FormName = "KYCDocuments";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        public ActionResult EnquiryList(Master model)
        {

            List<Master> lst = new List<Master>();

            DataSet ds = model.EnquiryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EnquiryID = r["PK_EnquiryMasterId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Details = r["Details"].ToString();

                    lst.Add(obj);
                }

                model.lstBlock1 = lst;
            }
                return View(model);
             
        }
        [HttpPost]
        [ActionName("EnquiryList")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveEnquiry(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_userId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveEnquiry();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Enquiry"] = "Enquiry saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Enquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Enquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Enquiry"] = ex.Message;
            }
            return RedirectToAction("EnquiryList", "AssociateDashboard");
        }


        public ActionResult PlotAvailability(AssociateBooking model)
        {
            #region ddlSiteType
            AssociateBooking objSiteType = new AssociateBooking();
            int count1 = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion


            List<SelectListItem> ddlSite = new List<SelectListItem>();
            ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
            ViewBag.ddlSite = ddlSite;

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View();
        }

        public ActionResult GetSiteBySiteType(string SiteTypeID)
        {
            try
            {
                AssociateBooking model = new AssociateBooking();
                model.SiteTypeID = SiteTypeID;

                #region GetSite
                List<SelectListItem> ddlSite = new List<SelectListItem>();
                DataSet dsSector = model.GetSiteList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });

                    }
                }
                model.ddlSite = ddlSite;
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetSitesDetails(string SiteID)
        {
            try
            {
                AssociateBooking model = new AssociateBooking();
                model.SiteID = SiteID;

                #region GetSiteRate
                DataSet dsSiteRate = model.GetSiteList();
                if (dsSiteRate != null)
                {
                    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                    model.Result = "yes";
                }
                #endregion
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                model.ddlSector = ddlSector;
                #endregion
                #region SitePLCCharge
                List<AssociateBooking> lstPlcCharge = new List<AssociateBooking>();
                DataSet dsPlcCharge = model.GetPLCChargeList();

                if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                {
                    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();
                        obj.SiteName = r["SiteName"].ToString();
                        obj.PLCName = r["PLCName"].ToString();
                        obj.PLCCharge = r["PLCCharge"].ToString();
                        obj.PLCID = r["PK_PLCID"].ToString();

                        lstPlcCharge.Add(obj);
                    }
                    model.lstPLC = lstPlcCharge;
                }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("PlotAvailability")]
        [OnAction(ButtonName = "Search")]

        public ActionResult Details(AssociateBooking model)
        {
            //Master model = new Master();
            List<AssociateBooking> lst = new List<AssociateBooking>();

            //model.SiteID = SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.SiteTypeID = model.SiteTypeID == "0" ? null : model.SiteTypeID;
            DataSet dsblock1 = model.GetPlotDetails();
            if (dsblock1 != null && dsblock1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock1.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.SectorID = r["FK_SectorID"].ToString();
                    obj.BlockID = r["FK_BlockID"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotStatus = r["Status"].ToString();
                    obj.ColorCSS = r["ColorCSS"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    //model.PlotID = dsblock.Tables[0].Rows[0]["PK_PLotID"].ToString();
                    //model.SiteID = dsblock.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    //model.SectorID = dsblock.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    //model.BlockID = dsblock.Tables[0].Rows[0]["FK_BlockID"].ToString();
                    //model.PlotNumber = dsblock.Tables[0].Rows[0]["PlotNumber"].ToString();
                    //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                    lst.Add(obj);
                }

                model.lstPlot = lst;
            }

            #region ddlSite
            int count1 = 0;
            AssociateBooking objmaster = new AssociateBooking();
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock = model.GetBlockList();


            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            #endregion

            #region ddlSiteType
            AssociateBooking objSiteType = new AssociateBooking();
            int countType = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = model.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (countType == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    countType = countType + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            return View(model);
        }
    }
}
