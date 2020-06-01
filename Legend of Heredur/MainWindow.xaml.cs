using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Legend_of_Heredur
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ItemGenerator> ItemDataBase = DatabaseContorller.GetAllData();
        public MainWindow()
        {
            InitializeComponent();

            //bt_dropItem = StaticHelper.SetButtonColors(bt_dropItem);


            InventoryController.StartEmptyInventory();

            InventoryController.StartEmptyLoot();
        }

        private void bt_dropItem_Click(object sender, RoutedEventArgs e)
        {        
            for (int i = 0; i < 1; i++)
            {
                int lvl = Convert.ToInt32(tb_dropLevel.Text);
                Item item = DropRandomItem(lvl);

                InventoryController.AddNewItem(item);
            }


        }

    

        Item DropRandomItem(int input)
        {
            Random random = new Random();
            Item newItem = new Item();

            //Szint szűrése
            List<ItemGenerator> ItemDatabaseFilteredByLevel = ItemDataBase;//.Where(x => x.item_id == 67).ToList();

            ItemGenerator selectedItem = ItemDatabaseFilteredByLevel[random.Next(ItemDatabaseFilteredByLevel.Count)];

            newItem.item_id = selectedItem.item_id;
            newItem.item_name = selectedItem.item_name;
            newItem.item_image = selectedItem.item_image;
            newItem.item_level = selectedItem.item_level;

            //Mennyiség random
            newItem.item_isStackable = (selectedItem.item_isStackable==0?false:true);

            if (selectedItem.item_quantityTo > 1)
            {
                newItem.item_quantity = random.Next(selectedItem.item_quantityFrom, selectedItem.item_quantityTo);
            }
            else
            {
                newItem.item_quantity = 1;
            }

            Thread.Sleep(50);
            //Minőség random
            newItem.item_quality = (EnumQuality)random.Next(selectedItem.item_qualityFrom, selectedItem.item_qualityTo+1);

            newItem.item_type = (EnumItemType)selectedItem.item_type;
            newItem.item_rarity = (EnumRarity)selectedItem.item_rarity;

            switch (newItem.item_type)
            {
                case EnumItemType.Fegyver:
                    //Fegyver sebzés random
                    int weaponDamage = random.Next(selectedItem.item_primaryStat_damageMax_randomMin, selectedItem.item_primaryStat_damageMax_randomMax);
                    newItem.item_primaryStat_damageMax = StaticHelper.CalculateStatByQuality(weaponDamage, newItem.item_quality);
                    break;
                case EnumItemType.Ruha:
                    //Ruha védelem random
                    int armorDefense = random.Next(selectedItem.item_primaryStat_defenseMax_randomMin, selectedItem.item_primaryStat_defenseMax_randomMax);
                    newItem.item_primaryStat_defense = StaticHelper.CalculateStatByQuality(armorDefense, newItem.item_quality);
                    break;
                case EnumItemType.Ital:
                    //Poti hatás, idő
                    newItem.item_potionEffect = (EnumPotionEffect)selectedItem.item_potionEffect;
                    newItem.item_potionEffectValue = selectedItem.item_potionEffectValue;
                    newItem.item_potionEffectDuration = selectedItem.item_potionEffectDuration;
                    break;

            }



            switch (selectedItem.item_rarity)
            {
                case 1:
                    Thread.Sleep(20);
                    int[] bonuses1_1 = StaticHelper.RollABonus(selectedItem);
                    newItem.item_bonus1Type = (EnumPossibleBonusStats)bonuses1_1[0];
                    newItem.item_bonus1Value = bonuses1_1[1];
                    break;
                case 2:
                    Thread.Sleep(20);
                    int[] bonuses2_1 = StaticHelper.RollABonus(selectedItem);
                    Thread.Sleep(20);
                    int[] bonuses2_2 = StaticHelper.RollABonus(selectedItem);
                    newItem.item_bonus1Type = (EnumPossibleBonusStats)bonuses2_1[0];
                    newItem.item_bonus1Value = bonuses2_1[1];
                   
                    newItem.item_bonus2Type = (EnumPossibleBonusStats)bonuses2_2[0];
                    newItem.item_bonus2Value = bonuses2_2[1];
                    break;
                case 3:
                    Thread.Sleep(20);
                    int[] bonuses3_1 = StaticHelper.RollABonus(selectedItem);
                    Thread.Sleep(20);
                    int[] bonuses3_2 = StaticHelper.RollABonus(selectedItem);
                    Thread.Sleep(20);
                    int[] bonuses3_3 = StaticHelper.RollABonus(selectedItem);
                    newItem.item_bonus1Type = (EnumPossibleBonusStats)bonuses3_1[0];
                    newItem.item_bonus1Value = bonuses3_1[1];

                    newItem.item_bonus2Type = (EnumPossibleBonusStats)bonuses3_2[0];
                    newItem.item_bonus2Value = bonuses3_2[1];

                    newItem.item_bonus3Type = (EnumPossibleBonusStats)bonuses3_3[0];
                    newItem.item_bonus3Value = bonuses3_3[1];
                    break;
            }


            return newItem;
        }

        private void bt_removeLoot_Click(object sender, RoutedEventArgs e)
        {
            InventoryController.DeleteLoot();
        }

        private void bt_pickAll_Click(object sender, RoutedEventArgs e)
        {
           

            foreach (Button item in wp_loot.Children)
            {
                InventoryController.PickALoot(item);
            }
           
        }

    }

}
