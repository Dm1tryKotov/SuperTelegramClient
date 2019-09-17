using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using Telegram.Bot.Types;
using The_best_app_in_the_world.Args;
using The_best_app_in_the_world.Models;
using The_best_app_in_the_world.View;

namespace The_best_app_in_the_world.VM
{
    public class MainViewModel
    {
        public ObservableCollection<UserViewModel> MyUsersViewModels => _myUsersViewModel;
        private ObservableCollection<UserViewModel> _myUsersViewModel;
        private Dictionary<User, ChatWindowViewModel> _myChatViewModels;

        private Dispatcher _dispatcher;
        private TelegramBotModel _bot;

        public MainViewModel() {
            _dispatcher = Dispatcher.CurrentDispatcher;

            _myChatViewModels = new Dictionary<User, ChatWindowViewModel>();
            _myUsersViewModel = new ObservableCollection<UserViewModel>();

            _bot = new TelegramBotModel("802115021:AAHfV-M4cK7FcytGZ4sz7GDYlM8DjkcNxWE");
            _bot.FindNewChat += FindNewChat_Event;
        }

        private void FindNewChat_Event(object sender, EventArgs e)
        {
            _dispatcher.Invoke(new Action(() =>
            {
                var messages = (e as ChatArgs).messages;
                var newUser = messages[0].From;

                if (!_myChatViewModels.ContainsKey(newUser)) {
                    var chatVm = new ChatWindowViewModel((e as ChatArgs).messages, _bot);
                    var usrVm = new UserViewModel(newUser);

                    _bot.ChatWasUpdated += chatVm.ChatWasUpdated_Event;
                    _myChatViewModels.Add(newUser, chatVm);
                    _myUsersViewModel.Add(usrVm);
                    OpenChat(usrVm);
                }
            }));
        }

        public void OpenChat(UserViewModel user) {
            var win = new ChatWindow() { DataContext = _myChatViewModels[user.User] };
            win.Show();
        }

        public void SendMessage(long chatId, string message) {
            _bot.BotSendMessage(chatId, message);
        }
    }
}
