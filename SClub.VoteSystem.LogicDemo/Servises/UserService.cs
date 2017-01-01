using System;
using System.Collections.Generic;
using System.Linq;
using SClub.VoteSystem.LogicDemo.DataBase;

namespace VoteSYstem.LogicDemo.SerVices
{
    public class UserService : IDisposable
    {
        private VoteSystemWebDBEntities _db;
        public UserService()
        {
            _db = new VoteSystemWebDBEntities();
        }
        //返回用户列表
        public List<User> GetUsers()
        {
            return _db.User.ToList();
        }
        //登录
        public User Login(string idOrName, string password)
        {
            int id;
            var ul = GetUsers()
                .Where(user => user.Name == idOrName)
                .Where(user => user.Password == password);
            //if (ul.SingleOrDefault() == null)
            //{
            //    try
            //    {
            //        id = Convert.ToInt32(idOrName);
            //        ul = GetUsers()
            //        .Where(user => user.Id == id)
            //        .Where(user => user.Password == password);
            //    }
            //    catch(FormatException)
            //    {
            //    }
            //}
            return ul.SingleOrDefault();
        }
        //根据Id返回user
        public User GetUser(int id)
        {
            var ul = _db.User
                .Where(user => user.Id == id);
            return ul.SingleOrDefault();
        }
        //是否重名
        public bool reName(string Name)
        {
            var ul = GetUsers()
                .Where(user => user.Name == Name).SingleOrDefault();
            if(ul!=null)
                return true;
            return false;
        }
        //注册用户
        public bool AddUser(string newName, string newPassword, string gender)
        {
            if (!reName(newName))
            {
                _db.User.Add
                (
                    new User
                    {
                        Name = newName,
                        Password = newPassword,
                        Gender = (gender == "男") ? true : false,
                    }
                );
                _db.SaveChanges();
                return true;
            }
            return false;
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
