using ElecStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.ViewModel
{
    public class UserDAO
    {
        public static List<User> GetUsers()
        {
            var userList = new List<User>();
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    userList = context.Users.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return userList;
        }

        public static List<User> Search(string keyword)
        {
            var userList = new List<User>();
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    userList = context.Users
                        .Where(u => u.Email.Contains(keyword))
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return userList;
        }

        public static User FindUserById(int userId)
        {
            User user;
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    user = context.Users.SingleOrDefault(u => u.UserId == userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public static User FindUserByEmail(string email)
        {
            User user;
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    user = context.Users.FirstOrDefault(u => u.Email == email);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public static void SaveUser(User user)
        {
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateUser(User user)
        {
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteUser(User user)
        {
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    var userToDelete = context.Users.SingleOrDefault(u => u.UserId == user.UserId);
                    context.Users.Remove(userToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
