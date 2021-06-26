using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.TaskIRepository
{
    public interface TaskIRepository
    {
        Task<List<IOT.ETL.Model.etl_task_info>> ShowTask();
        Task<int> AddTask(IOT.ETL.Model.etl_task_info ta);
        Task<int> DelTask(string id="");
    }
}
 