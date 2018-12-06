﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess.Repositories
{
    public class MockRepository : IUserRepository
    {
        protected List<User> Users { get; set; }
        public MockRepository()
        {
            Users = new List<User>();
        }

        public void Insert(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Cannot add null user.");
            }
            //User searchUser = Users.Find(x => x.Email == user.Email);
            if (!UserExists(user.Email))
            {
                Users.Add(user);
            }
            else
            {
                throw new ArgumentException("User with this email already exists.");
            }
        }

        public Boolean AddUser(User user)
        {
            if (user != null)
            {
                User searchUser = Users.Find(x => x.Email == user.Email);
                if (!UserExists(user.Email))
                {
                    Users.Add(user);
                    return true;
                }
            }
            return false;
        }

        public bool UserExists(string email)
        {
            User searchUser = Users.Find(x => x.Email == email);
            if (searchUser == null) { return false; }
            return true;
        }

        public void Delete(User user)
        {
            if (UserExists(user.Email))
            {
                Users.Remove(user);
            }
            else
            {
                throw new ArgumentException("User does not exist.");
            }
        }

        public void Update(User user)
        {
            // make changes to entity
        }

        public User GetByEmail(string email)
        {
            return Users.Find(x => x.Email == email);
            // need to add what happens when null
        }

        public IEnumerable<User> GetAll()
        {
            return Users;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}