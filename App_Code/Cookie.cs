using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace YYCMS
{
    public class Cookie
    {
        /// <summary>
        /// 保存一个Cookie
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="CookieValue">Cookie值</param>
        /// <param name="CookieTime">Cookie过期时间(小时),0为关闭页面失效</param>
        public static void SaveCookie(string CookieName, string CookieValue, double CookieTime)
        {
            CookieValue = HttpContext.Current.Server.UrlEncode(CookieValue);
            HttpCookie myCookie = new HttpCookie(CookieName);
            DateTime now = DateTime.Now;
            myCookie.Value =CookieValue;
            if (CookieTime != 0)
            {
                myCookie.Expires = now.AddDays(CookieTime);
                if (HttpContext.Current.Response.Cookies[CookieName] != null)
                    HttpContext.Current.Response.Cookies.Remove(CookieName);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            else
            {
                if (HttpContext.Current.Response.Cookies[CookieName] != null)
                    HttpContext.Current.Response.Cookies.Remove(CookieName);

                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }


        /// <summary>
        /// 取得CookieValue
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <returns>Cookie的值</returns>
        public static string GetCookie(string CookieName)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            myCookie = HttpContext.Current.Request.Cookies[CookieName];

            if (myCookie != null)
                return HttpContext.Current.Server.UrlDecode(myCookie.Value);
            else
                return null;
        }


        /// <summary>
        /// 清除CookieValue
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        public static void ClearCookie(string CookieName)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            DateTime now = DateTime.Now;

            myCookie.Expires = now.AddYears(-2);

            HttpContext.Current.Response.Cookies.Add(myCookie);
        }
        /// <summary>
        /// 清除所有CookieValue
        /// </summary>
        public static void ClearAllCookie()
        {
            HttpCookie myCookie;
            string cookieName;
            int limit = HttpContext.Current.Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = HttpContext.Current.Request.Cookies[i].Name;
                myCookie = new HttpCookie(cookieName);
                myCookie.Value = "";
                myCookie.Expires = DateTime.Now.AddYears(-2);
                HttpContext.Current.Request.Cookies.Add(myCookie);
            } 

        }

    }
}
