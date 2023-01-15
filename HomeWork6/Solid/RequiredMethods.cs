using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6.Solid
{
    public static class RequiredMethods
    {
        static string csvFilePath = Directory.GetCurrentDirectory() + "\\FileDataStorage.csv";
        
        public static List<int>? GetIdOfUsers()
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public static void ShowUserMenu()
        {
            Console.WriteLine($"1={UserMenu.DefineUser} ," +
                    $"\n2={UserMenu.ShowListOfUsers} ," +
                    $"\n3={UserMenu.LogOut}"
                    );
        }
        public static void ShowListOfUsersMenu()
        {
            Console.WriteLine($"1={UserMenu.UpdateUser}," +
                $"\n2={UserMenu.DeleteUser}," +
                $"\n3={UserMenu.LogOut}");
        }

        public static User GetUserInfo()
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

        public static List<User>? GetJsonUser(string csvFilePath)
        {
            var jsonText = File.ReadAllText(csvFilePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonText);
        }
    }
}
