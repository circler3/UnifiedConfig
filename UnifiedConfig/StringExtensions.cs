using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedConfig
{
    /// <summary>
    /// Extend the string convertion
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Return the value in the form of T
        /// </summary>
        /// <typeparam name="T">type name</typeparam>
        /// <param name="str">self</param>
        /// <returns>value in type T</returns>
        [CLSCompliant(false)]
        public static T ToObject<T>(this T str) where T : IConvertible
        {
            return (T)Convert.ChangeType(str, typeof(T));
        }
    }
}
