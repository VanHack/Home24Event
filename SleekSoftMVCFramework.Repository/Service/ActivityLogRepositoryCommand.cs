using System;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.Entities;
using Newtonsoft.Json;
using log4net;
using SleekSoftMVCFramework.Repository.CoreRepositories;

namespace SleekSoftMVCFramework.Repository
{
     public  class ActivityLogRepositoryCommand :IActivityLogRepositoryCommand
    {
         private readonly IRepositoryCommand<ActivityLog, long> _activityLogRepositoryCommand;
         private readonly ILog _log;
         public ActivityLogRepositoryCommand(IRepositoryCommand<ActivityLog, long> activityLogRepositoryCommand,ILog log)
         {
             _activityLogRepositoryCommand=activityLogRepositoryCommand;
            _log = log;
         }
        
         public async Task CreateActivityLogAsync(string descriptn, string moduleName, string moduleAction, Int64 userid, object record)
         {

                 try
                 {
                     ActivityLog alog = new ActivityLog
                     {

                         ModuleName = moduleName,
                         ModuleAction = moduleAction,
                         UserId = userid,
                         Description = descriptn,
                         Record = record!= null ?JsonConvert.SerializeObject(record):"N/A"
                     };
                     await _activityLogRepositoryCommand.InsertAsync(alog);
                     await _activityLogRepositoryCommand.SaveChangesAsync();
                 }
                 catch (Exception ex)
                 {

                   _log.Error(ex);

                 }
                
             
         }
         public void CreateActivityLog(string descriptn, string moduleName, string moduleAction, Int64 userid, object record)
         {
          
             try
             {
                ActivityLog alog = new ActivityLog
                {
                    ModuleName = moduleName,
                    ModuleAction = moduleAction,
                    UserId = userid,
                    Description = descriptn,
                    Record = record != null ? JsonConvert.SerializeObject(record) : "N/A"

                };
                _activityLogRepositoryCommand.Insert(alog);
                 _activityLogRepositoryCommand.SaveChanges();
             }
             catch (Exception ex)
             {
                _log.Error(ex);
            }
             
         }

         
    }
}
