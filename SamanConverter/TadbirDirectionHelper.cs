using System;
using System.Text;

namespace SamanConvertor
{
    public struct CharData
    {
        public char _char;
        public byte _el; // 0-62 => 6
        public BidiCharacterType _ct; // 0-18 => 5
        public int _idx;
    }
    public enum BidiCharacterType
    {
        #region Strong Types
        /// <summary>
        /// Left-to-Right
        /// </summary>
        /// <example>
        /// LRM, most alphabetic, syllabic, Han ideographs, non-European or non-Arabic digits, ...
        /// </example>
        /// <remarks>Strong Type</remarks>
        L,
        /// <summary>
        /// Right-to-Left
        /// </summary>
        /// <example>
        /// RLM, Hebrew alphabet, and related punctuation
        /// </example>
        /// <remarks>Strong Type</remarks>
        R,
        /// <summary>
        /// Right-to-Left Arabic
        /// </summary>
        /// <example>
        /// Arabic, Thaana, and Syriac alphabets, most punctuation specific to those scripts, ...
        /// </example>
        /// <remarks>Strong Type</remarks>
        AL,
        #endregion
        #region Weak Types
        /// <summary>
        /// European Number
        /// </summary>
        /// <example>
        /// European digits, Eastern Arabic-Indic digits, ...
        /// </example>
        /// <remarks>Weak Type</remarks>
        EN,
        /// <summary>
        /// European Number Separator
        /// </summary>
        /// <example>
        /// Plus sign, minus sign
        /// </example>
        /// <remarks>Weak Type</remarks>
        ES,
        /// <summary>
        /// European Number Terminator
        /// </summary>
        /// <example>
        /// Degree sign, currency symbols, ...
        /// </example>
        /// <remarks>Weak Type</remarks>
        ET,
        /// <summary>
        /// Arabic Number
        /// </summary>
        /// <example>
        /// Arabic-Indic digits, Arabic decimal and thousands separators, ...
        /// </example>
        /// <remarks>Weak Type</remarks>
        AN,
        /// <summary>
        /// Common Number Separator
        /// </summary>
        /// <example>
        /// Colon, comma, full stop (period), No-break space, ...
        /// </example>
        /// <remarks>Weak Type</remarks>
        CS,
        #endregion
        #region Neutral Types
        /// <summary>
        /// Whitespace
        /// </summary>
        /// <example>
        /// Space, figure space, line separator, form feed, General Punctuation spaces, ...
        /// </example>
        /// <remarks>Neutral Type</remarks>
        WS,
        /// <summary>
        /// Other Neutrals
        /// </summary>
        /// <example>
        /// All other characters, including OBJECT REPLACEMENT CHARACTER
        /// </example>
        /// <remarks>Neutral Type</remarks>
        ON,
        #endregion
    }


