using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Legend_of_Heredur
{
    public static class StaticHelper
    {
        public static string debugFolder = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(System.IO.Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location), "");

        private static int index = -1;

        public static Button ButtonConverter(object input)
        {
            Button button = new Button();
            if (input is Button)
            {
                button = (Button)input;
            }
            return button;
        }

        public static Image ImageConverter(object input)
        {
            Image image = new Image();
            if (input is Image)
            {
                image = (Image)input;
            }
            return image;
        }

        public static Canvas CanvasConverter(object input)
        {
            Canvas canvas = new Canvas();
            if (input is Canvas)
            {
                canvas = (Canvas)input;
            }
            return canvas;
        }

        //A bónusz statot írja ki szépen
        public static string GetBonusStatText(EnumPossibleBonusStats enumPossibleBonusStats, int BonusValue)
        {
            string itemBonusText = string.Empty;
            switch (enumPossibleBonusStats)
            {
                case EnumPossibleBonusStats.Damage:
                    itemBonusText = "+" + BonusValue +" Sebzés";
                    break;
                case EnumPossibleBonusStats.DamagePercent:
                    itemBonusText = "+" + BonusValue + "% Sebzés";
                    break;
                case EnumPossibleBonusStats.CriticalDamage:
                    itemBonusText = "+" + BonusValue + " Kritikus sebzés";
                    break;
                case EnumPossibleBonusStats.CriticalHitChancePercent:
                    itemBonusText = "+" + BonusValue + "% Esély kritikus találatra";
                    break;
                case EnumPossibleBonusStats.Defense:
                    itemBonusText = "+" + BonusValue + " Védelem";
                    break;
                case EnumPossibleBonusStats.DefensePercent:
                    itemBonusText = "+" + BonusValue + "% Védelem";
                    break;
                case EnumPossibleBonusStats.DodgeChancePercent:
                    itemBonusText = "+" + BonusValue + "% Elkerülés";
                    break;
                case EnumPossibleBonusStats.Health:
                    itemBonusText = "+" + BonusValue + " Élet";
                    break;
                case EnumPossibleBonusStats.HealthPercent:
                    itemBonusText = "+" + BonusValue + "% Élet";
                    break;
                case EnumPossibleBonusStats.Strength:
                    itemBonusText = "+" + BonusValue + " Erő";
                    break;
                case EnumPossibleBonusStats.Dexterity:
                    itemBonusText = "+" + BonusValue + " Ügyesség";
                    break;
                case EnumPossibleBonusStats.Vitality:
                    itemBonusText = "+" + BonusValue + " Vitalitás";
                    break;
                case EnumPossibleBonusStats.Luck:
                    itemBonusText = "+" + BonusValue + " Szerencse";
                    break;
                case EnumPossibleBonusStats.LifeSteal:
                    itemBonusText = "+" + BonusValue + "% Életlopás";
                    break;
                case EnumPossibleBonusStats.ReflectDamage:
                    itemBonusText = "+" + BonusValue + "% Visszasebzés";
                    break;
                case EnumPossibleBonusStats.Mana:
                    itemBonusText = "+" + BonusValue + " Mana";
                    break;
                case EnumPossibleBonusStats.ManaPercent:
                    itemBonusText = "+" + BonusValue + "% Mana";
                    break;
                case EnumPossibleBonusStats.ManaRegeneration:
                    itemBonusText = "+" + BonusValue + " Manaregeráció";
                    break;
                case EnumPossibleBonusStats.HealthRegeneration:
                    itemBonusText = "+" + BonusValue + " Életregeráció";
                    break;
                case EnumPossibleBonusStats.Intelligence:
                    itemBonusText = "+" + BonusValue + " Intelligencia";
                    break;
                case EnumPossibleBonusStats.SpellPower:
                    itemBonusText = "+" + BonusValue + " Varázserő";
                    break;
                case EnumPossibleBonusStats.SpellPowerPercent:
                    itemBonusText = "+" + BonusValue + "% Varázserő";
                    break;
            }
            return itemBonusText;
        }

        //Az ital statot írja ki szépen
        public static string GetPotionStatText(EnumPotionEffect potionEffect, int effectValue, int effectDuration) {
            string itemPotionText = string.Empty;
            switch (potionEffect)
            {
                case EnumPotionEffect.Health:
                    itemPotionText = "Élet töltés: " + effectValue + ((effectDuration > 0) ?", "+ effectDuration+" körig" : "");
                    break;
                case EnumPotionEffect.Mana:
                    itemPotionText = "Mana töltés: " + effectValue + ((effectDuration > 0) ? ", " + effectDuration + " körig" : "");
                    break;
                case EnumPotionEffect.StrengthPercent:
                    itemPotionText = "Erő: " + effectValue+"% "+ ((effectDuration > 0) ? ", " + effectDuration + " körig" : "");
                    break;
                case EnumPotionEffect.DexterityPercent:
                    itemPotionText = "Ügyesség:" + effectValue +"% "+ ((effectDuration > 0) ? ", " + effectDuration + " körig" : "");
                    break;
                case EnumPotionEffect.DefensePercent:
                    itemPotionText = "Védelem: " + effectValue + "% " + ((effectDuration > 0) ? ", " + effectDuration + " körig" : "");
                    break;
                case EnumPotionEffect.DodgeChancePercent:
                    itemPotionText = "Elkerülés: " + effectValue + "% " + ((effectDuration > 0) ? ", " + effectDuration + " körig" : "");
                    break;
                case EnumPotionEffect.CriticalHitChancePercent:
                    itemPotionText = "Kritikus esély: " + effectValue + "% " + ((effectDuration > 0) ? ", " + effectDuration + " körig" : "");
                    break;
            }
            return itemPotionText;
        }

        //Kisorsol egy ritkasági bónuszt az adott tárgy lehetséges bonuszaiból
        public static int[] RollABonus(ItemGenerator selectedItem)
        {
            
            int[] newRolledBonus = new int[2];

            List<ItemBonus> possibleBonuses = new List<ItemBonus>();

            possibleBonuses = selectedItem.item_bonuses.Where(x => x.item_bonus_max != 0).ToList();

            if (possibleBonuses.Count != 0)
            {
            
                ItemBonus rolledBonus = possibleBonuses[new Random().Next(possibleBonuses.Count)];
               
                newRolledBonus[0] = Convert.ToInt32(rolledBonus.item_bonusStatName);
             
                newRolledBonus[1] = new Random().Next(rolledBonus.item_bonus_min, rolledBonus.item_bonus_max+1);
            }
            return newRolledBonus;
        }

        //Az alap statot változtatja a minőség szorzó alapján
        public static int CalculateStatByQuality(int input, EnumQuality enumQuality)
        {
            double temp = 0;
            switch (enumQuality)
            {
                case EnumQuality.Gyenge:
                    temp = input - (input * 0.25);
                    break;
                case EnumQuality.Normál:
                    temp = input;
                    break;
                case EnumQuality.Jó:
                    temp = input + (input * 0.10);
                    break;
                case EnumQuality.Kiemelkedő:
                    temp = input + (input * 0.25);
                    break;
                case EnumQuality.Tökéletes:
                    temp = input + (input * 0.50);
                    break;
            }
            return Convert.ToInt32(temp);
        }

        //Növeli a visszaadott számot minden sorban
        public static int IndexLine(bool reset)
        {
            if (reset)
            {
                index = 0;
                return 0;
            }

            index++;
            return index;
        }



        
        //Át konvertálja a kis képet kurzorrá
        public static Cursor CreateCursor(BitmapSource bitmapSource, int hotspotX, int hotspotY)
        {
            using (var ms1 = new MemoryStream())
            {
                var pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                pngEncoder.Save(ms1);

                var pngBytes = ms1.ToArray();
                var size = pngBytes.GetLength(0);

                using (var ms = new MemoryStream())
                {
                    //Reserved must be zero; 2 bytes
                    ms.Write(BitConverter.GetBytes((short)0), 0, 2);

                    //image type 1 = ico 2 = cur; 2 bytes
                    ms.Write(BitConverter.GetBytes((short)2), 0, 2);

                    //number of images; 2 bytes
                    ms.Write(BitConverter.GetBytes((short)1), 0, 2);

                    //image width in pixels
                    ms.WriteByte(32);

                    //image height in pixels
                    ms.WriteByte(32);

                    //Number of Colors in the color palette. Should be 0 if the image doesn't use a color palette
                    ms.WriteByte(0);

                    //reserved must be 0
                    ms.WriteByte(0);

                    //2 bytes. In CUR format: Specifies the horizontal coordinates of the hotspot in number of pixels from the left.
                    ms.Write(BitConverter.GetBytes((short)hotspotX), 0, 2);
                    //2 bytes. In CUR format: Specifies the vertical coordinates of the hotspot in number of pixels from the top.
                    ms.Write(BitConverter.GetBytes((short)hotspotY), 0, 2);

                    //Specifies the size of the image's data in bytes
                    ms.Write(BitConverter.GetBytes(size), 0, 4);

                    //Specifies the offset of BMP or PNG data from the beginning of the ICO/CUR file
                    ms.Write(BitConverter.GetBytes(22), 0, 4);

                    ms.Write(pngBytes, 0, size); //write the png data.
                    ms.Seek(0, SeekOrigin.Begin);
                    return new Cursor(ms);
                }
            }
        }

        public static Button SetButtonColors(Button thisButton)
        {
            TextBlock textBlock = new TextBlock();

            ContentControl contentControl = new ContentControl();
            DataTemplate dataTemplate = new DataTemplate();
            dataTemplate.DataType = typeof(TextBlock);
            //contentControl.ContentTemplate = (DataTemplate)textBlock;

            thisButton.Content = contentControl;
                
            //Style style = new Style(typeof(Button));
            //style.Setters.Add(new Setter() { Property = Button.BackgroundProperty, Value = Brushes.Green });

            //ControlTemplate controlTemplate = new ControlTemplate() { TargetType = typeof(Button) };

            //Setter templateSetter = new Setter() { Property = Button.TemplateProperty, Value = controlTemplate };

            ////Border border = new Border() { Background = typeof(Te)};

            ////controlTemplate.VisualTree.SetValue = border;

            //Setter setter = new Setter() { Property = Button.TemplateProperty, Value = controlTemplate };


            ////Setter setter2 = new Setter() 






            //Trigger trigger = new Trigger() { Property = Button.IsMouseOverProperty, Value = true };
            //trigger.Setters.Add(new Setter() { Property = Button.BackgroundProperty, Value = Brushes.Goldenrod });

            //style.Triggers.Add(trigger);


            return thisButton;
        }
    }
}
