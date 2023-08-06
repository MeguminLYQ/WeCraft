using System;
using WeCraft.Core.C2S;

namespace WeCraft.Core.Entity
{
    public class Player: LivingEntity,IPlayer
    {  
        protected Player(uint id) : base(id)
        {
        }
        public Player(C2S_PlayerProfile profile,uint id):base(id)
        {
            this.Guid = new Guid(profile.Guid);
            this.Name = profile.Name;
            this.DisplayName = Name;
        }
    }
}