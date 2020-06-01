using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_Heredur
{
    public class Item
    {
        public int item_id { get; set; }
        public int item_inventorySlot { get; set; }
        public string item_name { get; set; }
        public string item_image { get; set; }
        public int item_level { get; set; }
        public int item_quantity { get; set; }
        public bool item_isStackable { get; set; }
        public EnumQuality item_quality { get; set; }
        public EnumItemType item_type { get; set; }
        public EnumRarity item_rarity { get; set; }
        public EnumPotionEffect item_potionEffect { get; set; }
        public int item_potionEffectValue { get; set; }
        public int item_potionEffectDuration { get; set; }
        public int item_primaryStat_damageMax { get; set; }
        public int item_primaryStat_defense { get; set; }
        public EnumPossibleBonusStats item_bonus1Type { get; set; }
        public int item_bonus1Value { get; set; }
        public EnumPossibleBonusStats item_bonus2Type { get; set; }
        public int item_bonus2Value { get; set; }
        public EnumPossibleBonusStats item_bonus3Type { get; set; }
        public int item_bonus3Value { get; set; }
        public EnumPossibleBonusStats item_bonus4Type { get; set; }
        public int item_bonus4Value { get; set; }
    }
}
