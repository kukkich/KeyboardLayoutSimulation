using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject.Models.UI
{
    public interface IDataElement
    {
        object GetData(string key);
        bool HasKey(string key);
        void AddOrChangeValue(string key, object value);
    }
}
