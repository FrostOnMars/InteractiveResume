using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveResume.Model
{
    public abstract class Education
    {
        public virtual string EducationSource { get; set; }
        public virtual string Date { get; set; }
    }
}

