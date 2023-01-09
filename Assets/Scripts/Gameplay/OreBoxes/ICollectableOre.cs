using Inventory;
using Misc;

namespace Gameplay.OreBoxes
{
    public interface ICollectableOre
    {
        public MaterialType GetMaterialType();
        public void GetCollected(PlayerInventory inventory);
    }
}