using System;
using System.Drawing;
using System.Threading.Tasks;
using BetterPMS.LastMessageStorage;
using Cysharp.Threading.Tasks;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Users;
using SDG.Unturned;
using Steamworks;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

namespace BetterPMS.Commands
{
    [Command("replymessage")]
    [CommandAlias("r")]
    [CommandAlias("reply")]
    [CommandAlias("rm")]
    [CommandSyntax("<message>")]
    public class CommandReplyMessage : OpenMod.Core.Commands.Command
    {
        private readonly IUnturnedUserDirectory m_userManager;
        public CommandReplyMessage(IServiceProvider serviceProvider, IUnturnedUserDirectory userManager) : base(serviceProvider)
        {
            m_userManager = userManager;
        }

        protected override async Task OnExecuteAsync()
        {
            UnturnedUser you = (UnturnedUser)Context.Actor;
            await Storage.Instance.GetLastMessengerAsync(((UnturnedUser)Context.Actor).SteamId, out CSteamID messenger);
            if(messenger == CSteamID.Nil)
            {
                await Context.Actor.PrintMessageAsync("No one has messaged you yet", Color.Orange);
                await UniTask.CompletedTask;
                return;
            }
            UnturnedUser _replyUser = m_userManager.FindUser(messenger);
            if (_replyUser == null)
            {
                await Context.Actor.PrintMessageAsync("The user is no longer online", Color.Red);
                await UniTask.CompletedTask;
                return;
            }
            string message = string.Join(" ", Context.Parameters);
            you?.PrintMessageAsync($"[PM to {_replyUser.DisplayName}]: {message}", Color.White);
            _replyUser?.PrintMessageAsync($"[PM] {Context.Actor.DisplayName}: {message}", Color.White);

            await UniTask.CompletedTask;
        }
    }
}
