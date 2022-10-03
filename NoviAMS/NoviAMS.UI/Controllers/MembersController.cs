using Microsoft.AspNetCore.Mvc;
using NoviAMS.BFF.Interfaces;
using NoviAMS.BFF.Services;
using NoviAMS.Domain.Models;

namespace NoviAMS.UI.Controllers;

[ApiController]
[Route("[controller]")]
public class MembersController : ControllerBase
{ 
    private readonly ILogger<MembersController> _logger;
    private readonly IMemberService _svc;

    public MembersController(IMemberService svc, ILogger<MembersController> logger)
    {
        _svc = svc;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Member> Get()
    {
        return _svc.Get();
    }

    [HttpGet("{id}")]
    public MemberDetail Get(string? id)
    {
        return _svc.Get(id);
    }
}

