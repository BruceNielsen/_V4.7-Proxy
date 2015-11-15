using System;
using System.Data;

namespace FruPak.PF.Utils.Security
{
    /*Description
    -----------------
    General Class.
     * This class holds code that is used in more than one place within this namespace.

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    internal class General
    {
        //Encrypts the entered password so it can be matched against the stored password
        public static string Password_Encryption(string str_Password)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(str_Password);
            data = x.ComputeHash(data);
            String md5Hash = System.Text.Encoding.ASCII.GetString(data);

            return md5Hash;
        }

        public static bool Password_Validate(string str_User_ID, string str_Password)
        {
            bool bol_valid_pswd = true;

            DataSet ds_Get_User_Info = FruPak.PF.Data.AccessLayer.SC_User.Get_Info(str_User_ID);
            DataRow dr_Get_User_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_User_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_User_Info = ds_Get_User_Info.Tables[0].Rows[i];

                if (dr_Get_User_Info["Password"].ToString() != General.Password_Encryption(str_Password))
                {
                    bol_valid_pswd = false;
                }
            }

            return bol_valid_pswd;
            //return bol_valid_pswd = true;
        }

        public static int User_Id_Retrival(string str_Logon)
        {
            int int_User_Id = 0;
            DataSet ds_Get_User_Info = FruPak.PF.Data.AccessLayer.SC_User.Get_Info(str_Logon);
            DataRow dr_Get_User_Info;

            for (int i = 0; i < Convert.ToInt32(ds_Get_User_Info.Tables[0].Rows.Count.ToString()); i++)
            {
                dr_Get_User_Info = ds_Get_User_Info.Tables[0].Rows[i];
                int_User_Id = Convert.ToInt32(dr_Get_User_Info["User_Id"].ToString());
            }

            return int_User_Id;
        }

        /// <summary>
        /// Validates User details
        /// </summary>
        /// <param name="str_First_Name"></param>
        /// <param name="str_Last_Name"></param>
        /// <param name="str_Logon"></param>
        /// <param name="str_Password"></param>
        /// <returns></returns>
        ///
        public static string User_Check(string str_First_Name, string str_Last_Name)
        {
            string str_msg = "";

            if (str_First_Name.Length == 0)
            {
                str_msg = str_msg + "Invalid First Name: Please enter a First Name" + Environment.NewLine;
            }
            if (str_Last_Name.Length == 0)
            {
                str_msg = str_msg + "Invalid Last Name: Please enter a Last Name" + Environment.NewLine;
            }
            return str_msg;
        }

        public static string User_Check(string str_First_Name, string str_Last_Name, string str_Logon, bool bol_check)
        {
            string str_msg = User_Check(str_First_Name, str_Last_Name);
            if (bol_check == true)
            {
                if (str_Logon.Length == 0)
                {
                    str_msg = str_msg + "Invalid UserID: Please enter a UserID" + Environment.NewLine;
                }
                else
                {
                    DataSet ds_Get_User_Info = FruPak.PF.Data.AccessLayer.SC_User.Get_Info(str_Logon);
                    if (Convert.ToInt32(ds_Get_User_Info.Tables[0].Rows.Count.ToString()) > 0)
                    {
                        str_msg = str_msg + "Invalid UserID: UserID has already been taken. Please choose a different UserID" + Environment.NewLine;
                    }
                }
            }
            return str_msg;
        }

        public static string User_Check(string str_First_Name, string str_Last_Name, string str_Logon, bool bol_check, string str_Password)
        {
            string str_msg = User_Check(str_First_Name, str_Last_Name, str_Logon, bol_check);

            if (str_Password.Length == 0)
            {
                str_msg = str_msg + "Invalid Password: Please enter a Password" + Environment.NewLine;
            }

            return str_msg;
        }
    }
}