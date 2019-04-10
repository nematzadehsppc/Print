using System;
using System.Collections.Generic;

namespace SamanConvertor
{
    class TadbirDirectionCodePage
    {
        //public static BidiCharacterType[] bidi_character_type;
        private static BidiCharacterType[] SamanCharType = 
        {
#region HARD_CODE_BIDI_ARRAY
            BidiCharacterType.ON,      //SamanCharType[0x00]
            BidiCharacterType.ON,      //SamanCharType[0x01]
            BidiCharacterType.ON,      //SamanCharType[0x02]
            BidiCharacterType.ON,      //SamanCharType[0x03]
            BidiCharacterType.ON,      //SamanCharType[0x04]
            BidiCharacterType.ON,      //SamanCharType[0x05]
            BidiCharacterType.ON,      //SamanCharType[0x06]
            BidiCharacterType.ON,      //SamanCharType[0x07]
            BidiCharacterType.ON,      //SamanCharType[0x08]
            BidiCharacterType.ON,      //SamanCharType[0x09]
            BidiCharacterType.ON,      //SamanCharType[0x0A]
            BidiCharacterType.ON,      //SamanCharType[0x0B]
            BidiCharacterType.ON,      //SamanCharType[0x0C]
            BidiCharacterType.ON,      //SamanCharType[0x0D]
            BidiCharacterType.ON,      //SamanCharType[0x0E]
            BidiCharacterType.ON,      //SamanCharType[0x0F]
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.ON,      //SamanCharType[0x10]
            BidiCharacterType.ON,      //SamanCharType[0x11]
            BidiCharacterType.ON,      //SamanCharType[0x12]
            BidiCharacterType.ON,      //SamanCharType[0x13]
            BidiCharacterType.ON,      //SamanCharType[0x14]
            BidiCharacterType.ON,      //SamanCharType[0x15]
            BidiCharacterType.ON,      //SamanCharType[0x16]
            BidiCharacterType.ON,      //SamanCharType[0x17]
            BidiCharacterType.ON,      //SamanCharType[0x18]
            BidiCharacterType.ON,      //SamanCharType[0x19]
            BidiCharacterType.ON,      //SamanCharType[0x1A]
            BidiCharacterType.ON,      //SamanCharType[0x1B]
            BidiCharacterType.ON,      //SamanCharType[0x1C]
            BidiCharacterType.ON,      //SamanCharType[0x1D]
            BidiCharacterType.ON,      //SamanCharType[0x1E]
            BidiCharacterType.ON,      //SamanCharType[0x1F]
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.WS, //   SamanCharType[0x20] =
            BidiCharacterType.ON, //   SamanCharType[0x21] =
            BidiCharacterType.ET, //   SamanCharType[0x22] =
            BidiCharacterType.ET, //   SamanCharType[0x23] =
            BidiCharacterType.ON, //   SamanCharType[0x24] =
            BidiCharacterType.ON, //   SamanCharType[0x25] =
            BidiCharacterType.ON, //   SamanCharType[0x26] =
            BidiCharacterType.ON, //   SamanCharType[0x27] =
            BidiCharacterType.ON, //   SamanCharType[0x28] =
            BidiCharacterType.ON, //   SamanCharType[0x29] =
            BidiCharacterType.ON, //   SamanCharType[0x2A] =
            BidiCharacterType.ES, //   SamanCharType[0x2B] =
            BidiCharacterType.CS, //   SamanCharType[0x2C] =
            BidiCharacterType.ES, //   SamanCharType[0x2D] =
            BidiCharacterType.CS, //   SamanCharType[0x2E] =
            BidiCharacterType.ON, //   SamanCharType[0x2F] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.EN, //   SamanCharType[0x30] =
            BidiCharacterType.EN, //   SamanCharType[0x31] =
            BidiCharacterType.EN, //   SamanCharType[0x32] =
            BidiCharacterType.EN, //   SamanCharType[0x33] =
            BidiCharacterType.EN, //   SamanCharType[0x34] =
            BidiCharacterType.EN, //   SamanCharType[0x35] =
            BidiCharacterType.EN, //   SamanCharType[0x36] =
            BidiCharacterType.EN, //   SamanCharType[0x37] =
            BidiCharacterType.EN, //   SamanCharType[0x38] =
            BidiCharacterType.EN, //   SamanCharType[0x39] =
            BidiCharacterType.CS, //   SamanCharType[0x3A] =
            BidiCharacterType.CS, //   SamanCharType[0x3B] =
            BidiCharacterType.ON, //   SamanCharType[0x3C] =
            BidiCharacterType.ON, //   SamanCharType[0x3D] =
            BidiCharacterType.ON, //   SamanCharType[0x3E] =
            BidiCharacterType.ON, //   SamanCharType[0x3F] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.ON, //   SamanCharType[0x40] =
            BidiCharacterType.L, //   SamanCharType[0x41] =
            BidiCharacterType.L, //   SamanCharType[0x42] =
            BidiCharacterType.L, //   SamanCharType[0x43] =
            BidiCharacterType.L, //   SamanCharType[0x44] =
            BidiCharacterType.L, //   SamanCharType[0x45] =
            BidiCharacterType.L, //   SamanCharType[0x46] =
            BidiCharacterType.L, //   SamanCharType[0x47] =
            BidiCharacterType.L, //   SamanCharType[0x48] =
            BidiCharacterType.L, //   SamanCharType[0x49] =
            BidiCharacterType.L, //   SamanCharType[0x4A] =
            BidiCharacterType.L, //   SamanCharType[0x4B] =
            BidiCharacterType.L, //   SamanCharType[0x4C] =
            BidiCharacterType.L, //   SamanCharType[0x4D] =
            BidiCharacterType.L, //   SamanCharType[0x4E] =
            BidiCharacterType.L, //   SamanCharType[0x4F] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.L, //  SamanCharType[0x50] =
            BidiCharacterType.L, //   SamanCharType[0x51] =
            BidiCharacterType.L, //   SamanCharType[0x52] =
            BidiCharacterType.L, //   SamanCharType[0x53] =
            BidiCharacterType.L, //   SamanCharType[0x54] =
            BidiCharacterType.L, //   SamanCharType[0x55] =
            BidiCharacterType.L, //   SamanCharType[0x56] =
            BidiCharacterType.L, //   SamanCharType[0x57] =
            BidiCharacterType.L, //   SamanCharType[0x58] =
            BidiCharacterType.L, //   SamanCharType[0x59] =
            BidiCharacterType.L, //   SamanCharType[0x5A] =
            BidiCharacterType.ON, //   SamanCharType[0x5B] =
            BidiCharacterType.ON, //   SamanCharType[0x5C] =
            BidiCharacterType.ON, //   SamanCharType[0x5D] =
            BidiCharacterType.ON, //   SamanCharType[0x5E] =
            BidiCharacterType.ON, //   SamanCharType[0x5F] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.ON,    //   SamanCharType[0x60] =
            BidiCharacterType.L,    //   SamanCharType[0x61] =
            BidiCharacterType.L,    //   SamanCharType[0x62] =
            BidiCharacterType.L,    //   SamanCharType[0x63] =
            BidiCharacterType.L,    //   SamanCharType[0x64] =
            BidiCharacterType.L,    //   SamanCharType[0x65] =
            BidiCharacterType.L,    //   SamanCharType[0x66] =
            BidiCharacterType.L,    //   SamanCharType[0x67] =
            BidiCharacterType.L,    //   SamanCharType[0x68] =
            BidiCharacterType.L,    //   SamanCharType[0x69] =
            BidiCharacterType.L,    //   SamanCharType[0x6A] =
            BidiCharacterType.L,   //   SamanCharType[0x6B] =
            BidiCharacterType.L,   //   SamanCharType[0x6C] =
            BidiCharacterType.L,   //   SamanCharType[0x6D] =
            BidiCharacterType.L,   //   SamanCharType[0x6E] =
            BidiCharacterType.L,   //   SamanCharType[0x6F] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.L,    //   SamanCharType[0x70] =
            BidiCharacterType.L,    //   SamanCharType[0x71] =
            BidiCharacterType.L,    //   SamanCharType[0x72] =
            BidiCharacterType.L,    //   SamanCharType[0x73] =
            BidiCharacterType.L,    //   SamanCharType[0x74] =
            BidiCharacterType.L,    //   SamanCharType[0x75] =
            BidiCharacterType.L,    //   SamanCharType[0x76] =
            BidiCharacterType.L,    //   SamanCharType[0x77] =
            BidiCharacterType.L,    //   SamanCharType[0x78] =
            BidiCharacterType.L,    //   SamanCharType[0x79] =
            BidiCharacterType.L,    //   SamanCharType[0x7A] =
            BidiCharacterType.ON,   //   SamanCharType[0x7B] =
            BidiCharacterType.ON,   //   SamanCharType[0x7C] =
            BidiCharacterType.ON,   //   SamanCharType[0x7D] =
            BidiCharacterType.ON,   //   SamanCharType[0x7E] =
            BidiCharacterType.ON,   //   SamanCharType[0x7F] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.AN,    //   SamanCharType[0x80] =
            BidiCharacterType.R,    //   SamanCharType[0x81] =
            BidiCharacterType.AN,    //   SamanCharType[0x82] =
            BidiCharacterType.AN,    //   SamanCharType[0x83] =
            BidiCharacterType.AN,    //   SamanCharType[0x84] =
            BidiCharacterType.AN,    //   SamanCharType[0x85] =
            BidiCharacterType.AN,    //   SamanCharType[0x86] =
            BidiCharacterType.AN,    //   SamanCharType[0x87] =
            BidiCharacterType.AN,    //   SamanCharType[0x88] =
            BidiCharacterType.AN,    //   SamanCharType[0x89] =
            BidiCharacterType.R,    //   SamanCharType[0x8A] =
            BidiCharacterType.R,   //   SamanCharType[0x8B] =
            BidiCharacterType.R,   //   SamanCharType[0x8C] =
            BidiCharacterType.R,   //   SamanCharType[0x8D] =
            BidiCharacterType.R,   //   SamanCharType[0x8E] =
            BidiCharacterType.R,   //   SamanCharType[0x8F] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.R,    //   SamanCharType[0x90] =
            BidiCharacterType.ON,    //   SamanCharType[0x91] =
            BidiCharacterType.ON,    //   SamanCharType[0x92] =
            BidiCharacterType.ON,    //   SamanCharType[0x93] =
            BidiCharacterType.ON,    //   SamanCharType[0x94] =
            BidiCharacterType.ON,    //   SamanCharType[0x95] =
            BidiCharacterType.ES,    //   SamanCharType[0x96] =
            BidiCharacterType.ES,    //   SamanCharType[0x97] =
            BidiCharacterType.R,    //   SamanCharType[0x98] =
            BidiCharacterType.ON,    //   SamanCharType[0x99] =
            BidiCharacterType.R,    //   SamanCharType[0x9A] =
            BidiCharacterType.ON,   //   SamanCharType[0x9B] =
            BidiCharacterType.L,   //   SamanCharType[0x9C] =
            BidiCharacterType.ON,   //   SamanCharType[0x9D] =
            BidiCharacterType.ON,   //   SamanCharType[0x9E] =
            BidiCharacterType.R,   //   SamanCharType[0x9F] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.CS,    //   SamanCharType[0xA0] =
            BidiCharacterType.CS,    //   SamanCharType[0xA1] =
            BidiCharacterType.ET,    //   SamanCharType[0xA2] =
            BidiCharacterType.ET,    //   SamanCharType[0xA3] =
            BidiCharacterType.ET,    //   SamanCharType[0xA4] =
            BidiCharacterType.ON,    //   SamanCharType[0xA5] =
            BidiCharacterType.ON,    //   SamanCharType[0xA6] =
            BidiCharacterType.ON,    //   SamanCharType[0xA7] =
            BidiCharacterType.ON,    //   SamanCharType[0xA8] =
            BidiCharacterType.ON,    //   SamanCharType[0xA9] =
            BidiCharacterType.R ,    //   SamanCharType[0xAA] =
            BidiCharacterType.ON,   //   SamanCharType[0xAB] =
            BidiCharacterType.ON,   //   SamanCharType[0xAC] =
            BidiCharacterType.ON,   //   SamanCharType[0xAD] =
            BidiCharacterType.ON,   //   SamanCharType[0xAE] =
            BidiCharacterType.ON,   //   SamanCharType[0xAF] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.ET,   //   SamanCharType[0xB0] =
            BidiCharacterType.AN,   //   SamanCharType[0xB1] =
            BidiCharacterType.ON,   //   SamanCharType[0xB2] =
            BidiCharacterType.ON,   //   SamanCharType[0xB3] =
            BidiCharacterType.ON,   //   SamanCharType[0xB4] =
            BidiCharacterType.ON,   //   SamanCharType[0xB5] =
            BidiCharacterType.ON,   //   SamanCharType[0xB6] =
            BidiCharacterType.ON,   //   SamanCharType[0xB7] =
            BidiCharacterType.ON,   //   SamanCharType[0xB8] =
            BidiCharacterType.AN,   //   SamanCharType[0xB9] =
            BidiCharacterType.ON,   //   SamanCharType[0xBA] =
            BidiCharacterType.ON,   //   SamanCharType[0xBB] =
            BidiCharacterType.ON,   //   SamanCharType[0xBC] =
            BidiCharacterType.ON,   //   SamanCharType[0xBD] =
            BidiCharacterType.ON,   //   SamanCharType[0xBE] =
            BidiCharacterType.R,   //   SamanCharType[0xBF] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.R,    //   SamanCharType[0xC0] =
            BidiCharacterType.R,    //   SamanCharType[0xC1] =
            BidiCharacterType.R,    //   SamanCharType[0xC2] =
            BidiCharacterType.R,    //   SamanCharType[0xC3] =
            BidiCharacterType.R,    //   SamanCharType[0xC4] =
            BidiCharacterType.R,    //   SamanCharType[0xC5] =
            BidiCharacterType.R,    //   SamanCharType[0xC6] =
            BidiCharacterType.R,    //   SamanCharType[0xC7] =
            BidiCharacterType.R,    //   SamanCharType[0xC8] =
            BidiCharacterType.R,    //   SamanCharType[0xC9] =
            BidiCharacterType.R,    //   SamanCharType[0xCA] =
            BidiCharacterType.R,   //   SamanCharType[0xCB] =
            BidiCharacterType.R,   //   SamanCharType[0xCC] =
            BidiCharacterType.R,   //   SamanCharType[0xCD] =
            BidiCharacterType.R,   //   SamanCharType[0xCE] =
            BidiCharacterType.R,   //   SamanCharType[0xCF] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.R,    //   SamanCharType[0xD0] =
            BidiCharacterType.R,    //   SamanCharType[0xD1] =
            BidiCharacterType.R,    //   SamanCharType[0xD2] =
            BidiCharacterType.R,    //   SamanCharType[0xD3] =
            BidiCharacterType.R,    //   SamanCharType[0xD4] =
            BidiCharacterType.R,    //   SamanCharType[0xD5] =
            BidiCharacterType.R,    //   SamanCharType[0xD6] =
            BidiCharacterType.ON,    //   SamanCharType[0xD7] =
            BidiCharacterType.R,   //   SamanCharType[0xD8] =
            BidiCharacterType.R,    //   SamanCharType[0xD9] =
            BidiCharacterType.R,    //   SamanCharType[0xDA] =
            BidiCharacterType.R,   //   SamanCharType[0xDB] =
            BidiCharacterType.R,   //   SamanCharType[0xDC] =
            BidiCharacterType.R,   //   SamanCharType[0xDD] =
            BidiCharacterType.R,   //   SamanCharType[0xDE] =
            BidiCharacterType.R,   //   SamanCharType[0xDF] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.L,   //   SamanCharType[0xE0] =
            BidiCharacterType.R,   //   SamanCharType[0xE1] =
            BidiCharacterType.L,   //   SamanCharType[0xE2] =
            BidiCharacterType.R,   //   SamanCharType[0xE3] =
            BidiCharacterType.R,   //   SamanCharType[0xE4] =
            BidiCharacterType.R,   //   SamanCharType[0xE5] =
            BidiCharacterType.R,   //   SamanCharType[0xE6] =
            BidiCharacterType.L,   //   SamanCharType[0xE7] =
            BidiCharacterType.L,   //   SamanCharType[0xE8] =
            BidiCharacterType.L,   //   SamanCharType[0xE9] =
            BidiCharacterType.L,   //   SamanCharType[0xEA] =
            BidiCharacterType.L,   //   SamanCharType[0xEB] =
            BidiCharacterType.R,   //   SamanCharType[0xEC] =
            BidiCharacterType.R,   //   SamanCharType[0xED] =
            BidiCharacterType.L,   //   SamanCharType[0xEE] =
            BidiCharacterType.L,   //   SamanCharType[0xEF] =
            //-------------------------------------------------------------------------------------------------
            BidiCharacterType.R,   //   SamanCharType[0xF0] =
            BidiCharacterType.R,   //   SamanCharType[0xF1] =
            BidiCharacterType.R,   //   SamanCharType[0xF2] =
            BidiCharacterType.R,   //   SamanCharType[0xF3] =
            BidiCharacterType.L,   //   SamanCharType[0xF4] =
            BidiCharacterType.R,   //   SamanCharType[0xF5] =
            BidiCharacterType.R,   //   SamanCharType[0xF6] =
            BidiCharacterType.ON,   //   SamanCharType[0xF7] =
            BidiCharacterType.R,   //   SamanCharType[0xF8] =
            BidiCharacterType.L,   //   SamanCharType[0xF9] =
            BidiCharacterType.R,   //   SamanCharType[0xFA] =
            BidiCharacterType.L,   //   SamanCharType[0xFB] =
            BidiCharacterType.L,   //   SamanCharType[0xFC] =
            BidiCharacterType.ON,   //   SamanCharType[0xFD] =
            BidiCharacterType.ON,   //   SamanCharType[0xFE] =
            BidiCharacterType.R  //   SamanCharType[0xFF] =
            //-------------------------------------------------------------------------------------------------
            #endregion HARD_CODE_BIDI_ARRAY
        };

