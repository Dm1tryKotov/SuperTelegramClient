using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using Telegram.Bot.Types;
using The_best_app_in_the_world.Args;
using The_best_app_in_the_world.Domain;
using The_best_app_in_the_world.Models;

namespace The_best_app_in_the_world.VM
{
    public class ChatWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MessageViewModel> Messages => _messages;
        private ObservableCollection<MessageViewModel> _messages;

        private Dispatcher _dispatcher;
        private TelegramBotModel _bot;
        private long _chatId;

        public ChatWindowViewModel(List<Message> messages, TelegramBotModel bot)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _messages = new ObservableCollection<MessageViewModel>();
            _chatId = messages[0].Chat.Id;
            _bot = bot;
            foreach (var item in messages) {
                _messages.Add(new MessageViewModel(item));
            }
        }

        private string _newMessage;
        public string NewMessage
        {
            get => _newMessage;
            set { this.MutateVerbose(ref _newMessage, value, RaisePropertyChanged()); }
        }

        public void ChatWasUpdated_Event(object sender, EventArgs e)
        {
            _dispatcher.Invoke(() => UpdateChat((e as ChatArgs).messages));
        }

        private void UpdateChat(List<Message> newMessages) {
            if (newMessages[0].Chat.Id != _chatId) return;
            var checkpointIndex = Messages.Count;

            for (int i = checkpointIndex; i < newMessages.Count; i++) {
                Messages.Add(new MessageViewModel(newMessages[i]));
            }
        }

        public ICommand SendMessageCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _bot.BotSendMessage(_chatId, NewMessage);
                    NewMessage = "";
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
