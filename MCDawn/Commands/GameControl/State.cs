using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCDawn.Commands.GameControl
{
    class Stat
    {
        private bool check;
        public string[] message;

        public Stat(bool aCheck, string[] aMessage)
        {
            check = aCheck;
            message = aMessage;
        }

        public Stat()
        {
            check = false;
            message = new string[0];
        }

        public bool GetComplete()
        {
            return check;
        }
        public void SetCompletingStatus(bool b)
        {
            check = b;
        }

        public void SendToLevel(Level l)
        {
            for (int i = 0; i < message.Length; i++)
            {
                l.ChatLevel(message[i]);
            }
        }
        public void SendToGameOPs(Game g)
        {
            for (int i = 0; i < message.Length; i++)
            {
                foreach (Player p in g.GameOPs)
                {
                    p.SendMessage(message[i]);
                }
            }
        }
        public void SendToPlayer(Player p)
        {
            for (int i = 0; i < message.Length; i++)
            {
                p.SendMessage(message[i]);
            }
        }

        public static void SendToLevel(Level l, string[] message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                l.ChatLevel(message[i]);
            }
        }
        public static void SendToGameOPs(Game g, string[] message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                foreach (Player p in g.GameOPs)
                {
                    p.SendMessage(message[i]);
                }
            }
        }
        public static void SendToPlayer(Player p, string[] message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                p.SendMessage(message[i]);
            }
        }
    }
}
