using PF.CustomSettings;
using NLog;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PF.PrintLayer
{
	public class Certificate_Of_Analysis
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private static DateTime dt_Load_Date;

		public static int Print(string Data, bool bol_print)
		{
			int return_code = 98;

			string[] DataParts = Data.Split(':');

			return_code = Print(Convert.ToInt32(DataParts[0]), DataParts[1], DataParts[2], DataParts[3],bol_print);
			return return_code;
		}

		public static int Print(int int_Order_Id, string str_delivery_name, string str_Address, string str_type, bool bol_print)
		{
			logger.Log(LogLevel.Info, "Certificate_Of_Analysis: Print: Order_Id: " + int_Order_Id.ToString());

			try
			{
				// The first entry on the second page is being doubled-up. 
				// I'm trying to find a solution by breaking the code down into more readable chunks
				string Bookmark = string.Empty;
				string NewText = string.Empty;

				// Yet another magic number
				int return_code = 97;

				#region Pull the Template path from the database
				DataSet ds_default = PF.Data.AccessLayer.CM_System.Get_Info_Like("PF%");
				DataRow dr_default;

				for (int i_d = 0; i_d < ds_default.Tables[0].Rows.Count; i_d++)
				{
					dr_default = ds_default.Tables[0].Rows[i_d];
					switch (dr_default["Code"].ToString())
					{
						case "PF-TPath":
							PF.PrintLayer.Word.TemplatePath = dr_default["Value"].ToString();
							break;

						case "PF-TCOA":
							PF.PrintLayer.Word.TemplateName = dr_default["Value"].ToString();
							break;
					}
				}
				ds_default.Dispose();

				#endregion

				//find the products and add them to the COA
				DataSet ds_Pallet = PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Order_prod(int_Order_Id);
				DataRow dr_Pallet;

				int ip = 1;

				int int_tot_quantity = 0;

				#region Checking for not pallets loaded against the order, or in actual english, pallets not loaded against the order
				try
				{
					Convert.ToInt32(ds_Pallet.Tables[0].Rows.Count);
					if (ds_Pallet.Tables[0].Rows.Count <= 0)
					{
						return_code = 96;
					}
				}
				catch
				{

					return_code = 96;
				}
				#endregion

				string str_cust_order = "";
				string str_Freight_Docket = "";
				for (int i = 0; i < ds_Pallet.Tables[0].Rows.Count; i++)
				{
					PF.PrintLayer.Word.StartWord();

					#region ------------- Order Details -------------

					DataSet ds_Order = PF.Data.AccessLayer.PF_Orders.Get_Info(int_Order_Id);
					DataRow dr_Order;
					for (int i_order = 0; i_order < ds_Order.Tables[0].Rows.Count; i_order++)
					{
						dr_Order = ds_Order.Tables[0].Rows[i_order];
						dt_Load_Date = Convert.ToDateTime(dr_Order["Load_Date"].ToString());
						PF.PrintLayer.Word.ReplaceText("Load_Date", dt_Load_Date.Date.ToString("dd/MM/yyyy"));
						str_cust_order = dr_Order["Customer_Order"].ToString();
						PF.PrintLayer.Word.ReplaceText("Order_Num", dr_Order["Customer_Order"].ToString());
						str_Freight_Docket = dr_Order["Freight_Docket"].ToString();
						PF.PrintLayer.Word.ReplaceText("Freight_Docket", dr_Order["Freight_Docket"].ToString());
					}

					PF.PrintLayer.Word.ReplaceText("Address", str_delivery_name + Environment.NewLine + str_Address);

					int_tot_quantity = 0;
					dr_Pallet = ds_Pallet.Tables[0].Rows[i];

					string str_product = dr_Pallet["Product"].ToString() + ": " + dr_Pallet["FruitType"].ToString();
					string str_size = dr_Pallet["Size"].ToString();
					string str_variety = "Variety: " + dr_Pallet["Varity"].ToString();

					PF.PrintLayer.Word.ReplaceText("Product", str_product + " " + str_size);
					PF.PrintLayer.Word.ReplaceText("Variety", str_variety);

					#endregion ------------- Order Details -------------

					#region Declare DataSet and DataRow: Get_Info3
					DataSet ds_Get_Info3;
					DataRow dr_Get_Info3;
					#endregion

					#region ------------- Lab Results -------------

					DataSet ds_LabResults = PF.Data.AccessLayer.PF_Product_LabResults.Get_Info_by_Prod(Convert.ToInt32(dr_Pallet["Product_Id"].ToString()), Convert.ToInt32(dr_Pallet["FruitType_Id"].ToString()), Convert.ToInt32(dr_Pallet["Variety_Id"].ToString()));
					DataRow dr_LabResults;
					string str_ph = "";
					string str_moist = "";
					string str_brix = "";
					for (int j = 0; j < Convert.ToInt32(ds_LabResults.Tables[0].Rows.Count.ToString()); j++)
					{
						dr_LabResults = ds_LabResults.Tables[0].Rows[j];
						switch (dr_LabResults["Code"].ToString())
						{
							case "pH":
								str_ph = dr_LabResults["Value"].ToString();
								PF.PrintLayer.Word.ReplaceText("PH", dr_LabResults["Value"].ToString());
								break;

							case "Moist":
								str_moist = dr_LabResults["Value"].ToString();
								PF.PrintLayer.Word.ReplaceText("MOIST", dr_LabResults["Value"].ToString());
								break;

							case "Brix":
								str_brix = dr_LabResults["Value"].ToString();
								PF.PrintLayer.Word.ReplaceText("BRIX", dr_LabResults["Value"].ToString());
								break;
						}
					}
					ds_LabResults.Dispose();

					#endregion ------------- Lab Results -------------

					#region ------------- Pallet Number-------------

					//pallet numbers
					int int_total_Chep = 0;
					int int_total_Oth = 0;
					DataSet ds_Orders_Pallets = PF.Data.AccessLayer.PF_Orders_Pallets.Get_Info_for_Order(int_Order_Id);
					DataRow dr_Orders_Pallets;
					for (int j = 0; j < Convert.ToInt32(ds_Orders_Pallets.Tables[0].Rows.Count.ToString()); j++)
					{
						dr_Orders_Pallets = ds_Orders_Pallets.Tables[0].Rows[j];
						if (dr_Orders_Pallets["Code"].ToString().ToUpper().Contains("CHEP"))
						{
							int_total_Chep = int_total_Chep + Convert.ToInt32(dr_Orders_Pallets["Number"].ToString());
						}
						else
						{
							int_total_Oth = int_total_Oth + Convert.ToInt32(dr_Orders_Pallets["Number"].ToString());
						}
					}
					ds_Orders_Pallets.Dispose();

					PF.PrintLayer.Word.ReplaceText("Chep_Pallets", Convert.ToString(int_total_Chep));
					PF.PrintLayer.Word.ReplaceText("Oth_Pallets", Convert.ToString(int_total_Oth));

					#endregion ------------- Pallet Number-------------

					#region Check and reset output filename


					if (PF.PrintLayer.Word.FileName.LastIndexOf('-') > 0)
					{
						PF.PrintLayer.Word.FileName =
							PF.PrintLayer.Word.FileName.Substring(0, PF.PrintLayer.Word.FileName.LastIndexOf('-')) + "-" + Convert.ToString(ip);
					}
					else
					{
						PF.PrintLayer.Word.FileName =
							PF.PrintLayer.Word.FileName.Substring(0, PF.PrintLayer.Word.FileName.Length) + "-" + Convert.ToString(ip);
					}

					#endregion

					#region ------------- Best Before Dates-------------

					//Get BBD (Best Before Date), number of months to add on to production date
					DataSet ds_Other = PF.Data.AccessLayer.PF_Product_Other.Get_Info(Convert.ToInt32(dr_Pallet["Product_Id"].ToString()), "BBD");
					DataRow dr_Other;
					int int_BBD_months = 0;
					for (int bbd = 0; bbd < ds_Other.Tables[0].Rows.Count; bbd++)
					{
						dr_Other = ds_Other.Tables[0].Rows[bbd];
						try
						{
							int_BBD_months = Convert.ToInt32(dr_Other["Value"].ToString());
						}
						catch
						{
							int_BBD_months = 0;
						}
					}
					ds_Other.Dispose();

					#endregion ------------- Best Before Dates-------------

					#region ------------- Batch Details -------------

					//get batch info
					ds_Get_Info3 = PF.Data.AccessLayer.PF_Pallet.Get_Info_for_Order(int_Order_Id, Convert.ToInt32(dr_Pallet["Product_Id"].ToString()), Convert.ToInt32(dr_Pallet["FruitType_Id"].ToString()), Convert.ToInt32(dr_Pallet["Variety_Id"].ToString()));
					int jp = 0;
					int kp = 0;
					for (int j = 0; j < Convert.ToInt32(ds_Get_Info3.Tables[0].Rows.Count.ToString()); j++)
					{
						dr_Get_Info3 = ds_Get_Info3.Tables[0].Rows[j];

						#region check to see if the batches are on the same pallet, if not add a blank (date, Batch) between Pallets

						if (j > 0)
						{
							if (Convert.ToInt32(dr_Get_Info3["Pallet_Id"].ToString()) == Convert.ToInt32(ds_Get_Info3.Tables[0].Rows[j - 1]["Pallet_Id"].ToString()))
							{
								jp++;
							}
							else
							{
								jp = jp + 2;
							}
						}
						else
						{
							jp++;
						}

						#endregion check to see if the batches are on the same pallet, if not add a blank (date, Batch) between Pallets


						if (jp > 18)
						{
							//kp++;
							if (PF.PrintLayer.Word.FileName.LastIndexOf('-') > 0)
							{
								PF.PrintLayer.Word.FileName =
									 PF.PrintLayer.Word.FileName.Substring(0, PF.PrintLayer.Word.FileName.LastIndexOf('-')) + "-" + Convert.ToString(kp++);

							}
							else
							{
								PF.PrintLayer.Word.FileName =
									PF.PrintLayer.Word.FileName.Substring(0, PF.PrintLayer.Word.FileName.Length) + "-" + Convert.ToString(kp);

							}

							#region Prepare the page for printing

							PF.PrintLayer.Word.CloseWord();
							PF.PrintLayer.Word.StartWord();


							PF.PrintLayer.Word.ReplaceText("Load_Date", dt_Load_Date.Date.ToString("dd/MM/yyyy"));
							PF.PrintLayer.Word.ReplaceText("Order_Num", str_cust_order);
							PF.PrintLayer.Word.ReplaceText("Freight_Docket", str_Freight_Docket);
							PF.PrintLayer.Word.ReplaceText("Address", str_delivery_name + Environment.NewLine + str_Address);
							PF.PrintLayer.Word.ReplaceText("Product", str_product + " " + str_size);
							PF.PrintLayer.Word.ReplaceText("Variety", str_variety);
							PF.PrintLayer.Word.ReplaceText("PH", str_ph);
							PF.PrintLayer.Word.ReplaceText("MOIST", str_moist);
							PF.PrintLayer.Word.ReplaceText("BRIX", str_brix);
							PF.PrintLayer.Word.ReplaceText("Chep_Pallets", Convert.ToString(int_total_Chep));
							PF.PrintLayer.Word.ReplaceText("Oth_Pallets", Convert.ToString(int_total_Oth));

							#endregion  Prepare the page for printing

							jp = 1;

							DateTime dat = new DateTime();
							dat = Convert.ToDateTime(dr_Get_Info3["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(0, 4)).AddMonths(int_BBD_months);

							Bookmark = "Date" + Convert.ToString(jp);   // Date1 etc.
							NewText = dr_Get_Info3["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(0, 4);
							PF.PrintLayer.Word.ReplaceText(Bookmark, NewText);
							//PF.PrintLayer.Word.ReplaceText("Date" + Convert.ToString(jp), dr_Get_Info3["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(0, 4));

							if (int_BBD_months > 0)
							{
								Bookmark = "BBD" + Convert.ToString(jp);    // BBD1 etc.
								NewText = dat.ToShortDateString();
								PF.PrintLayer.Word.ReplaceText(Bookmark, NewText);
								//PF.PrintLayer.Word.ReplaceText("BBD" + Convert.ToString(jp), dat.ToShortDateString());
							}

							Bookmark = "Batch" + Convert.ToString(jp);  // Batch1 etc.
							NewText = dr_Get_Info3["Batch_Num"].ToString().Substring(8) + " - " + dr_Get_Info3["Quantity"].ToString();
							PF.PrintLayer.Word.ReplaceText(Bookmark, NewText);
							//PF.PrintLayer.Word.ReplaceText("Batch" + Convert.ToString(jp), dr_Get_Info3["Batch_Num"].ToString().Substring(8) + " - " + dr_Get_Info3["Quantity"].ToString());
							int_tot_quantity = int_tot_quantity + Convert.ToInt32(dr_Get_Info3["Quantity"].ToString());

						}
						else
						{
							// Changing this to an else branch has finally solved the problem at long last 13-10-2015 BN

							DateTime dt = new DateTime();
							dt = Convert.ToDateTime(dr_Get_Info3["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(0, 4)).AddMonths(int_BBD_months);

							Bookmark = "Date" + Convert.ToString(jp);
							NewText = dr_Get_Info3["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(0, 4);

							PF.PrintLayer.Word.ReplaceText(Bookmark, NewText);
							//PF.PrintLayer.Word.ReplaceText("Date" + Convert.ToString(jp), dr_Get_Info3["Batch_Num"].ToString().Substring(6, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(4, 2) + "/" + dr_Get_Info3["Batch_Num"].ToString().Substring(0, 4));

							if (int_BBD_months > 0)
							{
								Bookmark = "BBD" + Convert.ToString(jp);
								NewText = dt.ToShortDateString();
								PF.PrintLayer.Word.ReplaceText(Bookmark, NewText);

								//PF.PrintLayer.Word.ReplaceText("BBD" + Convert.ToString(jp), dt.ToShortDateString());
							}

							Bookmark = "Batch" + Convert.ToString(jp);
							NewText = dr_Get_Info3["Batch_Num"].ToString().Substring(8) + " - " + dr_Get_Info3["Quantity"].ToString();
							PF.PrintLayer.Word.ReplaceText(Bookmark, NewText);

							//PF.PrintLayer.Word.ReplaceText("Batch" + Convert.ToString(jp), dr_Get_Info3["Batch_Num"].ToString().Substring(8) + " - " + dr_Get_Info3["Quantity"].ToString());

							int_tot_quantity = int_tot_quantity + Convert.ToInt32(dr_Get_Info3["Quantity"].ToString());
						}

						if (bol_print == true)
						{
							switch (str_type)
							{
								case "Print":
									// This generates zillions of pages because the page is built up by progressively adding to it
									//return_code = PF.PrintLayer.Word.Print();    // C:\Windows\System32\spool\PRINTERS

									// In both cases, save as pdf - it will be handled differently further up in Shipping_Search.cs
									return_code = PF.PrintLayer.Word.SaveAsPdf();
									//return_code = PF.PrintLayer.Word.SaveAS();
									break;

								case "Email":
									return_code = PF.PrintLayer.Word.SaveAsPdf();
									//return_code = PF.PrintLayer.Word.SaveAS();
									break;
							}
						}
						else
						{
							// View - PF.PrintLayer.Word.SaveAS - Saves as .docx
							return_code = PF.PrintLayer.Word.SaveAsPdf();
							//return_code = PF.PrintLayer.Word.SaveAS();
						}

					}
					#endregion

					#region SaveAsPdf This is the last thing to go on the page
					PF.PrintLayer.Word.ReplaceText("Quantity", Convert.ToString(int_tot_quantity));

					// Print works every single time without fail
					if (bol_print == true)
					{
						switch (str_type)
						{
							case "Print":
								//return_code = PF.PrintLayer.Word.Print();    // C:\Windows\System32\spool\PRINTERS
								return_code = PF.PrintLayer.Word.SaveAsPdf();
								//return_code = PF.PrintLayer.Word.SaveAS();
								//PF.PrintLayer.Word.CloseWord();
								break;

							case "Email":
								// I'm pretty sure I've just found the fault
								return_code = PF.PrintLayer.Word.SaveAsPdf();
								//return_code = PF.PrintLayer.Word.SaveAS();
								//PF.PrintLayer.Word.CloseWord();
								break;
						}
					}
					else
					{

						// View - Saves as .docx // This is faulty as well
						//return_code = PF.PrintLayer.Word.SaveAS();
						return_code = PF.PrintLayer.Word.SaveAsPdf();
						//return_code = PF.PrintLayer.Word.SaveAS();
						//PF.PrintLayer.Word.CloseWord();
						//throw new ArgumentException("The parameter was invalid");
					}

					// Doesn't seem to do anything
					//PF.PrintLayer.Word.CloseWord();
					#endregion

					ip++;

				}

				ds_Pallet.Dispose();


				PF.PrintLayer.Word.CloseWord();
				return return_code;
			}
			catch (Exception ex)
			{
				Common.Code.DebugStacktrace.StackTrace(ex);
				return 42;  // The answer to life, the universe and everything
			}
		}

	}
}