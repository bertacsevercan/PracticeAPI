using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Models;


namespace PracticeAPI.Controllers
{
    [ApiController]
    // items => controller
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                CreateDir(folderPath);

                string[] filePaths = Directory.GetFiles(folderPath);

                List<Item> items = new List<Item>();

                foreach (string filePath in filePaths)
                {
                    FileInfo file = new FileInfo(filePath);
                    Item item = new Item { Size = file.Length, Name = file.Name, DateUpload = file.CreationTime, URL = "https://localhost:5001/uploads/" + file.Name };
                    items.Add(item);
                }

                return Ok(items);

            }
            catch
            {
                return BadRequest();
            }



        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                // Guid guid = Guid.NewGuid();
                // string[] nameArr = file.FileName.Split(".");
                // string type = "." + nameArr[nameArr.Length - 1];

                CreateDir(folderPath);

                string path = Path.Combine(folderPath, file.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                return Ok(new { message = "File uploaded successfully!" });
            }
            catch
            {
                return BadRequest();
            }
        }

        private void CreateDir(string dirPath)
        {
            bool isFolderExists = Directory.Exists(dirPath);
            if (!isFolderExists) Directory.CreateDirectory(dirPath);

        }


    }
}
