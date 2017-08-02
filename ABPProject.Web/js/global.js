var $table = $('#table'),
    $remove = $('#remove'),
    tableItemSelections = [];
function initTable(tableParams) {
    $table.bootstrapTable({
        columns: tableParams.columns,
        method: "POST",
        queryParams: function (params) {
            return JSON.stringify({
                "PageSize": params.limit,
                "PageNumber": parseInt(params.offset / params.limit),
                "SortOrder": params.order,
                "SearchText": params.search == null ? "" : params.search,
                "SortName": params.sort == null ? "" : params.sort,
            })
        },
        responseHandler: function (res) {
            var data = {
                "total": res.result.totalCount,
                "rows": res.result.items
            }
            $.each(data.rows, function (i, row) {
                row.state = false
            });
            return data
        },
    });
    $table.on('check.bs.table uncheck.bs.table ' +
        'check-all.bs.table uncheck-all.bs.table', function () {
            $remove.prop('disabled', !$table.bootstrapTable('getSelections').length);
            tableItemSelections = getIdSelections();
        });
    $remove.click(function () {
        var ids = getIdSelections();
        $table.bootstrapTable('remove', {
            field: 'id',
            values: ids
        });
        $remove.prop('disabled', true);
    });
}
function getIdSelections() {
    return $.map($table.bootstrapTable('getSelections'), function (row) {
        return row.id
    });
}