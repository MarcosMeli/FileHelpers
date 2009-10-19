using System;

namespace FileHelpers
{
    internal interface IFieldInfoCacheManipulator
    {
        void ResetFieldInfoCache(Type type);
    }
}