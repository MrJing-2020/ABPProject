(function () {
    $(function () {
        var app = new Vue({
            el: '#vue-app',
            data: {
                formItem: {},
                abpService: abp.services.app.deliver,
                deleteId: null,
                salesOrder:[]
            },
            //生命周期钩子（vue替换dom完成之后执行）
            mounted: function () {
                this.init()
            },
            methods: {
                initTableData: function (params) {
                    var that = this;
                    initTable({
                        //表格
                        table: $('#table-data'),
                        //批量删除按钮
                        remove: $('#table-remove'),
                        //批量删除弹窗确认按钮
                        delConfirmed: $('#deleleAll-confirmed'),
                        columns: [
                            {
                                field: 'selectState',
                                checkbox: true,
                                align: 'center',
                                valign: 'middle'
                            },
                            {
                                field: 'salesOrderNum',
                                title: '订单号',
                                align: 'center'
                            },
                            {
                                field: 'logisticsCompany',
                                title: '物流公司',
                                align: 'center'
                            },
                            {
                                field: 'logisticsNum',
                                title: '物流单号',
                                sortable: true,
                                align: 'center'
                            },
                            {
                                field: 'deliverTime',
                                title: '发货时间',
                                sortable: true,
                                align: 'center',
                                formatter: function (value, row, index) {
                                    console.log(value)
                                    return value.substring(0, value.lastIndexOf("T"));
                                }
                            },
                            {
                                field: 'option',
                                title: '操作',
                                align: 'center',
                                events: params.operateEvents,
                                formatter: function (value, row, index) {
                                    return [
                                        '<button type="button" class="btn btn-info btn-xs edit-item table-btn">编辑</button>',
                                        '<button type="button" data-toggle="modal" data-target="#del-confirm" class="btn btn-danger btn-xs remove-item table-btn">删除</button>'
                                    ].join('');
                                }
                            }
                        ],
                        delete: params.deleteItem
                    });
                },
                //初始化页面
                init: function () {
                    var that = this;
                    //按钮事件
                    var operateEvents = {
                        'click .edit-item': function (e, value, row, index) {
                            that.getDeliverById(row.id);
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
                    this.abpService.getSelectList().done(function (res) {
                        that.salesOrder = res.salesOrder;
                    })
                    $('.datepicker-default').datepicker({
                        language: 'zh-CN'
                    }).on('changeDate', function (ev) {
                        that.formItem.deliverTime = ev.date;
                    });
                },

                //根据id获取详情
                getDeliverById: function (id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.getDeliverById(postData).done(function (res) {
                        that.formItem = res;
                        //仅取日期部分
                        that.formItem.deliverTime = that.formItem.deliverTime.substring(0, that.formItem.deliverTime.lastIndexOf("T"));
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },

                //编辑和新增
                createItem: function () {
                    this.formItem = {};
                    $("#tab-edit a:first").trigger("click");
                },
                subFormData: function (e) {
                    e.preventDefault();
                    var _$form = $("#editItemForm");
                    _$form.validate();
                    if (!_$form.valid()) {
                        return;
                    }
                    var postData = this.formItem;
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.editDeliver(postData).done(function () {
                        location.reload(true);
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },
                submitCancel: function (e) {
                    submitCancel(e.target);
                    if ($(e.target).attr("target") == "tab-edit") {
                        this.formItem = {};
                    }
                },

                //删除一到多项
                deleteItem: function (params) {
                    var that = this
                    var postData = { "ids": params.ids }
                    abp.ui.setBusy($("#vue-app"));
                    that.abpService.deleteDeliver(postData).done(function (res) {
                        params.callBack()
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                        $(".del-confirm").modal('hide');
                    });
                },
                delConfirmed: function () {
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