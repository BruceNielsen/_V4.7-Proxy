using NetOffice.ExcelApi.Enums;
using NLog;
using System;
using System.Drawing;
using System.Globalization;
using _Excel = NetOffice.ExcelApi;

namespace PF.Data.Excel
{
    public class Excel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // With the new v1.7.3 of the NetOffice stuff,
        // You get this error: Interop type 'Application' cannot be embedded. 
        // Use the applicable interface instead.
        // To resolve this, Right-Click the dll in the solution explorer,
        // and choose Properties, then set Embed Interop Types to False
        // BN 8/10/2015

        private static _Excel.Application excelApplication;
        private static _Excel.Workbook workBook;
        private static _Excel.Worksheet workSheet;

        public static string FilePath
        {
            get;
            set;
        }

        public static string FileName
        {
            get;
            set;
        }

        public static string fileExt
        {
            get;
            set;
        }

        public static void startExcel()
        {
            // start excel and turn Application msg boxes
            excelApplication = new _Excel.Application();

            logger.Log(LogLevel.Info, LogCodeStatic("Excel Version: " + excelApplication.Version + " is installed."));

            excelApplication.DisplayAlerts = false;
            fileExt = GetDefaultExtension(excelApplication);
            OpenWorkSheet();
        }

        private static void OpenWorkSheet()
        {
            // add a new workbook
            workBook = excelApplication.Workbooks.Add();
            workSheet = (_Excel.Worksheet)workBook.Worksheets[1];
        }

        public static int Open()
        {
            int return_code = 0;

            string complete_filename = FilePath + "\\" + FileName + fileExt;
            try
            {
                System.IO.File.Exists(complete_filename);
                workBook = excelApplication.Workbooks.Open(complete_filename);
                workSheet = (_Excel.Worksheet)workBook.Worksheets[1];
                return_code = 0;
            }
            catch
            {
                return_code = 9;
            }
            return return_code;
        }

