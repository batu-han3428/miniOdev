using DOMAIN.Context;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;

namespace miniOdev.Helpers
{
    public class SchedulerHelper
    {
        public static async void ZamanlayiciMail()
        {
            try
            {
                using (var dbContext = new SqlDbContext())
                {
                    ISchedulerFactory schedContext = new StdSchedulerFactory();
                    var scheduler = schedContext.GetScheduler();

                    if (!scheduler.Result.IsStarted)
                    {
                        await scheduler.Result.Start();
                    }

                    var jobList = dbContext.Set<DOMAIN.Models.JobTable>().Include(x => x.JobType).Where(x => x.IS_ACTIVE == true).ToList();

                    foreach (var jobItem in jobList)
                    {

                        //Aylık
                        if (jobItem.JobType.ID_JOB_TYPE == 3 && (jobItem.DAY > 0 || jobItem.DAY <= 28))
                        {
                            IJobDetail job = JobBuilder.Create<JobExecuteService>().WithIdentity(jobItem.JOB_KEY, "group1").Build();

                            ITrigger trigger = TriggerBuilder.Create()
                                .WithSchedule(CronScheduleBuilder
                                .MonthlyOnDayAndHourAndMinute(jobItem.DAY.Value, jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes)
                                .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")))
                                .WithDescription(jobItem.DESCRIPTION)
                                .Build();

                            await scheduler.Result.ScheduleJob(job, trigger);
                        }
                        //Haftalık
                        else if (jobItem.JobType.ID_JOB_TYPE == 2 && (jobItem.DAY >= 0 || jobItem.DAY <= 6))
                        {
                            IJobDetail job = JobBuilder.Create<JobExecuteService>().WithIdentity(jobItem.JOB_KEY, "group1").Build();

                            ITrigger trigger = TriggerBuilder.Create()
                                .WithSchedule(CronScheduleBuilder
                                .WeeklyOnDayAndHourAndMinute(GetDay.GetDayOfWeek(jobItem.DAY.Value), jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes)
                                .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")))
                                .WithDescription(jobItem.DESCRIPTION)
                                .Build();

                            await scheduler.Result.ScheduleJob(job, trigger);
                        }
                        //Günlük
                        else if (jobItem.JobType.ID_JOB_TYPE == 1)
                        {
                            IJobDetail job = JobBuilder.Create<JobExecuteService>().WithIdentity("JobExecuteService", "MailGrup").Build();

                            ITrigger trigger = TriggerBuilder.Create()
                            .WithIdentity("trigger3", "group1")
                            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(jobItem.JOB_TIME.Hours, jobItem.JOB_TIME.Minutes))
                            .ForJob(job)
                            .WithDescription(jobItem.DESCRIPTION)
                            .Build();


                            await scheduler.Result.ScheduleJob(job, trigger);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
