$(document).ready(function () {
    $('#chkViewAll').change(function () {
        ViewAllTickets();
    });
});

var ViewAllTickets = function () {    
    if ($('#chkViewAll').is(':checked')) {
        url = $('#lnkShowAll').val();
        window.location.href = url;
    }
    else
    {
        url = $('#lnkShowClosed').val();
        window.location.href = url;
    }
}
