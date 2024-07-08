var app = new Vue({
    el: '#formVue',
    data: {
        message: 'Hello Vue!',
        submit: false,
        islogin: true,
        model: {
            UserName: null,
            Password: null,
            RememberMe: true

        },
    },
        methods: {
            login: function () {
                this.submit = true;
                if (this.isValidateForm) {
                    var self = this
                    $.ajax({
                        url: '/Admin/Account/Login' ,
                        method: 'POST',
                        data: {
                            "loginView": this.model
                        },
                        success: function (data) {
                            if (data) {
                                window.location.href = '/Admin/Home/Index';
                            }
                            self.islogin = data;
                        }
                    });
                }
            }
    },
    computed: {
        isValidateForm: {
            get: function() { return this.model.UserName && this.model.Password }
        }
    }
});
$(window).keyup(function (e) {
    if (e.originalEvent && e.originalEvent.code == 'Enter') 
        {
        app.login();
        }
});