using Caliburn.Micro;
using FinalProject.Models;
using FinalProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class LogoutViewModel : Screen
    {
        private readonly AuthService authService;
        private readonly NavigationService navigationService;

        public LogoutViewModel(NavigationService navigationService, AuthService authService)
        {
            this.navigationService = navigationService;
            this.authService = authService;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            authService.SignOut();
            navigationService.NavigateToViewModel(typeof(LoginViewModel));
        }
    }
}
