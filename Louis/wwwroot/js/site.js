// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).next("custom-file-label").html(fileName);
    })

    $(".file-upload").on("click", function () {

        var fileInput = document.getElementById('fileInput');
        var file = $("#fileInput").val();

        if (file.length > 0) {
           
            var formData = new FormData();
            formData.append('file', fileInput.files[0]);
            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/MyProducts/ImageUpload');
            xhr.setRequestHeader('Content-type', 'multipart/form-data');

            //Appending file information in Http headers
           // xhr.setRequestHeader('X-File-Name', fileInput.files[0].name);
           // xhr.setRequestHeader('X-File-Type', fileInput.files[0].type);
           // xhr.setRequestHeader('X-File-Size', fileInput.files[0].size);
           // xhr.setRequestHeader['X-File-FileName', 1];
            //xhr.setRequestHeader['X-File-Id', id];

            //Sending file in XMLHttpRequest
            xhr.send(formData);
            xhr.onreadystatechange = function (data) {

                if (xhr.readyState == 4 && xhr.status == 200) {

                    alert("Image Uploaded Successfully...");

                }
            }
        }
    })
});