---
layout: default
title: Version History
permalink: /changelog/
---

###Changes in the library

-   [1.0 (10-Oct-2005)](javascript:unhide('v100');)
    -   Developed for the [Larkware Contest](http://www.larkware.com)
        and [Code
        Project](http://www.codeproject.com/KB/database/filehelpers.aspx)
        site.

-   [1.1.0 (11-Nov-2005)](javascript:unhide('v110');)
    -   MOD: The Decimal, Double and Single converters now are
        cultureless and discard all the blanks before converting the
        number to allow (" - 10.3", " - 13. ", etc)
    -   MOD: The default encoding of the FileHelperEngine was changed
        from UTF8 to Default (ANSI).
    -   ADD: FileHelperEngine.AppendToFile Method.(thanks Pierre)
    -   ADD: IgnoreFirstAttribute to discard the n first lines of the
        files. (thanks Sam)
    -   ADD: QuotedAttribute to mark the quoted fields in the files.
        (thanks jyjohnson)
    -   ADD: A lot of more NUnit tests to check the new and old
        features.
    -   ADD: .NET Compact Framework Support (thanks Pierre)

-   [1.1.4 (26-Nov-2005)](javascript:unhide('v114');)
    -   FIX: DataLink engine fail for File -\> Access option.
    -   FIX: CodeSmith templates generate bad insert statement.
    -   MOD: Documentation of DataLinks
    -   MOD: Enhanced Quoted String Support (now can discard blanks when
        trimming is enabled)
    -   MOD: Refactored code aims to simplicity.
    -   ADD: More and more NUnit tests.
    -   ADD: A better Demo Program for the Lib (for now only Easy and
        DataLink Examples)
    -   ADD: NAnt build file, for clean, build, test and validate.

-   [1.2.0 (7-Dec-2005)](javascript:unhide('v120');)
    -   FIX: BeginReadFile now is Encoding Depending.
    -   MOD: The Decimal, Double and Single converters are more smart to
        discard blanks. (thanks Ivan)
    -   MOD: More complete NAnt build file.
    -   MOD: Enhanced Documentation Browsing.
    -   MOD: A lot of name Refactoring to use the new naming conventions
        -   FixedLengthAttribute -\> FieldFixedLengthAttribute
        -   DelimitedAttribute -\> FieldDelimiterAttribute
        -   ConverterAttribute -\> FieldConverterAttribute
        -   FieldAlign -\> AlignMode
        -   AlignAttribute -\> FieldAlignAttribute
        -   TrimAttribute -\> FieldTrimAttribute
        -   NullValueAttribute -\> FieldNullValueAttribute
        -   QuotedAttribute -\> FieldQuotedAttribute

    -   ADD: New form in the demo for time and stress tests.
    -   ADD: New form in the demo for fixed length tests.
    -   ADD: More NUnit tests. (now are 90+)
    -   ADD: Require a custom converter for non system types.
    -   ADD: CustomConverter example in the docs (see advanced example).

-   [1.3.0 RC1 (27-Dec-2005)](javascript:unhide('v130');)
    -   FIX: BeginReadFile now uses the IgnoreFirst(n) attribute.
    -   FIX: Documentation examples (bad syntax and names)
    -   FIX: Documentation browsing (some see also topics was bad)
    -   MOD: A lot of name refactoring in the DataLinksto get the names
        shorts.
        -   IDataLinkProvider -\> IDataStorage
        -   DataBaseDataLinkProvider -\> DataBaseStorage
        -   SqlServerDataLinkProvider -\> SqlServerStorage
        -   AccessDataLinkProvider -\> AccessStorage

    -   MOD: DataLinkEngine now is GenericDataLink and does not need to
        be from/to file you can use all the implementations of the
        IDataStorage for example SqlServerStorage \<--\>ExcelStorage
    -   MOD: A **big** change in this revision is the split in the
        [FileHelperEngine](FileHelpers.FileHelperEngine.html) that now
        is only for whole operations like ReadFile, WriteFile, etc. and
        the
        [FileHelperAsyncEngine](FileHelpers.FileHelperAsyncEngine.html)
        that have the Async operations BeginReadFile, ReadNext, etc.
    -   MOD: Now the library has a decent NAnt build file that generates
        the zips for the distribution, run the test, generate the
        documentation (inserting the additional content)
    -   ADD: The FileDataLink with a similar functionality of the old
        DataLinkEngine with one IDataStorage and the operations to
        ExtractToFile and InsertFromFile.
    -   ADD: FileTransformDataLink to convert records of the type1 in
        one file to records of type2 in the destination file. (ideal for
        Delimited to Fixed Length record conversions).
    -   ADD: More examples in the SDK documentation.
    -   ADD: New ExcelStorage in a new assembly to avoid the import of
        the interop assemblies when you don't use it.
    -   ADD: You can get the
        [HeaderText](FileHelpers.EngineBase.HeaderText.html) of the
        IgnoreFristAttribute or set it before write a file to put it in
        the destination file.
    -   ADD: The EndsRead and EndsWrite to ensure that the streams are
        closed and disposed after use.
    -   ADD: More [NUnit tests](testing.html) (now 100+)

-   [1.3.1 (12-Jan-2006)](javascript:unhide('v131');)
    -   FIX: ExcelStorage: Fail when the number of columns in the
        worksheet is 1.
    -   FIX: BeginReadFile now uses the IgnoreFirst(n) attribute.
    -   DEL: FileHelpers.Convertion namespace (it has only one class
        "ConverterBase" that now is in the root namespace)
    -   MOD: ExcelStorage is now in advanced beta (only Date types don?
        work)
    -   MOD: A [Tips for VB.NET](vb_tips.html) section in the docs.
    -   MOD: Internal refactoring to improve the design.
    -   ADD: The WriteFile and WriteStream methods now have an optional
        third parameter with the maximum number of records to write.
    -   ADD: The ErrorManager class to handle the errors not only in the
        filehelper engine, but also in the DataStorage.
    -   ADD: FieldIgnored to exclude the specified Field from the
        engine.
    -   ADD: A navigator bar to the CHM Help and home site.
    -   ADD: A search option based on the Google site search (another
        goodie from google =)
    -   ADD: More examples in the SDK help.
    -   ADD: The ReadString and WriteString methods to the
        FileHelperEngine.
    -   ADD: The WriteFile, WriteStream and WriteString have an optional
        parameter with the maximum number of records to write.
    -   ADD: RecordInfo class that holds all the properties of the
        records, including the converters, with this the DataStorage can
        browse the properties of each field in a nice way.
    -   ADD: More [NUnit tests](testing.html) (now 122)

-   [1.3.2 (28-Jan-2006)](javascript:unhide('v132');)
    -   FIX: FieldIgnore: Problems when used in the last field in the
        record class.
    -   FIX: ExcelStorage.SheetName: didn't work, now yes and catch the
        sheet don't exists error.
    -   DEL: ColumnNumber removed from the Engine and the ErrorInfo
        (didn't work anyway)
    -   MOD: Refactor some internal classes to catch better the
        exceptions and clearer error messages.
    -   MOD: Some specific code reported by FxCop as problematic.
    -   ADD: WriteNexts and ReadNexts to the FileHelperAsyncEngine to
        process more than one record at once.
    -   ADD: A lot of [contributors](credits.html) this weeks I receive
        a lot of congrats and great ideas !! thanks to all of you.
    -   ADD: A DeveloperStuff directory in the distribution with links
        to the SVN, Trac, Mailing list, used tools, etc.
    -   ADD: More [NUnit tests](testing.html) (now 137)

-   [1.3.5 (14-Feb-2006)](javascript:unhide('v135');)
    -   ADD: This Release has **six new features**:
        -   [FileHelpers Wizard](wizard.html) - An Simple UI to generate
            the record classes (RC1)
        -   [.NET 2.0 Generics Support](generics.html) - A strong typed
            and cast less option.
        -   [](generics.html)[](generics.html)[Master Detail
            Engine](FileHelpers.MasterDetail.MasterDetailEngine.html) (
            see the [example1](example_masterdetail.html) and
            [example2](example_masterdetail.html) )
        -   [IgnoreLastAttribute](attributes.html) to discard the last
            **n** lines of a file or stream
        -   [EnumConverter](example_enumconverter.html) to auto parse
            and write enum values in the fields.
        -   Full VB.NET Support: solved the previous versions issues.

    -   ADD. Four new [examples](examples.html) !!
    -   ADD: Some new [class diagrams](class_diagram.html) for the
        [Engines](diagram_engines.html),
        [DataStorage](diagram_datastorage.html) and
        [DataLinks](diagram_datalinks.html)
    -   ADD: FooterText: you can read or read this string when you use
        the IgnoreLastAttribute.
    -   ADD: More [contributors](credits.html) for which I receive a lot
        of congrats and great ideas !! thanks to all again.
    -   ADD: More [NUnit tests](testing.html) (now 156 =)
    -   FIX: TotalRecords: report bad values when the file has errors or
        when the first lines are ignored.
    -   MOD: The optional parameters of the converters now are only
        strings to provide full vb.net compatibility.
    -   MOD: Refactor... a lot of refactoring to support the IgnoreLast,
        forward reading and others enhancements.

-   [1.4.0 (17-Mar-2006)](javascript:unhide('v140');)
    -   Major changes:
        -   DEL: ConverterKind.Custom was removed because it was
            irrelevant to use custom converters now the library uses
            [FieldConverter(typeof(YouConverter))]
        -   DEL: Some Exceptions trying to simplify the Exception
            handling and reducing the number of different exceptions.
            The deleted exceptions are **NullValueException,
            QuotedStringException and InternalException all replaced
            with the BadUsageException**.
        -   FIX: The Async engines now uses the ErrorMode in all cases.
        -   MOD: The MasterDetailEngine now takes the IgnoreFirst and
            IgnoreLast options from the master record an not from the
            detail.
        -   MOD: All methods in the FileHelpers Engines that **returned
            bool changed to void** because they was using error handling
            with exceptions and a bool result is redundant and useless,
            this provides a clearer OO design.
        -   MOD: [LoadErrors](example_errorhandling.html) from the
            ErrorManager class now is static.
        -   ADD: [The CommonEngine](example_commonengine.html): with
            four static methods for the common operations.
        -   ADD: [The progress notification
            feature](example_progress.html), with it you can get
            notified of the progress of each operation.
        -   ADD: Full ExcelCSV support, a new parameter to the
            FieldQuoted attribute to indicate if the Quote is optional
            (like excel does with CSV)
        -   NEWS: Full Subversion support in Sourceforge: now you can
            help in the development using any subversion client and send
            me the patches with your changes.
            [https://svn.sourceforge.net/svnroot/filehelpers/trunk](https://svn.sourceforge.net/svnroot/filehelpers/trunk)
        -   NEWS: [FileHelpers
            Forums](http://www.filehelpers.com/forums/) the main please
            for your feedback and discussions about the
            library.[](https://svn.sourceforge.net/svnroot/filehelpers/trunk)

    -   Minor changes:
        -   DEL: Description property to the FieldBase this is really
            old (from the beginning of the library.)
        -   DEL: Demo zip file now is merged into the binary zip.
        -   MOD: EngineBase.LineNum and ErrorInfo.LineNum now are
            **LineNumber**.
        -   MOD: Enhanced exception messages.
        -   MOD: SaveErrors can receive a header string to write into
            the file.
        -   MOD: ErrorManager.**LastErrors renamed
            to**ErrorManager.**Errors**
        -   ADD: The ExcelStorage checks if Excel is installed, if not
            throws a BadUsageException
        -   ADD: More [NUnit tests](testing.html) (more than 185)
        -   ADD: A lot of new [Usage Examples](examples.html).
        -   ADD: A new form in the demo app that shows how to use the
            [ProgressNotification](example_progress.html).
        -   ADD: Wizard: now use the [FireBall
            CodeEditor](http://www.codeproject.com/KB/miscctrl/fireballcodeeditor.aspx)
            for syntax highlighting.
        -   ADD: Wizard: a lot of new templates and fixes in others.

-   [1.5.0 (Jun-2006)](javascript:unhide('v150');)

    There have been some delays but the library is always moving forward
    thanks to hard work and the help from all of you.

    **Breaking changes:**

    -   Both DataStorage types: SqlServerStorage and AccessStorage
        completely changed, from now you don't need to override this
        classes (in fact you can't because they are sealed)  
         You can use these classes as client and provide the callbacks
        for the Insert and Fill record methods.([check the
        examples](examples.html))
    -   The FieldQuoted attribute was completely rewritten to support
        optional quote for read, write or both.  
         So you need to use now the QuoteMode enum and not only a
        boolean. (For Excel generated CSV files you must use the
        QuoteMode.OptionalForBoth value) ([check the
        examples](examples.html))
    -   MOD: DataBaseStorage to DatabaseStorage (Case change)
    -   MOD: Extract and Insert Records now returns the inserted or
        extracted records

    **Major changes:**

    -   ADD: New **FieldOptional** that allows that the n last fields of
        a record be optional.
    -   ADD: Multiline Quoted Field support (with this we finish the
        support for Excel generated CSV files)
    -   ADD: Multiline Records supports via the FieldInNewLine attribute
        to allow records to span multiple lines
    -   ADD: OleDbStorage thanks to Permjeet Pandha to use with any Data
        Storage that supports OleDb access.
    -   ADD: The ReadMethods of the FileHelperEngine can return the data
        as a **DataTable** the suffix of this methods are **AsDT**. That
        is perfect to load sample data into your NUnit test (examples
        coming soon)
    -   ADD: New **IgnoreEmptyLines** that allows you to mark a record
        with it to ignore empty lines in the source data.
    -   ADD: CommonEngine.**SortFile**and**SortRecords**by field or
        using the IComparable interface if the records implement it.
    -   ADD: New demo code in a Console Application project to make easy
        to newbies play with the library. ([download it from the
        examples](examples.html))
    -   MOD: A lot of refactoring in the source code and changes in the
        folder layout.
    -   MOD: Code in the Docs now highlighted with the excellent
        [dp.SyntaxHighlighter](http://www.dreamprojections.com/syntaxhighlighter/?ref=filehelpers)
        from Alex Gorbatchev

    **Minor changes:**

    -   FIX: The AsyncEngine could close the File in the BeginWriteFile
        under some conditions.
    -   FIX: IO Exception when appending to an empty file.
    -   FIX: Syntax in some samples and in the Wizard Templates.
    -   FIX: The notify progress in the DatabaseStorage working fine and
        tested.
    -   FIX: A little issue in the DatabaseStorage that can throws
        InvalidaCastException in the constructor.
    -   DEL: A lot of properties in the internal classes were deleted
        (aiming to reduce the dll size and library performance, much
        better for Compact Framework)
    -   DEL: The obsolete LastErrors of the ErrorManager. Now it is
        mandatory to use Errors.
    -   ADD: More and enhanced [NUnit tests](testing.html) (more than
        260) based in the coverage of clover.net
    -   ADD: Some new and corrected [Usage examples](examples.html)
    -   ADD: Now the wizard supports FieldOptional and IgnoreEmptyLines,
        also you can generate the properties that map to the fields
        (useful for Data Binding the RecordClass to any binding aware
        control in VS2005)
    -   ADD: Encoding support for source and destination in the
        transformation engine.
    -   ADD: A new feature for the custom converters to allow them to
        handle the null values
    -   ADD: More features in the CommonEngine: TransformFile and the
        fast TransformFileAsync
    -   ADD: two easy methods to export/import data to/from a file.
    -   MOD: Wizard: now use the Beta2 of the excellent [FireBall
        CodeEditor](http://www.codeproject.com/KB/miscctrl/fireballcodeeditor.aspx).
    -   MOD: A lot of documentation corrections and additions
    -   MOD: Demo application rewritten to show some new features and
        show the syntax changes in the old ones.

-   [1.6.0 (Aug-2006)](javascript:unhide('v160');)

    The library has 10 months now and a lot of things were added. So I
    want to thank all the people who send their feedback, posted
    motivating entries on the web and a lot of other ones that help me
    in the development. ([partial list of contributors)](credits.html)

    Now the important things !!... [**RunTime
    Records**](runtime_classes.html) are here thanks to some help
    Gerhardt Scriven to use RunTime Compilation. Enjoy it !!

    **I think in this version as 2.0** because the RunTime Records
    functionalities were one of the most requested features and also one
    of the hardest to implement features. A lot of effort was put into
    usability and a clear API, if you have any suggestion please post it
    in the forums =)  
     Other important news are the
    [CsvEngine](FileHelpers.CsvEngine.html) (for generic CSV files), the
    [FileDiffEngine](example_filediffengine.html), the
    [MultiRecordEngine](example_multirecords.html),..

    **Breaking changes:**

    -   MOD: **Now the FixedLengthRecords must contain the exact number
        of characters**. You can pass an extra parameter in the
        [FixedLengthRecord] attribute to allow a different behavior.
    -   MOD: CommonActions renamed to CommonSelector in the
        MasterDetailEngine

    **Major changes:**

    -   ADD: The awaited RunTime Records thanks to the suggestion of
        Gerhardt Scriven to use RunTime Compilation. Supports for
        classes written in C\# and VB.NET  
         There are a lot of features inside this new feature, [so take a
        look at the docs](runtime_classes.html)
    -   MOD: The **Record wizard** was completely rewritten to use the
        run time records and now has a lot of options to check the
        classes that you are building, in the future the wizard could
        help you discovering the record class based on a sample file
    -   ADD: Generic CSV files are now supported (for example Excel
        generated ones)  
         Was a hard work but worth it because it can be used with any
        engine.   
         In the next version I'll add the Excellent [Fast CSV
        Reader](http://www.codeproject.com/KB/database/CsvReader.aspx)
        from **S?stien Lorion** for standalone CSV processing.
    -   ADD: [Event support](events.html) !! From this version the
        FileHelperEngine has 4 events, 2 for read operations and 2 for
        write that are thrown before and after process each record.
    -   ADD: FileDiffEngine to compare files with the same record
        layout.
    -   ADD: MultiRecordEngine to read files with different record
        layouts.
    -   ADD: DataTableToCsv, CsvToDataTable, RecordsToDataTable,
        RemoveDuplicateRecords and other methods to the CommonEngine
    -   ADD: FixedMode Enum for fixed length records, used to provide
        better validation in fixed length files
    -   ADD: Template support in ExcelStorage
    -   ADD: [A lot of new examples check it out !!](examples.html)
    -   MOD: The decimal, double, and single converters now receive an
        string param to set the decimal delimiter (by default is ".")
    -   MOD: Improved documentation in the SDK, the general help and
        examples. (Thanks Antoine)

    **Minor changes:**

    -   FIX: FieldDelimiter doesn't work very well after the refactoring
        of the previous version.
    -   FIX: Bug when copy or saving to file in the wizard
    -   DEL: more and more properties of the internal classes, this
        enhances both the performance and the dll size.
    -   MOD: A lot of internal refactoring and enhancements
    -   MOD: ExcelStorage now returns the cells values when using
        ErrorMode.SaveAndContinue
    -   ADD: [These excellent **VB.NET - C\# comparison cheat
        sheets**](http://blog.krisvandermast.com/VBNETCComparisonCheatSheets.aspx)
        (to avoid translating the examples)
    -   ADD: RecordsToDataTable in the CommonEngine
    -   ADD: A lot of options to CommonSelector like: MasterIfBegins,
        MasterIfEnds, MasterIfEnclosed and the details counterparts
    -   ADD: BooleanConverter can receive two arguments for the true or
        false values
    -   ADD: New constructor to the SqlServerStorage to directly pass
        the ConnectionString. Thanks to Anatoly Kleyman
    -   ADD: Again a lot of new [NUnit tests](testing.html) near the 320
        (wow I never thought of writing that many)
    -   ADD: [Library statistics](statistics.html) page.
    -   ADD: [**How to help me with the development of the
        Library**](howhelp.html) page.

-   [2.0 (April-2007)](javascript:unhide('v200');)

    The library has passed its 1st birthday !!! and thanks to the work
    and contribution of a lot of people ([partial list of
    them)](credits.html)

    Too many things were changed, mostly internally, now we have a
    performance gain of more than 60% in .NET 2.0

    I will be releasing the version for .NET 1.1 because I know that a
    lot of people use it in some corporations, but maybe in the next
    releases the new features will be .net 2.0 only. It's a massive job
    maintain and optimize both =( versions

    There are also a lot of major changes, refactoring in the code, and
    a big core rewrite that allow the engines to use less memory and
    temporary string (we are using more buffers to avoid this)

    We were very busy to keep to library updated and to give support to
    the users. Check the forums for any problems as it's likely they
    have been addressed here.

    We have our own domain www.filehelpers.com thanks to (Antoine) and a
    DevBlog at http://blog.filehelpers.com

    Here do you have all the stuff that keep me busy for a while :P

    -   Breaking Changes
        -   The constructor of the MultiRecordEngine was changed to
            allow params args, and to allow users to not pass the
            RecordSelector (in write operations where it was not
            needed).
        -   EndsRead and EndsWrite deleted, now you have a Close()
            operation to simplify the API and to make it similar to the
            System.Data namespace. (The async engines also implement
            IDisposable)
        -   Rename FileHelperException --\> FileHelpers Exception a
            better name =) and an easy to solve problem
        -   By default the numeric fields in the FixedLengthRecords that
            don't have an [FieldAlign] will be aligned to the right and
            the rest the left by default, with this we avoid the problem
            of generate files with different meaning when reading and
            writing

    -   Performance related
        -   Internal use of Reflection.Emit, Dynamic Methods and char
            buffers to get **more than 50% enhancement in performance
            and for .NET 2.0 more than 65%**
        -   The Read..AsDT methods now creates the DataTable record by
            record, not at the end, so you can handle large files
            without any memory overload
        -   No more reflection in the operations (only in the
            constructors).
        -   FieldSorter faster too, Removed REFLECTION for EMIT

    -   Shining new features
        -   The very cool
            [DelimitedFileEngine](example_delimitedengine.html) and
            [FixedFileEngine](example_fixedengine.html) that allow to
            change options at RunTime (also with generic versions)
            [Check an example here](example_generics.html)
        -   [ConditionalRecords](example_conditionalrecords.html) you
            can easily include or exclude certain records based on a
            RecordCondition (like BeginsWith, EndsWith, Contains or
            RegEx) [Check the example](example_conditionalrecords.html)
        -   A new GenericDatabaseStorage to allow databases use ADO.NET
            to work with the FileHelpers (Thanks Rodolfo Finochietti)
        -   Experimental [Mono project](http://www.mono-project.com)
            Support (some users are already using it, we hope that in
            the next release we have a completely working version for
            this framework)
        -   New version checker in the demos and wizard (both with a
            renewed look and feel) [Check the
            movies](http://www.filehelpers.com/movies.html)
        -   .NET 2.0 Nullable Types support. (Thanks Vijayan) [Check the
            example](example_nullable.html)
        -   Notification Interfaces: you can now get notified of the
            events simply implement them
        -   Enhanced Debugging in .NET 2.0 with DebuggerDisplay and
            DebuggerBrowsable, DebugVisualizars to come.
        -   The wizard has a record class test that allows you to
            introduce sample data and your class and check for errors or
            results.
        -   You can [check our
            screencasts](http://www.filehelpers.com/movies.html) for the
            demos, wizard and other features of the library.
        -   Now the code for Vs2003 and Vs2005 are both on SVN, sharing
            the same files and the build script autodetect your Visual
            Studio installation. So no more excuses time to get involved
            =)
        -   The engines with asynchronous operations are now IDisposable
            and IEnumerable so you can declare it with using(..) and use
            them in a foreach loop

    -   API changes and extensions
        -   The write methods are less strict. You can pass now an
            IEnumerable instead of an array, so you can directly pass a
            List or ArrayList to the method without doing a ToArray()
        -   RunTime records can now get a DataTable in the constructor
            and use DataColumns to create one field for each column.
        -   Generic versions of near all the engines of the library (it
            is really hard to maintain the two copies :P so everyone it
            is time to port !!)
        -   Asynchronous operations in others engines like the
            MultiRecordEngine
        -   Event support to the MultiRecordEngine thanks to the
            contribution of Francis de Fouchier
        -   The library now handles infinite levels of inheritance
            adding the high level fields first in the result record
            class.
        -   Allow to ignore spaces and tabs in the
            [IgnoreEmptyLinesAttribute](FileHelpers.IgnoreEmptyLinesAttribute.html)
        -   [IgnoreCommentedLines](FileHelpers.IgnoreCommentedLinesAttribute.html)
            thanks to MCampbell
        -   The Read methods now have a maxRecords args to tell to the
            engines how much them must read.
        -   A new constructor overload to the engine that allow to pass
            the Encoding. This is useful to make the users aware of the
            Encoding feature.
        -   FileTransformEngine with two new methods
            ReadAndTransformRecords and TransformRecords
        -   Flush method to AsyncEngines to allow users to ensure data
            is written. For example, if they are using the library for
            logging.
        -   The converters are now smarter as they validate to which
            types can be assigned and throw exceptions if converter is
            wrong (for example if you use ConverterKind.Decimal in a int
            field)
        -   The [ConvertException](FileHelpers.ConvertException.html)
            has a lot of context information: LineNumber, ColumnNumber,
            FieldName. Also better exception messages.
        -   The integer converters receive now take a decimal separator
            to build an InvariantCulture to format and parse the values
            and strings

    -   Small changes
        -   Fixed the problem with ASP.NET: "Line 0: Metadata file
            'filehelpers.dll' could not be found"
        -   Fixed some problems with the CsvClassBuilder (it is a good
            practice to not to release the last minute added features)
        -   The assemblies are now signed
        -   The new ConverterBase.DefaultDateTimeFormat to avoid set the
            converter format field by field =)
        -   Some error messages from the library were rewritten and now
            provides more contextual information (line and column
            numbers, field name, etc).
        -   ExcelStorage supports more than 26 columns (thanks to Mark
            Izendooren)
        -   Encoding support to the CsvEngine
        -   The wizard remembers all the paths now
        -   SkipThisRecord in the AfterReadRecordEventArgs, this allows
            engine to skip records from the results (thanks Crestline)
        -   More methods for the
            [CommonEngine](FileHelpers.CommonEngineMembers.html)
        -   More than [420+ NUnit tests](testing.html) !!! (I cant
            believe it, I'm a lazy developer but I know how much value
            you get out of testing the library)
        -   You now have two versions for the demos, one for each
            framework version.
        -   The FixedLengthClassBuilder has more constructors to set the
            length of the fields in your instruction
        -   Improved documentation. (Thanks Antoine and Matt)
        -   [A lot of new examples check it out !!](examples.html)

-   [3.1 (July-2014)](javascript:unhide('v300');)

    Hi everyone again !!

    The FileHelpers has reached a big Milestone today  
     we are releasing the **3.1 version!**  
     With full support for **.NET 4.0 and 4.5** and with a ton of news
    and enhancements  
      
     After some time off for differents reasons we are now trying to
    update the library in a more regular fashion :)

    -   **Main changes**
        -   The core code of the library was refactored to make easier
            to extend it
        -   Better performance, mostly for FixedLenght records
        -   The error messages have been made more meaningful. In all
            exceptionsyou get; the FieldName, ColumnNumber, LineNumber,
            etc.
        -   Docs rewritten thanks to the work of Ken Foskey
        -   CHANGED: Removed support for .NET Compact Framework
        -   CHANGED: Better caching of internal classes
        -   NEW: Full .NET 2.0, 4.0 & 4.5 support
        -   NEW: FieldOrder: a new attribute to override the default
            order of fields
        -   NEW: FieldNotEmpty : Indicates whether the target field
            needs to be populated with a non-empty value.
        -   NEW: Partial support to use Autoproperties
        -   NEW: Async Methods returns IDisposable to allow using()
            statements that autoclose files on ends or exception
        -   NEW: ReadFileAsList: new methods to directly get a List
            instead of an array
        -   NEW: Events for the Async engines
        -   NEW: The events args now have TotalBytes and CurrentBytes to
            allow you show progress while reading or writting
        -   NEW: FieldIndexers you can now get the values in the
            AsyncEngine like in the DataReader of ADO.NET =) You can
            access them via index or fieldName (at the moment case
            sensitive)  
             This also completes the RunTime records support
        -   NEW: FormatDetector: A class designed to discover the format
            of delimited and flat files based on sample files. It is
            integrated with the wizzard
        -   NEW: Better Delited validations to make sure that the file
            contains the right number of fields
        -   NEW: FieldValueDiscarted: a new attribute to allow the value
            of a particular field to be read but not stored in the
            record class (this was a very requested feature)
        -   NEW: BigFileSorter: if you need to sort big files, you can
            use this feature that implements External Sorting

    -   **Breaking Changes**
        -   

    -   **API changes and extensions**
        -   

    -   **Minor Changes**
        -   ADD: UpdateLinks properties, allow to specify how the
            library must handle Workbook links (thanks to Stefan Sch?e)
        -   [Some new examples.](examples.html)
        -   FIX: A little problem with the quoted fields in the wizard
        -   More documentation has been added to the Library source
            code. It is now even easier to get involved in development!
        -   Lots of minor documentation updates