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
    public class ApplyPassport
    {

        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual User User { set; get; }
        [DataMember]
        public int? CountryId { set; get; }

        [ForeignKey("CountryId")]
        public virtual Country Country { set; get; }


        [DataMember]
        public int? StateId { set; get; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        [DataMember]
        public int? CityId { set; get; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string BookletType { get; set; }
        //[DataMember]
        //public string BookletId { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public string PassportNumber { get; set; }
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        [DataMember]
        public Double Amount { get; set; }
        [DataMember]
        public int Pin { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}