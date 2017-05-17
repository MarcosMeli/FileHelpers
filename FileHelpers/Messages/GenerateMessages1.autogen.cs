﻿namespace FileHelpers
{
    internal class Messages
{
internal class Errors
{

private static readonly TypesOfMessages.Errors.FieldOptionalClass mFieldOptional = new TypesOfMessages.Errors.FieldOptionalClass();
internal static TypesOfMessages.Errors.FieldOptionalClass FieldOptional
{ get { return  mFieldOptional; } }
private static readonly TypesOfMessages.Errors.InvalidIdentifierClass mInvalidIdentifier = new TypesOfMessages.Errors.InvalidIdentifierClass();
internal static TypesOfMessages.Errors.InvalidIdentifierClass InvalidIdentifier
{ get { return  mInvalidIdentifier; } }
private static readonly TypesOfMessages.Errors.EmptyClassNameClass mEmptyClassName = new TypesOfMessages.Errors.EmptyClassNameClass();
internal static TypesOfMessages.Errors.EmptyClassNameClass EmptyClassName
{ get { return  mEmptyClassName; } }
private static readonly TypesOfMessages.Errors.EmptyFieldNameClass mEmptyFieldName = new TypesOfMessages.Errors.EmptyFieldNameClass();
internal static TypesOfMessages.Errors.EmptyFieldNameClass EmptyFieldName
{ get { return  mEmptyFieldName; } }
private static readonly TypesOfMessages.Errors.EmptyFieldTypeClass mEmptyFieldType = new TypesOfMessages.Errors.EmptyFieldTypeClass();
internal static TypesOfMessages.Errors.EmptyFieldTypeClass EmptyFieldType
{ get { return  mEmptyFieldType; } }
private static readonly TypesOfMessages.Errors.ClassWithOutRecordAttributeClass mClassWithOutRecordAttribute = new TypesOfMessages.Errors.ClassWithOutRecordAttributeClass();
internal static TypesOfMessages.Errors.ClassWithOutRecordAttributeClass ClassWithOutRecordAttribute
{ get { return  mClassWithOutRecordAttribute; } }
private static readonly TypesOfMessages.Errors.ClassWithOutDefaultConstructorClass mClassWithOutDefaultConstructor = new TypesOfMessages.Errors.ClassWithOutDefaultConstructorClass();
internal static TypesOfMessages.Errors.ClassWithOutDefaultConstructorClass ClassWithOutDefaultConstructor
{ get { return  mClassWithOutDefaultConstructor; } }
private static readonly TypesOfMessages.Errors.ClassWithOutFieldsClass mClassWithOutFields = new TypesOfMessages.Errors.ClassWithOutFieldsClass();
internal static TypesOfMessages.Errors.ClassWithOutFieldsClass ClassWithOutFields
{ get { return  mClassWithOutFields; } }
private static readonly TypesOfMessages.Errors.ExpectingFieldOptionalClass mExpectingFieldOptional = new TypesOfMessages.Errors.ExpectingFieldOptionalClass();
internal static TypesOfMessages.Errors.ExpectingFieldOptionalClass ExpectingFieldOptional
{ get { return  mExpectingFieldOptional; } }
private static readonly TypesOfMessages.Errors.SameFieldOrderClass mSameFieldOrder = new TypesOfMessages.Errors.SameFieldOrderClass();
internal static TypesOfMessages.Errors.SameFieldOrderClass SameFieldOrder
{ get { return  mSameFieldOrder; } }
private static readonly TypesOfMessages.Errors.PartialFieldOrderClass mPartialFieldOrder = new TypesOfMessages.Errors.PartialFieldOrderClass();
internal static TypesOfMessages.Errors.PartialFieldOrderClass PartialFieldOrder
{ get { return  mPartialFieldOrder; } }
private static readonly TypesOfMessages.Errors.PartialFieldOrderInAutoPropertyClass mPartialFieldOrderInAutoProperty = new TypesOfMessages.Errors.PartialFieldOrderInAutoPropertyClass();
internal static TypesOfMessages.Errors.PartialFieldOrderInAutoPropertyClass PartialFieldOrderInAutoProperty
{ get { return  mPartialFieldOrderInAutoProperty; } }
private static readonly TypesOfMessages.Errors.MissingFieldArrayLenghtInNotLastFieldClass mMissingFieldArrayLenghtInNotLastField = new TypesOfMessages.Errors.MissingFieldArrayLenghtInNotLastFieldClass();
internal static TypesOfMessages.Errors.MissingFieldArrayLenghtInNotLastFieldClass MissingFieldArrayLenghtInNotLastField
{ get { return  mMissingFieldArrayLenghtInNotLastField; } }
private static readonly TypesOfMessages.Errors.SameMinMaxLengthForArrayNotLastFieldClass mSameMinMaxLengthForArrayNotLastField = new TypesOfMessages.Errors.SameMinMaxLengthForArrayNotLastFieldClass();
internal static TypesOfMessages.Errors.SameMinMaxLengthForArrayNotLastFieldClass SameMinMaxLengthForArrayNotLastField
{ get { return  mSameMinMaxLengthForArrayNotLastField; } }
private static readonly TypesOfMessages.Errors.FieldNotFoundClass mFieldNotFound = new TypesOfMessages.Errors.FieldNotFoundClass();
internal static TypesOfMessages.Errors.FieldNotFoundClass FieldNotFound
{ get { return  mFieldNotFound; } }
private static readonly TypesOfMessages.Errors.WrongConverterClass mWrongConverter = new TypesOfMessages.Errors.WrongConverterClass();
internal static TypesOfMessages.Errors.WrongConverterClass WrongConverter
{ get { return  mWrongConverter; } }
private static readonly TypesOfMessages.Errors.NullRecordClassClass mNullRecordClass = new TypesOfMessages.Errors.NullRecordClassClass();
internal static TypesOfMessages.Errors.NullRecordClassClass NullRecordClass
{ get { return  mNullRecordClass; } }
private static readonly TypesOfMessages.Errors.StructRecordClassClass mStructRecordClass = new TypesOfMessages.Errors.StructRecordClassClass();
internal static TypesOfMessages.Errors.StructRecordClassClass StructRecordClass
{ get { return  mStructRecordClass; } }
private static readonly TypesOfMessages.Errors.TestQuoteClass mTestQuote = new TypesOfMessages.Errors.TestQuoteClass();
internal static TypesOfMessages.Errors.TestQuoteClass TestQuote
{ get { return  mTestQuote; } }
private static readonly TypesOfMessages.Errors.MixOfStandardAndAutoPropertiesFieldsClass mMixOfStandardAndAutoPropertiesFields = new TypesOfMessages.Errors.MixOfStandardAndAutoPropertiesFieldsClass();
internal static TypesOfMessages.Errors.MixOfStandardAndAutoPropertiesFieldsClass MixOfStandardAndAutoPropertiesFields
{ get { return  mMixOfStandardAndAutoPropertiesFields; } }


}


}internal class TypesOfMessages
{
internal class Errors
{
internal class ClassWithOutDefaultConstructorClass: MessageBase
{

internal ClassWithOutDefaultConstructorClass(): base(@"The record class $ClassName$ needs a constructor with no args (public or private)") {}
 private string mClassName = null;
 internal ClassWithOutDefaultConstructorClass ClassName(string value)
{
    mClassName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$ClassName$", mClassName);
        return res;
    }


}internal class ClassWithOutFieldsClass: MessageBase
{

internal ClassWithOutFieldsClass(): base(@"The record class $ClassName$ don't contains any field") {}
 private string mClassName = null;
 internal ClassWithOutFieldsClass ClassName(string value)
{
    mClassName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$ClassName$", mClassName);
        return res;
    }


}internal class ClassWithOutRecordAttributeClass: MessageBase
{

internal ClassWithOutRecordAttributeClass(): base(@"The record class $ClassName$ must be marked with the [DelimitedRecord] or [FixedLengthRecord] Attribute") {}
 private string mClassName = null;
 internal ClassWithOutRecordAttributeClass ClassName(string value)
{
    mClassName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$ClassName$", mClassName);
        return res;
    }


}internal class EmptyClassNameClass: MessageBase
{

internal EmptyClassNameClass(): base(@"The ClassName can't be empty") {}
    protected override string GenerateText() 
    {
        var res = SourceText;
        return res;
    }


}internal class EmptyFieldNameClass: MessageBase
{

internal EmptyFieldNameClass(): base(@"The $Position$th field name can't be empty") {}
 private string mPosition = null;
 internal EmptyFieldNameClass Position(string value)
{
    mPosition = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$Position$", mPosition);
        return res;
    }


}internal class EmptyFieldTypeClass: MessageBase
{

internal EmptyFieldTypeClass(): base(@"The $Position$th field type can't be empty") {}
 private string mPosition = null;
 internal EmptyFieldTypeClass Position(string value)
{
    mPosition = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$Position$", mPosition);
        return res;
    }


}internal class ExpectingFieldOptionalClass: MessageBase
{

internal ExpectingFieldOptionalClass(): base(@"The field: $FieldName$ must be marked as optional because the previous field is marked as optional. (Try adding [FieldOptional] to $FieldName$)") {}
 private string mFieldName = null;
 internal ExpectingFieldOptionalClass FieldName(string value)
{
    mFieldName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$FieldName$", mFieldName);
        return res;
    }


}internal class FieldNotFoundClass: MessageBase
{

internal FieldNotFoundClass(): base(@"The field: $FieldName$ was not found in the class: $ClassName$. Remember that this option is case sensitive") {}
 private string mFieldName = null;
 internal FieldNotFoundClass FieldName(string value)
{
    mFieldName = value;
    return this;
}
 private string mClassName = null;
 internal FieldNotFoundClass ClassName(string value)
{
    mClassName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$FieldName$", mFieldName);
        res = StringHelper.ReplaceIgnoringCase(res, "$ClassName$", mClassName);
        return res;
    }


}internal class FieldOptionalClass: MessageBase
{

internal FieldOptionalClass(): base(@"The field: $Field$ must be marked as optional because the previous field is marked with FieldOptional. (Try adding [FieldOptional] to $Field$)") {}
 private string mField = null;
 internal FieldOptionalClass Field(string value)
{
    mField = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$Field$", mField);
        return res;
    }


}internal class InvalidIdentifierClass: MessageBase
{

internal InvalidIdentifierClass(): base(@"The string '$Identifier$' not is a valid .NET identifier") {}
 private string mIdentifier = null;
 internal InvalidIdentifierClass Identifier(string value)
{
    mIdentifier = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$Identifier$", mIdentifier);
        return res;
    }


}internal class MissingFieldArrayLenghtInNotLastFieldClass: MessageBase
{

internal MissingFieldArrayLenghtInNotLastFieldClass(): base(@"The field: $FieldName$ is of an array type and must contain a [FieldArrayLength] attribute because it is not the last field") {}
 private string mFieldName = null;
 internal MissingFieldArrayLenghtInNotLastFieldClass FieldName(string value)
{
    mFieldName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$FieldName$", mFieldName);
        return res;
    }


}internal class MixOfStandardAndAutoPropertiesFieldsClass: MessageBase
{

internal MixOfStandardAndAutoPropertiesFieldsClass(): base(@"You can mix standard fields and automatic properties only if you use [FieldOrder()] over the fields and properties in the $ClassName$ class.") {}
 private string mClassName = null;
 internal MixOfStandardAndAutoPropertiesFieldsClass ClassName(string value)
{
    mClassName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$ClassName$", mClassName);
        return res;
    }


}internal class NullRecordClassClass: MessageBase
{

internal NullRecordClassClass(): base(@"The record type can't be null") {}
    protected override string GenerateText() 
    {
        var res = SourceText;
        return res;
    }


}internal class PartialFieldOrderClass: MessageBase
{

internal PartialFieldOrderClass(): base(@"The field: $FieldName$ must be marked with FieldOrder because if you use this attribute in one field you must also use it on all of them.") {}
 private string mFieldName = null;
 internal PartialFieldOrderClass FieldName(string value)
{
    mFieldName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$FieldName$", mFieldName);
        return res;
    }


}internal class PartialFieldOrderInAutoPropertyClass: MessageBase
{

internal PartialFieldOrderInAutoPropertyClass(): base(@"The auto property: $PropertyName$ must be marked with FieldOrder because if you use this attribute in one field you must also use it on all of them.") {}
 private string mPropertyName = null;
 internal PartialFieldOrderInAutoPropertyClass PropertyName(string value)
{
    mPropertyName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$PropertyName$", mPropertyName);
        return res;
    }


}internal class SameFieldOrderClass: MessageBase
{

internal SameFieldOrderClass(): base(@"The field: $FieldName1$ has the same FieldOrder that: $FieldName2$ you must use different values") {}
 private string mFieldName1 = null;
 internal SameFieldOrderClass FieldName1(string value)
{
    mFieldName1 = value;
    return this;
}
 private string mFieldName2 = null;
 internal SameFieldOrderClass FieldName2(string value)
{
    mFieldName2 = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$FieldName1$", mFieldName1);
        res = StringHelper.ReplaceIgnoringCase(res, "$FieldName2$", mFieldName2);
        return res;
    }


}internal class SameMinMaxLengthForArrayNotLastFieldClass: MessageBase
{

internal SameMinMaxLengthForArrayNotLastFieldClass(): base(@"The array field: $FieldName$ must be of a fixed length because it is not the last field of the class, i.e. the min and max length of the [FieldArrayLength] attribute must be the same.") {}
 private string mFieldName = null;
 internal SameMinMaxLengthForArrayNotLastFieldClass FieldName(string value)
{
    mFieldName = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$FieldName$", mFieldName);
        return res;
    }


}internal class StructRecordClassClass: MessageBase
{

internal StructRecordClassClass(): base(@"The record type must be a class, and the type: $RecordType$ is a struct.") {}
 private string mRecordType = null;
 internal StructRecordClassClass RecordType(string value)
{
    mRecordType = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$RecordType$", mRecordType);
        return res;
    }


}internal class TestQuoteClass: MessageBase
{

internal TestQuoteClass(): base(@"The Message class also allows to use "" in any part of the "" text "" .") {}
    protected override string GenerateText() 
    {
        var res = SourceText;
        return res;
    }


}internal class WrongConverterClass: MessageBase
{

internal WrongConverterClass(): base(@"The converter for the field: $FieldName$ returns an object of Type: $ConverterReturnedType$  and the field is of type: $FieldType$") {}
 private string mFieldName = null;
 internal WrongConverterClass FieldName(string value)
{
    mFieldName = value;
    return this;
}
 private string mConverterReturnedType = null;
 internal WrongConverterClass ConverterReturnedType(string value)
{
    mConverterReturnedType = value;
    return this;
}
 private string mFieldType = null;
 internal WrongConverterClass FieldType(string value)
{
    mFieldType = value;
    return this;
}
    protected override string GenerateText() 
    {
        var res = SourceText;
        res = StringHelper.ReplaceIgnoringCase(res, "$FieldName$", mFieldName);
        res = StringHelper.ReplaceIgnoringCase(res, "$ConverterReturnedType$", mConverterReturnedType);
        res = StringHelper.ReplaceIgnoringCase(res, "$FieldType$", mFieldType);
        return res;
    }


}


}


}
}

