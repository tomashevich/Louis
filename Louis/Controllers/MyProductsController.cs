using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Louis.Data;
using Louis.Models;
using Louis.Entities;
using Louis.Repositories;
using Louis.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Louis.Controllers
{
    public class MyProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;

        public MyProductsController(ApplicationDbContext context, IProductService productService, IMapper mapper, IHostingEnvironment environment)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
            _env = environment;
        }

        // GET: MyProducts
        public async Task<IActionResult> Index()
        {           
            var products = await _productService.GetAll();
            var models = products.Select(p => _mapper.Map<Models.Product>(p));
            return View(models);
        }

        // GET: MyProducts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetById(id.Value);
      
            if (product == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<Models.Product>(product));
        }

        // GET: MyProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Photo,Price,LastUpdated")] Models.Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
             
                await _productService.Add(_mapper.Map<Entities.Product>(product));
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: MyProducts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetById(id.Value);
         
            if (product == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<Models.Product>(product));
        }

        // POST: MyProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Code,Name,Photo,Price,LastUpdated")] Models.Product product, IFormFile file)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var productToSave = _mapper.Map<Entities.Product>(product);
                    if (file != null)
                    {
                        var imageUrl = await ImageUpload(file);
                        productToSave.ImageUrl = imageUrl;
                    }
                    await _productService.Update(productToSave);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //maybe add logs ?
                    throw;
                }
                              
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: MyProducts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetById(id.Value);
           
            if (product == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<Models.Product>(product));
        }

        // POST: MyProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

       private async Task<string> ImageUpload(IFormFile file)
        {
            var filePath = "";
            if (file != null && file.Length > 0)
            {
                var imagePath = @"\Upload\Images\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var fileName = Path.GetFileName(Guid.NewGuid().ToString() + "." + file.FileName.Split(".")[1].ToLower());
                var fullPath = uploadPath + fileName;

                // imagePath = imagePath + @"\";
                //filePath =  Path.Combine(@"~/", fileName);
                filePath = fileName;

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                //ViewData["FileLocation"] = filePath;
            }
            return filePath;
        }
    }
}
