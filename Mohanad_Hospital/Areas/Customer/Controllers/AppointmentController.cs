using Hospital.DataAccess.Repository.IRepository;
using Hospital.Models;
using Hospital.Models.ViewModels;
using Hospital.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Mohanad_Hospital.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class AppointmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public AppointmentVM AppointmentVM { get; set; }
        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppointmentVM = new()
            {
                AppointmentList = _unitOfWork.Appointment.GetAll(u => u.ApllicationUserID == userId,
                includeProperties: "Doctor")
            };


            

            return View(AppointmentVM);
        }
        public IActionResult AppointmentSummary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppointmentVM = new()
            {
                AppointmentList = _unitOfWork.Appointment.GetAll(u => u.ApllicationUserID == userId,
                includeProperties: "Doctor"),
                AppointmentHeader = new()
            };
            AppointmentVM.AppointmentHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            AppointmentVM.AppointmentHeader.TheName = AppointmentVM.AppointmentHeader.ApplicationUser.TheName;
            AppointmentVM.AppointmentHeader.City = AppointmentVM.AppointmentHeader.ApplicationUser.City;
            AppointmentVM.AppointmentHeader.Region = AppointmentVM.AppointmentHeader.ApplicationUser.Region;
            AppointmentVM.AppointmentHeader.Number = AppointmentVM.AppointmentHeader.ApplicationUser.Number;



            return View(AppointmentVM);
        }
        [HttpPost]
        [ActionName("AppointmentSummary")]
        public IActionResult AppointmentSummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            AppointmentVM.AppointmentList = _unitOfWork.Appointment.GetAll(u => u.ApllicationUserID == userId,
            includeProperties: "Doctor");
            AppointmentVM.AppointmentHeader.ApplicationUserId = userId;
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);



            AppointmentVM.AppointmentHeader.ApoointmentStatus = SD.StatusPending;
            AppointmentVM.AppointmentHeader.PaymentStatus = SD.PaymentStatusPending;
            _unitOfWork.AppointmentHeader.Add(AppointmentVM.AppointmentHeader);
            _unitOfWork.Save();

            foreach(var appointment in AppointmentVM.AppointmentList)
            {
                AppointmentDetail appointmentDetail = new()
                {
                    DoctorId = appointment.Id,
                    AppointmentHeaderId = AppointmentVM.AppointmentHeader.Id
                };
                _unitOfWork.AppointmentDetail.Add(appointmentDetail);
                _unitOfWork.Save();
            }
            var DOMAIN = "https://localhost:7259/";
            var options = new SessionCreateOptions
            {

                SuccessUrl = DOMAIN + $"customer/appointment/AppointmentConfirmation?id={AppointmentVM.AppointmentHeader.Id}",
                CancelUrl = DOMAIN + "customer/appointment/index",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };
            
            
            foreach(var item in AppointmentVM.AppointmentList)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Doctor.DoctorName
                        }

                    }

                };
                options.LineItems.Add(sessionLineItem);
                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.AppointmentHeader.UpdateStripePaymentID(
                    AppointmentVM.AppointmentHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }






            return RedirectToAction(nameof(AppointmentConfirmation),new {id = AppointmentVM.AppointmentHeader.Id});


        }
        public IActionResult AppointmentConfirmation(int id)
        {
         
            return View(id);
        }
    }
}
