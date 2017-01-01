using SClub.VoteSystem.LogicDemo.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VoteSYstem.LogicDemo.SerVices
{
    public class VoteService : IDisposable
    {
        private VoteSystemWebDBEntities _db;
        private UserService _userServise = new UserService();
        public VoteService()
        {
            _db = new VoteSystemWebDBEntities();
        }
        //根据Id返回活动
        public VoteActivity GetActivityById(int activityId)
        {
            return _db.VoteActivity.Include("VoteProject").Where(act => act.Id == activityId).SingleOrDefault();
        }
        //返回活动列表
        public List<VoteActivity> GetActivities()
        {
            return _db.VoteActivity.ToList();
        }
        //添加活动
        public void AddActivities(string activityName, string activityIntro, DateTime activityST, DateTime activityET)
        {
            _db.VoteActivity.Add
            (
                new VoteActivity
                {
                    Name = activityName,
                    Intor = activityIntro,
                    BeginTime = activityST,
                    EndTime = activityET
                }
            );
            _db.SaveChanges();
        }
        //根据项目Id返回项目
        public VoteProject GetProjectById(int projectId)
        {
            return _db.VoteProject.Where(pro => pro.Id == projectId).SingleOrDefault();
        }
        //根据活动Id返回项目列表
        public List<VoteProject> GetProjectList(int activityId)
        {
            return _db.VoteProject.Where(project => project.ActivityId == activityId).ToList();
        }
        //添加项目
        public void AddProject(int activtyId, string projectName, string projectIntro)
        {
            _db.VoteProject.Add
            (
                new VoteProject
                {
                    ActivityId = activtyId,
                    Name = projectName,
                    Intro = projectIntro,
                    VoteCount = 0
                }
            );
            _db.SaveChanges();
        }
        //添加记录
        public void AddVoteRecord(int userId, int activityId, int projectId)
        {
            User user = _userServise.GetUser(userId);
            VoteActivity activity = GetActivityById(activityId);
            VoteProject project = GetProjectById(projectId);

            _db.VoteRecord.Add(
                new VoteRecord
                {
                    ActivityName = activity.Name,
                    ActivityId = activity.Id,
                    ProjectName = project.Name,
                    ProjectId = project.Id,
                    VoteTime = DateTime.Now,
                    UserName = user.Name,
                    UserId=user.Id
                }
            );
            project.VoteCount++;
            _db.SaveChanges();
        }
        //返回所有记录
        public List<VoteRecord> GetAllRecord()
        {
            return _db.VoteRecord.ToList();
        }
        //返回指定活动的记录
        public List<VoteRecord> GetVoteRecordByActivityId(int activityId)
        {
            return GetAllRecord().Where(record => record.ActivityId == activityId).ToList();
        }
        //返回指定项目的记录
        public List<VoteRecord> GetVoteRecordByProjectId(int projectID)
        {
            return GetAllRecord().Where(record => record.ProjectId == projectID).ToList();
        }
        //判断是否是第一次投该活动
        public bool IfVote(int userId, int activityID)
        {
            var checkRecordList = GetAllRecord()
                .Where(record => record.UserId == userId)
                .Where(record => record.ActivityId == activityID);
            return (checkRecordList.SingleOrDefault() == null);
        }
        //判断是否过期
        public bool IfExpired(int activityId)
        {
            VoteActivity activity = this.GetActivityById(activityId);
            DateTime now = DateTime.Now;
            return activity.BeginTime < now && now < activity.EndTime;
        }
        //投票
        public string Vote(int userId, int activityId, int projectId)
        {
            if (IfVote(userId, activityId))
            {
                if (IfExpired(activityId))
                {
                    AddVoteRecord(userId, activityId, projectId);
                    return "success";
                }
                return "timeError";
            }
            return "revote";
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
