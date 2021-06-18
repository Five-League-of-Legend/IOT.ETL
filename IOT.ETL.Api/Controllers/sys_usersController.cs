using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_user;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sys_usersController : ControllerBase
    {
        private readonly Isys_userRepository _sys_userRepository;
        public sys_usersController(Isys_userRepository sys_userRepository)
        {
            _sys_userRepository = sys_userRepository;
        }
        [Route("/api/Query")]
        [HttpGet]
        public IActionResult Query(string nm = "", string ph = "", string usernm = "")
        {
            try
            {
                List<Model.sys_user> ls = _sys_userRepository.Query();
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
        public int Insert(Model.sys_user a)
        {
            try
            {
                int i = _sys_userRepository.Insert(a);
                return i;
            }
            catch (Exception)
            {

                throw;
            }


        }
        [Route("/api/UptState")]
        [HttpPost]
        public int UptState(string id)
        {
            try
            {
                return _sys_userRepository.UptState(id);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Route("/api/Uptuser")]
        [HttpPost]
        public int Uptuser(Model.sys_user a)
        {
            try
            {
                int i = _sys_userRepository.Uptuser(a);
                return i;
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Route("/api/DelUser")]
        [HttpPost]
        public int DelUser(string id)
        {
            try
            {
                return _sys_userRepository.DelUser(id);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
