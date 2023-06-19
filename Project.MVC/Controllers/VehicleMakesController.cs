using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Models;
using Project.Services.DataAccess;
using Project.Services.Models;
using Project.Services.Services;
using Project.Services.Utilities;
using X.PagedList;

namespace Project.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly VehicleDBContext _context;
        private readonly IMakeService _makeService;
        private readonly IMapper _mapper;

        public VehicleMakesController(VehicleDBContext context, IMakeService makeService, IMapper mapper)
        {
            _context = context;
            _makeService = makeService;
            _mapper = mapper;
        }

        // GET: VehicleMakes
        public async Task<IActionResult> Index(SortingParameters sortingParameters,FilteringParameters filteringParameters, PagingParameters pagingParameters)
        {

            UpdatePageNumber(filteringParameters, pagingParameters);

            ViewData["CurrentSort"] = sortingParameters.sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortingParameters.sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = filteringParameters.searchString;


            var vehicleMakes = await _makeService.GetAllVehicleMakes();

            vehicleMakes = await _makeService.FilterMakesAsync(vehicleMakes, filteringParameters);

           vehicleMakes= await _makeService.SortMakesAsync(vehicleMakes, sortingParameters);

            IPagedList<VehicleMake> pagedVehicleMakes = await _makeService.PageMakesAsync(vehicleMakes, pagingParameters);
            var mappedMakes = _mapper.Map<IPagedList<VehicleMakeViewModel>>(pagedVehicleMakes);
            return View(mappedMakes);

        }

        private void UpdatePageNumber(FilteringParameters filteringParameters, PagingParameters pagingParameters)
        {
            if (!string.IsNullOrEmpty(filteringParameters.searchString))
            {
                pagingParameters.pageNumber = 1;
            }
            else
            {
                filteringParameters.searchString = pagingParameters.currentFilter;
            }
        }

        // GET: VehicleMakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _makeService.GetMakeDetails(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }
            var viewMake = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(viewMake);
        }

        // GET: VehicleMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                await _makeService.CreateVehicleMake(vehicleMake);
                return RedirectToAction(nameof(Index));
            }
            var viewMake = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(viewMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _makeService.GetVehicleMakeById(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }
            var viewMake = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(viewMake);
        }

        // POST: VehicleMakes/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMakeViewModel viewMake)
        {
            var vehicleMake = _mapper.Map<VehicleMake>(viewMake);

            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _makeService.UpdateVehicleMake(vehicleMake);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeExists(vehicleMake.Id))
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
            var makeView = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(makeView);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _makeService.GetMakeDetails(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            var viewMake = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(viewMake);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleMake = await _makeService.GetVehicleMakeById(id);

            await _makeService.DeleteVehicleMake(vehicleMake.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            return (_context.VehicleMake?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}