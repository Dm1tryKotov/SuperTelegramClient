using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using The_best_app_in_the_world.Domain;

namespace The_best_app_in_the_world.VM
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _firstName;
        private string _lastName;
        public UserViewModel(User user)
        {
            User = user;
            _userName = user.Username;
            _lastName = user.LastName;
            _firstName = user.FirstName;
        }
        public User User { get; }
        public string FirstName
        {
            get => _firstName;
            set { this.MutateVerbose(ref _firstName, value, RaisePropertyChanged()); }
        }
        public string LastName
        {
            get => _lastName;
            set { this.MutateVerbose(ref _lastName, value, RaisePropertyChanged()); }
        }
        public string UserName
        {
            get => _userName;
            set { this.MutateVerbose(ref _userName, value, RaisePropertyChanged()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
