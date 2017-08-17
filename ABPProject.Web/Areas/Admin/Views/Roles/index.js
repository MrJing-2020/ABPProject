(function () {
    $(function () {
        var app = new Vue({
            el: '#vue-app',
            data: {
                formItem: {},
                abpService: abp.services.app.role,
                deleteId: null,
                setPermissionRoleId: null,
                allPermissons: [],
                rolePermissions:[]
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
                                    events: params.operateEvents,
                                    formatter: function (value, row, index) {
                                        return [
                                            '<button type="button" class="btn btn-info btn-xs table-btn edit-item">编辑</button>',
                                            '<button type="button" class="btn btn-green btn-xs table-btn permission-item">权限</button>',
                                            '<button type="button" data-toggle="modal" data-target="#del-confirm" class="btn btn-danger btn-xs table-btn remove-item">删除</button>'
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
                            that.getRoleById(row.id);
                            $("#tab-edit a:first").trigger("click");
                        },
                        'click .permission-item': function (e, value, row, index) {
                            var id = row.id;
                            that.setPermission(id);
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
                getRoleById(id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.getRoleById(postData).done(function (res) {
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
                    this.abpService.editRole(postData).done(function () {
                        location.reload(true);
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },
                submitCancel(e) {
                    submitCancel(e.target);
                    if ($(e.target).attr("target") == "tab-edit") {
                        this.formItem = {};
                    }
                },

                //删除一到多项
                deleteItem(params) {
                    var that = this
                    var postData = { "ids": params.ids }
                    abp.ui.setBusy($("#vue-app"));
                    that.abpService.deleteRole(postData).done(function (res) {
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
                },

                //权限设置
                setPermission(id) {
                    var that = this;
                    $("#tab-permisssion").css("display", "").find("a:first").trigger("click");
                    that.setPermissionRoleId = id;
                    that.getPermissionsByRole(id)
                },
                getPermissionsByRole(id) {
                    var that = this
                    var postData = { "id": id }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.getPermissionsByRole(postData).done(function (res) {
                        that.allPermissons = res.allPermissons;
                        that.rolePermissions = res.rolePermissions
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                },
                subSetPermission() {
                    var that = this
                    var postData = { "RoleId": that.setPermissionRoleId, "GrantedPermissionNames": that.rolePermissions }
                    abp.ui.setBusy($("#vue-app"));
                    this.abpService.updateRolePermissions(postData).done(function () {
                        that.setRoleUserId = null
                        submitCancel();
                    }).always(function () {
                        abp.ui.clearBusy($("#vue-app"));
                    });
                }
            }
        })
    })
})();