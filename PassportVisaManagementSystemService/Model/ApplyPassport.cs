using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public User User { get; set; }
        [DataMember]
        public Country Country { get; set; }
        [DataMember]
        public State State { get; set; }
        [DataMember]
        public City City { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string BookletType { get; set; }
        [DataMember]
        public string BookletId { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public string TempId { get; set; }
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        [DataMember]
        public Double Amount { get; set; }

    }
}