using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using FTStore.App.Models;
using FTStore.App.Services;
using System.Linq;

namespace FTStore.Web.Controllers
{
    [Route("api/[Controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProductService _productService;
        public ProdutoController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _productService.ListAll();
            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (product == null)
                return BadRequest("There is no product information to handle");
            var productSaved = _productService.Save(product);
            if (productSaved == null)
                return BadRequest(_productService.GetErrorMessages());
            return Created(".", productSaved);
        }

        [HttpPut("{id}")]
        public IActionResult UploadProductImagem(int id)
        {
            var file = Request.Form.Files[0];
            if (file == null)
                return BadRequest("Non file sended");

            var fileName = file.FileName;
            using (MemoryStream productImage = new MemoryStream())
            {
                file.CopyTo(productImage);
                var fileUploaded = _productService.ReplaceProductImagem(id, productImage, fileName);
                if (!fileUploaded)
                    return BadRequest(_productService.GetErrorMessages());
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

                var produtoAlterado = _productService.Update(product);
                if (produtoAlterado == null)
                    return BadRequest(_productService.GetErrorMessages());
                return Ok(produtoAlterado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _productService.Delete(id);
            if (!deleted)
                return BadRequest(_productService.GetErrorMessages());

            return Ok();
        }
    }
}
