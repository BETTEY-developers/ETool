using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EliTool.Views.ControlPage.DeveloperTools;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Windows.UI.Popups;
using System.ComponentModel;
using Windows.ApplicationModel.DataTransfer;
using Newtonsoft.Json.Linq;
using Windows.Perception.People;
using System.Text;
using System.Collections.Specialized;
using Windows.ApplicationModel.Appointments.DataProvider;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace EliTool.ViewModels;

public partial class JsonCSharpConverterViewModel : ObservableRecipient, INotifyPropertyChanged
{

    private char[] CSBelowNameChars = new char[]
    {
        '#','!','@','$','%','^','&','*','(',')','+','=',
        '{','[',']','}',':',';','"','\'',',','<','.','>',
        '`','~','！','·','。','，','‘','’','“','”','：','；',
        '？','（','）'
    };

    private char[] CSBelowNameStartWith = new char[]
    {
        '0','1','2','3','4','5','6','7','8','9','-'
    };

    private Dictionary<int, string> CSProtect = new Dictionary<int, string>
    {
        [0] = "public",
        [1] = "private",
        [2] = "protected",
        [3] = "internal",
        [4] = "protected internal",
        [5] = "private protected"
    };

    private string _JsonString = "";
    private string _CSharpString = "";

    private string _inputstring;
    public string InputString
    {
        get
        {
            return _inputstring;
        }
        set
        {
            _inputstring = value;
            OnPropertyChanged(nameof(InputString));
        }
    }

    private bool _needdoc;
    public bool NeedDoc
    {
        get
        {
            return _needdoc;
        }
        set
        {
            _needdoc = value;
            OnPropertyChanged(nameof(NeedDoc));
        }
    }

    private bool _needvaluedoc;
    public bool NeedValueDoc
    {
        get
        {
            return _needvaluedoc; 
        }
        set
        {
            _needvaluedoc = value;
            OnPropertyChanged(nameof(NeedValueDoc));
        }
    }

    private bool _needkeydoc;
    public bool NeedKeyDoc
    {
        get
        {
            return _needkeydoc;
        }
        set
        {
            _needkeydoc = value;
            OnPropertyChanged(nameof(NeedKeyDoc));
        }
    }

    private bool _needrawstring;
    public bool NeedRawString
    {
        get
        {
            return _needrawstring; 
        }
        set
        {
            _needrawstring = value;
            OnPropertyChanged(nameof(NeedRawString));
        }
    }

    private bool _needclassexname;
    public bool NeedClassExname
    {
        get
        {
            return _needclassexname;
        }
        set
        {
            _needclassexname = value;
            OnPropertyChanged(nameof(NeedClassExname));
        }
    }

    private string _classexname;
    public string ClassExname
    {
        get
        {
            return _classexname;
        }
        set
        {
            _classexname = value;
            OnPropertyChanged(nameof(ClassExname));
        }
    }

    private bool _needautorename;
    public bool NeedAutoRename
    {
        get
        {
            return _needautorename;
        }
        set
        {
            _needautorename = value;
            OnPropertyChanged(nameof(NeedAutoRename));
        }
    }

    private bool _needautofrontname;
    public bool NeedAutoFrontName
    {
        get
        {
            return _needautofrontname;
        }
        set
        {
            _needautofrontname = value;
            OnPropertyChanged(nameof(NeedAutoFrontName));
        }
    }

    private string _autofrontname;
    public string AutoFrontName
    {
        get
        {
            return _autofrontname;
        }
        set
        {
            string temp = value;
            CSBelowNameChars.ToList().ForEach(x => temp.Replace(x.ToString(), ""));
            _autofrontname = temp;
            OnPropertyChanged(nameof(AutoFrontName));
        }
    }

    private bool _needautoreplace;
    public bool NeedAutoReplace
    {
        get
        {
            return _needautoreplace;
        }
        set
        {
            _needautoreplace = value;
            OnPropertyChanged(nameof(NeedAutoReplace));
        }
    }

    private string _autoreplace;
    public string AutoReplace
    {
        get
        {
            return _autoreplace;
        }
        set
        {
            _autoreplace = FilterBelowChars(value);
            OnPropertyChanged(nameof(AutoReplace));
        }
    }

    private string _namespace;
    public string Namespace
    {
        get
        {
            return _namespace;
        }
        set
        {
            _namespace = FilterBelowChars(value);
            OnPropertyChanged(nameof(Namespace));
        }
    }

    private int _protect = 0;
    public int Protect
    {
        get
        {
            return _protect;
        }
        set
        {
            _protect = value;
            OnPropertyChanged(nameof(Protect));
        }
    }

    private bool _isjson = true;

    public bool IsJson => _isjson;
    public string FilterBelowChars(string orgstring)
    {
        string temp = orgstring;
        CSBelowNameChars.ToList().ForEach(x => temp.Replace(x.ToString(), ""));
        return temp;
    }
    public JsonCSharpConverterPage Page { get; set; }

