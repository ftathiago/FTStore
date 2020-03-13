using System;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FTStore.App.Models;
using FTStore.App.Services;

namespace FTStore.Web.Controllers
{
    [Route("api/[Controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProductService _produtoService;
        private IHttpContextAccessor _httpContextAccessor;
        public ProdutoController(
            IProductService produtoService,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _produtoService = produtoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_produtoService.ListAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public IActionResult Post()
        {
            Product produto = PegarProdutoDaRequisição();
            if (produto == null)
                return BadRequest("As informações do produto não foram enviadas.");

            var imagemProduto = _httpContextAccessor
                .HttpContext.Request.Form.Files["imagem-produto"];
            var nomeArquivo = imagemProduto?.FileName;

            using (MemoryStream imagemProdutoStream = PegarStreamImagem(imagemProduto))
            {
                var produtoSalvo = _produtoService.Save(produto, imagemProdutoStream, nomeArquivo);
                if (!produtoSalvo)
                    return BadRequest(_produtoService.GetErrorMessages());

                return Created(".", produto);
            }
        }

        [HttpPut]
        public IActionResult Put()
        {
            try
            {
                Product produto = PegarProdutoDaRequisição();
                if (produto == null)
                    return BadRequest("As informações do produto não foram enviadas.");
                var imagemProduto = _httpContextAccessor
                    .HttpContext.Request.Form.Files["imagem-produto"];
                var nomeArquivo = imagemProduto?.FileName;

                using (MemoryStream imagemProdutoStream = PegarStreamImagem(imagemProduto))
                {
                    var produtoAlterado = _produtoService.Update(produto, imagemProdutoStream, nomeArquivo);
                    if (produtoAlterado == null)
                        return BadRequest(_produtoService.GetErrorMessages());
                    return Ok(produtoAlterado);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Product PegarProdutoDaRequisição()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _httpContextAccessor.HttpContext.Request.Form.TryGetValue("produto", out var produtoJSON);
            if (string.IsNullOrEmpty(produtoJSON))
                return null;
            var produto = JsonSerializer.Deserialize<Product>(produtoJSON, options);
            return produto;
        }

        private MemoryStream PegarStreamImagem(IFormFile formFile)
        {
            if (formFile == null)
                return null;
            MemoryStream memStream = new MemoryStream();
            formFile.CopyTo(memStream);
            memStream.Position = 0;
            return memStream;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var apagou = _produtoService.Delete(id);
            if (!apagou)
                return BadRequest(_produtoService.GetErrorMessages());

            return Ok();
        }
    }
}