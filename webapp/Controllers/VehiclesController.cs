using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VolvoWebApp.Data;
using VolvoWebApp.Dtos;
using VolvoWebApp.Models;
using VolvoWebApp.Services;

namespace VolvoWebApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehiclesService _service;

        public VehiclesController(IMapper mapper, IVehiclesService service)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            IEnumerable<VehicleReadDTO> data = await _service.GetAllAsync();
            IEnumerable<VehicleModel> model = _mapper.Map<IEnumerable<VehicleModel>>(data);
            return View(model);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VehicleReadDTO? data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            VehicleModel model = _mapper.Map<VehicleModel>(data);
            return View(model);
        }

        // GET: Vehicles/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    VehicleCreateDTO dto = _mapper.Map<VehicleCreateDTO>(model);
                    await _service.CreateAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    string message = ex.InnerException?.Message ?? ex.Message;
                    TempData["ErrorMessage"] = message;
                    ModelState.AddModelError("", message);
                }
            }
            return View(model);
        }

        // GET: Vehicles/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VehicleReadDTO? data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            VehicleModel model = _mapper.Map<VehicleModel>(data);
            return View(model);
        }

        // POST: Vehicles/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, VehicleModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    VehicleUpdateDTO dto = _mapper.Map<VehicleUpdateDTO>(model);
                    await _service.UpdateAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.InnerException?.Message ?? ex.Message;
                    TempData["ErrorMessage"] = message;
                    ModelState.AddModelError("", message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Vehicles/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VehicleReadDTO? data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            VehicleModel model = _mapper.Map<VehicleModel>(data);
            return View(model);
        }

        // POST: Vehicles/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                string message = ex.InnerException?.Message ?? ex.Message;
                TempData["ErrorMessage"] = message;
                ModelState.AddModelError("", message);
            }
            return View();
        }

        // GET: Vehicles/SearchByChassis
        //public async Task<IActionResult> SearchByChassis()
        public IActionResult SearchByChassis()
        {
            return View();
        }

        // POST: Vehicles/ShowSearchByChassisResults
        public async Task<IActionResult> ShowSearchByChassisResults(string chassisSeries, uint chassisNumber)
        {
            if (chassisSeries is null)
                throw new ArgumentNullException(nameof(chassisSeries));
            //if (chassisNumber is null)
            //    throw new ArgumentNullException(nameof(chassisNumber));
            IEnumerable<VehicleReadDTO> data = await _service.GetByChassisId(chassisSeries, chassisNumber);
            IEnumerable<VehicleModel> model = _mapper.Map<IEnumerable<VehicleModel>>(data);
            return base.View("Index", model);
        }

        private bool VehicleExists(string id)
        {
            return _service.GetByIdAsync(id) != null;
        }
    }
}
