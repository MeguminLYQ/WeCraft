namespace WeCraft.Core.Entity
{
    public class LivingEntity: Entity
    {
        public float Health;
        public float MaxHealth; 
        public float Yaw;
        /// <summary>
        /// HUD的名字,可以和Name不一样
        /// </summary>
        public string DisplayName;

        public LivingEntity(uint id):base(id)
        {
        }
    }
}