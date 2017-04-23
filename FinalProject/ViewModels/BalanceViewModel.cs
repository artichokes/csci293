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
    public class BalanceViewModel : Screen
    {
        private readonly AuthService authService;
        private readonly NavigationService navigationService;

        private decimal balance;

        public BalanceViewModel(NavigationService navigationService, AuthService authService)
        {
            this.navigationService = navigationService;
            this.authService = authService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateBalance();
        }

        public string Balance
        {
            get { return balance.ToString("C2"); }
        }

        private void UpdateBalance()
        {
            authService.User.UpdateBalance().Wait();
            balance = authService.User.Balance;
            NotifyOfPropertyChange(() => Balance);
        }

        public void GoToBalance()
        {
            navigationService.NavigateToViewModel(typeof(BalanceViewModel));
        }

        public void GoToWithdraw()
        {
            navigationService.NavigateToViewModel(typeof(WithdrawViewModel));
        }

        public void GoToTransactions()
        {
            navigationService.NavigateToViewModel(typeof(TransactionsViewModel));
        }

        public void GoToLogout()
        {
            navigationService.NavigateToViewModel(typeof(LogoutViewModel));
        }
    }
}
