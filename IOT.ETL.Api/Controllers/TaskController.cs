using IOT.ETL.IRepository.TaskIRepository;
using IOT.ETL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private readonly TaskIRepository _taskIRepository;
        public TaskController(TaskIRepository taskIRepository)
        {
            _taskIRepository = taskIRepository;
        }
        [HttpGet]
        [Route("/api/ShowTask")]
        public async Task<OkObjectResult>  ShowTask(string nm = "", int weight = -1, int status = -2)
        {
            List<etl_task_info> ls = _taskIRepository.ShowTask();
            if (!string.IsNullOrEmpty(nm))
            {
                ls = ls.Where(x => x.Name.Contains(nm)).ToList();
            }
            if (weight != -1)
            {
                ls = ls.Where(x => x.Weight.Equals(weight)).ToList();
            }
            if (status != -2)
            {
                ls = ls.Where(x => x.Process_status.Equals(status)).ToList();
            }
            logger.Debug($"显示了任务管理的数据，时间：{DateTime.Now}");
            return  Ok(new { msg = "", code = 0, data = ls });
        }
        [HttpPost]
        [Route("/api/AddTask")]
        public async Task<int> AddTask([FromForm]IOT.ETL.Model.etl_task_info ta)
        {
            int i = 0;
            try
            {
                 i = _taskIRepository.AddTask(ta);
                logger.Debug($"添加数据成功了，时间：{DateTime.Now}");
            }
            catch (Exception ex)
            {
                logger.Debug($"添加数据时出现了错误,报错：{ex}，时间：{DateTime.Now}");
                throw;


            }
            return i;
        }
        [HttpGet]
        [Route("/api/DelTask")]
        public async Task<int> DelTask(string id="")
        {
            int i = 0;
            try
            {
                i = _taskIRepository.DelTask(id);
                logger.Debug($"任务管理中的数据成功删除了一条，时间：{DateTime.Now}");
            }
            catch (Exception ex)
            {
                logger.Debug($"任务管理数据删除的时候删除失败了，报错：{ex}，时间：{DateTime.Now}");
                throw;
            }
            return i;
        }
    }
}
