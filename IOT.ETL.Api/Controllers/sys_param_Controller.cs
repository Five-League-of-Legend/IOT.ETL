using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_param;
using IOT.ETL.Model;
using NLog;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sys_param_Controller : ControllerBase
    {
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化日志
        private readonly sys_param_IRepository _param;
        public sys_param_Controller(sys_param_IRepository param)
        {
            _param = param;
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/sys_param_show")]
        public IActionResult sys_param_show()
        {
            logger.Debug($"对数据库中的规则引擎表进行查看");
            List<Model.sys_param> list = _param.GetList_sys_param();
            return Ok(list);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/sys_param_insert")]
        public int sys_param_insert(Model.sys_param model)
        {
            int i = _param.Insert_sys_param(model);
            if (i > 0)
            {
                logger.Debug($"对数据库中的规则引擎表进行添加数据");
            }
            return i;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/sys_param_delete")]
        public int sys_param_delete(string ids)
        {
            int i = _param.Delete_sys_param(ids);
            if (i > 0)
            {
                logger.Debug($"对数据库中的规则引擎表进行删除数据");
            }
            return i;
        }
        [HttpPost]
        [Route("/api/sys_param_uptdate")]
        public int sys_param_uptdate([FromForm] Model.sys_param model)
        {
            int i = _param.Uptdate_sys_param(model);
            if (i > 0)
            {
                logger.Debug($"对数据库中的规则引擎表进行修改数据");
            }
            return i;
        }
    }
}