        public static int find_cell(int Col, string value)
        {
            int found_Cell = 0;
            bool bol_found = false;
            for (int i = 0; i < workSheet.Columns[Col].Cells.Count && bol_found == false; i++)
            {
                try
                {
                    if (workSheet.Columns[Col].Cells[i].Value.ToString() == value)
                    {
                        bol_found = true;
                        found_Cell = i;
                    }
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, ex.Message);
                }
            }
            return found_Cell;
        }

        public static string find_value(int Col, int Cell)
        {
            string str_value = "";
            try
            {
                str_value = workSheet.Columns[Col].Cells[Cell].Value.ToString();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
            }
            return str_value;
        }

        public static void Add_Data_Text(string cell, string value)
        {
            workSheet.Range(cell).Value = value;
        }

        public static void Add_Data_Int(string cell, int value)
        {
            workSheet.Range(cell).Value = value;
        }

        public static void Font_Bold(string cell, bool bold)
        {
            workSheet.Range(cell).Font.Bold = bold;
        }

        public static void Cell_Border(string cell, string direction, string style, string colour)
        {
            XlLineStyle xlstyle = new XlLineStyle();
            XlBordersIndex xldirection = new XlBordersIndex();
            Color xlcolour = new Color();
            switch (colour)
            {
                case "Black":
                    xlcolour = Color.Black;
                    break;

                case "Blue":
                    xlcolour = Color.Blue;
                    break;

                case "Orange":
                    xlcolour = Color.Orange;
                    break;

                case "Red":
                    xlcolour = Color.Red;
                    break;
            }
            switch (direction)
            {
                case "InsideHorizontal":
                    xldirection = XlBordersIndex.xlInsideHorizontal;
                    break;

                case "InsideVertical":
                    xldirection = XlBordersIndex.xlInsideVertical;
                    break;

                case "Bottom":
                    xldirection = XlBordersIndex.xlEdgeBottom;
                    break;

                case "Left":
                    xldirection = XlBordersIndex.xlEdgeLeft;
                    break;

                case "Right":
                    xldirection = XlBordersIndex.xlEdgeRight;
                    break;

                case "Top":
                    xldirection = XlBordersIndex.xlEdgeTop;
                    break;
            }
            switch (style)
            {
                case "Continuous":
                    xlstyle = XlLineStyle.xlContinuous;
                    break;

                case "Dot":
                    xlstyle = XlLineStyle.xlDot;
                    break;

                case "Double":
                    xlstyle = XlLineStyle.xlDouble;
                    break;
            }

            workSheet.Range(cell).Borders[xldirection].LineStyle = xlstyle;
            workSheet.Range(cell).Borders[xldirection].Color = ToDouble(xlcolour);
        }

        public static void Font_Size(string cell, int size)
        {
            workSheet.Range(cell).Font.Size = size;
        }

        public static void Set_Column_Width(int Col)
        {
            // setup rows and columns
            workSheet.Columns[Col].AutoFit();
        }

        public static void Set_Column_Width(int Col, int size)
        {
            // setup rows and columns
            workSheet.Columns[Col].ColumnWidth = size;
        }

        public static void Set_Row_Height(int Row)
        {
            workSheet.Rows[Row].AutoFit();
        }

        public static void Set_Row_Height(int Row, int size)
        {
            workSheet.Rows[Row].RowHeight = size;
        }

        public static void Set_Horizontal_Aligment(string cell, string direction)
        {
            switch (direction)
            {
                case "Left":
                    workSheet.Range(cell).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    break;

                case "Center":
                    workSheet.Range(cell).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    break;

                case "Right":
                    workSheet.Range(cell).HorizontalAlignment = XlHAlign.xlHAlignRight;
                    break;

                case "Justify":
                    workSheet.Range(cell).HorizontalAlignment = XlHAlign.xlHAlignJustify;
                    break;

                case "Distributed":
                    workSheet.Range(cell).HorizontalAlignment = XlHAlign.xlHAlignDistributed;
                    break;
            }
        }

        public static void Set_Vertical_Aligment(string cell, string direction)
        {
            switch (direction)
            {
                case "Top":
                    workSheet.Range(cell).VerticalAlignment = XlVAlign.xlVAlignTop;
                    break;

                case "Center":
                    workSheet.Range(cell).VerticalAlignment = XlVAlign.xlVAlignCenter;
                    break;

                case "Bottom":
                    workSheet.Range(cell).VerticalAlignment = XlVAlign.xlVAlignBottom;
                    break;

                case "Justify":
                    workSheet.Range(cell).VerticalAlignment = XlVAlign.xlVAlignJustify;
                    break;

                case "Distributed":
                    workSheet.Range(cell).VerticalAlignment = XlVAlign.xlVAlignDistributed;
                    break;
            }
        }

        public static void Add_Data_Dec(string cell, decimal value, int Pattern)
        {
            // Disabled this to accommodate the latest stable version of the ribbon dll BN 15-04-2015

            // the given thread culture in all NetOffice calls are stored in NetOffice.Settings.
            // you can change the culture of course. Default is en-us.
            //CultureInfo cultureInfo = NetOffice.Settings.ThreadCulture;
            //string Pattern1 = string.Format("0{0}00", cultureInfo.NumberFormat.CurrencyDecimalSeparator);
            //string Pattern2 = string.Format("#{1}##0{0}00", cultureInfo.NumberFormat.CurrencyDecimalSeparator, cultureInfo.NumberFormat.CurrencyGroupSeparator);
            //string Pattern3 = string.Format("$#{1}##0{0}00", cultureInfo.NumberFormat.CurrencyDecimalSeparator, cultureInfo.NumberFormat.CurrencyGroupSeparator);

            //workSheet.Range(cell).Value = value;
            //switch (Pattern)
            //{
            //    case 1:
            //        workSheet.Range(cell).NumberFormat = Pattern1;
            //        break;
            //    case 2:
            //        workSheet.Range(cell).NumberFormat = Pattern2;
            //        break;
            //    case 3:
            //        workSheet.Range(cell).NumberFormat = Pattern3;
            //        break;
            //}
        }

        public static string SaveAS()
        {
            string return_code = "";
            // save the book
            try
            {
                string workbookFile = FilePath + "\\" + FileName + GetDefaultExtension(excelApplication);
                workBook.SaveAs(workbookFile);
                return_code = workbookFile;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
                return_code = "";
            }

            return return_code;
        }

        public static void CloseExcel()
        {
            // close excel and dispose reference
            excelApplication.Quit();
            excelApplication.Dispose();
        }

        private static string GetDefaultExtension(_Excel.Application application)
        {
            double Version = Convert.ToDouble(application.Version, CultureInfo.InvariantCulture);
            if (Version >= 12.00)
                return ".xlsx";
            else
                return ".xls";
        }

        public static int SaveASPDF()
        {
            int return_code = 0;

            object missing = System.Reflection.Missing.Value;

            string paramExportFilePath = @FilePath + "\\" + FileName;

            XlFixedFormatType paramExportFormat = XlFixedFormatType.xlTypePDF;

            try
            {
                workBook.ExportAsFixedFormat(paramExportFormat, paramExportFilePath);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
                return_code = 9;
            }
            return return_code;
        }

        public static int SaveASXPS()
        {
            int return_code = 0;

            object missing = System.Reflection.Missing.Value;

            string paramExportFilePath = @FilePath + "\\" + FileName;

            XlFixedFormatType paramExportFormat = XlFixedFormatType.xlTypeXPS;

            try
            {
                workBook.ExportAsFixedFormat(paramExportFormat, paramExportFilePath);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, ex.Message);
                return_code = 9;
            }
            return return_code;
        }

        public static void resize_column(int index)
        {
            // set colums
            workSheet.Columns[index].AutoFit();
        }

        public static void Border_cell(string first_cell, string last_cell, string style, int Weight)
        {
            XlLineStyle xlL_style = new XlLineStyle();

            // draw back color and border the range explicitly
            switch (style.ToUpper())
            {
                case "CONTINUOUS":
                    xlL_style = XlLineStyle.xlContinuous;
                    break;

                case "DASH":
                    xlL_style = XlLineStyle.xlDash;
                    break;

                case "DASHDOT":
                    xlL_style = XlLineStyle.xlDashDot;
                    break;

                case "DASHDOTDOT":
                    xlL_style = XlLineStyle.xlDashDotDot;
                    break;

                case "DOT":
                    xlL_style = XlLineStyle.xlDot;
                    break;

                case "DOUBLE":
                    xlL_style = XlLineStyle.xlDouble;
                    break;

                case "NONE":
                    xlL_style = XlLineStyle.xlLineStyleNone;
                    break;

                case "SLANTDASHDOT":
                    xlL_style = XlLineStyle.xlSlantDashDot;
                    break;

                default:
                    xlL_style = XlLineStyle.xlLineStyleNone;
                    break;
            }
            workSheet.Range(first_cell + ":" + last_cell).Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = xlL_style;
            workSheet.Range(first_cell + ":" + last_cell).Borders[XlBordersIndex.xlInsideHorizontal].Weight = Weight;
            workSheet.Range(first_cell + ":" + last_cell).Borders[XlBordersIndex.xlInsideHorizontal].Color = ToDouble(Color.Black);
        }

        /// <summary>
        /// Translate a color to double
        /// </summary>
        /// <param name="color">expression to convert</param>
        /// <returns>color</returns>
        private static double ToDouble(System.Drawing.Color color)
        {
            uint returnValue = color.B;
            returnValue = returnValue << 8;
            returnValue += color.G;
            returnValue = returnValue << 8;
            returnValue += color.R;
            return returnValue;
        }

        #region Log Code Static

        /// <summary>
        /// Formats the output to the debug log so that the actual log contents stand out amongst the machine-generated stuff.
        /// Refers to machine-generated content, not calling specifics.
        /// </summary>
        /// <param name="input">The code function that we want to log.</param>
        /// <returns>A formatted (Bracketed) string</returns>
        public static string LogCodeStatic(string input)
        {
            string Output = "___[ " + input + " ]___";
            return Output;
        }

        #endregion Log Code Static
    }
}