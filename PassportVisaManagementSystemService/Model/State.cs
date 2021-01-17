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

    public class State
    {
        [Key]
        [DataMember]
        public int StateId { set; get; }

        [DataMember]
        public string StateName { set; get; }

        [DataMember]
        public int? CountryId { set; get; }

        [ForeignKey("CountryId")]
        public virtual Country Country { set; get; }

    }
}