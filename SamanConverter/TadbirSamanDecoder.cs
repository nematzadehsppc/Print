using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamanConvertor.Extension;

namespace SamanConvertor
{
    public static class CharacterMapping
    {
        private static Dictionary<char, char> _UnicodeNumbersMapping = null;

        private static Dictionary<char, char> _UnicodeSymbolsMapping = null;

        private static Dictionary<char, char> _UnicodeCharactersMapping = null;

        static CharacterMapping()
        {
            if (_UnicodeNumbersMapping == null)
            {
                _UnicodeNumbersMapping = new Dictionary<char, char>();

                _UnicodeNumbersMapping.Add('\x00B1', '\u0661'); //       '\x00B1'    => 1
                _UnicodeNumbersMapping.Add('\x201A', '\u0662'); //       '\x201A'    => 2
                _UnicodeNumbersMapping.Add('\x0192', '\u0663'); //       '\x0192'    => 3
                _UnicodeNumbersMapping.Add('\x201E', '\u0664'); //       '\x201E'    => 4
                _UnicodeNumbersMapping.Add('\x2062', '\u0665'); //       '\x2062'    => 5
                _UnicodeNumbersMapping.Add('\x2020', '\u0666'); //       '\x2020'    => 6
                _UnicodeNumbersMapping.Add('\x2021', '\u0667'); //       '\x2021'    => 7
                _UnicodeNumbersMapping.Add('\x02C6', '\u0668'); //       '\x02C6'    => 8
                _UnicodeNumbersMapping.Add('\x2030', '\u0669'); //       '\x2030'    => 9
                _UnicodeNumbersMapping.Add('\x00B9', '\u0660'); //       '\x00B9'    => 0

                _UnicodeNumbersMapping.Add('\x0080', '\u0660'); //       '\x0080'    => 0
                _UnicodeNumbersMapping.Add('\x0081', '\u0661'); //       '\x0081'    => 1  => Vista Error
                _UnicodeNumbersMapping.Add('\x0082', '\u0662'); //       '\x0082'    => 2
                _UnicodeNumbersMapping.Add('\x0083', '\u0663'); //       '\x0083'    => 3
                _UnicodeNumbersMapping.Add('\x0084', '\u0664'); //       '\x0084'    => 4
                _UnicodeNumbersMapping.Add('\x0085', '\u0665'); //       '\x0085'    => 5
                _UnicodeNumbersMapping.Add('\x0086', '\u0666'); //       '\x0086'    => 6
                _UnicodeNumbersMapping.Add('\x0087', '\u0667'); //       '\x0087'    => 7
                _UnicodeNumbersMapping.Add('\x0088', '\u0668'); //       '\x0088'    => 8
                _UnicodeNumbersMapping.Add('\x0089', '\u0669'); //       '\x0089'    => 9

                _UnicodeNumbersMapping.Add('\u2026', '\u0665'); //       '\u2026'    => 5
            }

            if (_UnicodeSymbolsMapping == null)
            {
                _UnicodeSymbolsMapping = new Dictionary<char, char>();

                _UnicodeSymbolsMapping.Add('\u00B3', '\u0627'); //   '\d0179'   -->  '³'	=>  'ا'
                _UnicodeSymbolsMapping.Add('\u00B2', '\u0622'); //   '\d0178'   -->  '²'  =>  'آ'
                _UnicodeSymbolsMapping.Add('\u002f', '\u002f'); //   '\d0047'   -->  '/'  =>  '/'
                _UnicodeSymbolsMapping.Add('\u0028', '\u0028'); //   '\d0040'   -->  '('  =>  '('
                _UnicodeSymbolsMapping.Add('\u0029', '\u0029'); //   '\d0041'   -->  ')'  =>  ')'
                _UnicodeSymbolsMapping.Add('\u00AD', '\u00AD'); //   '\d0173'   -->  '-'  =>  '-'
                _UnicodeSymbolsMapping.Add('\u002D', '\u002D'); //   '\d0045'   -->  '-'  =>  '-'
                _UnicodeSymbolsMapping.Add('\u2012', '\u2012'); //   '\d8210'   -->  ':'  =>  ':'
                _UnicodeSymbolsMapping.Add('\u003A', '\u003A'); //   '\d8210'   -->  ':'  =>  ':'
            }

            if (_UnicodeCharactersMapping == null)
            {
                _UnicodeCharactersMapping = new Dictionary<char, char>();

                _UnicodeCharactersMapping.Add('\u0627', '\u0627');
                _UnicodeCharactersMapping.Add('\u0628', '\u0628');
                _UnicodeCharactersMapping.Add('\u0629', '\u0629');
                _UnicodeCharactersMapping.Add('\u0630', '\u0630');
                _UnicodeCharactersMapping.Add('\u0631', '\u0631');
                _UnicodeCharactersMapping.Add('\u0632', '\u0632');
                _UnicodeCharactersMapping.Add('\u0633', '\u0633');
                _UnicodeCharactersMapping.Add('\u0634', '\u0634');
                _UnicodeCharactersMapping.Add('\u0635', '\u0635');
                _UnicodeCharactersMapping.Add('\u0636', '\u0636');
                _UnicodeCharactersMapping.Add('\u0637', '\u0637');
                _UnicodeCharactersMapping.Add('\u0638', '\u0638');
                _UnicodeCharactersMapping.Add('\u0639', '\u0639');
                _UnicodeCharactersMapping.Add('\u0640', '\u0640');
                _UnicodeCharactersMapping.Add('\u0641', '\u0641');
                _UnicodeCharactersMapping.Add('\u0642', '\u0642');
                _UnicodeCharactersMapping.Add('\u0643', '\u0643');
                _UnicodeCharactersMapping.Add('\u0644', '\u0644');
                _UnicodeCharactersMapping.Add('\u0645', '\u0645');
                _UnicodeCharactersMapping.Add('\u0646', '\u0646');
                _UnicodeCharactersMapping.Add('\u0647', '\u0647');
                _UnicodeCharactersMapping.Add('\u0648', '\u0648');
                _UnicodeCharactersMapping.Add('\u0649', '\u0649');
                _UnicodeCharactersMapping.Add('\u0650', '\u0650');
                _UnicodeCharactersMapping.Add('\u0651', '\u0651');
                _UnicodeCharactersMapping.Add('\u0652', '\u0652');
                _UnicodeCharactersMapping.Add('\u0653', '\u0653');
                _UnicodeCharactersMapping.Add('\u0654', '\u0654');
                _UnicodeCharactersMapping.Add('\u0655', '\u0655');
                _UnicodeCharactersMapping.Add('\u0656', '\u0656');
                _UnicodeCharactersMapping.Add('\u0657', '\u0657');
                _UnicodeCharactersMapping.Add('\u0658', '\u0658');
                _UnicodeCharactersMapping.Add('\u0659', '\u0659');
                _UnicodeCharactersMapping.Add('\u0660', '\u0660');
                _UnicodeCharactersMapping.Add('\u0661', '\u0661');
                _UnicodeCharactersMapping.Add('\u0662', '\u0662');
                _UnicodeCharactersMapping.Add('\u0663', '\u0663');
                _UnicodeCharactersMapping.Add('\u0664', '\u0664');
                _UnicodeCharactersMapping.Add('\u0665', '\u0665');
                _UnicodeCharactersMapping.Add('\u0666', '\u0666');
                _UnicodeCharactersMapping.Add('\u0667', '\u0667');
                _UnicodeCharactersMapping.Add('\u0668', '\u0668');
                _UnicodeCharactersMapping.Add('\u0669', '\u0669');
                _UnicodeCharactersMapping.Add('\u0670', '\u0670');
                _UnicodeCharactersMapping.Add('\u0671', '\u0671');
                _UnicodeCharactersMapping.Add('\u0672', '\u0672');
                _UnicodeCharactersMapping.Add('\u0673', '\u0673');
                _UnicodeCharactersMapping.Add('\u0674', '\u0674');
                _UnicodeCharactersMapping.Add('\u0675', '\u0675');
                _UnicodeCharactersMapping.Add('\u0676', '\u0676');
                _UnicodeCharactersMapping.Add('\u0677', '\u0677');
                _UnicodeCharactersMapping.Add('\u0678', '\u0678');
                _UnicodeCharactersMapping.Add('\u0679', '\u0679');
                _UnicodeCharactersMapping.Add('\u0680', '\u0680');
                _UnicodeCharactersMapping.Add('\u0681', '\u0681');
                _UnicodeCharactersMapping.Add('\u0682', '\u0682');
                _UnicodeCharactersMapping.Add('\u0683', '\u0683');
                _UnicodeCharactersMapping.Add('\u0684', '\u0684');
                _UnicodeCharactersMapping.Add('\u0685', '\u0685');
                _UnicodeCharactersMapping.Add('\u0686', '\u0686');
                _UnicodeCharactersMapping.Add('\u0687', '\u0687');
                _UnicodeCharactersMapping.Add('\u0688', '\u0688');
                _UnicodeCharactersMapping.Add('\u0689', '\u0689');
                _UnicodeCharactersMapping.Add('\u0690', '\u0690');
                _UnicodeCharactersMapping.Add('\u0691', '\u0691');
                _UnicodeCharactersMapping.Add('\u0692', '\u0692');
                _UnicodeCharactersMapping.Add('\u0693', '\u0693');
                _UnicodeCharactersMapping.Add('\u0694', '\u0694');
                _UnicodeCharactersMapping.Add('\u0695', '\u0695');
                _UnicodeCharactersMapping.Add('\u0696', '\u0696');
                _UnicodeCharactersMapping.Add('\u0697', '\u0697');
                _UnicodeCharactersMapping.Add('\u0698', '\u0698');
                _UnicodeCharactersMapping.Add('\u0699', '\u0699');
            }
        }

        public static Dictionary<char, char> UnicodeNumbers
        {
            get
            {
                return _UnicodeNumbersMapping;
            }
        }

        public static Dictionary<char, char> UnicodeSymbols
        {
            get
            {
                return _UnicodeSymbolsMapping;
            }
        }

        public static Dictionary<char, char> UnicodeCharacters
        {
            get
            {
                return _UnicodeCharactersMapping;
            }
        }
    }

    public class TadbirSamanDecoder
    {
        public static string DecodeOnlyTadbirStrings(string strSource, bool usePersianDigits, bool usePersianYeh, bool insertHalfSpaces, bool correctParantheses)
        {
            Array.ForEach(CharacterMapping.UnicodeNumbers.ToArray(), t => strSource = strSource.Replace(t.Key, t.Value));
            return strSource;

            //string result = strSource.ConvertToUnicode(CharacterMapping.UnicodeSymbols, false);
            //return strSource.ConvertToUnicode(CharacterMapping.UnicodeSymbols, false);
        }
    }
}









