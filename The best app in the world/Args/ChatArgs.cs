using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace The_best_app_in_the_world.Args
{
    public class ChatArgs : EventArgs
    {
        public List<Message> messages;
    }
}
