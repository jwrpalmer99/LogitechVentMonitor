﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WhoIsSpeaking
{
    public static class KeyCodes
    {
        public static List<keyPosition> keypositions;
        static KeyCodes()
        {
            keypositions = new List<keyPosition> { };
            string[] lines = File.ReadAllLines("keymap.txt");

            foreach (string line in lines)
            {
                if (!line.StartsWith("key,"))
                {
                    string[] ele = line.Split(new char[] {','});
                    //KeyCodes.ScanCode code = (KeyCodes.ScanCode)Enum.Parse(typeof(KeyCodes.ScanCode), ele[0]);
                    keyPosition k = new keyPosition();
                    //k.keyCode = (int)code;
                    k.keyname = ele[0];
                    k.x = Convert.ToSingle(ele[1]);
                    k.y = Convert.ToSingle(ele[2]);
                    k.width = Convert.ToSingle(ele[3]);
                    k.height = Convert.ToSingle(ele[4]);
                    k.keyCode = Convert.ToInt16(ele[5], 16);
                    keypositions.Add(k);
                }
            }

        }

        public enum KeyCode
        {
        ESC                     = 0x01,
        F1                      = 0x3b,
        F2                      = 0x3c,
        F3                      = 0x3d,
        F4                      = 0x3e,
        F5                      = 0x3f,
        F6                      = 0x40,
        F7                      = 0x41,
        F8                      = 0x42,
        F9                      = 0x43,
        F10                     = 0x44,
        F11                     = 0x57,
        F12                     = 0x58,
        PRINT_SCREEN            = 0x137,
        SCROLL_LOCK             = 0x46,
        PAUSE_BREAK             = 0x145,
        TILDE                   = 0x29,
        ONE                     = 0x02,
        TWO                     = 0x03,
        THREE                   = 0x04,
        FOUR                    = 0x05,
        FIVE                    = 0x06,
        SIX                     = 0x07,
        SEVEN                   = 0x08,
        EIGHT                   = 0x09,
        NINE                    = 0x0A,
        ZERO                    = 0x0B,
        MINUS                   = 0x0C,
        EQUALS                  = 0x0D,
        BACKSPACE               = 0x0E,
        INSERT                  = 0x152,
        HOME                    = 0x147,
        PAGE_UP                 = 0x149,
        NUM_LOCK                = 0x45,
        NUM_SLASH               = 0x135,
        NUM_ASTERISK            = 0x37,
        NUM_MINUS               = 0x4A,
        TAB                     = 0x0F,
        Q                       = 0x10,
        W                       = 0x11,
        E                       = 0x12,
        R                       = 0x13,
        T                       = 0x14,
        Y                       = 0x15,
        U                       = 0x16,
        I                       = 0x17,
        O                       = 0x18,
        P                       = 0x19,
        OPEN_BRACKET            = 0x1A,
        CLOSE_BRACKET           = 0x1B,
        BACKSLASH               = 0x2B,
        KEYBOARD_DELETE         = 0x153,
        END                     = 0x14F,
        PAGE_DOWN               = 0x151,
        NUM_SEVEN               = 0x47,
        NUM_EIGHT               = 0x48,
        NUM_NINE                = 0x49,
        NUM_PLUS                = 0x4E,
        CAPS_LOCK               = 0x3A,
        A                       = 0x1E,
        S                       = 0x1F,
        D                       = 0x20,
        F                       = 0x21,
        G                       = 0x22,
        H                       = 0x23,
        J                       = 0x24,
        K                       = 0x25,
        L                       = 0x26,
        SEMICOLON               = 0x27,
        APOSTROPHE              = 0x28,
        ENTER                   = 0x1C,
        NUM_FOUR                = 0x4B,
        NUM_FIVE                = 0x4C,
        NUM_SIX                 = 0x4D,
        LEFT_SHIFT              = 0x2A,
        Z                       = 0x2C,
        X                       = 0x2D,
        C                       = 0x2E,
        V                       = 0x2F,
        B                       = 0x30,
        N                       = 0x31,
        M                       = 0x32,
        COMMA                   = 0x33,
        PERIOD                  = 0x34,
        FORWARD_SLASH           = 0x35,
        RIGHT_SHIFT             = 0x36,
        ARROW_UP                = 0x148,
        NUM_ONE                 = 0x4F,
        NUM_TWO                 = 0x50,
        NUM_THREE               = 0x51,
        NUM_ENTER               = 0x11C,
        LEFT_CONTROL            = 0x1D,
        LEFT_WINDOWS            = 0x15B,
        LEFT_ALT                = 0x38,
        SPACE                   = 0x39,
        RIGHT_ALT               = 0x138,
        RIGHT_WINDOWS           = 0x15C,
        APPLICATION_SELECT      = 0x15D,
        RIGHT_CONTROL           = 0x11D,
        ARROW_LEFT              = 0x14B,
        ARROW_DOWN              = 0x150,
        ARROW_RIGHT             = 0x14D,
        NUM_ZERO                = 0x52,
        NUM_PERIOD              = 0x53,
    }

        //public enum ScanCode
        //{
        //    ESCAPE = 0x01,
        //    F1 = 0x3b,
        //    F2 = 0x3c,
        //    F3 = 0x3d,
        //    F4 = 0x3e,
        //    F5 = 0x3f,
        //    F6 = 0x40,
        //    F7 = 0x41,
        //    F8 = 0x42,
        //    F9 = 0x43,
        //    F10 = 0x44,
        //    F11 = 0x57,
        //    F12 = 0x58,
        //    PRINTSCREEN = 0x137,
        //    SCROLL = 0x46,
        //    PAUSE = 0x145,
        //    OEM8 = 0x29,
        //    ONE = 0x02,
        //    TWO = 0x03,
        //    THREE = 0x04,
        //    FOUR = 0x05,
        //    FIVE = 0x06,
        //    SIX = 0x07,
        //    SEVEN = 0x08,
        //    EIGHT = 0x09,
        //    NINE = 0x0A,
        //    ZERO = 0x0B,
        //    OEMMINUS = 0x0C,
        //    OEMPLUS = 0x0D,
        //    BACK = 0x0E,
        //    INS = 0x152,
        //    HOME = 0x147,
        //    PGUP = 0x149,
        //    NUMLOCK = 0x45,
        //    DIVIDE = 0x135,
        //    MULTIPLY = 0x37,
        //    SUBTRACT = 0x4A,
        //    TAB = 0x0F,
        //    Q = 0x10,
        //    W = 0x11,
        //    E = 0x12,
        //    R = 0x13,
        //    T = 0x14,
        //    Y = 0x15,
        //    U = 0x16,
        //    I = 0x17,
        //    O = 0x18,
        //    P = 0x19,
        //    OEMOPENBRACKETS = 0x1A,
        //    OEM6 = 0x1B,
        //    OEM5 = 0x2B,
        //    DEL = 0x153,
        //    END = 0x14F,
        //    PGDN = 0x151,
        //    NUMPAD7 = 0x47,
        //    NUMPAD8 = 0x48,
        //    NUMPAD9 = 0x49,
        //    ADD = 0x4E,
        //    CAPITAL = 0x3A,
        //    A = 0x1E,
        //    S = 0x1F,
        //    D = 0x20,
        //    F = 0x21,
        //    G = 0x22,
        //    H = 0x23,
        //    J = 0x24,
        //    K = 0x25,
        //    L = 0x26,
        //    OEM1 = 0x27,
        //    OEMTILDE = 0x28,
        //    ENTER = 0x1C,
        //    OEM7 = 0x5D,
        //    NUMPAD4 = 0x4B,
        //    NUMPAD5 = 0x4C,
        //    NUMPAD6 = 0x4D,
        //    LSHIFTKEY = 0x2A,
        //    Z = 0x2C,
        //    X = 0x2D,
        //    C = 0x2E,
        //    V = 0x2F,
        //    B = 0x30,
        //    N = 0x31,
        //    M = 0x32,
        //    OEMCOMMA = 0x33,
        //    OEMPERIOD = 0x34,
        //    OEMQUESTION = 0x35,
        //    RSHIFTKEY = 0x36,
        //    UP = 0x148,
        //    NUMPAD1 = 0x4F,
        //    NUMPAD2 = 0x50,
        //    NUMPAD3 = 0x51,
        //    NUM_ENTER = 0x11C,
        //    LCONTROLKEY = 0x1D,
        //    LWIN = 0x15B,
        //    LMENU = 0x38,
        //    SPACE = 0x39,
        //    RMENU = 0x138,
        //    RWIN = 0x15C,
        //    APPS = 0x15D,
        //    RCONTROLKEY = 0x11D,
        //    LEFT = 0x14B,
        //    DOWN = 0x150,
        //    RIGHT = 0x14D,
        //    NUMPAD0 = 0x52,
        //    DECIMAL = 0x53,
        //}

        public struct keyPosition
        {
            public int keyCode;
            public float x;
            public float y;
            public float width;
            public float height;
            public string keyname;
        }
    }
}
