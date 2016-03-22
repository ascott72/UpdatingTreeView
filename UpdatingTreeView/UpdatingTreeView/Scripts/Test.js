$(document).ready(function () {

    $("#cnp").click(function () {
        var idn= '4';
        var pg = '/TreeViewModels/Edit?id='+idn;
        OpenWindow(pg);
       
    });
    
});

function OpenWindow(page) {
    var $dialog = $('#pop-box')
            .dialog({
                autoOpen: false,
                modal: true,
                width: 600,
                title: "Create"
                             
            })
    $dialog.dialog('open');
    $dialog.load(page);
}