using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SamanConvertor.Extension;

namespace SamanConvertor
{
    public class TadbirDecoder
    {
        #region Main Method
        internal static string DecodeOnlyTadbirStrings(string strSource, bool usePersianDigits, bool usePersianYeh, bool insertHalfSpaces, bool correctParantheses)
        {

            PrepareStaticArrays();

            char[] Endings =
                new char[] { '\n', '\r', '\"', '\'' };

            string strDecoded = "";
            int idxLastCopied = 0;

            bool NeedsDigitReplacement = false;
            int idxFirst = strSource.IndexOfAny(_TadbirChars);
            if (idxFirst != -1)
                NeedsDigitReplacement = true;
            else
                NeedsDigitReplacement = strSource.IndexOfAny(_DigitsSet) != -1;

            while (idxFirst != -1)
            {
                while (idxFirst > 0 && (strSource[idxFirst - 1] == ')' || strSource[idxFirst - 1] == '('))
                    idxFirst--;
                if (idxLastCopied != idxFirst)
                {
                    strDecoded += strSource.Substring(idxLastCopied, idxFirst - idxLastCopied);
                    idxLastCopied = idxFirst;
                }
                int idxLast = strSource.IndexOfAny(Endings, idxFirst); // checking quotation marks here is something you can call cheating
                if (idxLast == -1)
                    idxLast = strSource.Length - 1;
                else
                {
                    idxLast = idxFirst + 1 + strSource.Substring(idxFirst + 1, idxLast - idxFirst).LastIndexOfAny(_TadbirChars);
                }

                if (!string.IsNullOrEmpty(strSource) &&
                    strSource.Substring(idxFirst, idxLast - idxFirst + 1) != null &&
                    strSource.Substring(idxFirst, idxLast - idxFirst + 1).Length > 0 &&
                    (strSource.Substring(idxFirst, idxLast - idxFirst + 1)[0] == '\x00B3' ||
                    strSource.Substring(idxFirst, idxLast - idxFirst + 1)[0] == '\x00B2'))
                {
                    strDecoded = strSource.ConvertToUnicode();
                }
                else
                {
                    strDecoded += DecodePartly(strSource.Substring(idxFirst, idxLast - idxFirst + 1), usePersianYeh, insertHalfSpaces);
                }
                idxLastCopied = idxLast + 1;
                idxFirst = strSource.IndexOfAny(_TadbirChars, idxLastCopied);
                while (idxFirst > 0 && (strSource[idxFirst - 1] == ')' || strSource[idxFirst - 1] == '('))
                    idxFirst--;
            }
            if (idxLastCopied != idxFirst && idxLastCopied < strSource.Length)
                strDecoded += strSource.Substring(idxLastCopied);
            if (correctParantheses)
                strDecoded = ReverseParanthesesInPersianContext(strDecoded);
            if (NeedsDigitReplacement)
                return ReplaceDigits(strDecoded, usePersianDigits);

            return strDecoded;
        }

