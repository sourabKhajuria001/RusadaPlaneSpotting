using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using RusadaAircraftSpotting.Models;
using RusadaAircraftSpotting.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RusadaAircraftSpotting.Controllers
{
    public class AircraftSightingController : Controller
    {
        private readonly AircraftSightingRepository _repository = null;
        private readonly IFileProvider _fileProvider;

        public AircraftSightingController(AircraftSightingRepository repository, IFileProvider fileProvider)
        {
            _repository = repository;
            _fileProvider = fileProvider;
        }




        public ViewResult New(bool isSuccess = false, int newRecordId = 0)
        {

            ViewBag.isSuccess = isSuccess;
            ViewBag.newRecordId = newRecordId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewRecord(AircraftSightingModel model, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                //if (file == null || file.Length == 0)
                //    return Content("file not selected");

                string fullPath = "";
                string loc = "";

                if (file != null && file.Length > 0)
                {

                    fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.GetFilename());
                    loc = "/images/" + file.GetFilename();
                }
                int id = await _repository.AddNewSpottingAsync(model, loc);
                if (id > 0)
                {
                    #region copy image to below location 
                    try
                    {
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    #endregion
                    return RedirectToAction(nameof(New), new { isSuccess = true, newRecordId = id });
                }



            }

            return View("New");
        }
 
        public async Task<IActionResult> All(string searchstring, string searchBy)
        {
            #region add search opti0ns
            var searchList = new List<SelectListItem>();
            searchList.Add(new SelectListItem() { Text = "Make", Value = "make" });
            searchList.Add(new SelectListItem() { Text = "Model", Value = "model" });
            searchList.Add(new SelectListItem() { Text = "Reg", Value = "reg" });
            #endregion

            ViewBag.searchList = searchList;
            var _data = await _repository.GetAllSpottingsAsync(searchstring, searchBy);
            return View(_data);

        }
        public async Task<IActionResult> Details(int id)
        {

            var data = await _repository.GetByIDAsync(id);
            return View(data);
        }

        public async Task<IActionResult> Update(int id)
        {

            var data = await _repository.GetByIDAsync(id);
            return View(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
           var deleted = await _repository.DeleteByIdAsync(id);  //TODO
            return RedirectToAction(nameof(All));

        }


        public async Task<IActionResult> UpdateRecord(int id, AircraftSightingModel model)
        {
            var updatedId = await _repository.UpdateAsync(id, model);

            if (updatedId >= 1)
            {
                return RedirectToAction(nameof(Details), new { id = updatedId });
            }
            return RedirectToAction(nameof(All));
        }



    }//
}//
