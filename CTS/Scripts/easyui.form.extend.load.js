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
    loadLabelData: function (jq, data) {
        var jq_id = jq.attr("id");
        for (var key in data) {
            if (!key)
                continue;
            var value = data[key];
            if (!value && !(typeof (value) === "boolean"))
                continue;
            if (value.constructor == Object) {
                //this.dealObject(value, jq_id, key);
                for (var k in value) {
                    if (!k)
                        continue;
                    var v = value[k];
                    var t_key = key + "." + k;
                    if (!v)
                        continue;
                    if (v.constructor == Object) {

                    } else {
                        this.getElementById(jq_id, t_key).html(v);
                        this.getElementByName(jq_id, key).val(value);
                    }
                }
            }
            else {
                this.getElementById(jq_id, key).html(value);
                this.getElementByName(jq_id, key).val(value);
            }
        }
    },
    //自定义加载数据
    loadData: function (jq, data) {
        var jq_id = jq.attr("id");
        for (var key in data) {
            if (!key)
                continue;
            var value = data[key];
            if (!value && !(typeof (value) === "boolean"))
                continue;
            if (value.constructor == Object) {
                this.dealObject(value, jq_id, key);
            }
            else {
                this.dealData(jq_id, key, value);
            }
        }
        var r = $(jq[0]).form('validate');
    },
    dealObject: function (v, jq_id, k) {
        for (var key in v) {
            if (!key)
                continue;
            var value = v[key];
            var t_key = k + "." + key;
            if (!value)
                continue;
            if (value.constructor == Object) {
                this.dealObject(value, jq_id, t_key);
            } else {
                this.dealData(jq_id, t_key, value);
            }
        }
    },
    dealData: function (jq_id, key, value) {
        //根据name
        var obj = this.getElementByName(jq_id, key);
        //根据id
        var objid = this.getElementById(jq_id, key);
        obj == obj.length > 0 ? obj : objid;
        if (obj.length > 0) {
            //my97日期控件
            if (obj.hasClass("Wdate")) {
                obj.val(com.DateFormatter(value, null, null));
                return;
            }
            //easyui 数据框
            //var objid = $("#" + key);

            if (obj.hasClass("easyui-numberbox")) {
                obj.numberbox('setValue', value);
                return;
            }
            //easyui 下拉列表
            var comobj = this.getElementByComboName(jq_id, key);
            if (comobj && (comobj.hasClass("easyui-combobox") || comobj.hasClass("combobox-f"))) {
                //是布耳值 false为0 true为1
                //if (typeof (value) === "boolean") {
                //    if (value) {
                //        comobj.combobox('setValue', 1);
                //    } else {
                //        comobj.combobox('setValue', 0);
                //    }
                //}
                comobj.combobox('setValue', value);
                return;
            }
            //文本框
            var textobj = this.getElementByTextBoxName(jq_id, key);
            if (textobj && (textobj.hasClass("easyui-textbox") || textobj.hasClass("textbox-f"))) {
                textobj.textbox("setValue", value);
                return;
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
                return;
            }

            obj.val(value);
        }
    },
    getElementByName: function (a, b) {
        return $("#" + a + " [name='" + b + "']")
    },
    getElementById: function (a, b) {
        return $("#" + a + " [id='" + b + "']")
    },
    //easyui 
    getElementByTextBoxName: function (a, b) {
        return $("#" + a + " [textboxname='" + b + "']")
    },
    getElementByComboName: function (a, b) {
        return $("#" + a + " [comboname='" + b + "']");
    }

});