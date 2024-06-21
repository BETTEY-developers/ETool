using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExtensionSDK._Internal;

namespace EliTool.ExtensionSDK.Model.Document;
public class DocumentGroupCollection : CollectionAbsImpl<DocumentGroup>
{
    public DocumentGroupCollection(CollectionAbsImpl<DocumentGroup> coll) : base(coll) { }
    public DocumentGroupCollection() : base()
    {
    }
}
