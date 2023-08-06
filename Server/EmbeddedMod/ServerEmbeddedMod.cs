using System;
using NLog;
using WeCraft.Core.C2S;
using WeCraft.Core.Entity;
using WeCraft.Core.Event;
using WeCraft.Core.EventHandler;
using WeCraft.Core.Mod;
using WeCraft.Core.Network;
using WeCraft.Core.Utility;
using WeCraftServer.Event.Player;
using WeCraftServer.Network;

namespace WeCraftServer.EmbeddedMod
{
    public class ServerEmbeddedMod: ModBase
    {
        private WeCraftServer _server=WeCraftServer.GetInstance();
        private NetworkHandler _handler=>_server.NetworkHandlerImpl;
        private NetworkManager _manager => _server.NetworkManager;
        public ILogger Logger => _server.LoggerImpl;

        private ServerEmbeddedNetworkHandler _embeddedNetwork;
        private ServerEmbeddedEventHandler _embeddedEvent;
        public ServerEmbeddedMod()
        {
            Name = "WeCraftServer";
            _embeddedNetwork = new ServerEmbeddedNetworkHandler(this);
            _embeddedEvent = new ServerEmbeddedEventHandler(this);
        }
        public override void OnLoad()
        {
            
        }

        public override void OnEnable()
        {
            var defaultChannel = _handler.GetDefaultChannel();
            defaultChannel.RegisterHandler((ushort)PackId.C2S_PlayerProfile, _embeddedNetwork.OnPlayerJoin);
            EventBus.Register(new ModId(){Domain = "WeCraftServer"},new ExecuteDelegate<PlayerJoinEvent>(_embeddedEvent.OnPlayerJoin));
            
        }

        public override void OnDisable()
        {
            
        }



    }
}