    public JsonCSharpConverterViewModel()
    {
    }

    [RelayCommand]
    public async void SummonTest()
    {
       if(InputString != "")
       {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Page.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "输入框已有内容";
            dialog.PrimaryButtonText = "生成";
            dialog.SecondaryButtonText = "不生成";
            dialog.CloseButtonText = "取消";
            dialog.DefaultButton = ContentDialogButton.Secondary;
            dialog.Content = "输入框已有内容，生成示例后将会覆盖\n是否生成？";

            var result = await dialog.ShowAsync();
            if(result == ContentDialogResult.Secondary)
            {
                return;
            }
        }
        InputString =
@"{
  ""String_a"": ""Hello World!"",
  ""Int_b"": 114514,
  ""Long_c"": 11451419198,
  ""Boolean_d"": true,
  ""Type_obj_e"": {
    ""Obj_a"": 1,
    ""Obj_b"": 2
  },
  ""List_obj"": [
    {
        ""a"":1,
        ""b"":2
    },
    {
        ""a"":3,
        ""b"":4
    }
  ]
}";
        return;
    }

    [RelayCommand]
    public async void ClearInput()
    {
        if (InputString != "")
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Page.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "输入框已有内容";
            dialog.PrimaryButtonText = "清除";
            dialog.SecondaryButtonText = "不清除";
            dialog.CloseButtonText = "取消";
            dialog.DefaultButton = ContentDialogButton.Secondary;
            dialog.Content = "输入框已有内容，清楚后所有内容将会被清除\n是否清除？";

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                return;
            }
        }
        InputString = "";
    }

    [RelayCommand]
    public void CopyInput()
    {
        var package = new DataPackage();
        package.SetText(_CSharpString);
        Clipboard.SetContent(package);
    }

    [RelayCommand]
    public async void ResetOption()
    {
        ContentDialog dialog = new ContentDialog();

        // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
        dialog.XamlRoot = Page.XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "是否重置所有选项";
        dialog.PrimaryButtonText = "重置";
        //dialog.PrimaryButtonStyle = Application.Current.Resources["WarningButtonStyle"] as Style;
        dialog.SecondaryButtonText = "不重置";
        dialog.CloseButtonText = "取消";
        dialog.DefaultButton = ContentDialogButton.Secondary;
        dialog.Content = "是否重置所有选项";

        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Secondary)
        {
            return;
        }

        AutoFrontName = "";
        AutoReplace = "";
        ClassExname = "";
        NeedAutoFrontName = false;
        NeedAutoRename = false;
        NeedAutoReplace = false;
        NeedClassExname = false;
        NeedDoc = false; 
        NeedKeyDoc = false;
        NeedRawString = false;
        NeedValueDoc = false;
    }

    public async Task<string> CreateDoc(string name, JToken jtoken)
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine("    /// <summary>");
        if(NeedKeyDoc)
        {
            result.AppendLine("    /// Json Raw Key: " + name);
        }
        if(NeedValueDoc && jtoken.Type != JTokenType.Object && jtoken.Type != JTokenType.Array && jtoken.Type != JTokenType.Bytes)
        {
            try
            {
                result.AppendLine("    /// Json Raw Value: " + jtoken.Value<string>());
            }
            catch { }
        }
        if(NeedRawString && jtoken.Type !=  JTokenType.Object && jtoken.Type != JTokenType.Array && jtoken.Type != JTokenType.Bytes)
        {
            result.AppendLine("    /// Json Raw String: " + jtoken.ToString());
        }
        result.AppendLine("    /// </summary>");
        return result.ToString();
    }

    public async Task<(string aftername,bool needattribute)> RenameToAllow(string name)
    {
        bool replaced = false;
        string result = name;
        if (CSBelowNameChars.Contains(name[0]) || CSBelowNameStartWith.Contains(name[0]))
        {
            replaced = true;
            result = result.Insert(0, AutoFrontName);
        }

        if (name[1..].ToList().Exists(x => CSBelowNameChars.Contains(x)))
        {
            replaced = true;
            CSBelowNameChars.ToList().ForEach(x=>result = result.Replace(x.ToString(),AutoReplace));
        }

        return (result, replaced);
    }

    public string GetIntegerTypeName(JToken jtoken)
    {
        long l = (long)jtoken;
        if (int.MinValue <= l && l <= int.MaxValue)
            return "int";
        else
            return "long";
    }

    public StringBuilder CreateSimplePropertyString(string name, string type)
    {
        StringBuilder result = new StringBuilder();
        result.Append("    ");
        result.Append(CSProtect[Protect]);
        result.Append(" ");
        result.Append(type);
        result.Append(" ");
        result.Append(name);
        result.AppendLine(" { get; set; }");
        return result;
    }

    public async Task<string> CreateSimpleTypeProperty(string name,JToken jtoken,string typename = "")
    {
        StringBuilder result = new StringBuilder();
        string propname = name;
        if (NeedDoc)
        {
            result.Append(await CreateDoc(name, jtoken));
        }

        if (NeedAutoRename)
        {
            var t = await RenameToAllow(propname);
            if (t.needattribute)
            {
                result.AppendLine($"[JsonPropertyName(\"{propname}\")]");
                propname = t.aftername;
            }
        }
        string GetType(string name,JTokenType type)
        {
            Dictionary<JTokenType, string> keytype = new Dictionary<JTokenType, string>
            {
                [JTokenType.Object] = name,
                [JTokenType.String] = "string",
                [JTokenType.Boolean] = "bool",
                [JTokenType.Date] = "System.DateTime",
                [JTokenType.Array] = $"{name}[]",
                [JTokenType.Null] = "object",
                [JTokenType.Uri] = "System.Uri",
                [JTokenType.Bytes] = "byte[]",
                [JTokenType.TimeSpan] = "System.TimeSpan",
                [JTokenType.Guid] = "System.Guid",
                [JTokenType.Date] = "System.DateTime",
                [JTokenType.Float] = "float"
            };
            return keytype[type];
        }
        

        switch (jtoken.Type)
        {
            case JTokenType.Integer:
                result.Append(CreateSimplePropertyString(propname, GetIntegerTypeName(jtoken)));
                break;
            case JTokenType.Float:
                result.Append(CreateSimplePropertyString(propname, "float"));
                break;
            case JTokenType.String:
                result.Append(CreateSimplePropertyString(propname, "float"));
                break;
            case JTokenType.Boolean:
                result.Append(CreateSimplePropertyString(propname, "bool"));
                break;
            case JTokenType.Null:
                result.Append(CreateSimplePropertyString(propname, "object"));
                break;
            case JTokenType.Guid:
                result.Append(CreateSimplePropertyString(propname, "System.Guid"));
                break;
            case JTokenType.TimeSpan:
                result.Append(CreateSimplePropertyString(propname, "System.TimeSpan"));
                break;
            case JTokenType.Uri:
                result.Append(CreateSimplePropertyString(propname, "System.Uri"));
                break;
            case JTokenType.Date:
                result.Append(CreateSimplePropertyString(propname, "System.DateTime"));
                break;
            case JTokenType.Array:
                result.Append(CreateSimplePropertyString(propname, $"{GetType(ClassExname + propname, jtoken.ToArray()[0].Type)}[]"));
                break;
            case JTokenType.Object:
                result.Append(CreateSimplePropertyString(propname, typename));
                break;
            case JTokenType.Bytes:
                result.Append(CreateSimplePropertyString(propname, "byte[]"));
                break;
        }
        return result.ToString();
    }



    public async Task<List<string>> CreateClass(JObject jObject, string name)
    {
        List<List<string>> Classes = new List<List<string>>();
        List<string> Current = new List<string>();
        JObject jobj = jObject;
        Current.Add("class " + (NeedClassExname ? ClassExname : "") + name + Environment.NewLine);
        Current.Add("{" + Environment.NewLine);
        foreach (var kv in jobj)
        {
            var token = kv.Value;
            if (token.Type is JTokenType.Object or
                             JTokenType.Null or
                             JTokenType.Array)
            {
                switch(token.Type)
                {
                    case JTokenType.Object:
                        Classes.Add(await CreateClass(JObject.FromObject(token),kv.Key));
                        break;
                    case JTokenType.Constructor:
                        Classes.Add(await CreateClass(JObject.FromObject(token), kv.Key));
                        break;
                    case JTokenType.Property:
                        Classes.Add(await CreateClass(JObject.FromObject(token), kv.Key));
                        break;
                    case JTokenType.Array:
                        Classes.Add(await CreateClass(JObject.FromObject(token.ToArray()[0]), kv.Key));
                        break;
                }
            }
            Current.Add(await CreateSimpleTypeProperty(kv.Key, token,kv.Key));
            Current.Add(Environment.NewLine);
        }
        Current.RemoveAt(Current.Count - 1);
        Current.Add("}" + Environment.NewLine);
        foreach (var c in Classes)
        {
            Current.AddRange(c);
        }
        return Current;
    }

    public async void ToCS()
    {
        _JsonString = InputString;
        if ((_JsonString??"") != "")
        {
            try
            {
                _isjson = false;
                JObject jo = JObject.Parse(InputString);
                var cscode = await CreateClass(jo, "Root");
                _CSharpString = (Namespace == "" ? "" : $"namespace {Namespace};" + Environment.NewLine) + string.Join("", cscode);
                InputString = _CSharpString;
            }
            catch { }
        }
    }

    public async void ToJson()
    {
        InputString = _JsonString;
        _isjson = true;
    }
}
