var appChangePassWord = new Vue({
    el: '#modal-user-edit',
    data: {
        message: 'Hello Vue!',
        submit: false,
        islogin: true,
        modal: "#modal-user-edit",
        /// thêm mới - sửa
        modelEdit: {
            Id: null,
            FirstName: null,
            LastName: null,
            UserName: null,
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
        show: function () {
            this.submit = false;
            this.modelEdit = {
                Id: null,
                FirstName: null,
                LastName: null,
                UserName: null,
                DateOfBirth: null,
                PhoneNumber: null,
                EmailAddress: null,
                Password: null,
                PasswordAgain: null,
                IsActive: true,
                Roles: []
            };
            $(this.modal).modal('show');
        },
        validateSave: function () {
            return !this.modelEdit.Password
                || !this.modelEdit.PasswordAgain
                || this.isValidatePassword();
        },
        isValidatePassword: function () {
            if (
                !this.isNull(this.modelEdit.Password)
                && !this.isNull(this.modelEdit.PasswordAgain)
                && this.modelEdit.Password.trim() != this.modelEdit.PasswordAgain.trim()) {
                return true;
            }
            return false

        },
        isNull: function (item) {
            return item == undefined || item == null || item.toString().trim() == '';
        },
        alertCallApi: function (result) {
            var self = this;
            if (result.Success) {
                self.Toast.fire({
                    icon: 'success',
                    title: 'Thành công!'
                });
                self.submit = false;
                $(self.modal).modal('hide');
            }
            else {
                self.Toast.fire({
                    icon: 'error',
                    title: 'Đã xảy ra lỗi'
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
                $.ajax({
                    url: '/Admin/User/UpdatePassWord',
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
        Toast: {
            get: function () {
                return Swal.mixin({
                    toast: true,
                    position: 'bottom-end',
                    showConfirmButton: false,
                    timer: 3000
                })
            }
        }
    },
    beforeMount() {
    },
});

function showModalEditPassWord() {
    appChangePassWord.show();
}