        private static string DecodeAllStrings(string strSource, bool usePersianDigits, bool usePersianYeh, bool insertHalfSpaces, bool correctParantheses)
        {

            PrepareStaticArrays();

            char[] Endings =
                new char[] { '\n', '\r' };



            string strDecoded = "";

            bool NeedsDigitReplacement = strSource.IndexOfAny(_DigitsSet) != -1;
            int idxFirst = 0;

            while (idxFirst < strSource.Length)
            {
                int idxLast = strSource.IndexOfAny(Endings, idxFirst);
                if (idxLast == -1)
                    idxLast = strSource.Length - 1;
                else
                {
                    idxLast = idxFirst + 1 + strSource.Substring(idxFirst + 1, idxLast - idxFirst).LastIndexOfAny(_TadbirChars);
                }

                strDecoded += DecodePartly(strSource.Substring(idxFirst, idxLast - idxFirst + 1), usePersianYeh, insertHalfSpaces);
                idxFirst = idxLast + 1;
            }
            if (correctParantheses)
                strDecoded = ReverseParanthesesInPersianContext(strDecoded);
            if (NeedsDigitReplacement)
                return ReplaceDigits(strDecoded, usePersianDigits);

            return strDecoded;


        }
        public static byte[] GetMappedString(string strSource)
        {
            if (_dicMap == null)
            {
                _dicMap = new Dictionary<char, byte>();
                _dicMap['š'] = 0x9A;
                _dicMap['Ÿ'] = 0x9F;
                _dicMap['Ž'] = 0x8E;
                _dicMap['ž'] = 0x9E;
                _dicMap['–'] = 0x96;
                _dicMap['—'] = 0x97;
                _dicMap['‘'] = 0x91;
                _dicMap['’'] = 0x92;
                _dicMap['“'] = 0x93;
                _dicMap['”'] = 0x94;
                _dicMap['•'] = 0x95;
                _dicMap['›'] = 0x9B;
                _dicMap['™'] = 0x98;
                _dicMap['ƒ'] = 0x83;//3
                _dicMap['ˆ'] = 0x88;//8
                _dicMap['‚'] = 0x82;//2
                _dicMap['„'] = 0x84;//4
                _dicMap['‡'] = 0x87;//7
                _dicMap['†'] = 0x86;//6
                _dicMap['…'] = 0x85;//5
                _dicMap['‰'] = 0x89;//9

            }
            byte[] arrSource = new byte[strSource.Length];
            for (int i = 0; i < strSource.Length; i++)
            {
                byte b;
                if (_dicMap.TryGetValue(strSource[i], out b))
                    arrSource[i] = b;
                else
                    arrSource[i] = (byte)strSource[i];
            }
            return arrSource;

        }
        #endregion
        #region Variables, helper methods, ...



        /// <remarks>
        /// actually it does not do anything about digits and parantheres, for
        /// this reason I call it DecodePartly
        /// </remarks>
        private static string DecodePartly(string strSource, bool usePersianYeh, bool insertHalfSpaces)
        {
            return TadbirStrRev(PerformCharacterReplacements(strSource, usePersianYeh, insertHalfSpaces));
        }


        public static string ReplaceDigits(string strSource, bool usePersianDigits)
        {

            return strSource.Replace('ƒ', usePersianDigits ? _PersianDigits[3] : _LatinDigits[3])//402            //3
            .Replace('ˆ', usePersianDigits ? _PersianDigits[8] : _LatinDigits[8])//710       //8
            .Replace('‚', usePersianDigits ? _PersianDigits[2] : _LatinDigits[2])//8218  //2
            .Replace('„', usePersianDigits ? _PersianDigits[4] : _LatinDigits[4])//8222  //4
            .Replace('‡', usePersianDigits ? _PersianDigits[7] : _LatinDigits[7])//8225  //7
            .Replace('†', usePersianDigits ? _PersianDigits[6] : _LatinDigits[6])//8224  //6
            .Replace('…', usePersianDigits ? _PersianDigits[5] : _LatinDigits[5])//8230  //5
            .Replace('‰', usePersianDigits ? _PersianDigits[9] : _LatinDigits[9])//8240  //9
            .Replace((char)0x80, usePersianDigits ? _PersianDigits[0] : _LatinDigits[0])//0
            .Replace((char)0x81, usePersianDigits ? _PersianDigits[1] : _LatinDigits[1])//1
            .Replace((char)0x82, usePersianDigits ? _PersianDigits[2] : _LatinDigits[2])//2
            .Replace((char)0x83, usePersianDigits ? _PersianDigits[3] : _LatinDigits[3])//3
            .Replace((char)0x84, usePersianDigits ? _PersianDigits[4] : _LatinDigits[4])//4
            .Replace((char)0x85, usePersianDigits ? _PersianDigits[5] : _LatinDigits[5])//5
            .Replace((char)0x86, usePersianDigits ? _PersianDigits[6] : _LatinDigits[6])//6
            .Replace((char)0x87, usePersianDigits ? _PersianDigits[7] : _LatinDigits[7])//7
            .Replace((char)0x88, usePersianDigits ? _PersianDigits[8] : _LatinDigits[8])//8
            .Replace((char)0x89, usePersianDigits ? _PersianDigits[9] : _LatinDigits[9])//9
            .Replace((char)0xB1, usePersianDigits ? _PersianDigits[1] : _LatinDigits[1])//1
            .Replace((char)0xB9, usePersianDigits ? _PersianDigits[0] : _LatinDigits[0])//0
            ;
        }

