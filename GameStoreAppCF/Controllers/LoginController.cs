using GameStoreAppCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static GameStoreAppCF.Models.LoginVerifier;

namespace GameStoreAppCF.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string actionName)
        {
            TempData["Role"] = LoginVerifier.GetRoleByAction(actionName);
            TempData["Action"] = actionName;
            return PartialView();
        }

        public ActionResult Login(FormCollection formcollection)
        {
            Role role = (Role)TempData["Role"];
            var action = (string)TempData["Action"];
            if (LoginVerifier.VerifyAccess(role, formcollection["password"]))
            {
                return RedirectToAction("Index", action);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }
    }
}