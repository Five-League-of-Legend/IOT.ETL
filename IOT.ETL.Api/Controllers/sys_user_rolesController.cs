using IOT.ETL.IRepository.sysy_user_role;
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
    public class sys_user_rolesController : ControllerBase
    {
        private readonly Isys_user_roleRepository _sys_user_roleRepository;
        public sys_user_rolesController(Isys_user_roleRepository sys_user_roleRepository)
        {
            _sys_user_roleRepository = sys_user_roleRepository;
        }
        [Route("/api/Adds")]
        [HttpPost]
        public int Adds(Model.sys_user_role m)
        {
            return _sys_user_roleRepository.Add(m);
        }

    }
}
