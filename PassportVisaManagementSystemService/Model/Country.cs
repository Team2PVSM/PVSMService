using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    [DataContract]
    public class Country
    {
        [Key]
        [DataMember]
        public int CountryId { set; get; }
        
        [DataMember]
        public string CountryName { set; get; }

       


    }
}