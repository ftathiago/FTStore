using System;
using System.IO;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FTStore.App.Models;
using FTStore.App.Services;


namespace FTStore.Web.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var products = _productService.ListAll();
            if (!products.Any())
                return NoContent();

            return Ok(new { data = products });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NoContent();
            return Ok(new { data = product });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductRequest product)
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
            var form = Request.Form;
            if (form == null)
                return BadRequest();

            if (!form.Files.Any())
                return BadRequest("Non file sended");

            var file = Request.Form.Files.First();

            var fileName = file.FileName;
            using MemoryStream productImage = new MemoryStream();

            file.CopyTo(productImage);
            var fileUploaded = _productService.ReplaceProductImagem(id, productImage, fileName);
            if (!fileUploaded)
                return BadRequest(_productService.GetErrorMessages());
            return Ok();

        }

        [HttpPut]
        public IActionResult Put([FromBody] ProductRequest product)
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
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new { error = ex.Message });
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
