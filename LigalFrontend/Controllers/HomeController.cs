using LigalFrontend.DAL;
using LigalFrontend.ViewModels;
using System.Web.Mvc;
using System.Web.Security;

namespace LigalFrontend.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioLoginVM u)
        {
            // Lets first check if the Model is valid or not
            if (ModelState.IsValid)
            {
                using (UsuariosRepo repo = new UsuariosRepo())
                {
                    UsuariosVM usLogado = repo.validation(u);

                    if (usLogado != null)
                    {
                        string username = usLogado.usuario.LOGIN;

                        FormsAuthentication.SetAuthCookie(usLogado.usuario.LOGIN.ToString(), false);

                        Session["LogedUserID"] = usLogado.usuario.ID.ToString();
                        Session["Role"] = usLogado.usuario.USERTYPE.ToString();
                        Session["LogedUserFullname"] = usLogado.usuario.NOMBRE.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.erroInicio = "Usuario ou contrasinal incorrecto.";
                        ModelState.AddModelError("", "Usuario ou contrasinal incorrecto.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form               
            log.Error("Fallo en la validación Login");
            return View();
        }

        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOff(FormCollection form)
        {
            string decision = form["botonSalir"];

            if (!string.IsNullOrEmpty(decision))
            {
                Session.Clear();
                System.Web.Security.FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}