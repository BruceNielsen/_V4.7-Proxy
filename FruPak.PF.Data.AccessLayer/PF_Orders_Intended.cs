using FP.Utils.Data;
using System.Data;

namespace PF.Data.AccessLayer
{
    /*Description
    -----------------
    PF_Orders_Intended Class.
     *
     * This Class is a data access layer to the PF_Orders_Intended table
     * Where possible the following standard method names are used and standard column names used.
     *  1. Variable names as input to a method are the same as the column names they refer to.
     *
     * Methods.
     *  Get_Max_ID: Return the max ID from the table, and is stored as Current_Id
     *  Get_Info:   returns all data from the table or return data for a requested record
     *  Delete:     Deletes a single record, with an ID supplied
     *  Insert:     Inserts a new record
     *  Update:     Updates an existing record

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public class PF_Orders_Intended
    {
        public static DataSet Get_Max_ID()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT max(OrderIntent_Id) as Current_Id FROM PF_Orders_Intended");
        }

        public static DataSet Get_info_translated()
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_Query("SELECT OI.* , C.Name,  CAST(M.Material_Num as varchar) + ' - ' + M.Description as Material, sum(P.Weight_Gross) as Total_Sold " +
                                            "FROM dbo.PF_Orders_Intended OI " +
                                            "INNER JOIN dbo.PF_Customer C ON C.Customer_Id = OI.Customer_Id " +
                                            "INNER JOIN dbo.CM_Material M ON M.Material_Id = OI.Material_Id " +
                                            "LEFT OUTER JOIN dbo.PF_Orders O on SUBSTRING(O.Customer_Order, 0, CHARINDEX('-',O.Customer_Order,0)) = OI.Customer_Order " +
                                            "LEFT JOIN dbo.PF_Pallet P on P.Order_Id = O.Order_Id " +
                                            "GROUP BY OI.OrderIntent_Id,OI.Customer_Id, OI.Material_Id, OI.Quantity, OI.Unit_Price, OI.Comments, OI.Customer_Order, " +
                                                     "OI.PF_Active_Ind, OI.Mod_Date, OI.Mod_User_Id, C.Name, M.Material_Num, M.Description --, P.Order_Id");
        }

        public static int Insert(int OrderIntent_Id, int Customer_Id, int Material_Id, decimal Quantity, decimal Unit_Price, string Comments, string Customer_Order, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("INSERT INTO PF_Orders_Intended( OrderIntent_Id, Customer_Id, Material_Id, Quantity, Unit_Price, Comments, Customer_Order, PF_Active_Ind, Mod_Date, Mod_User_Id) " +
                                                "VALUES (" + OrderIntent_Id + "," + Customer_Id + "," + Material_Id + "," + Quantity + "," + Unit_Price + ",'" + Comments + "','" +
                                                             Customer_Order + "','" + PF_Active_Ind + "', GETDATE()," + Mod_User_Id + ")");
        }

        public static int Delete(int OrderIntent_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DELETE FROM PF_Orders_Intended WHERE OrderIntent_Id = " + OrderIntent_Id);
        }

        public static int Update(int OrderIntent_Id, int Customer_Id, int Material_Id, decimal Quantity, decimal Unit_Price, string Comments, string Customer_Order, bool PF_Active_Ind, int Mod_User_Id)
        {
            PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("UPDATE PF_Orders_Intended SET Customer_Id = " + Customer_Id + ", " +
                                                                  "Material_Id = " + Material_Id + ", " +
                                                                  "Quantity = " + Quantity + ", " +
                                                                  "Unit_Price = " + Unit_Price + ", " +
                                                                  "Comments = '" + Comments + "', " +
                                                                  "Customer_Order = '" + Customer_Order + "', " +
                                                                  "PF_Active_Ind = '" + PF_Active_Ind + "', " +
                                                                  "Mod_date = GETDATE(), " +
                                                                  "Mod_User_Id = " + Mod_User_Id +
                                              " WHERE OrderIntent_Id = " + OrderIntent_Id);
        }
    }
}