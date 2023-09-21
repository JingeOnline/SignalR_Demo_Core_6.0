using DataBaseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager
{
    public class UserRepository
    {
        private readonly List<UserModel> users = new List<UserModel>()
        {
            new UserModel("001","Mike","Admin"),
            new UserModel("002","Nancy", "Normal"),
            new UserModel("003","Tim", "Normal")
        };

        public List<UserModel> GetAllUsers()
        {
            return users;
        }

        public UserModel GetUserById(string id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public UserModel GetUserByName(string name)
        {
            return users.FirstOrDefault(x=>x.Name==name);
        }
    }
}
