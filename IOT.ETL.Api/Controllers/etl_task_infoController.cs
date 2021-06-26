using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.etl_task_info;
using IOT.ETL.Model;
using NLog;
namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class etl_task_infoController : ControllerBase
    {
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化日志
        private readonly etl_task_info_IRepository _edei;
        public etl_task_infoController(etl_task_info_IRepository edei)
        {
            _edei = edei;
        }
        /// <summary>
        /// 显示数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/etl_task_info_show")]
        public async Task<OkObjectResult> etl_task_info_show()
        {
            logger.Debug($"对数据库中的任务信息表进行查看");
            List<Model.etl_task_info> list =await _edei.GetList_etl_task_info();
            return Ok(list);
        }
    }
}
