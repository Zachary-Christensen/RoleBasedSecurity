using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedSecurityApp.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }

        public List<ProjectCounter> ProjectCounters { get; set; }

    }
}
