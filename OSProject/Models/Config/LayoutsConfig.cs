using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject.Models.Config
{
    public class LayoutsConfig
    {
        public DefaultLayoutCongfig DefaultLayoutCongfig { get; set; }
        public List<KeyboardLayout> LayoutsExamples { get; set; }

        public LayoutsConfig()
        {
        }

    }
}
