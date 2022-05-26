using System.Collections.Generic;
using System.Windows.Input;

namespace OSProject.Models
{
    public static class KeysConverter
    {
        public static readonly Dictionary<Key, char> ValueByKey = new Dictionary<Key, char>()
        {
            [Key.D0] = '0',
            [Key.D1] = '1',
            [Key.D2] = '2',
            [Key.D3] = '3',
            [Key.D4] = '4',
            [Key.D5] = '5',
            [Key.D6] = '6',
            [Key.D7] = '7',
            [Key.D8] = '8',
            [Key.D9] = '9',
            [Key.Q] = 'q',
            [Key.W] = 'w',
            [Key.E] = 'e',
            [Key.R] = 'r',
            [Key.T] = 't',
            [Key.Y] = 'y',
            [Key.U] = 'u',
            [Key.I] = 'i',
            [Key.O] = 'o',
            [Key.P] = 'p',
            [Key.A] = 'a',
            [Key.S] = 's',
            [Key.D] = 'd',
            [Key.F] = 'f',
            [Key.G] = 'g',
            [Key.H] = 'h',
            [Key.J] = 'j',
            [Key.K] = 'k',
            [Key.L] = 'l',
            [Key.Z] = 'z',
            [Key.X] = 'x',
            [Key.C] = 'c',
            [Key.V] = 'v',
            [Key.B] = 'b',
            [Key.N] = 'n',
            [Key.M] = 'm',
            [Key.Oem1] = ';',
            [Key.Oem3] = '`',
            [Key.Oem5] = '\\',
            [Key.Oem6] = ']',
            [Key.OemOpenBrackets] = '[',
            [Key.OemQuotes] = '\'',
            [Key.OemComma] = ',',
            [Key.OemPeriod] = '.',
            [Key.OemQuestion] = '/',
            [Key.OemPlus] = '=',
            [Key.OemMinus] = '-',
        };
    }
}
