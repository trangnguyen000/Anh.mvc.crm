Vue.component('v-pagination', window['vue-plain-pagination'])

var app = new Vue({
    el: '#forEmployee',
    data: {
        submit: false,
        search: '',
        data: [],
        total: 1,
        currentPage: 1,
        modal: null,
        titleModal: '',
        modelEdit: {},
        listGenders: [{ Key: 1, Value: 'Nam' }, { Key: 2, Value: 'Nữ' }],
        listActives: [{ Key: 1, Value: 'Đang Làm việc' }, { Key: 0, Value: 'Đã nghỉ' }],
        listlanguages: [{ Key: 'vi', Value: 'Vietnam' }, { Key: 'eng', Value: 'Korea' }],
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
                url: '/api/ApiBusiness/GetEmployeeByPaging?filter=' + this.search + '&page=' + this.currentPage + '&pageSize=' + this.pageSize,
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    self.data = result.Data;
                    self.data.filter(o => o.DateOfBirth).map(o => {
                        o.DateOfBirthText = moment(o.DateOfBirth).format('DD/MM/YYYY');
                        o.GenderName = self.listGenders.filter(c => c.Key == o.Gender)[0].Value;
                    });
                    self.total = result.Total;
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
        },

        getDanhMuc: function () {

        },
        changeImage: function (image) {
            this.modelEdit.Image = image;
        },
        showEdit: function (modal, item = undefined) {
            if (item) {
                this.titleModal = "Cập nhật nhân sự";
                this.modelEdit = item;
                $('.selectGender').val(item.Gender).trigger('change');
                $('.selectActive').val(item.IsActive === true ? 1 : 0).trigger('change');
                $('.selectlanguage').val(item.Language).trigger('change');
                if (item.DateOfBirth) {
                    var date = moment(item.DateOfBirth).format('DD/MM/YYYY');
                    $('#dateOfBirth').data('daterangepicker').setStartDate(date);
                }
                setTimeout(function () { CKEDITOR.instances['description'].setData(item.Note); }, 500);
            }
            else {
                this.titleModal = "Thêm mới nhân sự";
                this.modelEdit = { No: 1 };
                $('.selectActive').val(1).trigger('change');
                $('.selectlanguage').val('vi').trigger('change');
                $('.selectGender').val(1).trigger('change');
                setTimeout(function () { CKEDITOR.instances['description'].setData(''); }, 500);
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
            if (self.modelEdit.FullName) {
                self.modelEdit.Gender = $('.selectGender').val();
                self.modelEdit.IsActive = $('.selectActive').val() == 1;
                self.modelEdit.Note = CKEDITOR.instances['description'].getData();
                self.modelEdit.DateOfBirth = self.convertDate($('#dateOfBirth').val());
                self.modelEdit.Language = $('.selectlanguage').val();
                $.ajax({
                    url: '/api/ApiBusiness/SaveEmployee',
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
        },
        convertDate: function (date) {
            if (date) {
                var list = date.split("/");
                return list[1] + "/" + list[0] + "/" + list[2];
            }
            return null;
        },
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
$(function () {
    $('#exampleInputFile').click(function () {
        var ckfinder = new CKFinder();
        ckfinder.selectActionFunction = function (Images) {
            app.changeImage(Images);
        };
        ckfinder.popup();
    });
    CKEDITOR.replace("description", { customConfig: "/Content/ckeditor/config.js", height: 450 });
})
$(document).ready(function () {
    $('.selectGender').select2();
    $('.selectActive').select2();
    $('.selectlanguage').select2();

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
    $('#dateOfBirth').daterangepicker(
        {
            singleDatePicker: true,
            language: 'en',
            autoApply: true,
            "locale": local
        });
});
