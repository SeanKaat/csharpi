using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace csharpi {
    public class Commands {
        private static int patLevel = 0;


        public static async Task DigestCommmand(SocketMessage message) {
            if (message.Content.IndexOf("patdown") != -1) {
                patLevel--;
                await message.Channel.SendMessageAsync("Pat is now at " + patLevel);
            } else if (message.Content.IndexOf("patup") != -1) {
                patLevel++;
                await message.Channel.SendMessageAsync("Pat is now at " + patLevel);
            } else if (message.Content.IndexOf("shame") != -1 && message.MentionedUsers.Count > 0) {
                string s = (message.MentionedUsers.Count > 1 ? "All y'all been shamed now" : "you have been shamed");
                await message.Channel.SendMessageAsync(s);
            }

            // await message.DeleteAsync();

        }

        public static async Task DigestNormal(SocketMessage message) {
            //normal message
            
        }


        public static async Task PatMessage(SocketMessage message) {
            await message.Channel.SendMessageAsync(getPatResponse());
            await message.AddReactionAsync(new Emoji("\u267F"));
            await message.AddReactionAsync(new Emoji("\uD83C\uDF08")); 
        }

        private static string getPatResponse() {
            //below -20
            var choices = new[] { "g a r b a g e" };
            if (patLevel > 20)
                choices = new[] { "nice." };
            else if (patLevel > 0)
                choices = new[] { "who? who asked?" };
            else if (patLevel > -20) {
                choices = new[] { "All I see is: https://www.bing.com/images/search?view=detailV2&ccid=p0pFDfEJ&id=5A2EA005CD51790E4114F8A1651DC5E8BF2FE6B6&thid=OIP.p0pFDfEJD24xoOHL2np7VwHaE8&mediaurl=https%3a%2f%2fwww.allencountyhealth.com%2fwp-content%2fuploads%2f2014%2f10%2fGarbage.jpg&exph=1415&expw=2122&q=garbage&simid=607987337491712459&ck=3714083E75BB034217C372453E7DC4A7&selectedIndex=0&FORM=IRPRST&ajaxhist=0" };
            }

            return choices[new Random().Next(0, choices.Length)];
        }
    }
}