using System.Runtime.InteropServices;

namespace EushullyExtractionUtils;

public static class Operation
{

    public static void ReadToStructure<T>(Stream stream, out T structure, int size) where T : struct
    {
        structure = new T();
        if (structure is IFromBytes fromBytes)
        {
            var bytes = new byte[fromBytes.Size];
            stream.Read(bytes, 0, fromBytes.Size);
            fromBytes.GetFromBytes(bytes, 0);
            structure = (T)fromBytes;
        }
        else
        {
            var bytes = new byte[size];
            stream.Read(bytes, 0, size);
            structure = ByteArrayToStructure<T>(bytes);
        }

    }
    
    public static byte[] StructToBytes<T>(T str) where T : struct
    {
        int size = Marshal.SizeOf(str);
        byte[] arr = new byte[size];
        var ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(str, ptr, true);
        Marshal.Copy(ptr, arr, 0, size);
        Marshal.FreeHGlobal(ptr);
        return arr;
    }

    public static T ByteArrayToStructure<T>(byte[] bytes, int offset = 0) where T : struct
    {
        int size = Marshal.SizeOf(typeof(T));
        var ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(bytes, offset, ptr, size);
        var str = Marshal.PtrToStructure<T>(ptr);
        Marshal.FreeHGlobal(ptr);
        return str;
    }

    public static T[] ByteArrayToStructureArray<T>(byte[] bytes, int offset, int length) where T : struct
    {
        int traveled = 0;
        var list = new List<T>();
        var example = new T();
        if (example is IFromBytes fromBytes)
        {
            int size = fromBytes.Size;
            while (traveled < length)
            {
                var item = (IFromBytes)new T();
                item.GetFromBytes(bytes, offset + traveled);
                list.Add((T)item);
                traveled += size;
            }
        }
        else
        {
            int size = Marshal.SizeOf(typeof(T));
            while (traveled < length)
            {
                var ptr = Marshal.AllocHGlobal(size);
                Marshal.Copy(bytes, offset + traveled, ptr, size);
                var str = Marshal.PtrToStructure<T>(ptr);
                Marshal.FreeHGlobal(ptr);
                list.Add(str);
                traveled += size;
            }
        }
        return list.ToArray();
    }
}