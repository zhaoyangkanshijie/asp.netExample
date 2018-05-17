using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication1.Controllers.extends
{
    public static class staticUtil
    {
        /// <summary>
        /// 利用深度拷贝复制List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        public static IList<T> DeepClone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        /// <summary>
        ///     获取用户IP
        /// </summary>
        /// <returns></returns>
        public static string GetRealIp()
        {
            var result = String.Empty;
            //cdn ip
            if (HttpContext.Current.Request.Headers["Cdn-Src-Ip"] != null)
            {
                result = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (IsIpAddress(result))
                {
                    return result;
                }
            }
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理 
                if (result.IndexOf(".") == -1)
                {
                    //没有“.”肯定是非IPv4格式 
                    result = null;
                }
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理，取第一个不是内网的IP。 
                        result = result.Replace(" ", "").Replace("'", "");
                        var temparyip = result.Split(",;".ToCharArray());
                        for (var i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIpAddress(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                && temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 4) != "172.")
                            {
                                return temparyip[i]; //找到不是内网的地址 
                            }
                        }
                    }
                    else if (IsIpAddress(result)) //代理即是IP格式 
                        return result;
                    else
                        result = null; //代理中的内容 非IP，取IP 
                }
            }

            if (result == null || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (result == "::1")
                    result = "127.0.0.1";
            }
            return result;
        }

        /// <summary>
        ///     判断是否是ip格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static bool IsIpAddress(string ip)
        {
            IPAddress tip;
            return IPAddress.TryParse(ip, out tip);
        }
    }
}