using System;

namespace FileHelpers
{
	internal class EnumConverter : ConverterBase
	{
		Type mEnumType;

		public EnumConverter(Type sourceEnum)
		{
			if (sourceEnum.IsEnum == false)
				throw new BadUsageException("The sourceType must be an Enum and is of type " + sourceEnum.Name);

			mEnumType = sourceEnum;
		}

		public override object StringToField(string from)
		{
			return Enum.Parse(mEnumType, from.Trim(), true);
		}
	}
}