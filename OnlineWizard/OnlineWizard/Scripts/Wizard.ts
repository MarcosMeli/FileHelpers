
Dropzone.autoDiscover = false;

$(document).ready(() => {
   
    //$("#newfield").click(function (event) {
    //    $(record).hide().appendTo("#recordfields").slideDown(500);
    //});

    var record = $("#record1").parent().html();
    

    var myDropzone = new Dropzone("#fileHelpersDropZone",
    {
        paramName: "file", // The name that will be used to transfer the file
        maxFilesize: 4, // MB
       // acceptedFiles: "*.txt;*.csv;*.tab"
    });

    myDropzone.on("success", (file, result) => {

        var format:Models.RecordFormatInfo = result[0];

        var item: any = $('ul.tabs');
        item.tabs('select_tab', 'records');

        $("#recordfields").html("Format: Delimited" +
            "<br/>Confidence: " + format.Confidence +
            "<br/>Delimiter: '" + format.ClassBuilderAsDelimited.Delimiter + "'");
        

        var delay = 0;
        var i = 1;
        for (var field of format.ClassBuilderAsDelimited.Fields) {
            var newRecord = $(record);
            var fieldName = newRecord.find("#fieldName");
            fieldName.val(field.FieldName);
            fieldName.attr("id", "fieldName" + i);
            fieldName.next().attr("for", "fieldName" + i);
            newRecord.appendTo("#recordfields");
            newRecord.delay(delay).slideDown(200);
            delay += 150;
            i++;
        }

        $(".advanced").click(function (event) {
            var toRemove = $(this).parent().parent().parent();
            toRemove.slideUp(600, function () { $(this).remove(); });
        });


    });

    myDropzone.on("complete", file => {
        myDropzone.removeFile(file);
    });
    
    
});

declare module Models {

    export interface Converter {
        Kind: number;
        TypeName: string;
        Arg1: string;
        Arg2: string;
        Arg3: string;
    }

    export interface Field {
        FieldQuoted: boolean;
        QuoteChar: string;
        QuoteMode: number;
        QuoteMultiline: number;
        TrimMode: number;
        TrimChars: string;
        FieldIndex: number;
        FieldInNewLine: boolean;
        FieldNotInFile: boolean;
        FieldIgnored: boolean;
        FieldValueDiscarded: boolean;
        FieldOptional: boolean;
        Converter: Converter;
        FieldName: string;
        FieldType: string;
        FieldNullValue?: any;
        FieldNotEmpty: boolean;
        Visibility: number;
    }
    


    export interface ClassBuilder {
        Delimiter: string;
        Fields: Field[];
        FieldCount: number;
        ClassName: string;
        IgnoreFirstLines: number;
        IgnoreLastLines: number;
        IgnoreEmptyLines: boolean;
        GenerateProperties: boolean;
        CommentText?: any;
        Visibility: number;
        SealedClass: boolean;
        Namespace: string;
    }
    
    export interface ClassBuilderAsDelimited {
        Delimiter: string;
        Fields: Field[];
        FieldCount: number;
        ClassName: string;
        IgnoreFirstLines: number;
        IgnoreLastLines: number;
        IgnoreEmptyLines: boolean;
        GenerateProperties: boolean;
        CommentText?: any;
        Visibility: number;
        SealedClass: boolean;
        Namespace: string;
    }

    export interface RecordFormatInfo {
        Confidence: number;
        ClassBuilder: ClassBuilder;
        ClassBuilderAsFixed?: any;
        ClassBuilderAsDelimited: ClassBuilderAsDelimited;
    }

}

