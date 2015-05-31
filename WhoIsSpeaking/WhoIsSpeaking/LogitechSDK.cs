using System.Collections; 
using System.Runtime.InteropServices; 
using System.Collections.Specialized;
using System;  

public class LogitechGSDK  {  //LED SDK  
public const int  LOGI_LED_BITMAP_WIDTH = 21; 
public const int  LOGI_LED_BITMAP_HEIGHT =  6; 
public const int  LOGI_LED_BITMAP_BYTES_PER_KEY = 4;  
public const int  LOGI_LED_BITMAP_SIZE = LOGI_LED_BITMAP_WIDTH*LOGI_LED_BITMAP_HEIGHT*LOGI_LED_BITMAP_BYTES_PER_KEY;  
public const int  LOGI_LED_DURATION_INFINITE = 0;  
    
[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
public static extern bool LogiLedInit();  
    
[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]  
public static extern bool LogiLedSaveCurrentLighting(); 

[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)] 
public static extern bool LogiLedSetLighting(int redPercentage, int greenPercentage, int bluePercentage);   

[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)] 
public static extern bool LogiLedRestoreLighting ();  
 
[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)] 
public static extern bool LogiLedFlashLighting( int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);   
[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
public static extern bool LogiLedPulseLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);  
[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]  
public static extern bool LogiLedStopEffects ();   
[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]  
public static extern bool LogiLedSetLightingFromBitmap (byte[] bitmap);  

[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]  

public static extern bool LogiLedSetLightingForKeyWithScanCode (int keyCode, int redPercentage, int greenPercentage, int bluePercentage);  

[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)] 
public static extern bool LogiLedSetLightingForKeyWithHidCode (int keyCode, int redPercentage, int greenPercentage, int bluePercentage);  

[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)] 
public static extern bool LogiLedSetLightingForKeyWithQuartzCode (int keyCode, int redPercentage, int greenPercentage, int bluePercentage);  

[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)] 
public static extern bool LogiLedSetLightingForKeyWithKeyNameCode (int keyCode, int redPercentage, int greenPercentage, int bluePercentage);  

[DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]  
public static extern void LogiLedShutdown(); }