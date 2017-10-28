using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.BALTicketingSystem;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class SingUpController : Controller
    {
        public ActionResult Index()
        {
            if (Session["User"] != null) { return RedirectToAction("Index", "Ticket"); }
            User modelo = new Models.User();
            return View(modelo);
        }

        public ActionResult Create(User pUser)
        {
            try
            {
                if (ModelState.IsValid && new BALUser().Create(pUser))
                {
                    return RedirectToAction("CreateConfirmMsg", new { pError = false, pMsg = "El usuario " + pUser.name + " ha sido creado." });
                }
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intetar crear el usuario " + pUser.name + "." });
            }
            catch
            {
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intetar crear el usuario " + pUser.name + "." });
            }
        }

        public ActionResult CreateConfirmMsg(bool pError, string pMsg)
        {
            ErrorMsg model = new ErrorMsg(pError, pMsg);
            return View(model);
        }
    }
}