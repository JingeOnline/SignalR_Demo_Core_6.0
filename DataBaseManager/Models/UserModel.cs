﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public UserModel(string id, string name, string role)
        {
            this.Id = id;
            this.Name = name;
            this.Role = role;
        }
    }
}
