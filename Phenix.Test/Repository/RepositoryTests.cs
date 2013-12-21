using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Phenix.Core;
using Phenix.Core.Domain;
using Phenix.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phenix.Infrastructure.Data;

namespace Phenix.Data.Repository.Tests
{
    [TestClass()]
    public class RepositoryTests
    {
        private Repository<User> _userRepo;
        private Repository<UserValidation> _userValidationRepo;
        public RepositoryTests() {
            _userRepo = new Repository<User>();
            _userValidationRepo = new Repository<UserValidation>();
        }

        [TestMethod()]
        public void GetListTest() {
            int count = _userRepo.GetList("top 5", "", "").Count();
            Assert.AreEqual(count,5,"GetList通过"); 
        }

        [TestMethod()]
        public void AddTest()
        {
            var userValidation = new UserValidation()
            {
                UserId = 100401,Guid = Guid.NewGuid().ToString(),
                Type = (int)UserValidationEnum.FindPassWord,CreateOn = DateTime.Now
            };
            var userValidationId = _userValidationRepo.Add(userValidation);

            Assert.IsTrue(userValidationId>0);
        }

        [TestMethod()]
        public void DeleteByIdTest()
        {
            var userId = 100402;
           var isOk= _userRepo.DeleteById(userId);
            Assert.IsTrue(isOk,"删除成功");
            var user = _userRepo.GetById(userId);
            Assert.IsNull(user,"取出来的是空值");
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var userId = 100403;
            var user = _userRepo.GetById(userId);
            Assert.AreEqual(userId,user.Id,"取出来的值是"+userId);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var userId = 100403;
            var user = _userRepo.GetById(userId);
            user.Name = "向晚";
            var flag = _userRepo.Update(user);
            Assert.IsTrue(flag,"更新返回值");
            user = _userRepo.GetById(userId);
            Assert.AreEqual("向晚",user.Name,"更新的值和预期一样");
        }
        [TestMethod()]
        public void UpdateTest1()
        {
            var userVId = 1;
            var userV = _userValidationRepo.GetById(userVId);
            userV.UserId = 100403;
            var flag = _userValidationRepo.Update(userV);
            Assert.IsTrue(flag, "更新返回值");
            userV = _userValidationRepo.GetById(userVId);
            Assert.AreEqual(100403, userV.UserId, "更新的值和预期一样");
        }
        [TestMethod()]
        public void MapTest()
        {
            using (var conn=ConnectionFactory.CreateConnection())
            {
                var sql = "select u.*, uv.*from [user] u,[uservalidation] uv where u.id=uv.userid and uv.id=@id";
                var userValid = conn.Query<User, UserValidation,UserValidation>(sql, (u, uv) =>
                {
                    uv.User = u;
                    return uv;
                },new{id=1}).First();
                Assert.AreEqual(userValid.UserId,userValid.User.Id);
            }
        }

        [TestMethod()]
        public void BitToBooleanTest()
        {
            var userId = 100401;
            var user = _userRepo.GetById(userId);
            user.EMailIsValid = true;
            var isUpdateOk = _userRepo.Update(user);
            Assert.IsTrue(isUpdateOk,"更新成功");
            user = _userRepo.GetById(userId);
            Assert.IsTrue(user.EMailIsValid);
            user.EMailIsValid = false;
            isUpdateOk = _userRepo.Update(user);
            Assert.IsTrue(isUpdateOk, "更新成功");
        }

    }
}
