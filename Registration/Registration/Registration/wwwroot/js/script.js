$('#fileUpload').change(function ()
{
    var extension = this.files[0].type;
    var ext = extension.replace(/(.*)\//g, '');
    var types = ["jpg", "png"];
    var size = (this.files[0].size / 1024 / 1024).toFixed(2);
    const file = this.files[0];
    if (file) {
        if (size < 0.25 && types.indexOf(ext) >= 0) {
            $('#imagePreview').attr('src', URL.createObjectURL(file));
            $('#featurefilesize').hide();
        }
        else {
            $('#featurefilesize').show();
            $('#imagePreview').attr('src', "/images/icons/attach_icon.svg");
        }
    }
})

$("#StateSelect").on('change', function () {
    debugger
    var optionSelect = $("#StateSelect").val();
    var loadingoption = $('<option></option>').text("Pleas Wait");
    $('#city').attr("disabled", "disabled").empty().append(loadingoption);

    $.ajax({
        dataType: "json",
        url: "/Students/CityJson/",
        data: {
            state: optionSelect
        },
        success: function (response) {
            console.log(response)
            var defaultoption = $('<option value="">Please choose a City</option>');
            $('#citySelect').removeAttr("disabled").empty().append(defaultoption);

            var option2 = '';
            for (i = 0; i < response.length; i++)
            {
                option2 = option2 +  '<option value="' + response[i] + '">' + response[i] + '</option>';
            }
            $("#citySelect").append(option2);
        }
    });
});