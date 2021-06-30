using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.etl_task_info
{
    public interface etl_task_info_IRepository
    {
        Task<List<Model.etl_task_info>> GetList_etl_task_info();
    }
}
