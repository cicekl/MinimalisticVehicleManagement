﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Models;
using Project.Services.DataAccess;
using Project.Services.Models;
using Project.Services.Services;
using Project.Services.Utilities;
using X.PagedList;

namespace Project.MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly VehicleDBContext _dbContext;
        private readonly IModelService   _modelService;
        private IMapper _mapper;

        public VehicleModelsController(VehicleDBContext context,IModelService modelService,IMapper mapper)
        {
            _dbContext = context;
            _modelService  = modelService;
            _mapper = mapper;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index(SortingParameters sortingParameters, FilteringParameters filteringParameters, PagingParameters pagingParameters)
        {

            UpdatePageNumber(filteringParameters, pagingParameters);

            ViewData["CurrentSort"] = sortingParameters.SortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortingParameters.SortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = filteringParameters.SearchString;

            var vehicleModels = await _modelService.GetAllVehicleModels();

            if (!string.IsNullOrEmpty(filteringParameters.SearchString))
            {
                vehicleModels = vehicleModels.Where(m => m.Make.Name.IndexOf(filteringParameters.SearchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            switch (sortingParameters.SortOrder)
            {
                case "name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(m => m.Name).ToList();
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(m => m.Name).ToList();
                    break;
            }

            pagingParameters.PageSize = 5;
            var pagedVehicleModels = vehicleModels.ToPagedList(pagingParameters.PageNumber.GetValueOrDefault(1), pagingParameters.PageSize);
            var mappedModels = _mapper.Map<IPagedList<VehicleModelViewModel>>(pagedVehicleModels);
            return View(mappedModels);
        }

        private void UpdatePageNumber(FilteringParameters filteringParameters, PagingParameters pagingParameters)
        {
            if (!string.IsNullOrEmpty(filteringParameters.SearchString))
            {
                pagingParameters.PageNumber = 1;
            }
            else
            {
                filteringParameters.SearchString = pagingParameters.CurrentFilter;
            }
        }



        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _modelService.GetModelDetails(id);

            if (vehicleModel == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(viewModel);
        }

        // GET: VehicleModels/Create
        public IActionResult Create()
        {
            ViewData["MakeId"] = new SelectList(_dbContext.VehicleMake, "Id", "Name");
            return View();
        }

        // POST: VehicleModels/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
              await _modelService.CreateVehicleModel(vehicleModel);   
                return RedirectToAction(nameof(Index));
            }
            ViewData["MakeId"] = new SelectList(_dbContext.VehicleMake, "Id", "Name", vehicleModel.MakeId);
            var viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(viewModel);
        }

        // GET: VehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _modelService.GetVehicleModelById(id);

            if (vehicleModel == null)
            {
                return NotFound();
            }
            ViewData["MakeId"] = new SelectList(_dbContext.VehicleMake, "Id", "Name", vehicleModel.MakeId);
            var viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(viewModel);
        }

        // POST: VehicleModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MakeId,Name,Abrv")] VehicleModelViewModel viewModel)
        {

            var vehicleModel = _mapper.Map<VehicleModel>(viewModel);

            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _modelService.UpdateVehicleModel(vehicleModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleModelExists(vehicleModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MakeId"] = new SelectList(_dbContext.VehicleMake, "Id", "Name", vehicleModel.MakeId);
            var modelView = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(modelView);
        }

        // GET: VehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _modelService.GetModelDetails(id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);

            return View(viewModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
         
            var vehicleModel = await _modelService.GetVehicleModelById(id);

           await _modelService.DeleteVehicleModel(vehicleModel.Id);

            return RedirectToAction(nameof(Index)); 
        }

        private bool VehicleModelExists(int id)
        {
          return (_dbContext.VehicleModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
