using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanConvertor.Extension
{
    public static class TadbirStringExtension
    {
        private static Dictionary<char, char> DefaultMapping = new Dictionary<char, char>();

        static TadbirStringExtension()
        {
            DefaultMapping.Add('\u00B3', '\u0627'); //179 '³'	=> 'ا'
            DefaultMapping.Add('\u00B2', '\u0622'); //178 '²' => 'آ'
            DefaultMapping.Add('\u002f', '\u002f'); //47  '/' => '/'
            DefaultMapping.Add('\u0028', '\u0028'); //40  '(' => '('
            DefaultMapping.Add('\u0029', '\u0029'); //41  ')' => ')'
        }

        private static int GetFirstCharacterIndex(string[] stringArray, int index)
        {
            int count = -1;
            for (int i = 0; i <= index; i++)
            {
                count += stringArray[i].Length + 1;
            }
            return count;
        }

        private static string Replace(this string container, Dictionary<char, char> mapping, bool isSaman)
        {
            string[] splitStringArray = container.Split(mapping.Keys.ToArray());
            int splitCount = splitStringArray.Length;
            string concatedValue = string.Empty;

            for (int i = 0; i < splitCount; i++)
            {
                int index = GetFirstCharacterIndex(splitStringArray, i);
                char charValue = container.Length > index ? mapping[container[index]] : ' ';
                concatedValue = concatedValue.ConcatWithChar(splitStringArray[i], charValue, isSaman);
            }
            return concatedValue;
        }

        private static string ConcatWithChar(this string container, string value, char charValue = ' ', bool isSaman = false)
        {
            if (isSaman)
            {
                if (!string.IsNullOrEmpty(value) && Char.IsWhiteSpace(value[0]))
                {
                    container = char.IsWhiteSpace(charValue) ? (value.TrimStart() + ' ' + container) : (charValue + value.TrimStart() + ' ' + container);
                }
                else
                {
                    container = char.IsWhiteSpace(charValue) ? (container + value) : (charValue + value + container);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(value) && Char.IsWhiteSpace(value[0]))
                {
                    container = char.IsWhiteSpace(charValue) ? (value.TrimStart() + ' ' + container) : (charValue + value.TrimStart() + ' ' + container);
                }
                else
                {
                    container = char.IsWhiteSpace(charValue) ? (value + container) : (charValue + value + container);
                }
            }
            return container;
        }

        public static string ConvertToUnicode(this string container)
        {
            string result = container;
            foreach (var item in DefaultMapping)
            {
                result = result.Replace(DefaultMapping, false);
            }
            return result;
        }

        public static string ConvertToUnicode(this string container, Dictionary<char, char> mapping, bool isSaman)
        {
            string result = container;
            foreach (var item in mapping)
            {
                result = result.Replace(mapping, isSaman);
            }
            return result;
        }
    }
}
