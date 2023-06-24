AddCrudButtons = function (data, buttons) {
    let htmlOutput = "";
    buttons.forEach(b => {
        htmlOutput += '<a class="px-1" href="' + b.uriBase + '/' + data + '"><i class="lead fas fa-' + b.icon + '"></i></a>'
    });
    return htmlOutput;
}

CopyTextFromElement = function (elementId) {
    navigator.clipboard.writeText($(elementId).text());
}

LaunchSubProc = function (apiUrl, data) {
    $.ajax({
        url: apiUrl,
        data: data,
        success: function (data) {
            alert('Launched successfully: ' + data.message);
            location.reload();
        },
        error: function (xhr, status, error) {
            alert(xhr.responseJSON.message
                + '\nLaunch failed with status: ' + xhr.status);
            location.reload();
        }
    });
}
