using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telegram.Bot.Types;
using The_best_app_in_the_world.Domain;

namespace The_best_app_in_the_world.VM
{
    public class MessageViewModel : INotifyPropertyChanged
    {

        private const string PaddingForBot = "0 0 100 0";
        private const string PaddingForUser = "100 0 0 0";
        private const string HorizontalAlignmentForBot = "Right";
        private const string HorizontalAlignmentForUser = "Left";

        private string _currentPadding;
        private string _currentHorizontalAlignment;
        private string _messageText;
        private string _author;
        public MessageViewModel(Telegram.Bot.Types.Message m) {
            MessageText = m.Text;
            CurrentPadding = m.From.IsBot ? PaddingForBot : PaddingForUser;
            CurrentHorizontalAlignment = m.From.IsBot ? HorizontalAlignmentForBot : HorizontalAlignmentForUser;
            Author = m.From.IsBot ? "me" : m.From.FirstName;
        }

        public string CurrentPadding
        {
            get => _currentPadding;
            set { this.MutateVerbose(ref _currentPadding, value, RaisePropertyChanged()); }
        }
        public string CurrentHorizontalAlignment
        {
            get => _currentHorizontalAlignment;
            set { this.MutateVerbose(ref _currentHorizontalAlignment, value, RaisePropertyChanged()); }
        }
        public string Author
        {
            get => _author;
            set { this.MutateVerbose(ref _author, value, RaisePropertyChanged()); }
        }
        public string MessageText
        {
            get => _messageText;
            set { this.MutateVerbose(ref _messageText, value, RaisePropertyChanged()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
