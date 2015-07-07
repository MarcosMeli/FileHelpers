Dropzone.autoDiscover = false;
$(document).ready(function () {
    //$("#newfield").click(function (event) {
    //    $(record).hide().appendTo("#recordfields").slideDown(500);
    //});
    var record = $("#record1").parent().html();
    var myDropzone = new Dropzone("#fileHelpersDropZone", {
        paramName: "file",
        maxFilesize: 4,
    });
    myDropzone.on("success", function (file, result) {
        var format = result[0];
        var item = $('ul.tabs');
        item.tabs('select_tab', 'records');
        $("#recordfields").html("Format: Delimited" +
            "<br/>Confidence: " + format.Confidence +
            "<br/>Delimiter: '" + format.ClassBuilderAsDelimited.Delimiter + "'");
        var delay = 0;
        var i = 1;
        for (var _i = 0, _a = format.ClassBuilderAsDelimited.Fields; _i < _a.length; _i++) {
            var field = _a[_i];
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
    myDropzone.on("complete", function (file) {
        myDropzone.removeFile(file);
    });
});
//# sourceMappingURL=Wizard.js.map