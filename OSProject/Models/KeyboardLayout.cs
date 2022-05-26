using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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
        [JsonProperty("Lines")]
        public List<List<KeyboardButton>> Lines { get; set; }

        [JsonIgnore]
        private string _name;

        public KeyboardLayout(string name, string text)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("Пустое имя", nameof(name));
            if (String.IsNullOrEmpty(text)) throw new ArgumentException("Пустая строка", nameof(text));

            _name = name;
            Lines = new List<List<KeyboardButton>>();

            int lastLineIndex = 0;
            int lastButtonId = 0;
            foreach (string str in text.Split('\n'))
            {
                if (!String.IsNullOrEmpty(str))
                {
                    Lines.Add(new List<KeyboardButton>());
                    foreach (char sym in str)
                    {
                        Lines[lastLineIndex].Add(new KeyboardButton(lastButtonId, sym));
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
            Lines = new List<List<KeyboardButton>>();
        }

        public KeyboardLayout(string name, List<List<KeyboardButton>> lines)
            => (Name, Lines) = (name, lines);

        public char? GetBottonValue(int buttonId)
        {
            return Lines.FirstOrDefault(line =>
                    line.Any(button => button.Id == buttonId)
                )
                ?.FirstOrDefault(button => button.Id == buttonId)
                ?.Value;
        }

        public IEnumerator<List<KeyboardButton>> GetEnumerator()
            => Lines.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Lines).GetEnumerator();
        }
    }
}
