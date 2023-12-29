using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EliTool.Contracts.Services;
using EliTool.Helpers;
using EliTool.Views.ControlPage.DeveloperTools;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace EliTool.ViewModels;

public partial class JsonCSharpConverterViewModel : ObservableRecipient, INotifyPropertyChanged
{

    private bool opt_ischanged = true;

    private char[] CSBelowNameChars = new char[]
    {
        '#','!','@','$','%','^','&','*','(',')','+','=',
        '{','[',']','}',':',';','"','\'',',','<','.','>',
        '`','~','！','·','。','，','‘','’','“','”','：','；',
        '？','（','）', '0','1','2','3','4','5','6','7','8',
        '9','-'
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
            opt_ischanged = true;
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
            opt_ischanged = true;
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
            opt_ischanged = true;
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
            opt_ischanged = true;
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
            opt_ischanged = true;
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
            opt_ischanged = true;
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
            _classexname = FilterNameBelowChars(value);
            opt_ischanged = true;
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
            opt_ischanged = true;
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
            opt_ischanged = true;
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
            _autofrontname = FilterNameBelowChars(value);
            _autofrontname = FilterBelowChars(_autofrontname, CSBelowNameStartWith.ToList());
            opt_ischanged = true;
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
            opt_ischanged = true;
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
            _autoreplace = FilterNameBelowChars(value);
            opt_ischanged = true;
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
            _namespace = FilterNamespaceBelowChars(value);
            opt_ischanged = true;
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
            opt_ischanged = true;
            OnPropertyChanged(nameof(Protect));
        }
    }

    private bool _isjson = true;

    public bool IsJson => _isjson;
    public string FilterNamespaceBelowChars(string orgstring)
    {
        var temp = orgstring;
        var namespaceBelow = CSBelowNameChars.ToList();
        namespaceBelow.Remove('.');
        return FilterBelowChars(temp, namespaceBelow);
    }
    public string FilterNameBelowChars(string orgstring) => FilterBelowChars(orgstring, CSBelowNameChars.ToList());

    public string FilterBelowChars(string orgstring, List<char> below)
    {
        var temp = orgstring;
        below.ForEach(x => temp = temp.Replace(x.ToString(), ""));
        return temp;
    }

    public JsonCSharpConverterPage Page
    {
        get; set;
    }

    public JsonCSharpConverterViewModel()
    {
    }

    [RelayCommand]
    public void NavigationTo(string type)
    {
        App.GetService<INavigationService>().NavigateTo(type);
    }

    [RelayCommand]
    public async void SummonTest()
    {
        if (InputString != "" && InputString != null)
        {
            var result = await DialogHelper.StringContentDialog(
                    "JCC_InputIsNotNul".GetLocalized("JsonCSharpConverterPage"),
                    "JCC_Summon_Example_Choose".GetLocalized("JsonCSharpConverterPage"),
                    Primary: "System_Operator_Summon".GetLocalized(),
                    Secondary: "System_Operator_NoSummon".GetLocalized(),
                    Close: "System_Cancel".GetLocalized()
                    ).ShowAsync();
            if (result is ContentDialogResult.Secondary or ContentDialogResult.None)
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
  ],
  ""!Wrong_Name_st"":1,
  ""Wrong_Name2_nd"":1
}";
        return;
    }

    [RelayCommand]
    public async void ClearInput()
    {
        if (InputString != "")
        {
            var result = await DialogHelper.StringContentDialog(
                    "JCC_InputIsNotNul".GetLocalized("JsonCSharpConverterPage"),
                    "JCC_InputIsNotNul_Choose".GetLocalized("JsonCSharpConverterPage"),
                    Primary: "JCC_Clear_Input".GetLocalized("JsonCSharpConverterPage"),
                    Secondary: "JCC_NoClear_Input".GetLocalized("JsonCSharpConverterPage"),
                    Close: "System_Cancel".GetLocalized()
                    ).ShowAsync();
            if (result is ContentDialogResult.Secondary or ContentDialogResult.None)
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
        var result = await DialogHelper.StringContentDialog(
                "JCC_Choose_Reset_AllOfOptions".GetLocalized("JsonCSharpConverterPage"),
                "JCC_Choose_Reset_AllOfOptions".GetLocalized("JsonCSharpConverterPage"),
                Primary: "System_Operator_Reset".GetLocalized(),
                Secondary: "System_Operator_NoReset".GetLocalized()
                ).ShowAsync(); ;
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

    public string CreateDoc(string name, JToken jtoken)
    {
        var result = new StringBuilder();
        result.AppendLine("    /// <summary>");
        if (NeedKeyDoc)
        {
            result.AppendLine("    /// Json Raw Key: " + name);
        }

        if (NeedValueDoc && jtoken.Type != JTokenType.Object && jtoken.Type != JTokenType.Array && jtoken.Type != JTokenType.Bytes)
        {
            try
            {
                result.AppendLine("    /// Json Raw Value: " + jtoken.Value<string>());
            }
            catch { }
        }

        if (NeedRawString && jtoken.Type != JTokenType.Object && jtoken.Type != JTokenType.Array && jtoken.Type != JTokenType.Bytes)
        {
            result.AppendLine("    /// Json Raw String: " + jtoken.ToString());
        }
        result.AppendLine("    /// </summary>");
        return result.ToString();
    }

    public (string aftername, bool needattribute) RenameToAllow(string name)
    {
        var replaced = false;
        var result = name;
        
        if (CSBelowNameChars.Contains(name[0]) || CSBelowNameStartWith.Contains(name[0]))
        {
            replaced = true;
            var t = result.Insert(0, AutoFrontName??"").ToList();
            t.RemoveAt(AutoFrontName is null or ""? 0 : 1);
            result = "";
            t.ForEach(x => result += x);
        }

        if (name[1..].ToList().Exists(x => CSBelowNameChars.Contains(x)))
        {
            replaced = true;
            CSBelowNameChars.ToList().ForEach(x => result = result.Replace(x.ToString(), AutoReplace??""));
        }

        return (result, replaced);
    }

    public string GetIntegerTypeName(JToken jtoken)
    {
        var l = (long)jtoken;
        if (int.MinValue <= l && l <= int.MaxValue)
            return "int";
        else
            return "long";
    }

    public StringBuilder CreateSimplePropertyString(string name, string type)
    {
        var result = new StringBuilder();
        result.Append("    ");
        result.Append(CSProtect[Protect]);
        result.Append(" ");
        result.Append(type);
        result.Append(" ");
        result.Append(name);
        result.AppendLine(" { get; set; }");
        return result;
    }

    public string CreateSimpleTypeProperty(string name, JToken jtoken, string typename = "")
    {
        var result = new StringBuilder();
        var propname = name;
        if (NeedDoc)
        {
            result.Append(CreateDoc(name, jtoken));
        }

        if (NeedAutoRename)
        {
            var (aftername, needattribute) = RenameToAllow(propname);
            if (needattribute)
            {
                result.AppendLine($"    [JsonPropertyName(\"{propname}\")]");
                propname = aftername;
            }
        }
        string GetType(string name, JTokenType type)
        {
            var keytype = new Dictionary<JTokenType, string>
            {
                [JTokenType.Object] = name,
                [JTokenType.String] = "string",
                [JTokenType.Boolean] = "bool",
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

        if (jtoken.Type == JTokenType.Integer)
        {
            result.Append(CreateSimplePropertyString(propname, GetIntegerTypeName(jtoken)));
        }
        else
        {
            result.Append(CreateSimplePropertyString(propname, GetType(propname, jtoken.Type)));
        }
        return result.ToString();
    }


    public async Task<List<string>> CreateClass(JObject jObject, string name)
    {
        var Classes = new List<List<string>>();
        var Current = new List<string>();
        var jobj = jObject;
        Current.Add("class " + (NeedClassExname ? ClassExname : "") + name + Environment.NewLine);
        Current.Add("{" + Environment.NewLine);
        foreach (var kv in jobj)
        {
            var token = kv.Value;
            if (token.Type is JTokenType.Object or
                             JTokenType.Null or
                             JTokenType.Array)
            {
                switch (token.Type)
                {
                    case JTokenType.Object:
                    case JTokenType.Constructor:
                    case JTokenType.Property:
                        Classes.Add(await CreateClass(JObject.FromObject(token), kv.Key));
                        break;
                    case JTokenType.Array:
                        Classes.Add(await CreateClass(JObject.FromObject(token.ToArray()[0]), kv.Key));
                        break;
                }
            }
            Current.Add(CreateSimpleTypeProperty(kv.Key, token, kv.Key));
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
        if ((_JsonString ?? "") != "" && opt_ischanged)
        {
            try
            {
                _isjson = false;
                var jo = JObject.Parse(InputString);
                var cscode = await CreateClass(jo, "Root");
                _CSharpString = (Namespace is "" or null ? "" : $"namespace {Namespace};" + Environment.NewLine) + string.Join("", cscode);
                InputString = _CSharpString;
            }
            catch { }
        }
    }

    public void ToJson()
    {
        InputString = _JsonString;
        _isjson = true;
    }

    [RelayCommand]
    public async void OpenWithFile()
    {
        var openPicker = new FileOpenPicker();

        // Initialize the file picker with the window handle (HWND).
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, App.MainWindow.GetWindowHandle());

        // Set options for your file picker
        openPicker.ViewMode = PickerViewMode.Thumbnail;
        openPicker.FileTypeFilter.Add("*");

        // Open the picker for the user to pick a file
        var file = await openPicker.PickSingleFileAsync();

        if (file == null)
        {
            await DialogHelper.StringContentDialog(
                "System_Operator_Cancel".GetLocalized(),
                "System_Operator_Cancel".GetLocalized(),
                Primary: "System_OK".GetLocalized()
                ).ShowAsync();
            return;
        }

        var json = await FileIO.ReadTextAsync(file);

        _JsonString = json;

        if (IsJson)
            InputString = _JsonString;
    }

    [RelayCommand]
    public async void SaveWithFile()
    {
        if (_CSharpString == "")
        {
            await DialogHelper.StringContentDialog(
                "System_Caption".GetLocalized(),
                "JCC_CSharpCode_IsNul".GetLocalized("JsonCSharpConverterPage"),
                Primary: "System_OK".GetLocalized()
                ).ShowAsync();
            return;
        }

        var savePicker = new Windows.Storage.Pickers.FileSavePicker();

        savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

        savePicker.FileTypeChoices.Add("JSON", new List<string>() { ".json " });
        // Initialize the file picker with the window handle (HWND).
        WinRT.Interop.InitializeWithWindow.Initialize(savePicker, App.MainWindow.GetWindowHandle());

        var file = await savePicker.PickSaveFileAsync();

        if (file == null)
        {
            await DialogHelper.StringContentDialog(
                "System_Operator_Cancel".GetLocalized(),
                "System_Operator_Cancel".GetLocalized(),
                Primary: "System_OK".GetLocalized()
                ).ShowAsync();
            return;
        }

        FileIO.AppendTextAsync(file, _CSharpString);

        await DialogHelper.StringContentDialog(
            "System_OK".GetLocalized(),
            "JCC_Summon_JSON_Complated".GetLocalized("JsonCSharpConverterPage"),
            Primary: "System_OK".GetLocalized()
            ).ShowAsync();
    }
}
