using WeCraft.Core.Registery;

namespace WeCraft.Core.Entity
{
    public class EntityDefinition: IRegistrable<EntityDefinition>
    {
        public bool Equals(EntityDefinition other)
        {
            throw new System.NotImplementedException();
        }

        public EntityDefinition Value { get; }
        public string Type { get; }
    }
}