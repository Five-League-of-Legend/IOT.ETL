using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    public class etl_task_info
    {
        // 任务信息表
        public string Id { get; set; }                  //'id',
        public string Name { get; set; }                //任务名称
        public int Weight { get; set; }                 //任务权重级别 0紧急任务1高2中3低
        public int Process_status { get; set; }         //任务执行状态 -1待执行 0可执行 1正在执行 2执行完成
        public int Status { get; set; }                 //任务状态 1无错误0有错误
        public int Total { get; set; }                  //总处理条数
        public int Process_total { get; set; }          //已处理条数
        public decimal Proportion { get; set; }         //任务完成度
        public int Error_total { get; set; }            //错误条数
        public int Execute_total { get; set; }          //总需执行次数
        public int Complete_execute_total { get; set; } //已完成执行次数
        public int Success_insert_total { get; set; }   //成功插入条数
        public int Success_update_total { get; set; }   //成功修改条数
        public int Success_delete_total { get; set; }   //成功删除条数
        public int Error_add_total { get; set; }        //插入失败条数
        public int Error_logic_total { get; set; }      //逻辑失败条数
        public int Complete_time { get; set; }          //预计完成时间 精确到秒
        public int Grand_total_time { get; set; }       //累计处理时间 精确到秒
        public string Process_json { get; set; }        //前端json对象
        public int Revision { get; set; }               //乐观锁
        public string Create_by { get; set; }           //创建人
        public DateTime Create_time { get; set; }       //创建时间
        public string Update_by { get; set; }           //更新人
        public DateTime Update_time { get; set; }       //更新时间
    }
}
