$.extend($.fn.form.methods, {
	submitData: function (jq, arg) {
	    $.messager.progress();	// 显示进度条
	    arg.successAlert = arg.successAlert || true;
		$(jq[0]).form('submit', {
			url: arg.url,
			onSubmit: function () {
				var isValid = $(this).form('enableValidation').form('validate');
				if (!isValid) {
					$.messager.progress('close');	// 如果表单是无效的则隐藏进度条
				}
				return isValid;
			},
			success: function (data) {
				var jsonData= JSON.parse(data);
				$.messager.progress('close');	// 如果提交成功则隐藏进度条
				if (jsonData.Type == "Success") {
					$(jq[0]).form('reset');//
					if (data.Data) {
						arg.success(jsonData.Data);
					} else {
						if (arg.successAlert) {
							$.messager.alert('提示', jsonData.Content);
						}
						arg.success(jsonData);
					}
				}
				if (jsonData.Type == "Error") {
					$.messager.alert('提示', jsonData.Content, 'error');
					arg.success(jsonData);
				}
			}
		});
	}
});