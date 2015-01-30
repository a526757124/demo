using CTS.Common;
using CTS.Dto;
using CTS.Models;
using CTS.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTS.Areas.SendManagement.Controllers
{
    public class SendController : BaseController
    {
        //
        // GET: /SendManagement/Send/
        private SendService _service = new SendService();
        public ActionResult Index()
        {
            return View();
        }
        #region 增删改查
        public ActionResult Add(Send model)
        {
            _service.Add(model);
            return Json("T");
        }
        public ActionResult Edit(Send model)
        {
            _service.Edit(model);
            return Json("T");
        }

        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Json("T");
        }
        public ActionResult GetById(int id)
        {
            return Json(_service.GetById(id));
        }

        public ActionResult List(PagedParam<SendQueryDto> queryCond)
        {
            var result = _service.List(queryCond);
            return Json(new { rows = result.ToList(), total = result.TotalItemCount });
        }
        #endregion

        public ActionResult SendOut(int Id)
        {
            try
            {
                _service.SendOut(Id, null, null);
            }
            catch (BusinessException ex)
            {
                return Json(ex.Message);
            }
            return Json("T");
        }
    }
}
