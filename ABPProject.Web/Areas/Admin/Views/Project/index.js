(function () {
    $(function () {
        var app = new Vue({
            el: '#vue-app',
            data: {
                formItem: {},
                abpService: abp.services.app.project,
                deleteId: null
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
                        //批量删除弹窗确认按钮
                        delConfirmed: $('#deleleAll-confirmed'),
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
                                    title: '项目详情',
                                    colspan: 4,
                                    align: 'center'
                                }
                            ],
                            [
                                {
                                    field: 'name',
                                    title: '名称',
                                    sortable: true,
                                    align: 'center'
                                },
                                {
                                    field: 'description',
                                    title: '描述',
                                    align: 'center'
                                },
                                {
                                    field: 'creationTime',
                                    title: '创建时间',
                                    sortable: true,
                                    align: 'center',
                                    formatter: function (value, row, index) {
                                        return value.replace("T", " ").substring(0, value.lastIndexOf("."));
                                    }
                                },
                                {
                                    field: 'option',
                                    title: '操作',
                                    align: 'center',
                                    events: params.operateEvents,
                                    formatter: function (value, row, index) {
                                        return [
                                            '<button type="button" class="btn btn-info btn-xs edit-item">编辑</button>',
                                            '<button type="button" data-toggle="modal" data-target="#del-confirm" class="btn btn-danger btn-xs remove-item">删除</button>'
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
                        'click .edit-item': function (e, value, row, index) {
                            that.getProjectById(row.id);
                            $("#tab-edit a:first").trigger("click");
                        },
                        'click .remove-item': function (e, value, row, index) {
                            that.deleteId = [row.id]
                        }
                    }
                    that.initTableData({
                        operateEvents: operateEvents,
                        deleteItem: that.deleteItem
                    });
                },

                //根据id获取详情
                getProjectById(id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.getProjectById(postData).done(function (res) {
                        that.formItem = res;
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },

                //编辑和新增
                createItem() {
                    this.formItem = {};
                    $("#tab-edit a:first").trigger("click");
                },
                subFormData(e) {
                    e.preventDefault();
                    var _$form = $("#editItemForm");
                    _$form.validate();
                    if (!_$form.valid()) {
                        return;
                    }
                    var postData = this.formItem;
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.editProject(postData).done(function () {
                        location.reload(true);
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },

                //删除一到多项
                deleteItem(params) {
                    var that = this
                    var postData = { "ids": params.ids }
                    abp.ui.setBusy($("#vue-app"));
                    that.abpService.deleteProject(postData).done(function (res) {
                        params.callBack()
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                        $(".del-confirm").modal('hide');
                    });
                },
                delConfirmed() {
                    var that = this;
                    that.deleteItem(
                        {
                            ids: that.deleteId,
                            callBack: function () {
                                g_table.bootstrapTable('remove', {
                                    field: 'id',
                                    values: that.deleteId
                                });
                            }
                        }
                    )
                }
            }
        })
    })
})();