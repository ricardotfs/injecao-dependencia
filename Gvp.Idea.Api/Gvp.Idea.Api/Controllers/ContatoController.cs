using Gvp.Idea.Api.DAO;
using Gvp.Idea.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Gvp.Idea.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContatoController : Controller
    {
        [HttpGet("{id}")]
        public SacMobileContato Get([FromServices]ZapSacDAO _zapSacDAO, int id)
        {
            return _zapSacDAO.ConsultaContatoZapSac(id);
        }

        [HttpGet]
        public IEnumerable Get([FromServices]ZapSacDAO _zapSacDAO)
        {
            return _zapSacDAO.ConsultaContatoZapSac();
        }
    }
}
