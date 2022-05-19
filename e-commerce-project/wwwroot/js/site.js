getOrderCount()
function getOrderCount() {
    $.ajax({
        url: "/Cart/getOrderCountWhenNotId/",
        success: function (res) {
            if (res.count) {
                updateCartCount(res.count)
            }
        }
    });
}
