using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EliTool.ExtensionSDK.Common.Resource;
using EliTool.ExtensionSDK.File.InterfaceBase;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace EliTool.ExtensionSDK.Media;

public sealed class Image : ResourceBase, IFileResource, IPicture
{
    public Uri ImageUri => new(m_absolutePath);

    public Image(string Path, bool isRelative)
        : base()
    {
        if (isRelative)
        {
            m_relativePath = Path;
            m_absolutePath = ToAbsolute(Path);
            m_name = Path.Split("\\")[^1];
            m_resourceType = ResourceType.Binary;
        }
        else
        {
            m_relativePath = "";
            m_absolutePath = Path;
            m_name = Path.Split("\\")[^1];
            m_resourceType = ResourceType.Binary;
        }
    }

    public Stream AsRawStream()
    {
        return new MemoryStream(System.IO.File.ReadAllBytes(m_absolutePath));
    }

    public StreamReader AsReadStream()
    {
        return new(m_absolutePath);
    }

    public StreamWriter AsWriteStream()
    {
        return new(m_absolutePath);
    }

    public ImageSource AsWinUIImageSource()
    {
        return new BitmapImage(new(m_absolutePath));
    }
}
