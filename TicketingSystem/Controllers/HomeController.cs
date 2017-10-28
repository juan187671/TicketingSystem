using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web.Mvc;
using TicketingSystem.Models;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;

namespace TicketingSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(Session["User"] != null) { RedirectToAction("Index", "Ticket"); }
            return View(new TicketingSystem.Models.ErrorMsg(false, ""));
        }

        public ActionResult Login()
        {
            string email = Request["txtEmail"];
            string password = Request["txtPassword"];
            User user = (User)new TicketingSystem.BALTicketingSystem.BALUser().UserLogin(email, password);
            if (user != null)
            {
                var ident = new ClaimsIdentity(
                    new[] {
                        // En esta area se puede agregar los roles del usuario.
                        new Claim(ClaimTypes.NameIdentifier, user.email),
                        new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                        new Claim(ClaimTypes.Name,user.email),
                    }, DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);

                Session["User"] = user;
                Session["Name"] = user.name;
                if (user.enable)
                {
                    return RedirectToAction("Index", "Ticket");
                }
                return View("Index", new ErrorMsg(true, "El usuario esta deshabilitado contacte al administrador."));
            }
            return View("Index", new ErrorMsg(true, "Usuario y/o contraseña no validos."));
        }

        public ActionResult Logout()
        {
            this.LimparSession();
            return RedirectToAction("Index", "Home");
        }

        private void LimparSession()
        {
            Session.Clear();
            Session.Abandon();
            Request.GetOwinContext().Authentication.SignOut();
        }

    }
}