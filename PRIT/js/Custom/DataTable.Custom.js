var Grid = function () {

    InitGrid = function (settings) {

        $('#' + settings.tableId + ' tfoot th').each(function () {
            var title = $(this).text();
            if (!$(this).data('removesearch'))
                $(this).html('<input style="width: 100%;" type="text" placeholder="' + title + '" />');
        });

        // DataTable
        var table = $('#' + settings.tableId + '').DataTable({
            "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
            "processing": false,
            "serverSide": false,
            "bLengthChange": true,
            "bDestroy": true,
            "iDisplayLength": 10,
            "bAutoWidth": true,
            "bDeferRender": true,
            "bSortCellsTop": true,
            "ordering": true,
            "aaSorting": [[0, "asc"]],
            "aaSortingFixed": [],
            "columnDefs": [{ orderable: false, targets: ['nosort'] }],

            //"oLanguage": { "sEmptyTable": 'No data available ' },
            // "oClasses": { sLengthSelect: 'form-control', sFilterInput: 'form-control' },
        });
        // Apply the search
        table.columns().every(function () {
            var that = this;

            $('input', this.footer()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that
                        .search(this.value)
                        .draw();
                }
            });
        });
    },

    InitDynamicGrid = function (settings) {
        $('#' + settings.tableId + ' tfoot th').each(function () {
            var title = $(this).text();
            if (!$(this).data('removesearch'))
                $(this).html('<input style="width: 100%;" type="text" placeholder="' + title + '" />');
        });

        // DataTable
        var table = $('#' + settings.tableId + '').DataTable({
            "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
            serverSide: true,
            processing: true,
            //"iDisplayLength": appSettings.pageSize,
            "iDisplayLength": 10,
            "columnDefs": [{ orderable: false, targets: ['nosort'] }],
            "columns": settings.columns,
            "bAutoWidth": true,
            "bDeferRender": true,
            "bSortCellsTop": true,
            "ordering": true,
            "aaSorting": [[0, "asc"]],
            "aaSortingFixed": [],
            "ajax": {
                "url": settings.url,
                "type": "POST",
                "data": settings.data,
                "datatype": "json",
                complete: function () {
                    if ($("[name='cbxArchive']").length > 0)
                        $("[name='cbxArchive']").bootstrapSwitch();
                    if ($("[name='cbxActivate']").length > 0)
                        $("[name='cbxActivate']").bootstrapSwitch();

                    $('#' + settings.tableId + '').css('width', '100%');
                }
            },
        });
        // Apply the search
        table.columns().every(function () {
            var that = this;

            $('input', this.footer()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that
                        .search(this.value)
                        .draw();
                }
            });
        });

        return table;
    },

    ExportTableToExcel = function (tableId, sheetName) {
        $('#' + tableId).table2excel({
            name: "Worksheet Name",
            filename: sheetName,
            sheetName: sheetName
        });

    }   
    return {
        InitGrid: InitGrid,
        InitDynamicGrid: InitDynamicGrid,
        ExportTableToExcel: ExportTableToExcel       
    }
}();

