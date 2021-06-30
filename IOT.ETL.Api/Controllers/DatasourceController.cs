using IOT.ETL.Common;
using IOT.ETL.IRepository.IdatasourceRepository;
using IOT.ETL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers.DatasourceController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatasourceController : ControllerBase
    {
        string rideskey = "joinjoin";
        RedisHelper<string> rh = new RedisHelper<string>();
        List<Model.etl_datasource_info> list = new List<etl_datasource_info>();
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化日志
        /// <summary>
        /// 注入 IdatasourceRepository
        /// </summary>
        private readonly IdatasourceRepository _datasour;
        public DatasourceController(IdatasourceRepository datasour)
        {
            _datasour = datasour;
        }
        /// <summary>
        /// redis && mysql 数据源添加
        /// </summary> v
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DatasourceAdd")]
        public async Task<IActionResult> DatasourceAdd(etl_datasource_info m)
        {
            try
            {
                int add = await _datasour.DatasourceAdd(m);
                if (add > 0)
                {
                    logger.Debug($"数据源添加了一条数据");

                    using (IRedisClient client = new RedisClient("127.0.0.1", 6379))
                    {
                        client.AppendToValue(rideskey,Convert.ToString(m));
                    }
                }
                return Ok(new { data = add });
            }
            catch (Exception)
            {
                logger.Debug($"数据源添加报错");
                throw;
            }
        }
        /// <summary>
        /// 数据源修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DatasourceUpt")]
        public async Task<IActionResult> DatasourceUpt(etl_datasource_info m)
        {
            try
            {
                int upt = await _datasour.DatasourceUpt(m);
                logger.Debug($"数据源修改成功");
                return Ok(new { data = upt });
            }
            catch (Exception)
            {
                logger.Debug($"数据源修改报错");
                throw;
            }
        }
        /// <summary>
        /// 数据源删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Del")]
        public async Task<IActionResult> Del(string ids)
        {
            try
            {
                int del = await _datasour.DatasourceDel(ids);
                logger.Debug($"数据源删除成功");
                return Ok(new { data = del });
            }
            catch (Exception)
            {
                logger.Debug($"数据源删除失败");
                throw;
            }
        }
        /// <summary>
        /// 显示列表 + 查询
        /// </summary>
        /// <param name="miaoshu"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DatasourceList")]
        public async Task<IActionResult> DatasourceList(string describe = null,int typeid = 0)
        {
            try
            {

                //查询全部数据  
                List<Model.etl_datasource_info> info = await _datasour.DatasourceList();

                using (IRedisClient client = new RedisClient("127.0.0.1", 6379))
                {
                    client.Set(rideskey, info);
                }

                if (describe != null)
                {
                    rideskey = null;
                    info = info.Where(s => s.describes.Contains(describe)).ToList();
                    logger.Debug($"数据源查询数据源描述成功");
                }
                if (typeid != 0)
                {
                    info = info.Where(s => s.type == typeid).ToList();
                    logger.Debug($"数据源查询数据源类型成功");
                }
                return Ok(new { data = info });
            }
            catch (Exception)
            {
                logger.Debug($"数据源查询报错");
                throw;
            }
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Getdatasource_type")]
        public async Task<IActionResult> Getdatasource_type()
        {
            try
            {
                List<Model.etl_datasource_type> list = await _datasour.Getdatasource_type();
                return Ok(new { data = list });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CommWhy")]
        public async Task<IActionResult> CommWhy(string url, string pwd, string mysqlname)
        {
            int i = await _datasour.CommWhy(url, pwd, mysqlname);
            return Ok(new { data = i });
        }

    }
}
