using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasePrint
{
    /// <summary>
    /// سرویس تبدیل کدپیج
    /// این سرویس در پیاده‌سازی پیش‌فرض برای تبدیل کدپیج عربی تدبیر به یونیکد استفاده می‌شود
    /// این تبدیل شامل جایگزینی بعضی حروف و همینطور بازآرایی رشته‌هاست
    /// </summary>
    public interface ICodepageConvertor
    {
        /// <summary>
        /// تبدیل از یونیکد به عربی تدبیر
        /// </summary>
        /// <param name="input">رشته یونیکد</param>
        /// <returns>رشته تدبیر</returns>
        string toTadbir(string input);

        /// <summary>
        /// تبدیل از عربی تدبیر به یونیکد
        /// </summary>
        /// <param name="input">رشته تدبیر</param>
        /// <returns>رشته یونیکد</returns>
        string fromTadbir(string input);

        /// <summary>
        /// تبدیل اعداد رشته به اعداد تدبیری
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string numsToTadbir(string input);

        /// <summary>
        /// تبدیل اعداد تدبیری به اعداد انگلیسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string numsFromTadbir(string input);

        /// <summary>
        /// تبدیل اعداد فارسی یونیکد به انگلیسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string latinizeNumbers(string input);

        /// <summary>
        /// تبدیل اعداد انگلیسی به فارسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string persianizeNumbers(string input);        

    }
}
