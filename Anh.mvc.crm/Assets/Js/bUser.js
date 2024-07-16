Vue.component('v-pagination', window['vue-plain-pagination'])

var app = new Vue({
    el: '#formuser',
    data: {
        message: 'Hello Vue!',

        submit: false,
        islogin: true,
        search: '',
        data: [],
        dataRole: [],
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
        /// thêm mới - sửa
        modelEdit: {
            Id: null,
            FirstName: null,
            LastName: null,
            UserName: null,
            FullName: null,
            DonVi: null,
            DateOfBirth: null,
            PhoneNumber: null,
            EmailAddress: null,
            Password: null,
            PasswordAgain: null,
            IsActive: false,
            Roles: []
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
                url: '/Admin/User/GetDanhSachUser?filter=' + this.search + '&page=' + this.currentPage,
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    console.log(result);
                    self.data = result.Data;
                    self.data.map(o => o.Roles = result.Roles.filter(c => c.UserId == o.Id).map(c => c.RoleId));
                    self.data.filter(o => o.DateOfBirth).map(o => o.DateOfBirthText = moment(o.DateOfBirth).format('DD/MM/yyyy'));
                    self.total = result.Total;
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
        },

        getRoles: function () {
            var self = this;
            $.ajax({
                url: '/Admin/User/GetRoleAll',
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    self.dataRole = result.Data;
                    self.reloadRole();
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
        },

        reloadRole: function (aray = undefined) {
            this.dataRole.map(o => o.IsChecked = false);
            if (this.modelEdit.Id) {
                if (aray) {
                    this.dataRole.filter(o => aray.includes(o.Id)).map(o => o.IsChecked = true);
                }
            }
            else {
                this.dataRole.filter(o => o.IsDefault).map(o => o.IsChecked = true);
            }

        },

        converKeyCheckbox: function (index) {
            return 'KeyCheckBox' + index;
        },

        validateSave: function () {
            return !this.modelEdit.FullName
              /*  || !this.modelEdit.LastName*/
                || !this.modelEdit.UserName
                || !this.isValidatePhone
                || (this.modelEdit.EmailAddress  && !this.validateEmail(this.modelEdit.EmailAddress))
                || (!this.modelEdit.Id && !this.modelEdit.Password)
                || (!this.modelEdit.Id && !this.modelEdit.PasswordAgain)
                || this.isValidatePassword
                || this.isValidatePasswordEdit();

        },

        isValidatePasswordEdit: function () {
            return !this.isNull(this.modelEdit.Id)
                && (
                    (!this.isNull(this.modelEdit.Password) && !this.modelEdit.PasswordAgain)
                    || (!this.modelEdit.Password && !this.isNull(this.modelEdit.PasswordAgain))
                );
        },

        isNull: function (item) {
            return item == undefined || item == null || item.toString().trim() == '';
        },

        validateEmail: function (email) {
            const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return !email || re.test(String(email).toLowerCase());
        },

        showEdit: function (modal, item = undefined) {
            this.submit = false;
            if (item) {
                this.modelEdit = {
                    Id: item.Id,
                    FirstName: item.FirstName,
                    LastName: item.LastName,
                    UserName: item.UserName,
                    FullName: item.FullName,
                    DonVi: item.DonVi,
                    PhoneNumber: item.PhoneNumber,
                    EmailAddress: item.EmailAddress,
                    Password: null,
                    PasswordAgain: null,
                    IsActive: item.IsActive,
                    Roles: item.Roles
                };
                this.reloadRole(item.Roles);
                if (item.DateOfBirth) {
                    var date = moment(item.DateOfBirth).format('DD/MM/YYYY');
                    $('#reservation').data('daterangepicker').setStartDate(date);
                }
            }
            else {
                this.modelEdit = {
                    Id: null,
                    FirstName: null,
                    LastName: null,
                    FullName: null,
                    UserName: null,
                    DonVi: null,
                    DateOfBirth: null,
                    PhoneNumber: null,
                    EmailAddress: null,
                    Password: null,
                    PasswordAgain: null,
                    IsActive: true,
                    Roles: []
                };
                this.reloadRole();
            }
            this.modal = modal;
            $('#custom-tabs-three-home-tab').trigger('click');
            $(modal).modal('show');
        },

        huy: function (modal) {
            $(modal).modal('hide');
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

        deleteUser: function (item) {
            var self = this;
            $.confirm({
                title: '',
                content: '<div class="text-center">Bạn có chắc chắn muốn xóa nhân sự<br /> <h5 style="color:#17a2b8">' + item.FullName + '</h5></div>',
                buttons: {
                    tryAgain: {
                        text: 'Đồng ý',
                        btnClass: 'btn-red',
                        action: function () {
                            $.ajax({
                                url: '/Admin/User/DeleteUser',
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

        getRoleChecked: function () {
            return this.dataRole.some(o => o.IsChecked) ? this.dataRole.filter(o => o.IsChecked).map(o => o.Id) : null;
        },
        convertDate: function (date) {
            if (date) {
                var list = date.split("/");
                return list[1] + "/" + list[0] + "/" + list[2];
            }
            return null;
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
                self.modelEdit.Roles = self.getRoleChecked();

                var listName = self.modelEdit.FullName.split(" ").filter(o => o);

                self.modelEdit.LastName = listName[listName.length - 1];
                 listName.splice(listName.length - 1, 1);
                self.modelEdit.FirstName = listName.join(" ");
                
                self.modelEdit.DateOfBirth = self.convertDate($('#reservation').val());
                $.ajax({
                    url: '/Admin/User/CreateOfUpdateUser',
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
    computed: {
        isValidatePassword: {
            get: function () { return !this.isNull(this.modelEdit.Password) && !this.isNull(this.modelEdit.PasswordAgain) && this.modelEdit.Password.trim() != this.modelEdit.PasswordAgain.trim() }
        },

        isValidatePhone: {
            get: function () {
                var phoneNumberPattern = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
                return phoneNumberPattern.test(this.modelEdit.PhoneNumber)
            }
        },
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
        titleModal: {
            get: function () {
                return this.modelEdit.Id ? 'Cập nhật tài khoản người dùng' : 'Thêm mới tài khoản người dùng'
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
        this.getRoles();
        this.getData();
    },
});
var local = {
    "format": "DD/MM/YYYY",
    "separator": " - ",
    "applyLabel": "Apply",
    "cancelLabel": "Cancel",
    "fromLabel": "From",
    "toLabel": "To",
    "customRangeLabel": "Custom",
    "weekLabel": "W",
    "daysOfWeek": [
        "CN",
        "T2",
        "T3",
        "T4",
        "T5",
        "T6",
        "T7"
    ],
    "monthNames": [
        "Tháng 1",
        "Tháng 2",
        "Tháng 3",
        "Tháng 4",
        "Tháng 5",
        "Tháng 6",
        "Tháng 7",
        "Tháng 8",
        "Tháng 9",
        "Tháng 10",
        "Tháng 11",
        "Tháng 12"
    ],
    "firstDay": 1
}
$('#reservation').daterangepicker(
    {
        singleDatePicker: true,
        language: 'en',
        autoApply: true,
        "locale": local
    });
