
function readLines(id: string): Array<string> {
    var res = $(id).val().split("\n");
    if (res.length > 0) {
        if (res[res.length - 1] === "") {
            res.pop();
        }
    }

    return res;
}

function setResult(values: Array<string>)
{
    $("#txtResult").val(values.join("\n"));
}

$(document).ready(() => {

    $("#btnClearA").click(event => $("#txtSetA").val("") );
    $("#btnClearB").click(event => $("#txtSetB").val(""));
    $("#btnClearResults").click(event => $("#btnClearResults").val(""));

    $("#btnSortString").click(event => {
        var values = readLines("#txtResult");
        values.sort();

        setResult(values);
    });

    $("#btnSortNumeric").click(event => {
        var values = $("#txtResult").text().split("\n");
        values.sort((a, b) => parseFloat(a) - parseFloat(b) );

        setResult(values);
    });

    var unique = true;
    var trim = false;

    $("#btnUnion").click(event => {
        var duplicateCheck = {};
        var res = new Array<string>();

        var linesA = readLines("#txtSetA");
        var linesB = readLines("#txtSetB");

        for (var lineA of linesA) {

            if (unique) {
                if (duplicateCheck[lineA] === true)
                    continue;
                else
                    duplicateCheck[lineA] = true;
            }

            res.push(lineA);
        }

        for (var lineB of linesB) {
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

    $("#btnIntersect").click(event => {
        var duplicateCheck = {};
        var hashA = {};
        var res = new Array<string>();

        var linesA = readLines("#txtSetA");
        var linesB = readLines("#txtSetB");

        for (var lineA of linesA) {
            if (hashA[lineA] === true)
                continue;

            hashA[lineA] = true;
        }

        for (var lineB of linesB) {
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



    $("#btnANorB").click(event => {
        var duplicateCheck = {};
        var hashB = {};
        var res = new Array<string>();

        var linesA = readLines("#txtSetA");
        var linesB = readLines("#txtSetB");

        for (var lineB of linesB) {
            if (hashB[lineB] === true)
                continue;

            hashB[lineB] = true;
        }

        for (var lineA of linesA) {
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

    
    $("#btnBNorA").click(event => {
        var duplicateCheck = {};
        var hashB = {};
        var res = new Array<string>();

        var linesB = readLines("#txtSetA");
        var linesA = readLines("#txtSetB");

        for (var lineB of linesB) {
            if (hashB[lineB] === true)
                continue;

            hashB[lineB] = true;
        }

        for (var lineA of linesA) {
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
