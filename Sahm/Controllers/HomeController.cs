using Sahm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QRCoder; 
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace Sahm.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        public ActionResult UserDetails(int userId)
        {
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult MakePayment(int userId, decimal amount)
        {
            var payment = new Payment
            {
                UserId = userId,
                Amount = amount,
                PaymentDate = DateTime.Now
            };

            db.Payments.Add(payment);
            db.SaveChanges();

            return RedirectToAction("UserDetails", new { userId });
        }

        [HttpPost]
        public ActionResult RequestCar(int userId)
        {
            var carRequest = new CarRequest
            {
                UserId = userId,
                RequestDate = DateTime.Now
            };

            db.CarRequests.Add(carRequest);
            db.SaveChanges();

            return RedirectToAction("UserDetails", new { userId });
        }

        [HttpPost]
        public ActionResult Evaluate(int userId, string comment, string emoji)
        {
            var evaluation = new Evaluation
            {
                UserId = userId,
                Comment = comment,
                Emoji = emoji,
                EvaluationDate = DateTime.Now
            };

            db.Evaluations.Add(evaluation);
            db.SaveChanges();

            return RedirectToAction("UserDetails", new { userId });
        }
        public ActionResult GenerateQRCodes()
        {
            var users = db.Users.Take(2).ToList();

            var directoryPath = Server.MapPath("~/Content/QR");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (var user in users)
            {
                var qrCodeContent = Url.Action("UserDetails", "Home", new { userId = user.UserId }, Request.Url.Scheme);
                var qrCode = GenerateQRCode(qrCodeContent);
                var filePath = Path.Combine(directoryPath, $"User{user.UserId}.png");
                System.IO.File.WriteAllBytes(filePath, qrCode);
            }

            return RedirectToAction("Index");
        }

        private byte[] GenerateQRCode(string content)
        {
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrCodeData))
            using (var qrCodeImage = qrCode.GetGraphic(20))
            {
                using (var ms = new MemoryStream())
                {
                    qrCodeImage.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }
}