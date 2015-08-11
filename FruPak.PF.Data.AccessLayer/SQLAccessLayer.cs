using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using NLog;
using System.Windows.Forms;
using System.Reflection;

namespace FruPak.Utils.Data
{
    /// <summary>
    /// SQL Access Layer. 
    /// 
    /// A Basic - Database Access Layer for SQL. 
    /// June 2004 - Implements 3 basic SQL Command structures for Calling Stored Procs (Eujin). 
    /// 
    /// 22 Sept 2004 - Adding Transactional SQL capability. 
    /// 
    /// 27 Sept 2004 - Created Instance Capability. 
    /// 
    /// </summary>
    public class SQLAccessLayer
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private static SQLAccessLayer2 instance;
        public static SQLAccessLayer2 Instance
        {
            get
            {
                if(instance == null)
                    instance = new SQLAccessLayer2();

                return instance;
            }
        }

        private SQLAccessLayer()
        {
            //			 connectionString = string.Empty;
            //			 sqlConnection = null;
        }


        public static string ConnectionString
        {
            get
            {
                return Instance.ConnectionString;
                
            }
            set
            {
                Instance.ConnectionString = value;
            }
        }

        public static OleDbConnection Connection

        {
            get 
            {
                return Instance.Connection;
            }
        }


        public static object RunSP_Scalar(string spName, out object outputParam, params object[] parameters)
        {
            //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Scalar: " + spName, parameters));
            
            return Instance.RunSP_Scalar(spName, out outputParam, parameters);
        }
               
        public static object RunSP_Scalar(string spName, params object[] parameters)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Scalar: " + spName, parameters));
                
