using Gvp.Idea.Api.DAO;
using Gvp.Idea.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Gvp.Idea.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ConfigController : Controller
    {
        [HttpGet("{idc}")]
        public SacMobileConfig Get([FromServices]ZapSacDAO _zapSacDAO, int idc)
        {
            return _zapSacDAO.ConsultaConfigZapSac(idc);
        }
    }
}