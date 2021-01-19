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
    public class ApplyVisa
    {
        [Key]
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public int UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual User User { set; get; }

        [DataMember]
        public Country Countries { set; get; }
        [DataMember]
        public string Occupation { set; get; }

        [DataMember]
        public DateTime DateOfApplication { set; get; }

        [DataMember]
        public DateTime DateOfExpiry { set; get; }

        [DataMember]
        public string VisaNumber { set; get; }

        [DataMember]
        public DateTime DateOfIssue { set; get; }

        [DataMember]
        public double RegistrationCost { set; get; }

        [DataMember]
        public string status { set; get; }

        [DataMember]
        public int CancellationCharge { set; get; }


        



    }
}