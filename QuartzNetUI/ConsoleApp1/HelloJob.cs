using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class HelloJob : IJob
    {
        static int index = 1;
        public async Task Execute(IJobExecutionContext context)
        {
          //  Console.WriteLine(" helloJob index={0},current={1}, scheuler={2},nextTime={3}",
          //index++, DateTime.Now,
          //context.ScheduledFireTimeUtc?.LocalDateTime,
          //context.NextFireTimeUtc?.LocalDateTime);
          //  Console.ReadLine();
            //任务主体，这里强制要求必须是异步方法，如果不想用异步可以使用quartz 2.x版本
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }


    }
}
