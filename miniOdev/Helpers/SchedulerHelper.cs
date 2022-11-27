using DOMAIN.Context;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using RabbitMQ.Core.Abstract;
using RabbitMQ.Core.Concrete;
using static miniOdev.Helpers.JobExecuteService;

namespace miniOdev.Helpers
{
    public class SchedulerHelper
    {
        public static async void ZamanlayiciMail(IServiceProvider serviceProvider)
        {
            try
            {
                using (var dbContext = new SqlDbContext())
                {
                    ISchedulerFactory schedContext = new StdSchedulerFactory();
                    var scheduler = schedContext.GetScheduler().Result;

                    scheduler.JobFactory = new JobExecuteService(serviceProvider);
              
                    if (!scheduler.IsStarted)
                    {
                        await scheduler.Start();
                    }

                    var jobList = dbContext.Set<DOMAIN.Models.JobTable>().Include(x => x.JobType).Include(x=> x.CustomUser).Where(x => x.IS_ACTIVE == true).ToList();


                    foreach (var jobItem in jobList)
                    {
                        //Aylık
                        if (jobItem.JobType.ID_JOB_TYPE == 3 && (jobItem.DAY > 0 || jobItem.DAY <= 28))
                        {
                            IJobDetail job = JobBuilder.Create<RealJob>().WithIdentity(jobItem.JOB_KEY, "MailGrup2").Build();
                            job.JobDataMap["userData"] = jobItem;

                            ITrigger trigger = TriggerBuilder.Create()
                                .WithSchedule(CronScheduleBuilder
                                .MonthlyOnDayAndHourAndMinute(jobItem.DAY.Value, jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes)
                                .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")))
                                .WithDescription(jobItem.DESCRIPTION)
                                .Build();

                            await scheduler.ScheduleJob(job, trigger);
                        }
                        //Haftalık
                        else if (jobItem.JobType.ID_JOB_TYPE == 2 && (jobItem.DAY >= 0 || jobItem.DAY <= 6))
                        {
                            IJobDetail job = JobBuilder.Create<RealJob>().WithIdentity(jobItem.JOB_KEY, "MailGrup1").Build();
                            job.JobDataMap["userData"] = jobItem;

                            ITrigger trigger = TriggerBuilder.Create()
                                .WithSchedule(CronScheduleBuilder
                                .WeeklyOnDayAndHourAndMinute(GetDay.GetDayOfWeek(jobItem.DAY.Value), jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes)
                                .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")))
                                .WithDescription(jobItem.DESCRIPTION)
                                .Build();

                            await scheduler.ScheduleJob(job, trigger);
                        }
                        //Günlük
                        else if (jobItem.JobType.ID_JOB_TYPE == 1)
                        {
                            IJobDetail job = JobBuilder.Create<RealJob>().WithIdentity(jobItem.JOB_KEY, "MailGrup").Build();
                            job.JobDataMap["userData"] = jobItem;

                            ITrigger trigger = TriggerBuilder.Create()
                            .WithIdentity("trigger3", "group1")
                            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes))
                            .ForJob(job)
                            .WithDescription(jobItem.DESCRIPTION)
                            .Build();


                            await scheduler.ScheduleJob(job, trigger);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static string ResetJob(IServiceProvider serviceProvider)
        {
            string message;
            try
            {
                var schedContext = new StdSchedulerFactory();
                var scheduler = schedContext.GetScheduler();

                scheduler.Result.Shutdown();
                System.Diagnostics.Debug.WriteLine("Joblar Durduruldu");
                SchedulerHelper.ZamanlayiciMail(serviceProvider);
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
