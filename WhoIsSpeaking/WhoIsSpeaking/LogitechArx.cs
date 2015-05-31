using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System;

public class LogitechArx
{
    public const int LOGI_ARX_ORIENTATION_PORTRAIT = 0x01;
    public const int LOGI_ARX_ORIENTATION_LANDSCAPE = 0x10;
    public const int LOGI_ARX_EVENT_FOCUS_ACTIVE = 0x01;
    public const int LOGI_ARX_EVENT_FOCUS_INACTIVE = 0x02;
    public const int LOGI_ARX_EVENT_TAP_ON_TAG = 0x04;
    public const int LOGI_ARX_EVENT_MOBILEDEVICE_ARRIVAL = 0x08;
    public const int LOGI_ARX_EVENT_MOBILEDEVICE_REMOVAL = 0x10;
    public const int LOGI_ARX_DEVICETYPE_IPHONE = 0x01;
    public const int LOGI_ARX_DEVICETYPE_IPAD = 0x02;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_SMALL = 0x03;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_NORMAL = 0x04;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_LARGE = 0x05;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_XLARGE = 0x06;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_OTHER = 0x07;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void logiArxCB(int eventType, int eventValue,
    [MarshalAs(UnmanagedType.LPWStr)]String eventArg, IntPtr context);

    public struct logiArxCbContext
    {
        public logiArxCB arxCallBack;
        public IntPtr arxContext;
    }

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxInit(String identifier, String
    friendlyName, ref		logiArxCbContext callback);
    
    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxAddFileAs(String filePath, String
    fileName, String mimeType = "");

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxAddContentAs(byte[] content, int size,
    String fileName, String mimeType = "");

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxAddUTF8StringAs(String stringContent,
    String fileName, String mimeType = "");

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxAddImageFromBitmap(byte[] bitmap, int
    width, int height, String fileName);

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetIndex(String fileName);

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetTagPropertyById(String tagId, String
    prop, String newValue);

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetTagsPropertyByClass(String
    tagsClass, String prop, String newValue);

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetTagContentById(String tagId, String
    newContent);

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetTagsContentByClass(String
    tagsClass, String newContent);

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiArxGetLastError();

    [DllImport("LogitechGArxControlEnginesWrapper.dll", CharSet =
    CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogiArxShutdown();
}

