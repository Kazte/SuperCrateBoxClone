﻿using System;
using System.Collections.Generic;

namespace Game
{
    public class Utils
    {
        public static float Focus(float start, float end, float shift)
        {
            if (start < end)
            {
                return Math.Min(start + shift, end);
            }
            else
            {
                return Math.Max(start - shift, end);
            }
        }

        public static List<string> LoadAnims()
        {
            throw new NotImplementedException();
        }

        public static T Clamp<T>(T value, T max, T min) where T : System.IComparable<T>
        {
            T result = value;
            if (value.CompareTo(max) > 0)
                result = max;
            if (value.CompareTo(min) < 0)
                result = min;
            return result;
        }

        internal static sbyte Focus(sbyte yspd, object yspdMax, object gravity)
        {
            throw new NotImplementedException();
        }
    }
}
