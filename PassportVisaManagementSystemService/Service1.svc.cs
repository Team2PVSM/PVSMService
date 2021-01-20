using PassportVisaManagementSystemService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace PassportVisaManagementSystemService
{

    public class Service1 : IService1
    {
        Model1 PVMSModel = new Model1();
        public bool SignIn(string Username, string Password)
        {
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


        public string FetchUserByEmailAddress(string Email)
        {
            List<User> U = new List<User>();

            U = PVMSModel.Users.Where(x => x.EmailAddress == Email).ToList();
            var json = new JavaScriptSerializer().Serialize(U);
            return json;
        }

        public string FetchUserByuserId(string UserId)
        {
            List<User> U = new List<User>();

            U = PVMSModel.Users.Where(x => x.UserId == UserId).ToList();
            var json = new JavaScriptSerializer().Serialize(U);
            return json;
        }
        public string FetchUserByuserparameter(string parameter, string value)
        {
            if (parameter.ToLower() == "email")
            {
                List<User> U = new List<User>();

                U = PVMSModel.Users.Where(x => x.EmailAddress == value).ToList();
                var json = new JavaScriptSerializer().Serialize(U);
                return json;
            }
            else if (parameter.ToLower() == "userid")
            {
                List<User> U = new List<User>();

                U = PVMSModel.Users.Where(x => x.UserId == value).ToList();
                var json = new JavaScriptSerializer().Serialize(U);
                return json;
            }
            else
            {
                return null;
            }
        }
        public bool ApplyForPassport(ApplyPassport AP)
        {
            if (AP.BookletType.ToLower() == "60pages")
            {
                AP.PassportNumber = GetPassportIdForSixty();
            }
            if (AP.BookletType.ToLower() == "30pages")
            {
                AP.PassportNumber = GetPassportIdForThirty();
            }
            AP.Reason = "NA";
            AP.ExpiryDate = GetExpiryDate(AP.IssueDate);
            AP.Amount = GetAmount(AP.ServiceType);
            AP.Status = "applypassport";
            PVMSModel.ApplyPassports.Add(AP);
            PVMSModel.SaveChanges();
            return true;

        }

        private double GetAmount(string serviceType)
        {
            double namt = 2500;
            double tamt = 5000;
            if (serviceType.ToLower() == "normal")
            {
                return namt;
            }
            else
            {
                return tamt;
            }
        }

        private DateTime GetExpiryDate(DateTime issueDate)
        {
            return issueDate.AddYears(10);
        }

        private string GetPassportIdForSixty()
        {
            string str = "FPS-60";
            Random rnd = new Random();
            return str + rnd.Next(1000, 9999).ToString();
        }

        private string GetPassportIdForThirty()
        {
            string str = "FPS-30";
            Random rnd = new Random();
            return str + rnd.Next(1000, 9999).ToString();
        }

        public bool ReIssuePassport(ApplyPassport RP)
        {
            if (RP.BookletType.ToLower() == "60pages")
            {
                RP.PassportNumber = GetPassportIdForSixty();
            }
            if (RP.BookletType.ToLower() == "30pages")
            {
                RP.PassportNumber = GetPassportIdForThirty();
            }
            RP.ExpiryDate = GetExpiryDate(RP.IssueDate);
            RP.Amount = ReIssueAmount(RP.ServiceType);
            RP.Status = "reissuepassport";
            var oldPassport = PVMSModel.ApplyPassports.FirstOrDefault(x => x.UserId == RP.UserId);
            if (oldPassport != null)
            {
                PVMSModel.ApplyPassports.Remove(oldPassport);
                PVMSModel.ApplyPassports.Add(RP);
                PVMSModel.SaveChanges();

                OldPassportData O = PVMSModel.OldPassportDatas.FirstOrDefault(x => x.PassportNumber == oldPassport.PassportNumber);
                O.Reason = RP.Reason;
                PVMSModel.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private double ReIssueAmount(string serviceType)
        {
            double namt = 1500;
            double tamt = 3000;
            if (serviceType.ToLower() == "normal")
            {
                return namt;
            }
            else
            {
                return tamt;
            }
        }

        public List<Country> FetchCountries()
        {
            return PVMSModel.Countries.ToList();
        }
        public string FetchState(int CountryId)
        {
            List<State> U = new List<State>();
            U = PVMSModel.States.Where(x => x.CountryId == CountryId).ToList();
            var json = new JavaScriptSerializer().Serialize(U);
            return json;
        }
        public string FetchCity(int StateId)
        {

            List<City> U = new List<City>();

            U = PVMSModel.Cities.Where(x => x.StateId == StateId).ToList();
            var json = new JavaScriptSerializer().Serialize(U);
            return json;
            //return 
        }

        public List<State> State()
        {
            throw new NotImplementedException();
        }

        public List<City> City()
        {
            throw new NotImplementedException();
        }

        public int getIdByUserId(string userName)
        {
            List<User> U = new List<User>();

            U = PVMSModel.Users.Where(x => x.UserId == userName).ToList();
            if (U.Count > 0)
            {

                return U[0].Id;
            }
            return 0;
        }

        public string getPassportNumberByUserName(int userName)
        {
            List<ApplyPassport> U = new List<ApplyPassport>();
            U = PVMSModel.ApplyPassports.Where(x => x.UserId == userName).ToList();
            if (U.Count > 0)
            {
                return U[0].PassportNumber;
            }
            else
            {
                return null;
            }
        }

        
		public bool ApplyForVisa(ApplyVisa AV)
        {
            AV.DateOfIssue = AV.DateOfApplication.AddDays(10);
            DateTime ExpiryDate = DateTime.Today;
            int registrationcost = 0;
            string ct = (from c in PVMSModel.Countries
                         where AV.CountryId == c.CountryId
                         select c.CountryName).FirstOrDefault();
            string str = getPassportNumberByUserName(AV.UserId);
            if (str != null)
            {
                if (AV.Occupation.ToString() == "Student")
                {
                    AV.VisaNumber = GetVisaNoStudent();
                    ExpiryDate = AV.DateOfIssue.AddYears(2);
                    
                    if (ct == "United States")
                        registrationcost = 3000;
                    else if (ct == "China")
                        registrationcost = 1500;
                    else if (ct == "Japan")
                        registrationcost = 3500;
                    else
                        registrationcost = 2500;
                }
                else if (AV.Occupation.ToString() == "Private Employee")
                {
                    AV.VisaNumber = GetVisaNoPrivateEmployee();
                    ExpiryDate = AV.DateOfIssue.AddYears(3);

                    if (ct == "United States")
                        registrationcost = 4500;
                    else if (ct == "China")
                        registrationcost = 2000;
                    else if (ct == "Japan")
                        registrationcost = 4000;
                    else
                        registrationcost = 3000;
                }
                else if (AV.Occupation.ToString() == "Government Employee")
                {
                    AV.VisaNumber = GetVisaNoGovtEmployee();
                    ExpiryDate = AV.DateOfIssue.AddYears(4);

                    if (ct == "United States")
                        registrationcost = 5000;
                    else if (ct == "China")
                        registrationcost = 3000;
                    else if (ct == "Japan")
                        registrationcost = 4500;
                    else
                        registrationcost = 3500;
                }
                else if (AV.Occupation.ToString() == "Self Employed")
                {
                    AV.VisaNumber = GetVisaNoSelfEmployed();
                    ExpiryDate = AV.DateOfIssue.AddYears(1);

                    if (ct == "United States")
                        registrationcost = 6000;
                    else if (ct == "China")
                        registrationcost = 4000;
                    else if (ct == "Japan")
                        registrationcost = 9000;
                    else
                        registrationcost = 5500;
                }
                else if (AV.Occupation.ToString() == "Retired Employee")
                {
                    AV.VisaNumber = GetVisaNoRetiredEmployee();
                    ExpiryDate = AV.DateOfIssue.AddYears(1).AddMonths(6);

                    if (ct == "United States")
                        registrationcost = 2000;
                    else if (ct == "China")
                        registrationcost = 2000;
                    else if (ct == "Japan")
                        registrationcost = 1000;
                    else
                        registrationcost = 1000;
                }

                ApplyPassport PA = (from c in PVMSModel.ApplyPassports
                                    where c.ExpiryDate > ExpiryDate
                                    select c).FirstOrDefault();
                if (PA != null)
                    ExpiryDate = PA.ExpiryDate;

                AV.DateOfExpiry = ExpiryDate;
                AV.RegistrationCost = registrationcost;
                AV.status = "ApplyVisa";
                PVMSModel.ApplyVisas.Add(AV);
                PVMSModel.SaveChanges();

                return true;
            }
            else
                return false;
        }

        private string GetVisaNoRetiredEmployee()
        {
            string str = "VISA-RE-";
            Random rnd = new Random();
            return str + rnd.Next(1000, 9999).ToString();
        }

        private string GetVisaNoSelfEmployed()
        {
            string str = "VISA-SE-";
            Random rnd = new Random();
            return str + rnd.Next(1000, 9999).ToString();
        }

        private string GetVisaNoGovtEmployee()
        {
            string str = "VISA-GE-";
            Random rnd = new Random();
            return str + rnd.Next(1000, 9999).ToString();
        }

        private string GetVisaNoPrivateEmployee()
        {
            string str = "VISA-PE-";
            Random rnd = new Random();
            return str + rnd.Next(1000, 9999).ToString();
        }

        private string GetVisaNoStudent()
        {
            string str = "VISA-S-";
            Random rnd = new Random();
            return str + rnd.Next(1000, 9999).ToString();
        }

        public bool CancelVisa(ApplyVisa CV)
        {
            ApplyVisa VA = (from c in PVMSModel.ApplyVisas
                            where c.UserId == CV.UserId && c.VisaNumber == CV.VisaNumber && c.DateOfIssue == CV.DateOfIssue
                            select c).FirstOrDefault();
            //string str = getPassportNumberByUserName(CV.UserId);
            if (VA != null)
            {
                int cancellationcost = 0;
                int Diff_mon = 0;
                if (DateTime.Today < VA.DateOfExpiry)
                    Diff_mon = Math.Abs((VA.DateOfExpiry.Month - DateTime.Today.Month) + 12 * (VA.DateOfExpiry.Year - DateTime.Today.Year));
                if (VA.Occupation == "Student")
                {
                    if (Diff_mon < 6)
                        cancellationcost = (int)(0.15 * VA.RegistrationCost);
                    else if (Diff_mon >= 6)
                        cancellationcost = (int)(0.25 * VA.RegistrationCost);
                }
                else if (VA.Occupation == "Private Employee")
                {
                    if (Diff_mon < 6)
                        cancellationcost = (int)(0.15 * VA.RegistrationCost);
                    else if (Diff_mon >= 6 && Diff_mon < 12)
                        cancellationcost = (int)(0.25 * VA.RegistrationCost);
                    else if (Diff_mon >= 12)
                        cancellationcost = (int)(0.20 * VA.RegistrationCost);
                }
                else if (VA.Occupation == "Government Employee")
                {
                    if (Diff_mon >= 6 && Diff_mon < 12)
                        cancellationcost = (int)(0.20 * VA.RegistrationCost);
                    else if (Diff_mon >= 12)
                        cancellationcost = (int)(0.25 * VA.RegistrationCost);
                    else if (Diff_mon < 6)
                        cancellationcost = (int)(0.15 * VA.RegistrationCost);
                }
                else if (VA.Occupation == "Self Employed")
                {
                    if (Diff_mon < 6)
                        cancellationcost = (int)(0.15 * VA.RegistrationCost);
                    else if (Diff_mon >= 6)
                        cancellationcost = (int)(0.25 * VA.RegistrationCost);
                }
                else if (VA.Occupation == "Retired Employee")
                {
                    if (Diff_mon < 6)
                        cancellationcost = (int)(0.10 * VA.RegistrationCost);
                    else if (Diff_mon >= 6)
                        cancellationcost = (int)(0.20 * VA.RegistrationCost);
                }

                VA.CancellationCharge = cancellationcost;
                VA.status = "Cancelled";
                PVMSModel.SaveChanges();
                return true;
            }
            else
                return false;

        }
		public string fetchApplyPassportbyUserId(int UserId)
        {
            List<ApplyPassport> U = new List<ApplyPassport>();

            U = PVMSModel.ApplyPassports.Where(x => x.UserId == UserId).ToList();
            var json = new JavaScriptSerializer().Serialize(U);
            return json;
        }

        public bool AuthenticationQues(User U)
        {
            User Authenticity = (from c in PVMSModel.Users
                                   where c.UserId == U.UserId && c.HintAnswer == U.HintAnswer
                                   select c).FirstOrDefault();
            if (Authenticity != null)
            {
                return true;
            }
            else
                return false;
        }

        public string FetchVisaNumber(int id)
        {
            ApplyVisa A = PVMSModel.ApplyVisas.FirstOrDefault(x => x.UserId == id);
            return A.VisaNumber;
        }
        public string fetchApplyVisabyUserId(int UserId)
        {
            List<ApplyVisa> U = new List<ApplyVisa>();

            U = PVMSModel.ApplyVisas.Where(x => x.UserId == UserId).ToList();
            var json = new JavaScriptSerializer().Serialize(U);
            return json;
        }

        public string fetchCountryStateCityById(int country)
        {

            List<Country> C = new List<Country>();


            C = PVMSModel.Countries.Where(x => x.CountryId == country).ToList();
            if (C.Count > 0)
            {
                return C[0].CountryName;
            }
            else
            {
                return null;
            }

        }
    }
}
