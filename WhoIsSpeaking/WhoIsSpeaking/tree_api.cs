using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WhoIsSpeaking
{
    static class tree_api
    {

        public const int TVM_GETITEM = (TV_FIRST + 12);
        public const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        public const int TVGN_ROOT = 0x0;
        public const int TVGN_NEXT = 0x1;
        public const int TVGN_CHILD = 0x4;
        public const int TVIF_TEXT = 0x1;
        public const int MY_MAXLVITEMTEXT = 260;

        public const int TV_FIRST = 0x1100;
        public enum TVM
        {
            TVM_GETNEXTITEM = (TV_FIRST + 10),
            TVM_GETITEMA = (TV_FIRST + 12),
            TVM_GETITEM = (TV_FIRST + 62),
            TVM_GETCOUNT = (TV_FIRST + 5),
            TVM_SELECTITEM = (TV_FIRST + 11),
            TVM_DELETEITEM = (TV_FIRST + 1),
            TVM_EXPAND = (TV_FIRST + 2),
            TVM_GETITEMRECT = (TV_FIRST + 4),
            TVM_GETINDENT = (TV_FIRST + 6),
            TVM_SETINDENT = (TV_FIRST + 7),
            TVM_GETIMAGELIST = (TV_FIRST + 8),
            TVM_SETIMAGELIST = (TV_FIRST + 9),
            TVM_GETISEARCHSTRING = (TV_FIRST + 64),
            TVM_HITTEST = (TV_FIRST + 17),
        }

        public enum TVGN
        {
            TVGN_ROOT = 0x0,
            TVGN_NEXT = 0x1,
            TVGN_PREVIOUS = 0x2,
            TVGN_PARENT = 0x3,
            TVGN_CHILD = 0x4,
            TVGN_FIRSTVISIBLE = 0x5,
            TVGN_NEXTVISIBLE = 0x6,
            TVGN_PREVIOUSVISIBLE = 0x7,
            TVGN_DROPHILITE = 0x8,
            TVGN_CARET = 0x9,
            TVGN_LASTVISIBLE = 0xA
        }

        [Flags]
        public enum TVIF
        {
            TVIF_TEXT = 1,
            TVIF_IMAGE = 2,
            TVIF_PARAM = 4,
            TVIF_STATE = 8,
            TVIF_HANDLE = 16,
            TVIF_SELECTEDIMAGE = 32,
            TVIF_CHILDREN = 64,
            TVIF_INTEGRAL = 0x0080,
            TVIF_DI_SETITEM = 0x1000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TVITEMEX
        {
            public uint mask;
            public IntPtr hItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
            public int iIntegral;
            public uint uStateEx;
            public IntPtr hwnd;
            public int iExpandedImage;
            public int iReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TVITEM
        {
            public uint mask;
            public IntPtr hItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
        }

    }
}
