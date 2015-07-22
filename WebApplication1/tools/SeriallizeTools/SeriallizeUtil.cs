using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.src_dao.SeriallizeTools
{
    public class SeriallizeUtil
    {
        /// <summary>
        /// 将对象序列化成二进制数组
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
       public static byte[] SeriallizeBinary(Object o)
        {
            return MyCode.Code.MyEncode(o);
        }
        /// <summary>
        /// 将字节数组反序列化为对象
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static object DeseriallizeBinary(byte[] buf)
       {
           return MyCode.Code.MyEncode(buf);
       }

        public static T DeseriallizeBinary<T>(byte[] buf)
        {
            return MyCode.Code.MyDecode<T>(buf);
        }
    }
}