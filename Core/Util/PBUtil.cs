using System;
using System.IO;
using ProtoBuf;

namespace Core.Util
{
    public static class PBUtil
    {
        public static byte[] Serialize<T>(T obj)
        {
            try
            {
                using MemoryStream stream = new MemoryStream();
                Serializer.Serialize(stream,obj);
                return stream.ToArray();
            }
            catch (System.Exception e)
            {
                WeCraftCore.Logger.Error(e);
                return new byte[]{};
            }
        }

        public static T? Deserialize<T>(byte[] bytesData)
        {
            try
            {
                using MemoryStream stream = new MemoryStream();
                stream.Write(bytesData, 0, bytesData.Length);
                stream.Position = 0L;
                T result = Serializer.Deserialize<T>(stream);
                stream.Dispose();
                return result;
            }
            catch (System.Exception e)
            {
                WeCraftCore.Logger.Error(e);
            }

            return default(T);
        }
    }
}