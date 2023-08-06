using Riptide;

namespace WeCraftServer.Utility
{
    public static class ConnectionExt
    {
        public static void Disconnect(this Connection conn,string msg=null)
        {
            WeCraftServer.GetInstance().NetworkManager.EmbeddedServer.DisconnectClient(conn,null);
        }
    }
}