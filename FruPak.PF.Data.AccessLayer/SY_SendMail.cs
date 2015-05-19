using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using FruPak.Utils.Data;

namespace FruPak.PF.Data.AccessLayer
{
    public class SY_SendMail
    {
        public static int send(string from, string to, string subject, string body)
        {
            /*You can input multiple recipients and use comma (,) to separate multiple addresses */
            FruPak.PF.Data.AccessLayer.DConfig.CreateDConfig();
            return SQLAccessLayer.Run_NonQuery("DECLARE @ServerAddr nvarchar(128) Set @ServerAddr = '192.168.50.100' " +
                                               "DECLARE @From nvarchar(128) Set @From = '" + from + "' " +
                                               "DECLARE @To nvarchar(1024) Set @To = '" + to + "' " +
                                               "DECLARE @Subject nvarchar(256) Set @Subject = '" + subject + "' " +
                                               "DECLARE @Bodytext nvarchar(512) Set @BodyText = '" + body + "' " +
                                               "exec master.dbo.usp_SendTextEmail @ServerAddr, @From, @To, @Subject, @BodyText");
        }
    }
}
