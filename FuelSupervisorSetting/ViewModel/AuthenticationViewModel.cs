﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Security;
using FuelSupervisorSetting.Model;
using FuelSupervisorSetting.Helpers;
using System.Data;
using System.Linq;
using FLS.Business;

namespace FuelSupervisorSetting.ViewModel
{
    public interface IViewModel { }

    public class AuthenticationViewModel : IViewModel, INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly RelayCommand _loginCommand;
        private readonly RelayCommand _logoutCommand;
        private readonly RelayCommand _showViewCommand;
        private string _username;
        private string _status;
        private Boolean _isvisible;
        private Boolean _ismenuvisibility;

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _loginCommand = new RelayCommand(Login, CanLogin);
            _logoutCommand = new RelayCommand(Logout, CanLogout);
            _showViewCommand = new RelayCommand(ShowView, null);
            _isvisible = true;
        }

        #region Properties

        public Boolean IsMenuVisibility
        {
            get { return _ismenuvisibility; }
            set { _ismenuvisibility = value; NotifyPropertyChanged("IsMenuVisibility"); }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged("Username"); }
        }

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Signed in as {0}. {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("Administrators") ? "You are an administrator!"
                              : "You are NOT a member of the administrators group.");

                return "Not authenticated!";
            }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged("Status"); }
        }
        #endregion

        #region Commands
        public RelayCommand LoginCommand { get { return _loginCommand; } }

        public RelayCommand LogoutCommand { get { return _logoutCommand; } }

        public RelayCommand ShowViewCommand { get { return _showViewCommand; } }
        #endregion

        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            try
            {

                
                   
                //Validate credentials through the authentication service                    
                User user = _authenticationService.AuthenticateUser(Username, clearTextPassword);
                

                //Get the current principal object
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity(user.Username, user.Email, user.Roles);

                bool permission = HasPermission(BusinessHelper.UserPermissions(_username));
                IsVisibility = !permission;
                IsMenuVisibility = permission;
                //Update UI
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                _loginCommand.RaiseCanExecuteChanged();
                _logoutCommand.RaiseCanExecuteChanged();
                Username = string.Empty; //reset
                passwordBox.Password = string.Empty; //reset
                Status = string.Empty;
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Login failed! Please provide some valid credentials.";
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);

                App.Log.  LogException(ex);
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        private void Logout(object parameter)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                _loginCommand.RaiseCanExecuteChanged();
                _logoutCommand.RaiseCanExecuteChanged();
                Status = string.Empty;
            }
        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }
        public Boolean IsVisibility
        {
            get { return _isvisible; }
            set { _isvisible = value; NotifyPropertyChanged("IsVisibility"); }
        }
        private bool HasPermission(DataTable permissions)
        {
            bool exists = permissions.AsEnumerable().Where(c => c.Field<string>("Permission").Equals("AddStationView")).Count() > 0;
            //bool has = permissions.AsEnumerable().Any(row => == row.Field<String>("Permission") == "AddStationView");
            return exists;
        }
        private void ShowView(object parameter)
        {
            //try
            //{
            //    Status = string.Empty;
            //    IView view;
            //    if (parameter == null)
            //        view = new SecretWindow();
            //    else
            //        view = new AdminWindow();

            //    view.Show();
            //}
            //catch (SecurityException)
            //{
            //    Status = "You are not authorized!";
            //}
        }


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}