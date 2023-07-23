using WeCraft.Core.Registery;

namespace WeCraft.Core.World
{
    public class WorldDefinition:IRegistrable<WorldDefinition>
    {
        
        public bool Equals(WorldDefinition other)
        {
            throw new System.NotImplementedException();
        }

        public WorldDefinition Value { get; }
        public string Type { get; }
    }
}