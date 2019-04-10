using System;
using System.Collections.Generic;
using System.Text;


namespace SamanConvertor
{
    public static class Direction
    {
        public static string GetRTLString(string saman_string)
        {
            try
            {
                if (string.IsNullOrEmpty(saman_string))
                    return saman_string;
                List<string> text_saman_string = new List<string>();
                List<string> text_unicode_string = new List<string>();
                if (saman_string.IndexOf(Environment.NewLine) == -1)
                    text_saman_string.Add(saman_string);
                else
                    text_saman_string.AddRange(saman_string.Split(Environment.NewLine.ToCharArray()));
                string unicode_string = "";
                for (int x = 0; x < text_saman_string.Count; ++x)
                {
                    if (x > 0)
                        text_unicode_string.Add(Environment.NewLine);
                    CharData[] _cd;
                    System.Collections.ArrayList _al = new System.Collections.ArrayList();
                    string reversed_saman_string = TadbirDirectionHelper.ReverseString(text_saman_string[x]);
                    //unicode_string = TadbirDirectionCodePage.SamanStringToUnicode(reversed_saman_string);
                    //_cd = TadbirDirectionHelper.StepOne_CalculateTextDataInfo(unicode_string);
                    _cd = TadbirDirectionCodePage.SamanStringToUnicode(reversed_saman_string);
                    _cd = TadbirDirectionHelper.StepTwo_CalculateTextDataInfo(_cd);
                    _cd = TadbirDirectionHelper.StepThree_CalculateTextDataInfo(_cd);
                    _al = TadbirDirectionHelper.ReOrdering(_cd);
                    unicode_string = "";
                    for (int i = 0; i < _al.Count; ++i)
                    {
                        unicode_string += _al[i];
                    }
                    text_unicode_string.Add(unicode_string);
                }
                unicode_string = "";
                for (int i = 0; i < text_unicode_string.Count; ++i)
                {
                    unicode_string += text_unicode_string[i];
                }
                unicode_string = unicode_string.Replace('ي', 'ی');
                return unicode_string;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GetLTRString(string unicode_string)
        {

            try
            {
                //Modified By Saeedeh Taheri, در بعضی از گزارشات مقدار پارامتر از نوع استرینگ منفی یک ارسال می شود و باعث ایجاد خطا در اجرای گزارش می شود. مثلا گزارش تحلیلی سطوح حساب ها هشت ستونی گزینش بر اساس تاریخ
                //1395/12/22, تغییر با نظر آقای محمدی
                if (string.IsNullOrEmpty(unicode_string) || unicode_string == "'-1'" || unicode_string == "-1")
                    return unicode_string;
                List<string> text_saman_string = new List<string>();
                List<string> text_unicode_string = new List<string>();
                if (unicode_string.IndexOf(Environment.NewLine) == -1)
                    text_unicode_string.Add(unicode_string);
                else
                    text_unicode_string.AddRange(unicode_string.Split(Environment.NewLine.ToCharArray()));
                string saman_string = "";
                for (int x = 0; x < text_unicode_string.Count; ++x)
                {
                    if (x > 0)
                        text_unicode_string.Add(Environment.NewLine);
                    CharData[] _cd;
                    System.Collections.ArrayList _al = new System.Collections.ArrayList();
                    string reversed_unicode_string = TadbirDirectionHelper.ReverseString(text_unicode_string[x]);
                    //saman_string = TadbirDirectionCodePage.UnicodeStringToSaman(reversed_unicode_string);
                    _cd = TadbirDirectionCodePage.UnicodeStringToSaman(reversed_unicode_string);
                   // _cd = TadbirDirectionHelper.StepOne_CalculateTextDataInfo(saman_string);
                    _cd = TadbirDirectionHelper.StepTwo_CalculateTextDataInfo(_cd);
                    _cd = TadbirDirectionHelper.StepThree_CalculateTextDataInfo(_cd);
                    _al = TadbirDirectionHelper.ReOrdering(_cd);
                    saman_string = "";
                    for (int i = 0; i < _al.Count; ++i)
                    {
                        saman_string += _al[i];
                    }
                    text_saman_string.Add(saman_string);
                }
                saman_string = "";
                for (int i = 0; i < text_saman_string.Count; ++i)
                {
                    saman_string += text_saman_string[i];
                }
                return saman_string;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
