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
        public KeyboardLayout NewLayout
        {
            get
            {
                if (IsValid()) return _newLayout;
                else throw new InvalidOperationException(_problem);
            }
        }
        public KeyboardLayout _newLayout;
        public string Problem 
        { 
            get
            {
                if (IsValid()) return String.Empty;
                else return _problem;
            }
        }

        private readonly AppViewModel _appViewModel;
        private bool _hasException = false;
        private string _problem;

        public LayoutAddingViewModel(AppViewModel appViewModel, string name, string layout)
        {
            _appViewModel = appViewModel;
            try
            {
                _newLayout = new KeyboardLayout(name, layout);
                _hasException = false;
            }
            catch (ArgumentException ex)
            {
                _hasException = true;
                _problem = ex.Message;
            }
        }

        public void TryUpdate(string name, string layout)
        {
            try
            {
                _newLayout = new KeyboardLayout(name, layout);
                _hasException = false;
            }
            catch (ArgumentException ex)
            {
                _hasException = true;
                _problem = ex.Message;
            }
        }

        public bool IsValid()
        {
            if (_hasException) return false;

            var sameLayout = _appViewModel.Layouts.FirstOrDefault(layout => layout.Name == _newLayout.Name);
            if (!(sameLayout is null)) _problem = "Раскладка с таким именем уже существует"; 

            return sameLayout is null;
        }

    }
}
