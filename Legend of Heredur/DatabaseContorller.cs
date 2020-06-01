using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows;
using System.Reflection;
using System.IO;

namespace Legend_of_Heredur
{
    public static class DatabaseContorller
    {
        /// <summary>
        /// Létrehozza az adatbáziskapcsolatot
        /// </summary>
        /// <returns></returns>
        private static OleDbCommand Connect()
        {
            string debugFolder = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(System.IO.Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location), "");
            debugFolder = debugFolder.Replace(" ItemCreator", "");
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + debugFolder + "HeroDB.accdb;";
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            return connection.CreateCommand();
        }

        public static List<ItemGenerator> GetAllData()
        {            
            OleDbCommand command = Connect();
            command.CommandText = "SELECT * FROM [ItemGenerator]";
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            string debugFolder = System.AppDomain.CurrentDomain.BaseDirectory;
            List<ItemGenerator> ItemGenerators = new List<ItemGenerator>();

            

            foreach (DataRow row in dt.Rows)
            {
                ItemGenerator itemGenerator = new ItemGenerator();


                itemGenerator.item_id = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(true)]);
                itemGenerator.item_name = row.ItemArray[StaticHelper.IndexLine(false)].ToString();
                itemGenerator.item_image = debugFolder + "Images\\" + row.ItemArray[StaticHelper.IndexLine(false)].ToString();
                itemGenerator.item_type = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());

                itemGenerator.item_rarity = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_potionEffect = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_potionEffectValue = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_potionEffectDuration = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_level = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_requiedHeroLevel = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_quantityFrom = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_quantityTo = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_isStackable = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_qualityFrom = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_qualityTo = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)].ToString());
                itemGenerator.item_primaryStat_damageMax_randomMin = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_primaryStat_damageMax_randomMax = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_primaryStat_defenseMax_randomMin = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_primaryStat_defenseMax_randomMax = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);

                int bonusIndex = 19;
                itemGenerator.item_bonuses = new List<ItemBonus>();
                for (int i = 0; i < 22; i++)
                {
                    ItemBonus itemBonus = new ItemBonus();
                    itemBonus.item_bonusStatName = (EnumPossibleBonusStats)i;
                    itemBonus.item_bonus_min = Convert.ToInt32(row.ItemArray[bonusIndex]);
                    bonusIndex++;
                    itemBonus.item_bonus_max = Convert.ToInt32(row.ItemArray[bonusIndex]);
                    bonusIndex++;
                    itemGenerator.item_bonuses.Add(itemBonus);
                }
                




               
                



                ItemGenerators.Add(itemGenerator);
            }

            adapter.Dispose();
            command.Connection.Close();
                    
            
            return ItemGenerators;
        }


        public static void InsertNewRow(Item Item)
        {
            OleDbCommand command = Connect();
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO [Items](");
            for (int i = 1; i < typeof(Item).GetProperties().Length - 1; i++)
            {
                query.Append(typeof(Item).GetProperties()[i].Name + ",");
            }
            query.Append(typeof(Item).GetProperties().Last().Name);

            query.Append(") VALUES(");

            


            foreach (PropertyInfo prop in Item.GetType().GetProperties())
            {
                //Kihagyja az első propertit az ID-t
                if (Item.GetType().GetProperties().ToList().IndexOf(prop) == 0)
                {
                    continue;
                }

                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                switch (type.Name)
                {
                    case "Boolean":

                        if (bool.Parse(prop.GetValue(Item, null).ToString()))
                        {
                            query.Append("'1', ");
                        }
                        else
                        {
                            query.Append("'0', ");
                        }
                        break;
                    case "EnumQuality":
                        EnumQuality enumQuality = (EnumQuality)Enum.Parse(typeof(EnumQuality), prop.GetValue(Item, null).ToString());
                        query.Append("'" + Array.IndexOf(EnumQuality.GetValues(enumQuality.GetType()), enumQuality) + "', ");
                        break;
                    case "EnumItemType":
                        EnumItemType enumItemType = (EnumItemType)Enum.Parse(typeof(EnumItemType), prop.GetValue(Item, null).ToString());
                        query.Append("'" + Array.IndexOf(EnumItemType.GetValues(enumItemType.GetType()), enumItemType) + "', ");
                        break;
                    case "EnumRarity":
                        EnumRarity enumRarity = (EnumRarity)Enum.Parse(typeof(EnumRarity), prop.GetValue(Item, null).ToString());
                        query.Append("'" + Array.IndexOf(EnumRarity.GetValues(enumRarity.GetType()), enumRarity) + "', ");
                        break;
                    case "EnumPotionEffect":
                        EnumPotionEffect enumPotionEffect = (EnumPotionEffect)Enum.Parse(typeof(EnumPotionEffect), prop.GetValue(Item, null).ToString());
                        query.Append("'" + Array.IndexOf(EnumPotionEffect.GetValues(enumPotionEffect.GetType()), enumPotionEffect) + "', ");
                        break;
                    case "EnumPossibleBonusStats":
                        EnumPossibleBonusStats enumPossibleBonusStats = (EnumPossibleBonusStats)Enum.Parse(typeof(EnumPossibleBonusStats), prop.GetValue(Item, null).ToString());
                        query.Append("'" + Array.IndexOf(EnumPossibleBonusStats.GetValues(enumPossibleBonusStats.GetType()), enumPossibleBonusStats) + "', ");
                        break;
                    default:
                        //Az utolsót külön írja meg mert az utolsónál már le kell zárni a queryt ') -el
                        if (Item.GetType().GetProperties().ToList().IndexOf(prop) == Item.GetType().GetProperties().ToList().IndexOf(Item.GetType().GetProperties().ToList().Last()))
                        {
                            query.Append("'" + prop.GetValue(Item, null).ToString() + "')");
                        }
                        else
                        { 
                            query.Append("'" + prop.GetValue(Item, null).ToString() + "', ");
                        }
                        break;
                }

                
            }

            command.CommandText = query.ToString();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }
    

        //public static void UpdateRowAtId(string id, object inputObject)
        //{
        //    OleDbCommand command = Connect();
        //    StringBuilder query = new StringBuilder();

        //    switch (inputObject.GetType().Name)
        //    {               
        //        case "ColorSettings":
        //            ColorSettings colorSettings = (ColorSettings)inputObject;
        //            query.Append("UPDATE [Colors] SET ");

        //            query.Append("[" + typeof(ColorSettings).GetProperties()[1].Name + "] = " + "'" + colorSettings.color_name + "', ");
        //            query.Append("[" + typeof(ColorSettings).GetProperties()[2].Name + "] = " + "'" + colorSettings.color_window_background + "', ");
        //            query.Append("[" + typeof(ColorSettings).GetProperties()[3].Name + "] = " + "'" + colorSettings.color_subWindow_background + "', ");
        //            query.Append("[" + typeof(ColorSettings).GetProperties()[4].Name + "] = " + "'" + colorSettings.color_buttons_background + "', ");
        //            query.Append("[" + typeof(ColorSettings).GetProperties()[5].Name + "] = " + "'" + colorSettings.color_foreground + "', ");
        //            query.Append("[" + typeof(ColorSettings).GetProperties()[6].Name + "] = " + "'" + ((bool)colorSettings.color_isActive ? 1 : 0) + "'");

        //            query.Append(" WHERE color_id = " + id);

        //            command.CommandText = query.ToString();
        //            command.ExecuteNonQuery();
        //            command.Connection.Close();
        //            break;
        //    }

        //    command.Connection.Close();
        //}


        //public static void DeleteRow(object inputObject)
        //{
        //    OleDbCommand command = Connect();
        //    switch (inputObject.GetType().Name)
        //    {
        //        case "ChildCustom":
        //            ChildCustom childCustom = (ChildCustom)inputObject;
        //            command.CommandText = "DELETE FROM [Children] WHERE child_id = " + childCustom.child_id;
        //            break;
        //        case "Child":
        //            Child child = (Child)inputObject;
        //            command.CommandText = "DELETE FROM [Children] WHERE child_id = " + child.child_id;
        //            break;
        //        case "Group":
        //            Group group = (Group)inputObject;
        //            command.CommandText = "DELETE FROM [Group] WHERE group_id = " + group.group_id;
        //            break;
        //        case "Pattern":
        //            Pattern pattern = (Pattern)inputObject;
        //            command.CommandText = "DELETE FROM [Pattern] WHERE pattern_id = " + pattern.pattern_id;
        //            break;
        //        case "ColorSettings":
        //            ColorSettings colorSettings = (ColorSettings)inputObject;
        //            command.CommandText = "DELETE FROM [Colors] WHERE color_id = " + colorSettings.color_id;
        //            break;
        //    }
        //    command.ExecuteNonQuery();
        //    command.Connection.Close();
        //}


    }
}
