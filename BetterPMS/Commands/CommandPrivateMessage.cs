using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using BetterPMS.LastMessageStorage;
using Cysharp.Threading.Tasks;
using HarmonyLib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.Core.Commands;
using OpenMod.Extensions.Games.Abstractions.Players;
using OpenMod.Unturned.Plugins;
using OpenMod.Unturned.Users;
using SDG.Framework.Devkit;
using SDG.Unturned;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

namespace BetterPMS.Commands
{
    [Command("privatemessage")]
    [CommandAlias("pm")]
    [CommandAlias("dm")]
    [CommandAlias("m")]
    [CommandAlias("msg")]
    [CommandAlias("tell")]
    [CommandSyntax("<player> <message>")]
    public class CommandPrivateMessage : OpenMod.Core.Commands.Command
    {
        public CommandPrivateMessage(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        protected override async Task OnExecuteAsync()
        {
            await UniTask.SwitchToThreadPool();
            UnturnedUser you = (UnturnedUser)Context.Actor;
            IPlayerUser messenged = await Context.Parameters.GetAsync<IPlayerUser>(0);
            string message = string.Join(" ", Context.Parameters.Skip(1));
            messenged?.PrintMessageAsync($"[PM] {Context.Actor.DisplayName}: {message}", Color.White);
            you?.PrintMessageAsync($"[PM to {messenged.DisplayName}]: {message}", Color.White);
            await Storage.Instance.setLastMessenger(you.SteamId, ((UnturnedUser)messenged).SteamId);
            await UniTask.CompletedTask;
        }
    }
}
