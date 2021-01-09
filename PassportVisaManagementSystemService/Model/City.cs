using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    [DataContract]

    public class City
    {
        [DataMember]
        public int CityId { set; get; }

        [DataMember]
        public string CityName { set; get; }
    }
}