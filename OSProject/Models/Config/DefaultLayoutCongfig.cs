using System;
using System.Text;

namespace OSProject.Models.Config
{
    public class DefaultLayoutCongfig
    {
        public string[] Lines
        {
            get => _lines;
            set
            {
                _lines = value;
                StringBuilder sb = new StringBuilder();
                foreach (var line in Lines)
                {
                    sb.Append(line);
                }
                _text = sb.ToString();
            }
        }

        private string[] _lines;
        private string _text { get; set; }

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

        public char GetCharacterById(int id)
        {
            if (id < 0 || id >= _text.Length)
                throw new ArgumentOutOfRangeException(nameof(id));
            return _text[id];
        }

        public int GetIdByCharacter(char character)
        {
            return _text.IndexOf(character);
        }
    }
}
