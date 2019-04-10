using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//‌Added by Maryam Haghighi,95/6/20
namespace SamanConvertor
{

    // اینکد کردن اعداد فارسی
    public class TadbirEncoder
    {
        private static char[] _DigitsSet = new char[]
        {

           '¹',//0
           '±',//1
           '‚', //8218  //2
           'ƒ', //402    //3
           '„', //8222  //4
           '…', //8230  //5
           '†', //8224  //6
           '‡', //8225  //7
           'ˆ', //710    //8
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
             (char)0xB9,

        };

        public static string ReplaceDigits(string strSource)
        {
         
            return strSource.Replace('۰', _DigitsSet[0])  //۰
            .Replace('۱', _DigitsSet[1])  //۱
            .Replace('۲', _DigitsSet[2])//8218  //2
            .Replace('۳', _DigitsSet[3])//402       
            .Replace('۴', _DigitsSet[4])//8222  //4//3
            .Replace('۵', _DigitsSet[5])//8230  //5
            .Replace('۶', _DigitsSet[6])//8224  //6
            .Replace('۷', _DigitsSet[7])//8225  //7
            .Replace('۸', _DigitsSet[8])//710       //8
            .Replace('۹', _DigitsSet[9])//8240  //9

            .Replace((char)0x80, _DigitsSet[0])//0
            .Replace((char)0x81, _DigitsSet[1])//1
            .Replace((char)0x82, _DigitsSet[2])//2
            .Replace((char)0x83, _DigitsSet[3])//3
            .Replace((char)0x84, _DigitsSet[4])//4
            .Replace((char)0x85, _DigitsSet[5])//5
            .Replace((char)0x86, _DigitsSet[6])//6
            .Replace((char)0x87, _DigitsSet[7])//7
            .Replace((char)0x88, _DigitsSet[8])//8
            .Replace((char)0x89, _DigitsSet[9])//9
            .Replace((char)0xB1, _DigitsSet[1])//1
            .Replace((char)0xB9, _DigitsSet[0])//0

            .Replace('0', _DigitsSet[0])//0
            .Replace('1', _DigitsSet[1])//1
            .Replace('2', _DigitsSet[2])//2
            .Replace('3', _DigitsSet[3])//3
            .Replace('4', _DigitsSet[4])//4
            .Replace('5', _DigitsSet[5])//5
            .Replace('6', _DigitsSet[6])//6
            .Replace('7', _DigitsSet[7])//7
            .Replace('8', _DigitsSet[8])//8
            .Replace('9', _DigitsSet[9])//9


            ;

        }

    }
}
