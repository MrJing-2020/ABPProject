(function () {
    $(function () {
        var app = new Vue({
            el: '#vue-app',
            data: {
                formItem: { salesOrderItems :[{}] },
                abpService: abp.services.app.salesOrder,
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
                            {
                                field: 'state',
                                checkbox: true,
                                align: 'center',
                                valign: 'middle'
                            },
                            {
                                field: 'salesId',
                                title: '订单编号',
                                sortable: true,
                                align: 'center'
                            },
                            {
                                field: 'salesName',
                                title: '客户',
                                sortable: true,
                                align: 'center'
                            },
                            {
                                field: 'salesContractNum',
                                title: '合同号',
                                align: 'center'
                            },
                            {
                                field: 'deliveryDate',
                                title: '提交日期',
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
                                        '<button type="button" class="btn btn-green btn-xs detail-item">查看</button>',
                                        '<button type="button" data-toggle="modal" data-target="#del-confirm" class="btn btn-danger btn-xs remove-item">删除</button>'
                                    ].join('');
                                }
                            }
                        ],
                        delete: params.deleteItem,
                        detailFormatter: params.detailFormatter
                    });
                },
                //初始化页面
                init() {
                    var that = this;
                    //按钮事件
                    var operateEvents = {
                        'click .edit-item': function (e, value, row, index) {
                            that.getSalesOrderById(row.id);
                            $("#tab-edit a:first").trigger("click");
                        },
                        'click .detail-item': function (e, value, row, index) {
                            that.getSalesOrderById(row.id);
                            $("#tab-edit a:first").trigger("click");
                        },
                        'click .remove-item': function (e, value, row, index) {
                            that.deleteId = [row.id]
                        }
                    }
                    function detailFormatter(index, row) {
                        html = [];
                        $.map(row.salesOrderItems, function (obj, index) {
                            html.push('<h4>订单行' + parseInt(index + 1) + '：</h4>')
                            $.each(obj, function (key,value) {
                                html.push('<p><b>' + key + ':</b> ' + value + '</p>');
                            })
                        });
                        return html.join('');
                    };
                    that.initTableData({
                        operateEvents: operateEvents,
                        deleteItem: that.deleteItem,
                        detailFormatter: detailFormatter
                    });
                    console.log(that.formItem)
                },

                //根据id获取详情
                getSalesOrderById(id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.getSalesOrderById(postData).done(function (res) {
                        that.formItem = res;
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },

                //编辑和新增
                createItem() {
                    this.formItem = {};
                    this.formItem.salesOrderItems = [{}];
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
                    this.abpService.editSalesOrder(postData).done(function () {
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
                    that.abpService.deleteSalesOrder(postData).done(function (res) {
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