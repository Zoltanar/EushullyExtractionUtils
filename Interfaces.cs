namespace EushullyExtractionUtils;

public interface IFromBytes
{
    /// <summary>
    /// Size of item in bytes.
    /// </summary>
    int Size { get; }
    void GetFromBytes(byte[] data, int offset);
}