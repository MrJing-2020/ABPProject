(function () {
    $(function () {
        var app = new Vue({
            el: '#vue-app',
            data: {
                formItem: {},
                abpService: abp.services.app.product,
                deleteId: null,
                //产品分类下拉框数据（从json文件中获取）
                allProductCategory: [],
                productCategory: [],
                //单位下拉框数据
                unitList: [],
                projectList:[]
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
                                field: 'state',
                                checkbox: true,
                                align: 'center',
                                valign: 'middle'
                            },
                            {
                                field: 'name',
                                title: '名称',
                                align: 'center'
                            },
                            {
                                field: 'nameAlias',
                                title: '简称',
                                align: 'center'
                            },
                            {
                                field: 'category',
                                title: '分类',
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
                                field: 'stopped',
                                title: '是否启用',
                                align: 'center',
                                events: {
                                    'change #isUsered': function (e, value, row, index) {
                                        console.log(row)
                                        that.stopProduct(row.id);
                                    }
                                },
                                formatter: function (value, row, index) {
                                    var html = '';
                                    if (value == false) {
                                        html = '<input type="checkbox" id="isUsered" class="chooseBtn" checked/><label for="isUsered" class="choose-label" ></label>';
                                    } else {
                                        html = '<input type="checkbox" id="isUsered" class="chooseBtn" /><label for="isUsered" class="choose-label" ></label>';
                                    }
                                    return html;
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
                init: function() {
                    var that = this;
                    //按钮事件
                    var operateEvents = {
                        'click .edit-item': function (e, value, row, index) {
                            that.getProductById(row.id);
                            $(".cannot-change").prop('disabled', true);
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
                    $.getJSON("/StaticData/ProductCategory.json", function (data) {
                        that.allProductCategory = data
                    });
                    this.abpService.getSelectList().done(function (res) {
                        that.unitList = res.unitList
                        that.projectList = res.projectList
                    })
                },

                //根据id获取详情
                getProductById: function(id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.getProductById(postData).done(function (res) {
                        that.formItem = res;
                        that.projectIdChange();
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },

                //编辑和新增
                createItem: function() {
                    this.formItem = {};
                    $("#tab-edit a:first").trigger("click");
                },
                subFormData: function(e) {
                    e.preventDefault();
                    var _$form = $("#editItemForm");
                    _$form.validate();
                    if (!_$form.valid()) {
                        return;
                    }
                    var postData = this.formItem;
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.editProduct(postData).done(function () {
                        location.reload(true);
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },
                submitCancel: function (e) {
                    var that = this;
                    submitCancel(e.target);
                    if ($(e.target).attr("target") == "tab-edit") {
                        that.formItem = {};
                        that.productCategory = [];
                        $(".cannot-change").prop('disabled', false);
                    }
                },
                projectIdChange: function () {
                    var that = this
                    for (var key in that.projectList) {
                        if (that.projectList[key].id == that.formItem.projectId) {
                            that.productCategory = that.allProductCategory[that.projectList[key].axProjectId];
                        }
                    }
                },

                //删除一到多项
                deleteItem: function(params) {
                    var that = this
                    var postData = { "ids": params.ids }
                    abp.ui.setBusy($("#vue-app"));
                    that.abpService.deleteProduct(postData).done(function (res) {
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
                },

                stopProduct: function(id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.stopProduct(postData).done(function (res) {
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                }
            }
        })
    })
})();