using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using static BlenderWinForms.Form1;
using System.Xml.Linq;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using BlenderWinForms.Model;
using ObjectModel = BlenderWinForms.Model.ObjectModelClass.ObjectModel;
using ProjectModel = BlenderWinForms.Model.ProjectModel;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.AccessControl;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using System.Text;


namespace BlenderWinForms
{
    public partial class Form1 : Form
    {

        //public List<ProjectModel> projectsList  { get; set; }
        public static readonly Dictionary<string, List<ObjectModel>> ProjectObjectModels = new Dictionary<string, List<ObjectModel>>();
        public List<ObjectModel> editedObjectsList = new List<ObjectModel>();

        private const string ApiBaseUrl = "https://localhost:7052/api/Blender/";
        private DataTable table = new DataTable();

        ProjectModel selectedProject = null;
        ObjectModel objectProject = null;

        string CurrentProjectName;


        public Form1()
        {
            InitializeComponent();


            //List<ObjectModel> dataList = new List<ObjectModel>();
            //dataGridView1.DataSource = dataList;

            DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = "Name";
            dataColumn.Caption = "Name";
            dataColumn.DataType = typeof(string);
            dataColumn.ReadOnly = true;

            table.Columns.Add(dataColumn);




            dataColumn = new DataColumn();

            dataColumn.ColumnName = "Value";
            dataColumn.Caption = "Value";
            dataColumn.DataType = typeof(string);

            table.Columns.Add(dataColumn);
            //dataGridView1.AutoGenerateColumns = false;

            //dataGridView1.Columns[1].Width = 2;


        }


