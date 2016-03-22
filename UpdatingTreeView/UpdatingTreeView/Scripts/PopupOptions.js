var $dialog1;
$(document).ready(function () {
    //inital data load
    LoadTree();

    //User Clicks Button Create view is loaded in dialog popup  
    $("div").on("click", "#cnp", function () {
        var page = '/TreeViewModels/Create';
        OpenWindow(page, "Create New Parent");
    });

    // popup content options after li node is left clicked

    $.contextMenu({
        selector: '.context-menu-one',
        trigger: 'left',
        items: {
            "edit": {
                name: "Edit",
                icon: "edit",
                // superseeds "global" callback
                callback: function (key, options) {
                    var idn = $(this).attr("id");
                    var page = '/TreeViewModels/Edit?id='+idn;
                    OpenWindow(page, "Rename");
                }
            },
            "add": {
                name: "Add Child Nodes",
                icon: "add",
                // superseeds "global" callback
                callback: function (key, options) {
                    var idn = $(this).attr("id");
                    var page = '/TreeViewModels/AddNodes?id=' + idn;
                    OpenWindow(page, "Add Child Nodes");
                    LoadScpt();
                }
            },
            "sep1": "---------",
            "delete": {
                name: "Delete",
                icon: "delete",
                callback: function () {
                    var idn = $(this).attr("id");
                    var $c = confirm("Really?");
                    if ($c == true) {
                        DeleteNode(idn);
                    };
                }
            }

        }
    });

    $.contextMenu({
        selector: '.context-menu-two',
        trigger: 'left',
        items: {
            "edit": {
                name: "Edit",
                icon: "edit",
                // superseeds "global" callback
                callback: function (key, options) {
                    var idn = $(this).attr("id");
                    var page = '/TreeViewModels/Edit?id=' + idn;
                    OpenWindow(page, "Rename");
                }
            },
            "sep1": "---------",
            "delete": {
                name: "Delete",
                icon: "delete",
                callback: function () {
                    var idn = $(this).attr("id");
                    var $c = confirm("Really?");
                    if ($c == true) {
                        DeleteNode(idn);
                    };
                }
            }

        }
    });
});

//Load Tree
function LoadTree() {
   var treeDiv = $('#tree-display').empty();
    treeDiv.treeview({
        source: '/TreeViewModels/GetTreeData',

        onCompleted: function (event, data) {

        },
        onNodeExpanded: function (event, data) {

        },
        onNodeColapsed: function (event, data) {

        },
        onNodeCreated: function (event, data) {

        }
    });
}

//Dialog popup loads Partial view
function OpenWindow(page,title) {
    $dialog1 = $('#pop-box')
            .dialog({
                autoOpen: false,
                modal: true,
                width: 600,
                title: title,
            })
    $dialog1.dialog('open');
    $dialog1.load(page);
}

//Load Script for default values AddNodes View
function LoadScpt() {
    var script = document.createElement('script');

    var addDefaultDiv = document.getElementById('addDynamic');

    var addDefaultDivJQuery = $('#addDynamic');

    addDefaultDivJQuery.empty();

    script.src = addDefaultDivJQuery.data('default-values');

    addDefaultDiv.appendChild(script);
}

////Functions called upon Form Submit Updates Database
function alertSuccess(result) {
    alert(result.message);
    $dialog1.dialog("close");
    LoadTree();
}

function DeleteNode(idn) {

    $.ajax({
        url: '/TreeViewModels/Delete',
        data: '{ id:' + idn +'}',
        dataType: 'json',
        type: "POST",
        contentType: 'application/json; charset=uft-8',
        success: function (result) {
            alert(result.message);
            LoadTree();
        },
        error: function () {
            alert('Error!');
        }
    });
}