/// <reference path="../_references.js" />
var cashbag = cashbag || {};
cashbag.services = cashbag.services || {
    getHtml: function (url, success, sync, error) {
        this._functions.beforeCall();
        var self = this;
        $.ajax({
            type: "GET",
            async: !sync,
            url: url,
            dataType: "html",
            cache: false,
            success: function (data) {
                var result = data;
                try {
                    result = $.parseJSON(data);
                } catch (ex) { }
                self._functions.afterCall(result);
                success(result);
            },
            error: error || this._functions.error
        });
    },
    get: function (url, success, sync, error) {
        this._functions.beforeCall();
        var self = this;
        $.ajax({
            type: "GET",
            async: !sync,
            url: url,
            dataType: "json",
            cache: false,
            success: function (data) {
                self._functions.afterCall(data);
                success(data);
            },
            error: error || this._functions.error
        });
    },
    post: function (url, postData, success, sync, error) {
        this._functions.beforeCall();
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
                self._functions.afterCall(data);
                success(data);
            },
            error: error || this._functions.error
        });
    },
    postForm: function ($form, success, error) {
        this._functions.beforeCall();
        var self = this;
        $form.ajaxSubmit({
            dataType: "text",
            iframe: true,
            success: function (data, status, xhr) {
                var json = $.parseJSON(data);
                if (json && json.error) {
                    if (error) error(json.error);
                    else $.messager.alert('错误', json.error, 'error');
                } else {
                    success(json);
                }
                self._functions.afterCall(json);
            }
        });
    },
    _functions: {
        error: function (error) {
            cashbag.services._functions.afterCall(error);
            $.messager.alert('错误', error.responseText, 'error');
        },
        beforeCall: function () {
            //$.blockUI({ message: '<h1>请等待...</h1>', baseZ: 999999 });
        },
        afterCall: function (data) {
            //$.unblockUI();
            if (typeof data === "object" && data.RedirectTo)
                location.href = data.RedirectTo;

        }
    }
};
