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
        User FetchUserByEmailAddress(string Email);

    }

}
