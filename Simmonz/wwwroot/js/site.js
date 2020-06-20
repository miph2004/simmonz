// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
jQueryAjaxDelete = form => {
    if (confirm('Bạn có muốn xóa không ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('.view-all').html(res.html);
                },
                error: function (err) {
                    console.log(err);
                }

            })
        } catch (e) {
            console.log(e)
        }

    }
    return false;
};
