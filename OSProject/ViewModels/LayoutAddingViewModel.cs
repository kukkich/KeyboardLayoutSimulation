using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSProject.Models;


namespace OSProject.ViewModels
{
    public class LayoutAddingViewModel
    {
        private DefaultKeyboardLayoutConfig _layoutConfig;
        
        public LayoutAddingViewModel(DefaultKeyboardLayoutConfig layoutConfig)
        {
            _layoutConfig = layoutConfig;
        }


    }
}
