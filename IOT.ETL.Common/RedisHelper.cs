using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace IOT.ETL.Common
{
    public class RedisHelper<T>
    {
        /// <summary>
        /// 存入缓存
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        #region 放入缓存
        public void SetList(List<T> list, string key)
        {
            using (IRedisClient client = new RedisClient("127.0.0.1", 6379))
            {
                //存入
                client.Set<List<T>>(key, list);
            }
        }
        #endregion

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="key"></param>
        #region 删除key
        public void DelKey(string key)
        {
            using (IRedisClient client = new RedisClient("127.0.0.1", 6379))
            {
                //删除
                client.Remove(key);
            }
        }
        #endregion

        /// <summary>
        /// 判断key值是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        #region 判断key是否存在
        public bool Judge(string key)
        {
            using (IRedisClient client = new RedisClient("127.0.0.1", 6379))
            {
                //判断是否存在
                return client.ContainsKey(key);
            }
        }
        #endregion

        /// <summary>
        /// 取出缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        #region 取出缓存
        public List<T> GetList(string key)
        {
            using (IRedisClient client = new RedisClient("127.0.0.1", 6379))
            {
                //取出
                return client.Get<List<T>>(key);
            }
        } 
        #endregion
    }
}
