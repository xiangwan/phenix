function getiev() {
    var userAgent = window.navigator.userAgent.toLowerCase();
    $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
    $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
    $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
    var v;
    if ($.browser.msie8) {
        v = 8;
    } else if ($.browser.msie7) {
        v = 7;
    } else if ($.browser.msie6) {
        v = 6;
    } else {
        v = -1;
    }
    return v;
}

$(document).ready(function() {
    var v = getiev()
    if (v > 0) {
        $(document.body).addClass("ie ie" + v);
    }
});

//获取网站更目录
function Application_GetRoot() {

    var pathArr = window.location.pathname.split("/");

    if (pathArr.length == 1) {
        return "/";
    } else if (pathArr.length == 2) {
        return pathArr[0];
    } else {

        if (pathArr[0] == "") { //模式对话框

            if (pathArr[2].indexOf("(") > -1 && pathArr[2].indexOf(")") > -1)
                return "/" + pathArr[1] + "/" + pathArr[2];
            else
                return "/" + pathArr[1];

        } else {

            if (pathArr[2].indexOf("(") > -1 && pathArr[2].indexOf(")") > -1)
                return "/" + pathArr[0] + "/" + pathArr[1];
            else
                return "/" + pathArr[0];

        }
    }
}

/*********************动态载入JS Satrt************************/
function ansyloadJS(url, onload) {
    var domscript = document.createElement('script');
    domscript.src = url;
    if (!!onload) {
        domscript.afterLoad = onload;
        domscript.onreadystatechange = function() {
            if (domscript.readyState == "loaded" || domscript.readyState == "complete" || domscript.readyState == "interactive") {
                domscript.afterLoad();
            }
        }
        domscript.onload = function() {
            if (!!domscript.afterLoad)
                domscript.afterLoad();
        }
    }
    document.getElementsByTagName('head')[0].appendChild(domscript);
}

/*********************动态载入JS End************************/
var popUpWin;

function PopUpCenterWindow(URLStr, width, height, newWin, scrollbars) {
    var popUpWin = 0;
    if (typeof (newWin) == "undefined") {
        newWin = false;
    }
    if (typeof (scrollbars) == "undefined") {
        scrollbars = 0;
    }
    if (typeof (width) == "undefined") {
        width = 800;
    }
    if (typeof (height) == "undefined") {
        height = 600;
    }
    var left = 0;
    var top = 0;
    if (screen.width >= width) {
        left = Math.floor((screen.width - width) / 2);
    }
    if (screen.height >= height) {
        top = Math.floor((screen.height - height) / 2);
    }
    if (newWin) {
        open(URLStr, '', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=' + scrollbars + ',resizable=yes,copyhistory=yes,width=' + width + ',height=' + height + ',left=' + left + ', top=' + top + ',screenX=' + left + ',screenY=' + top + '');
        return;
    }

    if (popUpWin) {
        if (!popUpWin.closed) popUpWin.close();
    }
    popUpWin = open(URLStr, 'popUpWin', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=' + scrollbars + ',resizable=yes,copyhistory=yes,width=' + width + ',height=' + height + ',left=' + left + ', top=' + top + ',screenX=' + left + ',screenY=' + top + '');
    popUpWin.focus();
}

function OpenModalDialog(url, option) {
    option.type = 2;
    if ($.ShowIfrmDailog) {
        $.ShowIfrmDailog(url, option);
    }
}

function CloseModalDialog(callback, dooptioncallback, userstate) {
    if (parent && parent.$.closeIfrm) {
        parent.$.closeIfrm(callback, dooptioncallback, userstate);
    }
}

function OpenModelWindow(url, option) {
    var fun;
    try {
        if (parent != null && parent.$ != null && parent.$.ShowIfrmDailog != undefined) {
            fun = parent.$.ShowIfrmDailog
        } else {
            fun = $.ShowIfrmDailog;
        }
    } catch (e) {
        fun = $.ShowIfrmDailog;
    }
    fun(url, option);
}

function CloseModelWindow(callback, dooptioncallback, userstate) {
    if (parent) {
        parent.$.closeIfrm(callback, dooptioncallback, userstate);
    } else {
        window.close();
    }
}


function StrFormat(temp, dataarry) {
    return temp.replace(/\{([\d]+)\}/g, function(s1, s2) {
        var s = dataarry[s2];
        if (typeof (s) != "undefined") {
            if (s instanceof (Date)) {
                return s.getTimezoneOffset()
            } else {
                return encodeURIComponent(s)
            }
        } else {
            return ""
        }
    });
}

function StrFormatNoEncode(temp, dataarry) {
    return temp.replace(/\{([\d]+)\}/g, function(s1, s2) {
        var s = dataarry[s2];
        if (typeof (s) != "undefined") {
            if (s instanceof (Date)) {
                return s.getTimezoneOffset()
            } else {
                return (s);
            }
        } else {
            return "";
        }
    });
}

function showLoadingMsg(msg, position, isautohide, timeout) {
    var loadpanel = $("#loadpanel");
    if (loadpanel.length == 0) {
        loadpanel = $("<div id=\"loadpanel\" class=\"loadingpanel\"/>").appendTo("body");
    }
    loadpanel.html("<span>" + msg + "</span>");
    if (!position) {
        position = { right: 20, top: 3 };
    }
    loadpanel.css(position).show();
    if (isautohide) {
        showLoadTipTimerId = setTimeout(hideLoadingMsg, timeout);
    }
}

function hideLoadingMsg() {
    var loadpanel = $("#loadpanel");
    if (loadpanel.length > 0) {
        loadpanel.hide();
    }
}

var showErrorTipTimerId;
var showLoadTipTimerId;
var msg;

function showErrorTip(msg, position, isautohide, timeout) {
    var errorpanel = $("#errorpanel");
    if (errorpanel.length == 0) {
        errorpanel = $("<div id=\"errorpanel\" class=\"errorpanel\"/>").appendTo("body");
    }
    if (errorpanel.css("display") != "none") {
        errorpanel.find("* > dt").append("<dl>" + msg + "</dl>");
        if (showErrorTipTimerId) {
            window.clearTimeout(showErrorTipTimerId);
        }
    } else {
        errorpanel.html("<dt><dl>" + msg + "</dl></dt>");
        if (!position) {
            position = { right: 20, top: 3 };
        }
        errorpanel.css(position).fadeIn();
    }
    if (isautohide) {
        showErrorTipTimerId = setTimeout(hideErrortip, timeout);
    }

}

function hideErrortip() {
    var errorpanel = $("#errorpanel");
    if (errorpanel.length > 0) {
        errorpanel.fadeOut();
    }
}

function removeParent() {
    $(this).parent().hide();
    return false;
}

function showValidateError(error, element) {
    //var close = $("<a href=\"javascript:void(0)\" class=\"valiclose\">&nbsp;</a>").click(removeParent);  
    var pos = element.position();
    var height = element.height();
    if (pos.left + 155 >= document.documentElement.clientWidth) {
        pos.left = document.documentElement.clientWidth - 156;
    }
    var newpos = { left: pos.left, top: pos.top + height + 2 }
    error.appendTo("#fmEdit").css(newpos);
}