using System;
using System.Collections;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NoviAMS.Domain.Models;

namespace NoviAMS.BFF.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<JObject, IEnumerable<Member>>().ConvertUsing<JObjectToMemberCollectionConverter>();
            var memberMap = CreateMap<JToken, Member>();

            memberMap.ForMember(x => x.Id, y => y.MapFrom(j => j.SelectToken(".UniqueID")));
            memberMap.ForMember(x => x.Name, y => y.MapFrom(j => j.SelectToken(".Name")));
            memberMap.ForMember(x => x.Email, y => y.MapFrom(j => j.SelectToken(".Email")));
            memberMap.ForMember(x => x.PhoneNumber, y => y.MapFrom(j => j.SelectToken(".Phone")));
            memberMap.ForMember(x => x.CustomerType, y => y.MapFrom(j => j.SelectToken(".CustomerType")));
            memberMap.ForMember(x => x.IsActive, y => y.MapFrom(j => j.SelectToken(".Active")));
        }
    }

    public class MemberDetailProfile : Profile
    {
        public MemberDetailProfile()
        {
            CreateMap<JObject, MemberDetail>();
            CreateMap<JObject, Address>(MemberList.None);
            var memberDetailMap = CreateMap<JObject, MemberDetail>();

            memberDetailMap.ForMember(x => x.Id, y => y.MapFrom(j => j.SelectToken(".UniqueID")));
            memberDetailMap.ForMember(x => x.Name, y => y.MapFrom(j => j.SelectToken(".Name")));
            memberDetailMap.ForMember(x => x.Email, y => y.MapFrom(j => j.SelectToken(".Email")));
            memberDetailMap.ForMember(x => x.PhoneNumber, y => y.MapFrom(j => j.SelectToken(".Phone")));
            memberDetailMap.ForMember(x => x.CustomerType, y => y.MapFrom(j => j.SelectToken(".CustomerType")));
            memberDetailMap.ForMember(x => x.IsActive, y => y.MapFrom(j => j.SelectToken(".Active")));
            memberDetailMap.ForMember(x => x.BillingAddress, y => y.MapFrom(j => j.SelectToken(".BillingAddress")));
            memberDetailMap.ForMember(x => x.ShippingAddress, y => y.MapFrom(j => j.SelectToken(".ShippingAddress")));
        }
    }
    
    public class JObjectToMemberCollectionConverter : ITypeConverter<JObject, IEnumerable<Member>>
    {
        public IEnumerable<Member> Convert(JObject source, IEnumerable<Member> destination, ResolutionContext context)
        {
            var memberList = new List<Member>();
            var memberCount = source.SelectToken("Results").Children().Count();

            for (int i = 0; i < memberCount; i++)
            {
                var record = source.SelectToken($"Results[{i}]");
                var member = new Member();

                if (record != null)
                    member = context.Mapper.Map<JToken, Member>(record);

                memberList.Add(member);
            }

            return memberList;
        }
    }
}

