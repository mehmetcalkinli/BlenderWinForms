using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlenderWinForms.Model.ObjectModelClass;

namespace BlenderWinForms.Model
{
    public class ProjectListModel
    {
        public ProjectModel projectModel { get; set; }
        public List<ObjectModel> objectModel { get; set; }
    }
}
