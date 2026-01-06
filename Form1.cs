using Cloud.DataStructures;
using Cloud.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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


        // logs
        private string logFolderName = ConfigurationManager.AppSettings["Log Folder"];
        private string logFileName = ConfigurationManager.AppSettings["Log File Name"];

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

            WriteToLog("Starting my Cloud Application...");
        }

        private void WriteToLog(string logMessage)
        {
            try
            {
                string logFullPath = Path.Combine(logFolderName, logFileName);
                //string fullLogMessage = $"{DateTime.Now.ToShortDateString()}: {logMessage}\n";
                string fullLogMessage = $"{DateTime.Now.ToLongDateString()}, {DateTime.Now.ToShortTimeString()}: {logMessage}\n";

                if (!File.Exists(logFullPath))
                {

                    if (!Directory.Exists(logFolderName))
                        Directory.CreateDirectory(logFolderName);

                    File.Create(logFullPath);
                }

                File.AppendAllText(logFullPath, fullLogMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("The operation '" + logMessage + "' was not appended to the log, we got: " + ex.Message);
            }
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

                WriteToLog("DB '" + dbNameToCreate + "' was created");
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

        private async void btn_GetDBsNames_Click(object sender, EventArgs e)
        {
            comboBox_DBsNames.DataSource = await getDBsNamesFromCloudAccountAsync();
        }

        private async Task<List<string>> getDBsNamesFromCloudAccountAsync()
        {
            List<string> databasesNames = new List<string>();

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    databasesNames.Add(currentDBProp.Id);
                }
            }

            return databasesNames;
        }

        private async void btn_CountDBs_Click(object sender, EventArgs e)
        {
            int numOfDBs = await countDBsInCloudAccountAsync();
            textBox_DBsCounter.Text = numOfDBs.ToString();
        }

        private async Task<int> countDBsInCloudAccountAsync()
        {
            int numOfDbs = 0;

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    numOfDbs++;
                }
            }

            return numOfDbs;
        }


        private async void btn_GetTablesNames_Click(object sender, EventArgs e)
        {
            comboBox_TablesNames.DataSource = await getTablesNamesFromCloudAccountAsync();
        }



        // Returns a list of string that each one of its members is with the following format:
        // DBName - Table Name
        private async Task<List<string>> getTablesNamesFromCloudAccountAsync()
        {

            List<string> results = new List<string>();

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            results.Add(currentDBProp.Id + " - " + currentTableProp.Id);
                        }
                    }
                }
            }

            return results;
        }

        private async void btn_CountTables_Click(object sender, EventArgs e)
        {
            int tablesCounter = await countTablesInCloudAccountAsync();
            textBox_TablesCounter.Text = tablesCounter.ToString();
        }

        private async Task<int> countTablesInCloudAccountAsync()
        {
            int tablesCounter = 0;

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            tablesCounter++;
                        }
                    }
                }
            }

            return tablesCounter;
        }

        private async void btn_SearchDB_Click(object sender, EventArgs e)
        {

            comboBox_SearchedDBs.DataSource = await getDBsThatStartsWithTheInputAsync();

        }

        private async Task<List<string>> getDBsThatStartsWithTheInputAsync()
        {
            List<string> databasesNames = new List<string>();

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    if (currentDBProp.Id.ToLower().StartsWith(textBox_SearchDB.Text.ToLower()))
                    {
                        databasesNames.Add(currentDBProp.Id);
                    }
                }
            }

            return databasesNames;
        }

        private async void btn_DoesDBExist_Click(object sender, EventArgs e)
        {
            if (await DoesDBExistInCloudAsync())
                textBox_DoesDBExist.Text = "Database Exists in the Cloud!";
            else
                textBox_DoesDBExist.Text = "Database Does Not Exist in the Cloud!";



        }

        private async Task<bool> DoesDBExistInCloudAsync()
        {


            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    if (currentDBProp.Id.ToLower().Equals(textBox_CheckDB.Text.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private async void btn_DBsWithTablesCount_Click(object sender, EventArgs e)
        {
            comboBox_DBsWithTablesCount.DataSource = await getDBsNamesWithTheirAmountOfTablesAsync();
        }

        private async Task<List<string>> getDBsNamesWithTheirAmountOfTablesAsync()
        {
            List<string> databasesNames = new List<string>();


            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    int tablesCounter = 0;

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            tablesCounter++;
                        }
                    }

                    databasesNames.Add(currentDBProp.Id + " - " + tablesCounter + " Tables");
                }
            }

            return databasesNames;
        }

        private async void btn_DBsContainTable_Click(object sender, EventArgs e)
        {
            List<string> databasesNames = await getDBsNamesThatContainsTableAsync();
            List<string> emptyComboBox = new List<string>();
            emptyComboBox.Add("No such DBs exist");

            comboBox_DBsContainTable.DataSource = databasesNames.Count > 0 ? databasesNames : emptyComboBox;
        }


        private async Task<List<string>> getDBsNamesThatContainsTableAsync()
        {
            List<string> databasesNames = new List<string>();


            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();


                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            if (currentTableProp.Id.ToLower().Equals(textBox_TableNameInput.Text.ToLower()))
                            {
                                databasesNames.Add(currentDBProp.Id);
                            }
                        }
                    }

                }
            }

            return databasesNames;
        }

        private async void btn_TablesLength_Click(object sender, EventArgs e)
        {
            List<string> databasesNames = await getTablesWithLengthAsync();
            List<string> emptyComboBox = new List<string>();
            emptyComboBox.Add("No Tables Found");

            comboBox_TablesLength.DataSource = databasesNames.Count > 0 ? databasesNames : emptyComboBox;
        }

        private async Task<List<string>> getTablesWithLengthAsync()
        {
            if (String.IsNullOrEmpty(textBox_TablesLength.Text)) return new List<string>();
            List<string> databasesNames = new List<string>();


            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();


                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            if (currentTableProp.Id.Length > Convert.ToInt32(textBox_TablesLength.Text))
                            {
                                databasesNames.Add(currentDBProp.Id + " - " + currentTableProp.Id);
                            }
                        }
                    }

                }
            }

            return databasesNames;
        }

        private async void btn_DBsWithConditions_Click(object sender, EventArgs e)
        {
            List<string> databasesNames = await getDBsWithConditionAsync();
            List<string> emptyComboBox = new List<string>();
            emptyComboBox.Add("No such DBs exist");

            comboBox_DBsWithConditions.DataSource = databasesNames.Count > 0 ? databasesNames : emptyComboBox;
        }


        private async Task<List<string>> getDBsWithConditionAsync()
        {
            List<string> databasesNames = new List<string>();


            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();



            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {

                    if (currentDBProp.Id.Length % 2 != 0)
                    {

                        int tablesCounter = 0;

                        Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                        FeedIterator<ContainerProperties> tableIterator =
                        databaseRefObj.GetContainerQueryIterator<ContainerProperties>();


                        while (tableIterator.HasMoreResults)
                        {
                            foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                            {
                                tablesCounter++;
                            }
                        }

                        if (tablesCounter == 0 || tablesCounter > 2)
                            databasesNames.Add(currentDBProp.Id + " - " + tablesCounter + " Tables");


                    }
                }
            }

            return databasesNames;
        }

        private async void btn_TablesLengthX_Click(object sender, EventArgs e)
        {
            string databasesNames = await getDBsWithLengthAsync();

            textBox_DBsWithLengthX.Text = databasesNames.Length > 0 ? databasesNames : "No DBs Found With " + textBox_TablesLengthX.Text + " Tables";

        }


        private async Task<string> getDBsWithLengthAsync()
        {

            if (String.IsNullOrEmpty(textBox_TablesLengthX.Text))
            {
                return "No DBs Found With 0 Tables";
            }


            string databasesNames = "";

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    int tablesCounter = 0;

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();


                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            tablesCounter++;

                        }

                    }

                    if (tablesCounter == Convert.ToInt32(textBox_TablesLengthX.Text))
                    {
                        databasesNames += currentDBProp.Id + " ";
                    }

                }
            }

            return databasesNames;
        }

        private async void btn_Ex16SearchForDBs_Click(object sender, EventArgs e)
        {
            textBox_Ex16DBs.Text = await getDBsNamesWithTablesContainLengthAsync();

        }

        private async Task<string> getDBsNamesWithTablesContainLengthAsync()
        {
            if (String.IsNullOrEmpty(textBox_Ex16Length.Text)) return "";

            string databasesNames = "";


            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();


                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            if (currentTableProp.Id.Length > Convert.ToInt32(textBox_Ex16Length.Text))
                            {
                                databasesNames += currentDBProp.Id + " ";
                                break;

                            }
                        }
                    }

                }
            }

            return databasesNames;
        }

        // Ex17-1
        private async void btn_Ex17_1_Click(object sender, EventArgs e)
        {

            textBox_Ex17_1_Results.Text = await getDBsThatStartsWithTheInputAndTableAsync();

        }


        private async Task<string> getDBsThatStartsWithTheInputAndTableAsync()
        {

            if (textBox_Ex17_1.Text.Length == 0) return "";

            string databasesNames = "";

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    if (currentDBProp.Id.ToLower().StartsWith(textBox_Ex17_1.Text.ToLower()))
                    {
                        Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                        FeedIterator<ContainerProperties> tableIterator =
                        databaseRefObj.GetContainerQueryIterator<ContainerProperties>();


                        while (tableIterator.HasMoreResults)
                        {
                            foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                            {
                                if (currentTableProp.Id.ToLower().StartsWith(textBox_Ex17_1.Text.ToLower()))
                                {
                                    databasesNames += currentDBProp.Id + " ";
                                    break;

                                }
                            }
                            break;
                        }


                    }
                }
            }

            return databasesNames;
        }


        // Ex17-2
        private async void btn_Ex17_2_Click(object sender, EventArgs e)
        {
            textBox_Ex17_2_Results.Text = await getLongestDBThatStartsWithTheInputAndTableAsync();
        }

        private async Task<string> getLongestDBThatStartsWithTheInputAndTableAsync()
        {

            if (textBox_Ex17_2.Text.Length == 0) return "";

            string databasesNames = "";
            int maxLength = 0;

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();


            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {

                    if (currentDBProp.Id.ToLower().StartsWith(textBox_Ex17_2.Text.ToLower()))
                    {
                        Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                        FeedIterator<ContainerProperties> tableIterator =
                        databaseRefObj.GetContainerQueryIterator<ContainerProperties>();


                        while (tableIterator.HasMoreResults)
                        {
                            foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                            {
                                if (currentTableProp.Id.ToLower().StartsWith(textBox_Ex17_2.Text.ToLower()))
                                {
                                    if (currentDBProp.Id.Length >= maxLength)
                                        maxLength = currentDBProp.Id.Length;

                                    break;

                                }
                            }
                            break;
                        }
                    }
                }
            }

            dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {

                    if (currentDBProp.Id.ToLower().StartsWith(textBox_Ex17_2.Text.ToLower()))
                    {
                        Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                        FeedIterator<ContainerProperties> tableIterator =
                        databaseRefObj.GetContainerQueryIterator<ContainerProperties>();


                        while (tableIterator.HasMoreResults)
                        {
                            foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                            {
                                if (currentTableProp.Id.ToLower().StartsWith(textBox_Ex17_2.Text.ToLower()))
                                {
                                    if (currentDBProp.Id.Length == maxLength)
                                        databasesNames += currentDBProp.Id + " ";

                                    break;

                                }
                            }
                            break;
                        }
                    }
                }
            }

            return databasesNames;
        }


        // Ex20
        private async void btn_Ex20_Click(object sender, EventArgs e)
        {
            textBox_Ex20.Text = await GetDbsWithMostTablesAsync();
        }

        private async Task<string> GetDbsWithMostTablesAsync()
        {

            string databasesNames = "";

            bool isThereDB = false;
            int tablesMaxCount = 0;

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    isThereDB = true;
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);
                    int tableCounter = 0;

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            tableCounter++;
                        }
                    }

                    if (tableCounter >= tablesMaxCount)
                        tablesMaxCount = tableCounter;
                }
            }

            dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        int tableCounter = 0;


                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            tableCounter++;

                        }

                        if (tableCounter == tablesMaxCount)
                            databasesNames += currentDBProp.Id + " ";
                    }
                }
            }

            if (!isThereDB)
                return "No database exists in the current cloud account";

            if (tablesMaxCount == 0)
                return "There are no tables in any of the databases.";

            string dbNames = "The following databases contain " + tablesMaxCount + " tables: " + databasesNames;

            return dbNames;
        }

        // Ex 25 Driver Data Demo
        private async void btn_SaveDriverDataInCloud_Click(object sender, EventArgs e)
        {
            // Read the destination within cloud
            string db = textBox_DBNameEx25.Text;
            string container = textBox_ContainerNameEx25.Text;

            // Get Driver details: ( Hardcoded )
            Driver driver = new Driver();
            driver.id = Guid.NewGuid().ToString();
            driver.Name = "Hasan";
            driver.Age = 23.5;
            driver.YearsInService = 6;
            driver.Passengers = new Passenger[2];

            Passenger firstPassenger = new Passenger { Name = "Ward", SpecialRequests = "Be on time", Age = 21 };
            Passenger secondPassenger = new Passenger { Name = "Essa", SpecialRequests = "Wait for me", Age = 22 };

            driver.Passengers[0] = firstPassenger;
            driver.Passengers[1] = secondPassenger;

            driver.CabStations = new CabStation[1];
            CabStation myStation = new CabStation { address = "Haifa" };
            driver.CabStations[0] = myStation;

            // Save the above data with the defined cloud destination
            await SaveDriverDataIntoCloudAsync(db, container, driver);
        }


        private async Task SaveDriverDataIntoCloudAsync(string db, string container, Driver driverData)
        {
            DatabaseResponse databaseResponse = await myCosmosClient.CreateDatabaseIfNotExistsAsync(db);

            if (databaseResponse.StatusCode == System.Net.HttpStatusCode.OK ||
                databaseResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Database dbRefObj = databaseResponse.Database;
                ContainerResponse containerResponse = await dbRefObj.CreateContainerIfNotExistsAsync(container, "/id");

                if (containerResponse.StatusCode == System.Net.HttpStatusCode.OK ||
                    containerResponse.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Microsoft.Azure.Cosmos.Container containerRefObj = containerResponse.Container;
                    await containerRefObj.CreateItemAsync<Driver>(driverData);

                }
            }
        }

        // Ex 25 Person Data
        private async void btn_Ex25_PersonData_Click(object sender, EventArgs e)
        {
            string db = textBox_Ex25_DBName.Text;
            string container = textBox_Ex25_ContainerName.Text;


            // Person Data (Hardcoded)
            Person p1 = new Person();
            p1.id = Guid.NewGuid().ToString();
            p1.Name = textBox_Ex25_PersonName.Text;
            p1.Age = 23.6;
            p1.EyesColor = "Brown";

            await SavePersonDataIntoCloudAsync(db, container, p1);
        }

        private async Task SavePersonDataIntoCloudAsync(string db, string container, Person personData)
        {
            DatabaseResponse databaseResponse = await myCosmosClient.CreateDatabaseIfNotExistsAsync(db);

            if (databaseResponse.StatusCode == System.Net.HttpStatusCode.OK ||
                databaseResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Database dbRefObj = databaseResponse.Database;
                ContainerResponse containerResponse = await dbRefObj.CreateContainerIfNotExistsAsync(container, "/id");

                if (containerResponse.StatusCode == System.Net.HttpStatusCode.OK ||
                    containerResponse.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Microsoft.Azure.Cosmos.Container containerRefObj = containerResponse.Container;
                    await containerRefObj.CreateItemAsync<Person>(personData);

                }
            }
        }

        // Ex 26
        private async void btn_GetDBNamesForEx26_Click(object sender, EventArgs e)
        {
            comboBox_DBsNamesForEx26.DataSource = await getDBsNamesFromCloudAccountAsync();
        }

        private async void comboBox_SelectedIndexChangedEx26(object sender, EventArgs e)
        {
            // Get the tables names only for the selected database.
            string dbName = comboBox_DBsNamesForEx26.Text;
            comboBox_ContainersForEx26.DataSource = await getTablesNamesOfSelectedDBAsync(dbName);
        }

        private async Task<List<string>> getTablesNamesOfSelectedDBAsync(string selectedDB)
        {

            List<string> results = new List<string>();
            Database databaseRefObj = myCosmosClient.GetDatabase(selectedDB);

            FeedIterator<ContainerProperties> tableIterator =
            databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

            while (tableIterator.HasMoreResults)
            {
                foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                {
                    results.Add(currentTableProp.Id);
                }
            }

            return results;
        }

        private void btn_LoadJsonIntoScreen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select a JSON that contains student's data...";
            openFile.Filter = "JSON files(*.json)|*.json";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string studentsDataAsString = File.ReadAllText(openFile.FileName);
                richTextBox_JsonData.Text = studentsDataAsString;
            }
            else
            {
                MessageBox.Show("No file was selected", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btn_PerformSelectedActivity_Click(object sender, EventArgs e)
        {
            // Read the user choice
            string selectedActivity = radioButton_Delete.Checked ? CloudActivityTypes.Delete.ToString() :
                                      radioButton_Replace.Checked ? CloudActivityTypes.Replace.ToString() :
                                       CloudActivityTypes.Insert.ToString();

            // Read the selected destination (DB, Table)
            string db = comboBox_DBsNamesForEx26.Text;
            string table = comboBox_ContainersForEx26.Text;

            // Read the data
            string studentsAsString = richTextBox_JsonData.Text;
            List<Student> students = Student.ConvertStringIntoList(studentsAsString);

            // Perform activity in cloud
            await performActivityInCloudAsync(selectedActivity, db, table, students);


        }

        private async Task performActivityInCloudAsync(string selectedActivity, string db, string table, List<Student> students)
        {

            Microsoft.Azure.Cosmos.Container containerRefObj = myCosmosClient.GetContainer(db, table);
            bool isStudentExist;

            foreach (Student student in students)
            {
                try
                {
                    Student s = await containerRefObj.ReadItemAsync<Student>(student.id, new PartitionKey(student.id));
                    isStudentExist = true;
                }
                catch
                {
                    isStudentExist = false;
                }

                if (selectedActivity == CloudActivityTypes.Insert.ToString() && !isStudentExist)
                    await containerRefObj.CreateItemAsync<Student>(student);
                else if (selectedActivity == CloudActivityTypes.Delete.ToString() && isStudentExist)
                    await containerRefObj.DeleteItemAsync<Student>(student.id, new PartitionKey(student.id));
                else if (selectedActivity == CloudActivityTypes.Replace.ToString() && isStudentExist)
                    await containerRefObj.ReplaceItemAsync<Student>(student, student.id, new PartitionKey(student.id));



            }
        }



        // Ex28

        private async void btn_GetDBNamesForEx28_Click(object sender, EventArgs e)
        {
            comboBox_DBsNamesForEx28.DataSource = await getDBsNamesFromCloudAccountAsync();

        }

        private async void comboBox_SelectedIndexChangedEx28(object sender, EventArgs e)
        {
            // Get the tables names only for the selected database.
            string dbName = comboBox_DBsNamesForEx28.Text;
            comboBox_ContainersForEx28.DataSource = await getTablesNamesOfSelectedDBAsync(dbName);
        }

        private async void btn_PerformSelectedActivity_Ex28_Click(object sender, EventArgs e)
        {

            // Read the user choice
            string selectedActivity = radioButton_Delete_Ex28.Checked ? "Delete" :
                                      radioButton_Replace_Ex28.Checked ? "Replace" :
                                       "Insert";

            // Read the selected destination (DB, Table)
            string db = comboBox_DBsNamesForEx28.Text;
            string table = comboBox_ContainersForEx28.Text;

            // Read the data
            string businessAsString = richTextBox_JsonData_Ex28.Text;
            List<Business> business = Business.ConvertStringIntoList(businessAsString);

            // Perform activity in cloud
            await performActivityForBusinessInCloudAsync(selectedActivity, db, table, business);

        }

        private async Task performActivityForBusinessInCloudAsync(string selectedActivity, string db, string table, List<Business> businesses)
        {
            Microsoft.Azure.Cosmos.Container containerRefObj = myCosmosClient.GetContainer(db, table);
            bool isBusinessExist;

            foreach (Business business in businesses)
            {
                try
                {
                    Business s = await containerRefObj.ReadItemAsync<Business>(business.id, new PartitionKey(business.id));
                    isBusinessExist = true;
                }
                catch
                {
                    isBusinessExist = false;
                }

                if (selectedActivity == "Insert" && !isBusinessExist)
                    await containerRefObj.CreateItemAsync<Business>(business);
                else if (selectedActivity == "Delete" && isBusinessExist)
                    await containerRefObj.DeleteItemAsync<Business>(business.id, new PartitionKey(business.id));
                else if (selectedActivity == "Replace" && isBusinessExist)
                    await containerRefObj.ReplaceItemAsync<Business>(business, business.id, new PartitionKey(business.id));



            }
        }

        private void btn_LoadJsonIntoScreen_Ex28_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select a JSON that contains student's data...";
            openFile.Filter = "JSON files(*.json)|*.json";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string studentsDataAsString = File.ReadAllText(openFile.FileName);
                richTextBox_JsonData_Ex28.Text = studentsDataAsString;
            }
            else
            {
                MessageBox.Show("No file was selected", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //ex 35
        private async void btn_CountAllObjects_Ex35_Click(object sender, EventArgs e)
        {
            int totalObjectsInCloud = await CountObjectsInCloudAccountAsync();
            textBox_TotalObjectsInCloud_Ex35.Text = totalObjectsInCloud.ToString();
        }



        private async Task<int> CountObjectsInCloudAccountAsync()
        {

            int totalNumOfObjsInCloud = 0;

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(currentDBProp.Id, currentTableProp.Id);

                            FeedIterator<object> objIterator = tableRefObj.GetItemQueryIterator<object>();

                            while (objIterator.HasMoreResults)
                            {
                                foreach (object currentObj in await objIterator.ReadNextAsync())
                                {
                                    totalNumOfObjsInCloud++;
                                }
                            }
                        }
                    }

                }

            }
            return totalNumOfObjsInCloud;
        }

        // ex 36
        private async void btn_CountAllObjsInInputDB_Ex36_Click(object sender, EventArgs e)
        {
            string requestedDB = textBox_DbForEx36.Text;

            // option 1 with int
            //int totalObjectsInRequestedDB = await CountObjectsInRequestedDBInAccountV1Async(requestedDB);
            //textBox_TotalObjsInInputDB_Ex36.Text = totalObjectsInRequestedDB.ToString();

            // option 2 with string
            textBox_TotalObjsInInputDB_Ex36.Text = await CountObjectsInRequestedDBInAccountV2Async(requestedDB);



        }

        //private async Task<int> CountObjectsInRequestedDBInAccountV1Async(string requestedDB)
        //{

        //    int totalNumOfObjsInCloud = 0;

        //    try
        //    {

        //        Database databaseRefObj = myCosmosClient.GetDatabase(requestedDB);

        //        FeedIterator<ContainerProperties> tableIterator =
        //        databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

        //        while (tableIterator.HasMoreResults)
        //        {
        //            foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
        //            {
        //                Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(requestedDB, currentTableProp.Id);

        //                FeedIterator<object> objIterator = tableRefObj.GetItemQueryIterator<object>();

        //                while (objIterator.HasMoreResults)
        //                {
        //                    foreach (object currentObj in await objIterator.ReadNextAsync())
        //                    {
        //                        totalNumOfObjsInCloud++;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch
        //    {
        //        totalNumOfObjsInCloud = 0;
        //    }
        //    return totalNumOfObjsInCloud;

        //}


        private async Task<string> CountObjectsInRequestedDBInAccountV2Async(string requestedDB)
        {

            int totalNumOfObjsInCloud = 0;

            try
            {

                Database databaseRefObj = myCosmosClient.GetDatabase(requestedDB);

                FeedIterator<ContainerProperties> tableIterator =
                databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                while (tableIterator.HasMoreResults)
                {
                    foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                    {
                        Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(requestedDB, currentTableProp.Id);

                        FeedIterator<object> objIterator = tableRefObj.GetItemQueryIterator<object>();

                        while (objIterator.HasMoreResults)
                        {
                            foreach (object currentObj in await objIterator.ReadNextAsync())
                            {
                                totalNumOfObjsInCloud++;
                            }
                        }
                    }
                }

            }
            catch
            {
                return "Request Failed";
            }
            return totalNumOfObjsInCloud.ToString();

        }


        // ex 37
        private async void btn_CountAllObjsInInputContainer_Ex37_Click(object sender, EventArgs e)
        {
            string requestedDB = textBox_DBForEx37.Text;
            string requestedContainer = textBox_ContainerForEx37.Text;

            textBox_TotalObjsInInputContainer_Ex37.Text = await CountObjectsInRequestedDBAndRequestedContainerInAccountAsync(requestedDB, requestedContainer);

        }



        private async Task<string> CountObjectsInRequestedDBAndRequestedContainerInAccountAsync(string requestedDB, string requestedContainer)
        {

            int totalNumOfObjsInCloud = 0;

            try
            {

                Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(requestedDB, requestedContainer);

                FeedIterator<object> objIterator = tableRefObj.GetItemQueryIterator<object>();

                while (objIterator.HasMoreResults)
                {
                    foreach (object currentObj in await objIterator.ReadNextAsync())
                    {
                        totalNumOfObjsInCloud++;
                    }
                }

            }
            catch
            {
                return "Request Failed";
            }
            return totalNumOfObjsInCloud.ToString();

        }

        // ex 45
        private async void btn_GetDBsEx45_Click(object sender, EventArgs e)
        {
            comboBox_GetDBsEx45.DataSource = await getDBsNamesFromCloudAccountAsync();
        }

        private async void comboBox_GetDBsEx45_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the tables names only for the selected database.
            string dbName = comboBox_GetDBsEx45.Text;
            comboBox_GetContainersEx45.DataSource = await getTablesNamesOfSelectedDBAsync(dbName);
        }


        private async void btn_GetRequestedDocEx45_Click(object sender, EventArgs e)
        {
            string db = comboBox_GetDBsEx45.Text;
            string table = comboBox_GetContainersEx45.Text;
            string requestedID = textBox_GetDocIDEx45.Text;

            // option 1
            //string studentData = await getStudentDataV1Async(db, table, requestedID);
            //richTextBox_RequestedDocEx45.Text = studentData;

            // option 2
            //Student studentData = await getStudentDataV2Async(db, table, requestedID);
            //richTextBox_RequestedDocEx45.Text = (studentData == null) ?
            //    $"Student ID{requestedID} doesnt exist with table '{table}' in a '{db}'" : studentData.ToString();

            // option 3
            string studentData = await getStudentDataV3Async(db, table, requestedID);
            richTextBox_RequestedDocEx45.Text = studentData;
        }


        // opt1
        private async Task<string> getStudentDataV1Async(string db, string table, string requestedID)
        {
            Student result;
            try
            {
                Microsoft.Azure.Cosmos.Container containerRefObj = myCosmosClient.GetContainer(db, table);
                ItemResponse<Student> studentObj = await containerRefObj.ReadItemAsync<Student>(requestedID, new PartitionKey(requestedID));

                result = studentObj.Resource;
                return result.ToString();
            }
            catch
            {
                return $"Student with ID {requestedID} Doesnt Exist";
            }
        }

        // opt2
        private async Task<Student> getStudentDataV2Async(string db, string table, string requestedID)
        {
            Student result;
            try
            {
                Microsoft.Azure.Cosmos.Container containerRefObj = myCosmosClient.GetContainer(db, table);
                ItemResponse<Student> studentObj = await containerRefObj.ReadItemAsync<Student>
                    (requestedID, new PartitionKey(requestedID));

                result = studentObj.Resource;
                return result;
            }
            catch
            {
                //return $"Student ID{requestedID} doesnt exist with table '{table}' in a '{db}'";
                return null;
            }
        }

        // opt3
        private async Task<string> getStudentDataV3Async(string db, string table, string requestedID)
        {
            Student student;
            try
            {
                Microsoft.Azure.Cosmos.Container containerRefObj = myCosmosClient.GetContainer(db, table);
                ItemResponse<object> obj = await containerRefObj.ReadItemAsync<object>
                    (requestedID, new PartitionKey(requestedID));
                JToken token = (JToken)obj.Resource;

                // token = the object
                string type = token["ObjType"]?.ToString();

                if (type == Targil45.Student.ToString())
                {
                    student = token.ToObject<Student>();
                    return student.ToString();
                }
                else
                    return $" ID{requestedID} of type {type}";
            }
            catch
            {
                return $"Student ID{requestedID} doesnt exist with table '{table}' in a '{db}'";
            }
        }

        // ex 38
        private async void btn_Ex38_Click(object sender, EventArgs e)
        {
            //Task1
            List<string> results = await getTotalNumOfObjForEachTable();
            comboBox_Ex38.DataSource = results;

            //Task2
            List<Targil38SearchResults> resultsAsListOfClasses = await getTotalNumOfObjForEachTableClassView();
            dataGridView_Ex38.DataSource = resultsAsListOfClasses;

            dataGridView_Ex38.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn c in dataGridView_Ex38.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 18);
                c.DefaultCellStyle.ForeColor = Color.DarkGreen;
            }
        }
        private async Task<List<string>> getTotalNumOfObjForEachTable()
        {
            int totalNumOfObjsInCurrentTable = 0;
            List<string> result = new List<string>();

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(currentDBProp.Id, currentTableProp.Id);

                            FeedIterator<object> objIterator = tableRefObj.GetItemQueryIterator<object>();
                            totalNumOfObjsInCurrentTable = 0;
                            while (objIterator.HasMoreResults)
                            {
                                foreach (object currentObj in await objIterator.ReadNextAsync())
                                {
                                    totalNumOfObjsInCurrentTable++;
                                }
                            }
                            result.Add($"DB '{currentDBProp.Id}' - Table '{currentTableProp.Id}' - '{totalNumOfObjsInCurrentTable}' Obj");
                        }
                    }

                }
            }
            return result;
        }
        private async Task<List<Targil38SearchResults>> getTotalNumOfObjForEachTableClassView()
        {
            int totalNumOfObjsInCurrentTable = 0;
            List<Targil38SearchResults> result = new List<Targil38SearchResults>();

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(currentDBProp.Id, currentTableProp.Id);

                            FeedIterator<object> objIterator = tableRefObj.GetItemQueryIterator<object>();
                            totalNumOfObjsInCurrentTable = 0;
                            while (objIterator.HasMoreResults)
                            {
                                foreach (object currentObj in await objIterator.ReadNextAsync())
                                {
                                    totalNumOfObjsInCurrentTable++;
                                }
                            }
                            result.Add(new Targil38SearchResults
                            {
                                DatabaseName = currentDBProp.Id,
                                ContainerName = currentTableProp.Id,
                                TotalNumOfObjects = totalNumOfObjsInCurrentTable,
                            }
                            );
                        }
                    }

                }
            }
            return result;
        }


        // ex 30/12/2025    
        private async void btn_SearchStudents30_Click(object sender, EventArgs e)
        {

            string firstNamePrefix = textBox_FirstNameStartWith30.Text;
            int requestesnumOfAddr = Convert.ToInt32(textBox_ExactNumOfAddresse30.Text);
            int numOfCourses = Convert.ToInt32(textBox_MinNumOfCourses30.Text);

            List<OutputFor30> resultsAsListOfClasses = await getResultsForSearch30(firstNamePrefix, requestesnumOfAddr, numOfCourses);
            dataGridView_SearchResults30.DataSource = resultsAsListOfClasses;

            dataGridView_SearchResults30.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn c in dataGridView_SearchResults30.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 14);
                c.DefaultCellStyle.ForeColor = Color.Blue;
            }

        }

        private async Task<List<OutputFor30>> getResultsForSearch30(string firstNamePrefix, int requestesnumOfAddr, int numOfCourses)
        {
            string firstNameOfCurrentStudent = null;

            Address[] adresses;
            int counterForAddrOfCurrentStudent = 0;

            Course[] courses;
            int counterForCoursesOfCurrentStudent = 0;

            List<OutputFor30> result = new List<OutputFor30>();

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(currentDBProp.Id, currentTableProp.Id);

                            FeedIterator<Student> studentIterator = tableRefObj.GetItemQueryIterator<Student>();

                            while (studentIterator.HasMoreResults)
                            {
                                foreach (Student currentStudent in await studentIterator.ReadNextAsync())
                                {
                                    firstNameOfCurrentStudent = currentStudent.FirstName;
                                    if (string.IsNullOrEmpty(firstNameOfCurrentStudent) ||
                                        !firstNameOfCurrentStudent.ToLower().StartsWith(firstNamePrefix.ToLower())) continue;

                                    adresses = currentStudent.Addresses;
                                    if (adresses == null || adresses.Length == 0) continue;

                                    counterForAddrOfCurrentStudent = 0;
                                    foreach (Address address in adresses)
                                        if (address != null) counterForAddrOfCurrentStudent++;

                                    if (requestesnumOfAddr != counterForAddrOfCurrentStudent) continue;

                                    courses = currentStudent.Courses;
                                    if (courses == null || courses.Length == 0) continue;

                                    counterForCoursesOfCurrentStudent = 0;
                                    foreach (Course course in courses)
                                    {
                                        if (courses != null) counterForCoursesOfCurrentStudent++;

                                        if (counterForCoursesOfCurrentStudent == numOfCourses)
                                        {
                                            result.Add(new OutputFor30
                                            {
                                                DatabaseName = currentDBProp.Id,
                                                ContainerName = currentTableProp.Id,
                                                StudentId = currentStudent.id,
                                                FirstName = currentStudent.FirstName,
                                                LastName = (string.IsNullOrEmpty(currentStudent.LastName)) ?
                                                "No last name" : currentStudent.LastName
                                            }

                                            );

                                        }

                                    }
                                }
                            }
                        }

                    }
                }
            }
            return result;
        }

        // ex 62
        private async void btn_PresentStudentTargil62_Click(object sender, EventArgs e)
        {
            string requestedCourseName = textBox_CourseNameTargil62.Text;
            string requestedTeacher = textBox_TeacherNameTargil62.Text;
            int minGrade = Convert.ToInt32(textBox_MinGradeTargil62.Text);

            List<OutputTargil62> resultsAsListOfClasses = await getResultsTargil62(requestedCourseName, requestedTeacher, minGrade);
            dataGridView_ResultsTargil62.DataSource = resultsAsListOfClasses;

            dataGridView_ResultsTargil62.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn c in dataGridView_ResultsTargil62.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 10);
                c.DefaultCellStyle.ForeColor = Color.Blue;
            }
        }

        // ----------------------------------------------- << a >>
        private async Task<List<OutputTargil62>> getResultsTargil62(string requestedCourseName, string requestedTeacher, int minGrade)
        {
            Course[] courses;
            List<OutputTargil62> result = new List<OutputTargil62>();

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(currentDBProp.Id, currentTableProp.Id);

                            FeedIterator<Student> studentIterator = tableRefObj.GetItemQueryIterator<Student>();

                            while (studentIterator.HasMoreResults)
                            {
                                foreach (Student currentStudent in await studentIterator.ReadNextAsync())
                                {
                                    courses = currentStudent.Courses;
                                    if (courses == null || courses.Length == 0) continue;

                                    foreach (Course course in courses)
                                    {
                                        if (courses == null) continue;
                                        if (!string.IsNullOrEmpty(course.CourseName)
                                            && course.CourseName.Equals(requestedCourseName)
                                            && !string.IsNullOrEmpty(course.Teacher)
                                            && course.Teacher.Equals(requestedTeacher)
                                            && course.Grade >= minGrade)
                                        {
                                            result.Add(new OutputTargil62
                                            {
                                                DatabaseName = currentDBProp.Id,
                                                ContainerName = currentTableProp.Id,
                                                StudentId = currentStudent.id,
                                                FullName = currentStudent.FirstName + " " + currentStudent.LastName,
                                                Grade = course.Grade
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        // ----------------------------------------------- << b >>
        private async void btn_ApplyFactorOnTheAbovePopulationTargil62_Click(object sender, EventArgs e)
        {
            string requestedCourseName = textBox_CourseNameTargil62.Text;
            string requestedTeacher = textBox_TeacherNameTargil62.Text;
            int minGrade = Convert.ToInt32(textBox_MinGradeTargil62.Text);
            int factor = Convert.ToInt32(textBox_DefineFactorTargil62.Text);
            await updateFactorForSearchTargil62(requestedCourseName, requestedTeacher, minGrade, factor);
        }

        private async Task updateFactorForSearchTargil62(string requestedCourseName, string requestedTeacher, int minGrade, int factor)
        {
            Course[] courses;
            List<OutputTargil62> result = new List<OutputTargil62>();

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(currentDBProp.Id, currentTableProp.Id);

                            FeedIterator<Student> studentIterator = tableRefObj.GetItemQueryIterator<Student>();

                            while (studentIterator.HasMoreResults)
                            {
                                foreach (Student currentStudent in await studentIterator.ReadNextAsync())
                                {
                                    courses = currentStudent.Courses;
                                    if (courses == null || courses.Length == 0) continue;

                                    foreach (Course course in courses)
                                    {
                                        if (courses == null) continue;
                                        if (!string.IsNullOrEmpty(course.CourseName)
                                            && course.CourseName.Equals(requestedCourseName)
                                            && !string.IsNullOrEmpty(course.Teacher)
                                            && course.Teacher.Equals(requestedTeacher)
                                            && course.Grade >= minGrade)

                                        {
                                            // update
                                            course.Grade = Math.Min(100, course.Grade + factor);
                                            course.Grade = Math.Max(0, course.Grade);

                                            await tableRefObj.ReplaceItemAsync(currentStudent,
                                            currentStudent.id,
                                            new PartitionKey(currentStudent.id));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // ----------------------------------------------- << c >>  
        private async void btn_DeleteTheAbovePopulationTargil62_Click(object sender, EventArgs e)
        {
            string requestedCourseName = textBox_CourseNameTargil62.Text;
            string requestedTeacher = textBox_TeacherNameTargil62.Text;
            int minGrade = Convert.ToInt32(textBox_MinGradeTargil62.Text);
            int result = await deleteFactorTargil62(requestedCourseName, requestedTeacher, minGrade);
            textBox_TotalStudentDeletedTargil62.Text = result.ToString();
        }

        private async Task<int> deleteFactorTargil62(string requestedCourseName, string requestedTeacher, int minGrade)
        {
            Course[] courses;
            int count = 0;

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {
                            Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(currentDBProp.Id, currentTableProp.Id);

                            FeedIterator<Student> studentIterator = tableRefObj.GetItemQueryIterator<Student>();

                            while (studentIterator.HasMoreResults)
                            {
                                foreach (Student currentStudent in await studentIterator.ReadNextAsync())
                                {
                                    courses = currentStudent.Courses;
                                    if (courses == null || courses.Length == 0) continue;

                                    foreach (Course course in courses)
                                    {
                                        if (courses == null) continue;
                                        if (!string.IsNullOrEmpty(course.CourseName)
                                            && course.CourseName.Equals(requestedCourseName)
                                            && !string.IsNullOrEmpty(course.Teacher)
                                            && course.Teacher.Equals(requestedTeacher)
                                            && course.Grade >= minGrade)

                                        {
                                            count++;

                                            await tableRefObj.DeleteItemAsync<Student>(
                                                currentStudent.id,
                                                new PartitionKey(currentStudent.id));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }

        // ex 39
        private async void btn_Ex39_Click(object sender, EventArgs e)
        {
            textBox_Ex39.Text = await tablesWithMaxObjectsAsync();
        }

        private async Task<string> tablesWithMaxObjectsAsync()
        {
            int maxObjCounter = 0;
            string tablesWithMaxObjs = "The following tables with max num of objects ";

            FeedIterator<DatabaseProperties> dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {

                            int totalNumOfObjsInCurrentTable = 0;

                            Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(currentDBProp.Id, currentTableProp.Id);

                            FeedIterator<object> objIterator = tableRefObj.GetItemQueryIterator<object>();

                            while (objIterator.HasMoreResults)
                            {
                                foreach (object currentObj in await objIterator.ReadNextAsync())
                                {
                                    totalNumOfObjsInCurrentTable++;
                                }
                            }
                            if (totalNumOfObjsInCurrentTable > maxObjCounter)
                                maxObjCounter = totalNumOfObjsInCurrentTable;
                        }
                    }

                }
            }
            tablesWithMaxObjs += $"({maxObjCounter} objects) : ";

            dbIterator = myCosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();

            while (dbIterator.HasMoreResults)
            {
                foreach (DatabaseProperties currentDBProp in await dbIterator.ReadNextAsync())
                {
                    Database databaseRefObj = myCosmosClient.GetDatabase(currentDBProp.Id);

                    FeedIterator<ContainerProperties> tableIterator =
                    databaseRefObj.GetContainerQueryIterator<ContainerProperties>();

                    while (tableIterator.HasMoreResults)
                    {
                        foreach (ContainerProperties currentTableProp in await tableIterator.ReadNextAsync())
                        {

                            int totalNumOfObjsInCurrentTable = 0;

                            Microsoft.Azure.Cosmos.Container tableRefObj = myCosmosClient.GetContainer(currentDBProp.Id, currentTableProp.Id);

                            FeedIterator<object> objIterator = tableRefObj.GetItemQueryIterator<object>();

                            while (objIterator.HasMoreResults)
                            {
                                foreach (object currentObj in await objIterator.ReadNextAsync())
                                {
                                    totalNumOfObjsInCurrentTable++;
                                }
                            }
                            if (totalNumOfObjsInCurrentTable == maxObjCounter)
                                tablesWithMaxObjs += $"'{currentDBProp.Id}' - Table '{currentTableProp.Id}' - ";
                        }
                    }

                }
            }

            return tablesWithMaxObjs;

        }


        // ex 46
        private async void btn_Ex46_Click(object sender, EventArgs e)
        {
            string dbName = textBox_DBName_Ex46.Text;
            string containerName = textBox_TableName_Ex46.Text;
            string objID = textBox_ObjId_Ex46.Text;
            string lastName = textBox_LastName_Ex46.Text;
            bool result = await IsDataInDBAsync(dbName, containerName, objID, lastName);
            richTextBox_Ex46.Text = result == true ? "True" : "False";
        }

        private async Task<bool> IsDataInDBAsync(string dbName, string containerName, string objID, string lastName)
        {
            richTextBox_Ex46.ForeColor = Color.Black;

            try
            {
                Database dbRefObj = myCosmosClient.GetDatabase(dbName);
                if (dbRefObj == null) return false;

                Microsoft.Azure.Cosmos.Container containerRefObj = myCosmosClient.GetContainer(dbName, containerName);
                if (containerRefObj == null) return false;

                ItemResponse<object> studentObj = await containerRefObj.ReadItemAsync<object>(objID, new PartitionKey(objID));
                if (studentObj == null) return false;
                else
                {
                    JToken token = (JToken)studentObj.Resource;
                    string currentLastName = token["LastName"]?.ToString();
                    if (string.IsNullOrEmpty(currentLastName)) return false;
                    if (!currentLastName.Equals(lastName)) return false;

                }

            }
            catch
            {
                richTextBox_Ex46.BackColor = Color.Red;
                return false;
            }

            richTextBox_Ex46.BackColor = Color.Green;

            return true;
        }

        // ex 47
        private async void btn_Ex47_Click(object sender, EventArgs e)
        {
            string dbName = textBox_DBName_Ex47.Text;
            string containerName = textBox_TableName_Ex47.Text;
            string objID = textBox_ObjID_Ex47.Text;

            richTextBox_Ex47.Text = await CountCoursesForStudentDBAsync(dbName, containerName, objID);
        }

        private async Task<string> CountCoursesForStudentDBAsync(string dbName, string containerName, string objID)
        {
            int courseCount = 0;

            try
            {
                Database dbRefObj = myCosmosClient.GetDatabase(dbName);
                if (dbRefObj == null) return "No Courses";

                Microsoft.Azure.Cosmos.Container containerRefObj = myCosmosClient.GetContainer(dbName, containerName);
                if (containerRefObj == null) return "No Courses";

                ItemResponse<object> studentObj = await containerRefObj.ReadItemAsync<object>(objID, new PartitionKey(objID));
                if (studentObj == null) return "No Courses";
                else
                {

                    JToken token = (JToken)studentObj.Resource;
                    Course[] courses = token["Courses"]?.ToObject<Course[]>();

                    if (courses == null || courses.Length == 0) return "No Courses";

                    foreach (Course c in courses)
                    {
                        if (c != null)
                            courseCount++;
                    }

                }

            }
            catch
            {
                return "No Courses";
            }

            return $"The Student with the id of {textBox_ObjID_Ex47.Text} has {courseCount} Courses";
        }

        // ex 55 a
        private async void btn_Ex55_A_Click(object sender, EventArgs e)
        {
            string dbName = textBox_DBName_Ex55_A.Text;
            string tableName = textBox_TableName_Ex55_A.Text;
            textBox_Result_Ex55_A.Text = await getStudentsNumWithSpecificCriteriaAsyncV1(dbName, tableName);

        }

        private async Task<string> getStudentsNumWithSpecificCriteriaAsyncV1(string dbName, string tableName)
        {
            int countOfStudents = 0;

            bool containsL = false;
            bool hasAtLeastOneAddressInHaifa = false;
            bool inTwoCourses = false;

            try
            {
                Database dbRefObj = myCosmosClient.GetDatabase(dbName);
                if (dbRefObj == null) return "No Such Students";

                Microsoft.Azure.Cosmos.Container containerRefObj = myCosmosClient.GetContainer(dbName, tableName);
                if (containerRefObj == null) return "No Such Students";

                FeedIterator<Student> studentIterator = containerRefObj.GetItemQueryIterator<Student>();

                while (studentIterator.HasMoreResults)
                {
                    foreach (Student student in await studentIterator.ReadNextAsync())
                    {
                        containsL = student.FirstName.Contains("L") || student.LastName.Contains("L");

                        Address[] addresses = student.Addresses;
                        string haifa = "haifa";
                        foreach (Address addr in addresses)
                        {
                            if (addr != null && !string.IsNullOrEmpty(addr.City) && addr.City.ToLower().Equals(haifa))
                            {
                                hasAtLeastOneAddressInHaifa = true;
                                break;
                            }
                        }

                        int courseCount = 0;
                        Course[] courses = student.Courses;
                        foreach (Course c in courses)
                        {
                            if (c != null)
                                courseCount++;
                        }

                        inTwoCourses = courseCount == 2;

                        if (containsL && hasAtLeastOneAddressInHaifa && inTwoCourses)
                            countOfStudents++;

                        // reset for next student
                        containsL = false;
                        hasAtLeastOneAddressInHaifa = false;
                        inTwoCourses = false;

                    }
                }
            }
            catch
            {
                return "No Such Students";
            }

            return countOfStudents != 0 ? $"There are {countOfStudents} Students" : "No Such Students";
        }

        // ex 55 b
        private async void btn_Ex55_B_Click(object sender, EventArgs e)
        {
            string dbName = textBox_DBName_Ex55_B.Text;
            string tableName = textBox_TableName_Ex55_B.Text;

            List<OutputTargil55> result = await getStudentsNumWithSpecificCriteriaAsyncV2(dbName, tableName);

            dataGridView_Ex55.DataSource = result;

            dataGridView_Ex55.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn c in dataGridView_Ex55.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 10);
                c.DefaultCellStyle.ForeColor = Color.DarkGreen;
            }

        }

        private async Task<List<OutputTargil55>> getStudentsNumWithSpecificCriteriaAsyncV2(string dbName, string tableName)
        {
            List<OutputTargil55> result = new List<OutputTargil55>();
            bool containsL = false;
            bool hasAtLeastOneAddressInHaifa = false;
            bool inTwoCourses = false;

            try
            {
                Database dbRefObj = myCosmosClient.GetDatabase(dbName);
                if (dbRefObj == null) return new List<OutputTargil55>();

                Microsoft.Azure.Cosmos.Container containerRefObj = myCosmosClient.GetContainer(dbName, tableName);
                if (containerRefObj == null) return new List<OutputTargil55>();

                FeedIterator<Student> studentIterator = containerRefObj.GetItemQueryIterator<Student>();

                while (studentIterator.HasMoreResults)
                {
                    foreach (Student student in await studentIterator.ReadNextAsync())
                    {

                        containsL = student.FirstName.Contains("L") || student.LastName.Contains("L");

                        Address[] addresses = student.Addresses;
                        string haifa = "haifa";

                        string city = "";
                        string street = "";
                        string houseNum = "";
                        foreach (Address addr in addresses)
                        {
                            if (addr != null && !string.IsNullOrEmpty(addr.City) && addr.City.ToLower().Equals(haifa))
                            {
                                hasAtLeastOneAddressInHaifa = true;
                                city = addr.City;
                                street = addr.Street;
                                houseNum = addr.HouseNum;
                                break;
                            }
                        }

                        int courseCount = 0;
                        Course[] courses = student.Courses;
                        foreach (Course c in courses)
                        {
                            if (c != null)
                                courseCount++;
                        }

                        inTwoCourses = courseCount == 2;

                        if (containsL && hasAtLeastOneAddressInHaifa && inTwoCourses)
                        {
                            result.Add(new OutputTargil55
                            {
                                StudentId = student.id,
                                FullName = student.FirstName + " " + student.LastName,
                                City = city,
                                Street = street,
                                HouseNum = houseNum

                            });

                        }

                        // reset for next student
                        containsL = false;
                        hasAtLeastOneAddressInHaifa = false;
                        inTwoCourses = false;
                    }
                }
            }
            catch
            {
                return new List<OutputTargil55>();
            }

            return result;
        }


        // logs
        // refresh btn
        private void btn_Refresh_Load_Data_Click(object sender, EventArgs e)
        {
            getUpdatedLogData();
        }

        private void getUpdatedLogData()
        {
            try
            {
                string logFullPath = Path.Combine(logFolderName, logFileName);

                if (File.Exists(logFullPath))
                {
                    string logData = File.ReadAllText(logFullPath, Encoding.UTF8);
                    richTextBox_Log_Data.Text = logData;
                }
                else
                {
                    if (!Directory.Exists(logFolderName))
                        Directory.CreateDirectory(logFolderName);

                    File.Create(logFullPath);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Log File...\n" + ex.Message,
                                "Error Loading Log File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
            }
        }

        // reset btn
        private void btn_Reset_Log_Data_Click(object sender, EventArgs e)
        {
            try
            {
                string logFullPath = Path.Combine(logFolderName, logFileName);

                if (File.Exists(logFullPath))
                {
                    File.Delete(logFullPath);
                    richTextBox_Log_Data.Clear();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Deleting Log File...\n" + ex.Message,
                                "Error Deleting Log File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
            }
        }
    }


}

