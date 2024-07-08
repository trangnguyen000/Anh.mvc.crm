Vue.component('v-pagination', window['vue-plain-pagination'])

var app = new Vue({
    el: '#forDanhMuc',
    data: {
        title:'',
        titleModal: 'Hello Vue!',
        keyDanhMuc:'',
        submit: false,
        islogin: true,
        search: '',
        data: [],
        total: 1,
        currentPage: 1,
        modal: null,
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
        modelEdit: {
            Id: null,
            DisplayName: '',
            GroupCode:''
        }
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
                url: '/api/ApiBusiness/GetDanhMuc?key=' + self.keyDanhMuc + '&filter=' + self.search + '&page=' + self.currentPage,
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    console.log(result);
                    self.data = result.Data;
                    self.total = result.Total ;
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
        },
        showEdit: function (modal, item = undefined) {
            if (item) {
                this.titleModal = "Cập nhật chuyên mục";
                this.modelEdit = {
                    Id: item.Id,
                    DisplayName: item.DisplayName,
                    GroupCode: this.keyDanhMuc
                }
            }
            else {
                this.titleModal = "Thêm mới chuyên mục";
                this.modelEdit = {
                    Id: null,
                    DisplayName: '',
                    GroupCode: this.keyDanhMuc
                }
            }
            this.submit = false;
            this.modal = modal;
            $(modal).modal('show');
        },
        save: function () {
            var self = this;
            self.submit = true;
            if (!self.modelEdit.DisplayName) {
                this.Toast.fire({
                    icon: 'error',
                    title: 'Vui lòng kiểm tra thông tin'
                })
            }
            else {
                $.ajax({
                    url: '/api/ApiBusiness/SaveDanhMuc',
                    method: 'POST',
                    data:  self.modelEdit,
                    success: function (result) {
                        if (result == 1) {
                            self.Toast.fire({
                                icon: 'success',
                                title: 'Thành công!'
                            });
                            self.searchData();
                            $(self.modal).modal('hide');
                        }
                        else {
                            self.Toast.fire({
                                icon: 'error',
                                title: "Tên mục đã bị trùng"
                            })
                        }
                    }
                });
            }
        },
        deleteDanhMuc: function (item) {
            var self = this;
            $.confirm({
                title: '',
                content: '<div class="text-center">Bạn có chắc chắn muốn xóa?<br /> <h5 style="color:#17a2b8">' + item.DisplayName + '</h5></div>',
                buttons: {
                    tryAgain: {
                        text: 'Đồng ý',
                        btnClass: 'btn-red',
                        action: function () {
                            $.ajax({
                                url: '/api/ApiBusiness/DeleteDanhMuc',
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
                        action: function () {

                        }
                    }
                }
            });
        }

    },
    filters: {
        momentDate: function (date) {
            return date ? moment(date).format('DD/MM/yyyy') : '';
        }
    },
    computed: {
        Toast: {
            get: function () {
                return Swal.mixin({
                    toast: true,
                    position: 'bottom-end',
                    showConfirmButton: false,
                    timer: 3000
                })
            }
        },
        numberPage: {
            get: function () {
                return this.total % 10 == 0 ? this.total / 10 : parseInt(this.total / 10) + 1;
            }
        },
    },
    watch: {
        currentPage: function (newPage) {
            this.getData();
        }
    },
    beforeMount() {
        this.keyDanhMuc = $('#KeyDanhMuc').val();
        this.getData();
        this.title = "Danh mục";
        if (this.keyDanhMuc == 'KeyChuyenMucTinTuc') {
            this.title += " tin tức";
        }
        else if (this.keyDanhMuc == 'KeyDonVi') {
            this.title += " đơn vị";
        }
        else if (this.keyDanhMuc == 'KeyCoQuanBanHanh') {
            this.title += " cơ quan ban hành";
        }
        else if (this.keyDanhMuc == 'KeyVanBan') {
            this.title += " loại văn bản";
        }
    },
});