using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OSProject.Models
{
    public class KeyboardLayout : IEnumerable<List<KeyboardButton>>
    {
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private string _name;
        private List<List<KeyboardButton>> _lines;
        private static int _maxStringLength = 15;
        private static int _maxStringNumber = 5;

        public char? GetBottonValue(int buttonId)
        {
            return _lines.FirstOrDefault(line =>
                    line.Any(button => button.Id == buttonId)
                )
                ?.FirstOrDefault(button => button.Id == buttonId)
                ?.Value;
        }

        public KeyboardLayout(string name, StreamReader stream)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("Пустое имя", nameof(name));
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            _name = name;
            _lines = new List<List<KeyboardButton>>();

            int lastLineIndex = 0;
            int lastButtonId = 0;
            while (!stream.EndOfStream)
            {
                if (lastLineIndex > _maxStringNumber)
                    throw new ArgumentException("To much strings", nameof(stream));

                var str = stream.ReadLine();
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
                if (str.Length > _maxStringLength)
                    throw new ArgumentException(str, nameof(text));
                if (lastLineIndex > _maxStringNumber)
                    throw new ArgumentException("Много строк в раскладке", nameof(text));

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

        public IEnumerator<List<KeyboardButton>> GetEnumerator()
            => _lines.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_lines).GetEnumerator();
        }
    }
}
