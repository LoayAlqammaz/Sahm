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

        public ActionResult Index(int page = 1)
        {
            int pageSize = 6; 
            var users = db.Users.OrderBy(u => u.UserId).ToList(); 
            var pagedUsers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)users.Count / pageSize);

            return View(pagedUsers);
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
        public ActionResult MakePayment(int userId, double amount)
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


        [HttpPost]
        public ActionResult AddUser(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var newUser = new User { Name = name };
                db.Users.Add(newUser);
                db.SaveChanges();

                GenerateQRCodeForUser(newUser.UserId);

                return Json(new { success = true, message = "User added successfully!" });
            }

            return Json(new { success = false, message = "Please provide a name." });
        }

        [HttpGet]
        public ActionResult GetUser(int userId)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var user = db.Users.Find(userId);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." }, JsonRequestBehavior.AllowGet);
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult EditUser(int userId, string name)
        {
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }
            user.Name = name;
            db.SaveChanges();

            return Json(new { success = true, message = "User updated successfully." });
        }

        [HttpPost]
        public JsonResult DeleteUser(int userId)
        {
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }
            var filePath = Server.MapPath($"~/Content/QR/User{userId}.png");
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"User deleted, but an error occurred while deleting QR code: {ex.Message}" });
                }
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return Json(new { success = true, message = "User and QR code deleted successfully." });
        }



        private void GenerateQRCodeForUser(int userId)
        {
            var directoryPath = Server.MapPath("~/Content/QR");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var qrCodeContent = Url.Action("UserDetails", "Home", new { userId }, Request.Url.Scheme);
            var qrCode = GenerateQRCode(qrCodeContent);
            var filePath = Path.Combine(directoryPath, $"User{userId}.png");
            System.IO.File.WriteAllBytes(filePath, qrCode);
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