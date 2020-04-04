using System;
using Microsoft.AspNetCore.Mvc;
using FTStore.App.Models;
using FTStore.App.Services;

namespace FTStore.Web.Controllers
{
    [Route("api/[Controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUserService _usuarioService;

        public UsuarioController(IUserService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public ActionResult Post([FromBody]UserRequest usuario)
        {
            var novoUsuario = _usuarioService.Save(usuario);
            if (novoUsuario == null)
                return BadRequest(_usuarioService.GetErrorMessages());
            return Ok(novoUsuario);
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("VerificarUsuario")]
        public ActionResult VerificarUsuario([FromBody]UserRequest usuario)
        {
            var usuarioAutenticado = _usuarioService.Authenticate(usuario.Email, usuario.Password);
            if (usuarioAutenticado == null)
                return BadRequest(_usuarioService.GetErrorMessages());
            return Ok(usuarioAutenticado);
        }
    }
}
