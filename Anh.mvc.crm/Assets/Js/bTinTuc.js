Vue.component('v-pagination', window['vue-plain-pagination'])

var app = new Vue({
    el: '#forTinTuc',
    data: {
        message: 'Hello Vue!',

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
        typePage: {
            News: '1',
            Slikder: '2'
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
                url: '/api/ApiBusiness/GetDanhSachTinTuc?typePage=' + $('#TypePage').val() + '&filter=' + this.search + '&page=' + this.currentPage + '&pageSize=' + this.pageSize,
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    self.data = result.Data;
                    self.data.filter(o => o.CreationTime).map(o => o.CreationTimeText = moment(o.CreationTime).format('DD/MM/yyyy'));
                    self.total = result.Total;
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
        },
        showEdit: function (id) {
            var self = this;
            if ($('#TypePage').val() == self.typePage.News)
                window.location.href = '/Admin/Home/EditTinTuc/' + id;
            if ($('#TypePage').val() == self.typePage.Slikder)
                window.location.href = '/Admin/Home/PageSliderEdit/' + id;
        },
        deleteTinTuc: function (item) {
            var self = this;
            $.confirm({
                title: '',
                content: '<div class="text-center">Bạn có chắc chắn muốn xóa tin<br /> <h5 style="color:#17a2b8">' + item.TieuDe + '</h5></div>',
                buttons: {
                    tryAgain: {
                        text: 'Đồng ý',
                        btnClass: 'btn-red',
                        action: function () {
                            $.ajax({
                                url: '/api/ApiBusiness/DeleteTinTuc',
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
        this.getData();
    },
});
$('#reservation').daterangepicker({ singleDatePicker: true });