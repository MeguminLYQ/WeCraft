namespace WeCraft.Core.Entity
{
    public class NpcEntity:LivingEntity,IPlayer
    {
        public NpcEntity(uint id) : base(id)
        {
        }
    }
}