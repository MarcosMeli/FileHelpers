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


}


}internal  partial class TypesOfMessages
{
public  partial class Errors
{
public  partial class EmptyClassNameClass: MessageBase
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

public InvalidIdentifierClass(): base(@"The string '$Identifier$' not is a valid .NET identifier.") {}
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


}


}


}
}

