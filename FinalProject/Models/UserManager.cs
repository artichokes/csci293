using FinalProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    class UserManager
    {
        public static async Task<User> FindOrCreateAsync(uint id)
        {
            using (var db = new UserContext())
            {
                User user = db.Users
                    .Where(u => u.UserId == id)
                    .FirstOrDefault();

                if (user == null)
                {
                    user = new User
                    {
                        UserId = id,
                        Pin = 1234,
                        Balance = 5000.00M
                    };
                    await db.Users.AddAsync(user);
                }

                await db.SaveChangesAsync();
                return user;
            }
        }

        public static async Task<Result<User>> AuthenticateUser(uint id, uint pin)
        {
            using (var db = new UserContext())
            {
                var user = await FindOrCreateAsync(id);
                if (user.Pin == pin)
                {
                    return new Result<User>(user, true, null);
                }
                else
                {
                    return new Result<User>(null, false, "Invalid Pin");
                }
            }
        }
    }
}
