using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    [DataContract]

    public class State
    {
        [Key]
        [DataMember]
        public int StateId { set; get; }

        [DataMember]
        public string StateName { set; get; }

        [DataMember]
        public Country Country { set; get; }

    }
}