        private static Dictionary<char, byte> _dicMap = null;



        private static string PerformCharacterReplacements(string strSource, bool usePersianYeh, bool insertHalfSpaces)
        {
            if (insertHalfSpaces)
                strSource = ApplyCharacterShapings(strSource);
            return strSource.Replace('Š', '،')//352
            .Replace('š', 'ج')//353
            .Replace('Ÿ', 'ح')//376//؟
            .Replace('Ž', 'ئ')//381
            .Replace('ž', 'ح')//382
            .Replace('–', 'ت')//8211
            .Replace('—', 'ت')//8212
            .Replace('‘', 'ا')//8216
            .Replace('’', 'ب')//8217
            .Replace('“', 'ب')//8220
            .Replace('”', 'پ')//8221            
            .Replace('•', 'پ')//8226          
            .Replace('›', 'ج')//8250  
            .Replace('™', 'ث')//8482





            //Alef replacements:
            .Replace((char)0x90, 'ا')
            .Replace((char)0x8D, 'آ')
            .Replace((char)0xB2, 'آ')
            .Replace((char)0xB3, 'ا')
            .Replace((char)0x91, 'ا')

            //Be
            .Replace((char)0x92, 'ب')
            .Replace((char)0x93, 'ب')

            //Pe
            .Replace((char)0x94, 'پ')
            .Replace((char)0x95, 'پ')

            //Te
            .Replace((char)0x96, 'ت')
            .Replace((char)0x97, 'ت')

            //The
            .Replace((char)0x98, 'ث')
            .Replace((char)0x99, 'ث')

            //Jim
            .Replace((char)0x9A, 'ج')
            .Replace((char)0x9B, 'ج')

            //Che
            .Replace((char)0x9C, 'چ')
            .Replace((char)0x9D, 'چ')
            .Replace((char)0xD5, 'چ')

            //He
            .Replace((char)0x9E, 'ح')
            .Replace((char)0x9F, 'ح')
            .Replace((char)0xB6, 'ح')

            //Khe
            .Replace((char)0xA0, 'خ')
            .Replace((char)0xA1, 'خ')
            .Replace((char)0xB5, 'خ')

            //Sin
            .Replace((char)0xA7, 'س')
            .Replace((char)0xA8, 'س')

            //Shin
            .Replace((char)0xA9, 'ش')
            .Replace((char)0xAA, 'ش')

            //Sad
            .Replace((char)0xAB, 'ص')
            .Replace((char)0xAC, 'ص')

            //Zad
            .Replace((char)0xAD, 'ض')
            .Replace((char)0xAE, 'ض')

            //Za	
            .Replace((char)0xB0, 'ظ')
            .Replace((char)0xE0, 'ظ')


            //Ta
            .Replace((char)0xAF, 'ط')


            //eyn
            .Replace((char)0xE4, 'ع')
            .Replace((char)0xE4, 'ع')
            .Replace((char)0xE3, 'ع')
            .Replace((char)0xE2, 'ع')
            .Replace((char)0xE1, 'ع')

            //Gheyn
            .Replace((char)0xE8, 'غ')
            .Replace((char)0xE7, 'غ')
            .Replace((char)0xE6, 'غ')
            .Replace((char)0xE5, 'ع')

            //Fe
            .Replace((char)0xEA, 'ف')
            .Replace((char)0xE9, 'ف')

            //Ghaaf
            .Replace((char)0xEC, 'ق')
            .Replace((char)0xEB, 'ق')

            //Kaaf
            .Replace((char)0xEE, 'ک')
            .Replace((char)0xED, 'ک')

            //Gaaf
            .Replace((char)0xF0, 'گ')
            .Replace((char)0xEF, 'گ')


            //Lam
            .Replace((char)0xF1, 'ل')
            .Replace((char)0xF3, 'ل')

            //Laa
            .Replace(_Laa1, "ال")
            .Replace(_Laa2, "ال")


            //Mim
            .Replace((char)0xF4, 'م')
            .Replace((char)0xF5, 'م')

            //Noon
            .Replace((char)0xF6, 'ن')
            .Replace((char)0xF7, 'ن')

            //He
            .Replace((char)0xF9, 'ه')
            .Replace((char)0xFB, 'ه')
            .Replace((char)0xFA, 'ه')


            //Ye
            .Replace((char)0xFD, usePersianYeh ? 'ی' : 'ي')
            .Replace((char)0xFE, usePersianYeh ? 'ی' : 'ي')
            .Replace((char)0xFC, usePersianYeh ? 'ی' : 'ي')
            .Replace((char)0xDF, usePersianYeh ? 'ی' : 'ي')

            //Vav
            .Replace((char)0xF8, 'و')

            .Replace((char)0xB7, '-')
            .Replace((char)0xB8, '،')
            .Replace((char)0x8A, ',')//ممیز فارسی
            .Replace((char)0x8B, '-')
            .Replace((char)0x8B, '؟')


            .Replace((char)0x8E, 'ئ')
            .Replace((char)0xB4, 'ئ')
            .Replace((char)0x8F, 'ء')
            .Replace((char)0xD6, 'ء')


            //Dal
            .Replace((char)0xA2, 'د')

            //Zal
            .Replace((char)0xA3, 'ذ')

            //Ra
            .Replace((char)0xA4, 'ر')

            //Z
            .Replace((char)0xA5, 'ز')

            //ZH
            .Replace((char)0xA6, 'ز')

            //?
            .Replace('Œ', '؟');


        }

