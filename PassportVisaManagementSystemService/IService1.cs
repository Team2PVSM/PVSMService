using PassportVisaManagementSystemService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace PassportVisaManagementSystemService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        bool SignIn(string Username, string Password);

        [OperationContract]
        bool SignUp(User U);

        [OperationContract]
        List<HintQuestion> FetchHintQuestion();

        [OperationContract]
        string FetchUserByEmailAddress(string Email);

        [OperationContract]
        string FetchUserByuserId(string UserId);

        [OperationContract]
        string FetchUserByuserparameter(string parameter, string value);

        [OperationContract]
        bool ApplyForPassport(ApplyPassport AP);

        [OperationContract]
        bool ReIssuePassport(ApplyPassport RP);

        [OperationContract]
        List<Country> FetchCountries();

        [OperationContract]
        string FetchState(int CountryId);

        [OperationContract]
        string FetchCity(int StateId);


        [OperationContract]
        int getIdByUserId(string userName);

        [OperationContract]
        string getPassportNumberByUserName(int userName);


        [OperationContract]
        string fetchApplyPassportbyUserId(int UserId);

        [OperationContract]
        List<State> State();

        [OperationContract]
        List<City> City();
        
		[OperationContract]
        bool ApplyForVisa(ApplyVisa AV);

        [OperationContract]
        bool CancelVisa(ApplyVisa CV);

        [OperationContract]
        bool AuthenticationQues(User U);

    }

}
