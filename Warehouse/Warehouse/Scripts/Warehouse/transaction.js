$(document).ready(function () {
    // when document was opened
    //
    // Create ComboBox for input HTML element
    //
    
    itemNames = new kendo.data.DataSource({
        transport: {
            read: {
                url: base_url + "item/itemComboBoxSelection",
                dataType: "json"
            }
        },
        schema: {
            model: {
                fields: {
                    itemID: {type: "number" },
                    itemName: {type: "string" }
                }
            }
        }
    });

    $("#itemName").kendoComboBox({
        placeholder: "Select Item...",
        dataTextField: "itemName",
        dataValueField: "itemID",
        dataSource: itemNames
    });

    reasons = new kendo.data.DataSource({
        transport: {
            read: {
                url: base_url + "reason/reasoncomboBoxSelection",
                dataType: "json"
            }
        },
        schema: {
            model: {
                fields: {
                    reasonID: { type: "number" },
                    description: { type: "string" }
                }
            }
        }
    });
    $("#selectanyreasons").kendoComboBox({
        placeholder: "Select Transaction Type...",
        dataTextField: "description",
        dataValueField: "reasonID",
        dataSource: reasons
    });

    //
    // create DatePicker from input HTML element
    //
    $("#datepicker").kendoDatePicker();
});

$('#buttonSaveTransaction').click(function () {

    var submitURL = base_url + "log/insertLog";

    $.ajax({
        contentType: "application/json",
        type: 'POST',
        url: submitURL,
        data: JSON.stringify({
            itemID: $("#itemName").val(),
            totalnumber: $("#total").val(),
            reasonID: $("#selectanyreasons").val()
        }),
        success: function (result) {
            if (result.isValid) {
                $.messager.alert('Information', result.message, 'info', function () {
                    window.location = base_url + "log/getlastlog";
                });
            }
            else {
                $.messager.alert('Warning', result.message, 'warning');
            }
        }
    });

    alert("Transaction has been created.");

});