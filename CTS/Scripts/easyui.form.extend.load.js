$.extend($.fn.form.methods, {
    serialize: function (jq) {
        var arrayValue = $(jq[0]).serializeArray();
        var json = {};
        $.each(arrayValue, function () {
            var item = this;
            if (json[item["name"]]) {
                json[item["name"]] = json[item["name"]] + "," + item["value"];
            } else {
                json[item["name"]] = item["value"];
            }
        });
        return json;
    },
    getValue: function (jq, name) {
        var jsonValue = $(jq[0]).form("serialize");
        return jsonValue[name];
    },
    setValue: function (jq, name, value) {
        return jq.each(function () {
            _b(this, _29);
            var data = {};
            data[name] = value;
            $(this).form("load", data);
        });
    },
    //自定义加载数据
    loadData: function (jq, data) {
        var jq_id=jq.attr("id");
        for (var key in data) {
            //var obj = $('[name="' + key + '"]');
            var obj = this.getElementByName(jq_id, key)
            var value = data[key];
            if (obj.length > 0) {
                //my97日期控件
                if (obj.hasClass("Wdate")) {
                    obj.val(com.DateFormatter(value, null, null));
                    continue;
                }
                //easyui 数据框
                //var objid = $("#" + key);
                var objid = this.getElementById(jq_id, key);
                if (objid.hasClass("easyui-numberbox")) {
                    $("#" + key).numberbox('setValue', value);
                    continue;
                }
                //easyui 下拉列表
                if (objid.hasClass("easyui-combobox") || objid.hasClass("combobox-f")) {
                    $("#" + key).combobox('setValue', value);
                    continue;
                }
                //KindEditor富文件框
                var attr = obj.attr("kindeditor_data_kindeditor");
                if (attr || obj.hasClass("kind-editor")) {
                    if (KindEditor) {
                        var v = KindEditor.instances;
                        for (var ke in v) {
                            if (v[ke].id == key) {
                                v[ke].html($('<div/>').html(value).text());
                            }
                        }
                    } else {
                        console.warn("KindEditor富文件框未引用");
                    }
                    continue;
                }
                obj.val(value);
            }
        }
        var r = $(jq[0]).form('validate');
    },
    getElementByName: function (a, b) {
        return $("#" + a + " [name='" + b + "']")
    },
    getElementById: function (a, b) {
        return $("#" + a + " [id='" + b + "']")
    }
});