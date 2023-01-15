using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace HomeWork6.NoSolid
{
    public class MainClass
    {        
        public static void Main()
        {
            UserRepository UserRepository = new UserRepository();            
            Console.WriteLine("HomeWork6!");
            string input;
            do
            {
                UserRepository.ShowUserMenu();
                Console.WriteLine("Enter a number:");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":                        
                        if (UserRepository.cterateUser(UserRepository.GetUserInfo()))
                            Console.WriteLine("The User has been added successfully!");
                        else
                            Console.WriteLine("There is something wrong in adding of user!");
                        break;
                    case "2":
                        if (UserRepository.GetIdOfUsers == null)
                        {
                            Console.WriteLine("The List of users is null!");
                            break;
                        }                            
                        foreach (var id in UserRepository.GetIdOfUsers())
                        {
                            User usr = UserRepository.GetUser(id);
                            Console.WriteLine($"Id: {usr.Id}, Name: {usr.Name}, MobileNumber: {usr.mobileNumber}, " +
                                $"DateOfBirth: {usr.birthDate}, CreationDate: {usr.creationDate}");
                        }
                        string ss;
                        do
                        {
                            UserRepository.ShowListOfUsersMenu();
                            Console.WriteLine("Enter a number");
                            ss = Console.ReadLine();
                            switch(ss)
                            {
                                case "1":
                                    if (UserRepository.UpdateUser(UserRepository.GetUserInfo()))
                                        Console.WriteLine("The User has been updated successfully!");
                                    else
                                        Console.WriteLine("There is something wrong in Updating of user!");
                                    break;
                                case "2":
                                    Console.WriteLine("Enter the Id of user?");
                                    int idOfSelectedUser = Convert.ToInt32(Console.ReadLine());
                                    if(UserRepository.DeleteUser(idOfSelectedUser))
                                        Console.WriteLine("The User has been deleted successfully!");
                                    else
                                        Console.WriteLine("There is something wrong in deleting of user!");
                                    break;
                                case "3":
                                    Environment.Exit(0);
                                    break;
                            }
                        }while(ss != "3");
                        
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            } while (input != "3");


        }
    }
}
