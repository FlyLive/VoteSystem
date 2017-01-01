using SClub.VoteSystem.LogicDemo.DataBase;
using SClub.VoteSystem.Web.Controllers;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoteSYstem.LogicDemo.SerVices;

namespace SClub.VoteSystem.Web.Controllers
{
    public class VoteController : Controller
    {
        private VoteService _voteServise = new VoteService();
        private UserService _userServise = new UserService();
        //主页
        [HttpGet]
        public ActionResult HomePage()
        {
            var model = _voteServise.GetActivities();
            return View(model);
        }
        //活动
        [HttpGet]
        public ActionResult Activities()
        {
            List<VoteActivity> model = _voteServise.GetActivities();
            return View(model);
        }
        //投票
        [HttpPost]
        public ActionResult Vote(int activityId,int projectId)
        {
            User user = (User)Session["User"];
            if (user == null)
            {
                TempData["voteResult"] = "notLogin";
            }
            else
            {
                TempData["voteResult"] =_voteServise.Vote(user.Id, activityId, projectId);
            }
            return RedirectToAction("Activities");
        }
        //个性化项目详情
        [HttpGet]
        public ActionResult Project1Details(int id)
        {
            VoteProject model = _voteServise.GetProjectById(id);
            return View(model);
        }
        [HttpGet]
        public ActionResult Project2Details(int id)
        {
            VoteProject model = _voteServise.GetProjectById(id);
            return View(model);
        }
        //通用项目详情
        [HttpGet]
        public ActionResult ProjectGeneralDetails(int id)
        {
            VoteProject model = _voteServise.GetProjectById(id);
            return View(model);
        }
        [HttpGet]
        //活动下面的项目排名
        public ActionResult VoteRankByActivityId(int id)
        {
            List<VoteProject> model = _voteServise.GetProjectList(id).OrderByDescending(project => project.VoteCount).ToList();
            return PartialView("VoteRankByActivityId", model);
        }
        [HttpGet]
        //该活动下的投票记录
        public ActionResult VoteRecordByActivityId(int id)
        {
            List<VoteRecord> model = _voteServise.GetVoteRecordByActivityId(id);
            return PartialView("VoteRecordByActivityId",model);
        }
    }
}