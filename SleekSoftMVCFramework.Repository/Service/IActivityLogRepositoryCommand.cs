using System.Threading.Tasks;
using System;
using SleekSoftMVCFramework.Repository;
using SleekSoftMVCFramework.Data.Entities;
using SleekSoftMVCFramework.Repository.CoreRepositories;

namespace SleekSoftMVCFramework.Repository
{
    public interface IActivityLogRepositoryCommand : IAutoDependencyRegister 
    {
        //: IRepository<ActivityLog>
        Task CreateActivityLogAsync(string descriptn, string controllerName, string actionName, Int64 userid, object record);
        void CreateActivityLog(string descriptn, string controllerName, string actionNAme, Int64 userid, object record);
    }
}
