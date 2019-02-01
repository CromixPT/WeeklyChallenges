using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DrapperLibrary;
using DrapperLibrary.Models;

namespace WinFormApp
{
    public partial class Dashboard:Form
    {
        BindingList<UserModel> users = new BindingList<UserModel>();

        public Dashboard()
        {
            InitializeComponent();

            userDisplayList.DataSource = users;
            userDisplayList.DisplayMember = "FullName";

            List<UserModel> userList = DataAccess.GetUsers();

            UpdateUserList(userList);

        }

        private void createUserButton_Click(object sender, EventArgs e)
        {

            UserModel newUser = new UserModel
            {
                FirstName = firstNameText.Text,
                LastName = lastNameText.Text
            };

            DataAccess.AddUser(newUser);

            List<UserModel> userList = DataAccess.GetUsers();

            UpdateUserList(userList);

            firstNameText.Text = "";
            lastNameText.Text = "";
            firstNameText.Focus();


        }

        private void applyFilterButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DapperDemoDB"].ConnectionString;

            using(IDbConnection cnn = new SqlConnection(connectionString))
            {
                var p = new
                {
                    Filter = filterUsersText.Text
                };

                var records = cnn.Query<UserModel>("spSystemUser_GetFiltered", p, commandType: CommandType.StoredProcedure).ToList();

                UpdateUserList(records);

            }
        }

        private void UpdateUserList(List<UserModel> userList)
        {
            users.Clear();
            userList.ForEach(x => users.Add(x));
        }
    }
}
