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

namespace Legend_of_Heredur_ItemCreator
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

                //foreach (PropertyInfo prop in itemGenerator.GetType().GetProperties())
                //{

                //    int index = itemGenerator.GetType().GetProperties().ToList().IndexOf(prop);

                //    string rowString = row.ItemArray[index].ToString();

                //    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                //    switch (type.Name)
                //    {
                //        case "Int32": prop.SetValue(itemGenerator,Convert.ToInt32(row.ItemArray[index]));
                //            break;
                //        case "String": prop.SetValue(itemGenerator, row.ItemArray[index].ToString());
                //            break;
                //        case "Boolean": prop.SetValue(itemGenerator,Convert.ToBoolean(row.ItemArray[index]));
                //            break;                     
                //    }  
                //}
                //ItemGenerators.Add(itemGenerator);


                itemGenerator.item_id = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(true)]);
                itemGenerator.item_name = row.ItemArray[StaticHelper.IndexLine(false)].ToString();
                itemGenerator.item_image = debugFolder + "wowicons\\" + row.ItemArray[StaticHelper.IndexLine(false)].ToString();
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
                itemGenerator.item_bonus1_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus1_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus2_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus2_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus3_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus3_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus4_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus4_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus5_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus5_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus6_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus6_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus7_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus7_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus8_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus8_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus9_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus9_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus10_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus10_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus11_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus11_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus12_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus12_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus13_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus13_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus14_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus14_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus15_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus15_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);

                itemGenerator.item_bonus16_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus16_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus17_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus17_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus18_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus18_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus19_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus19_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus20_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus20_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus21_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus21_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus22_min = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);
                itemGenerator.item_bonus22_max = Convert.ToInt32(row.ItemArray[StaticHelper.IndexLine(false)]);

                ItemGenerators.Add(itemGenerator);
            }

            adapter.Dispose();
            command.Connection.Close();
                    
            
            return ItemGenerators;
        }

        public static void InsertNewRow(ItemGenerator itemGenerator)
        {
            OleDbCommand command = Connect();
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO [ItemGenerator](");
            for (int i = 1; i < typeof(ItemGenerator).GetProperties().Length-1; i++)
            {
                query.Append(typeof(ItemGenerator).GetProperties()[i].Name + ",");
            }
            query.Append(typeof(ItemGenerator).GetProperties().Last().Name);

            query.Append(") VALUES(");
            foreach (PropertyInfo prop in itemGenerator.GetType().GetProperties())
            {
                if (itemGenerator.GetType().GetProperties().ToList().IndexOf(prop) == 0)
                {
                    continue;
                }

                if (itemGenerator.GetType().GetProperties().ToList().IndexOf(prop) != itemGenerator.GetType().GetProperties().ToList().IndexOf(itemGenerator.GetType().GetProperties().ToList().Last()))
                {
                    query.Append("'" + prop.GetValue(itemGenerator, null).ToString() + "', ");
                }
                else
                {
                    query.Append("'" + prop.GetValue(itemGenerator, null).ToString() + "')");
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


        public static void DeleteRow(ItemGenerator selecetedItemGenerator)
        {
            OleDbCommand command = Connect();
           

            command.CommandText = "DELETE FROM [ItemGenerator] WHERE item_id = " + selecetedItemGenerator.item_id;
            
            
            command.ExecuteNonQuery();
            command.Connection.Close();
        }


    }
}
