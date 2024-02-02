using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderWinForms.Model
{

    public class ProjectModel
    {   
            public int id { get; set; }
            public string projectName { get; set; } = string.Empty;
            public string projectDescription { get; set; } = string.Empty;

            public string projectDirectory { get; set; } = string.Empty;

    }
    
}
