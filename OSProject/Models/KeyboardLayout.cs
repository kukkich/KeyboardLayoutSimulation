using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OSProject.Models
{
    [JsonObject]
    public class KeyboardLayout : IEnumerable<List<KeyboardButton>>
    {
        [JsonProperty]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [JsonIgnore]
        private string _name;
        [JsonProperty("Lines")]
        private List<List<KeyboardButton>> _lines;

        public KeyboardLayout(string name, string text)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("Пустое имя", nameof(name));
            if (String.IsNullOrEmpty(text)) throw new ArgumentException("Пустая строка", nameof(text));

            _name = name;
            _lines = new List<List<KeyboardButton>>();

            int lastLineIndex = 0;
            int lastButtonId = 0;
            foreach (string str in text.Split('\n'))
            {
                if (!String.IsNullOrEmpty(str))
                {
                    _lines.Add(new List<KeyboardButton>());
                    foreach (char sym in str)
                    {
                        _lines[lastLineIndex].Add(new KeyboardButton(lastButtonId, sym));
                        lastButtonId++;
                    }
                    lastLineIndex++;
                }
            }
        }

        //Required for serialization and deserialization
        public KeyboardLayout()
        {
            Name = "empty";
            _lines = new List<List<KeyboardButton>>();
        }

        public KeyboardLayout(string name, List<List<KeyboardButton>> lines)
            => (Name, _lines) = (name, lines);

        public char? GetBottonValue(int buttonId)
        {
            return _lines.FirstOrDefault(line =>
                    line.Any(button => button.Id == buttonId)
                )
                ?.FirstOrDefault(button => button.Id == buttonId)
                ?.Value;
        }

        public IEnumerator<List<KeyboardButton>> GetEnumerator()
            => _lines.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_lines).GetEnumerator();
        }
    }
}
