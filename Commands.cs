using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace csharpi
{
    public class Commands
    {
        private static int patLevel = 0;


        public static async Task DigestCommmand(SocketMessage message)
        {
            if (message.Content.IndexOf("patdown") != -1)
            {
                patLevel--;
                await message.Channel.SendMessageAsync("Pat is now at " + patLevel);
            }
            else if (message.Content.IndexOf("patup") != -1)
            {
                patLevel++;
                await message.Channel.SendMessageAsync("Pat is now at " + patLevel);
            }
            else if (message.Content.IndexOf("shame") != -1 && message.MentionedUsers.Count > 0)
            {
                string s = (message.MentionedUsers.Count > 1 ? "All y'all been shamed now" : "you have been shamed");
                await message.Channel.SendMessageAsync(s);
            }

            // await message.DeleteAsync();

        }

        public static async Task DigestNormal(SocketMessage message) {
            //normal message

            
            return;
        }


        public static async Task PatMessage(SocketMessage message)
        {
            await message.Channel.SendMessageAsync(getPatResponse());
        }

        private static string getPatResponse()
        {
            //below -20
            var choices = new[] { "You are literally the worst person to have ever existed" };
            if (patLevel > 20)
                choices = new[] { "nice" };
            else if (patLevel > 0)
                choices = new[] { "who? who asked?" };
            else if (patLevel > -20)
            {
                choices = new[] { "garbage" };
            }

            return choices[new Random().Next(0, choices.Length)];
        }
    }
}