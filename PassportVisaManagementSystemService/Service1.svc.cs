using PassportVisaManagementSystemService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
        //This method signin the user while checks whether the EmailId is already registered or not. 
        public bool SignIn(string Username, string Password)
        {
            try
            {
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
            catch (Exception)
            {
                return false;
            }
        }
        //This method register the user, generate the userid according to type.
        public bool SignUp(User U)
        {
            try
            {
                if (U.ApplyType.ToLower() == "passport")
                {
                    int passid = (from c in PVMSModel.Users
                                  where c.ApplyType == U.ApplyType
                                  select c).Count() + 1;
                    U.UserId = U.ApplyType.Substring(0, 4).ToUpper() + "-" + string.Format("{0:0000}", passid);

                }
                if (U.ApplyType.ToLower() == "visa")
                {
                    int visaid = (from c in PVMSModel.Users
                                  where c.ApplyType == U.ApplyType
                                  select c).Count() + 1;
                    U.UserId = U.ApplyType.Substring(0, 4).ToUpper() + "-" + string.Format("{0:0000}", visaid);

                }
                U.CitizenType = getCitizenType(U.DateOfBirth);
                U.Password = getPassword(U.DateOfBirth);
                PVMSModel.Users.Add(U);
                PVMSModel.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //This method get the user's citizen type based on his age.
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
        //This method generate the password for user.
        public string getPassword(DateTime dob)
        {

            string[] SpecialSymbols = { "$", "@", "#" };
            string month = dob.ToString("MMM");

            Random rnd = new Random();
            int index = rnd.Next(0, 2);
            string cureentDate = dob.ToString().Split('-')[0];


            return cureentDate + month + SpecialSymbols[index] + rnd.Next(111, 999).ToString();


        }
        //This method fetch the Hint questions for user when he is registering.
        public List<HintQuestion> FetchHintQuestion()
        {
            try
            {
                return PVMSModel.HintQuestions.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //This method check the email address whether it's already registerd or not
        public string FetchUserByEmailAddress(string Email)
        {
            try
            {
                List<User> U = new List<User>();

                U = PVMSModel.Users.Where(x => x.EmailAddress == Email).ToList();
                var json = new JavaScriptSerializer().Serialize(U);
                return json;
            }
            catch (Exception)
            {
                return "User Doesn't Exists";
            }
        }
        //This method check that userid is already registered or not.
        public string FetchUserByuserId(string UserId)
        {
            try
            {
                List<User> U = new List<User>();

                U = PVMSModel.Users.Where(x => x.UserId == UserId).ToList();
                var json = new JavaScriptSerializer().Serialize(U);
                return json;
            }
            catch (Exception)
            {
                return "User ID not Exists";
            }
        }
        //This method fetch the user by emaiil and and userid.
        public string FetchUserByuserparameter(string parameter, string value)
        {
            try
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
            catch (Exception)
            {
                return "Wrong Information";
            }
        }
        //This method generates PassportNumber based on validations and conditions mentioned and insert in database.
        public bool ApplyForPassport(ApplyPassport AP)
        {
            try
            {
                if (AP.BookletType.ToLower() == "60pages")
                {
                    var fps30 = (from c in PVMSModel.ApplyPassports
                                 select c.PassportNumber.Substring(c.PassportNumber.Length - 4, c.PassportNumber.Length)).Max();
                    if (fps30 == null)
                        fps30 = "0";
                    AP.PassportNumber = "FPS-30" + string.Format("{0:0000}", int.Parse(fps30) + 1);
                }
                if (AP.BookletType.ToLower() == "30pages")
                {
                    var fps60 = (from c in PVMSModel.ApplyPassports
                                 select c.PassportNumber.Substring(c.PassportNumber.Length - 4, c.PassportNumber.Length)).Max();
                    if (fps60 == null)
                        fps60 = "0";
                    AP.PassportNumber = "FPS-30" + string.Format("{0:0000}", int.Parse(fps60) + 1);
                }
                AP.Reason = "NA";
                AP.ExpiryDate = GetExpiryDate(AP.IssueDate);
                AP.Amount = GetAmount(AP.ServiceType);
                AP.Status = "applypassport";
                PVMSModel.ApplyPassports.Add(AP);
                PVMSModel.SaveChanges();
                return true;
            }
            catch (DbUpdateException d)
            {
                SqlException sql = (SqlException)d.GetBaseException();
               
                string str = sql.Message;
                return false;
            }
        }
        //This method generate the amount to be paid by user while applying for passport.
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
        //This method generate the expiry date based on the conditons mentioned for applying and reissuing passport
        private DateTime GetExpiryDate(DateTime issueDate)
        {
            return issueDate.AddYears(10);
        }
        //This method generates NewPassportNumber based on validations and conditions mentioned and insert in database
        //OldPassport data is also stored in database
        public bool ReIssuePassport(ApplyPassport RP)
        {
            try
            {
                if (RP.BookletType.ToLower() == "60pages")
                {
                    var fps30 = (from c in PVMSModel.ApplyPassports
                                 select c.PassportNumber.Substring(c.PassportNumber.Length - 4, c.PassportNumber.Length)).Max();
                    if (fps30 == null)
                        fps30 = "0";
                    RP.PassportNumber = "FPS-30" + string.Format("{0:0000}", int.Parse(fps30) + 1);
                }
                if (RP.BookletType.ToLower() == "30pages")
                {
                    var fps30 = (from c in PVMSModel.ApplyPassports
                                 select c.PassportNumber.Substring(c.PassportNumber.Length - 4, c.PassportNumber.Length)).Max();
                    if (fps30 == null)
                        fps30 = "0";
                    RP.PassportNumber = "FPS-30" + string.Format("{0:0000}", int.Parse(fps30) + 1);
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
            catch (Exception)
            {
                return false;
            }
        }
        //This method generate the amount to be paid by user while reissuing the passport.
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
        //This method fetch the list of country from database.
        public List<Country> FetchCountries()
        {
            try
            {
                return PVMSModel.Countries.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //This method fetch the list of state for selected country.
        public string FetchState(int CountryId)
        {
            try
            {
                List<State> U = new List<State>();
                U = PVMSModel.States.Where(x => x.CountryId == CountryId).ToList();
                var json = new JavaScriptSerializer().Serialize(U);
                return json;
            }
            catch (Exception)
            {
                return "Something went wrong";
            }
        }
        //This method fetch the list of city for selected state.
        public string FetchCity(int StateId)
        {
            try
            {
                List<City> U = new List<City>();

                U = PVMSModel.Cities.Where(x => x.StateId == StateId).ToList();
                var json = new JavaScriptSerializer().Serialize(U);
                return json;
            }
            catch (Exception)
            {
                return "Something went worng";
            }
            
        }

        public List<State> State()
        {
            throw new NotImplementedException();
        }

        public List<City> City()
        {
            throw new NotImplementedException();
        }
        //This method verify the userid.
        public int getIdByUserId(string userName)
        {
            try
            {
                List<User> U = new List<User>();

                U = PVMSModel.Users.Where(x => x.UserId == userName).ToList();
                if (U.Count > 0)
                {

                    return U[0].Id;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        //This method fetch the passport number while giving the userid to it.
        public string getPassportNumberByUserName(int userName)
        {
            try
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
            catch (Exception)
            {
                return "Not Exists";
            }
        }
        //This method is used for generating visa for the user and generate visa number, amount to be paid, expiry date and date of issue based on the conditon given.
		public bool ApplyForVisa(ApplyVisa AV)
        {
            try
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
                        int student_visaid = (from c in PVMSModel.ApplyVisas
                                              where c.Occupation == AV.Occupation
                                              select c).Count() + 1;
                        AV.VisaNumber = "STU-" + string.Format("{0:0000}", student_visaid);
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
                        int student_visaid = (from c in PVMSModel.ApplyVisas
                                              where c.Occupation == AV.Occupation
                                              select c).Count() + 1;
                        AV.VisaNumber = "PE-" + string.Format("{0:0000}", student_visaid);
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
                        int student_visaid = (from c in PVMSModel.ApplyVisas
                                              where c.Occupation == AV.Occupation
                                              select c).Count() + 1;
                        AV.VisaNumber = "GE-" + string.Format("{0:0000}", student_visaid);
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
                        int student_visaid = (from c in PVMSModel.ApplyVisas
                                              where c.Occupation == AV.Occupation
                                              select c).Count() + 1;
                        AV.VisaNumber = "SE-" + string.Format("{0:0000}", student_visaid);
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
                        int student_visaid = (from c in PVMSModel.ApplyVisas
                                              where c.Occupation == AV.Occupation
                                              select c).Count() + 1;
                        AV.VisaNumber = "RE-" + string.Format("{0:0000}", student_visaid);
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
            catch (Exception)
            {
                return false;
            }
        }
        //This method cancel the visa while checking if user already has visa or not and generate the cancellation charge for the user and change the status in database.
        public bool CancelVisa(ApplyVisa CV)
        {
            try
            {
                ApplyVisa VA = (from c in PVMSModel.ApplyVisas
                                where c.UserId == CV.UserId && c.VisaNumber == CV.VisaNumber && c.DateOfIssue == CV.DateOfIssue
                                select c).FirstOrDefault();
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
            catch (Exception)
            {
                return false;
            }

        }
		//This method fetch the passport number while giving the userid.
        public string fetchApplyPassportbyUserId(int UserId)
        {
            try
            {
                List<ApplyPassport> U = new List<ApplyPassport>();

                U = PVMSModel.ApplyPassports.Where(x => x.UserId == UserId).ToList();
                var json = new JavaScriptSerializer().Serialize(U);
                return json;
            }
            catch (Exception)
            {
                return "Doesn't Exists";
            }
        }
        //This method check whether the answer of hint question is given correct by the user. This method verify user for cancelling the visa.
        public bool AuthenticationQues(User U)
        {
            try
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
            catch (Exception)
            {
                return false;
            }
        }
        //This method fetch the visa number while giving the userid.
        public string FetchVisaNumber(int id)
        {
            try
            {
                ApplyVisa A = PVMSModel.ApplyVisas.FirstOrDefault(x => x.UserId == id);
                return A.VisaNumber;
            }
            catch(Exception)
            {
                return "Not Exists";
            }
        }
        //This method check if user have visa number or not.
        public string fetchApplyVisabyUserId(int UserId)
        {
            try
            {
                List<ApplyVisa> U = new List<ApplyVisa>();

                U = PVMSModel.ApplyVisas.Where(x => x.UserId == UserId).ToList();
                var json = new JavaScriptSerializer().Serialize(U);
                return json;
            }
            catch (Exception)
            {
                return "Not Exists";
            }
        }
        //This method fetch the country id while giving the country name.
        public string fetchCountryStateCityById(int country)
        {
            try
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
            catch (Exception)
            {
                return "Something went wrong";
            }
        }
        //This method fetch the hint question of specifid user while using his id.
        public string FetchHintQuestionByUserName(string username)
        {
            try
            {
                User U = new User();
                HintQuestion HQ = new HintQuestion();
                U = PVMSModel.Users.Where(x => x.UserId == username).FirstOrDefault();
                if (U != null)
                {
                    HQ = PVMSModel.HintQuestions.Where(x => x.Id == U.HintQuestionId).FirstOrDefault();
                    return HQ.Questions;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return "Something went worng";
            }
        }
		//This mehtod check the email address if it is already present in database or not.
        public string EmailAddress(string email)
        {
            try
            {
                User U = PVMSModel.Users.FirstOrDefault(x => x.EmailAddress == email);
                if (U != null)
                {
                    return U.EmailAddress;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return "Not Exists";
            }
        }
        //This mehtod check wheter user have passport or not.
        public bool CheckUserHaveApplyPassport(int userId)
        {
           List<ApplyPassport> AP = PVMSModel.ApplyPassports.ToList() ;
            if (AP.Count>0)
            {
                ApplyPassport U = PVMSModel.ApplyPassports.FirstOrDefault(x => x.UserId == userId);
                if(U != null)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //This mehtod check i fuser have visa number or not.
        public bool CheckUserHaveApplyVisa(int userId)
        {
            List<ApplyVisa> AP = PVMSModel.ApplyVisas.ToList();
            if (AP.Count > 0)
            {
                ApplyVisa U = PVMSModel.ApplyVisas.FirstOrDefault(x => x.UserId == userId);
                if (U != null)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
