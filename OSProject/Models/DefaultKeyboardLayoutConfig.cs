using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject.Models
{
    public class DefaultKeyboardLayoutConfig
    {
        public string[] Lines { get; set; }

        public KeyboardLayout GetDefaultLayout()
        {
            StringBuilder text = new StringBuilder();
            foreach (var line in Lines)
            {
                text.Append(line);
                text.Append('\n');
            }

            return new KeyboardLayout("default", text.ToString());
        }
    
    }
}
