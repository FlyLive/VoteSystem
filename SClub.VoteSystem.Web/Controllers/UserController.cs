using SClub.VoteSystem.LogicDemo.DataBase;
using System.Collections.Generic;
using System.Web.Mvc;
using VoteSYstem.LogicDemo.SerVices;

namespace SClub.VoteSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private UserService _userServise = new UserService();
        //登录
        [HttpPost]
        public ActionResult Login(string id, string password)
        {
            if (id == null || password == null)
            {
                TempData["loginError"] = "inputError";
                return RedirectToAction("../Vote/HomePage");
            }
            else
            {
                User user = _userServise.Login(id,password);
                if (user!=null) {
                    Session.Add("User",user);
                    return RedirectToAction("../Vote/Activities");
                }
                TempData["loginError"] = "Error";
                return RedirectToAction("../Vote/HomePage");
            }
        }
        //注销
        [HttpGet]
        public ActionResult Logout()
        {
            Session["User"] = null;
            TempData["loginOutSueccess"]="loginOutSueccess";
            return RedirectToAction("../Vote/Activities");
        }
        //注册
        [HttpPost]
        public ActionResult Register(string name, string fPassword,string sPassword, string gender)
        {
            if (name==null||fPassword==null||sPassword==null||gender==null)
            {
                TempData["registerResult"] = "inputError";
            }
            else if (fPassword!=sPassword)
            {
                TempData["registerResult"] = "passwordError";
            }
            else
            {
                if (_userServise.AddUser(name, fPassword, gender))
                {
                    User newuser = _userServise.GetUsers()[_userServise.GetUsers().Count-1];
                    TempData["Id"] = newuser.Id;
                    TempData["registerResult"] ="success";
                }
                else
                {
                    TempData["registerResult"] = "reName";
                }
            }
            return RedirectToAction("../Vote/HomePage");
        }
    }
}