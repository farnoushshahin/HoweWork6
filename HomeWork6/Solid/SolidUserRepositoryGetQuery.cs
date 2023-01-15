using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6.Solid
{
    public class SolidUserRepositoryGetQuery : ISolidUserRepositoryGetQuery<User>
    {
        string csvFilePath = Directory.GetCurrentDirectory() + "\\FileDataStorage.csv";
        
        public User? GetUser(int id)
        {
            List<User> users = GetJsonUser(csvFilePath);
            foreach (var user in users)
            {
                if (user.Id == id)
                    return user;
            }
            return null;
        }

        private List<User> GetJsonUser(string csvFilePath)
        {
            var jsonText = File.ReadAllText(csvFilePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonText);
        }
    }
}
