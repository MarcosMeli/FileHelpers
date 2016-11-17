function readLines(id) {
    var res = $(id).val().split("\n");
    if (res.length > 0) {
        if (res[res.length - 1] === "") {
            res.pop();
        }
    }
    return res;
}
function setResult(values) {
    $("#txtResult").val(values.join("\n"));
}
$(document).ready(function () {
    $("#btnClearA").click(function (event) { return $("#txtSetA").val(""); });
    $("#btnClearB").click(function (event) { return $("#txtSetB").val(""); });
    $("#btnClearResults").click(function (event) { return $("#btnClearResults").val(""); });
    $("#btnSortString").click(function (event) {
        var values = readLines("#txtResult");
        values.sort();
        setResult(values);
    });
    $("#btnSortNumeric").click(function (event) {
        var values = $("#txtResult").text().split("\n");
        values.sort(function (a, b) { return parseFloat(a) - parseFloat(b); });
        setResult(values);
    });
    var unique = true;
    var trim = false;
    $("#btnUnion").click(function (event) {
        var duplicateCheck = {};
        var res = new Array();
        var linesA = readLines("#txtSetA");
        var linesB = readLines("#txtSetB");
        for (var _i = 0, linesA_1 = linesA; _i < linesA_1.length; _i++) {
            var lineA = linesA_1[_i];
            if (unique) {
                if (duplicateCheck[lineA] === true)
                    continue;
                else
                    duplicateCheck[lineA] = true;
            }
            res.push(lineA);
        }
        for (var _a = 0, linesB_1 = linesB; _a < linesB_1.length; _a++) {
            var lineB = linesB_1[_a];
            if (unique) {
                if (duplicateCheck[lineB] === true)
                    continue;
                else
                    duplicateCheck[lineB] = true;
            }
            res.push(lineB);
        }
        setResult(res);
    });
    $("#btnIntersect").click(function (event) {
        var duplicateCheck = {};
        var hashA = {};
        var res = new Array();
        var linesA = readLines("#txtSetA");
        var linesB = readLines("#txtSetB");
        for (var _i = 0, linesA_2 = linesA; _i < linesA_2.length; _i++) {
            var lineA = linesA_2[_i];
            if (hashA[lineA] === true)
                continue;
            hashA[lineA] = true;
        }
        for (var _a = 0, linesB_2 = linesB; _a < linesB_2.length; _a++) {
            var lineB = linesB_2[_a];
            if (hashA[lineB] === true) {
                if (unique) {
                    if (duplicateCheck[lineB] === true)
                        continue;
                    else
                        duplicateCheck[lineB] = true;
                }
                res.push(lineB);
            }
        }
        setResult(res);
    });
    $("#btnANorB").click(function (event) {
        var duplicateCheck = {};
        var hashB = {};
        var res = new Array();
        var linesA = readLines("#txtSetA");
        var linesB = readLines("#txtSetB");
        for (var _i = 0, linesB_3 = linesB; _i < linesB_3.length; _i++) {
            var lineB = linesB_3[_i];
            if (hashB[lineB] === true)
                continue;
            hashB[lineB] = true;
        }
        for (var _a = 0, linesA_3 = linesA; _a < linesA_3.length; _a++) {
            var lineA = linesA_3[_a];
            if (hashB[lineA] === true) {
                continue;
            }
            if (unique) {
                if (duplicateCheck[lineA] === true)
                    continue;
                else
                    duplicateCheck[lineA] = true;
            }
            res.push(lineA);
        }
        setResult(res);
    });
    $("#btnBNorA").click(function (event) {
        var duplicateCheck = {};
        var hashB = {};
        var res = new Array();
        var linesB = readLines("#txtSetA");
        var linesA = readLines("#txtSetB");
        for (var _i = 0, linesB_4 = linesB; _i < linesB_4.length; _i++) {
            var lineB = linesB_4[_i];
            if (hashB[lineB] === true)
                continue;
            hashB[lineB] = true;
        }
        for (var _a = 0, linesA_4 = linesA; _a < linesA_4.length; _a++) {
            var lineA = linesA_4[_a];
            if (hashB[lineA] === true) {
                continue;
            }
            if (unique) {
                if (duplicateCheck[lineA] === true)
                    continue;
                else
                    duplicateCheck[lineA] = true;
            }
            res.push(lineA);
        }
        setResult(res);
    });
});
//# sourceMappingURL=CompareTools.js.map