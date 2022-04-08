using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OSProject.Models.UI
{
    public class DataButton : Button, IDataElement
    {
        private readonly Dictionary<string, object> _data;

        public DataButton()
            : base()
        {
            _data = new Dictionary<string, object>();
        }

        public object GetData(string key)
        {
            if (_data.ContainsKey(key))
                return _data[key];
            throw new ArgumentException(nameof(key));
        }

        public bool HasKey(string key)
            => _data.ContainsKey(key);
        
        public void AddOrChangeValue(string key, object value)
        {
            if (_data.ContainsKey(key))
                _data[key] = value;
            else
                _data.Add(key, value);
        }
    }
}
