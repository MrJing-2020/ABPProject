(function () {
    var app = new Vue({
        data() {
            return {
                formItem: null,
                roleService: abp.services.app.role,
                $table: null,
                $remove: null,
                selections: [],
                operateEvents: null
            }
        },
        created() {
            this.init()
        },
        methods: {
            initTable() {
                this.$table.bootstrapTable({
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
                this.$table.on('check.bs.table uncheck.bs.table ' +
                    'check-all.bs.table uncheck-all.bs.table', function () {
                        this.$remove.prop('disabled', !this.$table.bootstrapTable('getSelections').length);
                        this.selections = this.getIdSelections();
                    });
                this.$remove.click(function () {
                    var ids = getIdSelections();
                    this.$table.bootstrapTable('remove', {
                        field: 'id',
                        values: ids
                    });
                    this.$remove.prop('disabled', true);
                });
            },
            operateFormatter(value, row, index) {
                return [
                    '<button type="button" class="btn btn-info btn-xs edit-item">编辑</button>',
                    '<button type="button" class="btn btn-danger btn-xs remove-item">删除</button>',
                ].join('');
            },
            init() {
                this.$table = $('#table');
                this.$remove = $('#remove')
                this.initTable();
                this.operateEvents = {
                    'click .edit-item': function (e, value, row, index) {
                        var _roleService = abp.services.app.role;
                        var data = { "id": row.id }
                        abp.ui.setBusy($("html"));
                        _roleService.getRoleById(data).done(function (res) {
                            console.log(res)
                        }).always(function () {
                            abp.ui.clearBusy($("html"));
                        });
                    },
                    'click .remove-item': function (e, value, row, index) {
                        this.$table.bootstrapTable('remove', {
                            field: 'id',
                            values: [row.id]
                        });
                    }
                }
                this.roleService = abp.services.app.role;
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
            subFormData() {
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
                return $.map(this.$table.bootstrapTable('getSelections'), function (row) {
                    return row.id
                });
            }
        }
    })
})();

//(function () {
//    var $table = $('#table'),
//        $remove = $('#remove'),
//        selections = [];
//    function initTable() {
//        $table.bootstrapTable({
//            //height: getHeight(),
//            columns: [
//                [
//                    {
//                        field: 'state',
//                        checkbox: true,
//                        rowspan: 2,
//                        align: 'center',
//                        valign: 'middle'
//                    },
//                    {
//                        title: 'ID',
//                        field: 'id',
//                        rowspan: 2,
//                        align: 'center',
//                        valign: 'middle',
//                    },
//                    {
//                        title: '角色详情',
//                        colspan: 4,
//                        align: 'center'
//                    }
//                ],
//                [
//                    {
//                        field: 'displayName',
//                        title: '昵称',
//                        sortable: true,
//                        align: 'center'
//                    },
//                    {
//                        field: 'name',
//                        title: '名字',
//                        sortable: true,
//                        align: 'center'
//                    },
//                    {
//                        field: 'isStatic',
//                        title: '是否静态',
//                        align: 'center'
//                    },
//                    {
//                        field: 'option',
//                        title: '操作',
//                        align: 'center',
//                        events: operateEvents,
//                        formatter: operateFormatter
//                    }
//                ]
//            ],
//            method: "POST",
//            queryParams: function (params) {
//                return JSON.stringify({
//                    "PageSize": params.limit,
//                    "PageNumber": parseInt(params.offset / params.limit),
//                    "SortOrder": params.order,
//                    "SearchText": params.search == null ? "" : params.search,
//                    "SortName": params.sort == null ? "" : params.sort,
//                })
//            },
//            responseHandler: function (res) {
//                var data = {
//                    "total": res.result.totalCount,
//                    "rows": res.result.items
//                }
//                $.each(data.rows, function (i, row) {
//                    row.state = false
//                });
//                return data
//            },
//            detailFormatter: function (index, row) {
//                var html = [];
//                $.each(row, function (key, value) {
//                    html.push('<p><b>' + key + ':</b> ' + value + '</p>');
//                });
//                return html.join('');
//            }
//        });
//        $table.on('check.bs.table uncheck.bs.table ' +
//            'check-all.bs.table uncheck-all.bs.table', function () {
//                $remove.prop('disabled', !$table.bootstrapTable('getSelections').length);
//                selections = getIdSelections();
//            });
//        $remove.click(function () {
//            var ids = getIdSelections();
//            $table.bootstrapTable('remove', {
//                field: 'id',
//                values: ids
//            });
//            $remove.prop('disabled', true);
//        });
//    }
//    function getIdSelections() {
//        return $.map($table.bootstrapTable('getSelections'), function (row) {
//            return row.id
//        });
//    }
//    function operateFormatter(value, row, index) {
//        return [
//            '<button type="button" class="btn btn-info btn-xs edit-item">编辑</button>',
//            '<button type="button" class="btn btn-danger btn-xs remove-item">删除</button>',
//        ].join('');
//    }
//    window.operateEvents = {
//        'click .edit-item': function (e, value, row, index) {
//            var _roleService = abp.services.app.role;
//            var data = { "id": row.id }
//            abp.ui.setBusy($("html"));
//            _roleService.getRoleById(data).done(function (res) {
//                console.log(res)
//            }).always(function () {
//                abp.ui.clearBusy($("html"));
//            });
//        },
//        'click .remove-item': function (e, value, row, index) {
//            $table.bootstrapTable('remove', {
//                field: 'id',
//                values: [row.id]
//            });
//        }
//    };
//    function getHeight() {
//        return $(".table-content-cum").height();
//        //return 700;
//    }
//    $(function () {
//        initTable();
//        var _roleService = abp.services.app.role;
//        var _$form = $("#userCreateForm");
//        _$form.validate();
//        _$form.find('button[type="submit"]').click(function (e) {
//            e.preventDefault();
//            if (!_$form.valid()) {
//                return;
//            }
//            var role = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

//            abp.ui.setBusy($("html"));
//            _roleService.editRole(role).done(function () {
//                location.reload(true); //reload page to see new user!
//            }).always(function () {
//                abp.ui.clearBusy($("html"));
//            });
//        });
//    });
//})();