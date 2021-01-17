using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int? StateId { set; get; }

        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}