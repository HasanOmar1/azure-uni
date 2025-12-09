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
using Microsoft.Azure.Cosmos.Linq;
using Cloud.Models;
using System.IO;


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
            string selectedActivity = radioButton_Delete.Checked ? "Delete" :
                                      radioButton_Replace.Checked ? "Replace" :
                                       "Insert";

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

            foreach (Student student in students)
            {
                await containerRefObj.CreateItemAsync<Student>(student);
            }
        }
    }
}