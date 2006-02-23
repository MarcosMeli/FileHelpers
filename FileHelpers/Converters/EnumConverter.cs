using System;
using System.Text;

namespace FileHelpers
{
    internal class EnumConverter: ConverterBase
    {
        Type mEnumType;
        
        public EnumConverter(Type sourceEnum)
        {
            if (sourceEnum.IsEnum == false)
                throw new InternalException("The sourceType must be an Enum.");

            mEnumType = sourceEnum;
        }
        
        public override object StringToField(string from)
        {
            return Enum.Parse(mEnumType, from.Trim(), true);
        }
    }
}
