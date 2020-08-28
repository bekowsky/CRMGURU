




$("#show_info").click(function () {
    
    var name = $("#text_input").val();
    $.ajax({
        type: "POST",
        url: "/Home/InfoCountry",
        data: { "name": name },
        success: successAdd

    });

});

$("#show_all_info").click(function () {
    $.ajax({
        type: "POST",
        url: "/Home/AllCountry",
        success: function (data) {
            $("#items").empty();
            $("#items").append(data);
        }
    });


});


function save_info() {

    var obj = new Object();
    var data = $("#items .data");
    obj.name = data[0].innerText;
    obj.alpha3Code = data[1].innerText;
    obj.region = data[2].innerText;
    obj.capital = data[3].innerText;
    obj.population = data[4].innerText;
    obj.area = data[5].innerText;
   

   
    $.ajax({
        type: "POST",
        url: "/Home/SaveResult",
        data: { "obj": obj },
        success: function () {
            $("#myModal").modal('hide');
            alert("Сохранено");
        }
    });
}

function successAdd(data) {
    $("#items").empty();
    $("#items").append(data);
    if ($("#items").children().length>1)
    $("#myModal").modal('show');
}
