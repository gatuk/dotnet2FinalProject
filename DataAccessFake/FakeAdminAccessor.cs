﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class FakeAdminAccessor : AdminAccessorInterface
    {
        private List<User> users;
        private List<string> roles;
        private List<Flight> flights;

        public FakeAdminAccessor()
        {
            this.users = new List<User>();
            this.roles = new List<string>();
        }

        public FakeAdminAccessor(List<User> users, List<string> roles)
        {
            this.users = users;
            this.roles = roles;
        }

        public int deleteUser(User? user)
        {
            int result = users.Count;
            users.Remove(user);
            return result - users.Count;
        }

        public int insertUser(User user)
        {
            int result = users.Count;
            users.Add(user);
            return users.Count - result;
        }

        public List<string> selectRoles()
        {
            return roles;
        }

        public List<User> selectUsers()
        {
           return users;
        }

        public int updateUser(User oldUser)
        {
            int result = 0;
            foreach (User user in users)
            {
                if (user.UserId == oldUser.UserId)
                {
                    user.UserName = oldUser.UserName;
                    user.Password = oldUser.Password;
                    user.Role = oldUser.Role;
                    result = 1;
                    break;
                }
            }
            return result;
        }
    }
}
