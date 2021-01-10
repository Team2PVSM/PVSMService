using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    public class ApplyVisa
    {
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public User User { set; get; }

        [DataMember]
        public Country Countries { set; get; }
        [DataMember]
        public string Occupation { set; get; }

        [DataMember]
        public DateTime DateOfApplication { set; get; }

        [DataMember]
        public DateTime DateOfExpiry { set; get; }

        [DataMember]
        public DateTime VisaNumber { set; get; }

        [DataMember]
        public DateTime DateOfIssue { set; get; }

        [DataMember]
        public double RegistrationCost { set; get; }

        








    }
}