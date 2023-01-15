using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace HomeWork6.NoSolid
{
    public class UserRepository : IUserRepository<User>
    {
        string csvFilePath = Directory.GetCurrentDirectory() + "\\FileDataStorage.csv";
        string jsonString;
        public bool cterateUser(User user)
        {
            try
            {//Write text to book file
                List<User> users = GetJsonUser(csvFilePath);
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
                List<User> users = GetJsonUser(csvFilePath);
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
            List<User> users = GetJsonUser(csvFilePath);
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
                List<User> users = GetJsonUser(csvFilePath);
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
        private List<User>? GetJsonUser(string fileName)
        {
            var jsonText = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<User>>(jsonText);
        }
        public List<int>? GetIdOfUsers()
        {
            try
            {
                List<int> ids = new List<int>();
                List<User> users = GetJsonUser(csvFilePath);
                if (users is null)
                    return null;                
                foreach (var user in users)
                {
                    ids.Add(user.Id);
                }
                return ids;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public void ShowUserMenu()
        {
            Console.WriteLine($"1={UserMenu.DefineUser} ," +
                    $"\n2={UserMenu.ShowListOfUsers} ," +
                    $"\n3={UserMenu.LogOut}"                     
                    );
        }
        public void ShowListOfUsersMenu()
        {
            Console.WriteLine($"1={UserMenu.UpdateUser}," +
                $"\n2={UserMenu.DeleteUser}," +
                $"\n3={UserMenu.LogOut}");
        }

        public User GetUserInfo()
        {
            User us = new User();
            Console.WriteLine("Enter Name of user:");
            us.Name = Console.ReadLine();
            string mobile;
            do
            {
                Console.WriteLine("Enter Mobile Number of user:");
                mobile = Console.ReadLine();
            } while (mobile.Length != 11 && !int.TryParse(mobile, out int m));
            us.mobileNumber = Convert.ToInt32(mobile);
            string birthDate;
            do
            {
                Console.WriteLine("Enter your BirthDate:");
                birthDate = Console.ReadLine();
            } while (DateTime.Parse(birthDate) > DateTime.Now);
            us.birthDate = DateTime.Parse(birthDate);
            us.creationDate = DateTime.Now;
            return us;
        }
    }
}
