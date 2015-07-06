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
        for (var _i = 0; _i < linesA.length; _i++) {
            var lineA = linesA[_i];
            if (unique) {
                if (duplicateCheck[lineA] === true)
                    continue;
                else
                    duplicateCheck[lineA] = true;
            }
            res.push(lineA);
        }
        for (var _a = 0; _a < linesB.length; _a++) {
            var lineB = linesB[_a];
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
        for (var _i = 0; _i < linesA.length; _i++) {
            var lineA = linesA[_i];
            if (hashA[lineA] === true)
                continue;
            hashA[lineA] = true;
        }
        for (var _a = 0; _a < linesB.length; _a++) {
            var lineB = linesB[_a];
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
        for (var _i = 0; _i < linesB.length; _i++) {
            var lineB = linesB[_i];
            if (hashB[lineB] === true)
                continue;
            hashB[lineB] = true;
        }
        for (var _a = 0; _a < linesA.length; _a++) {
            var lineA = linesA[_a];
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
        for (var _i = 0; _i < linesB.length; _i++) {
            var lineB = linesB[_i];
            if (hashB[lineB] === true)
                continue;
            hashB[lineB] = true;
        }
        for (var _a = 0; _a < linesA.length; _a++) {
            var lineA = linesA[_a];
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