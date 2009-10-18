using System;

namespace FileHelpers
{
    // temporary Factory class to be replaced with Container calls.
    internal static class RecordInfoFactory
    {
        public static IRecordInfo CreateRecordInfo(Type recordType)
        {
            // TODO: Replace this with a Container.Resolve call (which needs to support ctor args)
            // then inline this method/class away
            return new RecordInfo(recordType);
            //return Container.Resolve<IRecordInfo>();
        }
    }
}