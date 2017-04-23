using Caliburn.Micro;
using FinalProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class TransactionsViewModel : Screen
    {
        private readonly AuthService authService;
        private readonly NavigationService navigationService;

        private ObservableCollection<TransactionModelView> transactions;

        public TransactionsViewModel(NavigationService navigationService, AuthService authService)
        {
            this.navigationService = navigationService;
            this.authService = authService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateTransactions();
        }

        public ObservableCollection<TransactionModelView> Transactions
        {
            get { return transactions; }
            set
            {
                transactions = value;
                NotifyOfPropertyChange(() => Transactions);
            }
        }

        private void UpdateTransactions()
        {
            authService.User.UpdateTransactions().Wait();
            authService.User.Transactions.Reverse();
            transactions = new ObservableCollection<TransactionModelView>();
            foreach (var transaction in authService.User.Transactions)
            {
                transactions.Add(new TransactionModelView
                {
                    Date = transaction.Inserted.ToLocalTime().ToString(),
                    Type = transaction.Type == Models.TransactionType.Withdrawal ? "Withdrawal" : "Deposit",
                    Amount = transaction.Amount.ToString("C2")
                });
            }
            NotifyOfPropertyChange(() => Transactions);
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

    public class TransactionModelView : PropertyChangedBase
    {
        private string date, type, amount;

        public String Date
        {
            get { return date; }
            set
            {
                date = value;
                NotifyOfPropertyChange(() => Date);
            }
        }

        public String Type
        {
            get { return type; }
            set
            {
                type = value;
                NotifyOfPropertyChange(() => Type);
            }
        }

        public String Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                NotifyOfPropertyChange(() => Amount);
            }
        }
    }
}
