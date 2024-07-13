Vue.component('v-pagination', window['vue-plain-pagination'])

var app = new Vue({
    el: '#forContactSupport',
    data: {
        submit: false,
        islogin: true,
        search: '',
        data: [],
        total: 1,
        currentPage: 1,
        modal: null,
        titleModal: '',
        modelEdit: {
            Id: null,
            CustomerName: '',
            PhoneNumber: '',
            EmailAddress: '',
            Description: '',
            FromDataSourceId: null,
            StudyProgramId: null,
            SupportEmployeeName: '',
            Note: '',
            Status: 1
        },
        listStudyProgram: [],
        listStudyProgramFilter:[],
        listStatus: [],
        bootstrapPaginationClasses: {
            ul: 'pagination pagination-sm m-0 float-right',
            li: 'page-item',
            liActive: 'active',
            liDisable: 'disabled',
            button: 'page-link'
        },
        paginationAnchorTexts: {
            first: '«',
            prev: '‹',
            next: '›',
            last: '»'
        },
        pageSize: $('#TypePage').val() == '1' ? 10 : 8
    },
    methods: {
        keymonitor: function (event) {
            if (event.key == "Enter") {
                this.searchData();
            }
        },

        searchData: function () {
            this.currentPage = 1;
            this.getData();
        },

        getData: function () {
            var self = this;
            $.ajax({
                url: '/api/ApiBusiness/GetDanhSachLienHe',
                method: 'POST',
                data: {
                    "Filter": self.search,
                    "PageIndex": self.currentPage,
                    "PageSize": self.pageSize,
                    "StudyProgramId": $('.selectStudyProgramFilter').val()
                },
                success: function (result) {
                    self.data = result.Data;
                    self.data.filter(o => o.CreationTime).map(o => o.CreationTimeText = moment(o.CreationTime).format('DD/MM/YYYY HH:mm'));
                    self.total = result.Total;
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
        },

        getDanhMuc: function () {
            var self = this;
            $.ajax({
                url: '/api/ApiBusiness/GetSuggestDanhMuc?key=KeyChuongTrinhHoc',
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    self.listStudyProgram = result;
                    self.listStudyProgramFilter = [...result];
                    self.listStudyProgramFilter.unshift({ Id: 0, DisplayName: "Tất cả" });
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
            $.ajax({
                url: '/api/ApiBusiness/GetStatus?key=KeyChuongTrinhHoc',
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    if (result.Success)
                        self.listStatus = result.Data;
                    else
                        self.listStatus = [];
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });

        },

        showEdit: function (modal, item = undefined) {
            if (item) {
                this.titleModal = "Cập nhật liên hệ";
                this.modelEdit = {
                    Id: item.Id,
                    CustomerName: item.CustomerName,
                    PhoneNumber: item.PhoneNumber,
                    EmailAddress: item.EmailAddress,
                    Description: item.Description,
                    FromDataSourceId: item.FromDataSourceId,
                    StudyProgramId: item.StudyProgramId,
                    SupportEmployeeName: item.SupportEmployeeName,
                    Note: item.Note,
                    Status: item.Status
                };
                $('.selectStudyProgram').val(item.StudyProgramId).trigger('change');
                $('.selectStatus').val(item.Status).trigger('change');
            }
            else {
                this.titleModal = "Thêm mới liên hệ";
                this.modelEdit = {};
            }
            this.submit = false;
            this.modal = modal;
            $(modal).modal('show');
        },
        deleteContact: function (item) {
            var self = this;
            $.confirm({
                title: '',
                content: '<div >Bạn có chắc chắn muốn xóa thông tin khách hàng <h5 style="color:#17a2b8" class="text-center" >' + item.CustomerName + '</h5></div>',
                buttons: {
                    tryAgain: {
                        text: 'Đồng ý',
                        btnClass: 'btn-red',
                        action: function () {
                            $.ajax({
                                url: '/api/ApiBusiness/DeleteContractSupport',
                                method: 'POST',
                                data: {
                                    "Id": item.Id
                                },
                                success: function (result) {
                                    self.Toast.fire({
                                        icon: 'success',
                                        title: 'Thành công!'
                                    });
                                    self.searchData();
                                }
                            });
                        }
                    },
                    close: {
                        text: 'Hủy',
                    }
                }
            });
        },

        save: function () {
            var self = this;
            self.submit = true;
            if (self.modelEdit.CustomerName || self.modelEdit.PhoneNumber) {
                self.modelEdit.StudyProgramId = $('.selectStudyProgram').val();
                self.modelEdit.Status = $('.selectStatus').val();
                $.ajax({
                    url: '/api/ApiBusiness/SaveContractSupport',
                    method: 'POST',
                    data: self.modelEdit,
                    success: function (result) {
                        self.Toast.fire({
                            icon: 'success',
                            title: 'Thành công!'
                        });
                        self.searchData();
                        $(self.modal).modal('hide');
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr);
                    }
                });
            }
            else {
                self.Toast.fire({
                    icon: 'error',
                    title: 'Vui lòng nhập đầy đủ thông tin'
                });
            }
        }
    },
    computed: {
        Toast: {
            get: function () {
                return Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000
                })
            }
        },
        numberPage: {
            get: function () {
                return this.total % this.pageSize == 0 ? this.total / this.pageSize : parseInt(this.total / this.pageSize) + 1;
            }
        },
    },
    watch: {
        currentPage: function (newPage) {
            this.getData();
        }
    },
    beforeMount() {
        this.getDanhMuc();
        this.getData();
    },
});
$(document).ready(function () {
    $('.selectStudyProgram').select2();
    $('.selectStatus').select2();
    $('.selectStudyProgramFilter').select2();

    $('.selectStudyProgramFilter').on('change', function (e) {
        app.searchData();
    });
});
