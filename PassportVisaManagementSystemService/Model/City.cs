﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    [DataContract]

    public class City
    {
        [Key]
        [DataMember]
        public int CityId { set; get; }

        [DataMember]
        public string CityName { set; get; }

        [DataMember]
        public State State { set; get; }
    }
}