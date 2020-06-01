using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_Heredur
{
    public class ItemGenerator
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_image { get; set; }
        public int item_level { get; set; }
        public int item_requiedHeroLevel { get; set; }
        public int item_quantityFrom { get; set; }
        public int item_quantityTo { get; set; }
        public int item_isStackable { get; set; }
        public int item_qualityFrom { get; set; }
        public int item_qualityTo { get; set; }
        public int item_type { get; set; }
        public int item_rarity { get; set; }
        public int item_potionEffect { get; set; }
        public int item_potionEffectValue { get; set; }
        public int item_potionEffectDuration { get; set; }
        public int item_primaryStat_damageMax_randomMin { get; set; }
        public int item_primaryStat_damageMax_randomMax { get; set; }
        public int item_primaryStat_defenseMax_randomMin { get; set; }
        public int item_primaryStat_defenseMax_randomMax { get; set; }
        public List<ItemBonus> item_bonuses { get; set; }
    }
}
