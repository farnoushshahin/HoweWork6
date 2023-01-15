using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6.Solid
{
    public class SolidUserRepositoryPutCommand : ISolidUserRepositoryPutCommand<User>
    {
        string csvFilePath = Directory.GetCurrentDirectory() + "\\FileDataStorage.csv";
        string jsonString;
        public bool CreateUser(User user)
        {
            try
            {//Write text to book file
                List<User> users = RequiredMethods.GetJsonUser(csvFilePath);
                if (users is null)
                {
                    string headerLines = string.Join(",", user.GetType().GetProperties().Select(p => p.Name));
                    user.Id = 0;
                    jsonString = JsonConvert.SerializeObject(user).ToCsv();

                }
                else
                {
                    user.Id = users[users.Count - 1].Id + 1;
                    users.Add(user);
                    jsonString = JsonConvert.SerializeObject(users).ToCsv();

                }
                File.WriteAllText(csvFilePath, jsonString);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            try
            {
                List<User> users = RequiredMethods.GetJsonUser(csvFilePath);
                if (users is null)
                    return false;
                foreach (var user in users)
                {
                    if (user.Id == id)
                    {
                        users.Remove(user);
                        string s = JsonConvert.SerializeObject(users);
                        File.WriteAllText(csvFilePath, s);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool UpdateUser(User User)
        {
            try
            {
                List<User> users = RequiredMethods.GetJsonUser(csvFilePath);
                foreach (var user in users)
                {
                    if (user.Id == User.Id)
                    {
                        user.Name = User.Name;
                        user.mobileNumber = User.mobileNumber;
                        user.birthDate = User.birthDate;
                        user.creationDate = User.creationDate;
                        string s = JsonConvert.SerializeObject(users);
                        File.WriteAllText(csvFilePath, s);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        

    }
}
