$("#next").click(function () {
    // console.log("run:", $("#num1").text(),".");
    var num1 = parseInt($("#num1").text());
    var num2 = parseInt($("#num2").text());
    var num3 = parseInt($("#num3").text());
    if (num3 == 3) {
        // console.log("run value");
        $("#prviewc").removeClass('disabled');
        $("#prview").attr("aria-disabled", 'false');
    }
    num1++; num2++; num3++;
    $("#num3").text(String(num3));
    $("#num2").text(String(num2));
    $("#num1").text(String(num1));
});
$("#prview").click(function () {
    var num1 = parseInt($("#num1").text());
    var num2 = parseInt($("#num2").text());
    var num3 = parseInt($("#num3").text());
    if (num1 == 1) {
        $("#prviewc").addClass('disabled');
        $("#prview").attr("aria-disabled", 'true');
    }
    else {
        num1--; num2--; num3--;
        $("#num3").text(String(num3));
        $("#num2").text(String(num2));
        $("#num1").text(String(num1));
        if (num1 == 1) {
            ("#prviewc").addClass('disabled');
            $("#prview").attr("aria-disabled", 'true');
        }
    }
});
