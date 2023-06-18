using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Paging;
using Project.Service.DataAccess;
using Project.Service.Models;
using Project.Service.Services;
using X.PagedList;

namespace Project.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly VehicleDBContext _context;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleMakesController(VehicleDBContext context, IVehicleService vehicleService, IMapper mapper)
        {
            _context = context;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        // GET: VehicleMakes
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


            var vehicleMakes = await _vehicleService.GetAllVehicleMakes();

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakes = vehicleMakes.Where(s => s.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(make => make.Name).ToList();
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(make => make.Name).ToList();
                    break;
            }

            int pageSize = 5;
            var mappedVehicleMakes = _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);

            IPagedList<VehicleMakeViewModel> pagedVehicleMakes = mappedVehicleMakes.ToPagedList(pageNumber.GetValueOrDefault(1), pageSize);
            return View(pagedVehicleMakes);


        }

        // GET: VehicleMakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleService.GetMakeDetails(id);

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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                await _vehicleService.CreateVehicleMake(vehicleMake);
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

            var vehicleMake = await _vehicleService.GetVehicleMakeById(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }
            var viewMake = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(viewMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    await _vehicleService.UpdateVehicleMake(vehicleMake);
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

            var vehicleMake = await _vehicleService.GetMakeDetails(id);

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
            var vehicleMake = await _vehicleService.GetVehicleMakeById(id);

            await _vehicleService.DeleteVehicleMake(vehicleMake.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            return (_context.VehicleMake?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}