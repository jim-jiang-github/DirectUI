using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Win32.Const
{
    internal enum CDM : uint
    {
        CDM_FIRST = ((uint)WM.WM_USER + 100),
        CDM_GETSPEC = (CDM_FIRST + 0x0000),
        CDM_GETFILEPATH = (CDM_FIRST + 0x0001),
        CDM_GETFOLDERPATH = (CDM_FIRST + 0x0002),
        CDM_GETFOLDERIDLIST = (CDM_FIRST + 0x0003),
        CDM_SETCONTROLTEXT = (CDM_FIRST + 0x0004),
        CDM_HIDECONTROL = (CDM_FIRST + 0x0005),
        CDM_SETDEFEXT = (CDM_FIRST + 0x0006)
    }
}
