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
    public class Program
    {
        private DiscordSocketClient _client;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;

            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");

            // build the configuration and assign to _config          
            var _config = _builder.Build();


            await _client.LoginAsync(TokenType.Bot, _config["Token"]);
            await _client.StartAsync();

            _client.MessageReceived += DigestMessage;

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }


        private async Task DigestMessage(SocketMessage mes)
        {
            // ensures we don't process system/other bot messages
            if (!(mes is SocketUserMessage message))
            {
                return;
            }

            if (mes.Source != MessageSource.User)
            {
                return;
            }

            if (mes.Author.Id == 796478634725867601)
            {
                await Commands.PatMessage(mes);
            }
            else if (mes.Content[0] == '+')
            {
                await Commands.DigestCommmand(mes);
            } else {
                await Commands.DigestNormal(mes);
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}