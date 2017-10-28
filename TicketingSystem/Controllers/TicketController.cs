using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        [Authorize]
        public ActionResult Index()
        {
            User usuario = (User)Session["User"];
            List<Models.Ticket> tickets = new BALTicketingSystem.BALTicket().GetAssigneeTickets(usuario);
            return View(tickets);
        }

        [Authorize]
        public ActionResult IndexAll()
        {
            User usuario = (User)Session["User"];
            List<Models.Ticket> tickets = new BALTicketingSystem.BALTicket().GetAllAssigneeTickets(usuario);
            return View("Index", tickets);
        }

        [Authorize]
        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            User usuario = (User)Session["User"];
            Ticket ticket = new BALTicketingSystem.BALTicket().GetTicketDetails(usuario, id);
            return View(ticket);
        }

        [Authorize]
        // GET: Ticket/Create
        public ActionResult Create()
        {
            User usuario = (User)Session["User"];
            ViewBag.usersForDropList = new BALTicketingSystem.BALUser().GetUsersForDropList(usuario);
            return View();
        }

        // POST: Ticket/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(string title, string body, string assignee)
        {
            try
            {
                User autor = (User)Session["User"];
                User asignado = new BALTicketingSystem.BALUser().getUser(assignee);
                if (autor != null && asignado != null)
                {
                    Ticket ticket = new Ticket(-1, title, body, "open", autor, asignado, DateTime.Now);
                    bool cont = new BALTicketingSystem.BALTicket().Create(ticket);
                    if (cont)
                    {
                        return RedirectToAction("CreateConfirmMsg", new { pError = false, pMsg = "El ticket " + title + " ha sido creado." });
                    }
                }
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intentar crear el ticket " + title + "." });
            }
            catch
            {
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intentar crear el ticket " + title + "." });
            }
        }

        // GET: Ticket/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            User usuario = (User)Session["User"];
            Ticket ticket = new BALTicketingSystem.BALTicket().GetTicketDetails(usuario, id);
            ViewBag.statusForDropList = new BALTicketingSystem.BALUtils().GetListOfStatus();
            ViewBag.usersForDropList = new BALTicketingSystem.BALUser().GetUsersList();
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, string body, string status, string assignee)
        {
            try
            {
                User usuario = (User)Session["User"];
                bool cont = new BALTicketingSystem.BALTicket().Edit(usuario, id, body, status, assignee);
                if (cont)
                {
                    return RedirectToAction("CreateConfirmMsg", new { pError = false, pMsg = "El ticket " + id + " fue actualizado." });
                }
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intentar acutalizar el ticket " + id + "." });
            }
            catch
            {
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intentar acutalizar el ticket " + id + "." });
            }
        }

        // GET: Ticket/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            User usuario = (User)Session["User"];
            Ticket ticket = new BALTicketingSystem.BALTicket().GetTicketDetails(usuario, id);
            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                User usuario = (User)Session["User"];
                bool eliminado = new BALTicketingSystem.BALTicket().DeleteTicket(usuario, id);
                if (eliminado)
                {
                    return RedirectToAction("CreateConfirmMsg", new { pError = false, pMsg = "Se cerro el ticket " + id + "." });
                }
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intentar cerrar el ticket " + id + "." });
            }
            catch
            {
                return RedirectToAction("CreateConfirmMsg", new { pError = true, pMsg = "Error al intentar cerrar el ticket " + id + "." });
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
