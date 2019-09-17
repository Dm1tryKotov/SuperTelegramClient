using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using The_best_app_in_the_world.Args;

namespace The_best_app_in_the_world.Models
{
    public class TelegramBotModel
    {
        private readonly TelegramBotClient _bot;
        private Dictionary<long, List<Message>> _allChats;

        public EventHandler FindNewChat;
        public EventHandler ChatWasUpdated;

        public TelegramBotModel(string token) {
            _allChats = new Dictionary<long, List<Message>>();

            _bot = new TelegramBotClient(token);
            _bot.OnMessage += BotOnMessageReceived;
            _bot.OnMessageEdited += BotOnMessageReceived;

            _bot.StartReceiving(Array.Empty<UpdateType>());
        }

        public async void BotSendMessage(long chatId, string msg) {
            if (msg == null) return;
            var correctMessage = @"" + msg;
            Message myMessage = await _bot.SendTextMessageAsync(
                chatId,
                correctMessage,
                replyMarkup: new ReplyKeyboardRemove());
            _allChats[chatId].Add(myMessage);
            ChatWasUpdated(this, new ChatArgs() { messages = _allChats[chatId] });
        }

        private void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            var chatId = message.Chat.Id;

            if (message == null || message.Type != MessageType.Text) return;

            if (!_allChats.ContainsKey(chatId))
            {
                _allChats.Add(chatId, new List<Message> { message });
                FindNewChat(this, new ChatArgs() { messages = _allChats[message.Chat.Id] } );
            }
            else
            {
                _allChats[message.Chat.Id].Add(message);
                ChatWasUpdated(this, new ChatArgs() { messages = _allChats[chatId] });
            }
        }
    }
}
