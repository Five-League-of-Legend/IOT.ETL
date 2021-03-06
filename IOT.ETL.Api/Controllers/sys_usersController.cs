using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_user;
using NLog;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sys_usersController : ControllerBase
    {
        Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly Isys_userRepository _sys_userRepository;
        public sys_usersController(Isys_userRepository sys_userRepository)
        {
            _sys_userRepository = sys_userRepository;
        }
        [Route("/api/Query")]
        [HttpGet]
        public async Task<OkObjectResult> Query(string nm = "", string ph = "", string usernm = "")
        {
            try
            {
                List<Model.sys_user> ls = await _sys_userRepository.Query();
                if (!string.IsNullOrEmpty(nm))
                {
                    ls = ls.Where(x => x.name.Contains(nm)).ToList();
                }
                if (!string.IsNullOrEmpty(ph))
                {
                    ls = ls.Where(x => x.phone.Contains(ph)).ToList();
                }
                if (!string.IsNullOrEmpty(usernm))
                {
                    ls = ls.Where(x => x.username.Contains(usernm)).ToList();
                }
                return Ok(new
                {
                    msg = "",
                    code = 0,
                    data = ls
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("/api/Insert")]
        [HttpPost]
        public async Task<int> Insert(Model.sys_user a)
        {
            try
            {
                logger.Debug($"用户对秒杀配置进行添加,添加的配置名称为:{a.name}");
                int i = await _sys_userRepository.Insert(a);
                return i;
            }
            catch (Exception)
            {

                throw;
            }


        }
        [Route("/api/UptState")]
        [HttpPost]
        public async Task<int> UptState(string id)
        {
            try
            {
                return await _sys_userRepository.UptState(id);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Route("/api/Uptuser")]
        [HttpPost]
        public async Task<int> Uptuser(Model.sys_user a)
        {
            try
            {
                logger.Debug($"用户对秒杀配置进行修改,修改的配置ID为:{a.id}");
                int i = await _sys_userRepository.Uptuser(a);
                return i;
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Route("/api/DelUser")]
        [HttpPost]
        public async Task<int> DelUser(string id)
        {
            try
            {
                logger.Debug($"用户对秒杀配置进行删除,删除的配置ID为:{id}");
                return await _sys_userRepository.DelUser(id);
            }
            catch (Exception ex)
            {
                string nn = ex.Message;
                throw;
            }

        }
        [Route("/api/Bang")]
        [HttpGet]
        public async Task<IActionResult> Bang()
        {
            List<Model.sys_role> list = await _sys_userRepository.Bang();
            return Ok(list);
        }

    }
}
