

using System;
using System.Reflection;
using System.Text;

namespace FileHelpers
{

	internal static class Attributes
	{
        public static T GetFirst<T>(MemberInfo type) where T : Attribute
        {
            return GetFirstCore<T>(type, false);
        }

        public static T GetFirstInherited<T>(MemberInfo type) where T : Attribute
        {
            return GetFirstCore<T>(type, true);
        }

        private static T GetFirstCore<T>(MemberInfo type, bool inherited) where T : Attribute
	    {
	        var attribs = type.GetCustomAttributes(typeof(T), inherited);
	        if (attribs.Length == 0)
	            return null;
	        else
	            return (T)attribs[0];
	    }

        public static void WorkWithFirst<T>(MemberInfo type, Action<T> action) where T : Attribute
        {
            var attribs = type.GetCustomAttributes(typeof(T), false);
            if (attribs.Length == 0)
                return;

            action((T) attribs[0]);
        }

	}
        
}
