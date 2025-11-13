using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Azure.Cosmos;


namespace Cloud
{
    public partial class Form1 : Form
    {
        // get dev data
        private string devId = ConfigurationManager.AppSettings["devId"];
        private string devName = ConfigurationManager.AppSettings["devName"];
        private string devMail = ConfigurationManager.AppSettings["devMail"];

        // get env data
        private string envType = ConfigurationManager.AppSettings["EnvType"];
        private string uri = ConfigurationManager.AppSettings["URI"];
        private string primaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

        // cosmos
        private CosmosClient myCosmosClient;

        public Form1()
        {
            InitializeComponent();
        }


        private void btn_CreateCosmosClient_Click(object sender, EventArgs e)
        {
            try
            {
                myCosmosClient = new CosmosClient(uri, primaryKey);

                btn_CreateCosmosClient.Enabled = false;
                btn_CreateCosmosClient.BackColor = Color.Green;


                MessageBox.Show("Cosmos Client Creation Succeeded",
                   "Cosmos Client was Created ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Cosmos Client Creation Failed with the following Error: " + ex.Message,
                    "Cosmos Client Creation Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_DevName.Text = devName;
            textBox_DevId.Text = devId;
            textBox_DevMail.Text = devMail;
            textBox_EnvType.Text = envType;
            textBox_URI.Text = uri;
            textBox_PrimaryKey.Text = primaryKey;
        }

        private async void btn_CreateDataInCloud_Click(object sender, EventArgs e)
        {
            // read the info from the screen
            string dbNameToCreate = textBox_DatabaseInput.Text;
            string containerNameToCreate = textBox_ContainerInput.Text;

            await CreateDBandContainerInCloudAsync(dbNameToCreate, containerNameToCreate);


        }

        private async Task CreateDBandContainerInCloudAsync(string dbNameToCreate, string containerNameToCreate)
        {

            // Stage 1: DB Creation
            if (string.IsNullOrEmpty(dbNameToCreate)) return;

            DatabaseResponse databaseResponse =
            await myCosmosClient.CreateDatabaseIfNotExistsAsync(dbNameToCreate);

            System.Net.HttpStatusCode dbCreationStatus = databaseResponse.StatusCode;

            if (dbCreationStatus == System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show("DB: '" + dbNameToCreate + "' was Created!",
                 "Creation Succeeded ",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
            else if (dbCreationStatus == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("DB: '" + dbNameToCreate + "' was not created, its already exists",
                 "DB Already Exists ",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);

            }
            else
            {
                MessageBox.Show("DB: '" + dbNameToCreate + "' was not created, and we got the following status code: " + dbCreationStatus,
                 "DB Creation Failed ",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error);

                return;

            }

            // Stage 2: Container / Table Creation

            if (string.IsNullOrEmpty(containerNameToCreate)) return;

            Database dbObj = databaseResponse.Database;

            ContainerResponse containerResponse =
            await dbObj.CreateContainerIfNotExistsAsync(containerNameToCreate, "/id");

            System.Net.HttpStatusCode tableCreationStatus = containerResponse.StatusCode;

            if (tableCreationStatus == System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show("Table: '" + containerNameToCreate + "' was Created! in DB '" + dbNameToCreate + "'",
                 "Creation Succeeded ",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
            else if (tableCreationStatus == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Table: '" + containerNameToCreate + "' was not created, its already exists in DB: '" + dbNameToCreate + "'",
                 "Table Already Exists ",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);

            }
            else
            {
                MessageBox.Show("Table: '" + containerNameToCreate + "' was not created, and we got the following status code: " + tableCreationStatus,
                 "Table Creation Failed ",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error);


            }



        }

        private void btn_databaseCounter_Click(object sender, EventArgs e)
        {
        }
    }
}
