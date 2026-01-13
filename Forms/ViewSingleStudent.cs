using Cloud.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloud.Forms
{
    public partial class ViewSingleStudent : Form
    {

        private string dbName, containerName;
        Student student;
        private Microsoft.Azure.Cosmos.Container container;

        public ViewSingleStudent()
        {
            InitializeComponent();
        }

        private void ViewSingleStudent_Load(object sender, EventArgs e)
        {
            this.Text = $"DB: {dbName}, Container: {containerName}, Student ID: {student.id}, First Name: {student.FirstName}";

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            richTextBox_SingleStudentDetails.Text = JsonSerializer.Serialize(student, options);
        }

        private async void btn_ReplaceStudent_Click(object sender, EventArgs e)
        {
            Student updatedStudent = JsonSerializer.Deserialize<Student>(richTextBox_SingleStudentDetails.Text);
            try
            {
                await container.ReplaceItemAsync(updatedStudent, student.id, 
                                                new Microsoft.Azure.Cosmos.PartitionKey(student.id));

                MessageBox.Show($"Student with the ID {student.id} has been replaced successfully.",
                                "Replace Succeeded", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Failed to replace student with ID {student.id}," +
                    $" We got the following Error:\n\n{ex.Message}",
                              "Replace Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();

        }

        private async void btn_DeleteStudent_Click(object sender, EventArgs e)
        {
            try
            {
                await container.DeleteItemAsync<Student>(student.id,
                                                        new Microsoft.Azure.Cosmos.PartitionKey(student.id));

                MessageBox.Show($"Student with the ID {student.id} has been Deleted successfully.",
                                "Delete Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to Deleted student with ID {student.id}," +
                    $" We got the following Error:\n\n{ex.Message}",
                              "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        public ViewSingleStudent(string dbName, string containerName, Microsoft.Azure.Cosmos.Container container, Student student)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.containerName = containerName;
            this.container = container;
            this.student = student;
        }
    }
}
