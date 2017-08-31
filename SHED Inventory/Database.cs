using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Data;
using System.Linq;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;
using System.Diagnostics;

namespace SHED_Inventory
{
    class Entry
    {
        public string name;
        public string almaName;
        public string modelID;
        public string description;
        public string barcode;
        public bool checkedIn;
        public string status;
        public string code;
        public int model;
        public string issue;
    }
    class Database
    {
        private const string ALMAEXCEL = @"Y:\SHED Inventory\SHED Inventory\bin\Debug\AlmaInventory.xlsx";
        private const string ACCESSEXCEL = "InventoryProject.accdb";
        private const string CONNECTIONSTRING = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Y:\SHED Inventory\SHED Inventory\bin\Debug\InventoryProject.accdb";
        private const string EXCELCONNSTRING = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Y:\SHED Inventory\SHED Inventory\bin\Debug\AlmaInventory.xlsx; Extended Properties=Excel 12.0;";
        private OleDbConnection connection;
        private const string DIRECTIONS = "Go through each item on this report and ensure that the physical status matches what is reported.  If the physical status does not match, delete item from report.  After verifying the status of each item save the text file.";
        private const string SUCCESS = "Report has been successfully generated";
        private const string TWOHOURS = "2 Hour Laptops";
        private const string WEEKLY = "Weekly Laptops";
        private const string ACPOWER = "Power Supplies";
        private const string HEADPHONES = "Headphones";
        private const string FLASH = "Flash Memory";
        private const string PROJECTS = "Project Laptops";
        private const string ITEMNAMEHEADER = "ITEM NAME";
        private const string DESCRIPTIONHEADER = "DESCRIPTION";
        private const string BARCODEHEADER = "BARCODE";
        private const string STATUSHEADER = "PHYSICAL STATUS";
        private const string EXISTS_IN_SHED = "Item barcode doesn't have match in Alma.";
        private const string NOT_IN_SHED = "Item barcode doesn't exist in SHED inventory.";
        public string timeSinceReport { get; set; }
        private bool tooFew = false;
        private bool tooMany = false;
        private const string FAILURE = "Could not successfully email report.  ";
        private OleDbCommand command;      
        private List<Entry> entries { get; set; }
        public List<Entry> getEntries { get { return entries; } }
        private List<Entry> shedEntries { get; set; }
        public List<Entry> getShedEntries { get { return shedEntries; } }
        private List<Entry> almaEntries { get; set; }
        public List<Entry> getAlmaEntries { get { return almaEntries; } }
        private const string ERRCONN = "Could not connect to database, please contact admin.";
        private const string SUCCEEDCONN = "Connected to database.";
        public List<Entry> getInventory(string items)
        {
            try
            {
                connection = new OleDbConnection(CONNECTIONSTRING);
            }
            catch
            {
                MessageBox.Show(ERRCONN);
                return null;
            }
            if (items == TWOHOURS)
            {
                getTwoHourLaptops();
                return getEntries;
            }
            else if (items == WEEKLY)
            {
                getWeeklyLaptops();
                return getEntries;
            }
            else if (items == ACPOWER)
            {
                getPowerSupplies();
                return getEntries;
            }
            else if (items == FLASH)
            {
                getMemory();
                return getEntries;
            }
            else if (items == HEADPHONES)
            {
                getHeadphones();
                return getEntries;
            }
            else
                MessageBox.Show("No items located.  Contact administrator for assistance.");
            return null;
        }
        public List<Entry> getWeeklyLaptops()
        {
            entries = new List<Entry>();
            string query = "SELECT [Item Name], [Model ID], Description, Barcode1, Status FROM [Project Laptops] ORDER BY [Model ID], Description";
            connection = new OleDbConnection(CONNECTIONSTRING);
            command = new OleDbCommand(query, connection);
            connection.Open();
            List<Entry> items = new List<Entry>();
            OleDbDataReader reader = command.ExecuteReader();
            items = new List<Entry>();
            while (reader.Read())
            {
                Entry item = new Entry();
                item.name = reader[0].ToString();
                item.modelID = reader[1].ToString();
                item.description = reader[2].ToString();
                item.barcode = reader[3].ToString();
                item.status = reader[4].ToString();
                items.Add(item);
            }
            connection.Close();
            sortWeeklies(items);
            return getEntries;
        }
        public List<Entry> getTwoHourLaptops()
        {
            string query = "SELECT [Item Name], [Model ID], Description, Barcode1, Status FROM [2 Hour Laptops] ORDER BY [Model ID], Description";
            connection = new OleDbConnection(CONNECTIONSTRING);
            command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            entries = new List<Entry>();
            while (reader.Read())
            {
                Entry item = new Entry();
                item.name = reader[0].ToString();
                item.modelID = reader[1].ToString();
                item.description = reader[2].ToString();
                item.barcode = reader[3].ToString();
                item.status = reader[4].ToString();
                item.model = int.Parse(reader[2].ToString());
                entries.Add(item);
            }
            connection.Close();
            sortList();
            return getEntries;
        }
        public List<Entry> getPowerSupplies()
        {
            string query = "SELECT [Item Name], [Model ID], Description, Barcode1, Status FROM [Power Supplies] ORDER BY [Model ID], Description";
            connection = new OleDbConnection(CONNECTIONSTRING);
            command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            entries = new List<Entry>();
            while (reader.Read())
            {
                Entry item = new Entry();
                item.name = reader[0].ToString();
                item.modelID = reader[1].ToString();
                item.description = reader[2].ToString();
                item.barcode = reader[3].ToString();
                item.status = reader[4].ToString();
                item.model = int.Parse(reader[2].ToString());
                entries.Add(item);
            }
            connection.Close();
            sortList();
            return getEntries;
        }
        public List<Entry> getMemory()
        {
            string query = "SELECT [Item Name], [Model ID], Description, Barcode1, Status FROM [Flash Memory] ORDER BY [Model ID], Description ASC";
            connection = new OleDbConnection(CONNECTIONSTRING);
            command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            entries = new List<Entry>();
            while (reader.Read())
            {
                Entry item = new Entry();
                item.name = reader[0].ToString();
                item.modelID = reader[1].ToString();
                item.description = reader[2].ToString();
                item.barcode = reader[3].ToString();
                item.model = int.Parse(reader[2].ToString());
                item.model = int.Parse(reader[2].ToString());
                item.status = reader[4].ToString();
                entries.Add(item);
            }
            connection.Close();
            sortList();
            return getEntries;
        }
        public List<Entry> getHeadphones()
        {
            string query = "SELECT [Item Name], [Model ID], Description, Barcode1, Status FROM [Headphones] ORDER BY [Model ID], Description ASC";
            connection = new OleDbConnection(CONNECTIONSTRING);
            command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            entries = new List<Entry>();
            while (reader.Read())
            {
                Entry item = new Entry();
                item.name = reader[0].ToString();
                item.modelID = reader[1].ToString();
                item.description = reader[2].ToString();
                item.barcode = reader[3].ToString();
                item.status = reader[4].ToString();
                item.model = int.Parse(reader[2].ToString());
                entries.Add(item);
            }
            connection.Close();
            sortList();
            return getEntries;
        }
        public string UpdateTable(string table, List<Entry> entries)
        {
            if (table == WEEKLY)
            {
                table = PROJECTS;
            }
            try {
                foreach (Entry e in entries)
                {
                    bool inStock = e.checkedIn;
                    string code = e.barcode;
                    string input = "";
                    if (inStock)
                        input = "Item in place";
                    else if (!inStock)
                        input = "Item not in place";
                    string query = "UPDATE [" + table + "] SET [Status] = ? WHERE Barcode1 = ?";                
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Status", input);
                    command.Parameters.AddWithValue("@Barcode1", code);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }               
                return "Values have been updated";
            }
            catch (Exception ex)
            {
                connection.Close();
                return "Could not update table" + ex;
            }
        }
        public bool reportInventory(string student)
        {
            List<Entry> twoHourLaptops = getTwoHourLaptops();
            List<Entry> weeklyLaptops = getWeeklyLaptops();
            List<Entry> headphones = getHeadphones();
            List<Entry> flashDrives = getMemory();
            List<Entry> powers = getPowerSupplies();
            entries = new List<Entry>();
            foreach (Entry e in twoHourLaptops)
            {
                entries.Add(e);
            }
            foreach (Entry e in weeklyLaptops)
            {
                entries.Add(e);
            }
            foreach (Entry e in headphones)
            {
                entries.Add(e);
            }
            foreach (Entry e in flashDrives)
            {
                entries.Add(e);
            }
            foreach (Entry e in powers)
            {
                entries.Add(e);
            }
            try {
                Document doc = new Document();
                ParagraphStyle defaultStyle = new ParagraphStyle(doc);
                defaultStyle.Name = "Default";
                defaultStyle.CharacterFormat.FontSize = 12;
                doc.Styles.Add(defaultStyle);
                int errorCount = 0;
                Section s = doc.AddSection();
                Paragraph p = s.AddParagraph();
                p.ApplyStyle(defaultStyle.Name);
                p.AppendText("SHED Inventory discrepancies for " + DateTime.Now.ToShortDateString() + "\r\n\r\nReport created by: " + student + "\r\nAt:" + DateTime.Now.ToShortTimeString());
                p.AppendText("\r\n\r\n\r\n" + ITEMNAMEHEADER + '\t' + '\t' + DESCRIPTIONHEADER + '\t' + '\t' + BARCODEHEADER + '\t' + '\t' + STATUSHEADER + "\r\n");
                p = s.AddParagraph();
                p.ApplyStyle(defaultStyle.Name);
                connection = new OleDbConnection(CONNECTIONSTRING);
                connection.Open();
                foreach (Entry e in entries)
                {
                    string query = "SELECT status FROM AlmaInventory WHERE Barcode = ?";
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode", e.barcode);
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string currentStatus = reader[0].ToString();
                        if (currentStatus != e.status)
                        {
                            errorCount++;
                            char[] c = e.name.ToCharArray();
                            if (c.Length >= 13)
                            {
                                p.AppendText(e.name + '\t' + '\t' + e.description + '\t' + '\t' + e.barcode + '\t' +'\t' + e.status + "\r\n");
                            }
                            else
                            {
                                p.AppendText(e.name + '\t' + '\t' + '\t' + e.description + '\t' + '\t' + e.barcode + '\t'+'\t' + e.status + "\r\n"); 
                            }
                        }
                        else
                            continue;
                    }
                    reader.Close();                    
                }
                ParagraphStyle style = new ParagraphStyle(doc);
                style.Name = "Size";
                style.CharacterFormat.FontSize = 18;
                style.CharacterFormat.FontName = "Showcard Gothic";
                doc.Styles.Add(style);
                ParagraphStyle style2 = new ParagraphStyle(doc);
                style2.Name = "Ross";
                style2.CharacterFormat.FontSize = 18;
                style2.CharacterFormat.FontName = "Harlow Solid Italic";
                doc.Styles.Add(style2);
                if (errorCount > 0)
                {
                    p = s.AddParagraph();
                    p.ApplyStyle(style.Name);
                    p.AppendText("\r\n\r\nNone shall pass...\r\n");                               
                    DocPicture pic = p.AppendPicture(Image.FromFile("Dorf.jpg"));
                    pic.Width = 250;
                    pic.Height = 300;
                   
                }
                else
                {
                    p = s.AddParagraph();
                    p.ApplyStyle(style2.Name);
                    p.AppendText("\r\n\r\nIt's a happy little report.\r\n");                                 
                    DocPicture pic = p.AppendPicture(Image.FromFile("BobRoss.jpg"));
                    pic.Width = 400;
                    pic.Height = 300;
                    
                }               
                p = s.AddParagraph();
                p.ApplyStyle(defaultStyle.Name);
                if (errorCount > 0)
                    p.AppendText(DIRECTIONS);
                else
                    p.AppendText("There are no discrepancies to report.  Please close report and click the No button.");
                doc.SaveToFile("ShedInventory.docx", FileFormat.Docx);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not create error report. " + ex);
                return false;
            }                  
        }
        public void setAlmaInventory()
        {
            createAlmaInventory();
            try
            {
                removeOldAlmaInventory();
            }
            catch (Exception ex)
            {
                createAlmaTable();
            }
            createAlmaTable();
            fillAlmaInventory(getAlmaEntries);
            createAlmaInventory();
            createSHEDInventory();
            getDiscrepancyItems();
            getDiscrepancyItems2();
        }
        private void fillAlmaInventory(List<Entry> entries)
        {
            connection = new OleDbConnection(CONNECTIONSTRING);
            string query = "INSERT INTO AlmaInventory([Name in Alma], [Item Name],[Status Code], Description, [Model ID], Status, Barcode) VALUES(?, ?, ?, ?, ?, ?, ?)";           
            foreach (Entry item in entries)
            {
                if (item.modelID == null)
                {
                    item.modelID = String.Empty;
                }
                connection.Open();
                command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@[Name in Alma]", item.name);
                command.Parameters.AddWithValue("@[Item Name]", item.almaName);
                command.Parameters.AddWithValue("@[Status Code]", item.code);
                command.Parameters.AddWithValue("@Description", item.description);
                command.Parameters.AddWithValue("@[Model ID]", item.modelID);
                command.Parameters.AddWithValue("@Status", item.status);
                command.Parameters.AddWithValue("@Barcode", item.barcode);
                command.ExecuteNonQuery();
                connection.Close();
            }           
        }
        private void removeOldAlmaInventory()
        {
            connection = new OleDbConnection(CONNECTIONSTRING);
            string query = "DROP TABLE AlmaInventory";
            command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        private void createAlmaTable()
        {
            string create = "CREATE TABLE AlmaInventory([Id] COUNTER, [Name in Alma] TEXT(30), [Item Name] TEXT(30), [Model ID] TEXT(25), [Status Code] TEXT(30), [Status] TEXT(35), [Description] TEXT(10), [Barcode] TEXT(30))";
            connection = new OleDbConnection(CONNECTIONSTRING);
            command = new OleDbCommand(create, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        private string parseText()
        {
            string output = String.Empty;
            StreamReader read = new StreamReader("AlmaDate.txt");
            while (!read.EndOfStream)
            {
                output += read.ReadLine();
            }
            return output;
        }
        public bool checkDate()
        {
            DateTime dt = File.GetLastWriteTime(ALMAEXCEL);
            if (dt.Date != DateTime.Now.Date)
            {
                return false;
            }
            else
                return true;
        }
        public bool checkTime()
        {
            DateTime dt = File.GetLastWriteTime(ALMAEXCEL);
            if (dt.AddMinutes(30).TimeOfDay < DateTime.Now.TimeOfDay)
            {
                timeSinceReport = (DateTime.Now.TimeOfDay - dt.TimeOfDay).Hours.ToString() + " hours " + (DateTime.Now.TimeOfDay - dt.TimeOfDay).Minutes.ToString() + " minutes ago.";
                return false;
            }
            else
                return true;
        }
        private void sortList()
        {
            List<Entry> newList = new List<Entry>();
            entries = getEntries.OrderBy(item => item.modelID).ThenBy(item => item.model).ToList();
        }
        public bool emailReport()
        {
            try
            {
                
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.To.Add("BaileyR@lanecc.edu");
                mail.To.Add("labstaff@lanecc.edu");
                mail.From = new MailAddress("shedinventoryreport@gmail.com");
                mail.Subject = "SHED Inventory";
                mail.Body = "Anything not physically here is marked as not in place since we have no way of knowing if any item not here is lost, missing or checked out without consulting Alma. The purpose of this report is to show discrepancies between what is physically stocked at SHED versus what Alma states.";
                Attachment attachment;
                attachment = new Attachment("ShedInventory.docx");
                mail.Attachments.Add(attachment);
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("shedinventoryreport@gmail.com", "Jamesiscool");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                mail.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147024864)
                {
                    DialogResult r = MessageBox.Show("Please save and close the report document before attempting to email report.", "Send Report?", MessageBoxButtons.RetryCancel);
                    if (r == DialogResult.Retry)
                        emailReport();
                    else if (r == DialogResult.Cancel)
                        return false;               
                }
                return false;
            }
        }
        public void sortWeeklies(List<Entry> inputs)
        {
            List<Entry> newList1 = new List<Entry>();
            List<Entry> newList2 = new List<Entry>();
            char[] idNumber;
            foreach (Entry e in inputs)
            {
                idNumber = e.description.ToCharArray();
                if (idNumber.Contains('P'))
                    newList1.Add(e);
                else
                    newList2.Add(e);
            }
            List<Entry> sortedList = new List<Entry>();
            foreach (Entry k in newList1)
            {
                k.description = k.description.Remove(k.description.Length - 1);
                k.model = int.Parse(k.description);
                sortedList.Add(k);
            }
            newList1 = new List<Entry>();
            newList1 = sortedList.OrderBy(a => a.model).ToList();
            List<Entry> outputs = new List<Entry>();
            foreach (Entry o in newList2)
                entries.Add(o);
            foreach (Entry p in newList1)
            {
                p.description = p.description + 'P';
                entries.Add(p);
            }
        }
        //YOU ARE HERE DINGUS!!!!
        public void getDiscrepancyItems()
        {
            List<Entry> items = new List<Entry>();
            foreach (Entry e in getShedEntries)
            {
                Entry discrepancy = new Entry();
                foreach (Entry p in getAlmaEntries)
                {
                    if (e.barcode == p.barcode)
                        discrepancy = null;                    
                }
                if (discrepancy != null)
                {
                    discrepancy = e;
                    discrepancy.issue = EXISTS_IN_SHED;
                    items.Add(discrepancy);
                }                  
            }
            removeExcessSHeD(items);

        }
        public void getDiscrepancyItems2()
        {
            List<Entry> items = new List<Entry>();
            foreach (Entry e in getAlmaEntries)
            {
                Entry discrepancy = new SHED_Inventory.Entry();
                foreach (Entry p in getShedEntries)
                {
                    if (e.barcode == p.barcode)
                        discrepancy = null;
                }
                if (discrepancy != null)
                {
                    discrepancy = e;
                    discrepancy.issue = NOT_IN_SHED;
                    items.Add(discrepancy);
                }
            }
            syncAlma(items);          
        }
        public void createSHEDInventory()
        {
            List<Entry> twoHourLaptops = getTwoHourLaptops();
            List<Entry> weeklyLaptops = getWeeklyLaptops();
            List<Entry> headphones = getHeadphones();
            List<Entry> flashDrives = getMemory();
            List<Entry> powers = getPowerSupplies();
            shedEntries = new List<Entry>();
            foreach (Entry e in twoHourLaptops)
            {
                shedEntries.Add(e);
            }
            foreach (Entry e in weeklyLaptops)
            {
                shedEntries.Add(e);
            }
            foreach (Entry e in headphones)
            {
                shedEntries.Add(e);
            }
            foreach (Entry e in flashDrives)
            {
                shedEntries.Add(e);
            }
            foreach (Entry e in powers)
            {
                shedEntries.Add(e);
            }
        }
        public void createAlmaInventory()
        {
            almaEntries = new List<Entry>();
            connection = new OleDbConnection(EXCELCONNSTRING);
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [results$]", connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            foreach (DataRow data in dt.Rows)
            {
                Entry e = new Entry();
                e.barcode = data[2].ToString();
                e.description = data[31].ToString();
                e.name = data[0].ToString();
                e.status = data[27].ToString();
                e.almaName = data[17].ToString();
                e.code = data[8].ToString();
                almaEntries.Add(e);
            }
        }
        private void syncAlma(List<Entry> items)
        {
            connection = new OleDbConnection(CONNECTIONSTRING);
            try {
                foreach (Entry i in items) {
                    string query = "UPDATE [2 Hour Laptops] SET Barcode1 = ? WHERE [Name in Alma] = ? AND Description = ?";                                     
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Name in Alma]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                foreach (Entry i in items)
                {
                    string query = "UPDATE [Flash Memory] SET Barcode1 = ? WHERE [Name in Alma] = ? AND Description = ?";
                    connection = new OleDbConnection(CONNECTIONSTRING);
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Name in Alma]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                foreach (Entry i in items)
                {
                    string query = "UPDATE [Headphones] SET Barcode1 = ? WHERE [Name in Alma] = ? AND Description = ?";
                    connection = new OleDbConnection(CONNECTIONSTRING);
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Name in Alma]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                foreach (Entry i in items)
                {
                    string query = "UPDATE [Power Supplies] SET Barcode1 = ? WHERE [Name in Alma] = ? AND Description = ?";
                    connection = new OleDbConnection(CONNECTIONSTRING);
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Name in Alma]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                foreach (Entry i in items)
                {
                    string query = "UPDATE [Project Laptops] SET Barcode1 = ? WHERE [Name in Alma] = ? AND Description = ?";
                    connection = new OleDbConnection(CONNECTIONSTRING);
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Name in Alma]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }    
            catch(Exception ex)
            {
                MessageBox.Show(ex + "");
            }                
        }
        private void removeExcessSHeD(List<Entry> items)
        {
            connection = new OleDbConnection(CONNECTIONSTRING);
            try
            {
                foreach (Entry i in items)
                {
                    string query = "DELETE * FROM [2 Hour Laptops] WHERE Barcode1 = ? AND [Item Name] = ? AND Description = ?";
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Item Name]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                foreach (Entry i in items)
                {
                    string query = "DELETE * FROM [Flash Memory] WHERE Barcode1 = ? AND [Item Name] = ? AND Description = ?";
                    connection = new OleDbConnection(CONNECTIONSTRING);
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Item Name]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                foreach (Entry i in items)
                {
                    string query = "DELETE * FROM [Headphones] WHERE Barcode1 = ? AND [Item Name] = ? AND Description = ?";
                    connection = new OleDbConnection(CONNECTIONSTRING);
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Item Name]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                foreach (Entry i in items)
                {
                    string query = "DELETE * FROM [Power Supplies] WHERE Barcode1 = ? AND [Item Name] = ? AND Description = ?";
                    connection = new OleDbConnection(CONNECTIONSTRING);
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Item Name]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                foreach (Entry i in items)
                {
                    string query = "DELETE * FROM [Project Laptops] WHERE Barcode1 = ? AND [Item Name] = ? AND Description = ?";
                    connection = new OleDbConnection(CONNECTIONSTRING);
                    command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Barcode1", i.barcode);
                    command.Parameters.AddWithValue("@[Item Name]", i.name);
                    command.Parameters.AddWithValue("@Description", i.description);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }
    }
}
