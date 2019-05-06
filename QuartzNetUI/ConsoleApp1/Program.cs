using Quartz;
using Quartz.Impl;
using Quartz.Impl.AdoJobStore;
using Quartz.Impl.AdoJobStore.Common;
using Quartz.Util;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgramRunExample().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();


            /*

            NameValueCollection properties = new NameValueCollection();

            properties["quartz.dataSource.sqlserver.provider"] = "sqlserver";
            properties["quartz.dataSource.sqlserver.connectionString"] = @"Data Source=MARKJI-PC;Initial Catalog=quartz;User ID=sa;Password=password01!";
            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            //注意这个名字改为了sqlserver，上面的都要跟着改，也可以改为别的名
            properties["quartz.jobStore.dataSource"] = "sqlserver";
            properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
            properties["quartz.serializer.type"] = "json";
            // properties["quartz.scheduler.instanceId"] = "AUTO";

            //properties["quartz.jobStore.lockHandler.type"] = "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz";
            //properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
            //properties["quartz.jobStore.dataSource"] = "default";
            //properties["quartz.dataSource.default.connectionString"] = "server=MARKJI-PC;user id=sa;password=password01!;persistsecurityinfo=True;database=quartz;";
            //properties["quartz.dataSource.default.provider"] = "SqlServer";
            //properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            //properties["quartz.jobStore.useProperties"] = "true";
            //properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            //properties["quartz.serializer.type"] = "binary";







            StdSchedulerFactory factory = new StdSchedulerFactory(properties);
            IScheduler scheduler = factory.GetScheduler().GetAwaiter().GetResult();
          
            IJobDetail job = JobBuilder.Create<HelloJob>()
                  .WithIdentity("job1", "group1")
                  .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                //.StartNow()
                //.WithSimpleSchedule(x => x
                //    .WithIntervalInSeconds(1)            //在这里配置执行延时
                //    .RepeatForever())
                .WithCronSchedule("
            3 * * * * ?")
                .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();

            **/

        }

        private static async Task RunProgramRunExample()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                //NameValueCollection properties = new NameValueCollection
                //{
                //    { "quartz.serializer.type", "binary" }
                //};

                //properties["quartz.dataSource.sqlserver.provider"] = "SqlServer-41";
                //properties["quartz.dataSource.sqlserver.connectionString"] = @"Data Source=.\MARKJI-PC;Initial Catalog=quartz;User ID=sa;Password=password01!";
                //properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
                ////注意这个名字改为了sqlserver，上面的都要跟着改，也可以改为别的名
                //properties["quartz.jobStore.dataSource"] = "sqlserver";
                //properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
                //properties["quartz.serializer.type"] = "json";

                var properties = new NameValueCollection
{
    { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
    { "quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz" },
    { "quartz.jobStore.tablePrefix", "QRTZ_" },
    { "quartz.jobStore.dataSource", "default" },
    { "quartz.dataSource.default.connectionString", "Server=(localdb)\\MARKJI-PC;Database=Quartz;Trusted_Connection=True;MultipleActiveResultSets=true" },
    { "quartz.dataSource.default.provider", "SqlServer-41" },
    { "quartz.jobStore.useProperties", "true" },
    { "quartz.serializer.type", "json" }
};


                StdSchedulerFactory factory = new StdSchedulerFactory(properties);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(1)            //在这里配置执行延时
                        .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                //                await Task.Delay(TimeSpan.FromSeconds(5));
                // and last shut down the scheduler when you are ready to close your program
                //                await scheduler.Shutdown();           

                //如果解除await Task.Delay(TimeSpan.FromSeconds(5))和await scheduler.Shutdown()的注释，
                //5秒后输出"Press any key to close the application"，
                //scheduler里注册的任务也会停止。


            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }
    }
}
