using IOT.ETL.IRepository.Etl_task_join_info;
using IOT.ETL.Model;
using IOT.ETL.Repository.Etl_task_join_info;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers
{
    public enum enum_DataBase
    {
        MySQL = 1,
        SqlServer = 2
    }
    [Route("api/[controller]")]
    [ApiController]
    public class Etl_task_join_infoController : ControllerBase
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Etl_task_join_infoIRepository _etl_Task_Join_InfoRepository;
        public Etl_task_join_infoController(Etl_task_join_infoIRepository etl_Task_Join_InfoRepository)
        {
            _etl_Task_Join_InfoRepository = etl_Task_Join_InfoRepository;
        }
        [HttpPost]
        [Route("/api/AddInputA")]
        //获取到第一个表的数据
        public async Task<int> AddInputA(etl_task_input_info ta)
        {
            int i = 0;   
            try
            {
                 i =await _etl_Task_Join_InfoRepository.AddInputA(ta);
            }
            catch (Exception ex)
            {
                if (i > 0)
                {
                    logger.Debug($"第一个表的数据获取成功");
                }
                else
                {
                    logger.Debug($"第一个表的数据获取失败,报错：{ex}");
                }
                throw;
            }
            return i;
        }
        [HttpPost]
        [Route("/api/AddInputB")]
        //获取到第二个表的数据
        public async Task<int> AddInputB(etl_task_input_info ta)
        {

            int i = 0;
            try
            {
                i =await _etl_Task_Join_InfoRepository.AddInputB(ta);
            }
            catch (Exception ex)
            {
                if (i > 0)
                {
                    logger.Debug($"第二个表的数据获取成功");
                }
                else
                {
                    logger.Debug($"第二个表的数据获取失败,报错：{ex}");
                }
                throw;
            }
            return i;
        }
        [HttpPost]
        [Route("/api/AddJoin")]
        //拼接sql语句
        public async Task<int> AddJoin(etl_task_join_info jo)
        {
            int i =await _etl_Task_Join_InfoRepository.AddJoin(jo);
            return i;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //根据数据库名称查出所有字段
        [HttpGet]
        [Route("/api/ShowField")]
        public async Task<OkObjectResult> ShowField(string database_name="")
        {
            return Ok(new { msg="",code=0,data=await _etl_Task_Join_InfoRepository.ShowField(database_name) });
        }
        [HttpGet]
        [Route("/api/FanFieldID")]
        //根据任务ID查询出其任务设计的相关信息   显示出对应字段 
        public async Task<OkObjectResult> FanFieldID(string id="")
        {
            return Ok(new { msg="",code=0,data=await _etl_Task_Join_InfoRepository.FanFieldID(id) });
        }
        [HttpGet]
        [Route("/api/SelectBind")]
        //根据ID和所属的数据库进行筛选   显示其中选中字段
        public async Task<OkObjectResult> SelectBind(string database_name="", string id="")
        {
            return Ok(new { msg="",code=0,data=await _etl_Task_Join_InfoRepository.SelectBind(database_name, id) });
        }
        [HttpGet]
        [Route("/api/OutputAllField")]
        //所有选中的字段输出
        public async Task<OkObjectResult> OutputAllField(string id="")
        {
            return Ok(new { msg="",code=0,data=await _etl_Task_Join_InfoRepository.OutputAllField(id) });
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //点击执行
        [HttpPost]
        [Route("/api/ExecuteAllSql")]
        public async Task<int> ExecuteAllSql(int flag=1,string id="",string name="")
        {
            try
            {
                enum_DataBase ed = (enum_DataBase)flag;
                int i = 0;
                switch (ed)
                {
                    case enum_DataBase.MySQL:
                        //获取全部数据
                        i =await _etl_Task_Join_InfoRepository.ExecuteAllSql(id,"");
                        break;
                    case enum_DataBase.SqlServer:
                        //获取全部数据
                        i =await _etl_Task_Join_InfoRepository.ExecuteAllSql(id, name);
                        break;
                }


                if (!string.IsNullOrEmpty(name))
                {
                    logger.Debug($"根据数据库名称，sql查询其数据库并返回，执行SQL为:sqlserve");
                }
                else
                {
                    logger.Debug($"根据数据库名称，sql查询其数据库并返回，执行SQL为:mysql");
                }
               

                return i;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
