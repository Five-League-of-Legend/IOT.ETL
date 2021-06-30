using IOT.ETL.IRepository.sys_role_modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sys_role_modulesContoller : ControllerBase
    {
        private readonly Isys_role_modulesRepository _sys_role_modulesRepository;
        public sys_role_modulesContoller(Isys_role_modulesRepository sys_role_modulesRepository)
        {
            _sys_role_modulesRepository = sys_role_modulesRepository;
        }
        [Route("/api/Addss")]
        [HttpPost]
        public async Task<int> Adds(Model.sys_role_modules m)
        {
            return await _sys_role_modulesRepository.Adds(m);
        }
        [Route("/api/Uptss")]
        [HttpPost]
        public async Task<int> Uptuser(Model.sys_role_modules a)
        {

            return await _sys_role_modulesRepository.Uptuser(a);

        }
        [Route("/api/Uptft")]
        [HttpPost]
        public async Task<List<Model.sys_role_modules>> Uptft(string id)
        {
            return await _sys_role_modulesRepository.Uptft(id);
        }
        [Route("/api/chas")]
        [HttpPost]
        public async Task<IActionResult> cha(string id)
        {
            object ls = await _sys_role_modulesRepository.cha(id);
            if(ls==null)
            {
                return Ok(-1);
            }
            else
            {
                string ss = ls.ToString();
                string[] arr = ss.Split(',');
                return Ok(arr);
            }
        }
    }
}