        public static CharData[] UnicodeStringToSaman(string unicode_str)
        {
            ushort[] unicode_byte_array;
            BidiCharacterType[] bidi_character_type;
            Byte[] saman_byte_array;
            unicode_byte_array = new ushort[unicode_str.Length];
            saman_byte_array = new byte[unicode_str.Length];
            bidi_character_type = new BidiCharacterType[unicode_str.Length];
            byte temp;

            int _length = unicode_str.Length;
            CharData[] txt_data = new CharData[_length];
            int idx = 0;
            for (int i = 0; i < _length; ++i)
            {
                unicode_byte_array[i] = (ushort)unicode_str[i];
                temp = UnicodeCharToSamanChar(unicode_byte_array[i]);
                if (temp != 0)
                    saman_byte_array[i] = temp;
                else
                    saman_byte_array[i] =
                        System.Text.Encoding.GetEncoding(1256).GetBytes(unicode_str[i].ToString())[0];
                bidi_character_type[i] = SamanCharType[saman_byte_array[i]];
                ///
                char c = System.Text.Encoding.GetEncoding(1256).GetChars(saman_byte_array)[i];
                txt_data[i]._ct = bidi_character_type[i];
                txt_data[i]._char = c;
                txt_data[i]._idx = idx;
                idx++;
            }
            string saman_str = System.Text.Encoding.GetEncoding(1256).GetString(saman_byte_array);
            //   return saman_str;
            return txt_data;
        }
        public static CharData[] SamanStringToUnicode(string saman_str)
        {
            int _length = saman_str.Length;
            ushort[] unicode_byte_array;
            Byte[] saman_byte_array;
            BidiCharacterType[] bidi_character_type;
            string unicode_str = "";
            unicode_byte_array = new ushort[_length];
            saman_byte_array = new byte[_length];
            UInt16 temp;
            bidi_character_type = new BidiCharacterType[_length];

            CharData[] txt_data = new CharData[_length];
            int idx = 0;
            for (int i = 0; i < _length; ++i)
            {
                saman_byte_array[i] =
                      System.Text.Encoding.GetEncoding(1256).GetBytes(saman_str[i].ToString())[0];
                bidi_character_type[i] = SamanCharType[saman_byte_array[i]];
                temp = SamanCharToUnicodeChar(saman_byte_array[i]);
                if (temp != 0)
                    unicode_byte_array[i] = temp;
                else
                    unicode_byte_array[i] = (UInt16)(saman_str[i]);
                unicode_str += (char)(unicode_byte_array[i]);
                txt_data[i]._ct = bidi_character_type[i];
                txt_data[i]._char = (char)(unicode_byte_array[i]);
                txt_data[i]._idx = idx;
                idx++;
            }
            return txt_data;
        }
        private static ushort SamanCharToUnicodeChar(byte _b)
        {
            switch (_b)
            {
                //case 0x80:
                //    return 0x30;
                case 0xB9:
                    return 0x06F0;
                case 0xB1:
                    return 0x06F1;
                case 0x82:
                    return 0x06F2;
                case 0x83:
                    return 0x06F3;
                case 0x84:
                    return 0x06F4;
                case 0x85:
                    return 0x06F5;
                case 0x86:
                    return 0x06F6;
                case 0x87:
                    return 0x06F7;
                case 0x88:
                    return 0x06F8;
                case 0x89:
                    return 0x06F9;
                default:
                    return 0;
            }

        }
        private static byte UnicodeCharToSamanChar(ushort _b)
        {
            switch (_b)
            {
                //case 0x30:
                //return 0x80;
                case 0x06F0:
                    return 0xB9;
                case 0x06F1:
                    return 0xB1;
                case 0x06F2:
                    return 0x82;
                case 0x06F3:
                    return 0x83;
                case 0x06F4:
                    return 0x84;
                case 0x06F5:
                    return 0x85;
                case 0x06F6:
                    return 0x86;
                case 0x06F7:
                    return 0x87;
                case 0x06F8:
                    return 0x88;
                case 0x06F9:
                    return 0x89;
                default:
                    return 0;
            }
        }
    }
}
