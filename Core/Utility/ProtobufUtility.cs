using System.IO;
using ProtoBuf;

namespace WeCraft.Core.Utility
{
    public static class ProtobufUtility
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
                WeCraftCore.Instance.LoggerImpl.Error(e);
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
                WeCraftCore.Instance.LoggerImpl.Error(e);
            }

            return default(T);
        }
    }
}