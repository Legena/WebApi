using DataLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly OfferContext db = new OfferContext();

        [HttpPost]
        [ActionName("register")]
        public void Register([FromBody]User user)
        {
            using (db)
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }

        [HttpPost]
        [ActionName("login")]
        public string Login([FromBody] User user)
        {
            using (db)
            {
                var loggedUser = db.Users.FirstOrDefault(u => u.Email == user.Email);

                if (loggedUser.AuthCode == user.AuthCode)
                {
                    loggedUser.AccessToken = Guid.NewGuid().ToString();

                    db.SaveChanges();

                    return loggedUser.AccessToken;
                }
            }

            return null;
        }

        [HttpGet]
        [ActionName("logout")]
        public void Logout(string accessToken)
        {
            using (db)
            {
                var loggedUser = db.Users.FirstOrDefault(u => u.AccessToken == accessToken);
                loggedUser.AccessToken = null;

                db.SaveChanges();
            }
        }
    }
}
