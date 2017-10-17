using Gvp.Idea.Api.DAO;
using Gvp.Idea.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Gvp.Idea.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TelefoneController : Controller
    {
        [HttpGet("{idc}")]
        public SacMobileTelefone Get([FromServices]ZapSacDAO _zapSacDAO, int idc)
        {
            return _zapSacDAO.ConsultaTelefoneZapSac(idc);
        }
    }
}
