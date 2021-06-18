using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.etl_data_engine;
using IOT.ETL.Model;
using NLog;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class etl_data_engine_Controller : ControllerBase
    {
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化日志
        private readonly etl_data_engine_IRepository _edei;
        public etl_data_engine_Controller(etl_data_engine_IRepository edei)
        {
            _edei = edei;
        }
        /// <summary>
        /// 绑定下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/Binds")]
        public IActionResult Binds()
        {
            List<Model.etl_data_engine_type> list = _edei.Binds();
            return Ok(list);
        }
        /// <summary>
        /// 显示数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/etl_data_engine_show")]
        public IActionResult etl_data_engine_show(string rulename="",string codetype="",string clname="")
        {
            logger.Debug($"对数据库中的规则引擎表进行查看");
            List<Model.etl_data_engine> list = _edei.GetList_etl_data_engine();
            if (!string.IsNullOrEmpty(rulename))
            {
                list = list.Where(x => x.engine_name.Contains(rulename)).ToList();
            }
            if (!string.IsNullOrEmpty(codetype))
            {
                list = list.Where(x => x.code_type.Contains(codetype)).ToList();
            }
            if (!string.IsNullOrEmpty(clname))
            {
                list = list.Where(x => x.cl_name.Contains(clname)).ToList();
            }
            return Ok(list);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/etl_data_engine_insert")]
        public int etl_data_engine_insert([FromForm]Model.etl_data_engine model)
        {
            int i = _edei.Insert_etl_data_engine(model);
             if (i>0)
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
        [Route("/api/etl_data_engine_delete")]
        public int etl_data_engine_delete(string ids)
        {
            int i = _edei.Delete_etl_data_engine(ids);
            if (i > 0)
            {
                logger.Debug($"对数据库中的规则引擎表进行删除数据");
            }
            return i;
        }
        [HttpPost]
        [Route("/api/etl_data_engine_uptdate")]
        public int etl_data_engine_uptdate([FromForm]Model.etl_data_engine model)
        {
            int i = _edei.Uptdate_etl_data_engine(model);
            if (i > 0)
            {
                logger.Debug($"对数据库中的规则引擎表进行修改数据");
            }
            return i;
        }
    }
}
