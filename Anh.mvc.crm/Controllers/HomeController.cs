using Anh.mvc.crm.Authentication;
using Anh.mvc.Models;
using Hangfire;
using Module.BusinessLogic.Dto;
using Module.BusinessLogic.Helper;
using Module.BusinessLogic.Shared;
using Module.BusinessLogic.SharedCore;
using Module.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Configuration;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Web.Services.Description;
using System.Threading.Tasks;

namespace Anh.mvc.crm.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IFrontEndLogic _frontEndLogic;
        private readonly IBusinessLogic _businessLogic;
        private readonly ConfigCurent _config;
        public HomeController()
        {
            _config = new ConfigCurent();
            var unitOfwork = new UnitOfWork();
            _frontEndLogic = new FrontEndLogic(unitOfwork);
            _businessLogic = new BusinessLogic(unitOfwork);
        }
        public ActionResult Index()
        {
            var data = new TinTucViewHomeDto();
            data = _frontEndLogic.GetTinTucByHome();
            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            if (Thread.CurrentThread.CurrentCulture.Name == "vi-VN")
                return View();
            else
                return RedirectToAction("CompanyKorea", "Home");
        }

        public ActionResult Contact()
        {
            ViewBag.StudyPrograms = _frontEndLogic.GetChuyenMuc(KeyCodeDictionary.StudyProgram);
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SendEmail(ContactFormModel model)
        {
            if (model.name is null || model.phoneNumber is null)
            {
                if (model.name is null)
                {
                    TempData["ValidateName"] = "Vui lòng nhập tên";
                }
                if (model.phoneNumber is null)
                {
                    TempData["ValidatePhone"] = "Vui lòng nhập số điện thoại";
                }
                return View("Contact");

            }
            try
            {
                var modelContractSupport = new CreateOrUpdateContactSuportDto()
                {
                    CustomerName = model.name,
                    Description = model.subject,
                    EmailAddress = model.Email,
                    StudyProgramId = model.country,
                    PhoneNumber = model.phoneNumber,
                    Status = (short)Common.StatusContactSupport.New,
                };
                await _businessLogic.SaveContractSupport(modelContractSupport, null);
                var studyProgramName = await _frontEndLogic.GetStudyProgramById(model.country);
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Form mail", "trangmy2909@gmail.com"));
                message.To.Add(new MailboxAddress("To mail", "hoanglinh23vp@gmail.com"));
                message.Subject = $"[Đăng ký tư vấn] {model.name}";

                message.Body = new TextPart("plain")
                {
                    Text = $@"Thông báo đăng ký tư vấn:
                           Thông tin người đăng ký:
                           Họ và tên:{model.name},
                           Số điện thoại: {model.phoneNumber},
                           Chương trình quan tâm: {studyProgramName},
                           Câu hỏi: {model.subject},"
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("trangmy2909@gmail.com", "owxy itfr uhql dufn");
                    client.Send(message);
                    client.Disconnect(true);
                }
                TempData["AlertMessage"] = "Đăng ký tư vấn thành công! Chúng tôi sẽ sớm liên hệ lại với bạn!";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Contact", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = $"Xảy ra lỗi {ex}";
                TempData["AlertType"] = "alert-danger";
                return RedirectToAction("Contact", "Home");
            }

        }
    
        public ActionResult Master()
        {
            return View();
        }

        public ActionResult D2Visa()
        {
            return View();
        }
        public ActionResult ActivitiesPage(int? id, string title)
        {
            var data = _frontEndLogic.GetTinTucById(id);
            if (data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(data);
        }
        public ActionResult CompanyKorea()
        {
            if (Thread.CurrentThread.CurrentCulture.Name != "vi-VN")
                return View();
            else
                return RedirectToAction("About", "Home");
        }
        public ActionResult _MainLeft()
        {
            var data = _frontEndLogic.GetTopTinTuc(5);
            return PartialView(data);
        }

        public ActionResult D4Visa()
        {
            return View();
        }
        public ActionResult D10Visa()
        {
            return View();
        }
        public ActionResult E7Visa()
        {
            return View();
        }
        public ActionResult HighSchool()
        {
            return View();
        }
        public ActionResult E9()
        {
            return View();
        }
        public ActionResult BusinessKorean()
        {
            return View();
        }
        public ActionResult University()
        {
            return View();
        }
        public ActionResult Handbook()
        {
            return View();
        }
        public ActionResult LearnFree()
        {
            return View();
        }
    }
}