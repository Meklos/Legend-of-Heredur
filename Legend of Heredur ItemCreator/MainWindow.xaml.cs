using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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

namespace Legend_of_Heredur_ItemCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ItemGenerator> ItemGenerators = new List<ItemGenerator>();
        string debugFolder = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            debugFolder = System.AppDomain.CurrentDomain.BaseDirectory;


            //Összes item betöltése a datagridbe
            GetData();




            //Ikonok feltöltése
            
            foreach (string icon in Directory.GetFiles("wowicons", "*.png"))
            {
                Image im_icon = new Image() {
                    Width = 50,
                    Height = 50,
                    Source = new BitmapImage(new Uri(debugFolder + icon)),
                    Tag = System.IO.Path.GetFileName(debugFolder + icon)
                };
                im_icon.MouseDown += Im_icon_MouseDown;
                wp_itemIcons.Children.Add(im_icon);
            }

            //Legördülők feltöltése
            //Típus

            cb_itemType.Items.Add(EnumItemType.Fegyver);
            cb_itemType.Items.Add(EnumItemType.Ruha);
            cb_itemType.Items.Add(EnumItemType.Ital);         
            cb_itemType.Items.Add(EnumItemType.Fizetőeszköz);         
            cb_itemType.Items.Add(EnumItemType.Szemét);
            cb_itemType.Items.Add(EnumItemType.Varázslat);
            cb_itemType.Items.Add(EnumItemType.Alapanyag);
            //Ritkaság
            cb_itemRarity.Items.Add(EnumRarity.Normal);
            cb_itemRarity.Items.Add(EnumRarity.Magic);
            cb_itemRarity.Items.Add(EnumRarity.Rare);
            cb_itemRarity.Items.Add(EnumRarity.Unique);
            cb_itemRarity.SelectedIndex = 0;
            //Minőség
            cb_itemQualityFrom.Items.Add(EnumQuality.Gyenge + " (-25% alapstat)");
            cb_itemQualityFrom.Items.Add(EnumQuality.Normál + " (+0% alapstat)");
            cb_itemQualityFrom.Items.Add(EnumQuality.Jó + " (+10% alapstat)");
            cb_itemQualityFrom.Items.Add(EnumQuality.Kiemelkedő + " (+25% alapstat)");
            cb_itemQualityFrom.Items.Add(EnumQuality.Tökéletes + " (+50% alapstat)");
            cb_itemQualityTo.Items.Add(EnumQuality.Gyenge + " (-25% alapstat)");
            cb_itemQualityTo.Items.Add(EnumQuality.Normál + " (+0% alapstat)");
            cb_itemQualityTo.Items.Add(EnumQuality.Jó + " (+10% alapstat)");
            cb_itemQualityTo.Items.Add(EnumQuality.Kiemelkedő + " (+25% alapstat)");
            cb_itemQualityTo.Items.Add(EnumQuality.Tökéletes + " (+50% alapstat)");
            //Poti hatások
            cb_itemPotionEffect.Items.Add(EnumPotionEffect.Health);
            cb_itemPotionEffect.Items.Add(EnumPotionEffect.Mana);
            cb_itemPotionEffect.Items.Add(EnumPotionEffect.StrengthPercent);
            cb_itemPotionEffect.Items.Add(EnumPotionEffect.DexterityPercent);
            cb_itemPotionEffect.Items.Add(EnumPotionEffect.DefensePercent);
            cb_itemPotionEffect.Items.Add(EnumPotionEffect.DodgeChancePercent);
            cb_itemPotionEffect.Items.Add(EnumPotionEffect.CriticalHitChancePercent);

            //Minden nullázása
            bt_new_Click(null, null);
        }

        private void GetData()
        {
            ItemGenerators = DatabaseContorller.GetAllData();
            
            //DataGrid feltöltése           
            dg_items.Items.Clear();
            foreach (ItemGenerator item in ItemGenerators)
            {                
                dg_items.Items.Add(item);
            }

            //DataGrid formázása
            StaticHelper.DataGridFormat(dg_items, ItemGenerators, (bool)cb_enableBonuses.IsChecked);


        }

        private void Im_icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            im_selected.Source = StaticHelper.ImageConverter(sender).Source;
            tb_itemImage.Text = System.IO.Path.GetFileName(StaticHelper.ImageConverter(sender).Source.ToString());
        }
        private void cb_itemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sp_itemDamage.Visibility = Visibility.Collapsed;
            sp_itemDefense.Visibility = Visibility.Collapsed;
            sp_itemQuanity.Visibility = Visibility.Collapsed;
            sp_itemQuality.Visibility = Visibility.Collapsed;
            lb_bonusStats.Visibility = Visibility.Collapsed;
            sv_bonusStats.Visibility = Visibility.Collapsed;
            sp_itemPotionEffect.Visibility = Visibility.Collapsed;
            sp_itemRarity.Visibility = Visibility.Collapsed;
            cb_isStackable.IsEnabled = true;
            switch (StaticHelper.ComboBoxConverter(sender).SelectedIndex)
            {
                case 0: //Fegyver
                    sp_itemDamage.Visibility = Visibility.Visible;
                    sp_itemQuality.Visibility = Visibility.Visible;
                    lb_bonusStats.Visibility = Visibility.Visible;
                    sv_bonusStats.Visibility = Visibility.Visible;
                    sp_itemRarity.Visibility = Visibility.Visible;
                    break;
                case 1: //Ruha
                    sp_itemDefense.Visibility = Visibility.Visible;
                    sp_itemQuality.Visibility = Visibility.Visible;
                    lb_bonusStats.Visibility = Visibility.Visible;
                    sv_bonusStats.Visibility = Visibility.Visible;
                    sp_itemRarity.Visibility = Visibility.Visible;
                    break;
                case 2: //Ital
                    sp_itemQuanity.Visibility = Visibility.Visible;
                    sp_itemQuanity.Visibility = Visibility.Visible;                   
                    sp_itemPotionEffect.Visibility = Visibility.Visible;
                    break;

                case 3: //Fizetőeszköz
                    sp_itemQuanity.Visibility = Visibility.Visible;
                    sp_itemQuanity.Visibility = Visibility.Visible;
                    cb_isStackable.IsEnabled = false;
                    break;
                 
                case 4: //Szemét
                    sp_itemQuanity.Visibility = Visibility.Visible;
                    sp_itemQuanity.Visibility = Visibility.Visible;
                    sp_itemPotionEffect.Visibility = Visibility.Visible;
                    break;
                case 5: //Varázslat

               
                    break;
                case 6: //Alapanyag
                    sp_itemQuanity.Visibility = Visibility.Visible;
                    sp_itemQuanity.Visibility = Visibility.Visible;
                    sp_itemPotionEffect.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void cb_itemRarity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb_bonus1_min.Text = "0";
            tb_bonus1_max.Text = "0";
            tb_bonus2_min.Text = "0";
            tb_bonus2_max.Text = "0";
            tb_bonus3_min.Text = "0";
            tb_bonus3_max.Text = "0";
            tb_bonus4_min.Text = "0";
            tb_bonus4_max.Text = "0";
            tb_bonus5_min.Text = "0";
            tb_bonus5_max.Text = "0";
            tb_bonus6_min.Text = "0";
            tb_bonus6_max.Text = "0";
            tb_bonus7_min.Text = "0";
            tb_bonus7_max.Text = "0";
            tb_bonus8_min.Text = "0";
            tb_bonus8_max.Text = "0";
            tb_bonus9_min.Text = "0";
            tb_bonus9_max.Text = "0";
            tb_bonus10_min.Text = "0";
            tb_bonus10_max.Text = "0";
            tb_bonus11_min.Text = "0";
            tb_bonus11_max.Text = "0";
            tb_bonus12_min.Text = "0";
            tb_bonus12_max.Text = "0";
            tb_bonus13_min.Text = "0";
            tb_bonus13_max.Text = "0";
            tb_bonus14_min.Text = "0";
            tb_bonus14_max.Text = "0";
            tb_bonus15_min.Text = "0";
            tb_bonus15_max.Text = "0";
            tb_bonus16_min.Text = "0";
            tb_bonus16_max.Text = "0";
            tb_bonus17_min.Text = "0";
            tb_bonus17_max.Text = "0";
            tb_bonus18_min.Text = "0";
            tb_bonus18_max.Text = "0";
            tb_bonus19_min.Text = "0";
            tb_bonus19_max.Text = "0";
            tb_bonus20_min.Text = "0";
            tb_bonus20_max.Text = "0";
            tb_bonus21_min.Text = "0";
            tb_bonus21_max.Text = "0";
            tb_bonus22_min.Text = "0";
            tb_bonus22_max.Text = "0";
            switch (StaticHelper.ComboBoxConverter(sender).SelectedIndex)
            {
                case 0:
                    lb_bonusStats.Background = Brushes.White;
                    lb_bonusStats.Content = "Mágiksus statok:";
                    lb_bonusStats.Visibility = Visibility.Collapsed;
                    sv_bonusStats.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    lb_bonusStats.Background = Brushes.Green;
                    lb_bonusStats.Content = "Mágiksus statok legalább 1 kötelező:";
                    lb_bonusStats.Visibility = Visibility.Visible;
                    sv_bonusStats.Visibility = Visibility.Visible;
                    break;
                case 2:
                    lb_bonusStats.Background = Brushes.Blue;
                    lb_bonusStats.Content = "Mágiksus statok legalább 2 kötelező:";
                    lb_bonusStats.Visibility = Visibility.Visible;
                    sv_bonusStats.Visibility = Visibility.Visible;
                    break;
                case 3:
                    lb_bonusStats.Background = Brushes.Gold;
                    lb_bonusStats.Content = "Mágiksus statok legalább 3 kötelező:";
                    lb_bonusStats.Visibility = Visibility.Visible;
                    sv_bonusStats.Visibility = Visibility.Visible;
                    break;
            }
        }


        private void bt_save_Click(object sender, RoutedEventArgs e)
        {
            ItemGenerator itemGenerator = new ItemGenerator();
            itemGenerator.item_name = tb_itemName.Text.ToString();
            itemGenerator.item_image = tb_itemImage.Text.ToString();         
            if (cb_itemType.SelectedItem != null)
            {
                itemGenerator.item_type = cb_itemType.SelectedIndex;               
            }        
            if (cb_itemRarity.SelectedItem != null)
            {
                itemGenerator.item_rarity = cb_itemRarity.SelectedIndex;
            }

            if (cb_itemPotionEffect.SelectedItem != null)
            {
                itemGenerator.item_potionEffect = cb_itemPotionEffect.SelectedIndex;
            }
            itemGenerator.item_potionEffectValue = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_itemPotionEffectValue.Text));
            itemGenerator.item_potionEffectDuration = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_itemPotionEffectDuration.Text));

            itemGenerator.item_primaryStat_damageMax_randomMin = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_item_primaryStat_damageMax_randomMin.Text));
            itemGenerator.item_primaryStat_damageMax_randomMax = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_item_primaryStat_damageMax_randomMax.Text));
            itemGenerator.item_primaryStat_defenseMax_randomMin = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_item_primaryStat_defenseMax_randomMin.Text));
            itemGenerator.item_primaryStat_defenseMax_randomMax = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_item_primaryStat_defenseMax_randomMax.Text));


            itemGenerator.item_level = Convert.ToInt32(tb_itemLevel.Text);
            itemGenerator.item_requiedHeroLevel = Convert.ToInt32(tb_itemRequiedLevel.Text);
            itemGenerator.item_quantityFrom = Convert.ToInt32(tb_itemQuantityFrom.Text);
            itemGenerator.item_quantityTo = Convert.ToInt32(tb_itemQuantityTo.Text);

            itemGenerator.item_isStackable = ((bool)cb_isStackable.IsChecked ? 1 : 0);

            if (cb_itemQualityFrom.SelectedItem != null && cb_itemQualityTo.SelectedItem != null)
            {
                itemGenerator.item_qualityFrom = cb_itemQualityFrom.SelectedIndex;
                itemGenerator.item_qualityTo = cb_itemQualityTo.SelectedIndex;
            }
            


            itemGenerator.item_bonus1_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus1_min.Text));
            itemGenerator.item_bonus1_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus1_max.Text));
            itemGenerator.item_bonus2_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus2_min.Text));
            itemGenerator.item_bonus2_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus2_max.Text));
            itemGenerator.item_bonus3_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus3_min.Text));
            itemGenerator.item_bonus3_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus3_max.Text));
            itemGenerator.item_bonus4_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus4_min.Text));
            itemGenerator.item_bonus4_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus4_max.Text));
            itemGenerator.item_bonus5_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus5_min.Text));
            itemGenerator.item_bonus5_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus5_max.Text));
            itemGenerator.item_bonus6_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus6_min.Text));
            itemGenerator.item_bonus6_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus6_max.Text));
            itemGenerator.item_bonus7_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus7_min.Text));
            itemGenerator.item_bonus7_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus7_max.Text));
            itemGenerator.item_bonus8_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus8_min.Text));
            itemGenerator.item_bonus8_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus8_max.Text));
            itemGenerator.item_bonus9_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus9_min.Text));
            itemGenerator.item_bonus9_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus9_max.Text));
            itemGenerator.item_bonus10_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus10_min.Text));
            itemGenerator.item_bonus10_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus10_max.Text));
            itemGenerator.item_bonus11_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus11_min.Text));
            itemGenerator.item_bonus11_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus11_max.Text));
            itemGenerator.item_bonus12_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus12_min.Text));
            itemGenerator.item_bonus12_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus12_max.Text));
            itemGenerator.item_bonus13_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus13_min.Text));
            itemGenerator.item_bonus13_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus13_max.Text));
            itemGenerator.item_bonus14_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus14_min.Text));
            itemGenerator.item_bonus14_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus14_max.Text));
            itemGenerator.item_bonus15_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus15_min.Text));
            itemGenerator.item_bonus15_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus15_max.Text));

            itemGenerator.item_bonus16_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus16_min.Text));
            itemGenerator.item_bonus16_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus16_max.Text));
            itemGenerator.item_bonus17_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus17_min.Text));
            itemGenerator.item_bonus17_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus17_max.Text));
            itemGenerator.item_bonus18_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus18_min.Text));
            itemGenerator.item_bonus18_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus18_max.Text));
            itemGenerator.item_bonus19_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus19_min.Text));
            itemGenerator.item_bonus19_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus19_max.Text));
            itemGenerator.item_bonus20_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus20_min.Text));
            itemGenerator.item_bonus20_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus20_max.Text));
            itemGenerator.item_bonus21_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus21_min.Text));
            itemGenerator.item_bonus21_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus21_max.Text));
            itemGenerator.item_bonus22_min = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus22_min.Text));
            itemGenerator.item_bonus22_max = Convert.ToInt32(StaticHelper.ifStringEmpty(tb_bonus22_max.Text));

            DatabaseContorller.InsertNewRow(itemGenerator);

            bt_new_Click(null, null);


            
            //bemásolja azt a képet amelyik ki lett választva
            string from = debugFolder+ "wowicons\\" + itemGenerator.item_image;

            string to = from.Replace(" ItemCreator", "").Replace("wowicons\\", "Images\\");
            File.Copy(from, to, true);

            GetData();
        }

        private void dg_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ItemGenerator selectedItemGenerator = (ItemGenerator)dg_items.SelectedItem;
            if (selectedItemGenerator != null)
            {
                im_selected.Source = new BitmapImage(new Uri(selectedItemGenerator.item_image));
                tb_itemImage.Text = System.IO.Path.GetFileName(selectedItemGenerator.item_image);
                tb_itemName.Text = selectedItemGenerator.item_name;
                cb_itemType.SelectedIndex = selectedItemGenerator.item_type;
                tb_item_primaryStat_damageMax_randomMin.Text = selectedItemGenerator.item_primaryStat_damageMax_randomMin.ToString();
                tb_item_primaryStat_damageMax_randomMax.Text = selectedItemGenerator.item_primaryStat_damageMax_randomMax.ToString();
                tb_item_primaryStat_defenseMax_randomMin.Text = selectedItemGenerator.item_primaryStat_defenseMax_randomMin.ToString();
                tb_item_primaryStat_defenseMax_randomMax.Text = selectedItemGenerator.item_primaryStat_defenseMax_randomMax.ToString();
                cb_itemRarity.SelectedIndex = selectedItemGenerator.item_rarity;
                cb_itemPotionEffect.SelectedIndex = selectedItemGenerator.item_potionEffect;
                tb_itemPotionEffectValue.Text = selectedItemGenerator.item_potionEffectValue.ToString();
                tb_itemPotionEffectDuration.Text = selectedItemGenerator.item_potionEffectDuration.ToString();

                cb_itemQualityFrom.SelectedIndex = selectedItemGenerator.item_qualityFrom;
                cb_itemQualityTo.SelectedIndex = selectedItemGenerator.item_qualityTo;
                cb_isStackable.IsChecked = (selectedItemGenerator.item_isStackable == 0 ? false : true);
                tb_itemQuantityFrom.Text = selectedItemGenerator.item_quantityFrom.ToString();
                tb_itemQuantityTo.Text = selectedItemGenerator.item_quantityTo.ToString();
                tb_itemLevel.Text = selectedItemGenerator.item_level.ToString();
                tb_itemRequiedLevel.Text = selectedItemGenerator.item_requiedHeroLevel.ToString();
                tb_bonus1_min.Text = selectedItemGenerator.item_bonus1_min.ToString();
                tb_bonus1_max.Text = selectedItemGenerator.item_bonus1_max.ToString();
                tb_bonus2_min.Text = selectedItemGenerator.item_bonus2_min.ToString();
                tb_bonus2_max.Text = selectedItemGenerator.item_bonus2_max.ToString();
                tb_bonus3_min.Text = selectedItemGenerator.item_bonus3_min.ToString();
                tb_bonus3_max.Text = selectedItemGenerator.item_bonus3_max.ToString();
                tb_bonus4_min.Text = selectedItemGenerator.item_bonus4_min.ToString();
                tb_bonus4_max.Text = selectedItemGenerator.item_bonus4_max.ToString();
                tb_bonus5_min.Text = selectedItemGenerator.item_bonus5_min.ToString();
                tb_bonus5_max.Text = selectedItemGenerator.item_bonus5_max.ToString();
                tb_bonus6_min.Text = selectedItemGenerator.item_bonus6_min.ToString();
                tb_bonus6_max.Text = selectedItemGenerator.item_bonus6_max.ToString();
                tb_bonus7_min.Text = selectedItemGenerator.item_bonus7_min.ToString();
                tb_bonus7_max.Text = selectedItemGenerator.item_bonus7_max.ToString();
                tb_bonus8_min.Text = selectedItemGenerator.item_bonus8_min.ToString();
                tb_bonus8_max.Text = selectedItemGenerator.item_bonus8_max.ToString();
                tb_bonus9_min.Text = selectedItemGenerator.item_bonus9_min.ToString();
                tb_bonus9_max.Text = selectedItemGenerator.item_bonus9_max.ToString();
                tb_bonus10_min.Text = selectedItemGenerator.item_bonus10_min.ToString();
                tb_bonus10_max.Text = selectedItemGenerator.item_bonus10_max.ToString();
                tb_bonus11_min.Text = selectedItemGenerator.item_bonus11_min.ToString();
                tb_bonus11_max.Text = selectedItemGenerator.item_bonus11_max.ToString();
                tb_bonus12_min.Text = selectedItemGenerator.item_bonus12_min.ToString();
                tb_bonus12_max.Text = selectedItemGenerator.item_bonus12_max.ToString();
                tb_bonus13_min.Text = selectedItemGenerator.item_bonus13_min.ToString();
                tb_bonus13_max.Text = selectedItemGenerator.item_bonus13_max.ToString();
                tb_bonus14_min.Text = selectedItemGenerator.item_bonus14_min.ToString();
                tb_bonus14_max.Text = selectedItemGenerator.item_bonus14_max.ToString();
                tb_bonus15_min.Text = selectedItemGenerator.item_bonus15_min.ToString();
                tb_bonus15_max.Text = selectedItemGenerator.item_bonus15_max.ToString();

                tb_bonus16_min.Text = selectedItemGenerator.item_bonus16_min.ToString();
                tb_bonus16_max.Text = selectedItemGenerator.item_bonus16_max.ToString();
                tb_bonus17_min.Text = selectedItemGenerator.item_bonus17_min.ToString();
                tb_bonus17_max.Text = selectedItemGenerator.item_bonus17_max.ToString();
                tb_bonus18_min.Text = selectedItemGenerator.item_bonus18_min.ToString();
                tb_bonus18_max.Text = selectedItemGenerator.item_bonus18_max.ToString();
                tb_bonus19_min.Text = selectedItemGenerator.item_bonus19_min.ToString();
                tb_bonus19_max.Text = selectedItemGenerator.item_bonus19_max.ToString();
                tb_bonus20_min.Text = selectedItemGenerator.item_bonus20_min.ToString();
                tb_bonus20_max.Text = selectedItemGenerator.item_bonus20_max.ToString();
                tb_bonus21_min.Text = selectedItemGenerator.item_bonus21_min.ToString();
                tb_bonus21_max.Text = selectedItemGenerator.item_bonus21_max.ToString();
                tb_bonus22_min.Text = selectedItemGenerator.item_bonus22_min.ToString();
                tb_bonus22_max.Text = selectedItemGenerator.item_bonus22_max.ToString();
            }

        }

        private void bt_new_Click(object sender, RoutedEventArgs e)
        {
            tb_itemName.Text = string.Empty;
            im_selected.Source = null;
            cb_itemType.SelectedIndex = -1;
            tb_item_primaryStat_damageMax_randomMin.Text = "0";
            tb_item_primaryStat_damageMax_randomMax.Text = "0";
            tb_item_primaryStat_defenseMax_randomMin.Text = "0";
            tb_item_primaryStat_defenseMax_randomMax.Text = "0";
            cb_itemRarity.SelectedIndex = 0;
            cb_itemQualityFrom.SelectedIndex = 1;
            cb_itemQualityTo.SelectedIndex = 1;
            tb_itemQuantityFrom.Text = "1";
            tb_itemQuantityTo.Text = "1";
            cb_isStackable.IsChecked = false;
            tb_itemLevel.Text = "0";
            tb_itemRequiedLevel.Text = "0";
            tb_bonus1_min.Text = "0";
            tb_bonus1_max.Text = "0";
            tb_bonus2_min.Text = "0";
            tb_bonus2_max.Text = "0";
            tb_bonus3_min.Text = "0";
            tb_bonus3_max.Text = "0";
            tb_bonus4_min.Text = "0";
            tb_bonus4_max.Text = "0";
            tb_bonus5_min.Text = "0";
            tb_bonus5_max.Text = "0";
            tb_bonus6_min.Text = "0";
            tb_bonus6_max.Text = "0";
            tb_bonus7_min.Text = "0";
            tb_bonus7_max.Text = "0";
            tb_bonus8_min.Text = "0";
            tb_bonus8_max.Text = "0";
            tb_bonus9_min.Text = "0";
            tb_bonus9_max.Text = "0";
            tb_bonus10_min.Text = "0";
            tb_bonus10_max.Text = "0";
            tb_bonus11_min.Text = "0";
            tb_bonus11_max.Text = "0";
            tb_bonus12_min.Text = "0";
            tb_bonus12_max.Text = "0";
            tb_bonus13_min.Text = "0";
            tb_bonus13_max.Text = "0";
            tb_bonus14_min.Text = "0";
            tb_bonus14_max.Text = "0";
            tb_bonus15_min.Text = "0";
            tb_bonus15_max.Text = "0";
            cb_itemPotionEffect.SelectedIndex = -1;
            tb_itemPotionEffectValue.Text = "0";
            tb_itemPotionEffectDuration.Text = "0";
        }


        private void sp_itemPrimaryStat_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            string from = StaticHelper.TextBoxConverter(StaticHelper.StackPanelConverter(sender).Children[1]).Text;
            string to = StaticHelper.TextBoxConverter(StaticHelper.StackPanelConverter(sender).Children[2]).Text;




            StaticHelper.StackPanelConverter(sender).ToolTip = 
                "Gyenge: "+StaticHelper.CalculateQualityValue(Convert.ToInt32(from),EnumQuality.Gyenge)+"-"+ StaticHelper.CalculateQualityValue(Convert.ToInt32(to), EnumQuality.Gyenge)+
                "\nNormál: "+ from + "-" + to +
                "\nJó: "+StaticHelper.CalculateQualityValue(Convert.ToInt32(from), EnumQuality.Jó) + "-" + StaticHelper.CalculateQualityValue(Convert.ToInt32(to), EnumQuality.Jó) +
               "\nKiemelkedő: "+StaticHelper.CalculateQualityValue(Convert.ToInt32(from), EnumQuality.Kiemelkedő) + "-" + StaticHelper.CalculateQualityValue(Convert.ToInt32(to), EnumQuality.Kiemelkedő) +
               "\nTökéletes:" + StaticHelper.CalculateQualityValue(Convert.ToInt32(from), EnumQuality.Tökéletes) + "-" + StaticHelper.CalculateQualityValue(Convert.ToInt32(to), EnumQuality.Tökéletes);
            
        }

        private void cb_enableBonuses_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void bt_copyImages_Click(object sender, RoutedEventArgs e)
        {

            //Fájl törlés
            string thisFolder = debugFolder.Replace(" ItemCreator", "")+"Images\\";
            string[] fileArray = Directory.GetFiles(thisFolder, "*.png");
            foreach (string imagesPath in fileArray)
            {
                File.Delete(imagesPath);
            }





            foreach (ItemGenerator imagesPath in DatabaseContorller.GetAllData())
            {
                string from = imagesPath.item_image;

                string to = imagesPath.item_image.Replace(" ItemCreator", "").Replace("wowicons\\", "Images\\");
                File.Copy(from, to, true);
            }
        }

        private void bt_delete_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemGenerator item in dg_items.SelectedItems)
            {
                DatabaseContorller.DeleteRow(item);
            }
            GetData();
        }
    }
}
