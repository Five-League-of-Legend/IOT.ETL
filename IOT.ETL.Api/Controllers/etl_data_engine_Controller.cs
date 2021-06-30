using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.etl_data_engine;
using IOT.ETL.Model;
using NLog;
using IOT.ETL.Common;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class etl_data_engine_Controller : ControllerBase
    {
        //实例化登录缓存
        RedisHelper<Model.sys_user> rh = new RedisHelper<Model.sys_user>();
        //缓存关键字
        string redisLogin;
        //登录的集合
        List<Model.sys_user> lst = new List<sys_user>();
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化日志
        private readonly etl_data_engine_IRepository _edei;
        public etl_data_engine_Controller(etl_data_engine_IRepository edei)
        {
            _edei = edei;
            redisLogin = "loginlist";
            lst= rh.GetList(redisLogin);
        }
        /// <summary>
        /// 绑定下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/Binds")]
        public async Task<OkObjectResult> Binds()
        {
            List<Model.etl_data_engine_type> list = await _edei.Binds();
            return Ok(list);
        }
        /// <summary>
        /// 显示数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/etl_data_engine_show")]
        public async Task<OkObjectResult> etl_data_engine_show(string rulename="",string codetype="",string clname="")
        {
            Model.sys_user user = lst.FirstOrDefault();
            logger.Debug($"{user.name}对数据库中的规则引擎表进行查看");
            List<Model.etl_data_engine> list = await _edei.GetList_etl_data_engine();
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
        public async Task<int> etl_data_engine_insert([FromForm]Model.etl_data_engine model)
        {
            int i = await _edei.Insert_etl_data_engine(model);
             if (i>0)
            {
                Model.sys_user user = lst.FirstOrDefault();
                logger.Debug($"{user.name}对数据库中的规则引擎表进行添加,规则引擎的名称为{model.engine_name}");
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
        public async Task<int> etl_data_engine_delete(string ids)
        {
            int i =await _edei.Delete_etl_data_engine(ids);
            if (i > 0)
            {
                Model.sys_user user = lst.FirstOrDefault();
                logger.Debug($"{user.name}对数据库中的规则引擎表进行删除,删除的ids为{ids}");
            }
            return i;
        }
        /// <summary>
        /// 编写代码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/Update_Codedata")]
        public async Task<int> Update_Codedata([FromForm]Model.etl_data_engine model)
        {
            int i =await _edei.Uptdate_code(model);
            if (i>0)
            {
                Model.sys_user user = lst.FirstOrDefault();
                logger.Debug($"{user.name}对数据库中的规则引擎表进行编写代码,编写代码的id的规则引擎id为{model.id}");
            }
            return i;
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/etl_data_engine_uptdate")]
        public async Task<int> etl_data_engine_uptdate([FromForm]Model.etl_data_engine model)
        {
            int i = await _edei.Uptdate_etl_data_engine(model);
            if (i > 0)
            {
                Model.sys_user user = lst.FirstOrDefault();
                logger.Debug($"{user.name}对数据库中的规则引擎表进行修改,修改的id为{model.id}");
            }
            return i;
        }
    }
}
