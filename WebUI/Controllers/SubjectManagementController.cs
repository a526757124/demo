﻿using DomainModels;
using DomainService;
using QuerytDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility.Paged;
using WebUI.Common;

namespace WebUI.Controllers
{
    public class SubjectManagementController : BaseController
    {
        //
        // GET: /SubjectManagement/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(Subject model)
        {
            new SubjectService().Add(model);
            return Json(new AjaxResult("添加成功", AjaxResultType.Success, null));
        }
        public ActionResult Edit(Subject model)
        {
            new SubjectService().Update(model);
            return Json(new AjaxResult("修改成功", AjaxResultType.Success, null));
        }
        public ActionResult Delete(int id)
        {
            new SubjectService().Delete(id);
            return Json(new AjaxResult("删除成功", AjaxResultType.Success, null));
        }
        public ActionResult GetSubjectTypeTreeList()
        {
            var result = new SubjectTypeService().GetTreeList();
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, result));
        }
        public ActionResult GetTreeGridList(int? type)
        {
            var result = new SubjectService().GetTreeGridList(type);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, new { rows = result, total = 0 }));
        }
        public ActionResult GetSubjectById(int id)
        {
            var result = new SubjectService().GetById(id);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, result));
        }
        public ActionResult List(PagedParam<SubjectQuery> queryCond)
        {
            var result = new SubjectService().List(queryCond);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, new { rows = result, total = result.TotalItemCount }));
        }

        public ActionResult GetComboboxList()
        {
            var result = new SubjectCategoryService().GetComboboxList();
            return Json(result);
        }
        public ActionResult GetSubjectCategoryBalanceDirection(int id)
        {
            var result = new SubjectCategoryService().GetById(id);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success,result.BalanceDirection));
        }
    }
}