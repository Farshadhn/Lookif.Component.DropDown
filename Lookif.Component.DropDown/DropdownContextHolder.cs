using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookif.Component.DropDown;

public class DropdownContextHolder<T>
{

    public DropdownContextHolder(string content, T key, bool status = false)
    {
        Content = content;
        Key = key;
        Status = status;
    }

    public DropdownContextHolder()
    {

    }
    public string Content { get; init; }
    public T Key { get; init; }
    public bool Status { get; set; }


}

