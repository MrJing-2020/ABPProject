(function () {
    var $table = $('#table'),
        $remove = $('#remove'),
        selections = [];
    function initTable() {
        $table.bootstrapTable({
            height: getHeight(),
            columns: [
                [
                    {
                        field: 'state',
                        checkbox: true,
                        rowspan: 2,
                        align: 'center',
                        valign: 'middle'
                    },
                    {
                        title: 'ID',
                        field: 'id',
                        rowspan: 2,
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        title: '角色详情',
                        colspan: 4,
                        align: 'center'
                    }
                ],
                [
                    {
                        field: 'displayName',
                        title: '昵称',
                        sortable: true,
                        align: 'center'
                    },
                    {
                        field: 'name',
                        title: '名字',
                        sortable: true,
                        align: 'center'
                    },
                    {
                        field: 'isStatic',
                        title: '是否静态',
                        align: 'center'
                    },
                    {
                        field: 'option',
                        title: '操作',
                        align: 'center',
                        events: operateEvents,
                        formatter: operateFormatter
                    }
                ]
            ],
            method: "POST",
            queryParamsType: undefined,
            queryParams: function (params) {
                console.log(params)
                return JSON.stringify({
                    "PageSize": params.limit,
                    "PageNumber": params.offset,
                    "SortOrder": params.order,
                    "SearchText": params.search == null ? "" : params.search,
                    "SortName": params.sort == null ? "" : params.sort,
                })
            },
            responseHandler: function (res) {
                var data = {
                    "total": 2,
                    "rows": res.result.items
                }
                $.each(data.rows, function (i, row) {
                    row.state = false
                });
                return data
            },
            detailFormatter: function detailFormatter(index, row) {
                var html = [];
                $.each(row, function (key, value) {
                    html.push('<p><b>' + key + ':</b> ' + value + '</p>');
                });
                return html.join('');
            }
        });
        // sometimes footer render error.
        //setTimeout(function () {
        //    $table.bootstrapTable('resetView');
        //}, 200);
        $table.on('check.bs.table uncheck.bs.table ' +
            'check-all.bs.table uncheck-all.bs.table', function () {
                $remove.prop('disabled', !$table.bootstrapTable('getSelections').length);
                // save your data, here just save the current page
                selections = getIdSelections();
                // push or splice the selections if you want to save all data selections
            });
        //$table.on('expand-row.bs.table', function (e, index, row, $detail) {
        //    if (index % 2 == 1) {
        //        $detail.html('Loading from ajax request...');
        //        $.get('LICENSE', function (res) {
        //            $detail.html(res.replace(/\n/g, '<br>'));
        //        });
        //    }
        //});
        $table.on('all.bs.table', function (e, name, args) {
            console.log(name, args);
        });
        $remove.click(function () {
            var ids = getIdSelections();
            $table.bootstrapTable('remove', {
                field: 'id',
                values: ids
            });
            $remove.prop('disabled', true);
        });
        $(window).resize(function () {
            $table.bootstrapTable('resetView', {
                height: getHeight()
            });
        });
    }
    function getIdSelections() {
        return $.map($table.bootstrapTable('getSelections'), function (row) {
            return row.id
        });
    }
    function operateFormatter(value, row, index) {
        return [
            '<a class="like" href="javascript:void(0)" title="Like">',
            '<i class="glyphicon glyphicon-heart"></i>',
            '</a>  ',
            '<a class="remove" href="javascript:void(0)" title="Remove">',
            '<i class="glyphicon glyphicon-remove"></i>',
            '</a>'
        ].join('');
    }
    window.operateEvents = {
        'click .like': function (e, value, row, index) {
            alert('You click like action, row: ' + JSON.stringify(row));
        },
        'click .remove': function (e, value, row, index) {
            $table.bootstrapTable('remove', {
                field: 'id',
                values: [row.id]
            });
        }
    };
    function totalTextFormatter(data) {
        return 'Total';
    }
    function totalNameFormatter(data) {
        return data.length;
    }
    function totalPriceFormatter(data) {
        var total = 0;
        $.each(data, function (i, row) {
            total += +(row.price.substring(1));
        });
        return '$' + total;
    }
    function getHeight() {
        //return $(window).height() - $('h1').outerHeight(true
        return $(window).height() - 100;
    }

    $(function () {
        initTable();
        var _roleService = abp.services.app.role;
        var _$modal = $('#RoleCreateModal');
        var _$form = _$modal.find('form');
        _$form.validate();

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var role = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

            abp.ui.setBusy(_$modal);
            _roleService.createRole(role).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new user!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });
    });
})();