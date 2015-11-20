using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace PF.Common.Code
{
    /*Description
    -----------------
    General Class.
     * This class holds code that is used in more than one place within this namespace.

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public class General
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static DataSet ds_Get_Max_ID;
        private static DataSet ds_system;
        private static DataRow dr_system;

        public static int Delete_Record(string str_table, string Data, string msg_value)
        {
            DialogResult DLR_Message = new DialogResult();
            int int_result = 0;
            string str_msg;

            string[] DataParts = Data.Split(':');
            int int_Delete_Id = 0;
            int int_Current_User_Id = 0;
            try
            {
                int_Delete_Id = Convert.ToInt32(DataParts[0]);
                int_Current_User_Id = Convert.ToInt32(DataParts[1]);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }

            str_msg = "You are about to delete " + Environment.NewLine;
            str_msg = str_msg + msg_value + Environment.NewLine;
            str_msg = str_msg + "Do you want to continue?";
            DLR_Message = MessageBox.Show(str_msg, "Common - Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (DLR_Message == DialogResult.Yes)
            {
                switch (str_table)
                {
                    #region ------------- Common -------------

                    case "CM_Brand":
                        int_result = PF.Data.AccessLayer.CM_Brand.Delete(int_Delete_Id);
                        break;

                    case "CM_Cities":
                        int_result = PF.Data.AccessLayer.CM_Cities.Delete(int_Delete_Id);
                        break;

                    case "CM_Count":
                        int_result = PF.Data.AccessLayer.CM_Count.Delete(int_Delete_Id);
                        break;

                    case "CM_Defect":
                        int_result = PF.Data.AccessLayer.CM_Defect.Delete(int_Delete_Id);
                        break;

                    case "CM_Defect_Class":
                        int_result = PF.Data.AccessLayer.CM_Defect_Class.Delete(int_Delete_Id);
                        break;

                    case "CM_ESP":
                        int_result = PF.Data.AccessLayer.CM_ESP.Delete(int_Delete_Id);
                        break;

                    case "CM_Fruit_Type":
                        int_result = PF.Data.AccessLayer.CM_Fruit_Type.Delete(int_Delete_Id);
                        break;

                    case "CM_Grade":
                        int_result = PF.Data.AccessLayer.CM_Grade.Delete(int_Delete_Id);
                        break;

                    case "CM_Growing_Method":
                        int_result = PF.Data.AccessLayer.CM_Growing_Method.Delete(int_Delete_Id);
                        break;

                    case "CM_Location":
                        int_result = PF.Data.AccessLayer.CM_Location.Delete(int_Delete_Id);
                        break;

                    case "CM_Market_Attribute":
                        int_result = PF.Data.AccessLayer.CM_Market_Attribute.Delete(int_Delete_Id);
                        break;

                    case "CM_Pack_Type":
                        int_result = PF.Data.AccessLayer.CM_Pack_Type.Delete(int_Delete_Id);
                        break;

                    case "CM_Pallet_Type":
                        int_result = PF.Data.AccessLayer.CM_Pallet_Type.Delete(int_Delete_Id);
                        break;

                    case "CM_Product_Group":
                        int_result = PF.Data.AccessLayer.CM_Product_Group.Delete(int_Delete_Id);
                        break;

                    case "CM_Size":
                        int_result = PF.Data.AccessLayer.CM_Size.Delete(int_Delete_Id);
                        break;

                    case "CM_Storage":
                        int_result = PF.Data.AccessLayer.CM_Storage.Delete(int_Delete_Id);
                        break;

                    case "CM_Treatment":
                        int_result = PF.Data.AccessLayer.CM_Treatment.Delete(int_Delete_Id);
                        break;

                    case "CM_Truck":
                        int_result = PF.Data.AccessLayer.CM_Truck.Delete(int_Delete_Id);
                        break;

                    #endregion ------------- Common -------------

                    #region ------------- Process Factory -------------

                    case "PF_Other_Work":
                        int_result = PF.Data.AccessLayer.PF_Other_Work.Delete(int_Delete_Id);
                        break;

                    case "PF_Other_Work_Types":
                        int_result = PF.Data.AccessLayer.PF_Other_Work_Types.Delete(int_Delete_Id);
                        break;

                    case "PF_Orders":
                        int_result = PF.Data.AccessLayer.PF_Orders_Staff_Relationship.Delete_Order(int_Delete_Id);
                        int_result = int_result + PF.Data.AccessLayer.PF_Orders_Pallets.Delete(int_Delete_Id);
                        int_result = int_result + PF.Data.AccessLayer.PF_Orders.Delete(int_Delete_Id);
                        break;

                    case "PF_Orders_Intended":
                        int_result = PF.Data.AccessLayer.PF_Orders_Intended.Delete(int_Delete_Id);
                        break;

                    case "PF_Pallet":
                        int_result = PF.Data.AccessLayer.PF_Pallet_Details.Delete_Pallet(int_Delete_Id);
                        int_result = int_result + PF.Data.AccessLayer.PF_Pallet.Delete(int_Delete_Id);
                        break;

                    case "PF_Pallet_Details":
                        int_result = PF.Data.AccessLayer.PF_Pallet_Details.Delete_Pallet(int_Delete_Id);
                        break;

                    case "PF_Stock_Holding":
                        int_result = PF.Data.AccessLayer.PF_Stock_Holding.Delete(int_Delete_Id);
                        break;

                    case "PF_Stock_Item":
                        int_result = PF.Data.AccessLayer.PF_Stock_Item.Delete(int_Delete_Id);
                        break;

                    #endregion ------------- Process Factory -------------

                    #region ------------- Process Factory  Accounting-------------

                    case "PF_A_Customer_Sales_Discount":
                        int_result = PF.Data.AccessLayer.PF_A_Customer_Sales_Discount.Delete(int_Delete_Id);
                        break;

                    case "PF_A_Customer_Sales_Rates":
                        int_result = PF.Data.AccessLayer.PF_A_Customer_Sales_Rates.Delete(int_Delete_Id);
                        break;

                    case "PF_A_Fruit_Purchase":
                        int_result = PF.Data.AccessLayer.GH_Submission.Reset_Purchase(int_Delete_Id, int_Current_User_Id);
                        int_result = int_result + PF.Data.AccessLayer.PF_A_Fruit_Purchase.Delete(int_Delete_Id);
                        break;

                    case "PF_A_Invoice":
                        int_result = PF.Data.AccessLayer.PF_Orders.Remove_From_Invoiced(int_Delete_Id, int_Current_User_Id);
                        int_result = int_result + PF.Data.AccessLayer.PF_A_Invoice_Details.Delete(int_Delete_Id);
                        int_result = int_result + PF.Data.AccessLayer.PF_A_Invoice.Delete(int_Delete_Id);
                        break;

                    case "PF_A_Rates":
                        int_result = PF.Data.AccessLayer.CM_Material_Rates_Relationship.Delete_Rates(int_Delete_Id);
                        int_result = int_result + PF.Data.AccessLayer.PF_A_Rates.Delete(int_Delete_Id);
                        break;

                        #endregion ------------- Process Factory  Accounting-------------
                }
            }
            return int_result;
        }

        public static int int_max_user_id(string dsname)
        {
            int int_Season = 0;
            switch (dsname)
            {
                #region ------------- Common -------------

                case "CM_Bins":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Bins.Get_Max_ID();
                    break;

                case "CM_Block":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Block.Get_Max_ID();
                    break;

                case "CM_Block_Variety_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Block_Variety_Relationship.Get_Max_ID();
                    break;

                case "CM_Brand":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Brand.Get_Max_ID();
                    break;

                case "CM_Cities":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Cities.Get_Max_ID();
                    break;

                case "CM_Count":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Count.Get_Max_ID();
                    break;

                case "CM_Defect":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Defect.Get_Max_ID();
                    break;

                case "CM_Defect_Class":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Defect_Class.Get_Max_ID();
                    break;

                case "CM_Defect_Defect_Class_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Defect_Defect_Class_Relationship.Get_Max_ID();
                    break;

                case "CM_ESP":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_ESP.Get_Max_ID();
                    break;

                case "CM_Fruit_Type":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Fruit_Type.Get_Max_ID();
                    break;

                case "CM_Fruit_Variety":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Fruit_Variety.Get_Max_ID();
                    break;

                case "CM_Grade":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Grade.Get_Max_ID();
                    break;

                case "CM_Grower":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Grower.Get_Max_ID();
                    break;

                case "CM_Growing_Method":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Growing_Method.Get_Max_ID();
                    break;

                case "CM_Location":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Location.Get_Max_ID();
                    break;

                case "CM_Market_Attribute":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Market_Attribute.Get_Max_ID();
                    break;

                case "CM_Material":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Material.Get_Max_ID();
                    break;

                case "CM_Material_Rates_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Material_Rates_Relationship.Get_Max_ID();
                    break;

                case "CM_Material_ST_Product_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Material_ST_Product_Relationship.Get_Max_ID();
                    break;

                case "CM_Orchardist":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Orchardist.Get_Max_ID();
                    break;

                case "CM_Pack_Type":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Pack_Type.Get_Max_ID();
                    break;

                case "CM_Pack_Type_Weight":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Pack_Type_Weight.Get_Max_ID();
                    break;

                case "CM_Pallet_Type":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Pallet_Type.Get_Max_ID();
                    break;

                case "CM_Product_Group":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Product_Group.Get_Max_ID();
                    break;

                case "CM_Product_Product_Group_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Product_Product_Group_Relationship.Get_Max_ID();
                    break;

                case "CM_Size":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Size.Get_Max_ID();
                    break;

                case "CM_Storage":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Storage.Get_Max_ID();
                    break;

                case "CM_Trader":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Trader.Get_Max_ID();
                    break;

                case "CM_Treatment":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Treatment.Get_Max_ID();
                    break;

                case "CM_Truck":
                    ds_Get_Max_ID = PF.Data.AccessLayer.CM_Truck.Get_Max_ID();
                    break;

                #endregion ------------- Common -------------

                #region ------------- Process Factory -------------

                case "PF_BaseLoad":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_BaseLoad.Get_Max_ID();
                    break;

                case "PF_Batch_Chemical":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Batch_Chemical.Get_Max_ID();
                    break;

                case "PF_Batch_Test":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Batch_Test.Get_Max_ID();
                    break;

                case "PF_Chemicals":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Chemicals.Get_Max_ID();
                    break;

                case "PF_Cleaning_Area":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Cleaning_Area.Get_Max_ID();
                    break;

                case "PF_Cleaning_Area_Completed":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Cleaning_Area_Completed.Get_Max_ID();
                    break;

                case "PF_Clean_Area_Area_Parts_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Clean_Area_Area_Parts_Relationship.Get_Max_ID();
                    break;

                case "PF_Cleaning_Area_Parts":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Cleaning_Area_Parts.Get_Max_ID();
                    break;

                case "PF_Cleaning_Area_Parts_Completed":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Cleaning_Area_Parts_Completed.Get_Max_ID();
                    break;

                case "PF_Customer":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Customer.Get_Max_ID();
                    break;

                case "PF_Defect_Results":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Defect_Results.Get_Max_ID();
                    break;

                case "PF_Orders_Intended":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Orders_Intended.Get_Max_ID();
                    break;

                case "PF_Other_Work":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Other_Work.Get_Max_ID();
                    break;

                case "PF_Other_Work_Types":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Other_Work_Types.Get_Max_ID();
                    break;

                case "PF_Orders":
                    ds_system = PF.Data.AccessLayer.CM_System.Get_Info_For_Code("Season");
                    for (int i = 0; i < ds_system.Tables[0].Rows.Count; i++)
                    {
                        dr_system = ds_system.Tables[0].Rows[i];
                        int_Season = Convert.ToInt32(dr_system["Value"].ToString());
                    }
                    ds_system.Dispose();
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Orders.Get_Max_ID();
                    break;

                case "PF_Orders_Pallets":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Orders_Pallets.Get_Max_ID();
                    break;

                case "PF_Orders_Staff_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Orders_Staff_Relationship.Get_Max_ID();
                    break;

                case "PF_Pallet":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Pallet.Get_Max_ID();
                    break;

                case "PF_Pallet_Details":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Pallet_Details.Get_Max_ID();
                    break;

                case "PF_Process_Setup":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Process_Setup.Get_Max_ID();
                    break;

                case "PF_PreSystem_Pallet_Stock":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_PreSystem_Pallet_Stock.Get_Max_ID();
                    break;

                case "PF_Process_Setup_Work_Order_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Process_Setup_Work_Order_Relationship.Get_Max_ID();
                    break;

                case "PF_Product":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Product.Get_Max_ID();
                    break;

                case "PF_Product_Cleaning_Area_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Product_Cleaning_Area_Relationship.Get_Max_ID();
                    break;

                case "PF_Product_Chemicals_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Product_Chemicals_Relationship.Get_Max_ID();
                    break;

                case "PF_Product_LabResults":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Product_LabResults.Get_Max_ID();
                    break;

                case "PF_Product_Other":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Product_Other.Get_Max_ID();
                    break;

                case "PF_Product_Tests_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Product_Tests_Relationship.Get_Max_ID();
                    break;

                case "PF_Reports":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Reports.Get_Max_ID();
                    break;

                case "PF_Staff":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Staff.Get_Max_ID();
                    break;

                case "PF_Tests":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Tests.Get_Max_ID();
                    break;

                case "PF_Work_Order":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Work_Order.Get_Max_ID();
                    break;

                case "PF_Work_Order_Material_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Work_Order_Material_Relationship.Get_Max_ID();
                    break;

                case "PF_Work_Order_Staff_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Work_Order_Staff_Relationship.Get_Max_ID();
                    break;

                #endregion ------------- Process Factory -------------

                #region ------------- Process Factory  Accounting-------------

                case "PF_A_Customer_Sales_Discount":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_A_Customer_Sales_Discount.Get_Max_ID();
                    break;

                case "PF_A_Customer_Sales_Rates":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_A_Customer_Sales_Rates.Get_Max_ID();
                    break;

                case "PF_A_Fruit_Purchase":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_A_Fruit_Purchase.Get_Max_ID();
                    break;

                case "PF_A_Invoice":
                    ds_system = PF.Data.AccessLayer.CM_System.Get_Info_For_Code("Season");
                    for (int i = 0; i < ds_system.Tables[0].Rows.Count; i++)
                    {
                        dr_system = ds_system.Tables[0].Rows[i];
                        int_Season = Convert.ToInt32(dr_system["Value"].ToString());
                    }
                    ds_system.Dispose();
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_A_Invoice.Get_Max_ID();
                    break;

                case "PF_A_Invoice_Details":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_A_Invoice_Details.Get_Max_ID();
                    break;

                case "PF_A_Rates":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_A_Rates.Get_Max_ID();
                    break;

                #endregion ------------- Process Factory  Accounting-------------

                //Process Factory - Stock Control

                #region ------------- Process Factory - Stock Control -------------

                case "PF_Stock_Holding":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Stock_Holding.Get_Max_ID();
                    break;

                case "PF_Stock_Item":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Stock_Item.Get_Max_ID();
                    break;

                case "PF_Stock_Used":
                    ds_Get_Max_ID = PF.Data.AccessLayer.PF_Stock_Used.Get_Max_ID();
                    break;

                #endregion ------------- Process Factory - Stock Control -------------

                //PrintRequest - SY

                #region ------------- Print Request PR -------------

                case "SY_PrintRequest":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SY_PrintRequest.Get_Max_ID();
                    break;

                case "SY_Message":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SY_Message.Get_Max_ID();
                    break;

                #endregion ------------- Print Request PR -------------

                //GateHouse

                #region ------------- GateHouse -------------

                case "GH_Submission":
                    ds_Get_Max_ID = PF.Data.AccessLayer.GH_Submission.Get_Max_ID();
                    break;

                #endregion ------------- GateHouse -------------

                //Security

                #region ------------- Security -------------

                case "SC_Install":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_Install.Get_Max_ID();
                    break;

                case "SC_User":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_User.Get_Max_ID();
                    break;

                case "SC_User_Groups":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_User_Groups.Get_Max_ID();
                    break;

                case "SC_User_Group_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_User_Group_Relationship.Get_Max_ID();
                    break;

                case "SC_Menu":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_Menu.Get_Max_ID();
                    break;

                case "SC_Menu_Group_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_Menu_Group_Relationship.Get_Max_ID();
                    break;

                case "SC_Menu_Panel":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_Menu_Panel.Get_Max_ID();
                    break;

                case "SC_Menu_Panel_Group_Relationship":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_Menu_Panel_Group_Relationship.Get_Max_ID();
                    break;

                case "SC_Log_Action":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_Log_Action.Get_Max_ID();
                    break;

                case "SC_Log":
                    ds_Get_Max_ID = PF.Data.AccessLayer.SC_Log.Get_Max_ID();
                    break;

                #endregion ------------- Security -------------

                //External

                #region ------------- External -------------

                case "EX_GDI_Bins":
                    ds_Get_Max_ID = PF.Data.AccessLayer.EX_GDI_Bins.Get_Max_ID();
                    break;

                case "EX_GDI_Submission":
                    ds_Get_Max_ID = PF.Data.AccessLayer.EX_GDI_Submission.Get_Max_ID();
                    break;

                    #endregion ------------- External -------------
            }

            DataRow dr_Get_Max_ID;
            int max_user_id = 1;
            for (int i = 0; i < Convert.ToInt32(ds_Get_Max_ID.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_Max_ID = ds_Get_Max_ID.Tables[0].Rows[i];

                try
                {
                    if (int_Season == 0 || int_Season == Convert.ToInt32(dr_Get_Max_ID["Current_Id"].ToString().Substring(0, 4)))
                    {
                        max_user_id = Convert.ToInt32(dr_Get_Max_ID["Current_Id"].ToString()) + 1;
                    }
                    else
                    {
                        max_user_id = Convert.ToInt32(Convert.ToString(int_Season) + "000001");
                    }
                }
                catch
                {
                    max_user_id = Convert.ToInt32(Convert.ToString(int_Season) + "000001");
                }
            }
            ds_Get_Max_ID.Dispose();
            return max_user_id;
        }

        public static string Get_Printer(string str_Size)
        {
            string str_printer = "";
            var printerSettings = new PrinterSettings();

            if (printerSettings.IsDefaultPrinter == true && printerSettings.DefaultPageSettings.PaperSize.Kind.ToString() == str_Size)
            {
                str_printer = printerSettings.PrinterName.ToString();
            }
            else
            {
                System.Windows.Forms.PrintDialog pd = new System.Windows.Forms.PrintDialog();
                pd.ShowDialog();
                str_printer = pd.PrinterSettings.PrinterName.ToString();
            }

            return str_printer;
        }

        public static int Print_Insert(string papersize, string Template, string Outputfile, string Data, int User_Id)
        {
            int int_PrintRequest_Id = PF.Common.Code.General.int_max_user_id("SY_PrintRequest");
            PF.Data.AccessLayer.SY_PrintRequest.Insert(int_PrintRequest_Id, PF.Common.Code.General.Get_Printer(papersize), Template, Outputfile, Data, User_Id);
            return int_PrintRequest_Id;
        }

        public static string Get_Path(string code)
        {
            string path = "";
            DataSet ds = PF.Data.AccessLayer.CM_System.Get_Info_For_Code(code);
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                path = dr["Value"].ToString();
            }
            ds.Dispose();
            return path;
        }

        public static void write_log(int int_User_Id, string str_Action, int int_Current_User_Id)
        {
            try
            {
                DataSet ds_Get_Info = PF.Data.AccessLayer.SC_Log_Action.Get_Info(str_Action);
                DataRow dr_Get_Info;

                int int_Action_id = 0;

                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                    int_Action_id = Convert.ToInt32(dr_Get_Info["Action_Id"].ToString());
                }

                PF.Data.AccessLayer.SC_Log.Insert(General.int_max_user_id("SC_Log"), int_User_Id, int_Action_id, int_Current_User_Id);
                ds_Get_Info.Dispose();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
            }
        }

        private static List<string> lst_filenames = new List<string>();
        private static List<string> lst_paths = new List<string>();

        public static void Delete_After_Printing(string path, string filetype)
        {
            // All region comments I've added for clarity (BN 12/08/2015)

            #region Get the file path

            if (path == "")
            {
                DataSet ds_Get_Info = PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
                DataRow dr_Get_Info;
                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_Get_Info = ds_Get_Info.Tables[0].Rows[i];
                    switch (dr_Get_Info["Code"].ToString())
                    {
                        case "PF-SPath":
                            lst_paths.Add(dr_Get_Info["Value"].ToString());
                            break;

                        case "PF-TPPath":
                            lst_paths.Add(dr_Get_Info["Value"].ToString());
                            break;
                    }
                }
                ds_Get_Info.Dispose();
            }
            else
            {
                lst_paths.Add(path);
            }

            #endregion Get the file path

            #region If the path exists, add everything in there to the filename list, otherwise create the path

            foreach (string str_path in lst_paths)
            {
                if (System.IO.Directory.Exists(str_path))
                {
                    lst_filenames.AddRange(System.IO.Directory.GetFiles(str_path, filetype, System.IO.SearchOption.TopDirectoryOnly));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(str_path);
                }
            }
            lst_paths.Clear();

            #endregion If the path exists, add everything in there to the filename list, otherwise create the path

            #region Perform the deletion - forcibly close Word if unsuccessful and try again

            foreach (string filename in lst_filenames)
            {
                // Crashes here if the file is still open
                try
                {
                    #region Delete each file in the list

                    Console.WriteLine("Trying to delete: " + filename);
                    logger.Log(LogLevel.Info, "Trying to delete: " + filename);

                    File.Delete(filename);
                    // Might need some sort of delay here
                    System.Threading.Thread.Sleep(250); // Milliseconds
                    if (File.Exists(filename))
                    {
                        Console.WriteLine("Delete failed: " + filename);
                        logger.Log(LogLevel.Warn, "Delete failed: " + filename);
                    }
                    else
                    {
                        Console.WriteLine("Delete success: " + filename);
                        logger.Log(LogLevel.Info, "Delete success: " + filename);
                    }

                    #endregion Delete each file in the list
                }
                catch (IOException ioe)
                {
                    logger.Log(LogLevel.Error, ioe.Message + " - " + filename);

                    //MessageBox.Show(ioe.Message, "IOException", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #region Kill the WINWORD process

                    List<Process> procs = Common.Code.FileUtil.WhoIsLocking(filename);

                    if (procs.Count > 0) // System.Diagnostics.Process (WINWORD)
                    {
                        Console.WriteLine("Attempting to kill: " + procs[0].ProcessName);
                        logger.Log(LogLevel.Info, "Attempting to kill: " + procs[0].ProcessName);

                        procs[0].Kill();
                        procs[0].WaitForExit();

                        Console.WriteLine("Retrying to delete: " + filename);
                        logger.Log(LogLevel.Info, "Retrying to delete: " + filename);

                        File.Delete(filename);  // Retry the delete - If no exception was thrown, then it deleted ok

                        // Might need some sort of delay here
                        System.Threading.Thread.Sleep(250); // Milliseconds
                        if (File.Exists(filename))
                        {
                            Console.WriteLine("Delete failed: " + filename);
                            logger.Log(LogLevel.Warn, "Delete failed: " + filename);
                        }
                        else
                        {
                            Console.WriteLine("Delete success: " + filename);
                            logger.Log(LogLevel.Info, "Delete success: " + filename);
                        }
                    }

                    #endregion Kill the WINWORD process
                }
            }
            lst_filenames.Clear();

            #endregion Perform the deletion - forcibly close Word if unsuccessful and try again
        }

        /// <summary>
        /// Uses ProcessStartInfo to open up a docx or pdf (BN)
        /// </summary>
        /// <param name="str_report_name"></param>
        public static void Report_Viewer(string str_report_name)
        {
            logger.Log(LogLevel.Info, "Report Viewer: " + str_report_name);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = str_report_name;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            Process p = Process.Start(startInfo);
        }

        public static decimal Material_Weight(int int_material_id)
        {
            DataSet ds_weight = PF.Data.AccessLayer.CM_Material.Get_Info(int_material_id);
            DataRow dr_weight;
            decimal dec_material_weight = 0;

            for (int k = 0; k < ds_weight.Tables[0].Rows.Count; k++)
            {
                dr_weight = ds_weight.Tables[0].Rows[k];
                dec_material_weight = Convert.ToDecimal(dr_weight["Weight"].ToString());
            }
            ds_weight.Dispose();

            return dec_material_weight;
        }

        public static decimal Current_Gross_Weight(int int_Pallet_id)
        {
            DataSet ds_current_weight = PF.Data.AccessLayer.PF_Pallet.Get_Info_For_Pallet_Id(int_Pallet_id);
            DataRow dr_current_weight;
            decimal dec_current_gross = 0;
            for (int k = 0; k < ds_current_weight.Tables[0].Rows.Count; k++)
            {
                dr_current_weight = ds_current_weight.Tables[0].Rows[k];
                try
                {
                    dec_current_gross = Convert.ToDecimal(dr_current_weight["Weight_Gross"].ToString());
                }
                catch
                {
                    dec_current_gross = 0;
                }
            }
            ds_current_weight.Dispose();

            return dec_current_gross;
        }

        public static decimal Current_Tare_Weight(int int_Pallet_id)
        {
            DataSet ds_current_weight = PF.Data.AccessLayer.PF_Pallet.Get_Info_For_Pallet_Id(int_Pallet_id);
            DataRow dr_current_weight;
            decimal dec_current_tare = 0;
            for (int k = 0; k < ds_current_weight.Tables[0].Rows.Count; k++)
            {
                dr_current_weight = ds_current_weight.Tables[0].Rows[k];
                try
                {
                    dec_current_tare = Convert.ToDecimal(dr_current_weight["Weight_Tare"].ToString());
                }
                catch
                {
                    dec_current_tare = 0;
                }
            }
            ds_current_weight.Dispose();

            return dec_current_tare;
        }

        public static decimal Calculate_Nett_Weight(int material_Id, decimal Quantity)
        {
            decimal dec_Nett_Weight = 0;
            decimal dec_material_weight = 0;
            DataSet ds = PF.Data.AccessLayer.CM_Material.Get_Info(material_Id);
            DataRow dr;
            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    dec_material_weight = Convert.ToDecimal(dr["Weight"].ToString());
                }
                ds.Dispose();
            }
            catch
            {
                dec_material_weight = 0;
            }

            dec_Nett_Weight = dec_material_weight * Quantity;
            return dec_Nett_Weight;
        }

        public static int Get_Season()
        {
            int int_Season = 0;
            DataSet ds_system = PF.Data.AccessLayer.CM_System.Get_Info_For_Code("Season");
            DataRow dr_system;

            for (int i = 0; i < ds_system.Tables[0].Rows.Count; i++)
            {
                dr_system = ds_system.Tables[0].Rows[i];
                int_Season = Convert.ToInt32(dr_system["Value"].ToString());
            }
            ds_system.Dispose();

            return int_Season;
        }

        public static decimal Get_GST()
        {
            decimal dec_GST = 0;
            DataSet ds_system = PF.Data.AccessLayer.CM_System.Get_Info_For_Code("GST");
            DataRow dr_system;

            for (int i = 0; i < ds_system.Tables[0].Rows.Count; i++)
            {
                dr_system = ds_system.Tables[0].Rows[i];
                dec_GST = Convert.ToDecimal(dr_system["Value"].ToString());
            }
            ds_system.Dispose();

            return dec_GST;
        }

        public static string Get_Single_System_Code(string code)
        {
            string return_code = "";
            DataSet ds = PF.Data.AccessLayer.CM_System.Get_Info_For_Code(code);
            DataRow dr;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                return_code = dr["Value"].ToString();
            }
            ds.Dispose();
            return return_code;
        }

        public static void Consumable_Check(int int_Current_User_Id)
        {
            //check holding and used levels
            var stock1 = new List<stock>();
            DataSet ds1 = PF.Data.AccessLayer.PF_Stock_Holding.Holding_Sum();
            DataSet ds2 = PF.Data.AccessLayer.PF_Stock_Used.Used_Sum();
            DataRow dr1;
            DataRow dr2;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                dr1 = ds1.Tables[0].Rows[i];

                for (int i2 = 0; i2 < ds2.Tables[0].Rows.Count; i2++)
                {
                    dr2 = ds2.Tables[0].Rows[i2];

                    if (Convert.ToInt32(dr1["Stock_Item_Id"].ToString()) == Convert.ToInt32(dr2["Stock_Item_Id"].ToString()))
                    {
                        stock1.Add(new stock
                        {
                            Stock_Item_Id = Convert.ToInt32(dr1["Stock_Item_Id"].ToString()),
                            Holding = Convert.ToInt32(dr1["Holding"].ToString()),
                            Used = Convert.ToInt32(dr2["Used"].ToString())
                        });
                    }
                }
            }
            ds1.Dispose();
            ds2.Dispose();
            string str_emsg = "";
            for (int i = 0; i < stock1.Count; i++)
            {
                DataSet ds = PF.Data.AccessLayer.PF_Stock_Item.Get_Info(stock1[i].Stock_Item_Id);
                DataRow dr;
                for (int i2 = 0; i2 < ds.Tables[0].Rows.Count; i2++)
                {
                    dr = ds.Tables[0].Rows[i2];
                    if (Convert.ToBoolean(dr["Email_Sent"].ToString()) == false)
                    {
                        if (Convert.ToInt32(dr["Trig"].ToString()) >= (stock1[i].Holding - stock1[i].Used))
                        {
                            str_emsg = str_emsg + dr["Code"].ToString() + " - " + dr["Description"].ToString() + ": Current Holding = " + Convert.ToString((stock1[i].Holding - stock1[i].Used)) + Environment.NewLine;
                        }
                    }
                }
                ds.Dispose();
            }
            if (str_emsg.Length > 0)
            {
                str_emsg = "The following consumable items are running low. " + Environment.NewLine + str_emsg;

                PF.Data.AccessLayer.SY_SendMail.send("services@FP.co.nz", "GlenysP@FP.co.nz", "Consumable Levels Email", str_emsg);
                PF.Data.AccessLayer.SY_SendMail.send("services@FP.co.nz", "AlisonC@FP.co.nz", "Process Factory - Consumable Levels Email", str_emsg);

                for (int i = 0; i < stock1.Count; i++)
                {
                    PF.Data.AccessLayer.PF_Stock_Item.Update_Email(stock1[i].Stock_Item_Id, true, int_Current_User_Id);
                }
            }
        }

        public static string Create_Billing_Address()
        {
            string str_delivery_name = "";
            try
            {
                //other address is used for the billing address, if this is blank, then use delivery address
                if (PF.Data.Outlook.Contacts.Contact_Other_Address == "")
                {
                    str_delivery_name = str_delivery_name + Environment.NewLine + PF.Data.Outlook.Contacts.Contact_Business_Address.ToString() + Environment.NewLine +
                                        PF.Data.Outlook.Contacts.Contact_Business_City.ToString() + Environment.NewLine + PF.Data.Outlook.Contacts.Contact_Business_Postal_Code.ToString();
                }
                else
                {
                    // Null

                    str_delivery_name = str_delivery_name + Environment.NewLine + PF.Data.Outlook.Contacts.Contact_Other_Address.ToString() + Environment.NewLine +
                                        PF.Data.Outlook.Contacts.Contact_Other_City.ToString() + Environment.NewLine + PF.Data.Outlook.Contacts.Contact_Other_Postal_Code.ToString();
                }
                return str_delivery_name;
            }
            catch (Exception ex)
            {
                    string errormessage = ex.Message + "\r\n\r\n" + "Business/Business City/Business Postal Code or Other Address/Other City/Other Postal Code is empty.\r\nPlease update your Outlook details.";
                    Console.WriteLine(ex.Message);
                    logger.Log(LogLevel.Debug, errormessage);
                    GeneralErrorMessage(errormessage, "Check_Address problem", ex);
               
                // Added 28-04-2015 BN
                str_delivery_name = "Business/Business City/Business Postal Code or Other Address/Other City/Other Postal Code is empty. Please update your Outlook details.";
                return str_delivery_name;
            }
        }

        public static string Check_Email()
        {
            string str_msg = "";

            if (PF.Data.Outlook.Contacts.Contact_Email == null || PF.Data.Outlook.Contacts.Contact_Email.Count == 0)
            {
                str_msg = "No Email Address have been loaded for this customer. Please load an Email Address in Outlook." + Environment.NewLine;
            }
            return str_msg;
        }

        public static string delivery_name
        {
            get;
            set;
        }

        public static string Check_Name()
        {
            string str_msg = "";
            if (PF.Data.Outlook.Contacts.Contact_Company_Name == "")
            {
                if (PF.Data.Outlook.Contacts.Contact_First_Name == "" && PF.Data.Outlook.Contacts.Contact_Last_Name == "")
                {
                    str_msg = str_msg + "No Names (Company, First, Last) are loaded in Outlook for this Customer. Please load a Name and try again." + Environment.NewLine;
                }
                else
                {
                    delivery_name = PF.Data.Outlook.Contacts.Contact_First_Name + " " + PF.Data.Outlook.Contacts.Contact_Last_Name;
                }
            }
            else
            {
                delivery_name = PF.Data.Outlook.Contacts.Contact_Company_Name;
            }
            return str_msg;
        }

        public static string Check_Address()
        {
            string str_msg = "";
            try {
                if (PF.Data.Outlook.Contacts.Contact_Business_Address.ToString() == "")
                {
                    str_msg = str_msg + "No Delivery Address loaded in OutLook for this Customer. Please load an Address and try again." + Environment.NewLine;
                }
                if (PF.Data.Outlook.Contacts.Contact_Other_Address.ToString() == "")
                {
                    str_msg = str_msg + "No Billing Address loaded in OutLook for this Customer. Please load an Address and try again." + Environment.NewLine;
                }
                return str_msg;
            }
            catch (Exception ex)
            {
                string errormessage = ex.Message + "\r\n\r\n" + "Either Contact_Business_Address or Contact_Other_Address is null.\r\n\r\nYou need to re-link the customers.";
                Console.WriteLine(ex.Message);
                logger.Log(LogLevel.Debug, errormessage);
                GeneralErrorMessage(errormessage, "Check_Address problem", ex);

                    //throw new Exception(ex.Message + "\r\n" + "Either Contact_Business_Address or Contact_Other_Address is null.\r\nYou need to re-link the customers.");

                return str_msg;
            }

        }

        public static string Calculate_Totals(string code, string str_WHERE)
        {
            string str_return = "";
            DataSet ds_bins;
            DataRow dr_bins;
            switch (code)
            {
                // Gets the number of cartons/drums on each pallet, and totals for the work order
                case "BAGS":
                case "BOXS":
                    ds_bins = PF.Data.AccessLayer.PF_Pallet_Details.Get_Info_For_WorkOrder(str_WHERE, code);
                    int int_sum_boxes = 0;
                    for (int i_bins = 0; i_bins < Convert.ToInt32(ds_bins.Tables[0].Rows.Count.ToString()); i_bins++)
                    {
                        dr_bins = ds_bins.Tables[0].Rows[i_bins];
                        int_sum_boxes = int_sum_boxes + Convert.ToInt32(dr_bins["Quantity"].ToString());
                    }
                    str_return = Convert.ToString(int_sum_boxes);

                    ds_bins.Dispose();
                    break;
                // gets the number of bins tipped on the work orders
                case "BINS":
                    ds_bins = PF.Data.AccessLayer.CM_Bins.Get_Info_for_Work_Order(str_WHERE);
                    str_return = ds_bins.Tables[0].Rows.Count.ToString();
                    ds_bins.Dispose();
                    break;
                //Get the number of Batches created on the work order
                case "BOILER":
                    ds_bins = PF.Data.AccessLayer.PF_Pallet_Details.Get_Batches_For_WorkOrder(str_WHERE, code);
                    str_return = ds_bins.Tables[0].Rows.Count.ToString();
                    ds_bins.Dispose();
                    break;

                case "FREIGHT":
                    str_return = "1";
                    break;
                //Gets the total weight of the bins tipped on the work order
                case "HOTFILL":
                case "OLIVES":
                case "BERRIES": // Added 19/10/2015 BN
                    ds_bins = PF.Data.AccessLayer.CM_Bins.Get_Info_for_Work_Order(str_WHERE, code);

                    decimal dec_sum_weight = 0;

                    for (int i_bins = 0; i_bins < Convert.ToInt32(ds_bins.Tables[0].Rows.Count.ToString()); i_bins++)
                    {
                        dr_bins = ds_bins.Tables[0].Rows[i_bins];
                        dec_sum_weight = dec_sum_weight + Convert.ToDecimal(dr_bins["Weight_Gross"].ToString()) - Convert.ToDecimal(dr_bins["Weight_Tare"].ToString());
                    }
                    str_return = Convert.ToString(dec_sum_weight);
                    ds_bins.Dispose();
                    break;
                //Get the number of pallets created for the work order
                case "PALLETS":
                    ds_bins = PF.Data.AccessLayer.PF_Pallet_Details.Get_Info_For_WorkOrder(str_WHERE, code);
                    str_return = ds_bins.Tables[0].Rows.Count.ToString();
                    ds_bins.Dispose();
                    break;
                //Get the number of days worked, by counting the number of work orders, as each work order is only for a single day
                case "RENTAL":
                case "POWER":
                case "CLEAN":
                    ds_bins = PF.Data.AccessLayer.PF_Work_Order.Get_Invoice_Rate(str_WHERE, code);
                    str_return = ds_bins.Tables[0].Rows.Count.ToString();
                    ds_bins.Dispose();
                    break;

                case "STORAGE-1": //unprocessed fruit
                    ds_bins = PF.Data.AccessLayer.CM_Bins.Get_Bin_Storage(str_WHERE);
                    decimal dec_storeage1 = 0;

                    for (int i_bins = 0; i_bins < Convert.ToInt32(ds_bins.Tables[0].Rows.Count.ToString()); i_bins++)
                    {
                        dr_bins = ds_bins.Tables[0].Rows[i_bins];
                        dec_storeage1 = dec_storeage1 + Convert.ToDecimal(dr_bins["Bins"].ToString()) * Convert.ToDecimal(dr_bins["Storage_Time"].ToString());
                    }
                    str_return = Convert.ToString(dec_storeage1);
                    ds_bins.Dispose();
                    break;

                case "STORAGE-2": //Processed Product in Cool Storeage
                case "STORAGE-3": //Processed Product in Dry Storeage
                    ds_bins = PF.Data.AccessLayer.PF_Orders.Get_Storage(str_WHERE);
                    decimal dec_storeage2 = 0;

                    for (int i_bins = 0; i_bins < Convert.ToInt32(ds_bins.Tables[0].Rows.Count.ToString()); i_bins++)
                    {
                        dr_bins = ds_bins.Tables[0].Rows[i_bins];
                        dec_storeage2 = dec_storeage2 + Convert.ToDecimal(dr_bins["Total"].ToString()) * Convert.ToDecimal(dr_bins["Storage_Time"].ToString());
                    }
                    str_return = Convert.ToString(dec_storeage2);
                    ds_bins.Dispose();
                    break;

                case "WAGES":
                    ds_bins = PF.Data.AccessLayer.PF_Work_Order.Get_Staff_for_Work_Order(str_WHERE);
                    decimal dec_total_wages = 0;

                    for (int i_wages = 0; i_wages < ds_bins.Tables[0].Rows.Count; i_wages++)
                    {
                        dr_bins = ds_bins.Tables[0].Rows[i_wages];
                        dec_total_wages = dec_total_wages + Math.Round(Convert.ToDecimal(dr_bins["mins"].ToString()) * (Convert.ToDecimal(dr_bins["Hourly_Rate"].ToString()) / 60), 2);
                    }
                    str_return = Convert.ToString(dec_total_wages);
                    break;
            }
            return str_return;
        }

        public static void GeneralErrorMessage(string MessageDetails, string MessageCaption, Exception MessageException)
        {
            Console.WriteLine(MessageDetails);
            logger.Log(LogLevel.Debug, MessageDetails); // Would be nice if a stack trace was available here

            if (MessageException.InnerException != null)
            {
                MessageBox.Show(MessageDetails  + "\r\n\r\n" + MessageException.InnerException, MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(MessageDetails, MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class stock
    {
        public int Stock_Item_Id { get; set; }
        public int Holding { get; set; }
        public int Used { get; set; }
    }
}