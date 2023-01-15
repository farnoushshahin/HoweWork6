using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace HomeWork6
{
    public class UserRepository : IUserRepository<User>
    {
        string csvFilePath = Directory.GetCurrentDirectory() + "\\FileDataStorage.csv";
        string jsonString;
        public bool cterateUser(User user)
        {
            try
            {//Write text to book file
                List<User> users = GetJsonBooks(csvFilePath);
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
                List<User> users = GetJsonBooks(csvFilePath);
                if (users is null)
                    return false;
                foreach (var user in users)
                {
                    if(user.Id == id)
                    { 
                        users.Remove(user);
                        string s = JsonConvert.SerializeObject(users);
                        File.WriteAllText(csvFilePath, s);
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public User GetUser(int id)
        {
            List<User> users = GetJsonBooks(csvFilePath);
            foreach (var user in users)
            {
                if(user.Id==id)
                    return user;
            }
            return null;
        }

        public bool UpdateUser(User User)
        {            
            try
            {
                List<User> users = GetJsonBooks(csvFilePath);
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;

        }
        List<User>? GetJsonBooks(string fileName)
        {
            var jsonText = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<User>>(jsonText);
        }
        public void ShowUserMenu()
        {
            Console.WriteLine($"1={UserMenu.DefineUser} ," +
                    $"\n2={UserMenu.ShowListOfUsers} ," +
                    $"\n3={UserMenu.UpdateUser} ," +
                    $"\n4={UserMenu.DeleteUser} ," +
                    $"\n5={UserMenu.LogOut}"
                    );
        }
    }
}
