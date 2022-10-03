using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NoviAMS.BFF.Interfaces;
using NoviAMS.Domain.Models;
using NoviAMS.BFF.Profiles;
using RestSharp;
using AutoMapper;

namespace NoviAMS.BFF.Services
{
    public class MemberService : IMemberService
    {
        private readonly MapperConfiguration _config;
        private readonly RestClientOptions _options;

        public MemberService()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JObject, IEnumerable<Member>>();
                cfg.CreateMap<JObject, MemberDetail>();
                cfg.AddProfile<MemberProfile>();
                cfg.AddProfile<MemberDetailProfile>();
            });

            _options = new RestClientOptions("https://180930b.novitesting.com/api/")
            {
                ThrowOnAnyError = true,
                MaxTimeout = 30000
            };
        }

        /// <summary>
        /// Retrieves a collection of Members and maps it to the Domain Models
        /// </summary>
        /// <returns>Collection of Members</returns>
        public IEnumerable<Member> Get()
        {
            using (var client = new RestClient(_options))
            {
                var request = new RestRequest("members", Method.Get)
                    .AddHeader("Authorization", "Basic oNiPIWDjyGSkvLuxwHTzbXgBg2woNoW2TjU/tJs0E7U=")
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Content-Type", "application/json");

                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                var response = client.ExecuteAsync(request);
                var jsonObj = JObject.Parse(response.Result.Content);
                var mapper = _config.CreateMapper();

                var dto = mapper.Map<IEnumerable<Member>>(jsonObj);
                return dto;
            }
        }

        /// <summary>
        /// Get details of a specific Member from the NoviAMS Rest API
        /// </summary>
        /// <param name="id">Members Unique Id</param>
        /// <returns>MemberDetail Domain Model</returns>
        public MemberDetail Get(string? id)
        {
            using (var client = new RestClient(_options))
            {
                var request = new RestRequest($"members/{id}", Method.Get)
                    .AddHeader("Authorization", "Basic oNiPIWDjyGSkvLuxwHTzbXgBg2woNoW2TjU/tJs0E7U=")
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Content-Type", "application/json");

                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                var response = client.ExecuteAsync(request);
                var jsonObj = JObject.Parse(response.Result.Content);
                var mapper = _config.CreateMapper();

                var dto = mapper.Map<MemberDetail>(jsonObj);
                return dto;
            }
        }
    }
}

