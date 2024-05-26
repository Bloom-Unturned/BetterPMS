using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

namespace BetterPMS.LastMessageStorage
{
    public class Storage
    {
        private static Storage _instance;
        private Storage()
        {
            SteamID = new Dictionary<CSteamID, CSteamID>();
        }
        public Dictionary<CSteamID, CSteamID> SteamID { get; set; }
        public static Storage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Storage();
                }
                return _instance;
            }
        }

        public Task setLastMessenger(CSteamID messenger, CSteamID messenged)
        {
            if (Storage.Instance.SteamID.ContainsKey(messenged))
            {
                Storage.Instance.SteamID[messenged] = messenger;
                return Task.CompletedTask;
            }
            else
            {
                Storage.Instance.SteamID.Add(messenged, messenger);
                return Task.CompletedTask;
            }
        }
        public Task GetLastMessengerAsync(CSteamID messenged, out CSteamID messenger)
        {
            messenger = CSteamID.Nil;
            if (Storage.Instance.SteamID.TryGetValue(messenged, out CSteamID lastMessenger))
            {
                messenger = lastMessenger;
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
