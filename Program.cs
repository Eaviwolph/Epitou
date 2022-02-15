using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Epitou
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().RunBotAsync().GetAwaiter().GetResult();
        }
        public DiscordSocketClient client;
        public CommandService commands;
        public IServiceProvider servises;
        public string prefix = ".";
        public async Task RunBotAsync()
        {
            this.client = new DiscordSocketClient();
            this.commands = new CommandService();
            this.servises = new ServiceCollection().AddSingleton(client).AddSingleton(commands).BuildServiceProvider();

            string token = File.ReadAllText("token");
            await client.SetGameAsync("Etre pipou");

            client.Log += Client_Log;
            await RegisterCommandsAsync();
            await client.LoginAsync(TokenType.Bot, token);

            await client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), servises);
        }
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(client, message);
            if (message.Author.IsBot)
            {
                return;
            }

            int argPos = 0;
            if (message.HasStringPrefix(prefix, ref argPos))
            {
                // if(message.Author.Mention != "<@!270963644618113024>")
                {
                    CommandsBot.ParseCommand(message.Content, message, client);
                }
            }
        }
    }
}
