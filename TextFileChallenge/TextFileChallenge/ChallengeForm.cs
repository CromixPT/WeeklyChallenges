using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextFileChallenge
{
    public partial class ChallengeForm:Form
    {
        BindingList<UserModel> users = new BindingList<UserModel>();
        string filePath = @"D:\Repos\WeeklyChallenges\TextFileChallenge\TextFileChallenge\StandardDataSet.csv";

        public ChallengeForm()
        {
            InitializeComponent();

            WireUpDropDown();
        }

        private void WireUpDropDown()
        {
            usersListBox.DataSource = users;
            usersListBox.DisplayMember = nameof(UserModel.DisplayText);
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            var newUser = new UserModel();

            newUser.FirstName = firstNameText.Text;
            newUser.LastName = lastNameText.Text;
            newUser.Age = (int)agePicker.Value;
            if(!isAliveCheckbox.Checked)
            {
                newUser.IsAlive = false;
            }
            else
            {
                newUser.IsAlive = true;
            }



            users.Add(newUser);
        }

        private void ChallengeForm_Load(object sender, EventArgs e)
        {


            var lines = File.ReadAllLines(filePath).Skip(1).ToList();

            foreach(string line in lines)
            {
                string[] info = line.Split(',');

                var newUser = new UserModel();

                newUser.FirstName = info[0];
                newUser.LastName = info[1];
                newUser.Age = Convert.ToInt32(info[2]);

                newUser.IsAlive = Convert.ToBoolean(Convert.ToInt32(info[3]));

                users.Add(newUser);
            }
        }

        private void saveListButton_Click(object sender, EventArgs e)
        {

            List<string> output = new List<string>();


            output.Add("FirstName,LastName,Age,IsAlive");

            foreach(var user in users)
            {
                output.Add($"{user.FirstName},{ user.LastName },{ user.Age},{Convert.ToInt32(user.IsAlive)}");
            }
            File.WriteAllLines(filePath, output);
        }
    }
}
