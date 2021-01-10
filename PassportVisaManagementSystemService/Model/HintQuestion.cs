using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassportVisaManagementSystemService.Model
{
    public class HintQuestion
    {
        [DataMember]
        public int Id { set; get; }
        public string Questions { set; get; }

    }
}