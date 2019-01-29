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
        int firstNameIndex = 0;
        int lastNameIndex = 0;
        int ageIndex = 0;
        int isAliveIndex = 0;
        string firstLine;


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
            newUser.IsAlive = isAliveCheckbox.Checked;

            users.Add(newUser);
        }

        private void ChallengeForm_Load(object sender, EventArgs e)
        {


            string firstLine = File.ReadLines(filePath).First();
            List<string> lines = File.ReadAllLines(filePath).Skip(1).ToList();



            var header = firstLine.Split(',');

            for(int i = 0; i < header.Count(); i++)
            {
                switch(header[i])
                {
                    case "FirstName":
                        firstNameIndex = i;
                        break;
                    case "LastName":
                        lastNameIndex = i;
                        break;
                    case "Age":
                        ageIndex = i;
                        break;
                    case "IsAlive":
                        isAliveIndex = i;
                        break;
                    default:
                        break;
                }
            }
            foreach(string line in lines)
            {
                string[] info = line.Split(',');

                var newUser = new UserModel();

                newUser.FirstName = info[firstNameIndex];
                newUser.LastName = info[lastNameIndex];
                newUser.Age = Convert.ToInt32(info[ageIndex]);

                newUser.IsAlive = Convert.ToBoolean(Convert.ToInt32(info[isAliveIndex]));

                users.Add(newUser);
            }

        }

        private void saveListButton_Click(object sender, EventArgs e)
        {


            List<string> output = new List<string>();


            output.Add(firstLine);

            foreach(var user in users)
            {
                string[] outputString = new string[4];

                outputString[firstNameIndex] = user.FirstName;
                outputString[lastNameIndex] = user.LastName;
                outputString[ageIndex] = user.Age.ToString();
                outputString[isAliveIndex] = Convert.ToInt32(user.IsAlive).ToString();


                output.Add($"{outputString[0]},{outputString[1]},{outputString[2]},{outputString[3]}");
            }
            File.WriteAllLines(filePath, output);
        }
    }
}
