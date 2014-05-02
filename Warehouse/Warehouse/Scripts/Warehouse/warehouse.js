$(document).ready(function () {
    // when document was opened
    //
    // List all the items in the warehouse
    //
    jQuery("#list_warehouse").jqGrid({
        url: base_url + 'warehouse/GetAllWarehouse',
        datatype: "json",
        mtype: "GET",
        colNames:['Barcode', 'Item', 'Total'],
        colModel:[
            { name: 'itemID', index: 'itemID', width: 60 },
            { name: 'itemName', index: 'itemName', width: 200 },
            { name: 'totalstock', index: 'totalstock', width: 80 },
        ],
        rowNum:10,
        rowList:[10,20,30],
        width: 500,
        height: "100%",
        pager: '#pager_warehouse',
        sortname: 'itemName',
        viewrecords: true,
        sortorder: "asc",
        caption: "Warehouse Assets",
        onSelectRow: function(ids) {
        if(ids == null) {
            ids=0;
            if(jQuery("#list_warehouse_detail").jqGrid('getGridParam','records') >0 )
            {
                jQuery("#list_warehouse_detail").jqGrid('setGridParam', { url: "log/getlogsbyname?itemname=" + ids, page: 1 });
                jQuery("#list_warehouse_detail").jqGrid('setCaption',"Detail Transaksi untuk Barang: "+ids)
				.trigger('reloadGrid');
            }
        } else {
            jQuery("#list_warehouse_detail").jqGrid('setGridParam', { url: "log/getlogsbyname?itemname=" + ids, page: 1 });
            jQuery("#list_warehouse_detail").jqGrid('setCaption', "Detail Transaksi untuk Barang: " + ids)
			.trigger('reloadGrid');			
        }
    }
    });
    jQuery("#list_warehouse").jqGrid('navGrid', '#pager_warehouse', { edit: false, add: false, del: false });
    
    //
    // List all the detail transaction of selected items in the warehouse
    //
    jQuery("#list_warehouse_detail").jqGrid({
        url: base_url + 'log/getlogsbyname?itemname=unidentified',
        datatype: "json",
        mtype: "GET",
        colNames: [ 'Invoice #', 'Tanggal', 'Total', 'Keterangan'],
        colModel: [
            { name: 'invoicenumber', index: 'invoicenumber', width: 130 },
            { name: 'date', index: 'date', width: 100 },
            { name: 'totalstock', index: 'totalstock', width: 100,  align: "center" },
            { name: 'description', index: 'description', width: 200, align: "right" },
        ],
        rowNum: 10,
        rowList: [10, 20, 30],
        width: 500,
        height: "100%",
        pager: '#pager_warehouse_detail',
        sortname: 'date',
        viewrecords: true,
        sortorder: "asc",
        caption: "Detail Transaksi untuk Barang: "
    });
    jQuery("#list_log").jqGrid('navGrid', '#pager_log', { edit: false, add: false, del: false });
});