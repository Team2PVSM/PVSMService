using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    [DataContract]
    public class HintQuestion
    {
        [Key]
        [DataMember]
        public int Id { set; get; }
        [DataMember]
        public string Questions { set; get; }

        [DataMember]
        public virtual IEnumerable<User> UserEnumerable { set; get; }

    }
}