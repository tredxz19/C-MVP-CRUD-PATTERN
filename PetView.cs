using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSHARPCRUDMVC.Views
{
    public partial class PetView : Form,IPetView
    {
        //Fields
        private string message;
        private bool isSuccessful;
        private bool isEdit;
        public static PetView newConnection;

        //Constructor
        public PetView()
        {
            newConnection = this;
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPagePetDetails);
            btnClose.Click += delegate { this.Close(); };
        }

        private void AssociateAndRaiseViewEvents()
        {
            try
            {
                //Search
                btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
                txtSearch.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                        SearchEvent?.Invoke(this, EventArgs.Empty);
                };
                //Add new
                btnAddNew.Click += delegate
                {
                    AddNewEvent?.Invoke(this, EventArgs.Empty);
                    tabControl1.TabPages.Remove(tabPagePetList);
                    tabControl1.TabPages.Add(tabPagePetDetails);
                    tabPagePetDetails.Text = "Add new pet";
                };
                //Edit
                btnEdit.Click += delegate
                {
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    tabControl1.TabPages.Remove(tabPagePetList);
                    tabControl1.TabPages.Add(tabPagePetDetails);
                    tabPagePetDetails.Text = "Edit pet";
                };
                //Save Change
                btnSave.Click += delegate
                {
                    SaveEvent?.Invoke(this, EventArgs.Empty);
                    if (isSuccessful)
                    {
                        tabControl1.TabPages.Remove(tabPagePetDetails);
                        tabControl1.TabPages.Add(tabPagePetList);
                    }
                    MessageBox.Show(message);
                };
                //Cancel
                btnCancel.Click += delegate
                {
                    CancelEvent?.Invoke(this, EventArgs.Empty);
                    tabControl1.TabPages.Remove(tabPagePetDetails);
                    tabControl1.TabPages.Add(tabPagePetList);
                };
                //Delete
                btnDelete.Click += delegate
                {
                    var Result = MessageBox.Show("Are you sure do you want to delete the selected pet?", "Warning",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Result == DialogResult.Yes)
                    {
                        DeleteEvent?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show(Message);
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Properties
        public string PetId
        {
            get { return txtPetId.Text; }
            set { txtPetId.Text = value; }
        }

        public string PetName
        {
            get { return txtPetName.Text; }
            set { txtPetName.Text = value; }
        }

        public string PetType
        {
            get { return txtPetType.Text; }
            set { txtPetType.Text = value; }
        }

        public string PetColour
        {
            get { return txtPetColour.Text; }
            set { txtPetColour.Text = value; }
        }   
        public string SearchValue
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        public bool IsEdit 
             {
            get { return isEdit; }
            set { isEdit = value; }
             }

        public bool IsSuccessful
        {
            get { return isSuccessful ; }
            set { isSuccessful = value; }
        }

      

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

       


        //Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //Methods
        public void SetPetListBindingSource(BindingSource petList)
        {
            dataGridView1.DataSource = petList;
        }

        //Singleton Pattern (Open a single form of instance)
        public static PetView instance;
        public static PetView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new PetView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
             }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }


        private void PetView_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {

        }

        private void tabPagePetList_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
