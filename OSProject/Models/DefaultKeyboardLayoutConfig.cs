﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject.Models
{
    public class DefaultKeyboardLayoutConfig
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

    }
}