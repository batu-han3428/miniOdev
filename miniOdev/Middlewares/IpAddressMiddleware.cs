using BL.Models;
using DOMAIN.Models;
using IpData;
using IpData.Models;
using System.Net;

namespace miniOdev.Middlewares
{
    public class IpAddressMiddleware
    {
        RequestDelegate _next;
        ServiceProvider _builder;
        public IpAddressMiddleware(RequestDelegate next, ServiceProvider builder)
        {
            _next = next;
            _builder = builder;
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if(ipAddress != null)
            {
                var visitorInformationServices = _builder.GetService<IVisitorInformationServices>();

               
                //var data = visitorInformationServices?.GetVisitorInformation(ipAddress);

                //if(data != null)
                //{
                    List<string?>? blackList = visitorInformationServices?.GetBlackList();

                    if (blackList != null)
                    {
                        if (blackList.Any(x=>x == ipAddress))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            await context.Response.WriteAsync("Aren't you ashamed of not being honest?\nVam ne stydno byt' nechestnym?\n");
                            return;
                        }
                    }

                    var client = new IpDataClient("bb93a9855fc9d48b7ae666199617479932c4ff5cdeec5a9199194915");

                    IpInfo? ipInfo = await client.Lookup(ipAddress);

                    VisitorInformationThreat visitorInformationThreat = new VisitorInformationThreat();
                    visitorInformationThreat.IsAnonymous = ipInfo.Threat.IsAnonymous;
                    visitorInformationThreat.IsBogon = ipInfo.Threat.IsAnonymous;
                    visitorInformationThreat.IsKnownAbuser = ipInfo.Threat.IsKnownAbuser;
                    visitorInformationThreat.IsKnownAttacker = ipInfo.Threat.IsKnownAttacker;
                    visitorInformationThreat.IsProxy = ipInfo.Threat.IsProxy;
                    visitorInformationThreat.IsThreat = ipInfo.Threat.IsThreat;
                    visitorInformationThreat.IsTor = ipInfo.Threat.IsTor;


                    VisitorInformationAsn visitorInformationAsn = new VisitorInformationAsn();
                    visitorInformationAsn.Asn = ipInfo.Asn.Asn;
                    visitorInformationAsn.Domain = ipInfo.Asn.Domain;
                    visitorInformationAsn.Name = ipInfo.Asn.Name;
                    visitorInformationAsn.Route = ipInfo.Asn.Route;
                    visitorInformationAsn.Type = ipInfo.Asn.Type;


                    VisitorInformation visitorInformation = new VisitorInformation();
                    visitorInformation.Ip = ipInfo.Ip;
                    visitorInformation.Process = context.Request.Method;
                    visitorInformation.Time = DateTime.Now;
                    visitorInformation.Path = context.Request.Path;
                    visitorInformation.Longitude = ipInfo.Longitude;
                    visitorInformation.IsEu = ipInfo.IsEu;
                    visitorInformation.CallingCode = ipInfo.CallingCode;
                    visitorInformation.City = ipInfo.City;
                    visitorInformation.ContinentCode = ipInfo.ContinentCode;
                    visitorInformation.ContinentName = ipInfo.ContinentName;
                    visitorInformation.Count = ipInfo.Count;
                    visitorInformation.CountryCode = ipInfo.CountryCode;
                    visitorInformation.CountryName = ipInfo.CountryName;
                    visitorInformation.EmojiFlag = ipInfo.EmojiFlag;
                    visitorInformation.EmojiUnicode = ipInfo.EmojiUnicode;
                    visitorInformation.Flag = ipInfo.Flag;
                    visitorInformation.Latitude = ipInfo.Latitude;
                    visitorInformation.Organisation = ipInfo.Organisation;
                    visitorInformation.Postal = ipInfo.Postal;
                    visitorInformation.Region = ipInfo.Region;
                    visitorInformation.RegionCode = ipInfo.RegionCode;
                    visitorInformation.Threat = visitorInformationThreat;
                    visitorInformation.Asn = visitorInformationAsn;

                    visitorInformationServices?.SetVisitorInformation(visitorInformation);


                    if (ipInfo != null)
                    {
                        if (ipInfo.CallingCode != "90" || ipInfo.Threat.IsThreat == true)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            await context.Response.WriteAsync("Aren't you ashamed of not being honest?\nVam ne stydno byt' nechestnym?\n");
                            return;
                        }
                    }

                    //if (data == false || context.Request.Method == "POST")
                    //{
                       

                    //    VisitorInformationThreat visitorInformationThreat = new VisitorInformationThreat();
                    //    visitorInformationThreat.IsAnonymous = ipInfo.Threat.IsAnonymous;
                    //    visitorInformationThreat.IsBogon = ipInfo.Threat.IsAnonymous;
                    //    visitorInformationThreat.IsKnownAbuser = ipInfo.Threat.IsKnownAbuser;
                    //    visitorInformationThreat.IsKnownAttacker = ipInfo.Threat.IsKnownAttacker;
                    //    visitorInformationThreat.IsProxy = ipInfo.Threat.IsProxy;
                    //    visitorInformationThreat.IsThreat = ipInfo.Threat.IsThreat;
                    //    visitorInformationThreat.IsTor = ipInfo.Threat.IsTor;


                    //    VisitorInformationAsn visitorInformationAsn = new VisitorInformationAsn();
                    //    visitorInformationAsn.Asn = ipInfo.Asn.Asn;
                    //    visitorInformationAsn.Domain = ipInfo.Asn.Domain;
                    //    visitorInformationAsn.Name = ipInfo.Asn.Name;
                    //    visitorInformationAsn.Route = ipInfo.Asn.Route;
                    //    visitorInformationAsn.Type = ipInfo.Asn.Type;


                    //    VisitorInformation visitorInformation = new VisitorInformation();
                    //    visitorInformation.Ip = ipInfo.Ip;
                    //    visitorInformation.Process = context.Request.Method;
                    //    visitorInformation.Time = DateTime.Now;
                    //    visitorInformation.Path = context.Request.Path;
                    //    visitorInformation.Longitude = ipInfo.Longitude;
                    //    visitorInformation.IsEu = ipInfo.IsEu;
                    //    visitorInformation.CallingCode = ipInfo.CallingCode;
                    //    visitorInformation.City = ipInfo.City;
                    //    visitorInformation.ContinentCode = ipInfo.ContinentCode;
                    //    visitorInformation.ContinentName = ipInfo.ContinentName;
                    //    visitorInformation.Count = ipInfo.Count;
                    //    visitorInformation.CountryCode = ipInfo.CountryCode;
                    //    visitorInformation.CountryName = ipInfo.CountryName;
                    //    visitorInformation.EmojiFlag = ipInfo.EmojiFlag;
                    //    visitorInformation.EmojiUnicode = ipInfo.EmojiUnicode;
                    //    visitorInformation.Flag = ipInfo.Flag;
                    //    visitorInformation.Latitude = ipInfo.Latitude;
                    //    visitorInformation.Organisation = ipInfo.Organisation;
                    //    visitorInformation.Postal = ipInfo.Postal;
                    //    visitorInformation.Region = ipInfo.Region;
                    //    visitorInformation.RegionCode = ipInfo.RegionCode;
                    //    visitorInformation.Threat = visitorInformationThreat;
                    //    visitorInformation.Asn = visitorInformationAsn;

                    //    visitorInformationServices?.SetVisitorInformation(visitorInformation);

                    //}
                //}
            }
            
            await _next.Invoke(context);
        }
    }
}
