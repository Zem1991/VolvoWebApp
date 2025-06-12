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

namespace VolvoWebApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VehiclesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicle.OrderByDescending(x => x.LastUpdate).ToListAsync());
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
            List<Vehicle> filtered = await _context.Vehicle
                .Where(x => 
                    (string.IsNullOrEmpty(chassisSeries) || x.ChassisSeries.Equals(chassisSeries)) 
                    && (string.IsNullOrEmpty($"{chassisNumber}") || x.ChassisNumber == chassisNumber))
                .ToListAsync();
            return base.View("Index", filtered);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
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
        public async Task<IActionResult> Create([Bind("ChassisSeries,ChassisNumber,Type,Color")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    string message = ex.InnerException?.Message ?? ex.Message;
                    TempData["ErrorMessage"] = message;
                    ModelState.AddModelError("", message);
                }
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ChassisSeries,ChassisNumber,Type,Color")] VehicleUpdateDTO vehicleDto)
        {
            if (id != vehicleDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Vehicle? vehicle = _context.Vehicle.FirstOrDefault(x => x.Id == vehicleDto.Id);
                    if (vehicle == null)
                    {
                        return NotFound();
                    }

                    vehicle = _mapper.Map(vehicleDto, vehicle);
                    vehicle.WriteUpdate();
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicleDto.Id))
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
            return View(vehicleDto);
        }

        // GET: Vehicles/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var vehicle = await _context.Vehicle.FindAsync(id);
            try
            {
                if (vehicle != null)
                {
                    _context.Vehicle.Remove(vehicle);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                string message = ex.InnerException?.Message ?? ex.Message;
                TempData["ErrorMessage"] = message;
                ModelState.AddModelError("", message);
            }
            return View(vehicle);
        }

        private bool VehicleExists(string id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
