using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
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

            string filter = filterUsersText.Text;

            List<UserModel> userList = DataAccess.GetUsers(filter);

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

            string filter = filterUsersText.Text;
            List<UserModel> userList = DataAccess.GetUsers(filter);

            UpdateUserList(userList);

            firstNameText.Text = "";
            lastNameText.Text = "";
            firstNameText.Focus();


        }

        private void applyFilterButton_Click(object sender, EventArgs e)
        {

            string filter = filterUsersText.Text;

            List<UserModel> userList = DataAccess.GetUsers(filter);

            UpdateUserList(userList);

        }

        private void UpdateUserList(List<UserModel> userList)
        {
            users.Clear();
            userList.ForEach(x => users.Add(x));
        }
    }
}
