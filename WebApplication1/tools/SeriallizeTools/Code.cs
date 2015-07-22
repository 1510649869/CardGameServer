using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MyCode
{
    /// <summary>
    /// Encode:序列化
    /// Decode:反序列化
    /// </summary>
    public class Code
    {
        public static Assembly ass;
        //反序列化
        public static Object MyDecode(byte[] data)
        {
            ass = Assembly.GetCallingAssembly();
            try
            {
                Object t = decode(data);
                return t;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

         /*
         * 反序列化为指定的对象
         */
        public static T MyDecode<T>(byte[] data)
        {
            ass = Assembly.GetCallingAssembly();
            return (T)decode(data);
        }

        //反序列化类型分析
        private static Object decode(byte[] data)
        {
            ByteArray arr = new ByteArray(data);
            int type = arr.ReadInt();
            switch (type)
            {
                case TypeCode.ARRAY:
                    return arrayDecode(arr.ReadBytes(arr.ReadInt()));
                case TypeCode.BYTE:
                    return arr.ReadByte();
                case TypeCode.DATE://时间
                    return dateDecode(arr.ReadLong());
                case TypeCode.DOUBLE://double
                    return arr.ReadDouble();
                case TypeCode.FLOAT://float
                    return arr.ReadFloat();
                case TypeCode.INT://int
                    return arr.ReadInt();
                case TypeCode.LONG://long
                    return arr.ReadLong();
                case TypeCode.OBJECT://对象
                    return objDecode(arr.ReadBytes(arr.ReadInt()));
                case TypeCode.STRING://字符串
                    return arr.ReadUTFBytes();
                case TypeCode.NULL:
                    return null;
                case TypeCode.BOOLEAN:
                    return arr.ReadBoolean();
                default:
                    throw new Exception("decode value type is undefine");
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data">需要序列化的对象</param>
        /// 目前支持类型
        /// int double float long byte string boolean object array null
        /// 注：float精确到小数点后4位 double精确到小数点后8位
        /// <returns></returns>
        public static byte[] MyEncode(Object data)
        {
            ByteArray arr = new ByteArray();
            writeType(data, arr);
            encode(data, arr);
            return arr.getBuffer();
        }

        private static void writeType(Object obj, ByteArray arr)
        {
            //向byte数组中写入数据类型
            if (obj == null)
            {
                arr.WriteInt(TypeCode.NULL);
            }
            else if (obj is bool)
            {
                arr.WriteInt(TypeCode.BOOLEAN);
            }
            else if (obj is int)
            {
                arr.WriteInt(TypeCode.INT);
            }
            else if (obj is long)
            {
                arr.WriteInt(TypeCode.LONG);
            }
            else if (obj is Double)
            {
                arr.WriteInt(TypeCode.DOUBLE);
            }
            else if (obj is float)
            {
                arr.WriteInt(TypeCode.FLOAT);
            }
            else if (obj is Byte)
            {
                arr.WriteInt(TypeCode.BYTE);
            }
            else if (obj is Array)
            {
                arr.WriteInt(TypeCode.ARRAY);
            }
            else if (obj is String)
            {
                arr.WriteInt(TypeCode.STRING);
            }
            else if (obj is DateTime)
            {
                arr.WriteInt(TypeCode.DATE);
            }
            else
            {
                arr.WriteInt(TypeCode.OBJECT);
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="arr"></param>
        private static void encode(Object data, ByteArray arr)
        {
            if (data == null) return;
            if (data is bool)
            {
                arr.WriteBoolean((bool)data);
            }
            else if(data is int)
            {
                arr.WriteInt((int)data);
            }
            else if(data is long)
            {
                arr.WriteLong((long)data);
            }
            else if(data is double)
            {
                arr.WriteDouble((double)data);
            }
            else if(data is float)
            {
                arr.WriteFloat((float)data);
            }
            else if(data is byte)
            {
                arr.WriteByte((byte)data);
            }
            else if(data is Array)
            {
                arrayEncode((Array)data, arr);
            }
            else if(data is string)
            {
                arr.WriteUTFBytes((string)data);
            }
            else if(data is DateTime)
            {
                dateEncode((DateTime)data, arr);
            }else
            {
                objEncode(data, arr);
            }
        }

        /// <summary>
        /// 对象序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="arr"></param>
        private static void objEncode(Object obj, ByteArray arr)
        {
            ByteArray temp = new ByteArray();
            Type t = obj.GetType();
            temp.WriteUTFBytes(t.FullName);//先写入类名
            FieldInfo[] fields = t.GetFields();
            foreach(FieldInfo info in fields)
            {
                string fieldName = info.Name;//每个字段名
                temp.WriteUTFBytes(fieldName);
                writeType(info.GetValue(obj), temp);//先向字节数组中写入数据类型
                encode(info.GetValue(obj), temp);
            }
            arr.WriteInt(temp.Length);
            arr.WriteBytes(temp.getBuffer());
        }
        /// <summary>
        /// 对象反序列化//
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static Object objDecode(byte[] data)
        {
            ByteArray arr = new ByteArray(data);
            string className = arr.ReadUTFBytes();
            Type t = Type.GetType(className);
            Object obj = Activator.CreateInstance(t);
            //Object obj = ass.CreateInstance();
            while(arr.readnable())
            {
                String fieldName = arr.ReadUTFBytes();//字段名
                int fieldType = arr.ReadInt();//字段类型
                FieldInfo fields = t.GetField(fieldName);//读取字段
                if (fields == null)
                    throw new Exception("decode object is not field");
                switch(fieldType)
                {
                    case TypeCode.ARRAY:
                        int arrayLength = arr.ReadInt();
                        Object objs = arrayDecode(arr.ReadBytes(arrayLength));
                        fields.SetValue(obj, objs);
                        break;
                    case TypeCode.BYTE:
                        fields.SetValue(obj, arr.ReadByte());
                        break;
                    case TypeCode.DATE:
                        fields.SetValue(obj,dateDecode(arr.ReadLong()));
                        break;
                    case TypeCode.DOUBLE:
                        fields.SetValue(obj, arr.ReadDouble());
                        break;
                    case TypeCode.FLOAT:
                        fields.SetValue(obj, arr.ReadFloat());
                        break;
                    case TypeCode.INT:
                        fields.SetValue(obj, arr.ReadInt());
                        break;
                    case TypeCode.LONG:
                        fields.SetValue(obj, arr.ReadLong());
                        break;
                    case TypeCode.OBJECT:
                        int objLength = arr.ReadInt();
                        fields.SetValue(obj, objDecode(arr.ReadBytes(objLength)));
                        break;
                    case TypeCode.STRING:
                        fields.SetValue(obj, arr.ReadUTFBytes());
                        break;
                    case TypeCode.NULL:
                        fields.SetValue(obj, null);
                        break;
                    case TypeCode.BOOLEAN:
                        fields.SetValue(obj, arr.ReadBoolean());
                        break;
                    default:
                        throw new Exception("decode object type is undefine");
                }
            }
            return obj;

        }
        /// <summary>
        ///序列化时间 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="arr"></param>
        private static void dateEncode(DateTime dateTime, ByteArray arr)
        {
            long date = dateTime.Year*10+dateTime.Month;
            arr.WriteLong(date);
        }
        /// <summary>
        /// 解析时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="arr"></param>
        private static Object dateDecode(long date)
        {
            DateTime d = new DateTime(date);
            return d;
        }

        /// <summary>
        /// 数组序列化
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arr"></param>
        private static void arrayEncode(Array array, ByteArray arr)
        {
            ByteArray temp = new ByteArray();
            int i = 0;
            foreach (Object obj in array)
            {
                if (i == 0)
                {
                    writeType(obj, temp);
                    i++;
                }
                encode(obj, temp);
            }
            arr.WriteInt(temp.Length);
            arr.WriteBytes(temp.getBuffer());
        }

        /// <summary>
        /// 解析数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static Object arrayDecode(byte[] data)
        {
            ByteArray arr = new ByteArray(data);
            if(!arr.readnable())
            {
                return null;
            }
            int listType = arr.ReadInt();
            List<Object> list = new List<object>();
            switch(listType)
            {
                case TypeCode.ARRAY:
                    while(arr.readnable())
                    {
                        int arrayLength = arr.ReadInt();
                        list.Add(arrayDecode(arr.ReadBytes(arrayLength)));
                    }
                    break;
                case TypeCode.BYTE:
                    while(arr.readnable())
                    {
                        list.Add(arr.ReadByte());
                    }
                    break;
                case TypeCode.DATE:
                    while(arr.readnable())
                    {
                        list.Add(dateDecode(arr.ReadLong()));
                    }
                    break;
                case TypeCode.DOUBLE:
                    while(arr.readnable())
                    {
                        list.Add(arr.ReadDouble());
                    }
                    break;
                case TypeCode.FLOAT:
                    while(arr.readnable())
                    {
                        list.Add(arr.ReadFloat());
                    }
                    break;
                case TypeCode.INT:
                    while(arr.readnable())
                    {
                        list.Add(arr.ReadInt());
                    }
                    break;
                case TypeCode.LONG://long
                    while (arr.readnable())
                    {
                        list.Add(arr.ReadLong());
                    }
                    break;

                case TypeCode.OBJECT://对象
                    while (arr.readnable())
                    {
                        int objLength = arr.ReadInt();
                        list.Add(objDecode(arr.ReadBytes(objLength)));
                    }
                    break;
                case TypeCode.STRING://字符串
                    while (arr.readnable())
                    {
                        list.Add(arr.ReadUTFBytes());
                    }
                    break;
                case TypeCode.BOOLEAN:
                    while (arr.readnable())
                    {
                        list.Add(arr.ReadBoolean());
                    }
                    break;
                default:
                    throw new Exception("decode array type is undefine");
            }
            Type t = list[0].GetType();
            Object ar1=Array.CreateInstance(t,list.Count);
            Array.Copy(list.ToArray(),0,(Array)ar1,0,list.Count);
            return ar1;
        }
       
    }
}