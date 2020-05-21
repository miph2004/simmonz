var user = {
    init: function () {
        user.registerEvents();
    },
    //Bắt sự kiện
    registerEvents: function () {
        $('.btn-statusactive').off('click').on('click', function(e)
        {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            
            $.ajax({
                url: "/User/ChangeStatus",
                data: { id: id },
                datatype: "json",
                type: "POST",
                success: function (response) {
                    console.log(response)
                    if (response.status == true) {
                        btn.text('Kich hoạt');

                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
                
            
        });
    }
}
user.init();