        private void buttonGetProjects(object sender, EventArgs e)
        {
            treeView1.Visible = false;
            editedObjectsList.Clear();
            ProjectObjectModels.Clear();


            string endpoint = "GetList";
            string apiUrl = $"{ApiBaseUrl}{endpoint}";

            var client = new RestClient(apiUrl);
            var request = new RestRequest(apiUrl, Method.Post);
            request.Timeout = 1500000; // Set timeout to 150 seconds (1000 milliseconds = 1 second)


            RestResponse responseProject = client.Execute(request);




            List<ProjectListModel> List = new List<ProjectListModel>();
            List = JsonConvert.DeserializeObject<List<ProjectListModel>>(responseProject.Content);


            foreach (var projectList in List)
            {
                ProjectModel currentProject = projectList.projectModel;
                List<ObjectModel> currentObjectList = projectList.objectModel;


                TreeNode projectNode1 = new TreeNode(currentProject.projectName);
                projectNode1.Tag = currentProject; // Attach the ProjectModel to the TreeNode

                treeView1.Nodes.Add(projectNode1);


                ProjectObjectModels.Add(currentProject.projectName, projectList.objectModel);


                foreach (var item in currentObjectList)
                {
                    TreeNode objectNode = new TreeNode(item.name);
                    objectNode.Tag = item; // Attach the ProjectModel to the TreeNode


                    projectNode1.Nodes.Add(objectNode);

                }

            }



            treeView1.Visible = true;
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Retrieve the selected project from the selected node's Tag

            //ProjectModel selectedProject = null;
            //ObjectModel objectProject = null;
            try
            {
                selectedProject = (ProjectModel)e.Node.Tag;

            }
            catch (Exception)
            {
                objectProject = (ObjectModel)e.Node.Tag;


                if (CurrentProjectName != objectProject.projectName && !string.IsNullOrEmpty(CurrentProjectName))
                {
                    if (MessageBox.Show("You are changing project. Any changes you made is unsaved. Do you want to continue? ", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        editedObjectsList.Clear();
                        CurrentProjectName = objectProject.projectName;

                    }
                    else
                    {
                        textBox1.Text = string.Empty;
                        table.Rows.Clear();
                        dataGridView1.DataSource = table;

                        return;
                    }
                }
                else
                {
                    CurrentProjectName = objectProject.projectName;
                }
            }







            if (objectProject == null)
            {

                textBox1.Text = $"Project Name: {selectedProject.projectName}\r\n" +
                                $"Project Path: {selectedProject.projectDirectory}\r\n" +
                                $"Project Description: {selectedProject.projectDescription}";
            }
            else
            {
                textBox1.Text = $"Name: {objectProject.name}\r\n" +
                               $"Type: {objectProject.type}\r\n" +
                               $"Content: {objectProject.content}\r\n" +
                               $"Path: {objectProject.path}\r\n" +
                               $"\r\n" +
                               $"X: {objectProject.coordinates.X}\r\n" +
                               $"Y: {objectProject.coordinates.Y}\r\n" +
                               $"Z: {objectProject.coordinates.Z}\r\n" +
                               $"\r\n" +
                               $"Rotation X: {objectProject.rotation.Rotation_X}°\r\n" +
                               $"Rotation Y: {objectProject.rotation.Rotation_Y}°\r\n" +
                               $"RotationX Z: {objectProject.rotation.Rotation_Z}°";


                table.Rows.Clear();
                dataGridView1.DataSource = table;

                table.Rows.Add("name", objectProject.name);
                table.Rows.Add("type", objectProject.type);
                table.Rows.Add("content", objectProject.content);
                table.Rows.Add("path", objectProject.path);

                table.Rows.Add("coordinates_X", objectProject.coordinates.X);
                table.Rows.Add("coordinates_Y", objectProject.coordinates.Y);
                table.Rows.Add("coordinates_Z", objectProject.coordinates.Z);

                table.Rows.Add("rotation_X", objectProject.rotation.Rotation_X);
                table.Rows.Add("rotation_Y", objectProject.rotation.Rotation_Y);
                table.Rows.Add("rotation_Z", objectProject.rotation.Rotation_Z);


                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //dataGridView1.Height = dataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.Displayed);

            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Cells[1].ReadOnly = true;
                dataGridView1.Rows[1].Cells[1].ReadOnly = true;

            }



        }






        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


                

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            ObjectModel editedObject = new ObjectModel();

            DataGridView dataGridView = sender as DataGridView;

            string grName = dataGridView.Rows[0].Cells[1].Value.ToString();
            string grType = dataGridView.Rows[1].Cells[1].Value.ToString();
            string grContent = dataGridView.Rows[2].Cells[1].Value.ToString();
            string grPath = dataGridView.Rows[3].Cells[1].Value.ToString();



            //bool isObjectInList = editedObjectsList.Any(obj => obj.name == grName);
            ObjectModel objectFound = editedObjectsList.Where(obj => obj.name == grName).FirstOrDefault();

            if (objectFound == null)
            {
                objectFound = new ObjectModel();

                objectFound.name = grName;
                objectFound.type = grType;
                objectFound.content = grContent;
                objectFound.path = grPath;

                objectFound.projectName = CurrentProjectName;

                objectFound.coordinates = new ObjectModelClass.Coordinates();
                objectFound.rotation = new ObjectModelClass.Rotation();

                editedObjectsList.Add(objectFound);

            }
            else
            {
                objectFound.content = grContent;
                objectFound.path = grPath;
            }


            objectProject.content = grContent;
            objectProject.path = grPath;





        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSaveProjectClick(object sender, EventArgs e)
        {
            string endpoint = "SaveObject";
            string apiUrl = $"{ApiBaseUrl}{endpoint}";

            var client = new RestClient(apiUrl);
            var request = new RestRequest(apiUrl, Method.Post);


            //  request.AddParameter("objectModelList", editedObjectsList,ParameterType.);

            var json = JsonConvert.SerializeObject(editedObjectsList, Formatting.None);




            //request.AddHeader("content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            //request.AddBody(json);

            //request.AddParameter("objectModelList", json);


            RestResponse responseObject = client.Execute(request);
        }

        private void buttonRender(object sender, EventArgs e)
        {
            string endpoint = "RenderBlenderProject";
            string apiUrl = $"{ApiBaseUrl}{endpoint}";

            var client = new RestClient(apiUrl);
            var request = new RestRequest(apiUrl, Method.Post);


            //  request.AddParameter("objectModelList", editedObjectsList,ParameterType.);

            List<string> renproList = new List<string>();

            renproList.Add(CurrentProjectName);

            var json = JsonConvert.SerializeObject(renproList);




            //request.AddHeader("content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            //request.AddBody(json);

            //request.AddParameter("objectModelList", json);


            RestResponse responseObject = client.Execute(request);
        }

        
    }
}