        /// <summary>
        /// insert half space character whenever a capital shaped saman character exists without any space after it
        /// </summary>
        /// <remarks>
        /// should be called before reversing string and on non-decodes saman string
        /// </remarks>
        private static string ApplyCharacterShapings(string strSource)
        {
            string halfSpace = "" + (char)8204;
            int i = 1;
            while (i < strSource.Length)
            {
                if (Array.IndexOf(_CapitalShapedChars, strSource[i]) != -1)
                    if (strSource[i - 1] != ' ' && strSource[i - 1] != '(' && strSource[i - 1] != ')' && strSource[i - 1] != 'Š'/*,*/ && Array.IndexOf(_TadbirChars, strSource[i - 1]) == -1)
                    {
                        strSource = strSource.Insert(i, halfSpace);
                        i++;
                    }
                i++;
            }
            return strSource;
        }

        private static char[] _TadbirChars = null;
        private static char[] extendedSet = new char[]
            {


            'Š', //'،'//352
            'š', //'ج'//353
            'Ÿ', //'ح'//376//؟
            'Ž', //'ئ'
            'ž', //'ح'//382
            '–', //'ت'//8211
            '—', //'ت'//8212
            '‘', //'ا'//8216
            '’', //'ب'//8217
            '“', //'ب'//8220
            '”', //'پ'//8221            
            '•', //'پ'//8226          
            '›', //'ج'//8250  
            '™', //'ث'//8482
            'Œ', //0x8C

            };
        private static char[] _DigitsSet = new char[]
        {

            'ƒ', //402    //3
            'ˆ', //710    //8
            '‚', //8218  //2
            '„', //8222  //4
            '‡', //8225  //7
            '†', //8224  //6
            '…', //8230  //5
            '‰', //8240  //9

             (char)0x80,
             (char)0x81,
             (char)0x82,
             (char)0x83,
             (char)0x84,
             (char)0x85,
             (char)0x86,
             (char)0x87,
             (char)0x88,
             (char)0x80,

             (char)0xB1,
             (char)0xB9

        };

        private static char[] _PersianDigits =
        {
            '۰',
            '۱',
            '۲',
            '۳',
            '۴',
            '۵',
            '۶',
            '۷',
            '۸',
            '۹'
        };

        private static char[] _LatinDigits =
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9'
        };

