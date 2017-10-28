using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.BALTicketingSystem;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            List<TicketingSystem.Models.User> usersList = new BALUser().GetUsersList();
            return View(usersList);
        }

        [Authorize]
        // GET: User/Details/5
        public ActionResult Details(string pEmail)
        {
            User user = new BALUser().getUser(pEmail);
            return View(user);
        }

        // GET: User/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(User model)
        {
            try
            {
                if (ModelState.IsValid && new BALUser().Create(model))
                {
                    return RedirectToAction("CreateConfirmMsg", new { pError = false, pMsg = "El usuario " + model.name + " ha sido creado." });
                }
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intetar crear el usuario " + model.name + "." });
            }
            catch
            {
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intetar crear el usuario " + model.name + "." });
            }
        }

        // GET: User/Edit/5
        [Authorize]
        public ActionResult Edit(string pEmail)
        {
            User usr = new BALUser().getUser(pEmail);
            return View(usr);
        }

        // POST: User/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(User pUser)
        {
            try
            {
                if (new BALUser().Edit(pUser))
                {
                    return RedirectToAction("CreateConfirmMsg", new { pError = false, pMsg = "El usuario " + pUser.name + " ha sido modificado." });
                }
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intetar modificar el usuario " + pUser.name + "." });
            }
            catch
            {
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al modificar crear el usuario " + pUser.name + "." });
            }
        }

        // GET: User/Delete/5
        [Authorize]
        public ActionResult Delete(string pEmail)
        {
            try
            {
                if (new BALUser().Delete(pEmail))
                {
                    return RedirectToAction("CreateConfirmMsg", new { pError = false, pMsg = "Se elimino el usuario " + pEmail + "." });
                }
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intetar eliminar el usuario " + pEmail + "." });
            }
            catch
            {
                return View();
            }
        }

        // POST: User/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(string pEmail, FormCollection collection)
        {
            try
            {
                if (new BALUser().Delete(pEmail))
                {
                    return RedirectToAction("CreateConfirmMsg", new { pError = false, pMsg = "Se elimino el usuario " + pEmail + "." });
                }
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intetar eliminar el usuario " + pEmail + "." });
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult CreateConfirmMsg(bool pError, string pMsg)
        {
            ErrorMsg msg = new ErrorMsg(pError, pMsg);
            return View(msg);
        }
    }
}
