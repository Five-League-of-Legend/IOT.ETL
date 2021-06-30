using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.TaskIRepository
{
    public interface TaskIRepository
    {
        List<IOT.ETL.Model.etl_task_info> ShowTask();
        int AddTask(IOT.ETL.Model.etl_task_info ta);
        int DelTask(string id="");
        List<Model.sys_user_role> Bang();
    }
}
 