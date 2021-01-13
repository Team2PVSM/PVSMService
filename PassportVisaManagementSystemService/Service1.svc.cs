using PassportVisaManagementSystemService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PassportVisaManagementSystemService
{

    public class Service1 : IService1
    {
        Model1 PVMSModel = new Model1();
        public bool SignIn(string Username, string Password)
        {
<<<<<<< HEAD
            //if (U.UserName=="admin" && U.Password=="admin")
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
=======
            //Model1 M = new Model1();
            List<User> U = new List<User>();
            U = PVMSModel.Users.Where(w => w.UserId == Username && w.Password == Password).ToList();

            if (U.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
>>>>>>> 8e0c69e864a1313ebc0cf1e3826472d6f8a87dae
        }

        public bool SignUp(User U)
        {
            //Model1 W = new Model1();
            if (U.ApplyType.ToLower() == "passport")
            {
                U.UserId = getRandomNoForPass();

            }
            if (U.ApplyType.ToLower() == "visa")
            {
                U.UserId = getRandomNoForVisa();

            }
            U.CitizenType = getCitizenType(U.DateOfBirth);
            U.Password = getPassword(U.DateOfBirth);
            PVMSModel.Users.Add(U);
            PVMSModel.SaveChanges();
            return true;
        }

        public string getRandomNoForPass()
        {
            string str = "PASS-";
            Random rnd = new Random();
            return str + rnd.Next(1000, 9999).ToString();
        }

        public string getRandomNoForVisa()
        {
            string str = "VISA-";
            Random rnd = new Random();
            return str + rnd.Next(1000, 9999).ToString() + "-" + rnd.Next(1000, 9999).ToString();
        }

        public string getCitizenType(DateTime Dob)
        {
            int age = DateTime.Now.Year - Dob.Year;
            string str = "";
            if (age == 0 || age == 1)
            {
                str = "infant";
            }
            else if (age > 1 && age <= 10)
            {
                str = "Children";
            }
            else if (age > 10 && age <= 20)
            {
                str = "Teen";
            }
            else if (age > 20 && age <= 50)
            {
                str = "Adult";
            }
            else
            {
                str = "Senior Citizen";
            }

            return str;
        }

        public string getPassword(DateTime dob)
        {

            string[] SpecialSymbols = { "$", "@", "#" };
            string month = dob.ToString("MMM");

            Random rnd = new Random();
            int index = rnd.Next(0, 2);
            string cureentDate = dob.ToString().Split('-')[0];


            return cureentDate + month + SpecialSymbols[index] + rnd.Next(111, 999).ToString();


        }

        public List<HintQuestion> FetchHintQuestion()
        {
            //Model1 W = new Model1();
            return PVMSModel.HintQuestions.ToList();
        }

        public User FetchUserByEmailAddress(string Email)
        {
           return PVMSModel.Users.Where(x => x.EmailAddress == Email).FirstOrDefault();
        }
    }
}
