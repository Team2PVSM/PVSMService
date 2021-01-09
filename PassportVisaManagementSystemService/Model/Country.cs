using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    [DataContract]
    public class Country
    {
        [DataMember]
        public int CountryId { set; get; }
        
        [DataMember]
        public string CountryName { set; get; }

        [DataMember]
        public State State { set; get; }


    }
}