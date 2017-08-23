(function () {
    $(function () {
        var app = new Vue({
            el: '#vue-app',
            data: {
                formItem: { purchaseOrderItems: [{}] },
                abpService: abp.services.app.purchaseOrder,
                deleteId: null,
                inventSite: [],
                supplier: [],
                contract: [],
                inventLocations: [],
                product:[]
            },
            //生命周期钩子（vue替换dom完成之后执行）
            mounted: function() {
                this.init()
            },
            methods: {
                initTableData: function(params) {
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
                                field: 'purchNum',
                                title: '订单编号',
                                sortable: true,
                                align: 'center'
                            },
                            {
                                field: 'supplierName',
                                title: '供应商',
                                sortable: true,
                                align: 'center'
                            },
                            {
                                field: 'contractNum',
                                title: '合同编号',
                                align: 'center'
                            },
                            {
                                field: 'creationTime',
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
                                        '<button type="button" class="btn btn-info btn-xs table-btn edit-item">编辑</button>',
                                        '<button type="button" class="btn btn-green btn-xs table-btn detail-item">查看</button>',
                                        '<button type="button" data-toggle="modal" data-target="#del-confirm" class="btn btn-danger btn-xs table-btn remove-item">删除</button>'
                                    ].join('');
                                }
                            }
                        ],
                        delete: params.deleteItem,
                        detailFormatter: params.detailFormatter
                    });
                },
                //初始化页面
                init: function() {
                    var that = this;
                    //按钮事件
                    var operateEvents = {
                        'click .edit-item': function (e, value, row, index) {
                            that.getPurchaseOrderById(row.id);
                            $("#tab-edit a:first").trigger("click");
                        },
                        'click .detail-item': function (e, value, row, index) {
                            that.getPurchaseOrderById(row.id);
                            $("#tab-edit a:first").trigger("click");
                        },
                        'click .remove-item': function (e, value, row, index) {
                            that.deleteId = [row.id]
                        }
                    }
                    function detailFormatter(index, row) {
                        html = [];
                        $.map(row.purchaseOrderItems, function (obj, index) {
                            html.push('<h4>采购订单行' + parseInt(index + 1) + '：</h4>')
                            $.each(obj, function (key, value) {
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
                    this.abpService.getSelectList().done(function (res) {
                        that.supplier = res.supplier;
                        that.contract = res.contract;
                        that.inventSite = res.inventSite;
                        that.product = res.product;
                    });
                    that.formItem = { purchaseOrderItems: [] };
                    that.formItem.purchaseOrderItems.push({ index: Math.random(), inventBatch: [] });
                },

                //根据id获取详情
                getPurchaseOrderById: function(id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.getPurchaseOrderById(postData).done(function (res) {
                        that.formItem = res;
                        for (var key in that.formItem.purchaseOrderItems) {
                            var index = Math.random();
                            that.formItem.purchaseOrderItems[key].index = index;
                            that.changeInventBatch(index);
                        }
                        that.inventSiteChange();
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },

                //编辑和新增
                createItem: function() {
                    this.formItem = { purchaseOrderItems: [] };
                    this.formItem.purchaseOrderItems.push({ index: Math.random(), inventBatch: [] });
                    $("#tab-edit a:first").trigger("click");
                },
                subFormData: function (e) {
                    var that = this;
                    e.preventDefault();
                    var _$form = $("#editItemForm");
                    _$form.validate();
                    if (!_$form.valid()) {
                        return;
                    }
                    var postData = this.formItem;
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.editPurchaseOrder(postData).done(function () {
                        that.formItem = { purchaseOrderItems: [] };
                        that.formItem.purchaseOrderItems.push({ index: Math.random(), inventBatch: [] });
                        location.reload(true);
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },
                submitCancel: function (e) {
                    var that = this;
                    submitCancel(e.target);
                    if ($(e.target).attr("target") == "tab-edit") {
                        that.formItem = { purchaseOrderItems: [] };
                        that.formItem.purchaseOrderItems.push({ index: Math.random(), inventBatch: [] });
                    }
                },
                inventSiteChange: function() {
                    var that = this;
                    for (var key in that.inventSite) {
                        if (that.inventSite[key].id == that.formItem.inventSiteId) {
                            that.inventLocations = that.inventSite[key].inventLocations;
                        }
                    }
                },
                //产品change事件
                productChange: function (e) {
                    var index = $(e.target).attr("target");
                    this.changeInventBatch(index);
                },
                changeInventBatch: function (index) {
                    var that = this;
                    orderitems = that.formItem.purchaseOrderItems;
                    var targetItem = {};
                    var itemKey;
                    for (var key in orderitems) {
                        if (orderitems[key].index == index) {
                            targetItem = orderitems[key];
                            itemKey = key;
                        }
                    };
                    for (var key in that.product) {
                        if (that.product[key].id == targetItem.productId) {
                            that.formItem.purchaseOrderItems[itemKey].inventBatch = that.product[key].inventBatchs;
                        }
                    }
                },
                //添加产品
                addProduct: function () {
                    var that = this
                    that.formItem.purchaseOrderItems.push({ index: Math.random(), inventBatch: [] });
                },
                //删除产品
                delProduct: function (e) {
                    var that = this;
                    var index = $(e.target).attr("target");
                    for (var key in that.formItem.purchaseOrderItems) {
                        if (that.formItem.purchaseOrderItems[key].index == index) {
                            that.formItem.purchaseOrderItems.splice(key, 1);
                        }
                    }
                },

                //删除一到多项
                deleteItem: function(params) {
                    var that = this
                    var postData = { "ids": params.ids }
                    abp.ui.setBusy($("#vue-app"));
                    that.abpService.deletePurchaseOrder(postData).done(function (res) {
                        params.callBack()
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                        $(".del-confirm").modal('hide');
                    });
                },
                delConfirmed: function() {
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