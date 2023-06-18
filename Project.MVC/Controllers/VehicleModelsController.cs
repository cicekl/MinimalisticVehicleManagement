using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.MVC.DependencyInjection;
using Project.Service.DataAccess;
using Project.Service.Models;
using Project.Service.Services;
using X.PagedList;

namespace Project.MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly VehicleDBContext _dbContext;
        private readonly IVehicleService   _vehicleService;
        private IMapper _mapper;

        public VehicleModelsController(VehicleDBContext context,IVehicleService vehicleService,IMapper mapper)
        {
            _dbContext = context;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var vehicleModels = await _vehicleService.GetAllVehicleModels();

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModels = vehicleModels.Where(s => s.Make.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(s => s.Name).ToList();
                    break;
            }

            int pageSize = 5;
            var mappedVehicleModels = _mapper.Map<List<VehicleModelViewModel>>(vehicleModels);

            IPagedList<VehicleModelViewModel> pagedVehicleModels = mappedVehicleModels.ToPagedList(pageNumber.GetValueOrDefault(1), pageSize);
            return View(pagedVehicleModels);
        }



        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleService.GetModelDetails(id);

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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
              await _vehicleService.CreateVehicleModel(vehicleModel);   
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

            var vehicleModel = await _vehicleService.GetVehicleModelById(id);

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
                   await _vehicleService.UpdateVehicleModel(vehicleModel);
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

            var vehicleModel = await _vehicleService.GetModelDetails(id);

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
         
            var vehicleModel = await _vehicleService.GetVehicleModelById(id);

           await _vehicleService.DeleteVehicleModel(vehicleModel.Id);

            return RedirectToAction(nameof(Index)); 
        }

        private bool VehicleModelExists(int id)
        {
          return (_dbContext.VehicleModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
