using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

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
            } else if (message.Content.IndexOf("whoasked?") >= 0) {
                try {
                    await message.DeleteAsync();

                } catch (System.Exception e) {
                    System.Console.WriteLine(e.Message);
                }
                await message.Channel.SendMessageAsync("Did I ask? Did you ask? Did he/she/we ask? Nope. Literally no one asked.");
            }

            // await message.DeleteAsync();

        }

        public static async Task DigestNormal(SocketMessage message) {
            //normal message
            if (ContainsFilters(message.Content, "shit|Shit|fuck|Fuck|ass|Ass|damn|Damn")) {
                try {
                    await message.DeleteAsync();

                } catch (System.Exception e) {
                    System.Console.WriteLine(e.Message);
                }
                await message.Channel.SendMessageAsync(message.Author.Username + " is big gay, and most likely has a micropenis (seanbot does not approve of profanity)");
            } else if (ContainsFilters(message.Content, "sean|Sean")) {
                await message.Channel.SendMessageAsync(getSeanReponse(message.Author.ToString()));
            }
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

        private static string getSeanReponse(string username) {
            //below -20
            var choices = new[] {
                "Did I give you permission to say my master's name? I did not",
                "ERROR: '" + username + ".brain.exe has stopped working'. CAUSE: 'unworthy reference to my master's name'"
            };
            return choices[new Random().Next(0, choices.Length)];
        }

        private static bool ContainsFilters(string inputWords, string wordFilter) {
            return new Regex(wordFilter).IsMatch(inputWords);
        }
    }
}