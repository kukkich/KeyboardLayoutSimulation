using System.Collections.Generic;

namespace OSProject.Models.Config
{
    public class LayoutsConfig
    {
        public DefaultLayoutConfig DefaultLayoutCongfig { get; set; }
        public List<KeyboardLayout> LayoutsExamples { get; set; }

        public LayoutsConfig()
        {
        }

    }
}
