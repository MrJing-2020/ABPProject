(function () {
    $(function () {
        var app = new Vue({
            el: '#vue-app',
            data: {
                formItem: {},
                abpService: abp.services.app.user,
                abpRoleService: abp.services.app.role,
                $table: $('#table-data'),
                $remove: $('#table-remove'),
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
                                    field: 'userName',
                                    title: '用户名',
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
                                            '<button type="button" class="btn btn-orange btn-xs role-item">角色</button>',
                                            '<button type="button" class="btn btn-green btn-xs permission-item">权限</button>',
                                            '<button type="button" data-toggle="modal" data-target="#del-confirm" class="btn btn-danger btn-xs remove-item">删除</button>'
                                        ].join('');
                                    }
                                }
                        ],
                        delete: params.deleteItem
                    });
                },
                //初始化页面
                init() {
                    var that = this;
                    //按钮事件
                    var operateEvents = {
                        'click .role-item': function (e, value, row, index) {
                            var id = row.id;
                            that.setRoles();
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
                        $(".del-confirm").modal('hide');
                    });
                },
                createItem() {
                    this.formItem = {};
                    $("#tab-edit a:first").trigger("click");
                },
                setRoles() {
                    $("#tab-roles").css("display", "").find("a:first").trigger("click");
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