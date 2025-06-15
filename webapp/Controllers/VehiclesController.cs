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
            //TODO: if error shows, use data.tolist()
            return View(data);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = await _service.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
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
            IEnumerable<VehicleReadDTO> data = await _service.GetByChassisId(chassisSeries, chassisNumber);
            return base.View("Index", data);
        }

        // GET: Vehicles/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChassisSeries,ChassisNumber,Type,Color")] VehicleCreateDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
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
            return View(dto);
        }

        // GET: Vehicles/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = await _service.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            VehicleUpdateDTO toUpdate = _mapper.Map<VehicleUpdateDTO>(vehicle);
            return View(toUpdate);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Color")] VehicleUpdateDTO dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(dto.Id))
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
            return View(dto);
        }

        // GET: Vehicles/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = await _service.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
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

        private bool VehicleExists(string id)
        {
            return _service.GetByIdAsync(id) != null;
        }
    }
}
