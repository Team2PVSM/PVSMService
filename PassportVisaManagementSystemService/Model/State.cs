using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    [DataContract]

    public class State
    {
        [DataMember]
        public int StateId { set; get; }

        [DataMember]
        public string StateName { set; get; }

        [DataMember]
        public City City { set; get; }

    }
}