        private static char[] _CapitalShapedChars = 
        {
            (char)0x92,//ب
            (char)0x94,//پ
            (char)0x96,//ت
            (char)0x98,//ث
            (char)0x9A,//ج
            (char)0x9C,//چ
            (char)0x9E,//ح
            (char)0x9E,//ح
            (char)0xA0,//خ
            (char)0xA7,//س
            (char)0xA9,//ش
            (char)0xAB,//ص
            (char)0xAD,//ض
            (char)0xAD,//ض
            (char)0xB5,//خ
            (char)0xB6,//خ
            (char)0xE1,//ع
            (char)0xE5,//غ
            (char)0xE9,//ف
            (char)0xEB,//ق
            (char)0xED,//ک
            (char)0xEF,//گ
            (char)0xF1,//ل
            (char)0xF4,//م
            (char)0xF6,//ن
            (char)0xF9,//ه
            (char)0xFC,//ی
            (char)0xFD,//ی


            'š',//=(char)353 ج
            'ž',//=(char)382 ح
            '–',//=(char)8211 ت
            '’',//=(char)8217 ب
            '”',//=(char)8221 پ

        };


        private static string _Laa1 = "" + (char)0xF2;
        private static string _Laa2 = String.Format("%c%c", 0x90, 0xF2);
        private static string TadbirStrRev(string strSource, bool fromRecursion = false)
        {
            if (string.IsNullOrEmpty(strSource))
                return strSource;
            if (!fromRecursion)
                if (OnlyIncludesParanthesesAndSpace(strSource))
                    return strSource;

            string strStartNonTadbir = "";
            int sdx = 0;
            if (strSource.Length > 0)
                if ("اآبپتثجچحخدذرزژسشصضطظکگعغفقلمنوهیيئ".IndexOf(strSource[0]) == -1)
                {
                    int idx = 0;
                    while (idx < strSource.Length &&
                        ("اآبپتثجچحخدذرزژسشصضطظکگعغفقلمنوهیيئ".IndexOf(strSource[idx]) == -1
                        ||
                        strSource[idx] == ' '
                        )
                        )
                        idx++;

                    //restore trailing parantheses to tadbir part:                                        
                    if (idx > 0)
                        while (idx > 0 && (
                            (strSource[idx - 1] == '(') || (strSource[idx - 1] == ')'))
                            )
                            idx--;


                    strStartNonTadbir = strSource.Substring(0, idx);
                    sdx = idx;
                }



            int mdx = sdx;
            if (strSource.Length > mdx)
                while (mdx < strSource.Length &&
                    ("اآبپتثجچحخدذرزژسشصضطظکگعغفقلمنوهیيئ".IndexOf(strSource[mdx]) != -1
                    ||
                    strSource[mdx] == '('
                    ||
                    strSource[mdx] == ')'

                     )
                    )
                {
                    mdx++;
                }


            if (sdx == 0 && mdx == strSource.Length)
                return StrRev(strSource);//ALL TADBIR, SO JUST REVERSE IT



            string strTadbir = "";
            if (mdx > sdx && mdx <= strSource.Length)
                strTadbir = strSource.Substring(sdx, mdx - sdx);

            return TadbirStrRev(strSource.Substring(mdx), true) + StrRev(strTadbir) + strStartNonTadbir;
        }
        private static string StrRev(string strSource)
        {
            char[] chrSource = strSource.ToCharArray();
            Array.Reverse(chrSource);
            strSource = new string(chrSource);

            return strSource;
        }

        ///<remarks>
        ///Call for a unicode Persian string with wrong parantheses
        ///</remarks>
        private static string ReverseParanthesesInPersianContext(string strSource)
        {
            for (int i = 0; i < strSource.Length; i++)
                if (strSource[i] == ')')
                {
                    if (ShouldReverseParantheseInPosition(strSource, i))
                        strSource = strSource.Remove(i, 1).Insert(i, "(");
                }
                else
                    if (strSource[i] == '(')
                    {
                        if (ShouldReverseParantheseInPosition(strSource, i))
                            strSource = strSource.Remove(i, 1).Insert(i, ")");
                    }
            return strSource;
        }

        private static bool ShouldReverseParantheseInPosition(string strSource, int i)
        {
            if (!(PrecedesWithQuotationMarkOrIsInBOLF(strSource, i) && ForecedesWithQuotationMarkOrIsInEOLF(strSource, i)))
            {
                if (PrecedesWithPersianContext(strSource, i))
                {
                    if (ForecedesWithPersianContext(strSource, i) || ForecedesWithQuotationMarkOrIsInEOLF(strSource, i))
                    {
                        return true;
                    }
                }
                else
                    if (ForecedesWithPersianContext(strSource, i))
                    {
                        if (PrecedesWithQuotationMarkOrIsInBOLF(strSource, i))
                            return true;
                    }
            }
            return false;
        }

