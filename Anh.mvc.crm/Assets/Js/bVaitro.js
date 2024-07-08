Vue.component('v-pagination', window['vue-plain-pagination'])

var app = new Vue({
    el: '#formVaiTro',
    data: {

        submit: false,
        search: '',
        data: [],
        dataPermissions:[],
        total: 1,
        modal: null,
        currentPage: 1,
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
        /// thêm mới - sửa
        modelEdit: {
            Id: null,
            DisplayName: null,
            IsDefault: false,
            Permissions:[]
        }
    },
    methods: {
        // tìm kiếm phân trang
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
                url: '/Admin/User/GetDanhSachRole?filter=' + this.search + '&page=' + this.currentPage,
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    self.data = result.Data;
                    self.data.map(o => o.Permissions = result.Permissions.filter(c => c.RoleId == o.Id).map(c => c.Name))
                    self.data.map(o => o.NgayCapNhat = moment(o.LastModificationTime).format('DD/MM/yyyy HH:mm'));
                    self.total = result.Total;
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
        },

        changePage: function (page) {
            if (this.data.length>0 && this.currentPage != page) {
                this.currentPage = page;
                this.getData();
            }
        },

        nextPage: function (page) {
            if (this.data.length > 0 && (this.currentPage + page) >= 1 && (this.currentPage + page) <= this.numberPage ) {
                this.currentPage = this.currentPage + page;
                this.getData();
            }
        },

        // get role
        getPermissions: function () {
            var self = this;
            $.ajax({
                url: '/Admin/User/GetPermissions',
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    console.log(result);
                    self.dataPermissions = result;
                    self.reloadPermissions();
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
        },

        reloadPermissions: function (aray = undefined) {
            var data = Object.assign([], this.dataPermissions);
             for (var i = 0; i < data.length; i++) {
                var item = data[i];
                item.Children.map(o => o.IsChecked = false);
                if (aray) {
                    item.Children.filter(o => aray.includes(o.KeyValue)).map(o => o.IsChecked = true);
                }
                item.IsChecked = item.Children.some(o => o.IsChecked);
            }
            this.dataPermissions = data;
        },

        changeCheckPermissions: function (item) {
            item.Children.map(o => o.IsChecked = item.IsChecked);
            item.Children = Object.assign([],item.Children );
        },

        changeCheckChildren: function (item) {
                if (item.Children.some(o => o.IsChecked)) {
                    item.IsChecked = true;
                }
                else {
                        item.IsChecked = false;
                }
                item.Children = Object.assign([], item.Children);
        },

        validateEmail: function (email) {
            const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return !email || re.test(String(email).toLowerCase());
        },

        showEdit: function (modal, item = undefined) {
            console.log(item);
            this.submit = false;
            if (item) {
                this.modelEdit = {
                    Id: item.Id,
                    DisplayName: item.DisplayName,
                    IsDefault: item.IsDefault
                }
                this.reloadPermissions(item.Permissions);
            }
            else {
                this.modelEdit = {
                    Id: null,
                    DisplayName: null,
                    IsDefault: false
                }
                this.reloadPermissions();
            }
            this.modal = modal;
            $(modal).modal('show');
        },

        huy: function (modal) {
            $(modal).modal('hide');
        },

        validateSave: function () {
            return !this.modelEdit.DisplayName ;

        },

        getPermissionsChecked: function() {
            var array = [];
            var data = this.dataPermissions.filter(o => o.IsChecked).map(o => o.Children);
            for (let i = 0; i < data.length; i++) {
                let item = data[i].filter(o => o.IsChecked).map(o => o.KeyValue);
                array = array.concat(item);
            }
            return array;
        },

        deleteVaiTro: function (item) {
            var self = this;
            $.confirm({
                title: '',
                content: '<div class="text-center">Bạn có chắc chắn muốn xóa vai trò<br /> <h5 style="color:#17a2b8">' + item.DisplayName+'</h5></div>',
                buttons: {
                    tryAgain: {
                        text: 'Đồng ý',
                        btnClass: 'btn-red',
                        action: function () {
                            $.ajax({
                                url: '/Admin/User/DeleteRole',
                                method: 'POST',
                                data: {
                                    "id": item.Id
                                },
                                success: function (result) {
                                    self.alertCallApi(result);
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
           
        },

        alertCallApi: function (result) {
            var self = this;
            if (result.Success) {
                self.Toast.fire({
                    icon: 'success',
                    title: 'Thành công!'
                });
                self.searchData();
                self.huy(self.modal);
            }
            else {
                self.Toast.fire({
                    icon: 'error',
                    title: result.Data
                })
            }
        },

        save: function () {
            this.submit = true;
            if (this.validateSave()) {
                this.Toast.fire({
                    icon: 'error',
                    title: 'Vui lòng kiểm tra thông tin'
                })
            }
            else {
                var self = this;
                self.modelEdit.Permissions = this.getPermissionsChecked();
                $.ajax({
                    url: '/Admin/User/CreateOfUpdateRole',
                    method: 'POST',
                    data: {
                        "model": self.modelEdit
                    },
                    success: function (result) {
                        self.alertCallApi(result);
                       
                    }
                });
            }
        }
    },
    watch: {
        currentPage: function (newQuestion, oldQuestion) {
            this.getData();
        }
    },
    computed: {
        isValidatePassword: {
            get: function () { return this.modelEdit.Password && this.modelEdit.PasswordAgain && this.modelEdit.Password.trim() != this.modelEdit.PasswordAgain.trim() }
        },

        isValidatePhone: {
            get: function () {
                var phoneNumberPattern = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
                return phoneNumberPattern.test(this.modelEdit.PhoneNumber)
            }
        },

        Toast:  {
            get: function () {
                return Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000
                })
            }
        },

        titleModal: {
            get: function () {
                return this.modelEdit.Id ? 'Cập nhật vai trò' : 'Thêm mới vai trò'
            }
        },

        numberPage:  {
            get: function () {
                return this.total % 10 == 0 ? this.total / 10 : parseInt(this.total / 10) + 1;
            }
        },

        pagination: {
            get: function () {
                var array = [];
                for (var i = 1; i <= this.numberPage; i++) {
                    if (array.length < 5) {
                        if ((this.currentPage - i) <= 2 || (i - this.currentPage) <= 2) {
                            array.push(i);
                        }
                    }
                }
                return array;
            }
        }
        
    },

    beforeMount() {
        this.getPermissions();
        this.getData();
    },
});