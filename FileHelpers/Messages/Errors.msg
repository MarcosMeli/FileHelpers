<Messages>

  <FieldOptional>
    The field: $Field$ must be marked as optional because the previous field is marked with FieldOptional. (Try adding [FieldOptional] to $Field$)
  </FieldOptional>

  
  <InvalidIdentifier>
    The string '$Identifier$' not is a valid .NET identifier
  </InvalidIdentifier>

  
  <EmptyClassName>
    The ClassName can't be empty
  </EmptyClassName>

  
  <EmptyFieldName>
    The $Position$th field name can't be empty
  </EmptyFieldName>

  
  <EmptyFieldType>
    The $Position$th field type can't be empty
  </EmptyFieldType>

  
  <ClassWithOutRecordAttribute>
    The record class $ClassName$ must be marked with the [DelimitedRecord] or [FixedLengthRecord] Attribute
  </ClassWithOutRecordAttribute>

  
  <ClassWithOutDefaultConstructor>
    The record class $ClassName$ needs a constructor with no args (public or private)
  </ClassWithOutDefaultConstructor>

  
  <ClassWithOutFields>
    The record class $ClassName$ don't contains any field
  </ClassWithOutFields>

  
  <ExpectingFieldOptional>
    The field: $FieldName$ must be marked as optional because the previous field is marked as optional. (Try adding [FieldOptional] to $FieldName$)
  </ExpectingFieldOptional>

  <SameFieldOrder>
    The field: $FieldName1$ has the same FieldOrder that: $FieldName2$ you must use different values
  </SameFieldOrder>

  <PartialFieldOrder>
    The field: $FieldName$ must be marked with FieldOrder because if you use this attribute in one field you must also use it on all of them.
  </PartialFieldOrder>

  <PartialFieldOrderInAutoProperty>
    The auto property: $PropertyName$ must be marked with FieldOrder because if you use this attribute in one field you must also use it on all of them.
  </PartialFieldOrderInAutoProperty>

  <MissingFieldArrayLenghtInNotLastField>
     The field: $FieldName$ is of an array type and must contain a [FieldArrayLength] attribute because it is not the last field
  </MissingFieldArrayLenghtInNotLastField>


  <SameMinMaxLengthForArrayNotLastField>
    The array field: $FieldName$ must be of a fixed length because it is not the last field of the class, i.e. the min and max length of the [FieldArrayLength] attribute must be the same.
  </SameMinMaxLengthForArrayNotLastField>

  <FieldNotFound>
    The field: $FieldName$ was not found in the class: $ClassName$. Remember that this option is case sensitive
  </FieldNotFound>

  <WrongConverter>
    The converter for the field: $FieldName$ returns an object of Type: $ConverterReturnedType$  and the field is of type: $FieldType$
  </WrongConverter>

  <NullRecordClass>
    The record type can't be null
  </NullRecordClass>

  <StructRecordClass>
    The record type must be a class, and the type: $RecordType$ is a struct.
  </StructRecordClass>

  <TestQuote>
    The Message class also allows to use " in any part of the " text " .
  </TestQuote>

  <MixOfStandardAndAutoPropertiesFields>
    You can mix standard fields and automatic properties only if you use [FieldOrder()] over the fields and properties in the $ClassName$ class.
    </MixOfStandardAndAutoPropertiesFields>
</Messages>