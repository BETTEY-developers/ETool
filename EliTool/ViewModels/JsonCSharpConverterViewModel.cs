using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EliTool.Views.ControlPage.DeveloperTools;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Windows.UI.Popups;
using System.ComponentModel;
using Windows.ApplicationModel.DataTransfer;

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

    private string _inputstring;
    public string InputString
    {
        get
        {
            return _inputstring ?? "";
        }
        set
        {
            _inputstring = value;
            OnPropertyChanged(nameof(InputString));
        }
    }

    private bool _needdoc;
    public bool Needdoc
    {
        get
        {
            return _needdoc;
        }
        set
        {
            _needdoc = value;
            OnPropertyChanged(nameof(Needdoc));
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
            _needdoc = value;
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
            return _needautorename;
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
            string temp = value;
            CSBelowNameChars.ToList().ForEach(x => temp.Replace(x.ToString(), ""));
            _autoreplace = temp;
        }
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
    public void ClearInput()
    {
        InputString = "";
    }

    [RelayCommand]
    public void CopyInput()
    {
        //var package = new DataPackage();
        //package.SetText(InputString);
        //Clipboard.SetContent(package);
    }
}
