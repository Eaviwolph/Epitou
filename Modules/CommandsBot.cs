using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace Epitou
{
    public class CommandsBot : ModuleBase<SocketCommandContext>
    {
        public static void ParseCommand(string command, SocketUserMessage message, DiscordSocketClient client)
        {
            string com = "";
            string arg = "";
            int i = 0;
            while (i < command.Length && command[i] != ' ')
            {
                com += command[i];
                i++;
            }
            i++;
            Console.WriteLine("{0} {1}: {2}", message.Timestamp.DateTime, message.Author.Username, command);
            while (i < command.Length)
            {
                arg += command[i];
                i++;
            }
            if (com == ".help")
            {
                Help(message);
            }
            else if (com == ".b0")
            {
                Bison0(arg, message);
            }
            else if (com == ".b1")
            {
                Bison1(arg, message);
            }
            else if (com == ".ha")
            {
                HaskellComp(arg, message);
            }
            else if (com == ".li")
            {
                LispComp(arg, message);
            }
        }
        public static void HaskellComp(string arg, SocketUserMessage message)
        {
            if (File.Exists("HaskellRep1"))
            {
                File.Delete("HaskellRep1");
            }
            if (File.Exists("HaskellRep2"))
            {
                File.Delete("HaskellRep2");
            }
            if (File.Exists("haskell.hs"))
            {
                File.Delete("haskell.hs");
            }
            if (File.Exists("haskellexe"))
            {
                File.Delete("haskellexe");
            }
            if (File.Exists("haskell.o"))
            {
                File.Delete("haskell.o");
            }
            if (File.Exists("haskell.hi"))
            {
                File.Delete("haskell.hi");
            }

            File.WriteAllText("haskell.hs", arg);
            PowerShell ps = PowerShell.Create();
            ps.AddScript(File.ReadAllText(@"haskell1.ps1")).Invoke();
            if (File.Exists("HaskellRep1"))
            {
                message.Channel.SendMessageAsync("```" + File.ReadAllText("HaskellRep1") + "```");
                System.Threading.Thread.Sleep(100);
                message.Channel.SendMessageAsync(File.ReadAllText("HaskellRep2"));
            }
            if (File.Exists("HaskellRep1"))
            {
                File.Delete("HaskellRep1");
            }
            if (File.Exists("HaskellRep2"))
            {
                File.Delete("HaskellRep2");
            }
            if (File.Exists("haskell.hs"))
            {
                File.Delete("haskell.hs");
            }
            if (File.Exists("haskellexe"))
            {
                File.Delete("haskellexe");
            }
            if (File.Exists("haskell.o"))
            {
                File.Delete("haskell.o");
            }
            if (File.Exists("haskell.hi"))
            {
                File.Delete("haskell.hi");
            }
        }

        public static void Bison0(string arg, SocketUserMessage message)
        {
            File.WriteAllText("bison.y", "%%\n");
            File.AppendAllText("bison.y", arg);
            File.AppendAllText("bison.y", "\n%%");
            PowerShell ps = PowerShell.Create();
            ps.AddScript(File.ReadAllText(@"bison.ps1")).Invoke();
            if (File.Exists("bison.dot"))
            {
                PowerShell ps2 = PowerShell.Create();
                ps2.AddScript(File.ReadAllText(@"bison2.ps1")).Invoke();
                message.Channel.SendFileAsync("bison.png");
                File.Delete("bison.c");
                File.Delete("bison.dot");
            }
            if (File.Exists("BizOut") && File.ReadAllText("BizOut").Length > 0)
            {
                message.Channel.SendMessageAsync("```" + File.ReadAllText("BizOut") + "```");
            }
            File.Delete("BizOut");
        }

        public static void Bison1(string arg, SocketUserMessage message)
        {
            File.WriteAllText("bison.y", "%define lr.type canonical-lr\n");
            File.AppendAllText("bison.y", "%%\n");
            File.AppendAllText("bison.y", arg);
            File.AppendAllText("bison.y", "\n%%");
            PowerShell ps = PowerShell.Create();
            ps.AddScript(File.ReadAllText(@"bison.ps1")).Invoke();
            if (File.Exists("bison.dot"))
            {
                PowerShell ps2 = PowerShell.Create();
                ps2.AddScript(File.ReadAllText(@"bison2.ps1")).Invoke();
                message.Channel.SendFileAsync("bison.png");
                File.Delete("bison.c");
                File.Delete("bison.dot");
            }
            else
            {
                message.Channel.SendMessageAsync("Syntax error");
            }
        }


        public static void Help(SocketUserMessage message)
        {
            string msg = @"```todo```";
            message.Channel.SendMessageAsync(msg);
        }

        public static void LispComp(string arg, SocketUserMessage message)
        {
            if (File.Exists("LispRep"))
            {
                File.Delete("LispRep");
            }
            if (File.Exists("LispErr"))
            {
                File.Delete("LispErr");
            }
            if (File.Exists("lisp.lisp"))
            {
                File.Delete("lisp.lisp");
            }
            File.WriteAllText("lisp.lisp", arg);
            PowerShell ps = PowerShell.Create();
            ps.AddScript(File.ReadAllText(@"lisp.ps1")).Invoke();
            if (File.Exists("LispRep") && File.ReadAllText("LispRep").Length > 0)
            {
                message.Channel.SendMessageAsync("```" + File.ReadAllText("LispRep") + "```");
            }
            else
            {
                message.Channel.SendMessageAsync("```" + File.ReadAllText("LispErr").Substring(0, 1000) + "...```");
            }
        }
    }
}
