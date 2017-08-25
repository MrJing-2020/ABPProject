;
getSelections = [];
var g_table
var g_remove
var g_delConfirmed
function initTable(tableParams) {
    g_table = tableParams.table;
    g_remove = tableParams.remove;
    g_delConfirm = tableParams.delConfirmed
    g_table.bootstrapTable({
        //height: getTableHeight(),
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
                row.selectState = false
            });
            return data
        },
        detailFormatter: function (index, row) {
            var html;
            if (tableParams.detailFormatter == null) {
                html = [];
                $.each(row, function (key, value) {
                    html.push('<p><b>' + key + ':</b> ' + value + '</p>');
                });
                return html.join('');
            }
            else {
                html = tableParams.detailFormatter(index, row);
                return html
            }
        }
    });
    g_table.on('check.bs.table uncheck.bs.table ' +
        'check-all.bs.table uncheck-all.bs.table', function () {
            g_remove.prop('disabled', !g_table.bootstrapTable('getSelections').length);
            getSelections = $.map(g_table.bootstrapTable('getSelections'), function (row) {
                 return row.id
            });
        });
    g_delConfirm.click(function () {
        var ids = $.map(g_table.bootstrapTable('getSelections'), function (row) {
            return row.id
        });
        if (tableParams.delete != null) {
            tableParams.delete(
                {
                    ids: ids,
                    callBack: function () {
                        g_table.bootstrapTable('remove', {
                            field: 'id',
                            values: ids
                        });
                        g_remove.prop('disabled', true);
                    }
                }
            );
        }
        else {
            g_table.bootstrapTable('remove', {
                field: 'id',
                values: ids
            });
            g_remove.prop('disabled', true);
        }
    });
}
function getTableHeight() {
    return $("#vue-app").height();
}
function getIdSelections() {
    return $.map(g_table.bootstrapTable('getSelections'), function (row) {
        return row.id
    });
}
function initCheckBox() {
    $('.check-radio-ickeck').iCheck({
        labelHover: false,
        cursor: true,
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue',
        increaseArea: '10%'
    });
}
function submitCancel(e) {
    var $targetEle = $("#" + $(e).attr("target"));
    if ($targetEle.hasClass("tab-hidden")) {
        $targetEle.css("display", "none");
    }
    $("#tab-list a:first").trigger("click");
    //$(".tab-hidden").css("display", "none");
}
function sumcn(num) {
    var strOutput = "";
    var strUnit = '仟佰拾亿仟佰拾万仟佰拾元角分';
    num += "00";
    var intPos = num.indexOf('.');
    if (intPos >= 0)
        num = num.substring(0, intPos) + num.substr(intPos + 1, 2);
    strUnit = strUnit.substr(strUnit.length - num.length);
    for (var i = 0; i < num.length; i++)
        strOutput += '零壹贰叁肆伍陆柒捌玖'.substr(num.substr(i, 1), 1) + strUnit.substr(i, 1);
    return strOutput.replace(/零角零分$/, '整').replace(/零[仟佰拾]/g, '零').replace(/零{2,}/g, '零').replace(/零([亿|万])/g, '$1').replace(/零+元/, '元').replace(/亿零{0,3}万/, '亿').replace(/^元/, "零元");
}
function moneyFormat(s) {
    if (/[^0-9\.]/.test(s)) return "invalid value";
    s = s.replace(/^(\d*)$/, "$1.");
    s = (s + "00").replace(/(\d*\.\d\d)\d*/, "$1");
    s = s.replace(".", ",");
    var re = /(\d)(\d{3},)/;
    while (re.test(s))
        s = s.replace(re, "$1,$2");
    s = s.replace(/,(\d\d)$/, ".$1");
    return s.replace(/^\./, "0.")
}
;