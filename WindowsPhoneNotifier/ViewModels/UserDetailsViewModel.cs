using StackExchangeClient;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WindowsPhoneNotifier.Commands;

namespace WindowsPhoneNotifier.ViewModels
{
    public enum Trend
    {
        Unchanged,
        Increase,
        Decrease
    }

    public class UserDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IUserRepository _factory;
        private int _userId;

        public event PropertyChangedEventHandler PropertyChanged;
        private Uri _userImageUri;
        private string _userName;
        private int _userReputation;
        private Brush _reputationTrend;
        private bool _wasInitialized;

        public UserDetailsViewModel(IUserRepository userRepository)
        {
            _reputationTrend = new SolidColorBrush(Colors.White);
            _factory = userRepository;
            RefershUserDetails = new DelegateCommand(() => LoadUser());
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand RefershUserDetails { get; private set; }

        public int UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                _userId = value;

                NotifyPropertyChanged();
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;

                NotifyPropertyChanged();
            }
        }

        public Uri UserImageUri
        {
            get
            {
                return _userImageUri;
            }

            set
            {
                _userImageUri = value;

                NotifyPropertyChanged();
            }
        }

        public int UserReputation
        {
            get
            {
                return _userReputation;
            }

            set
            {
                _userReputation = value;

                NotifyPropertyChanged();
            }
        }

        public Brush ReputationTrend
        {
            get
            {
                return _reputationTrend;
            }

            set
            {
                _reputationTrend = value;

                NotifyPropertyChanged();
            }
        }

        public async Task LoadUser()
        {
            var currentUser = await _factory.GetUser(UserId);

            UserName = currentUser.DisplayName;
            if (!String.IsNullOrEmpty(currentUser.ImageUrl))
            {
                UserImageUri = new Uri(currentUser.ImageUrl);
            }

            if (_wasInitialized)
            {
                if (UserReputation > currentUser.Reputation)
                {
                    ReputationTrend = new SolidColorBrush(Colors.Red);
                }
                else if (UserReputation < currentUser.Reputation)
                {
                    ReputationTrend = new SolidColorBrush(Colors.Green);
                }
            }

            _wasInitialized = true;

            UserReputation = currentUser.Reputation;
        }
    }
}
