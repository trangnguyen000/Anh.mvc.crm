
const app = new Vue({
    el: '#forTinTucEdit',
    data: {
        listChuyenMuc: [],
        title: "",
        defaultSelected: null,
        submit: false,
        modelEdit: {
            Id: null,
            TieuDe: null,
            MoTa: null,
            Anh: null,
            ChuyenMucId: null,
            NoiDung: null,
            IsViewHome: false,
            TypePage:null
        },
        typePage: {
            News: '1',
            Slider: '2'
        },
    },
    methods: {
        getDetailById: function (id) {
            var self = this;
            if (id) {
                self.title = "Cập nhật";
                $.ajax({
                    url: '/api/ApiBusiness/GetChiTietTinTucById?id=' + id,
                    contentType: "application/json; charset=utf-8",
                    method: 'GET',
                    success: function (result) {
                        var item = result[0];
                        self.modelEdit = {
                            Id: item.Id,
                            TieuDe: item.TieuDe,
                            MoTa: item.Mota,
                            Anh: item.Anh,
                            ChuyenMucId: item.ChuyenMucId,
                            NoiDung: item.NoiDung,
                            IsViewHome: item.IsViewHome
                        };
                        setTimeout(function () { CKEDITOR.instances['description'].setData(item.NoiDung); }, 500);

                        if (item.ChuyenMucId) {
                            $('.selectcareer').val(item.ChuyenMucId).trigger('change');
                        }
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr);
                    }
                });
            }
            else {
                self.title = "Thêm mới";
            }
        },
        changeImage: function (image) {
            this.modelEdit.Anh = image;
        },
        getDanhMuc: function () {
            var self = this;
            $.ajax({
                url: '/api/ApiBusiness/GetSuggestDanhMuc?key=KeyChuyenMucTinTuc',
                contentType: "application/json; charset=utf-8",
                method: 'GET',
                success: function (result) {
                    self.listChuyenMuc = result;
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }
            });
        },
        back: function () {
            var self = this;
            if ($('#TypePage').val() == self.typePage.News)
                window.location.href = '/Admin/Home/TinTuc';
            if ($('#TypePage').val() == self.typePage.Slider)
                window.location.href = '/Admin/Home/PageSlider';
        },
        save: function () {
            var self = this;
            self.submit = true;
            if (self.modelEdit.TieuDe || $('#TypePage').val() == self.typePage.Slider) {
                self.modelEdit.ChuyenMucId = $('.selectcareer').val();
                self.modelEdit.NoiDung = CKEDITOR.instances['description'].getData();
                self.modelEdit.TypePage = $('#TypePage').val();
                $.ajax({
                    url: '/api/ApiBusiness/SaveTinTuc',
                    method: 'POST',
                    data: self.modelEdit,
                    success: function (result) {
                        if (result == 1) {
                            self.Toast.fire({
                                icon: 'success',
                                title: 'Thành công!'
                            });
                            setTimeout(function () { self.back() }, 1000);
                        }
                        else {
                            self.Toast.fire({
                                icon: 'error',
                                title: 'Tiêu đề bị trùng'
                            })
                        }
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
        }
    },
    beforeMount() {
        this.getDanhMuc();
        this.getDetailById($('#NewsID').val());
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
    CKEDITOR.replace("description", { customConfig: "/Content/ckeditor/config.js", height: 700 });
})
$('.selectcareer').select2();