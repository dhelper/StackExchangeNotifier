using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsPhoneNotifier.ViewModels;

namespace WindowsPhoneNotifier.Views
{
    public partial class UserDetailsView : PhoneApplicationPage
    {
        public UserDetailsView()
        {
            InitializeComponent();

            DataContext = IoCContainter.Get<UserDetailsViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string userId = string.Empty;
            if(NavigationContext.QueryString.TryGetValue("id", out userId))
            {
                ((UserDetailsViewModel)DataContext).UserId = int.Parse(userId);   
            }            
        }
    }
}