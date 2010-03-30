using FileHelpers;
using System.Diagnostics;
using System; 

namespace FileHelpers
{
internal  partial class Messages
{
public  partial class Errors
{

private static TypesOfMessages.Errors.FieldOptionalClass mFieldOptional = new TypesOfMessages.Errors.FieldOptionalClass();
public static TypesOfMessages.Errors.FieldOptionalClass FieldOptional
{ get { return  mFieldOptional; } }
private static TypesOfMessages.Errors.InvalidIdentifierClass mInvalidIdentifier = new TypesOfMessages.Errors.InvalidIdentifierClass();
public static TypesOfMessages.Errors.InvalidIdentifierClass InvalidIdentifier
{ get { return  mInvalidIdentifier; } }
private static TypesOfMessages.Errors.EmptyClassNameClass mEmptyClassName = new TypesOfMessages.Errors.EmptyClassNameClass();
public static TypesOfMessages.Errors.EmptyClassNameClass EmptyClassName
{ get { return  mEmptyClassName; } }
private static TypesOfMessages.Errors.EmptyFieldNameClass mEmptyFieldName = new TypesOfMessages.Errors.EmptyFieldNameClass();
public static TypesOfMessages.Errors.EmptyFieldNameClass EmptyFieldName
{ get { return  mEmptyFieldName; } }
private static TypesOfMessages.Errors.EmptyFieldTypeClass mEmptyFieldType = new TypesOfMessages.Errors.EmptyFieldTypeClass();
public static TypesOfMessages.Errors.EmptyFieldTypeClass EmptyFieldType
{ get { return  mEmptyFieldType; } }
private static TypesOfMessages.Errors.ClassWithOutRecordAttributeClass mClassWithOutRecordAttribute = new TypesOfMessages.Errors.ClassWithOutRecordAttributeClass();
public static TypesOfMessages.Errors.ClassWithOutRecordAttributeClass ClassWithOutRecordAttribute
{ get { return  mClassWithOutRecordAttribute; } }
private static TypesOfMessages.Errors.ClassWithOutDefaultConstructorClass mClassWithOutDefaultConstructor = new TypesOfMessages.Errors.ClassWithOutDefaultConstructorClass();
public static TypesOfMessages.Errors.ClassWithOutDefaultConstructorClass ClassWithOutDefaultConstructor
{ get { return  mClassWithOutDefaultConstructor; } }
private static TypesOfMessages.Errors.ClassWithOutFieldsClass mClassWithOutFields = new TypesOfMessages.Errors.ClassWithOutFieldsClass();
public static TypesOfMessages.Errors.ClassWithOutFieldsClass ClassWithOutFields
{ get { return  mClassWithOutFields; } }
private static TypesOfMessages.Errors.ExpectingFieldOptionalClass mExpectingFieldOptional = new TypesOfMessages.Errors.ExpectingFieldOptionalClass();
public static TypesOfMessages.Errors.ExpectingFieldOptionalClass ExpectingFieldOptional
{ get { return  mExpectingFieldOptional; } }
private static TypesOfMessages.Errors.SameFieldOrderClass mSameFieldOrder = new TypesOfMessages.Errors.SameFieldOrderClass();
public static TypesOfMessages.Errors.SameFieldOrderClass SameFieldOrder
{ get { return  mSameFieldOrder; } }
private static TypesOfMessages.Errors.PartialFieldOrderClass mPartialFieldOrder = new TypesOfMessages.Errors.PartialFieldOrderClass();
public static TypesOfMessages.Errors.PartialFieldOrderClass PartialFieldOrder
{ get { return  mPartialFieldOrder; } }
private static TypesOfMessages.Errors.MissingFieldArrayLenghtInNotLastFieldClass mMissingFieldArrayLenghtInNotLastField = new TypesOfMessages.Errors.MissingFieldArrayLenghtInNotLastFieldClass();
public static TypesOfMessages.Errors.MissingFieldArrayLenghtInNotLastFieldClass MissingFieldArrayLenghtInNotLastField
{ get { return  mMissingFieldArrayLenghtInNotLastField; } }
private static TypesOfMessages.Errors.SameMinMaxLengthForArrayNotLastFieldClass mSameMinMaxLengthForArrayNotLastField = new TypesOfMessages.Errors.SameMinMaxLengthForArrayNotLastFieldClass();
public static TypesOfMessages.Errors.SameMinMaxLengthForArrayNotLastFieldClass SameMinMaxLengthForArrayNotLastField
{ get { return  mSameMinMaxLengthForArrayNotLastField; } }
private static TypesOfMessages.Errors.FieldNotFoundClass mFieldNotFound = new TypesOfMessages.Errors.FieldNotFoundClass();
public static TypesOfMessages.Errors.FieldNotFoundClass FieldNotFound
{ get { return  mFieldNotFound; } }
private static TypesOfMessages.Errors.WrongConverterClass mWrongConverter = new TypesOfMessages.Errors.WrongConverterClass();
public static TypesOfMessages.Errors.WrongConverterClass WrongConverter
{ get { return  mWrongConverter; } }
private static TypesOfMessages.Errors.NullRecordClassClass mNullRecordClass = new TypesOfMessages.Errors.NullRecordClassClass();
public static TypesOfMessages.Errors.NullRecordClassClass NullRecordClass
{ get { return  mNullRecordClass; } }
private static TypesOfMessages.Errors.StructRecordClassClass mStructRecordClass = new TypesOfMessages.Errors.StructRecordClassClass();
public static TypesOfMessages.Errors.StructRecordClassClass StructRecordClass
{ get { return  mStructRecordClass; } }
private static TypesOfMessages.Errors.TestQuoteClass mTestQuote = new TypesOfMessages.Errors.TestQuoteClass();
public static TypesOfMessages.Errors.TestQuoteClass TestQuote
{ get { return  mTestQuote; } }
private static TypesOfMessages.Errors.MixOfStandardAndAutoPropertiesFieldsClass mMixOfStandardAndAutoPropertiesFields = new TypesOfMessages.Errors.MixOfStandardAndAutoPropertiesFieldsClass();
public static TypesOfMessages.Errors.MixOfStandardAndAutoPropertiesFieldsClass MixOfStandardAndAutoPropertiesFields
{ get { return  mMixOfStandardAndAutoPropertiesFields; } }


}


}internal  partial class TypesOfMessages
{
public  partial class Errors
{
public  partial class ClassWithOutDefaultConstructorClass: MessageBase
{

public ClassWithOutDefaultConstructorClass(): base(@"The record class $ClassName$ needs a constructor with no args (public or private)") {}
 private string mClassName = null;
 public ClassWithOutDefaultConstructorClass ClassName(string value)
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


}public  partial class ClassWithOutFieldsClass: MessageBase
{

public ClassWithOutFieldsClass(): base(@"The record class $ClassName$ don't contains any field") {}
 private string mClassName = null;
 public ClassWithOutFieldsClass ClassName(string value)
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


}public  partial class ClassWithOutRecordAttributeClass: MessageBase
{

public ClassWithOutRecordAttributeClass(): base(@"The record class $ClassName$ must be marked with the [DelimitedRecord] or [FixedLengthRecord] Attribute") {}
 private string mClassName = null;
 public ClassWithOutRecordAttributeClass ClassName(string value)
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


}public  partial class EmptyClassNameClass: MessageBase
{

public EmptyClassNameClass(): base(@"The ClassName can't be empty") {}
    protected override string GenerateText() 
    {
        var res = SourceText;
        return res;
    }


}public  partial class EmptyFieldNameClass: MessageBase
{

public EmptyFieldNameClass(): base(@"The $Position$th field name can't be empty") {}
 private string mPosition = null;
 public EmptyFieldNameClass Position(string value)
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


}public  partial class EmptyFieldTypeClass: MessageBase
{

public EmptyFieldTypeClass(): base(@"The $Position$th field type can't be empty") {}
 private string mPosition = null;
 public EmptyFieldTypeClass Position(string value)
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


}public  partial class ExpectingFieldOptionalClass: MessageBase
{

public ExpectingFieldOptionalClass(): base(@"The field: $FieldName$ must be marked as optional because his previoud field is marked as optional. (Try adding [FieldOptional] to $FieldName$)") {}
 private string mFieldName = null;
 public ExpectingFieldOptionalClass FieldName(string value)
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


}public  partial class FieldNotFoundClass: MessageBase
{

public FieldNotFoundClass(): base(@"The field: $FieldName$ was not found in the class: $ClassName$. Remember that this option is case sensitive") {}
 private string mFieldName = null;
 public FieldNotFoundClass FieldName(string value)
{
    mFieldName = value;
    return this;
}
 private string mClassName = null;
 public FieldNotFoundClass ClassName(string value)
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


}public  partial class FieldOptionalClass: MessageBase
{

public FieldOptionalClass(): base(@"The field: $Field$ must be marked as optional because the previous field is marked with FieldOptional. (Try adding [FieldOptional] to $Field$)") {}
 private string mField = null;
 public FieldOptionalClass Field(string value)
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


}public  partial class InvalidIdentifierClass: MessageBase
{

public InvalidIdentifierClass(): base(@"The string '$Identifier$' not is a valid .NET identifier") {}
 private string mIdentifier = null;
 public InvalidIdentifierClass Identifier(string value)
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


}public  partial class MissingFieldArrayLenghtInNotLastFieldClass: MessageBase
{

public MissingFieldArrayLenghtInNotLastFieldClass(): base(@"The field: $FieldName$ is of an array type and must contain a [FieldArrayLength] attribute because is not the last field") {}
 private string mFieldName = null;
 public MissingFieldArrayLenghtInNotLastFieldClass FieldName(string value)
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


}public  partial class MixOfStandardAndAutoPropertiesFieldsClass: MessageBase
{

public MixOfStandardAndAutoPropertiesFieldsClass(): base(@"You must only use all standard fields or all automatic properties, but you can't mix them like in the $ClassName$ class.") {}
 private string mClassName = null;
 public MixOfStandardAndAutoPropertiesFieldsClass ClassName(string value)
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


}public  partial class NullRecordClassClass: MessageBase
{

public NullRecordClassClass(): base(@"The record type can't be null") {}
    protected override string GenerateText() 
    {
        var res = SourceText;
        return res;
    }


}public  partial class PartialFieldOrderClass: MessageBase
{

public PartialFieldOrderClass(): base(@"The field: $FieldName$ must be marked with FieldOrder because if you use this attribute in one field you must also use it in all.") {}
 private string mFieldName = null;
 public PartialFieldOrderClass FieldName(string value)
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


}public  partial class SameFieldOrderClass: MessageBase
{

public SameFieldOrderClass(): base(@"The field: $FieldName1$ has the same FieldOrder that: $FieldName2$ you must use different values") {}
 private string mFieldName1 = null;
 public SameFieldOrderClass FieldName1(string value)
{
    mFieldName1 = value;
    return this;
}
 private string mFieldName2 = null;
 public SameFieldOrderClass FieldName2(string value)
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


}public  partial class SameMinMaxLengthForArrayNotLastFieldClass: MessageBase
{

public SameMinMaxLengthForArrayNotLastFieldClass(): base(@"The array field: $FieldName$ must be of a fixed length because is not the last field of the class, i.e. the min and max length of the [FieldArrayLength] attribute must be the same because") {}
 private string mFieldName = null;
 public SameMinMaxLengthForArrayNotLastFieldClass FieldName(string value)
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


}public  partial class StructRecordClassClass: MessageBase
{

public StructRecordClassClass(): base(@"The record type must be a class, and the type: $RecordType$ is a struct.") {}
 private string mRecordType = null;
 public StructRecordClassClass RecordType(string value)
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


}public  partial class TestQuoteClass: MessageBase
{

public TestQuoteClass(): base(@"The Message class also allows to use "" in any part of the "" text "" .") {}
    protected override string GenerateText() 
    {
        var res = SourceText;
        return res;
    }


}public  partial class WrongConverterClass: MessageBase
{

public WrongConverterClass(): base(@"The converter for the field: $FieldName$ returns an object of Type: $ConverterReturnedType$  and the field is of type: $FieldType$") {}
 private string mFieldName = null;
 public WrongConverterClass FieldName(string value)
{
    mFieldName = value;
    return this;
}
 private string mConverterReturnedType = null;
 public WrongConverterClass ConverterReturnedType(string value)
{
    mConverterReturnedType = value;
    return this;
}
 private string mFieldType = null;
 public WrongConverterClass FieldType(string value)
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

