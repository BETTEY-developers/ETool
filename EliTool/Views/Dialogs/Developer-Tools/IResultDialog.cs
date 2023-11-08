using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliTool.Views.Dialogs.DeveloperTools
{
    interface IResultDialog<T, in IT>
    {
        public T Result { get; }

        public void SetIn(IT invalue);
    }

    interface IResultDialog<T>
    {
        public T Result
        {
            get;
        }

        public void SetIn(T invalue);
    }
}