                object outputParam = new object();
                return RunSP_Scalar(spName, out outputParam, parameters);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                MessageBox.Show(spName + " - " + ex.Message, "RunSP_Scalar", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
                //throw ex;
            }

        }

        public static object RunSP_Scalar(string spName, out object outputParam)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Scalar: " + spName, null));
                
                return RunSP_Scalar(spName, out outputParam, null);
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                MessageBox.Show(spName + " - " + ex.Message, "RunSP_Scalar", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                outputParam = null;
                return null;

                //throw ex;
            }
        }
        public static object RunSP_Scalar(string spName)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Scalar: " + spName, null));
                
                return RunSP_Scalar(spName, null);
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                MessageBox.Show(spName + " - " + ex.Message, "RunSP_Scalar", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
                //throw ex;
            }
        }


        public static int RunSP_NonQuery(string spName, out object outputParam, params object[] parameters)
        {
            //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_NonQuery: " + spName, parameters));
            
            return Instance.RunSP_NonQuery(spName, out outputParam, parameters);
        }

        public static int RunSP_NonQuery(string spName, params object[] parameters)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_NonQuery: " + spName, parameters));
                
                object outputParam = new object();
                return RunSP_NonQuery(spName, out outputParam, parameters);
            }
            catch (Exception ex)
            {
                //logger.Log(LogLevel.Debug, ex.Message);
                logger.Log(LogLevel.Debug, spName + " (return 0) - " + ex.Message);
                MessageBox.Show(spName + " - " + ex.Message, "RunSP_NonQuery", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return 0;
                //throw ex;
            }
        }

        public static int RunSP_NonQuery(string spName, out object outputParam)
        {
            //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_NonQuery: " + spName, null));
            
            return RunSP_NonQuery(spName, out outputParam, null);
        }

        public static int RunSP_NonQuery(string spName)
        {
            //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_NonQuery: " + spName, null));
            
            return RunSP_NonQuery(spName, null);
        }


        public static DataSet RunSP_Query(string spName, out object outputParam, params object[] parameters)
        {
            //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_NonQuery: " + spName, parameters));
            
            return Instance.RunSP_Query(spName, out outputParam, parameters);
        }

        public static DataSet RunSP_Query(string spName, params object[] parameters)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Query: " + spName, parameters));
                
                object outputParam = new object();
                return RunSP_Query(spName, out outputParam, parameters);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                MessageBox.Show(spName + " - " + ex.Message, "RunSP_Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //MessageBox.Show(ex.Message, "Database Possibly Corrupted", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;    //  BN 31/21/2014
                //throw ex;
            }
        }
        
        public static DataSet RunSP_Query(string spName, out object outputParam)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Query: " + spName, null));
                
                return RunSP_Query(spName, out outputParam, null);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                MessageBox.Show(spName + " - " + ex.Message, "RunSP_Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                outputParam = null;
                return null;    //  BN 31/21/2014
                //throw ex;
            }
        }

        public static DataSet RunSP_Query(string spName)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Query: " + spName, null));
                
                return RunSP_Query(spName, null);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                
                // Disabled 28/01/2015 BN - It was getting annoying as I've now handled a null result in the calling method.
                //MessageBox.Show(spName + " - " + ex.Message, "RunSP_Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;    //  BN 31/21/2014
                //throw ex;
            }
        }


        public static DataSet Run_Query(string CommandText, params OleDbParameter[] parameters)
        {
            // Suppress this ridiculously long Select statement, as it screws up the entire CSV
            // 6/01/2015	53:03.2	Info	
            // Run_Query: SELECT DISTINCT M.Menu_Id, M.Name AS MENU ,MP.MenuPan_Id, 
            // MP.Name AS SUBMENU, MPGR.Write_Access FROM  dbo.SC_User U INNER JOIN  
            // dbo.SC_User_Group_Relationship UGR ON U.User_Id = UGR.User_Id INNER JOIN  
            // dbo.SC_Menu_Group_Relationship MGR ON MGR.UserGroup_Id = UGR.UserGroup_Id INNER JOIN  
            // dbo.SC_Menu_Panel_Group_Relationship MPGR ON MPGR.UserGroup_Id = UGR.UserGroup_Id 
            // INNER JOIN  dbo.SC_Menu_Panel MP ON MP.MenuPan_Id = MPGR.MenuPan_Id 
            // INNER JOIN  dbo.SC_Menu M ON M.Menu_Id = MGR.Menu_Id AND M.Menu_Id = MP.Menu_Id 
            // WHERE U.User_Id = 3	FruPak.Utils.Data.SQLAccessLayer	
            // Run_Query	FruPak.Utils.Data.SQLAccessLayer.Run_Query(SQLAccessLayer.cs:236)	BRUCE-LAPTOP

            //if (CommandText.StartsWith("SELECT DISTINCT M.Menu_Id, M.Name AS MENU") == true)
            //{
            //    logger.Log(LogLevel.Info, LogCodeStatic("Run_Query: Excessively long SELECT statement has been suppressed for readability in the CSV log.", parameters));
            //}
            //else
            //{
                //logger.Log(LogLevel.Info, LogCodeStatic("Run_Query: " + CommandText, parameters));
            //}
            return Instance.Run_Query(CommandText, parameters);
        }

        public static int Run_NonQuery(string CommandText, params OleDbParameter[] parameters)
        {
            //logger.Log(LogLevel.Info, LogCodeStatic("Run_NonQuery: " + CommandText, parameters));
            
            return Instance.Run_NonQuery(CommandText, parameters);
        }

        
        internal static void CreateParams(OleDbCommand cmd, OleDbParameter[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++) 
            {
                cmd.Parameters.Add(parameters[i]);
            }
        }

        internal static void PopulateParams(OleDbCommand cmd, Object[] parameters)
        {
            for (int i = 1; i < parameters.Length + 1; i++) 
            {
                if(i < cmd.Parameters.Count)
                    cmd.Parameters[i].Value = parameters[i - 1];
            }
        }
        
        public static void TSQL_Begin()
        {
            Instance.TSQL_Begin();
        }

        public static void TSQL_Commit()
        {
            Instance.TSQL_Commit();
        }

        public static void TSQL_Rollback()
        {
            Instance.TSQL_Rollback();
        }

        
        /// <summary>
        /// Workaround for deriving parameters for Transactional SQL Procedures
        /// </summary>
        /// <param name="spName">Name of Stored Proc</param>
        /// <param name="cmd">SqlCommand to populate with derived parameters</param>
        internal static void TSQL_DeriveParameters(string spName, OleDbCommand cmd,string connString)
        {	
            // Open a Secondary SQL Connection
            OleDbConnection tempConn = new OleDbConnection(connString);
            try 
            {
                tempConn.Open();
                OleDbCommand tempCmd = new OleDbCommand(spName, tempConn);
                tempCmd.CommandType = CommandType.StoredProcedure;
                OleDbCommandBuilder.DeriveParameters(tempCmd);
            
                // Now Clone each of the Parameters from tempCmd
                foreach(OleDbParameter p in tempCmd.Parameters)
                {
                    OleDbParameter newp = new OleDbParameter(p.ParameterName, p.OleDbType, p.Size, p.Direction, p.IsNullable, p.Precision, p.Scale, p.SourceColumn, p.SourceVersion, p.Value);
                    cmd.Parameters.Add(newp);
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
                throw ex;   // Will generate an unhandled exception
            }
            finally
            {
                tempConn.Close();
            }
        }

        #region Log Code
        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff. 
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public string LogCode(string input)
        {
            string Output = "___[ " + input + " ]___";
            return Output;
        }
        #endregion
        #region Log Code Static
        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff. 
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public static string LogCodeStatic(string input, object[] parameters)
        {
            string Output = string.Empty;

            if (parameters != null) // BN 3/02/2015 (Oops, forgot the null check)
            {
                string stemp = " - Parameters: ";
                for (int i = 0; i < parameters.Length; i++)
                {
                    stemp += parameters[i].ToString() + ",";
                }
                stemp = stemp.TrimEnd(',');
                if (stemp != " - Parameters: ")
                {
                    Output = "___[ " + input + stemp + " ]___";
                }
                else
                {
                    Output = "___[ " + input + " ]___";
                }
            }
            else
            {
                Output = "___[ " + input + " ]___";
            }

            return Output;
        }
        #endregion

    }

    public class SQLAccessLayer2
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();     

        private string connectionString;
        private OleDbConnection sqlConnection;
        private OleDbTransaction transaction;

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                if(transaction == null)
                {
                    if(sqlConnection != null)
                    {
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                        sqlConnection = null;
                    }
                    connectionString = value;
                }
                else
                {
                    logger.Log(LogLevel.Debug, "InvalidOperationException: Current transaction in progress, unable to change DB connection string: " + connectionString);
                    throw new InvalidOperationException("Current transaction in progress, unable to change DB connection string");
                }
            }
        }

        public OleDbConnection Connection 
        {
            get
            {
                if(sqlConnection == null)
                {
                    if(!connectionString.Equals(string.Empty))
                    {
                        sqlConnection = new OleDbConnection(connectionString);
                        try
                        {
                            sqlConnection.Open();
                            return sqlConnection;
                        }
                        catch (System.Data.OleDb.OleDbException olex)
                        {
                            // The network is down
                            MessageBox.Show("Either the Network is down, or the SQL server cannot be found.\r\n\r\n" + 
                                "This application will Exit when you press the OK button.\r\n\r\n" + olex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
                            proc.Kill();

                            return null;
                        }
                    }
                    else 
                    {
                        logger.Log(LogLevel.Debug, "Invalid Connection String: " + connectionString);
                        throw new Exception("Invalid Connection String");
                    }
                }
                else 
                {
                    try
                    {
                        if (sqlConnection.State == ConnectionState.Closed)
                            sqlConnection.Open(); // If it has crashed previously, will end up here, unable to recover.
                        return sqlConnection;
                    }
                    catch (Exception ex)
                    {
                        logger.Log(LogLevel.Debug, "Sql Connection is null. Returning null.");
                        MessageBox.Show(ex.Message, "Connection", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return null;    //  BN 31/12/2014
                        //throw ex;
                    }
                }
            }
        }

        public OleDbTransaction Transaction
        {
            get
            {
                return transaction;
            }
            set
            {
                transaction = value;
            }
        }
        public SQLAccessLayer2()
        {
            connectionString = string.Empty;
            sqlConnection = null;
            transaction = null;
        }
        public object RunSP_Scalar(string spName, out object outputParam, params object[] parameters)
        {
            try
            {
                //logger.Log(LogLevel.Info, LogCode("RunSP_Scalar: " + spName));
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Scalar: " + spName, parameters));

                OleDbCommand cmd = new OleDbCommand(spName, Connection);				
                cmd.CommandType = CommandType.StoredProcedure;
                
                if(transaction == null)
                    OleDbCommandBuilder.DeriveParameters(cmd);
                else
                {
                    SQLAccessLayer.TSQL_DeriveParameters(spName,cmd,this.connectionString);		
                    cmd.Transaction = transaction;
                }
                
                if(parameters != null)
                {
                    SQLAccessLayer.PopulateParams(cmd, parameters);
                }
                object retVal = cmd.ExecuteScalar();
                outputParam = cmd.Parameters[0];
                return retVal;
            }
            catch (Exception ex) 
            {
                logger.Log(LogLevel.Debug, ex.Message);
                throw ex;   // Will generate an unhandled exception
            }
            finally 
            {
                if(transaction == null)
                    Connection.Close();
            }
        }
               
        public object RunSP_Scalar(string spName, params object[] parameters)
        {

            try 
            {
                //logger.Log(LogLevel.Info, LogCode("RunSP_Scalar: " + spName));
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Scalar: " + spName, parameters));

                object outputParam = new object();
                return RunSP_Scalar(spName, out outputParam, parameters);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
                throw ex;   // Will generate an unhandled exception
            }
        }

        public object RunSP_Scalar(string spName, out object outputParam)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCode("RunSP_Scalar: " + spName));
                return RunSP_Scalar(spName, out outputParam, null);
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
                throw ex;   // Will generate an unhandled exception
            }
        }
        public object RunSP_Scalar(string spName)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCode("RunSP_Scalar: " + spName));
                return RunSP_Scalar(spName, null);
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
                throw ex;   // Will generate an unhandled exception
            }
        }

        public int RunSP_NonQuery(string spName, out object outputParam, params object[] parameters)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCode("RunSP_NonQuery: " + spName));
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_NonQuery: " + spName, parameters));

                OleDbCommand cmd = new OleDbCommand(spName, Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if(transaction == null)
                    OleDbCommandBuilder.DeriveParameters(cmd);
                else
                {
                    SQLAccessLayer.TSQL_DeriveParameters(spName, cmd, this.connectionString);
                    cmd.Transaction = transaction;
                }
                
                if(parameters != null)
                {
                    SQLAccessLayer.PopulateParams(cmd, parameters);
                }
                int retVal = cmd.ExecuteNonQuery();
                // Assign output Param
                outputParam = cmd.Parameters[0];
                return retVal;
            }
            catch (Exception ex) 
            {
                // Log Error
                logger.Log(LogLevel.Debug, ex.Message);
                throw ex;   // Will generate an unhandled exception
            }
            finally 
            {
                if(transaction == null)
                    Connection.Close();
            }
        }

        public int RunSP_NonQuery(string spName, params object[] parameters)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCode("RunSP_NonQuery: " + spName));
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_NonQuery: " + spName, parameters));

                object outputParam = new object();
                return RunSP_NonQuery(spName, out outputParam, parameters);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
                throw ex;   // Will generate an unhandled exception
            }
        }

        public int RunSP_NonQuery(string spName, out object outputParam)
        {
            //logger.Log(LogLevel.Info, LogCode("RunSP_NonQuery: " + spName));
            
            return RunSP_NonQuery(spName, out outputParam, null);
        }

        public int RunSP_NonQuery(string spName)
        {
            //logger.Log(LogLevel.Info, LogCode("RunSP_NonQuery: " + spName));
            
            return RunSP_NonQuery(spName, null);
        }

        public DataSet RunSP_Query(string spName, out object outputParam, params object[] parameters)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Query: " + spName, parameters));

                // BN Superseded Code 28/01/2015
                //if (parameters != null)
                //{
                //    logger.Log(LogLevel.Info, LogCode("RunSP_Query: " + spName + ", parameters count: " + parameters.Length.ToString() + ", " + parameters[0].ToString()));
                //}
                //else
                //{
                //    logger.Log(LogLevel.Info, LogCode("RunSP_Query: " + spName));
                //}

                OleDbCommand cmd = new OleDbCommand(spName, Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                
                if(transaction == null)
                    OleDbCommandBuilder.DeriveParameters(cmd);
                else
                {
                    SQLAccessLayer.TSQL_DeriveParameters(spName, cmd, this.connectionString);
                    cmd.Transaction = transaction;
                }
                
                if(parameters != null)
                {
                    SQLAccessLayer.PopulateParams(cmd, parameters);
                }

                DataSet oDataSet = new DataSet();
                OleDbDataAdapter oSqlDataAdapter = new OleDbDataAdapter(cmd);
                oSqlDataAdapter.Fill(oDataSet);

                // Grab Output Parameters 
                outputParam = cmd.Parameters[0];


                #region Right here is the dataset, which has all the column headers available (BN 3/02/2015)
                string sColName = string.Empty;
                foreach (DataColumn column in oDataSet.Tables[0].Columns)
                {
                    Console.WriteLine(column.ColumnName);
                    sColName += column.ColumnName + ", ";
                }

                sColName = sColName.TrimEnd(' ');
                sColName = sColName.TrimEnd(',');

                if (parameters != null)
                {
                    logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Query: " + spName + " [ Columns: " + sColName + " ] ", parameters));
                }
                else
                {
                    logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Query: " + spName + " [ Columns: " + sColName + " ] (parameters = null)", parameters));
                }
                #endregion

                return oDataSet;
            }
            catch (Exception ex) 
            {
                // Log Error
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_NonQuery: " + spName, parameters));

                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                if (parameters != null)
                {
                    MessageBox.Show(spName + " - " + ex.Message + "\r\nparameters count: " + parameters.Length.ToString() + ", " + parameters[0].ToString(), "RunSP_Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show(spName + " - " + ex.Message, "RunSP_Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                outputParam = null;
                return null;
                //throw ex;
            }
            finally 
            {
                if(transaction == null)
                    Connection.Close();
            }
        }

        public DataSet RunSP_Query(string spName, params object[] parameters)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCode("RunSP_Query: " + spName));
                //logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Query: " + spName, parameters));

                object outputParam = new object();
                return RunSP_Query(spName, out outputParam, parameters);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                MessageBox.Show(spName + " - " + ex.Message, "RunSP_Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
                //throw ex;
            }
        }
        
        public DataSet RunSP_Query(string spName, out object outputParam)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCode("RunSP_Query: " + spName));

                return RunSP_Query(spName, out outputParam, null);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                MessageBox.Show(spName + " - " + ex.Message, "RunSP_Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                outputParam = null;
                return null;
                //throw ex;
            }
        }

        public DataSet RunSP_Query(string spName)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCode("RunSP_Query: " + spName));

                return RunSP_Query(spName, null);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, spName + " (return null) - " + ex.Message);
                MessageBox.Show(spName + " - " + ex.Message, "RunSP_Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
                //throw ex;
            }
        }

        public DataSet Run_Query(string CommandText, params OleDbParameter[] parameters)
        {
            //logger.Log(LogLevel.Info, LogCodeStatic("Run_Query: " + CommandText, parameters));

            //logger.Log(LogLevel.Warn, "Search Crash: " + CommandText);

            try
            {
                // This line was causing the double logging 06-01-2015 BN
                //logger.Log(LogLevel.Info, LogCode("Run_Query: " + CommandText);

                OleDbCommand cmd = new OleDbCommand(); // Crashes right here if incorrect MAC address
                cmd.Connection = Connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = CommandText;

                if (parameters != null)
                {
                    SQLAccessLayer.CreateParams(cmd, parameters);
                }
                DataSet ds = new DataSet();
                OleDbDataAdapter OleDbDataAdapter = new OleDbDataAdapter(cmd);
                OleDbDataAdapter.Fill(ds);
                
                // Restrict the output somewhat
                if (CommandText.Contains("WHERE Invoice_Id = ") || CommandText.Contains("WHERE Customer_Id = ") || CommandText.Contains("WHERE Trader_Id = "))
                {

                    // This dataset returns various tables. I'll log it anyway. BN 3/02/2015
                    #region Right here is the dataset, which has all the column headers available (BN 3/02/2015)
                    string sColName = string.Empty;
                    //string sDataRow = string.Empty;
                    string sReadable = string.Empty;
                    int iColumnCount = 0;

                    //object[] objColumnArray = null;
                    //object[] objValueArray = null;


                    // Concatenate a string with the column names
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        sColName += column.ColumnName + ", ";
                    }
                    iColumnCount = ds.Tables[0].Columns.Count;

                    object[] objColumnArray = new object[iColumnCount]; // To store the column names
                    object[] objValueArray = new object[iColumnCount];  // To store the corresponding values

                    // Make a copy of each column name in the objectArray
                    // This is much newer code re the old string way
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)    // or iColumnCount
                    {
                        objColumnArray[i] = ds.Tables[0].Columns[i];
                    }

                    // Debugging to Console ------------------------------------
                    //sColName = sColName.TrimEnd(' ');
                    //sColName = sColName.TrimEnd(',');
                    //Console.WriteLine("Column Names: " + sColName);
                    // End Debugging to Console---------------------------------

                    //Don't log just yet 'till I figure this out properly
                    //if (parameters != null)
                    //{
                    //    logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Query: " + CommandText + " [ Columns: " + sColName + " ] ", parameters));
                    //}
                    //else
                    //{
                    //    logger.Log(LogLevel.Info, LogCodeStatic("RunSP_Query: " + CommandText + " [ Columns: " + sColName + " ] (parameters = null)", parameters));
                    //}


                    //foreach (DataColumn dc in ds.Tables[0].Columns)
                    //{
                    //    foreach (DataRow row in ds.Tables[0].Rows)
                    //    {

                    //        objValueArray = row.ItemArray;
                    //        for (int i = 0; i < objValueArray.Length; i++)
                    //        {
                    //            if (objValueArray[i] != null && objValueArray[i] != DBNull.Value)
                    //            {
                    //                // Ignore this recommendation regarding casting the left-hand side to string; it breaks if you 'Fix' it
                    //                //if ((string)objArray[i] == string.Empty)    // Will break because you can't cast an int to a string

                    //                if (objValueArray[i] == string.Empty)    // Comment fields are quite often empty
                    //                {
                    //                    sDataRow += "No Comment" + ", ";
                    //                }
                    //                else
                    //                {
                    //                    sDataRow += objValueArray[i].ToString() + ", ";
                    //                }
                    //            }
                    //            else
                    //            {
                    //                sDataRow += "null" + ", ";
                    //            }

                    //            sReadable = objColumnArray[i] + ": " + sDataRow + ",";
                    //        }

                    //        sDataRow = sDataRow.TrimEnd(' ');
                    //        sDataRow = sDataRow.TrimEnd(',');
                    //        //Console.WriteLine(sDataRow);

                    //        sReadable = sReadable.TrimEnd(' ');
                    //        sReadable = sReadable.TrimEnd(',');
                    //        //Console.WriteLine(sReadable);

                    //        //logger.Log(LogLevel.Info, LogCodeStatic("DataRow Values: " + sDataRow, null));
                    //    }
                    
                    //Console.WriteLine("RowCount: " + ds.Tables[0].Rows.Count.ToString());

                    Console.WriteLine("\r\n");  // Newline


                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        objValueArray = row.ItemArray;
                        for (int i = 0; i < objValueArray.Length; i++)
                        {

                            if (objValueArray[i] != null && objValueArray[i] != DBNull.Value)
                            {
                                // Ignore this recommendation regarding casting the left-hand side to string; it breaks if you 'Fix' it
                                //if ((string)objArray[i] == string.Empty)    // Will break because you can't cast an int to a string

                                if (objValueArray[i] == string.Empty)    // Comment fields are quite often empty
                                {
                                    //sDataRow += "No Comment" + ", ";
                                    objValueArray[i] = "No Comment";
                                }
                                //else
                                //{
                                //    sDataRow += objValueArray[i].ToString() + ", ";
                                //}
                            }
                            else
                            {
                                //sDataRow += "null" + ", ";
                                objValueArray[i] = "null";
                            }
                        }
                        // For each column, there must be a data value entry
                        for (int i = 0; i < iColumnCount; i++)
                        {
                            sReadable += objColumnArray[i] + ": " + objValueArray[i] + ", ";
                        }
                        sReadable = sReadable.TrimEnd(' ');
                        sReadable = sReadable.TrimEnd(',');
                        Console.WriteLine(sReadable);

                        logger.Log(LogLevel.Info, LogCodeStatic("Final Values: " + sReadable, null));
                    }

                    //}

                }

                #endregion

                return ds;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                logger.Log(LogLevel.Fatal, "Showstopper crash is about to occur: OleDbException: " + CommandText);
                logger.Log(LogLevel.Debug, "OleDbException: " + ex.Message);
                MessageBox.Show(CommandText + " - " + ex.Message, "OleDbException", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return null;
                //throw ex;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Fatal, "Unexpected crash is about to occur: OleDbCommand MAC address is the prime suspect: " + CommandText);
                logger.Log(LogLevel.Debug, CommandText + " (return null) - " + ex.Message);
                MessageBox.Show(CommandText + " - " + ex.Message, "OleDbCommand", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                if(transaction == null)
                    if (Connection != null)
                    {
                        Connection.Close();
                        //logger.Log(LogLevel.Info, LogCode("Closed database connection.");
                    }
            }
            return null;
        }

        public int Run_NonQuery(string CommandText, params OleDbParameter[] parameters)
        {
            try 
            {
                //logger.Log(LogLevel.Info, LogCode("Run_NonQuery: " + CommandText));
                //logger.Log(LogLevel.Info, LogCodeStatic("Run_NonQuery: " + CommandText, parameters));

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = CommandText;
                
                if(parameters != null)
                {
                    SQLAccessLayer.CreateParams(cmd, parameters);
                }

                if(Connection.State != ConnectionState.Open)
                    Connection.Open();
                
                return cmd.ExecuteNonQuery(); // Returns the number of rows affected - BN 3/02/2015
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, CommandText + " (return 0) - " + ex.Message);
                MessageBox.Show(CommandText + " - " + ex.Message, "Run_NonQuery", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //throw ex;
                return 0;
            }
            finally
            {
                if(transaction == null)
                    Connection.Close();
            }
        }


        #region Transactions
        public void TSQL_Begin()
        {
            if (transaction != null)
                throw new InvalidOperationException("Another transaction is currently in progress");

            logger.Log(LogLevel.Info, LogCode("TSQL_Begin"));
            transaction = Connection.BeginTransaction();
        }

        public void TSQL_Commit()
        {
            if (transaction != null)
                transaction.Commit();

            logger.Log(LogLevel.Info, LogCode("TSQL_Commit"));
            transaction = null;
            sqlConnection.Close();
        }

        public void TSQL_Rollback()
        {
            if (transaction != null)
                transaction.Rollback();

            logger.Log(LogLevel.Info, LogCode("TSQL_Rollback"));
            transaction = null;
            sqlConnection.Close();
        } 
        #endregion

        #region Log Code
        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff. 
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public string LogCode(string input)
        {
            string Output = "___[ " + input + " ]___";
            return Output;
        }
        #endregion
        #region Log Code Static
        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff. 
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public static string LogCodeStatic(string input, object[] parameters)
        {
            string Output = string.Empty;

            if (parameters != null) // BN 3/02/2015 (Oops, forgot the null check)
            {
                string stemp = " - Parameters: ";
                for (int i = 0; i < parameters.Length; i++)
                {
                    stemp += parameters[i].ToString() + ",";
                }
                stemp = stemp.TrimEnd(',');
                if (stemp != " - Parameters: ")
                {
                    Output = "___[ " + input + stemp + " ]___";
                }
                else
                {
                    Output = "___[ " + input + " ]___";
                }
            }
            else
            {
                Output = "___[ " + input + " ]___";
            }

            return Output;
        }
        #endregion

    }
}
