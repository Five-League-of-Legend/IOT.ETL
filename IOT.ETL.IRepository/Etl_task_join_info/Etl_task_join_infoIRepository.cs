using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.Etl_task_join_info
{
   public  interface Etl_task_join_infoIRepository
    {
        int AddInputA(IOT.ETL.Model.etl_task_input_info ta);
        int AddInputB(IOT.ETL.Model.etl_task_input_info ta);
        int AddJoin(IOT.ETL.Model.etl_task_join_info jo);
        List<IOT.ETL.Model.etl_task_VModel> ShowField(string database_name);
        List<IOT.ETL.Model.etl_task_input_info> FanFieldID(string id);
        List<IOT.ETL.Model.etl_task_VModel> SelectBind(string database_name, string id);
        List<IOT.ETL.Model.etl_task_VModel> OutputAllField(string id);
        int ExecuteAllSql(string id, string name);
    }
}
