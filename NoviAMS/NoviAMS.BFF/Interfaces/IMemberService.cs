using System;
using NoviAMS.Domain.Models;

namespace NoviAMS.BFF.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<Member> Get();

        MemberDetail Get(string? id);
    }
}

