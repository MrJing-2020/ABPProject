(function () {
    $(function () {
        var app = new Vue({
            el: '#page-wrapper',
            data: {
                formItem: {},
                abpService: abp.services.app.user,
                $table: $('#table-data'),
                $remove: $('#table-remove'),
                selections: [],
                operateEvents: null
            },
            //生命周期钩子（vue替换dom完成之后执行）
            mounted() {
                this.init()
            },
            methods: {
                initTableData(params) {
                    var that = this;
                    initTable({
                        //表格
                        table: $('#table-data'),
                        //批量删除按钮
                        remove: $('#table-remove'),
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
                                    title: '用户详情',
                                    colspan: 5,
                                    align: 'center'
                                }
                            ],
                            [
                                {
                                    field: 'userName',
                                    title: '用户名',
                                    sortable: true,
                                    align: 'center'
                                },
                                {
                                    field: 'fullName',
                                    title: '名字',
                                    sortable: true,
                                    align: 'center'
                                },
                                {
                                    field: 'emailAddress',
                                    title: '邮箱',
                                    align: 'center'
                                },
                                {
                                    field: 'isActive',
                                    title: '是否激活',
                                    align: 'center'
                                },
                                {
                                    field: 'option',
                                    title: '操作',
                                    align: 'center',
                                    events: params.operateEvents,
                                    formatter: function (value, row, index) {
                                        return [
                                            '<button type="button" class="btn btn-danger btn-xs remove-item">删除</button>',
                                        ].join('');
                                    }
                                }
                            ]
                        ],
                        delete: params.deleteItem
                    });
                },
                //初始化页面
                init() {
                    var that = this;
                    //按钮事件
                    var operateEvents = {
                        'click .remove-item': function (e, value, row, index) {
                            var ids = [row.id]
                            that.deleteItem(
                                {
                                    ids: ids,
                                    callBack: function () {
                                        g_table.bootstrapTable('remove', {
                                            field: 'id',
                                            values: [row.id]
                                        });
                                    }
                                }
                            )
                        }
                    }
                    that.initTableData({
                        operateEvents: operateEvents,
                        deleteItem: that.deleteItem
                    });
                },
                //根据id获取详情
                getUserById(id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("html"));
                    this.abpService.getUserById(postData).done(function (res) {
                        that.formItem = res;
                    }).always(function () {
                        abp.ui.clearBusy($("html"));
                    });
                },
                createItem() {
                    this.formItem = {};
                    $("#tab-edit a:first").trigger("click");
                },
                //表单数据提交
                subFormData(e) {
                    e.preventDefault();
                    var _$form = $("#editItemForm");
                    _$form.validate();
                    if (!_$form.valid()) {
                        return;
                    }
                    var postData = this.formItem;
                    abp.ui.setBusy($("html"));
                    this.abpService.createUser(postData).done(function () {
                        location.reload(true);
                    }).always(function () {
                        abp.ui.clearBusy($("html"));
                    });
                },
                //删除一到多项
                deleteItem(params) {
                    var that = this
                    var postData = { "ids": params.ids }
                    abp.ui.setBusy($("html"));
                    that.abpService.deleteUser(postData).done(function (res) {
                        params.callBack()
                    }).always(function () {
                        abp.ui.clearBusy($("html"));
                    });
                }
            }
        })
    })
})();