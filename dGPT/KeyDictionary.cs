using System;

namespace dGPT
{
    internal class KeyDictionary
    {
        public static uint ParseKey(string key)
        {
            return (uint)Convert.ToChar(key);
        }
    }
}