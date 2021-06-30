using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_role;
using NLog;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sys_rolesController : ControllerBase
    {
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化
        private readonly Isys_roleRepository _sys_roleRepository;
        public sys_rolesController(Isys_roleRepository sys_roleRepository)
        {
            _sys_roleRepository = sys_roleRepository;
        }
        [Route("/api/ShowRoles")]
        [HttpGet]
        public async Task<OkObjectResult> ShowRoles(string sname = "")
        {
            try
            {
                var ls =await _sys_roleRepository.ShowRoles();
                if (!string.IsNullOrEmpty(sname))
                {
                   
                    ls = ls.Where(x => x.role_name.Contains(sname)).ToList();
                }
                return Ok(new
                {
                    data = ls,
                    code = 0,
                    msg = ""

                });
            }
            catch (Exception)
            {

                throw;
            }


        }
        [Route("/api/DelRoles")]
        [HttpPost]
        public async Task<int> DelRoles(string id)
        {
            try
            {
                logger.Debug($"用户对秒杀配置进行删除,删除的配置ID为:{id}");
                return await _sys_roleRepository.DelRoles(id);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Route("/api/insertRoles")]
        [HttpPost]
        public async Task<int> insertRoles(Model.sys_role a)
        {
            try
            {
                logger.Debug($"用户对秒杀配置进行添加,添加的配置名称为:{a.role_name}");
                int i = await _sys_roleRepository.insertRoles(a);
                return i;
            }
            catch (Exception)
            {

                throw;
            }


        }
        [Route("/api/UpdateRoles")]
        [HttpPost]
        public async Task<int> UpdateRoles([FromForm]Model.sys_role a)
        {
            try
            {
                logger.Debug($"用户对秒杀配置进行修改,修改的配置ID为:{a.id}");
                int i = await _sys_roleRepository.UpdateRoles(a);
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }
      

     
    }
}
