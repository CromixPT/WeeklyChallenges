using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextFileChallenge
{
    public partial class ChallengeForm:Form
    {
        BindingList<UserModel> users = new BindingList<UserModel>();

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
            string filePath = @"D:\Repos\WeeklyChallenges\TextFileChallenge\TextFileChallenge\StandardDataSet.csv";

            var header = File.ReadLines(filePath);
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
    }
}
