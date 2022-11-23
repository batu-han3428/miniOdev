using DOMAIN.Context;
using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RabbitMQ.Core.Concrete
{
    public class JobSchedulerService
    {
        public static async void Start()
        {
            //        try
            //        {
            //            using (var dbContext = new SqlDbContext())
            //            {
            //                ISchedulerFactory schedContext = new StdSchedulerFactory();
            //                var scheduler = schedContext.GetScheduler();

            //                if (!scheduler.Result.IsStarted)
            //                {
            //                     await scheduler.Result.Start();
            //                }

            //                var jobList = dbContext.Set<JobTable>().Include(x=>x.JobType).Where(x => x.IS_ACTIVE == true).ToList();

            //                foreach (var jobItem in jobList)
            //                {

            //                    //Aylık
            //                    if (jobItem.JobType.ID_JOB_TYPE == 3 && (jobItem.DAY > 0 || jobItem.DAY <= 28))
            //                    {
            //                        IJobDetail job = JobBuilder.Create<JobExecuteService>().WithIdentity(jobItem.JOB_KEY, "group1").Build();

            //                        ITrigger trigger = TriggerBuilder.Create()
            //                            .WithSchedule(CronScheduleBuilder
            //                            .MonthlyOnDayAndHourAndMinute(jobItem.DAY.Value, jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes)
            //                            .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")))
            //                            .WithDescription(jobItem.DESCRIPTION)
            //                            .Build();

            //                        scheduler.Result.ScheduleJob(job, trigger);
            //                    }
            //                    //Haftalık
            //                    else if (jobItem.JobType.ID_JOB_TYPE == 2 && (jobItem.DAY >= 0 || jobItem.DAY <= 6))
            //                    {
            //                        IJobDetail job = JobBuilder.Create<JobExecuteService>().WithIdentity(jobItem.JOB_KEY, "group1").Build();

            //                        ITrigger trigger = TriggerBuilder.Create()
            //                            .WithSchedule(CronScheduleBuilder
            //                            .WeeklyOnDayAndHourAndMinute(GetDayOfWeek(jobItem.DAY.Value), jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes)
            //                            .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")))
            //                            .WithDescription(jobItem.DESCRIPTION)
            //                            .Build();

            //                        scheduler.Result.ScheduleJob(job, trigger);
            //                    }
            //                    //Günlük
            //                    else if (jobItem.JobType.ID_JOB_TYPE == 1)
            //                    {
            //                        Console.WriteLine("job1 trigger job bağlama");

            //                        //IJobDetail job = JobBuilder.Create<Job>().WithIdentity(jobItem.JOB_KEY, "group1").Build();
            //                        IJobDetail job = JobBuilder.Create<JobExecuteService>().WithIdentity("JobExecuteService", "MailGrup").Build();



            //                        //ITrigger trigger = TriggerBuilder.Create()
            //                        //    .WithSchedule(CronScheduleBuilder
            //                        //        .DailyAtHourAndMinute(jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes)
            //                        //        .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"))
            //                        //    )
            //                        //    .WithDescription(jobItem.DESCRIPTION)
            //                        //    .Build();

            //                        //ISimpleTrigger TriggerGorev = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("JobExecuteService").StartAt(DateTime.UtcNow).WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()).Build();



            //                        //                        var example = TriggerBuilder.Create()
            //                        //.WithIdentity("JobExecuteService", "MailGrup")
            //                        //.ForJob("JobExecuteService", "MailGrup")
            //                        //.WithSimpleSchedule(o =>
            //                        //{
            //                        //    o.WithRepeatCount(5)
            //                        //        .WithInterval(TimeSpan.FromMinutes(1));
            //                        //})
            //                        //.Build();


            //                        ITrigger trigger = TriggerBuilder.Create()
            //.WithIdentity("trigger3", "group1")
            //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes))
            //.ForJob(job)
            //.Build();

            //                       await  scheduler.Result.ScheduleJob(job, trigger);






            //                    }
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }

            ISchedulerFactory schedContext = new StdSchedulerFactory();
            var scheduler = schedContext.GetScheduler();

            if (!scheduler.Result.IsStarted)
            {
                await scheduler.Result.Start();
            }


            IJobDetail job = JobBuilder.Create<JobExecuteService>().WithIdentity("JobExecuteService", "MailGrup").Build();



            ITrigger trigger = TriggerBuilder.Create()
.WithIdentity("JobExecuteService", "MailGrup")
.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(20, 02))
.ForJob(job)
.Build();

            await scheduler.Result.ScheduleJob(job, trigger);

        }
        public static DayOfWeek GetDayOfWeek(int day)
        {
            if (day == (int)DayOfWeek.Sunday)
            {
                return DayOfWeek.Sunday;
            }
            else if (day == (int)DayOfWeek.Monday)
            {
                return DayOfWeek.Monday;
            }
            else if (day == (int)DayOfWeek.Tuesday)
            {
                return DayOfWeek.Tuesday;
            }
            else if (day == (int)DayOfWeek.Wednesday)
            {
                return DayOfWeek.Wednesday;
            }
            else if (day == (int)DayOfWeek.Thursday)
            {
                return DayOfWeek.Thursday;
            }
            else if (day == (int)DayOfWeek.Friday)
            {
                return DayOfWeek.Friday;
            }
            else if (day == (int)DayOfWeek.Saturday)
            {
                return DayOfWeek.Saturday;
            }
            else
            {
                return DayOfWeek.Monday;
            }
        }

        public async Task<string> ResetJob()
        {
            string message;
            try
            {
                var schedContext = new StdSchedulerFactory();
                var scheduler = schedContext.GetScheduler();

                scheduler.Result.Shutdown();
                System.Diagnostics.Debug.WriteLine("Joblar Durduruldu");
                JobSchedulerService.Start();
                System.Diagnostics.Debug.WriteLine("Yeniden Başlatıldı");
                message = "Joblar başarılı bir şekilde resetlendi.";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }
    }
}
