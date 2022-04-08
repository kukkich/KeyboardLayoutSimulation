using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject.Models
{
    public class KeyboardLayout : IEnumerable<List<KeyboardButton>>
    {
        // Сделать чтобы клавиши хранились по линиям в List<buttons>
        // или сделать класс линии, который будет хранить в себе кнопки
        // Сделать конструктор, принимающий StreamReader

        public string Name 
        { 
            get => _name;
            set => _name = value;
        }

        private string _name;
        private List<List<KeyboardButton>> _lines;

        public KeyboardLayout(string name, StreamReader stream)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            _name = name;
            _lines = new List<List<KeyboardButton>>();
            
            int lastLineIndex = 0;
            int lastButtonId = 0;
            while (!stream.EndOfStream)
            {
                _lines.Add(new List<KeyboardButton>());
                foreach (char sym in stream.ReadLine())
                {
                    _lines[lastLineIndex].Add(new KeyboardButton(lastButtonId, sym));
                    lastButtonId++;
                }
                lastLineIndex++;
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
