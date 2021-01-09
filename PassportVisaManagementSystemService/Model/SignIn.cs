using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    public class User
    {
        //[Key]

        [DataMember]
        public int UserId { set; get; }

        [DataMember]
        public string UserName { set; get; }

        [DataMember]
        public string Password { set; get; }

        [DataMember]
        public DateTime DOB { set; get; }

        [DataMember]
        public string Address { set; get; }

        [DataMember]
        public Country Country { set; get; }

        [DataMember]
        public int PinCode { set; get; }

        [DataMember]
        public string Type { set; get; }

        [DataMember]
        public string Status { set; get; }
    }
}