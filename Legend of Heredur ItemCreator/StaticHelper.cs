using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Legend_of_Heredur_ItemCreator
{
    public static class StaticHelper
    {
        private static int index = 0;
        public static Image ImageConverter(object input)
        {
            Image image = new Image();
            if (input is Image)
            {
                image = (Image)input;
            }
            return image;
        }

        public static ComboBox ComboBoxConverter(object input)
        {
            ComboBox comboBox = new ComboBox();
            if (input is ComboBox)
            {
                comboBox = (ComboBox)input;
            }
            return comboBox;
        }
        public static CheckBox CheckBoxConverter(object input)
        {
            CheckBox checkBox = new CheckBox();
            if (input is CheckBox)
            {
                checkBox = (CheckBox)input;
            }
            return checkBox;
        }

        public static StackPanel StackPanelConverter(object input)
        {
            StackPanel stackPanel = new StackPanel();
            if (input is StackPanel)
            {
                stackPanel = (StackPanel)input;
            }
            return stackPanel;
        }

        public static TextBox TextBoxConverter(object input)
        {
            TextBox textBox = new TextBox();
            if (input is TextBox)
            {
                textBox = (TextBox)input;
            }
            return textBox;
        }

        public static int ifStringEmpty(string input)
        {
            int number = 0;
            if (input != string.Empty)
            {
                number = Convert.ToInt32(input);
            }
            return number;
        }

        public static DataGrid DataGridFormat(DataGrid dataGrid, List<ItemGenerator> ItemGenerators, bool enableBonuses)
        {
            foreach (var column in dataGrid.Columns)
            {
                //Beszínezi a nevet a rarity alapján
                if (column.Header.ToString() == "Név" && column is DataGridTextColumn)
                {
                    DataGridTextColumn textColumn = (DataGridTextColumn)column;
                    Style style = new Style(typeof(TextBlock));

                    foreach (ItemGenerator item in ItemGenerators)
                    {
                        var bc = new BrushConverter();
                        Trigger groupStyle = new Trigger() { Property = TextBlock.TextProperty, Value = item.item_name };
                        switch (item.item_rarity)
                        {
                            case 0:
                                groupStyle.Setters.Add(new Setter() { Property = TextBlock.BackgroundProperty, Value = Brushes.Transparent });
                                break;
                            case 1:
                                groupStyle.Setters.Add(new Setter() { Property = TextBlock.BackgroundProperty, Value = Brushes.Green });
                                break;
                            case 2:
                                groupStyle.Setters.Add(new Setter() { Property = TextBlock.BackgroundProperty, Value = Brushes.Blue });
                                break;
                            case 3:
                                groupStyle.Setters.Add(new Setter() { Property = TextBlock.BackgroundProperty, Value = Brushes.Gold });
                                break;
                        }



                        style.Triggers.Add(groupStyle);
                    }

                    textColumn.ElementStyle = style;
                }

                if ((column.Header.ToString() == "S-" ||
                    column.Header.ToString() == "S+" ||
                    column.Header.ToString() == "V-" ||
                    column.Header.ToString() == "V+" ||
                    column.Header.ToString() == "I" ||
                    column.Header.ToString() == "Ii" ||
                    column.Header.ToString() == "B1+" ||
                    column.Header.ToString() == "B1-" ||
                    column.Header.ToString() == "B2-" ||
                    column.Header.ToString() == "B2+" ||
                    column.Header.ToString() == "B3+" ||
                    column.Header.ToString() == "B3-" ||
                    column.Header.ToString() == "B4-" ||
                    column.Header.ToString() == "B4+" ||
                    column.Header.ToString() == "B5-" ||
                    column.Header.ToString() == "B5+" ||
                    column.Header.ToString() == "B6-" ||
                    column.Header.ToString() == "B6+" ||
                    column.Header.ToString() == "B7-" ||
                    column.Header.ToString() == "B7+" ||
                    column.Header.ToString() == "B8-" ||
                    column.Header.ToString() == "B8+" ||
                    column.Header.ToString() == "B9-" ||
                    column.Header.ToString() == "B9+"
                    ) && column is DataGridTextColumn)
                {
                    DataGridTextColumn textColumn = (DataGridTextColumn)column;
                    Style style = new Style(typeof(TextBlock));

                    foreach (ItemGenerator item in ItemGenerators)
                    {
                        var bc = new BrushConverter();
                        Trigger groupStyle = new Trigger() { Property = TextBlock.TextProperty, Value = "0" };

                        groupStyle.Setters.Add(new Setter() { Property = TextBlock.ForegroundProperty, Value = Brushes.Transparent });


                        style.Triggers.Add(groupStyle);
                    }

                    textColumn.ElementStyle = style;
                }

                if (enableBonuses)
                {
                    if ((
                   column.Header.ToString() == "B1+" ||
                   column.Header.ToString() == "B1-" ||
                   column.Header.ToString() == "B2-" ||
                   column.Header.ToString() == "B2+" ||
                   column.Header.ToString() == "B3+" ||
                   column.Header.ToString() == "B3-" ||
                   column.Header.ToString() == "B4-" ||
                   column.Header.ToString() == "B4+" ||
                   column.Header.ToString() == "B5-" ||
                   column.Header.ToString() == "B5+" ||
                   column.Header.ToString() == "B6-" ||
                   column.Header.ToString() == "B6+" ||
                   column.Header.ToString() == "B7-" ||
                   column.Header.ToString() == "B7+" ||
                   column.Header.ToString() == "B8-" ||
                   column.Header.ToString() == "B8+" ||
                   column.Header.ToString() == "B9-" ||
                   column.Header.ToString() == "B9+"
                   ) && column is DataGridTextColumn)
                    {
                        column.Visibility = Visibility.Visible;
                    }

                }
                else
                {
                    if ((
                     column.Header.ToString() == "B1+" ||
                     column.Header.ToString() == "B1-" ||
                     column.Header.ToString() == "B2-" ||
                     column.Header.ToString() == "B2+" ||
                     column.Header.ToString() == "B3+" ||
                     column.Header.ToString() == "B3-" ||
                     column.Header.ToString() == "B4-" ||
                     column.Header.ToString() == "B4+" ||
                     column.Header.ToString() == "B5-" ||
                     column.Header.ToString() == "B5+" ||
                     column.Header.ToString() == "B6-" ||
                     column.Header.ToString() == "B6+" ||
                     column.Header.ToString() == "B7-" ||
                     column.Header.ToString() == "B7+" ||
                     column.Header.ToString() == "B8-" ||
                     column.Header.ToString() == "B8+" ||
                     column.Header.ToString() == "B9-" ||
                     column.Header.ToString() == "B9+"
                     ) && column is DataGridTextColumn)
                    {
                        column.Visibility = Visibility.Collapsed;
                    }
                   
                }

            }
            return dataGrid;
        }

        public static int CalculateQualityValue(int input, EnumQuality enumQuality)
        {
            double temp = 0;
            switch (enumQuality)
            {
                case EnumQuality.Gyenge:
                    temp = input - (input * 0.25);
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
    }
}
