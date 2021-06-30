using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_param;
using IOT.ETL.Model;
using NLog;
using IOT.ETL.Common;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sys_param_Controller : ControllerBase
    {
        //创建登录缓存关键字
        string LoginKey;
        //登录集合
        List<Model.sys_user> lstl = new List<Model.sys_user>();
        //实例化帮助文件
        RedisHelper<Model.sys_user> rl = new RedisHelper<Model.sys_user>();
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化日志

        private readonly sys_param_IRepository _param;
        public sys_param_Controller(sys_param_IRepository param)
        {
            _param = param;
            LoginKey = "loginlist";
            lstl = rl.GetList(LoginKey);
        }
        /// <summary>
        /// 绑定下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/BindParams")]
        public async Task<OkObjectResult> BindParams()
        {
            return Ok(await _param.BindParent());
        }




        /// <summary>
        /// 显示数据
        /// </summary>
        /// <returns></returns>
        /// 


        [HttpGet]
        [Route("/api/sys_param_show")]
        public async Task<OkObjectResult> sys_param_show(string pid = null)
        {
            //获取全部数据
            List<IOT.ETL.Model.sys_param> sys_Params = await _param.GetList_sys_param(pid);
            return Ok(new
            {
                msg = "",
                code = 0,
                data = sys_Params
            });
        }


        // [HttpGet]
        // [Route("/api/sys_param_show")]
        // public IActionResult sys_param_show(string pid=null)
        // {
        //     logger.Debug($"对数据库中的规则引擎表进行查看");
        //     List<Model.sys_param> list = _param.GetList_sys_param();
        //     return Ok(list);
        // }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/sys_param_insert")]
        public async Task<int> sys_param_insert([FromForm] Model.sys_param sys_Param)
        {
            int i =await _param.Insert_sys_param(sys_Param);
            if (i > 0)
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                //添加text文本日志
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
        public async Task<int> sys_param_delete(string ids)
        {
            int i =await _param.Delete_sys_param(ids);
            if (i > 0)
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                //添加text文本日志
                logger.Debug($"对数据库中的规则引擎表进行删除数据");
            }
            return i;
        }
        [HttpPost]
        [Route("/api/sys_param_uptdate")]
        public async Task<int> sys_param_uptdate([FromForm] Model.sys_param model)
        {
            int i =await _param.Uptdate_sys_param(model);
            if (i > 0)
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                //添加text文本日志
                logger.Debug($"对数据库中的规则引擎表进行修改数据");
            }
            return i;
        }
    }
}
