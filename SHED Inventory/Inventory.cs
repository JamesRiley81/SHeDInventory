using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace SHED_Inventory
{
    public partial class Inventory : Form
    {
        private const string DEVICEHEADER = "Device";
        private const string MODELHEADER = "Model #";
        private const string IDHEADER = "Device #";
        private const string BARCODEHEADER = "Barcode #";
        private const string INVENTORYERROR = " has yet to be inventoried, still create report?";
        private const string TWOHOURS = "2 Hour Laptops";
        private const string WEEKLY = "Weekly Laptops";
        private const string ACPOWER = "Power Supplies";
        private const string HEADPHONES = "Headphones";
        private const string FLASH = "Flash Memory";
        private const string SEPARATOR = "-";
        private const string TAGYELLOW = "Yellow Tag";
        private const string TAGPURPLE = "Purple";
        private const string REPORTSPROMPT = "Report has not been run yet, are you sure you wish to exit?";
        private const string FAILURE = "Could not successfully create report.  ";
        private const string NOTSAVED = "You have not saved inventory for selected category yet, would you like to select a new category anyway?";
        private const string EMAILPROMPT = "If discrepancies still exist click Yes to email report.  Select No if no discrepancies exist.";
        private bool start = true;
        private bool dataSaved = false;
        bool generated = false;
        bool weeklyLaps = false;
        bool twohourlaps = false;
        bool powers = false;
        bool phones = false;
        bool flashes = false;
        int lastSelection = -1;
        bool wentBack = false;
        private int count = 0;
        bool autoIncrement = false;
        Database d = new Database();
        public Inventory()
        {
            InitializeComponent();                       
        }
        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (wentBack == false)
            {
                dataSaved = false;
                if (start == false)
                {
                    if (dataSaved == false && autoIncrement == false)
                    {
                        DialogResult r;
                        r = MessageBox.Show(NOTSAVED, "New Category?", MessageBoxButtons.YesNo);
                        if (r == DialogResult.No)
                        {
                            wentBack = true;
                            categoryComboBox.SelectedIndex = lastSelection;                          
                            return;
                        }
                    }
                }
                autoIncrement = false;
                inventoryListBox.Items.Clear();
                string item = categoryComboBox.SelectedItem.ToString();
                List<Entry> items = d.getInventory(item);
                List<Entry> status = new List<Entry>();
                if (item == TWOHOURS)
                {
                    modelLabel.Show();
                }
                else if (item == WEEKLY)
                {
                    modelLabel.Show();
                }
                else if (item == ACPOWER)
                {
                    modelLabel.Show();
                }
                else if (item == HEADPHONES)
                {
                    modelLabel.Show();
                }
                else if (item == FLASH)
                {
                    modelLabel.Hide();
                }
                foreach (Entry output in items)
                {
                    string name = output.name;
                    string model = output.modelID;
                    string description = output.description;
                    string barcode = output.barcode;
                    int tagspacing = 0;
                    if (model == String.Empty)
                    {
                        if (description.Length == 1)
                            inventoryListBox.Items.Add(name.PadRight(45) + '\t' + SEPARATOR + description.PadRight(13) + '\t' + '\t' + (SEPARATOR + barcode).PadLeft(20));
                        else
                            inventoryListBox.Items.Add(name.PadRight(45) + '\t' + SEPARATOR + description.PadRight(12) + '\t' + '\t' + (SEPARATOR + barcode).PadLeft(20));
                    }
                    else
                    {
                        if (description.Length == 1)
                            inventoryListBox.Items.Add(name.PadRight(20) + SEPARATOR + model.PadRight(20 + tagspacing) + '\t' + SEPARATOR + description.PadRight(13) + '\t' + '\t' + (SEPARATOR + output.barcode).PadLeft(20));
                        else
                            inventoryListBox.Items.Add(name.PadRight(20) + SEPARATOR + model.PadRight(20 + tagspacing) + '\t' + SEPARATOR + description.PadRight(12) + '\t' + '\t' + (SEPARATOR + output.barcode).PadLeft(20));
                    }
                    if (output.status == "Item in place")
                        output.checkedIn = true;
                    else
                        output.checkedIn = false;
                    status.Add(output);
                }
                setChecks(status);
                inventoryListBox.Focus();
                count++;
                if (count == 1)
                    start = false;
            }
            else
            {
                wentBack = false;
                return;
            }                 
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            if (generated == false)
            {
                DialogResult r = MessageBox.Show(REPORTSPROMPT, "Close Program?", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            else
                this.Close();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            List<Entry> entries = new List<Entry>();
            try
            {
                for (int i = 0; i < inventoryListBox.Items.Count; i++)
                {
                    Entry item = new Entry();
                    CheckState st = inventoryListBox.GetItemCheckState(i);
                    string input = inventoryListBox.Items[i].ToString();
                    string[] inputs = input.Split('-');
                    if (categoryComboBox.SelectedItem.ToString() == FLASH)
                    {
                        item.barcode = inputs[2];
                    }
                    else
                        item.barcode = inputs[3];
                    if (st == CheckState.Checked)
                    {
                        item.checkedIn = true;
                    }
                    else
                    {
                        item.checkedIn = false;
                    }
                    entries.Add(item);
                }
                string result = d.UpdateTable(categoryComboBox.SelectedItem.ToString(), entries);
                if (categoryComboBox.SelectedItem.ToString() == WEEKLY)
                {
                    weeklyLaps = true;
                }
                else if (categoryComboBox.SelectedItem.ToString() == TWOHOURS)
                {
                    twohourlaps = true;
                }
                else if (categoryComboBox.SelectedItem.ToString() == HEADPHONES)
                {
                    phones = true;
                }
                else if(categoryComboBox.SelectedItem.ToString() == FLASH)
                {
                    flashes = true;
                }
                else if (categoryComboBox.SelectedItem.ToString() == ACPOWER)
                {
                    powers = true;
                }
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
            dataSaved = true;
            moveForward();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            DialogResult r;

            string s = string.Empty;
            do {
                r = MessageBox.Show("Complete Inventory Report?", "Inventory Sign off", MessageBoxButtons.OKCancel);
                if (r == DialogResult.Cancel)
                {
                    return;
                }
                s = Interaction.InputBox("", "");              
            } while(s == string.Empty);
            
            if (powers == false)
            {
                r = MessageBox.Show(ACPOWER + INVENTORYERROR, "Send Report?", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    generated = true;
                    if (d.reportInventory(s))
                    {
                        showReport();
                    }
                }
                else
                    return;
            }
            else if (twohourlaps == false)
            {
                r = MessageBox.Show(TWOHOURS + INVENTORYERROR, "Send Report?", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    generated = true;
                    if (d.reportInventory(s))
                    {
                        showReport();
                    }
                }
                else
                    return;
            }
            else if (weeklyLaps == false)
            {
                r = MessageBox.Show(WEEKLY + INVENTORYERROR, "Send Report?", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    generated = true;
                    if (d.reportInventory(s))
                    {
                        showReport();
                    }
                }
                else
                    return;
            }
            else if (phones == false)
            {
                r = MessageBox.Show(HEADPHONES + INVENTORYERROR, "Send Report?", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    generated = true;
                    if (d.reportInventory(s))
                    {
                        showReport();
                    }
                }
                else
                    return;
            }
            else if (flashes == false)
            {
                r = MessageBox.Show(FLASH + INVENTORYERROR, "Send Report?", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    generated = true;
                    if (d.reportInventory(s))
                        showReport();
                }
                else
                    return;
            }
            else
            {
                generated = true;
                if (d.reportInventory(s))
                {
                    showReport();
                }
            }
        }
        private void Inventory_Load(object sender, EventArgs e)
        {
            try
            {
                d.checkDate();
                if (!d.checkDate())
                {
                    MessageBox.Show("Uploaded Alma Inventory is from previous date, please ask Raymond Bailey or Shannon Mootz to upload new inventory report.");
                    Close();
                }
                if (!d.checkTime())
                {
                    DialogResult r = MessageBox.Show("Last uploaded Alma Inventory report was created " +  d.timeSinceReport + "\r\nWould you like to still use this report?  \r\nIf not, please click 'No' and ask Raymond Bailey or Shannon Mootz to upload new inventory report.", "Use sheet?", MessageBoxButtons.YesNo);
                    if (r == DialogResult.No)
                        Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to file. " + ex);
            }
            try
            {
                d.setAlmaInventory();
            }
            catch (Exception ex)
            {
                return;
            }
            categoryComboBox.SelectedIndex = -1;                
        }
       private void showReport()
        {
            try
            {
                string fileName = "ShedInventory.docx";
                Process.Start(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(FAILURE + ex);
            }
            emailReport();
        }
        private void setChecks(List<Entry> items)
        {

            for (int i = 0; i < items.Count; i++)
            {
                CheckState st;
                if (items[i].checkedIn == true)
                    st = CheckState.Checked;
                else
                    st = CheckState.Unchecked;
                inventoryListBox.SetItemCheckState(i, st);
            }
        }
        private void moveForward()
        {
            int selection = categoryComboBox.SelectedIndex;
            if (selection == 4)
            {
                categoryComboBox.SelectedIndex = 4;
            }
            else
            {
                autoIncrement = true;
                categoryComboBox.SelectedIndex = selection + 1;
            }
        }
        private void emailReport()
        {
            DialogResult r = MessageBox.Show(EMAILPROMPT, "Send Report?", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
                d.emailReport();
            else if (r == DialogResult.No)
                this.Close();
            else
                return;
        }

        private void categoryComboBox_Click(object sender, EventArgs e)
        {
            lastSelection = categoryComboBox.SelectedIndex;
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            showReport();
        }
    }

}
