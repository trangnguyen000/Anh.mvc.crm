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

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            if (Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName == "vie")
            {
                var data = await _frontEndLogic.GetEmployeeByAbout(Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName);
                return View(data);
            }
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
            if (!ModelState.IsValid)
            {
                var listTextError = ModelState.Where(c => c.Value.Errors.Count > 0).Select(c => c.Value.Errors).SelectMany(c => c.Select(d => d.ErrorMessage)).ToList();
                TempData["AlertMessage"] = $"Thông tin nội dung không đúng: "+(string.Join(", ", listTextError));
                TempData["AlertType"] = "alert-danger";
                return RedirectToAction("Contact", "Home");
            }
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
                    StudyProgramName = model.country,
                    PhoneNumber = model.phoneNumber,
                    Status = (short)Common.StatusContactSupport.New,
                };
                await _businessLogic.SaveContractSupport(modelContractSupport, null);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("rnituyensinh", "rnituyensinh@gmail.com"));
                message.To.Add(new MailboxAddress("rnituyensinh", "kipavninfo@gmail.com"));
                message.Subject = $"[Đăng ký tư vấn] {model.name}";
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<b>This is some html text</b>";
                //bodyBuilder.TextBody = "This is some plain text";
                var subjectNew = "";
                if (model.subject != null)
                {
                    subjectNew = TextToHtml(model.subject);
                }
                message.Body = new TextPart("html")
                {
                    Text = $@"<div style=""font-size:22px;color: #000000;font-weight: 700;"">Thông tin người đăng ký:</div>
                        <div><span style=""font-size: 14px;color: #000000;font-weight: 600;padding-left: 40px;"">Họ và tên: </span><span style=""color: #000000;font-weight: 500;font-size: 18px;"">{model.name},</span></div>
                        <div><span style=""font-size: 14px;color: #000000;font-weight: 600;padding-left: 40px;"">Số điện thoại: </span><span style=""color: #000000;font-weight: 500;font-size: 18px;"">{model.phoneNumber},</span></div>
                        <div><span style=""font-size: 14px;color: #000000;font-weight: 600;padding-left: 40px;"">Chương trình quan tâm: </span><span style=""color: #000000;font-weight: 500;font-size: 18px;"">{model.country},</span></div>
                        <div><span style=""font-size: 14px;color: #000000;font-weight: 600;padding-left: 40px;"">Câu hỏi: </span><span style=""color: #000000;display: inline-grid;font-weight: 500;font-size: 18px;"">{subjectNew}</span></div>"
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("rnituyensinh@gmail.com", "bsnn fjza yksg adhi");
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
        public static string TextToHtml(string text)
        {
            text = HttpUtility.HtmlEncode(text);
            text = text.Replace("\r\n", "\r");
            text = text.Replace("\n", "\r");
            text = text.Replace("\r", "<br>\r\n");
            text = text.Replace("  ", " &nbsp;");
            return text;
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
        public async Task<ActionResult> CompanyKorea()
        {
            if (Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName != "vie")
            {
                var data = await _frontEndLogic.GetEmployeeByAbout(Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName);
                return View(data);
            }
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