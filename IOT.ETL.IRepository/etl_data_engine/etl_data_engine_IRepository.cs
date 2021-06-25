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
        List<Model.etl_data_engine> GetList_etl_data_engine();
        int Insert_etl_data_engine(Model.etl_data_engine model);
        int Delete_etl_data_engine(string ids);
        int Uptdate_etl_data_engine(Model.etl_data_engine model);
        List<Model.etl_data_engine_type> Binds();
        int Uptdate_code(Model.etl_data_engine model);
    }
}
