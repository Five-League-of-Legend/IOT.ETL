using IOT.ETL.IRepository.sys_modules;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers
{
    public class sys_modulesController : Controller
    {
        private readonly Isys_modulesRepository _sys_modulesRepository;
        public sys_modulesController(Isys_modulesRepository sysmodulesRepository)
        {
            _sys_modulesRepository = sysmodulesRepository;
        }

        [Route("/api/GetSys_Modules")]
        [HttpGet]
        public async Task<List<Model.sys_modules>> GetSys_Modules()
        {
            try
            {

                return await _sys_modulesRepository.GetSys_Modules();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [HttpGet]
        [Route("/api/BindTree")]
        public async Task<IActionResult> BindTree()
        {
            List<Dictionary<string, object>> ls =await _sys_modulesRepository.BindTree();
            return Ok(ls);
        }

    }
}
