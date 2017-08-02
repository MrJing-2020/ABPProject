(function () {
    $(function () {

    })

})();
var appVue = new Vue({
    el: '#aogdhlo',
    data: {
        formItem: null,
        roleService: abp.services.app.role,
        $table: null,
        $remove: null,
        selections: [],
        operateEvents: null
    },
    //created() {
    //    this.init()
    //},
    methods: {
        test: function () {
            alert("123")
        },
        initTable() {
            var that = this;
            that.$table.bootstrapTable({
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
                            events: this.operateEvents,
                            formatter: this.operateFormatter
                        }
                    ]
                ],
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
                detailFormatter: function (index, row) {
                    var html = [];
                    $.each(row, function (key, value) {
                        html.push('<p><b>' + key + ':</b> ' + value + '</p>');
                    });
                    return html.join('');
                }
            });
            that.$table.on('check.bs.table uncheck.bs.table ' +
                'check-all.bs.table uncheck-all.bs.table', function () {
                    that.$remove.prop('disabled', !that.$table.bootstrapTable('getSelections').length);
                    that.selections = that.getIdSelections();
                });
            this.$remove.click(function () {
                var ids = that.getIdSelections();
                that.$table.bootstrapTable('remove', {
                    field: 'id',
                    values: ids
                });
                that.$remove.prop('disabled', true);
            });
        },
        operateFormatter(value, row, index) {
            return [
                '<button type="button" class="btn btn-info btn-xs edit-item">编辑</button>',
                '<button type="button" class="btn btn-danger btn-xs remove-item">删除</button>',
            ].join('');
        },
        init() {
            this.roleService = abp.services.app.role;
            this.$table = $('#table');
            this.$remove = $('#remove');
            var that = this;
            this.operateEvents = {
                'click .edit-item': function (e, value, row, index) {
                    var data = { "id": row.id }
                    abp.ui.setBusy($("html"));
                    that.roleService.getRoleById(data).done(function (res) {
                        console.log(res)
                    }).always(function () {
                        abp.ui.clearBusy($("html"));
                    });
                },
                'click .remove-item': function (e, value, row, index) {
                    that.$table.bootstrapTable('remove', {
                        field: 'id',
                        values: [row.id]
                    });
                }
            }
            this.initTable();
        },
        getRoleById(id) {
            var data = { "id": id }
            abp.ui.setBusy($("html"));
            this.roleService.getRoleById(data).done(function (res) {
                console.log(res)
            }).always(function () {
                abp.ui.clearBusy($("html"));
            });
        },
        subFormData: function (e) {
            e.preventDefault();
            var _$form = $("#userCreateForm");
            _$form.validate();
            if (!_$form.valid()) {
                return;
            }
            var role = this.formItem;
            abp.ui.setBusy($("html"));
            this.roleService.editRole(role).done(function () {
                location.reload(true);
            }).always(function () {
                abp.ui.clearBusy($("html"));
            });
        },
        getIdSelections() {
            var that = this;
            return $.map(that.$table.bootstrapTable('getSelections'), function (row) {
                return row.id
            });
        }
    }
})