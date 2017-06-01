if (!window.contentWidth)
    window.contentWidth = 480;
var fnZoom = function (dw) {
    if (document.body) {
        var iw = $(window).width(); // document.documentElement.clientWidth;
        var r = iw / dw;
        if (document.body.style.zoom != r)
            document.body.style.zoom = r;
    }
};

fnZoom(window.contentWidth);
setInterval(function () {
    fnZoom(window.contentWidth);
}, 50);