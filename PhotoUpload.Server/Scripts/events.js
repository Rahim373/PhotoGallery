$(document).ready(function () {
    // ajax call 
    $(".materialboxed").materialbox();
    loadGallery();
   
});

var loadGallery = function () {
    const getUrl = "http://localhost:34417/api/Photo";
    $.ajax({
        type: "GET",
        url: getUrl,
        success: function (response) {
            var fileList = response.Data;
            var array = [];
            for (var file in fileList) {
                if (fileList.hasOwnProperty(file)) {
                    var data = `<li><div class=" z-depth-1"><img width="650"  id="gridImage" src="${fileList[file]}"  class="materialboxed"/></div></li>`;
                    array.push(data);
                }
            }
            $("#imageGrid").append(array);
        },
        error: function () {
            alert("Error Loading Data");
        }
    });
}

$("#fileSelect")
    .change(function () {
        var loader = $("#load");
        var input = $("input:file");
        var list = $("#uploadList");

        reset(loader);
        list.css("visibility", "visible");


        var data = new FormData(input[0]);

        var files = input.get(0).files;
        for (var i = 0; i < files.length; ++i) {
            var file = files[i];
            if (file.size > 0) {
                data.append("Images", file);
                var name = `<a class="collection-item">${file.name}"</a>`;
                list.append(name);
                console.log(file);
            }
        }


        loader.removeClass("determinate");
        loader.addClass("indeterminate");

        $.ajax({
            url: "http://localhost:34417/api/Photo",
            type: "POST",
            processData: false,
            contentType: false,
            data: data,
            success: function (response) {
                if (response.IsSuccess) {
                    Materialize.toast('Successfull', 3000, 'rounded');
                    full(loader);
                   // var image = `<li><div class="hoverable"><img  id="selection" src="${fileList[file]}"  class="grid-image"/></div></li>`;

                    $("#imageGrid").empty();
                    loadGallery();
                } else {
                    Materialize.toast(response.Message, 3000, 'rounded');
                    reset(loader);
                }
            },
            error: function (er) {
                Materialize.toast(e.message, 3000, 'rounded');
                reset(loader);
            }
        });
    });

var reset = function (loader) {
    loader.removeClass("indeterminate");
    loader.addClass("determinate");
    loader.css("width", "0%");
}

var full = function (loader) {
    loader.removeClass("indeterminate");
    loader.addClass("determinate");
    loader.css("width", "100%");
}