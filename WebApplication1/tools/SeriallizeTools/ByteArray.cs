using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MyCode
{
    public class ByteArray
    {
        private List<Byte> bytes=new List<Byte>();
        public ByteArray()
        {
        }
        public ByteArray(byte[] buffer)
        {
            for(int i=0;i<buffer.Length;i++)
            {
                bytes.Add(buffer[i]);
            }
        }
        public int Length
        {
            get { return bytes.Count; }
        }
        public int Postion
        {
            get;
            set;
        }
        public Boolean ReadBoolean()
        {
            byte b = bytes[Postion];
            Postion += 1;
            return b == (byte)0 ? false : true;
        }
        public byte ReadByte()
        {
            byte result = bytes[Postion];
            Postion += 1;
            return result;
        }
        public void WriteInt(int value)//32位的int型
        {
            byte[] bs = new byte[8];
            int temp = (value >> 24) & 0xff;
            bs[0] = (byte)(temp / 127);
            bs[1] = (byte)(temp % 127);

            temp = (value >> 16) & 0xff;
            bs[2] = (byte)(temp / 127);
            bs[3] = (byte)(temp % 127);

            temp = (value >> 8) & 0xff;
            bs[4] = (byte)(temp / 127);
            bs[5] = (byte)(temp % 127);

            temp = (value >> 0) & 0xff;
            bs[6] = (byte)(temp / 127);
            bs[7] = (byte)(temp % 127);

            bytes.AddRange(bs);
        }

        public int ReadInt()
        {
            byte[] bs = new byte[8];
            for(int i=0;i<8;i++)
            {
                bs[i] = bytes[i + Postion];
            }
            Postion += 8;
            int g  = (int)bs[7] + (int)bs[6] * 127;
            int g1 = (int)bs[5] + (int)bs[4] * 127;
            int g2 = (int)bs[3] + (int)bs[2] * 127;
            int g3 = (int)bs[1] + (int)bs[0] * 127;
            int result = g | ((int)g1 << 8) | ((int)g2 << 16) | ((int)g3 << 24);
            return result;
        }
        public String ReadUTFBytes()
        {
            int length = ReadInt();
            if(length<=0)
            {
                return "";
            }
            byte[] b = new byte[length];
            for(int i=0;i<length;i++)
            {
                b[i] = bytes[i + Postion];
            }
            Postion += (int)length;
            String decodeString = Encoding.UTF8.GetString(b);
            return decodeString;
        }
        public void WriteUTFBytes(string value)
        {
            byte[] bs = Encoding.UTF8.GetBytes(value);
            WriteInt(bs.Length);
            bytes.AddRange(bs);
        }
        public void WriteBoolean(bool value)
        {
            bytes.Add(value? ((byte)1):((byte)0));
        }
        public void WriteByte(byte value)
        {
            bytes.Add(value);
        }
        public void WriteBytes(byte[] value,int offset,int length)
        {
            for(int i=0;i<length;i++)
            {
                bytes.Add(value[i + offset]);
            }
        }
        public void WriteBytes(byte[] value)
        {
            bytes.AddRange(value);
        }
        public bool readnable()
        {
            return Length > Postion;
        }
        public byte[] ReadBytes()
        {
            byte[] bs = new byte[Length - Postion];
            for(int i=0;i<Length-Postion;i++)
            {
                bs[i] = bytes[i + Postion];
            }
            Postion = Length;
            return bs;
        }
        public byte[] ReadBytes(int length)
        {
            byte[] bs = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bs[i] = bytes[i + Postion];
            }
            Postion += length;
            return bs;
        }
        private float FLOAT_C = 10000.0f;
        private double DOUBLE_C = 100000000.0;
        public double ReadDouble()
        {
            long value = ReadLong();
            return value / DOUBLE_C;
        }
        public float ReadFloat()
        {
            int value = ReadInt();
            return value / FLOAT_C;
        }

        public long ReadLong()
        {
            byte[] bs = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                bs[i] = bytes[i + Postion];
            }
            Postion += 16;
            long g = (long)bs[15] + (long)bs[14] * 127;
            long g1 = (long)bs[13] + (long)bs[12] * 127;
            long g2 = (long)bs[11] + (long)bs[10] * 127;
            long g3 = (long)bs[9] + (long)bs[8] * 127;
            long g4 = (long)bs[7] + (long)bs[6] * 127;
            long g5 = (long)bs[5] + (long)bs[4] * 127;
            long g6 = (long)bs[3] + (long)bs[2] * 127;
            long g7 = (long)bs[1] + (long)bs[0] * 127;
            long result = g | ((long)g1 << 8) | ((long)g2 << 16) | ((long)g3 << 24) | ((long)g4 << 32) | ((long)g5 << 40) | ((long)g6 << 48) | ((long)g7 << 56);
            return result;
        }
        public void WriteDouble(double value)
        {
            long v = (long)(value * DOUBLE_C);
            WriteLong(v);
        }

        public void WriteFloat(float value)
        {
            int v = (int)(value * FLOAT_C);
            WriteInt(v);
        }

        public void WriteLong(long value)
     {
         byte[] bs = new byte[16];
         int temp=(int)(value >> 56)&0xff;
         bs[0] = (byte)(temp/127);
         bs[1] = (byte)(temp%127);
         
         temp=(int)(value >> 48)&0xff;
         bs[2] = (byte)(temp/127);
         bs[3] = (byte)(temp%127);
         
         temp=(int)(value >> 40)&0xff;
         bs[4] = (byte)(temp/127);
         bs[5] = (byte)(temp%127);
         
         temp=(int)(value >> 32)&0xff;
         bs[6] = (byte)(temp/127);
         bs[7] = (byte)(temp%127);
         
         temp=(int)(value >> 24)&0xff;
         bs[8] = (byte)(temp/127);
         bs[9] = (byte)(temp%127);
         
         temp=(int)(value >> 16)&0xff;
         bs[10] = (byte)(temp/127);
         bs[11] = (byte)(temp%127);
         
         temp=(int)(value >> 8)&0xff;
         bs[12] = (byte)(temp/127);
         bs[13] = (byte)(temp%127);

         temp=(int)(value&0xff);
         bs[14] = (byte)(temp/127);
         bs[15] = (byte)(temp%127);
         bytes.AddRange(bs);
     }

        public byte[] getBuffer(){
    	 byte[] r=new byte[bytes.Count];
    	 int i=0;
    	 foreach (var b in bytes) {
			r[i]=b;
			i++;
		}
    	 return r;
     }
    }
}