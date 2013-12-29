using System.Windows.Navigation;
using Microsoft.Phone.Controls;
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

            string userId;
            if(NavigationContext.QueryString.TryGetValue("id", out userId))
            {
                ((UserDetailsViewModel)DataContext).UserId = int.Parse(userId);   
            }            
        }
    }
}