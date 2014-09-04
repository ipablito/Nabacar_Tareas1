using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Security.Principal;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace ExcelXml
{
    public class ExcelWriter : IDisposable
    {
        private XmlWriter _writer;

        public enum CellStyle { General, Number, Currency, DateTime, ShortDate };

        public void WriteStartDocument()
        {
            if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

            _writer.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");
            _writer.WriteStartElement("ss", "Workbook", "urn:schemas-microsoft-com:office:spreadsheet");
            WriteExcelStyles();
        }

        public void WriteEndDocument()
        {
            if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

            _writer.WriteEndElement();
        }

        private void WriteExcelStyleElement(CellStyle style)
        {
            _writer.WriteStartElement("Style", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteAttributeString("ID", "urn:schemas-microsoft-com:office:spreadsheet", style.ToString());
            _writer.WriteEndElement();
        }

        private void WriteExcelStyleElement(CellStyle style, string NumberFormat)
        {
            _writer.WriteStartElement("Style", "urn:schemas-microsoft-com:office:spreadsheet");

            _writer.WriteAttributeString("ID", "urn:schemas-microsoft-com:office:spreadsheet", style.ToString());
            _writer.WriteStartElement("NumberFormat", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteAttributeString("Format", "urn:schemas-microsoft-com:office:spreadsheet", NumberFormat);
            _writer.WriteEndElement();

            _writer.WriteEndElement();

        }

        private void WriteExcelStyles()
        {
            _writer.WriteStartElement("Styles", "urn:schemas-microsoft-com:office:spreadsheet");

            WriteExcelStyleElement(CellStyle.General);
            WriteExcelStyleElement(CellStyle.Number, "General Number");
            WriteExcelStyleElement(CellStyle.DateTime, "General Date");
            WriteExcelStyleElement(CellStyle.Currency, "Currency");
            WriteExcelStyleElement(CellStyle.ShortDate, "Short Date");

            _writer.WriteEndElement();
        }

        public void WriteStartWorksheet(string name)
        {
            if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

            _writer.WriteStartElement("Worksheet", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteAttributeString("Name", "urn:schemas-microsoft-com:office:spreadsheet", name);
            _writer.WriteStartElement("Table", "urn:schemas-microsoft-com:office:spreadsheet");
        }

        public void WriteEndWorksheet()
        {
            if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

            _writer.WriteEndElement();
            _writer.WriteEndElement();
        }

        public ExcelWriter(string outputFileName)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            _writer = XmlWriter.Create(outputFileName, settings);
        }

        public void Close()
        {
            if (_writer == null) throw new NotSupportedException("Already closed.");

            _writer.Close();
            _writer = null;
        }

        public void WriteExcelColumnDefinition(int columnWidth)
        {
            if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

            _writer.WriteStartElement("Column", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteStartAttribute("Width", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteValue(columnWidth);
            _writer.WriteEndAttribute();
            _writer.WriteEndElement();
        }

        public void WriteExcelUnstyledCell(string value)
        {
            if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

            _writer.WriteStartElement("Cell", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteStartElement("Data", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteAttributeString("Type", "urn:schemas-microsoft-com:office:spreadsheet", "String");
            _writer.WriteValue(value);
            _writer.WriteEndElement();
            _writer.WriteEndElement();
        }

        public void WriteStartRow()
        {
            if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

            _writer.WriteStartElement("Row", "urn:schemas-microsoft-com:office:spreadsheet");
        }

        public void WriteEndRow()
        {
            if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

            _writer.WriteEndElement();
        }

        public void WriteExcelStyledCell(object value, CellStyle style)
        {
            try
            {
                if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

                _writer.WriteStartElement("Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                _writer.WriteAttributeString("StyleID", "urn:schemas-microsoft-com:office:spreadsheet", style.ToString());
                _writer.WriteStartElement("Data", "urn:schemas-microsoft-com:office:spreadsheet");
                switch (style)
                {
                    case CellStyle.General:
                        _writer.WriteAttributeString("Type", "urn:schemas-microsoft-com:office:spreadsheet", "String");
                        break;
                    case CellStyle.Number:
                        _writer.WriteAttributeString("Type", "urn:schemas-microsoft-com:office:spreadsheet", "Number"); // 與原例子不同,自己加的,未驗證
                        break; // 與原例子不同,自己加的,未驗證
                    case CellStyle.Currency:
                        _writer.WriteAttributeString("Type", "urn:schemas-microsoft-com:office:spreadsheet", "Number");
                        break;
                    case CellStyle.ShortDate:
                    case CellStyle.DateTime:
                        _writer.WriteAttributeString("Type", "urn:schemas-microsoft-com:office:spreadsheet", "DateTime");
                        break;
                }
                _writer.WriteValue(value);
                //  tag += String.Format("{1}\"><ss:Data ss:Type=\"DateTime\">{0:yyyy\\-MM\\-dd\\THH\\:mm\\:ss\\.fff}</ss:Data>", value,

                _writer.WriteEndElement();
                _writer.WriteEndElement();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        public void WriteExcelAutoStyledCell(object value)
        {
            try
            {
                var formats = new[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyy-M-d", "yyyy-M-d HH:mm:ss", "MM/dd/yyyy HH:mm:ss tt", "MM/dd/yyyy" };
                DateTime dx;

                if (_writer == null) throw new NotSupportedException("Cannot write after closing.");

                //write the <ss:Cell> and <ss:Data> tags for something
                if (value is Int16 || value is Int32 || value is Int64 || value is SByte ||
                value is UInt16 || value is UInt32 || value is UInt64 || value is Byte)
                {
                    WriteExcelStyledCell(value, CellStyle.Number);
                }
                else if (value is Single || value is Double || value is Decimal) //we'll assume it's a currency
                {
                    //WriteExcelStyledCell(value, CellStyle.Currency);      //原例子
                    WriteExcelStyledCell(value, CellStyle.Number); // 與原例子不同,自己改的,未驗證
                }
                else if (value is DateTime || value is MySql.Data.Types.MySqlDateTime)
                {
                    if (value is DateTime)
                    {
                        //check if there's no time information and use the appropriate style
                        WriteExcelStyledCell(value, ((DateTime)value).TimeOfDay.CompareTo(new TimeSpan(0, 0, 0, 0, 0)) == 0 ? CellStyle.ShortDate : CellStyle.DateTime);
                    }
                    else
                    {
                        DateTime.TryParse(value.ToString(),out dx);
                        WriteExcelStyledCell(dx,CellStyle.DateTime);
                    }
                }
                else
                {
                    WriteExcelStyledCell(value, CellStyle.General);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_writer == null)
                return;

            _writer.Close();
            _writer = null;
        }

        #endregion

        public static void ExcelExportNT(DataSet data, String fileName, bool openAfter)
        {
            //export a DataTable to Excel
            DialogResult retry = DialogResult.Retry;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            object value = null;
            DateTime d = new DateTime();
            int i = 0;
            bool isdate = false;
            string field_name = "";

            var formats = new[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyy-M-d", "yyyy-M-d HH:mm:ss", };

            while (retry == DialogResult.Retry)
            {
                try
                {
                    using (ExcelXml.ExcelWriter writer = new ExcelXml.ExcelWriter(fileName))
                    {
                        writer.WriteStartDocument();
                        int no_sheet = 0;
                        foreach (DataTable Table in data.Tables)
                        {
                            no_sheet++;
                            // Write the worksheet contents
                            string tname = "";
                            tname = Table.TableName;
                            if (string.IsNullOrEmpty(tname) == true)
                            {
                                writer.WriteStartWorksheet("Sheet" + no_sheet.ToString());
                            }
                            else
                            {
                                writer.WriteStartWorksheet(tname);
                            }

                            //Write header row
                            writer.WriteStartRow();
                            foreach (DataColumn col in Table.Columns)
                                writer.WriteExcelUnstyledCell(col.Caption);
                            writer.WriteEndRow();

                            //write data
                            foreach (DataRow row in Table.Rows)
                            {
                                i = 0;
                                writer.WriteStartRow();
                                foreach (object o in row.ItemArray)
                                {
                                    field_name = row.Table.Columns[i].ToString();
                                    if (field_name.Contains("date") || field_name.Contains("Date"))
                                        isdate = true;
                                    else
                                        isdate = false;
                                    i++;
                                    Type b = o.GetType();
                                    if (b.Name == "DBNull")
                                        value = "";
                                    else
                                        if (b.Name == "String" && o.ToString().Contains("\0"))
                                            value = o.ToString().Replace("\0", "");
                                        //else if (DateTime.TryParse(o.ToString(), out  d))
                                        else
                                            if (isdate && DateTime.TryParseExact(o.ToString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                                            {   
                                                if (d.Year >= 1900)
                                                {
                                                    if (o.ToString().Length > 10)
                                                        value = d;
                                                    else
                                                        if (o.ToString().Length < 10 && o.ToString().Contains(":"))
                                                            //value = d.TimeOfDay;  //导出到EXCEL的格式有问题
                                                            value = o;
                                                        else
                                                            if (isdate)
                                                                value = d.Date;
                                                            else
                                                                value = o;
                                                
                                                }
                                                else
                                                    value = o;
                                            }
                                            else
                                                value = o;
                                    string c = o.ToString();
                                    writer.WriteExcelAutoStyledCell(value);
                                }
                                writer.WriteEndRow();
                            }
                            writer.WriteEndWorksheet();
                        }
                        // Close up the document
                        writer.WriteEndDocument();
                        writer.Close();
                        if (openAfter)
                        {
                            openFileDialog.FileName = fileName;
                            //openFileDialog.ShowDialog();
                            openFileDialog.OpenFile();
                            retry = DialogResult.Cancel;
                        }
                    }
                }
                catch (Exception myException)
                {
                    retry = MessageBox.Show(myException.Message, "Excel Export", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);
                }
            }
        }
        public static void ExcelExport(DataTable data, String fileName, bool openAfter)
        {
            //export a DataTable to Excel
            DialogResult retry = DialogResult.Retry;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            object value = null;
            DateTime d = new DateTime();
            int i = 0;
            bool isdate = false;
            string field_name = "";

            var formats = new[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyy-M-d", "yyyy-M-d HH:mm:ss", };

            while (retry == DialogResult.Retry)
            {
                try
                {
                    using (ExcelXml.ExcelWriter writer = new ExcelXml.ExcelWriter(fileName))
                    {
                        writer.WriteStartDocument();

                        // Write the worksheet contents
                        writer.WriteStartWorksheet("Sheet1");

                        //Write header row
                        writer.WriteStartRow();
                        foreach (DataColumn col in data.Columns)
                            writer.WriteExcelUnstyledCell(col.Caption);
                        writer.WriteEndRow();

                        //write data
                        foreach (DataRow row in data.Rows)
                        {
                            i = 0;
                            writer.WriteStartRow();
                            foreach (object o in row.ItemArray)
                            {
                                field_name = row.Table.Columns[i].ToString();
                                if (field_name.Contains("date"))
                                    isdate = true;
                                else
                                    isdate = false;
                                i++;
                                Type b = o.GetType();
                                if (b.Name == "DBNull")
                                    value = "";
                                else
                                    if (b.Name == "String" && o.ToString().Contains("\0"))
                                        value = o.ToString().Replace("\0", "");
                                    //else if (DateTime.TryParse(o.ToString(), out  d))
                                    else
                                        if (isdate && DateTime.TryParseExact(o.ToString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                                        {
                                            if (d.Year >= 1900)
                                            {
                                                if (o.ToString().Length > 10)
                                                    value = d;
                                                else
                                                    if (o.ToString().Length < 10 && (o.ToString().Contains(":") || o.ToString().Contains("-")))
                                                        //value = d.TimeOfDay;  //导出到EXCEL的格式有问题
                                                        value = o;
                                                    else
                                                        if (isdate)
                                                            value = d.Date;
                                                        else
                                                            value = o;
                                            }
                                            else
                                                value = o;
                                        }
                                        else
                                            value = o;
                                string c = o.ToString();
                                writer.WriteExcelAutoStyledCell(value);
                            }
                            writer.WriteEndRow();
                        }

                        // Close up the document
                        writer.WriteEndWorksheet();
                        writer.WriteEndDocument();
                        writer.Close();
                        if (openAfter)
                        {
                            openFileDialog.FileName = fileName;
                            //openFileDialog.ShowDialog();
                            openFileDialog.OpenFile();
                            retry = DialogResult.Cancel;
                        }
                    }
                }
                catch (Exception myException)
                {
                    retry = MessageBox.Show(myException.Message, "Excel Export", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);
                }
            }
        }

        public static bool DataSetToExcel(DataSet dataSet, bool isShowExcel, string fileName)
        {
            //Create Excel Object
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //excel.Application.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add();  //Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

            int sheetcounter = 0;

            if (dataSet.Tables.Count == 0)
            {
                return false;
            }

            foreach (DataTable datatable in dataSet.Tables)    
            {
                //DataTable dataTable = dataSet.Tables[0];}
                sheetcounter++;
                int rowNo = datatable.Rows.Count;
                int columnNo = datatable.Columns.Count;
                int colIndex = 0;
                
                //if (rowNo == 0)
                //{
                //    return false;
                //}

                Microsoft.Office.Interop.Excel.Worksheet worksheet;  //(Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[sheetcounter];
                worksheet = (Excel.Worksheet)excel.Application.Worksheets.Add();
                worksheet.Name = datatable.TableName.ToString();
                excel.Visible = isShowExcel;

                //Microsoft.Office.Interop.Excel.Worksheet worksheet =(Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];
                Microsoft.Office.Interop.Excel.Range range;

                //Generate Fields Name
                foreach (DataColumn col in datatable.Columns)
                {
                    colIndex++;
                    excel.Cells[1, colIndex] = col.ColumnName;
                }

                object[,] objData = new object[rowNo, columnNo];

                for (int row = 0; row < rowNo; row++)
                {
                    for (int col = 0; col < columnNo; col++)
                    {
                        objData[row, col] = datatable.Rows[row][col];
                    }
                    //Application.DoEvents();
                }
                range = worksheet.Range[excel.Cells[2, 1], excel.Cells[rowNo + 1, columnNo]];

                //Set Cell Format as Text
                range.NumberFormat = "@";
                range.Value2 = objData;
                //worksheet.Range[excel.Cells[4, 3], excel.Cells[rowNo + 1, 1]].NumberFormat = "yyyy-m-d h:mm";
               
            }
            workbook.SaveAs(fileName, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            System.Reflection.Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            workbook.Close();
            excel.Quit();
            GC.Collect();
            /*Kill Excel();*/
            return true;
        }

    }
}