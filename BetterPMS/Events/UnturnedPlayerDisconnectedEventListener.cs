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
using OpenMod.API.Eventing;
using OpenMod.API.Plugins;
using OpenMod.API.Users;
using OpenMod.Core.Commands;
using OpenMod.Core.Users.Events;
using OpenMod.Extensions.Games.Abstractions.Players;
using OpenMod.Unturned.Players.Connections.Events;
using OpenMod.Unturned.Plugins;
using OpenMod.Unturned.Users;
using SDG.Framework.Devkit;
using SDG.Unturned;
using Steamworks;
using YamlDotNet.Core.Tokens;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

namespace BetterPMS.Events
{
    public class UnturnedPlayerDisconnectedEventListener : IEventListener<UnturnedPlayerDisconnectedEvent>
    {
        private readonly ILogger<UnturnedPlayerDisconnectedEventListener> m_Logger;
        public UnturnedPlayerDisconnectedEventListener(ILogger<UnturnedPlayerDisconnectedEventListener> logger)
        {
            m_Logger = logger;
        }

        public Task HandleEventAsync(object sender, UnturnedPlayerDisconnectedEvent @event)
        {
            CSteamID id = @event.Player.SteamId;
            if (Storage.Instance.SteamID.ContainsKey(id))
            {
                Storage.Instance.SteamID.Remove(id);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
