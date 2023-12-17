using System.IO;

namespace EliTool.ExternSDK.File.InterfaceBase;

public interface IFileResource
{
    public Stream AsRawStream();

    public StreamReader AsReadStream();

    public StreamWriter AsWriteStream();
}
