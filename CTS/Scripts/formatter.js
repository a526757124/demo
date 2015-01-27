var com = com || {};
/*
 * 数据格式化
 *
 */
function dataInt(value) {
    return new Date(parseInt(value.slice(6, 19)));
}
//EasyUI用DataGrid用日期格式化 HH:MM:SS
com.TimeFormatter = function (value, rec, index) {
    if (value == undefined) {
        return "";
    }
    if (value.indexOf("Date") > 0) {
        value = dataInt(value);
    }
    if (value instanceof Date) {
        var h = value.getHours();
        var mim = value.getMinutes();
        var s = value.getSeconds();
        return (h < 10 ? ("0" + h) : h) +":"+ (mim < 10 ? ("0" + mim) : mim) +":"+ (s < 10 ? ("0" + s) : s);
    }
    /*json格式时间转js时间格式*/
    value = value.substr(11, 8);
    //var obj = eval('(' + "{Date: new " + value + "}" + ')');
    //var dateValue = obj["Date"];
    //if (dateValue.getFullYear() < 1900) {
    //    return "";
    //}
    //var val = dateValue.format("yyyy-mm-dd HH:MM");
    return value;
}
/**
 * yyyy-mm-dd HH:MM:SS
 */
com.DateTimeFormatter = function (value, rec, index) {
    if (value == undefined) {
        return "";
    }
    if (value.indexOf("Date")>0) {
        value=dataInt(value);
    }
    if (value instanceof Date) {
        var y = value.getFullYear();
        var m = value.getMonth() + 1;
        var d = value.getDate();
        var h = value.getHours();
        var mim = value.getMinutes();
        var s = value.getSeconds();
        return y + "-" + (m < 10 ? ("0" + m) : m) + "-" + (d < 10 ? ("0" + d) : d) + " " + (h < 10 ? ("0" + h) : h)
             +":"+ (mim < 10 ? ("0" + mim) : mim) +":"+ (s < 10 ? ("0" + s) : s);
    }
    /*json格式时间转js时间格式*/
    value = (value.substr(0, 10) + " " + value.substr(11, 8));
    //var obj = eval('(' + "{Date: new " + value + "}" + ')');
    //var dateValue = obj["Date"];
    //if (dateValue.getFullYear() < 1900) {
    //    return "";
    //}
    return value;
}
//EasyUI用DataGrid用日期格式化 yyyy-mm-dd
com.DateFormatter = function (value, rec, index) {
    if (value == undefined) {
        return "";
    }
    if (value.indexOf("Date") > 0) {
        value = dataInt(value);
    }
    if (value instanceof Date) {
        var y = value.getFullYear();
        var m = value.getMonth() + 1;
        var d = value.getDate();
        return y + "-" + (m < 10 ? ("0" + m) : m) + "-" + (d < 10 ? ("0" + d) : d) + "";
    }
    /*json格式时间转js时间格式*/
    value = value.substr(0, 10);
    //var obj = eval('(' + "{Date: new " + value + "}" + ')');
    //var dateValue = obj["Date"];
    //if (dateValue.getFullYear() < 1900) {
    //    return "";
    //}
    //var dateValue = new Date(value);
    //dateValue.toString()
    return value;
};
/**
 * 禁用启用
 */
com.BooleanFormatter = function (value, rec, index) {
    if (value == undefined) {
        return "禁用";
    }
    /*把True转为启用,false 转为禁用*/
    if (value) {
        return "启用";
    }
    return "禁用";
};
/**
 * 性别：男女
 */
com.SexFormatter = function (value, rec, index) {
    if (value == undefined) {
        return "保密";
    }
    if (value=="1") {
        return "男";
    }
    return "女";
};
/**
 * Icon图标显示
 */
com.IconFormatter = function (value, rec, index) {
    if (value == undefined) {
        return "";
    }
    return "<div id=\"BtnIconImg" + index + "\"  title=\"icon图标\" class=\"" + value + "\"></div>";
};
/**
 * 分隔
 */
com.SeparatorFormatter = function (value, rec, index) {
    if (value == undefined) {
        return "";
    }
    if (value)
        return "分隔";
    return "";
};
/**
 * 按钮类型
 */
com.BtnTypeFormatter = function (value, rec, index) {
    if (value == undefined) {
        return "";
    }
    if (value == 1)
        return "工具栏";
    if (value == 2)
        return "右键菜单";
    if (value == 3)
        return "Grid行按钮";
    return "";
};
com.ConclusionFormatter = function (value, rec, index) {
    if (value == 1) {
        return "通过";
    }
    if (value == 2) {
        return "继续调查";
    }
    return "不通过";
}
//数据转换为钱
com.MoneyFormatter = function (value, rec, index) {
    return formatMoney(value, 2, "￥");
};
//数据转换为钱
com.PercentFormatter = function (value, rec, index) {
    if (value) return (value += "%");
    return value;
};

// To set it up as a global function:
//(参数：数值,保留小数位数(2)，货币符号（$），整数部分千位分隔符(3)，小数分隔符(.))
function formatMoney(number, places, symbol, thousand, decimal) {
    number = number || 0;
    places = !isNaN(places = Math.abs(places)) ? places : 2;
    symbol = symbol !== undefined ? symbol : "$";
    thousand = thousand || ",";
    decimal = decimal || ".";
    var negative = number < 0 ? "-" : "",
        i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
}