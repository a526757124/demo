/// <reference path="../_references.js" />
var cashbag = cashbag || {};
cashbag.services = cashbag.services || {
    post: function (url, postData, success, arg, sync, error) {
        arg = arg || {nuload:false}
        this._functions.beforeCall(arg);
        var self = this;
        $.ajax({
            type: "POST",
            async: !sync,
            url: url,
            dataType: "json",
            contentType: "application/json",
            cache: false,
            data: JSON.stringify(postData),
            success: function (data) {
                self._functions.afterCall(arg, data);
                if (data.Type == "Success") {
                    if (data.Data||data.Data===0) {
                        success(data.Data);
                    } else {
                        $.messager.alert('提示', data.Content, 'info', function () {
                            success(data);
                        });
                        
                    }
                }
                if (data.Type == "Error") {
                    $.messager.alert('提示', data.Content, 'error', function () {
                        success(data);
                    });
                    
                }
                if (data.Type == "Warning") {
                    $.messager.alert('提示', data.Content, 'warning', function () {
                        success(data);
                    });
                    
                }
                if (data.Type == "Info") {
                    $.messager.alert('提示', data.Content, 'info', function () {
                        success(data);
                    });
                    
                }
                if (data.Type == "NoLogin") {
                    $.messager.alert('提示', data.Content, 'info', function () {
                        top.loginOpen();
                        success(data);
                    });
                    
                }
            },
            error: error || this._functions.error
        });
    },
    _functions: {
        error: function (error) {
            cashbag.services._functions.afterCall({}, error);
            $.messager.alert('错误', error.responseText, 'error');
        },
        beforeCall: function (arg) {
            if (!arg.nuload) {
                $.messager.progress({
                    onClose: function(){}
                });	// 显示进度条
            }
        },
        afterCall: function (arg, data) {
            if (!arg.nuload) {
                try {
                    $.messager.progress('close');
                } catch (e) {
                    $.messager.alert('错误', e, 'error');
                }	// 如果提交成功则隐藏进度条
            }
            if (typeof data === "object" && data.RedirectTo)
                location.href = data.RedirectTo;

        }
    }
};
