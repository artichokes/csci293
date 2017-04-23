using Caliburn.Micro;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class AuthService : PropertyChangedBase
    {
        private bool authenticated = false;
        private User user;

        public bool Authenticated
        {
            get { return authenticated; }
            private set
            {
                authenticated = value;
                NotifyOfPropertyChange(() => Authenticated);
            }
        }

        public User User
        {
            get { return user; }
            private set
            {
                user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        public void SignIn(User user)
        {
            User = user;
            Authenticated = true;
        }

        public void SignOut()
        {
            User = null;
            Authenticated = false;
        }
    }
}
