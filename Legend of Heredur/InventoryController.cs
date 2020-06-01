using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Legend_of_Heredur
{
    public static class InventoryController
    {
        private static WrapPanel wp_inventory = ((MainWindow)System.Windows.Application.Current.MainWindow).wp_inventory;
        private static WrapPanel wp_loot = ((MainWindow)System.Windows.Application.Current.MainWindow).wp_loot;

        private static ListBox listbox = ((MainWindow)System.Windows.Application.Current.MainWindow).listBox;
        private static List<Item> Items = new List<Item>();
        private static List<Item> ItemsLoot = new List<Item>();

        public static void StartEmptyInventory()
        {
            for (int i = 0; i < 28; i++)
            {
                Button bt_inventorySlot = new Button()
                {
                    Width = 50,
                    Height = 50,
                    Background = Brushes.Black,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(18, 18, 18)),
                    BorderThickness = new Thickness(1)
                };
                bt_inventorySlot.PreviewMouseDown += Bt_inventorySlot_Click;
                wp_inventory.Children.Add(bt_inventorySlot);
            }
        }

        internal static void StartEmptyLoot()
        {
            for (int i = 0; i < 12; i++)
            {
                Button bt_lootSlot = new Button()
                {
                    Width = 50,
                    Height = 50,
                    Background = Brushes.Black,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(18, 18, 18)),
                    BorderThickness = new Thickness(1)
                };
                bt_lootSlot.Click += Bt_bt_lootSlot_Click;
                wp_loot.Children.Add(bt_lootSlot);
            }
        }

        public static void AddNewItem(Item inputItem)
        {
            if (ItemsLoot.Count != 12)
            {
                foreach (Button item in wp_loot.Children)
                {
                    if (item.Tag == null)
                    {
                        int index = wp_loot.Children.IndexOf(item);
                        inputItem.item_inventorySlot = index;
                        ItemsLoot.Insert(index, inputItem);
                        break;
                    }

                }


                CreateWrapPanelItems(EnumWrapPanels.Loot);
            }
            else
            {
                MessageBox.Show("Azinventorimassz full");
            }
        }

        private static bool clicked = false;
        private static Item pickedItem;
        private static void Bt_inventorySlot_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Released)
            {
                int selectedc = wp_inventory.Children.IndexOf((Button)sender);
                Item item = Items.FirstOrDefault(x => x.item_inventorySlot == selectedc);

                Items.Remove(item);
                CreateWrapPanelItems(EnumWrapPanels.Inventory);
                return;
            }

            if (e.MouseDevice.RightButton == MouseButtonState.Released)
            {
               
            }

            int selectedSlot = wp_inventory.Children.IndexOf((Button)sender);

            if (!clicked)
            {
                //Kijelöli azt a cuccot amire rákattintottunk
                clicked = true;
                pickedItem = Items.FirstOrDefault(x => x.item_inventorySlot == selectedSlot);

                //Amire rákattintottunk az halvány lesz
                StaticHelper.ButtonConverter(sender).Opacity = 0.3;

                //Amíg a kezünkben van a cucc addig az lesz az egér kurzor
                BitmapImage clickedImage = new BitmapImage(new Uri(StaticHelper.ImageConverter(StaticHelper.CanvasConverter(StaticHelper.ButtonConverter(sender).Content).Children[0]).Source.ToString()));              

                foreach (Button item in wp_inventory.Children)
                {
                    item.Cursor = StaticHelper.CreateCursor(clickedImage, 10, 10);
                }             
            }
            else
            {
                //Aztán megnézi hogy azon a helyen van-e valami ahova utána kattintunk
                clicked = false;
                //Ha leraktuk a cuccot visszaállítja alapértelmezettre a kurzort
                foreach (Button item in wp_inventory.Children)
                {
                    item.Cursor = Cursors.Hand;
                    item.Opacity = 1;
                }
                ExchangeItem(pickedItem.item_inventorySlot, selectedSlot);
                ExchangeItem(selectedSlot, pickedItem.item_inventorySlot);


            }
        }

        private static void Bt_bt_lootSlot_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PickALoot((Button)sender);
        }

        //Akkor fut le he megfogok a kezembe egy tárgyat és elhelyeztem.
        public static void ExchangeItem(int from, int to)
        {
            if (StaticHelper.ButtonConverter(wp_inventory.Children[to]).Tag != null)
            {
                Items.FirstOrDefault(x => x.item_inventorySlot == from).item_inventorySlot = to;
                Items.FirstOrDefault(x => x.item_inventorySlot == to).item_inventorySlot = from;
            }
            else
            {
                Items.FirstOrDefault(x => x.item_inventorySlot == from).item_inventorySlot = to;
            }
            listbox.Items.Clear();
            foreach (var item in Items)
            {
                listbox.Items.Add(item.item_name + "  " + item.item_inventorySlot);
            }
            CreateWrapPanelItems(EnumWrapPanels.Inventory);
        }





        private static void CreateWrapPanelItems(EnumWrapPanels enumWrapPanels)
        {
            WrapPanel currentWrapPanel = new WrapPanel();
            List<Item> itemsTemp = new List<Item>();

            switch (enumWrapPanels)
            {
                case EnumWrapPanels.Inventory:
                    currentWrapPanel = wp_inventory;
                    itemsTemp = Items;
                    break;
                case EnumWrapPanels.Loot:
                    currentWrapPanel = wp_loot;
                    itemsTemp = ItemsLoot;
                    break;
            }
            foreach (Button itemSlot in currentWrapPanel.Children)
            {
                int index = currentWrapPanel.Children.IndexOf(itemSlot);
                Item item = itemsTemp.FirstOrDefault(x => x.item_inventorySlot == index);

                if (item != null)
                {
                    itemSlot.Tag = "1";
                    itemSlot.Cursor = Cursors.Hand;
                    itemSlot.BorderThickness = new Thickness(1);

                    //Felszerelés képe

                    Image itemImage = new Image()
                    {
                        Width = 44,
                        Height = 44
                    };
                    TextBlock ItemQuantity = new TextBlock()
                    {
                        Width = 50,
                        Text = (item.item_quantity > 1 ? item.item_quantity.ToString() : string.Empty),
                        Foreground = Brushes.White,
                        TextAlignment = TextAlignment.Right,
                        Margin = new Thickness(-10, 26, 0, 0)
                    };
                    Canvas itemImageContainer = new Canvas()
                    {
                        Width = 50,
                        Height = 50
                    };
                    itemImageContainer.Children.Add(itemImage);
                    itemImageContainer.Children.Add(ItemQuantity);

                    if (item.item_image != null)
                    {
                        itemImage.Source = new BitmapImage(new Uri(item.item_image));
                        itemSlot.Content = itemImageContainer;
                    }
                    else
                    {
                        itemImage.Source = null;
                        itemSlot.Content = item.item_name;
                    }


                    //Felszerelés tulajdonságai
                    StackPanel stackPanelItemProperties = new StackPanel()
                    {
                        Orientation = Orientation.Vertical,
                        Width = 120
                    };

                    //Felszerelés neve
                    TextBlock tb_itemName = new TextBlock()
                    {
                        Text = item.item_name,
                        FontWeight = FontWeights.Bold,
                        Width = 120,
                        TextAlignment = TextAlignment.Center
                    };
                    stackPanelItemProperties.Children.Add(tb_itemName);

                    //Felszerelés alap tulajdnoságai
                    TextBlock tb_itemPrimaryStat = new TextBlock()
                    {
                        Width = 120,
                        TextAlignment = TextAlignment.Center
                    };

                    //Minőség
                    TextBlock tb_itemQuality = new TextBlock()
                    {
                        Text = "minőség: " + item.item_quality.ToString(),
                        Foreground = Brushes.DarkGray,
                        Width = 120,
                        TextAlignment = TextAlignment.Center
                    };
                    stackPanelItemProperties.Children.Add(tb_itemQuality);

                    switch (item.item_type)
                    {
                        case EnumItemType.Fegyver:
                            tb_itemQuality.Visibility = Visibility.Visible;
                            tb_itemPrimaryStat.Text = "sebzés: " + Convert.ToInt32(item.item_primaryStat_damageMax * 0.8) + " - " + item.item_primaryStat_damageMax; // a minimum sebzés a maximum 50%-a                            
                            stackPanelItemProperties.Children.Add(tb_itemPrimaryStat);
                            break;
                        case EnumItemType.Ruha:
                            tb_itemQuality.Visibility = Visibility.Visible;
                            tb_itemPrimaryStat.Text = "védelem: " + item.item_primaryStat_defense;
                            stackPanelItemProperties.Children.Add(tb_itemPrimaryStat);
                            break;
                        case EnumItemType.Ital:
                            tb_itemQuality.Visibility = Visibility.Visible;
                            tb_itemPrimaryStat.Text = StaticHelper.GetPotionStatText(item.item_potionEffect, item.item_potionEffectValue, item.item_potionEffectDuration);
                            stackPanelItemProperties.Children.Add(tb_itemPrimaryStat);
                            break;
                        case EnumItemType.Fizetőeszköz:
                            tb_itemQuality.Visibility = Visibility.Collapsed;                          
                            break;
                    }

                    itemSlot.ToolTip = new ToolTip()
                    {
                        Background = new SolidColorBrush(Color.FromRgb(31, 31, 31)),
                        Foreground = Brushes.White,
                        Width = 130,
                        MinHeight = 100,
                        BorderBrush = Brushes.DarkGray,
                        BorderThickness = new Thickness(1),
                        Content = stackPanelItemProperties
                    };

                    if (item.item_rarity != EnumRarity.Normal)
                    {
                        TextBlock tb_itemSecondaryStat1 = new TextBlock()
                        {
                            Width = 120,
                            TextAlignment = TextAlignment.Center,
                            Foreground = new SolidColorBrush(Color.FromRgb(3, 87, 255))
                        };
                        TextBlock tb_itemSecondaryStat2 = new TextBlock()
                        {
                            Width = 120,
                            TextAlignment = TextAlignment.Center,
                            Foreground = new SolidColorBrush(Color.FromRgb(3, 87, 255))
                        };
                        TextBlock tb_itemSecondaryStat3 = new TextBlock()
                        {
                            Width = 120,
                            TextAlignment = TextAlignment.Center,
                            Foreground = new SolidColorBrush(Color.FromRgb(3, 87, 255))
                        };

                        switch (item.item_rarity)
                        {
                            case EnumRarity.Magic:
                                tb_itemName.Foreground = Brushes.Green;
                                itemSlot.BorderBrush = Brushes.Green;
                                itemSlot.BorderThickness = new Thickness(2);
                                tb_itemSecondaryStat1.Text = StaticHelper.GetBonusStatText(item.item_bonus1Type, item.item_bonus1Value);
                                stackPanelItemProperties.Children.Add(tb_itemSecondaryStat1);
                                break;
                            case EnumRarity.Rare:
                                tb_itemName.Foreground = new SolidColorBrush(Color.FromRgb(3, 87, 255));
                                itemSlot.BorderBrush = new SolidColorBrush(Color.FromRgb(3, 87, 255));


                                tb_itemSecondaryStat1.Text = StaticHelper.GetBonusStatText(item.item_bonus1Type, item.item_bonus1Value);
                                tb_itemSecondaryStat2.Text = StaticHelper.GetBonusStatText(item.item_bonus2Type, item.item_bonus2Value);
                                itemSlot.BorderThickness = new Thickness(2);
                                stackPanelItemProperties.Children.Add(tb_itemSecondaryStat1);
                                stackPanelItemProperties.Children.Add(tb_itemSecondaryStat2);
                                break;
                            case EnumRarity.Unique:
                                tb_itemName.Foreground = Brushes.Gold;
                                itemSlot.BorderBrush = Brushes.Gold;
                                tb_itemSecondaryStat1.Text = StaticHelper.GetBonusStatText(item.item_bonus1Type, item.item_bonus1Value);
                                tb_itemSecondaryStat2.Text = StaticHelper.GetBonusStatText(item.item_bonus2Type, item.item_bonus2Value);
                                tb_itemSecondaryStat3.Text = StaticHelper.GetBonusStatText(item.item_bonus3Type, item.item_bonus3Value);
                                itemSlot.BorderThickness = new Thickness(2);
                                stackPanelItemProperties.Children.Add(tb_itemSecondaryStat1);
                                stackPanelItemProperties.Children.Add(tb_itemSecondaryStat2);
                                stackPanelItemProperties.Children.Add(tb_itemSecondaryStat3);
                                break;
                        }
                    }
                    else
                    {
                        itemSlot.BorderBrush = new SolidColorBrush(Color.FromRgb(18, 18, 18));
                        itemSlot.BorderThickness = new Thickness(1);
                    }





                }
                else
                {
                    itemSlot.Content = null;
                    itemSlot.ToolTip = null;
                    itemSlot.Tag = null;
                    itemSlot.Background = Brushes.Black;
                    itemSlot.BorderBrush = new SolidColorBrush(Color.FromRgb(18, 18, 18));
                    itemSlot.BorderThickness = new Thickness(1);

                }
            }

            if (ItemsLoot.Count > 0)
            {
                wp_loot.Visibility = Visibility.Visible;
                Border db_wp_loot = (Border)wp_loot.Parent;
                db_wp_loot.Visibility = Visibility.Visible;
            }
            else
            {
                wp_loot.Visibility = Visibility.Visible;
                Border db_wp_loot = (Border)wp_loot.Parent;
                db_wp_loot.Visibility = Visibility.Hidden;
            }
        }

        public static void DeleteLoot()
        {
            ItemsLoot.Clear();
            CreateWrapPanelItems(EnumWrapPanels.Loot);
            //CreateWrapPanelItems(EnumWrapPanels.Inventory);


            if (ItemsLoot.Count > 0)
            {
                wp_loot.Visibility = Visibility.Visible;
                Border db_wp_loot = (Border)wp_loot.Parent;
                db_wp_loot.Visibility = Visibility.Visible;
            }
            else
            {
                wp_loot.Visibility = Visibility.Visible;
                Border db_wp_loot = (Border)wp_loot.Parent;
                db_wp_loot.Visibility = Visibility.Hidden;
            }
        }

        public static void PickALoot(Button bt_itemLoot)
        {
            Item itemLoot = new Item();
            if (bt_itemLoot.Tag != null)
            {
                itemLoot = ItemsLoot.FirstOrDefault(x => x.item_inventorySlot == wp_loot.Children.IndexOf(bt_itemLoot));
            }
            else
            {
                return;
            }

            if (itemLoot.item_isStackable)
            {
                //Ha a tárgy egymásba pakolható
                Item itemsInStack = Items.FirstOrDefault(x => x.item_id == itemLoot.item_id);
                if (itemsInStack != null)
                {
                    itemsInStack.item_quantity += itemLoot.item_quantity;
                }
                else
                {
                    if (Items.Count != 28)
                    {
                        foreach (Button item in wp_inventory.Children)
                        {

                            if (item.Tag == null)
                            {
                                int index = wp_inventory.Children.IndexOf(item);
                                itemLoot.item_inventorySlot = index;
                                Items.Insert(index, itemLoot);
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Azinventorimassz full");
                    }
                }
            }
            else
            {
                //Ha a tárgy nem egymásba pakolható
                if (Items.Count != 28)
                {
                    foreach (Button item in wp_inventory.Children)
                    {
                        if (item.Tag == null)
                        {
                            int index = wp_inventory.Children.IndexOf(item);
                            itemLoot.item_inventorySlot = index;
                            Items.Insert(index, itemLoot);
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Azinventorimassz full");
                }
            }
            //DatabaseContorller.InsertNewRow(itemLoot);
            ItemsLoot.Remove(itemLoot);
            CreateWrapPanelItems(EnumWrapPanels.Loot);
            CreateWrapPanelItems(EnumWrapPanels.Inventory);
        }
    }
}
