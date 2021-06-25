using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.Model;

namespace IOT.ETL.IRepository.etl_data_engine
{
    public interface etl_data_engine_IRepository
    {
        Task<List<Model.etl_data_engine>> GetList_etl_data_engine();
        Task<int> Insert_etl_data_engine(Model.etl_data_engine model);
        Task<int> Delete_etl_data_engine(string ids);
        Task<int> Uptdate_etl_data_engine(Model.etl_data_engine model);
        Task<List<Model.etl_data_engine_type>> Binds();
        Task<int> Uptdate_code(Model.etl_data_engine model);
    }
}
