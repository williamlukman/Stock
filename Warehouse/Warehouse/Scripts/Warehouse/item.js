$(document).ready(function () {
    jQuery("#list_item").jqGrid({
        url: base_url + 'item/AllItems',
        datatype: "json",
        mtype: "GET",
        colNames:['ID', 'Name'],
        colModel:[
            { name: 'itemID', index:'itemID', width: 55 },
            { name: 'itemName', index: 'itemName', width: 200, editable: true }
        ],
        rowNum:10,
        rowList: [10, 20, 30],
        width: 500,
        height: "100%",
        pager: '#pager_item',
        sortname: 'itemName',
        viewrecords: true,
        sortorder: "asc",
        editurl: base_url + "item/edit",
        
        caption: "Warehouse: All Items"
    });
    jQuery("#list_item").jqGrid('navGrid', '#pager_item', { edit: false, add: false, del: false });
    //jQuery("#list_item").jqGrid('inlineNav', "#pager_item");
});

$('#buttonSaveItem').click(function () {

    var submitURL = base_url + "item/createItem";

    $.ajax({
        contentType: "application/json",
        type: 'POST',
        url: submitURL,
        data: JSON.stringify({
            itemName: $("#itemName").val()
        }),
        success: function (result) {
            if (result.isValid) {
                $.messager.alert('Information', result.message, 'info', function () {
                    window.location = base_url + "log/getlastitem";
                });
            }
            else {
                $.messager.alert('Warning', result.message, 'warning');
            }
        }
    });

    alert("Item has been created.");
});