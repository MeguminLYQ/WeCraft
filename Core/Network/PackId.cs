namespace WeCraft.Core.Network
{
    public enum PackId:ushort
    {
        C2S_Ping=1,
        S2C_Pong=2,
        C2S_PlayerProfile=3,
        S2C_SpawnEntity=4,
        S2C_SpawnPlayerEntity=5,
    }
}