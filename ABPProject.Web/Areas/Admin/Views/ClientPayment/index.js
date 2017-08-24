(function () {
    $(function () {
        var app = new Vue({
            el: '#vue-app',
            data: {},
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
                                field: 'salesOrderNum',
                                title: '订单编号',
                                sortable: true,
                                align: 'center'
                            },
                            {
                                field: 'clientName',
                                title: '客户',
                                sortable: true,
                                align: 'center'
                            },
                            {
                                field: 'paymentSum',
                                title: '金额',
                                align: 'right'
                            },
                            {
                                field: 'creatorUserName',
                                title: '提交用户',
                                align: 'center'
                            },
                            {
                                field: 'creationTime',
                                title: '日期',
                                sortable: true,
                                align: 'center',
                                formatter: function (value, row, index) {
                                    return value.replace("T", " ").substring(0, value.lastIndexOf("."));
                                }
                            },
                            {
                                field: 'state',
                                title: '状态',
                                sortable: true,
                                align: 'center',
                                formatter: function (value, row, index) {
                                    return '<span class="badge badge-green">新添加</span>';
                                }
                            },
                            {
                                field: 'option',
                                title: '操作',
                                align: 'center',
                                events: params.operateEvents,
                                formatter: function (value, row, index) {
                                    return [
                                        '<button type="button" class="btn btn-green btn-xs table-btn detail-item">查看</button>'
                                    ].join('');
                                }
                            }
                        ],
                        delete: params.deleteItem,
                        detailFormatter: params.detailFormatter
                    });
                },
                //初始化页面
                init: function () {
                    var that = this;
                    //按钮事件
                    var operateEvents = {
                        'click .detail-item': function (e, value, row, index) {
                            window.location.href = "/Admin/ClientPayment/Detail?id=" + row.id;
                        },
                    }
                    that.initTableData({
                        operateEvents: operateEvents
                    });
                }
            }
        })
    })
})();