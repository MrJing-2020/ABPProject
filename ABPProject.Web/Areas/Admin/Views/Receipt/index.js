(function () {
    $(function () {
        var app = new Vue({
            el: '#vue-app',
            data: {
                formItem: { journalBalanceNum:""},
                abpService: abp.services.app.receipt,
                deleteId: null,
                client: [],
                contract: [],
                salesOrder: [],
                bank: [],
                receiptWay: [],
                paymentMethod: [],
                journalName: [],
                postingProfile: []
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
                                field: 'journalName',
                                title: '日记账名称',
                                align: 'center'
                            },
                            {
                                field: 'clientName',
                                title: '客户',
                                align: 'center'
                            },
                            {
                                field: 'journalBalance',
                                title: '金额',
                                sortable: true,
                                align: 'right'
                            },
                            {
                                field: 'receiptDate',
                                title: '付款时间',
                                sortable: true,
                                align: 'center',
                                formatter: function (value, row, index) {
                                    return value.substring(0, value.lastIndexOf("T"));
                                }
                            },
                            {
                                field: 'state',
                                title: '状态',
                                align: 'center',
                            },
                            {
                                field: 'option',
                                title: '操作',
                                align: 'center',
                                events: params.operateEvents,
                                formatter: function (value, row, index) {
                                    return [
                                        '<button type="button" class="btn btn-info btn-xs edit-item table-btn">编辑</button>',
                                        '<button type="button" class="btn btn-green btn-xs edit-item table-btn">查看</button>',
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
                            that.getReceiptById(row.id);
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
                    $.getJSON("/StaticData/AboutReceipt.json", function (data) {
                        that.receiptWay = data.receiptWay;
                        that.paymentMethod = data.paymentMethod;
                        that.journalName = data.journalName;
                        that.postingProfile = data.postingProfile;
                    });
                    this.abpService.getSelectList().done(function (res) {
                        that.client = res.client;
                        that.contract = res.contract;
                        that.salesOrder = res.salesOrder;
                        that.bank = res.bank;
                    });
                    $('.datepicker-default').datepicker({
                        language: 'zh-CN'
                    }).on('changeDate', function (ev) {
                        that.formItem.receiptDate = ev.date;
                    });
                },

                //根据id获取详情
                getReceiptById: function (id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.getReceiptById(postData).done(function (res) {
                        that.formItem = res;
                        that.formItem.receiptDate = that.formItem.receiptDate.substring(0, that.formItem.receiptDate.lastIndexOf("T"));
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },

                //编辑和新增
                createItem: function () {
                    this.formItem = { journalBalanceNum: ""};
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
                    this.abpService.editReceipt(postData).done(function () {
                        location.reload(true);
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },
                submitCancel: function (e) {
                    submitCancel(e.target);
                    if ($(e.target).attr("target") == "tab-edit") {
                        this.formItem = { journalBalanceNum: ""};
                    }
                },
                bankChange: function () {
                    var that = this;
                    var bankName = that.formItem.bankName;
                    for (var key in that.bank) {
                        if (that.bank[key].bankName == bankName) {
                            that.formItem.journalBalanceNum = that.bank[key].accountId;
                        }
                    }
                },

                //删除一到多项
                deleteItem: function (params) {
                    var that = this
                    var postData = { "ids": params.ids }
                    abp.ui.setBusy($("#vue-app"));
                    that.abpService.deleteReceipt(postData).done(function (res) {
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