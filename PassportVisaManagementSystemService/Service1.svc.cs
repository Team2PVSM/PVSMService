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
        public bool SignIn(User U)
        {
            if (U.UserName=="admin" && U.Password=="admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
