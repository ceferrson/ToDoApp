using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoAppNTier.Business.Interfaces;
using ToDoAppNTier.Dtos.Dtos;
using ToDoAppNTier.UI.Extensions;
using ToDoAppNTIer.Common.Response;

namespace ToDoAppNTier.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workService;

        public HomeController(IWorkService workService) 
        {
            _workService = workService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _workService.GetAll();
            return this.ResponseView(response);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto workCreateDto)
        {
            var response = await _workService.Create(workCreateDto);
            return this.ResponseRedirectToAction(response, "Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _workService.GetById(id);
            return this.ResponseView(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WorkDto workUpdateDto)
        {
            var response = await _workService.Update(workUpdateDto); 
            return this.ResponseRedirectToAction(response, "Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var response = await _workService.Remove(id);
            return this.ResponseRedirectToAction(response, "Index");
        }

        public IActionResult NotFound(int statusCode)
        {
            ViewBag.ErrorCode = statusCode;
            return View();
        }

        public IActionResult Error()
        {
            //Get Exception Error:
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //Get folder path 
            var logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
            //create log folder
            DirectoryInfo directoryInfo = new DirectoryInfo(logFolderPath);
            if(!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            //Get file name
            var logFileName = DateTime.Now.ToString() + ".txt";
            logFileName = logFileName.Replace("/", "-");
            logFileName = logFileName.Replace(" ", "_");
            logFileName = logFileName.Replace(":", "-");

            //Get file path:
            var logFilePath = Path.Combine(logFolderPath, logFileName);

            //Create File
            FileInfo fileInfo = new FileInfo(logFilePath);

            var textWriter = fileInfo.CreateText();
            textWriter.WriteLine($"Error happened here: {exception.Path}");
            textWriter.WriteLine($"Error: {exception.Error}");
            textWriter.Close();
            return View();


        }

    }
}
