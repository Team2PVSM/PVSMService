using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    [DataContract]
    public class User
    {
        [Key]
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public string UserId { set; get; }
       
        [DataMember]
        public string FirstName { set; get; }

        [DataMember]
        public string SurName { set; get; }

        [DataMember]
        public DateTime DateOfBirth { set; get; }

        [DataMember]
        public string Address { set; get; }

        //[DataMember]
        //public Country Country { set; get; }

        [DataMember]
        public int	ContactNo { set; get; }

        [DataMember]
        public string EmailAddress { set; get; }

        [DataMember]
        public string Qualification { set; get; }

        [DataMember]
        public string Gender { set; get; }

        [DataMember]
        public string ApplyType { set; get; }

        [DataMember]
        public HintQuestion HintQuestion { set; get; }

        [DataMember]
        public string HintAnswer { set; get; }

        [DataMember]
        public string CitizenType { set; get; }

        [DataMember]
        public string Password { set; get; }
    }
}