        private static bool PrecedesWithPersianContext(string strSource, int idx)
        {
            bool bPrecedesWithPersianContext = false;
            int j = idx - 1;
            while (j > 0)
                if (strSource[j] == ' ')
                    j--;
                else
                    if (strSource[j] == '\"' || strSource[j] == '\'' || strSource[j] == '\n' || strSource[j] == '\r')
                        return false;
                    else
                        break;

            if (j > 0)
                if ("اآبپتثجچحخدذرزژسشصضطظکگعغفقلمنوهیيئ".IndexOf(strSource[j]) != -1)
                    bPrecedesWithPersianContext = true;
            return bPrecedesWithPersianContext;
        }

        private static bool ForecedesWithPersianContext(string strSource, int idx)
        {
            bool bForeedesWithPersianContext = false;
            int j = idx + 1;
            while (j < strSource.Length)
                if (strSource[j] == ' ')
                    j++;
                else
                    if (strSource[j] == '\"' || strSource[j] == '\'' || strSource[j] == '\n' || strSource[j] == '\r')
                        return false;
                    else
                        break;
            if (j < strSource.Length)
                if ("اآبپتثجچحخدذرزژسشصضطظکگعغفقلمنوهیيئ".IndexOf(strSource[j]) != -1)
                    bForeedesWithPersianContext = true;
            return bForeedesWithPersianContext;
        }

        private static bool PrecedesWithQuotationMarkOrIsInBOLF(string strSource, int idx)
        {
            int j = idx - 1;
            while (j > 0)
                if (strSource[j] == ' ' || strSource[j] == '(' || strSource[j] == ')')
                    j--;
                else
                    break;
            if (j > 0)
            {
                if (strSource[j] == '\"' || strSource[j] == '\'' || strSource[j] == '\n' || strSource[j] == '\r')
                    return true;
            }
            else
                return true;
            return false;
        }

        private static bool ForecedesWithQuotationMarkOrIsInEOLF(string strSource, int idx)
        {
            int j = idx + 1;
            while (j < strSource.Length)
                if (strSource[j] == ' ' || strSource[j] == '(' || strSource[j] == ')')
                    j++;
                else
                    break;
            if (j < strSource.Length)
            {
                if (strSource[j] == '\"' || strSource[j] == '\'' || strSource[j] == '\n' || strSource[j] == '\r')
                    return true;
            }
            else
                return true;
            return false;
        }




        private static bool OnlyIncludesParanthesesAndSpace(string strSource)
        {
            for (int i = 0; i < strSource.Length; i++)
                if ("() ".IndexOf(strSource[i]) == -1)
                    return false;
            return true;
        }

        private static void PrepareStaticArrays()
        {
            if (_TadbirChars == null)
            {
                List<char> tadBirChars = new List<char>();
                for (int x = /*0x80*/0x8A; x <= 0xFE; x++)
                    if (x != 0xB1 && x != 0xB9)
                        tadBirChars.Add((char)x);
                foreach (char eChar in extendedSet)
                    tadBirChars.Add(eChar);

                tadBirChars.Add((char)8204);//half space unicode character, which may be inserted by ourselves

                _TadbirChars = tadBirChars.ToArray();
            }
        }

        /// <summary>
        /// جایگزینی اعداد لاتین با فارسی در یک رشته یونیکد عادی (ربطی به سامان ندارد)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string PersianizeDigits(string input)
        {
            for (int i = 0; i < _LatinDigits.Length; i++)
                input = input.Replace(_LatinDigits[i], _PersianDigits[i]);
            return input;
        }

        /// <summary>
        /// جایگزینی اعداد فارسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EnglishDigits(string input)
        {
            for (int i = 0; i < _PersianDigits.Length; i++)
                input = input.Replace(_PersianDigits[i], _LatinDigits[i]);
            return input;
        }



        #endregion
    }
}
