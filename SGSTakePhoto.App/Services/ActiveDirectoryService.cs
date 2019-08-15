using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SGSTakePhoto.Infrastructure;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 域服务器服务类
    /// </summary>
    public class ActiveDirectoryService
    {
        public string AdName { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }

        public ActiveDirectoryService() { }

        public ActiveDirectoryService(string adName, string loginName, string loginPwd)
        {
            this.AdName = adName;
            this.LoginName = loginName;
            this.LoginPwd = loginPwd;
        }

        /// <summary>
        /// 验证域用户是否存在
        /// </summary>
        /// <returns></returns>
        public Response<string> ValidateDomainUser()
        {
            try
            {
                DirectoryEntry entry = new DirectoryEntry(AdName, LoginName, LoginPwd);
                string objectSid = (new SecurityIdentifier((byte[])entry.Properties["objectSid"].Value, 0).Value);

                return new Response<string> { Data = objectSid };
            }
            catch (Exception ex)
            {
                return new Response<string> { ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Response ADLogin()
        {
            try
            {
                DirectoryEntry entry = new DirectoryEntry(AdName, LoginName, LoginPwd);
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry)
                {
                    Filter = string.Format("(SAMAccountName={0})", LoginName)
                };
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                return new Response { Success = (result != null) };
            }
            catch (Exception ex)
            {
                return new Response { ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// 查询域中的所有用户
        /// </summary>
        /// <returns></returns>
        public Response QueryADUsers()
        {
            DirectoryEntry objDE = new DirectoryEntry(AdName, LoginName, LoginPwd);
            string strFilter = "(&(objectCategory=person)(objectClass=user))";
            DirectorySearcher objSearcher = new DirectorySearcher(objDE, strFilter)
            {
                //排序 
                Sort = new SortOption("name", SortDirection.Ascending)
            };
            SearchResultCollection src = objSearcher.FindAll();
            //src 中保存了所有用户的所有信息
            //如果想查找域中所有的组织单元，可以把筛选字符串改为： 
            //string strFilter = "(&(objectCategory=organizationalUnit)(objectClass=organizationalUnit))";
            //如果想查找域中所有的群组，可以把筛选字符串改为： 
            //string strFilter = "(&(objectCategory=group)(objectClass=group))";

            return new Response();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Response QueryUserProperty()
        {
            //获取域用户属性 
            DirectoryEntry objDE = new DirectoryEntry(AdName, LoginName, LoginPwd);
            string strFilter = "(&(objectCategory=person)(objectClass=user))";
            DirectorySearcher objSearcher = new DirectorySearcher(objDE, strFilter);
            //排序 
            objSearcher.Sort = new SortOption("name", SortDirection.Ascending);
            SearchResultCollection src = objSearcher.FindAll();
            DirectoryEntry entry = src[102].GetDirectoryEntry();
            foreach (string pro in entry.Properties.PropertyNames)
            {
                //this.tbLog.Text += entry.Properties[pro].PropertyName + ".........." + entry.Properties[pro].Value.ToString() + " \r\n";
            }

            //或者：

            foreach (string strPropNamein in src[102].Properties.PropertyNames)
            {
                //this.tbLog.Text += strPropName;
            }

            return new Response { };
        }
    }
}
