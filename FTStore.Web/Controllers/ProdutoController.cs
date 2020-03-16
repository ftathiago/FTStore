using System;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FTStore.App.Models;
using FTStore.App.Services;
using System.Collections.Generic;
using System.Linq;

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
        public IActionResult Post([FromBody]Product product)
        {
            if (product == null)
                return BadRequest("As informações do produto não foram enviadas.");
            var productSaved = _produtoService.Save(product);
            if (productSaved == null)
                return BadRequest(_produtoService.GetErrorMessages());
            return Created(".", productSaved);
        }

        [HttpPut("{id}")]
        public IActionResult UploadProductImagem(int id)
        {
            var file = Request.Form.Files[0];// formFiles.FirstOrDefault();
            if (file == null)
                return BadRequest("Non file sended");

            var fileName = file.FileName;
            using (MemoryStream productImage = new MemoryStream())
            {
                file.CopyTo(productImage);
                var fileUploaded = _produtoService.AddProductImage(id, productImage, fileName);
                if (!fileUploaded)
                    return BadRequest(_produtoService.GetErrorMessages());
                return Ok();
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("There is no product information to handle");

                var produtoAlterado = _produtoService.Update(product);
                if (produtoAlterado == null)
                    return BadRequest(_produtoService.GetErrorMessages());
                return Ok(produtoAlterado);
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