    class TadbirDirectionHelper
    {
        public static BidiCharacterType base_character_type = BidiCharacterType.R;
        private static UInt16[] mirror_array =
        {
        //0x93,       0x94,
        //0x2c,        0xA1,
        0x3e,        0x3c,
        0x7D,        0x7B,
        //0xBA,        0x3B,
        //0xBF,        0x3F,
        0x5D,        0x5B,
        0x29,        0x28,
        0xBB,        0xAB
    };
        public static string ReverseString(string strSource)
        {
            char[] chrSource = strSource.ToCharArray();
            Array.Reverse(chrSource);
            strSource = new string(chrSource);
            return strSource;
        }
        public static string ReverseSubString(int begin, int end, CharData[] _cd)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = end - 1; i >= begin; --i)
                builder.Append(_cd[i]._char);
            return builder.ToString();
        }

        //public static CharData[] StepOne_CalculateTextDataInfo(string source)
        //{
        //    CharData[] txt_data = new CharData[source.Length];
        //    int _length = source.Length;
        //    int idx = 0;
        //    char c;
        //    for (int i = 0; i < _length; ++i)
        //    {
        //        c = source[i];
        //        txt_data[i]._ct = TadbirDirectionCodePage.bidi_character_type[i];
        //        txt_data[i]._char = c;
        //        txt_data[i]._idx = idx;
        //        idx++;
        //    }
        //    return txt_data;
        //}

        //public static CharData[] CalculateEmbeddingLevel(CharData[] _cd)
        //{
        //    for (int i = 0; i < _cd.Length; i++)
        //    {
        //        if (_cd[i]._ct == BidiCharacterType.L)
        //            _cd[i]._el = 0;
        //        else if (_cd[i]._ct == BidiCharacterType.R)
        //            _cd[i]._el = 1;
        //        else if (_cd[i]._ct == BidiCharacterType.EN)
        //            _cd[i]._el = 0;
        //        else if (_cd[i]._ct == BidiCharacterType.AN)
        //            _cd[i]._el = 2;
        //        else if (_cd[i]._ct == BidiCharacterType.ET)
        //            _cd[i]._el = 3;
        //        else if (_cd[i]._ct == BidiCharacterType.CS)
        //            _cd[i]._el = 4;
        //        else if (_cd[i]._ct == BidiCharacterType.WS)
        //            _cd[i]._el = 5;
        //        else if (_cd[i]._ct == BidiCharacterType.ES)
        //            _cd[i]._el = 6;
        //        else
        //            _cd[i]._el = 100;
        //    }
        //    return _cd;
        //}

        private static byte CountWhiteSpaces(int index, CharData[] _cd)
        {
            byte counter = 0;
            while (_cd[index]._ct == BidiCharacterType.WS)
            {
                ++counter;
                ++index;
                if (index >= _cd.Length)
                    break;
            }
            return counter;
        }
        private static byte CountArabicChars(int index, CharData[] _cd)
        {
            byte counter = 0;
            while (_cd[index]._ct == BidiCharacterType.AN)
            {
                ++counter;
                ++index;
                if (index >= _cd.Length)
                    break;
            }
            return counter;
        }
        public static CharData[] StepTwo_CalculateTextDataInfo(CharData[] _td)
        {
            byte ws_count = 0;
            for (int i = 0; i < _td.Length; i++)
            {
                char cc = _td[i]._char;


                #region WS
                if (_td[i]._ct == BidiCharacterType.WS)
                {
                    ws_count = CountWhiteSpaces(i, _td);
                    if (ws_count == 1)
                    {
                        if (i > 0 && i < _td.Length - 1)
                        {

                            if (_td[i - 1]._ct == _td[i + 1]._ct && _td[i - 1]._ct == BidiCharacterType.R)
                            {
                                _td[i]._ct = _td[i - 1]._ct;

                            }
                            if (
                                (_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN || (_td[i - 1]._ct == BidiCharacterType.AN))
                                && (_td[i + 1]._ct == BidiCharacterType.AN || _td[i + 1]._ct == BidiCharacterType.EN || _td[i + 1]._ct == BidiCharacterType.L)
                                )
                            {
                                _td[i - 1]._ct = _td[i + 1]._ct = _td[i]._ct = BidiCharacterType.L;

                            }
                            //if (
                            //   (_td[i - 1]._ct == BidiCharacterType.R || _td[i - 1]._ct == BidiCharacterType.AN)
                            //   && (_td[i + 1]._ct == BidiCharacterType.AN || _td[i + 1]._ct == BidiCharacterType.R)
                            //   )
                            //{
                            //    _td[i - 1]._ct = _td[i + 1]._ct = _td[i]._ct = BidiCharacterType.R;

                            //}
                        }

                    }
                    else
                    {
                        int t = i + ws_count;
                        if (i > 0 && t < _td.Length)
                        {
                            if (((_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN || _td[i - 1]._ct == BidiCharacterType.AN)
                                && (_td[t]._ct == BidiCharacterType.L || _td[t]._ct == BidiCharacterType.EN || _td[t]._ct == BidiCharacterType.AN)))
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.L;
                                    ++i;
                                }
                            }
                            else if ((_td[i - 1]._ct == BidiCharacterType.R)// || _td[i - 1]._ct == BidiCharacterType.AN)
                                && (_td[t]._ct == BidiCharacterType.R))//|| _td[t]._ct == BidiCharacterType.AN)))
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.R;
                                    ++i;
                                }
                            }
                            else
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.WS;
                                    ++i;
                                }
                            }
                        }
                        else if (t == _td.Length)
                        {
                            if (_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN)
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.L;
                                    i++;
                                }
                            }
                            //else if (_td[i - 1]._ct == BidiCharacterType.R || _td[i - 1]._ct == BidiCharacterType.AN)
                            //{
                            //    while (i < t)
                            //    {
                            //        _td[i]._ct = BidiCharacterType.R;
                            //        i++;
                            //    }
                            //}
                            else
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.WS;
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            while (i < t)
                            {
                                _td[i]._ct = BidiCharacterType.WS;
                                ++i;
                            }
                        }
                        --i;
                    }
                }
                #endregion

                #region CS
                else if (_td[i]._ct == BidiCharacterType.CS)
                {
                    if (i > 0 && i < _td.Length - 1)
                    {
                        if (_td[i - 1]._ct == _td[i + 1]._ct)
                        //&& (_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.R))
                        {
                            _td[i]._ct = _td[i - 1]._ct;

                        }
                        if ((_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN || _td[i - 1]._ct == BidiCharacterType.AN) &&
                        (_td[i + 1]._ct == BidiCharacterType.L || _td[i + 1]._ct == BidiCharacterType.EN || _td[i + 1]._ct == BidiCharacterType.AN))
                        {
                            _td[i]._ct = _td[i - 1]._ct = _td[i + 1]._ct = BidiCharacterType.L;

                        }

                    }
                    if (i > 1 && i < _td.Length - 1)
                    {
                        if (_td[i - 1]._ct == BidiCharacterType.WS && _td[i + 1]._ct == BidiCharacterType.R &&
                      _td[i - 2]._ct == BidiCharacterType.R)
                        {
                            _td[i]._ct = _td[i - 1]._ct = BidiCharacterType.R;

                        }
                    }
                    if (i > 0 && i < _td.Length - 2)
                    {
                        if (_td[i - 1]._ct == BidiCharacterType.R && _td[i + 1]._ct == BidiCharacterType.WS &&
                    _td[i + 2]._ct == BidiCharacterType.R)
                        {
                            _td[i]._ct = _td[i + 1]._ct = BidiCharacterType.R;

                        }
                    }
                }
                #endregion
                #region ET
                else if (_td[i]._ct == BidiCharacterType.ET)
                {
                    _td[i]._ct = BidiCharacterType.ET;
                }
                #endregion
                #region ON


                else if (_td[i]._ct == BidiCharacterType.ON)
                {
                    //if (_td.Length>1 && i == 0 && !IsMirrorCharacter(_td[i]._char))
                    //{
                    //    int t = i+1;
                    //    while (_td[t]._ct != BidiCharacterType.EN && _td[t]._ct != BidiCharacterType.AN
                    //        && _td[t]._ct != BidiCharacterType.R && _td[t]._ct != BidiCharacterType.L)
                    //        ++t;
                    //    _td[i]._ct = _td[t]._ct;
                    //}
                    //if (_td.Length>1 && i == _td.Length-1 && !IsMirrorCharacter(_td[i]._char))
                    //{
                    //    int t = i - 1;
                    //    while (_td[t]._ct != BidiCharacterType.EN && _td[t]._ct != BidiCharacterType.AN
                    //        && _td[t]._ct != BidiCharacterType.R && _td[t]._ct != BidiCharacterType.L)
                    //        --t;
                    //    _td[i]._ct = _td[t]._ct;
                    //}

                    if (IsMirrorCharacter(_td[i]._char))
                        _td[i]._char = GetBidiMirroredChar(_td[i]._char);
                    if (i > 0 && i < _td.Length - 1)
                    {

                        if (_td[i - 1]._ct == _td[i + 1]._ct && (!IsMirrorCharacter(_td[i]._char)))
                        {
                            _td[i]._ct = _td[i - 1]._ct;
                        }


                        if (_td[i - 1]._ct == _td[i + 1]._ct &&
                            (_td[i - 1]._ct == BidiCharacterType.L ||
                            _td[i - 1]._ct == BidiCharacterType.AN || _td[i - 1]._ct == BidiCharacterType.EN)
                            && (!IsMirrorCharacter(_td[i]._char)))
                        {
                            _td[i]._ct = _td[i - 1]._ct = BidiCharacterType.L;
                        }
                        if ((_td[i - 1]._ct == BidiCharacterType.AN || _td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN)
                            && (_td[i + 1]._ct == BidiCharacterType.AN || _td[i + 1]._ct == BidiCharacterType.L || _td[i + 1]._ct == BidiCharacterType.EN)
                            && !IsMirrorCharacter(_td[i]._char))
                        {
                            _td[i]._ct = BidiCharacterType.L;

                        }
                    }
                }
                #endregion
                #region ES
                if (_td[i]._ct == BidiCharacterType.ES)
                {

                    if (i > 2 && i < _td.Length - 2)
                    {
                        if (_td[i - 1]._ct == _td[i + 1]._ct && _td[i + 2]._ct == BidiCharacterType.R && _td[i - 3]._ct == BidiCharacterType.R && _td[i - 2]._ct == BidiCharacterType.WS)
                        {
                            _td[i]._ct = _td[i - 1]._ct = _td[i - 2]._ct = _td[i + 1]._ct = BidiCharacterType.R;

                        }

                    }
                    if (i > 0 && i < _td.Length - 1)
                    {
                        if (_td[i - 1]._ct == _td[i + 1]._ct &&
                            (_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.R ||
                            _td[i - 1]._ct == BidiCharacterType.AN || _td[i - 1]._ct == BidiCharacterType.EN))
                        {
                            _td[i]._ct = BidiCharacterType.L;

                        }
                        if ((_td[i - 1]._ct == BidiCharacterType.EN || _td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.AN) &&
                            (_td[i + 1]._ct == BidiCharacterType.EN || _td[i + 1]._ct == BidiCharacterType.L || _td[i + 1]._ct == BidiCharacterType.AN))
                        {
                            _td[i]._ct = _td[i - 1]._ct = _td[i + 1]._ct = BidiCharacterType.L;

                        }
                    }
                    if (i > 1 && i < _td.Length - 2)
                    {
                        if (_td[i - 1]._ct == _td[i + 1]._ct && _td[i + 2]._ct == BidiCharacterType.R && _td[i - 2]._ct == BidiCharacterType.R)
                        {
                            _td[i]._ct = _td[i - 1]._ct = _td[i]._ct = _td[i + 1]._ct = BidiCharacterType.R;

                        }
                        else if (_td[i - 1]._ct == BidiCharacterType.WS && _td[i - 2]._ct == BidiCharacterType.R && _td[i + 1]._ct == BidiCharacterType.R)
                        {
                            _td[i]._ct = _td[i - 1]._ct = BidiCharacterType.R;

                        }
                        else if (_td[i - 1]._ct == BidiCharacterType.R && _td[i + 1]._ct == BidiCharacterType.WS && _td[i + 2]._ct == BidiCharacterType.R)
                        {
                            _td[i]._ct = _td[i + 1]._ct = BidiCharacterType.R;

                        }
                        else if (_td[i - 1]._ct == BidiCharacterType.WS &&
                            (_td[i + 1]._ct == BidiCharacterType.AN || _td[i + 1]._ct == BidiCharacterType.L || _td[i + 1]._ct == BidiCharacterType.EN)
                            && (_td[i - 2]._ct == BidiCharacterType.AN || _td[i - 2]._ct == BidiCharacterType.L || _td[i - 2]._ct == BidiCharacterType.EN))
                        {
                            _td[i]._ct = _td[i - 1]._ct = _td[i - 2]._ct = _td[i + 1]._ct = BidiCharacterType.L;

                        }
                    }
                    if (i > 1 && i < _td.Length - 3)
                    {
                        if ((_td[i - 1]._ct == BidiCharacterType.AN || _td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN) && (_td[i + 1]._ct == BidiCharacterType.WS)
                          && (_td[i + 2]._ct == BidiCharacterType.AN || _td[i + 2]._ct == BidiCharacterType.L || _td[i + 2]._ct == BidiCharacterType.EN))
                        {
                            _td[i]._ct = _td[i - 1]._ct = _td[i + 2]._ct = _td[i + 1]._ct = BidiCharacterType.L;

                        }
                        else if (_td[i - 1]._ct == _td[i + 1]._ct && _td[i - 2]._ct == BidiCharacterType.R && _td[i + 2]._ct == BidiCharacterType.WS
                            && _td[i + 3]._ct == BidiCharacterType.R)
                        {
                            _td[i]._ct = _td[i + 1]._ct = _td[i - 1]._ct = _td[i + 2]._ct = BidiCharacterType.R;

                        }
                    }
                }

                #endregion
            }
            return _td;
        }
        public static CharData[] StepThree_CalculateTextDataInfo(CharData[] _td)
        {

            int _length = _td.Length;
            for (int i = 0; i < _length; ++i)
            {
                byte an_count = 0;
                #region AN
                if (_td[i]._ct == BidiCharacterType.AN)
                {
                    an_count = CountArabicChars(i, _td);
                    if (an_count == 1)
                    {
                        if (i > 0 && i < _td.Length - 1)
                        {
                            if ((_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN) ||
                            (_td[i + 1]._ct == BidiCharacterType.L || _td[i + 1]._ct == BidiCharacterType.EN))
                            {
                                _td[i]._ct = BidiCharacterType.L;
                            }

                            if (_td[i - 1]._ct == _td[i + 1]._ct &&
                                (_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.R ||
                                _td[i - 1]._ct == BidiCharacterType.AN || _td[i - 1]._ct == BidiCharacterType.EN))
                            {
                                _td[i]._ct = _td[i - 1]._ct;

                            }
                            if ((_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN) &&
                               (_td[i + 1]._ct == BidiCharacterType.L || _td[i + 1]._ct == BidiCharacterType.EN))
                            {
                                _td[i - 1]._ct = _td[i + 1]._ct = _td[i]._ct = BidiCharacterType.L;

                            }
                            if ((_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN)
                                && (_td[i + 1]._ct == BidiCharacterType.AN)
                            ||
                                (_td[i - 1]._ct == BidiCharacterType.AN)
                                && (_td[i + 1]._ct == BidiCharacterType.EN || _td[i + 1]._ct == BidiCharacterType.L)
                                )
                            {
                                _td[i - 1]._ct = _td[i + 1]._ct = _td[i]._ct = BidiCharacterType.L;

                            }
                        }

                        else if (i > 1 && i < _td.Length - 2)
                        {
                            if ((_td[i - 1]._ct == _td[i + 1]._ct && _td[i - 1]._ct == BidiCharacterType.WS) &&
                           (_td[i - 2]._ct == _td[i + 2]._ct && _td[i - 2]._ct == BidiCharacterType.L))
                            {
                                _td[i - 1]._ct = _td[i + 1]._ct = _td[i]._ct = _td[i + 2]._ct;
                            }
                        }
                        else
                            _td[i]._ct = BidiCharacterType.L;
                    }
                    else
                    {
                        int t = i + an_count;


                        if (i > 0 && t < _td.Length)
                        {
                            if (((_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN)
                           && (_td[t]._ct == BidiCharacterType.L || _td[t]._ct == BidiCharacterType.EN)))
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.L;
                                    i++;
                                }
                            }
                            else if ((_td[i - 1]._ct == BidiCharacterType.R)//|| _td[i - 1]._ct == BidiCharacterType.AN)
                                && (_td[t]._ct == BidiCharacterType.R))// || _td[t]._ct == BidiCharacterType.AN)))
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.R;
                                    i++;
                                }
                            }
                            else if (((_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN)
                           || (_td[t]._ct == BidiCharacterType.L || _td[t]._ct == BidiCharacterType.EN)))
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.L;
                                    i++;
                                }
                            }
                            else
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.AN;
                                    i++;
                                }
                            }
                        }
                        else if (t == _td.Length && i != 0)
                        {
                            if (_td[i - 1]._ct == BidiCharacterType.L || _td[i - 1]._ct == BidiCharacterType.EN)
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.L;
                                    i++;
                                }
                            }
                            else
                            {
                                while (i < t)
                                {
                                    _td[i]._ct = BidiCharacterType.L;
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            while (i < t)
                            {
                                _td[i]._ct = BidiCharacterType.L;
                                i++;
                            }
                        }
                        --i;


                    }

                }
                #endregion
                //////////////////////////////////////////////////////////////
            }
            for (int i = 0; i < _length; ++i)
            {
                #region EMBEDDING_LEVEL
                if (_td[i]._ct == BidiCharacterType.L)
                    _td[i]._el = 0;
                else if (_td[i]._ct == BidiCharacterType.R)
                    _td[i]._el = 1;
                else if (_td[i]._ct == BidiCharacterType.EN)
                    _td[i]._el = 0;
                else if (_td[i]._ct == BidiCharacterType.AN)
                    _td[i]._el = 2;
                else if (_td[i]._ct == BidiCharacterType.ET)
                    _td[i]._el = 3;
                else if (_td[i]._ct == BidiCharacterType.CS)
                    _td[i]._el = 4;
                else if (_td[i]._ct == BidiCharacterType.WS)
                    _td[i]._el = 5;
                else if (_td[i]._ct == BidiCharacterType.ES)
                    _td[i]._el = 6;
                else
                    _td[i]._el = 100;
                #endregion EMBEDDING_LEVEL
                //////////////////////////////////////////////////////////////
            }
            return _td;
        }
        public static System.Collections.ArrayList ReOrdering(CharData[] _cd)
        {
            System.Collections.ArrayList array_list = new System.Collections.ArrayList();
            int begin = 0;
            int end = 0;
            byte current_el;
            int i = 0;
            while (i < _cd.Length)
            {
                current_el = _cd[i]._el;
                begin = i;
                while (current_el == _cd[i]._el && current_el != 100)
                {
                    ++i;
                    if (i >= _cd.Length)
                    {
                        break;
                    }
                }
                if (current_el == 100)
                    i++;
                end = i;
                array_list.Add(ReverseSubString(begin, end, _cd));
            }
            return array_list;
        }
        private static bool IsMirrorCharacter(char c)
        {
            return GetBidiMirroredChar(c)!=(ushort)c;
        }
        private static char GetBidiMirroredChar(char c)
        {
            byte temp;
            switch ((ushort)c)
            {
                case 0x3e: temp = 0x3c;  //> 
                    break;
                case 0x3c: temp = 0x3e;  //< 
                    break;
                case 0x7D: temp = 0x7B;  //} 
                    break;
                case 0x7B: temp = 0x7D;  //{ 
                    break;
                case 0x5D: temp = 0x5B;//]
                    break;
                case 0x5B: temp = 0x5D;//[
                    break;
                case 0x29: temp = 0x28;//)
                    break;
                case 0x28: temp = 0x29;//(
                    break;
                case 0xBB: temp = 0xAB;//>>
                    break;
                case 0xAB: temp = 0xBB;//<<
                    break;
                default: return c;
            }
            return (char)temp;
        }
    }
}
