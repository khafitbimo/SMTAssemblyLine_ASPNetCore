using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMTAssemblyLineWebApp.Data;
using SMTAssemblyLineWebApp.Models;
using SMTAssemblyLineWebApp.Models.SMTViewModels;

namespace SMTAssemblyLineWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SMTContext _context;

        public HomeController(SMTContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var stations = _context.Stations.AsNoTracking().ToListAsync();
            //var stations = _context.Stations.Select(s => new StationCountDataToday
            //{
            //    STATION_ID = s.ID,
            //    STATION_NAME = s.STATION_NAME,
            //    STATION_COUNT_TODAY = _context.TransStations.Where(t => t.STATION_ID == s.ID && t.TIME_STAMP.Date == DateTime.Now.Date).Count()
            //}).ToList();

          

            var stations = _context.Stations.Select(s => new StationCountDataToday
            {
                STATION_ID = s.ID,
                STATION_NAME = s.STATION_NAME,
                STATION_COUNT_TODAY = _context.TransStations.Where(t => t.STATION_ID == s.ID && t.TIME_STAMP.Date == DateTime.Now.Date).Count(),
                STATION_AVG_COUNT_TODAY = _context.StationCountPerHours.Where(t=>t.STATION_ID==s.ID && t.COUNT_HOUR.Date==DateTime.Now.Date).Select(t=>t.COUNT_IN_HOUR).DefaultIfEmpty(0).Average(),
                STATION_MAX_COUNT_TODAY = _context.StationCountPerHours.Where(t => t.STATION_ID == s.ID && t.COUNT_HOUR.Date == DateTime.Now.Date).Select(t => t.COUNT_IN_HOUR).DefaultIfEmpty(0).Max(),
                STATION_MIN_COUNT_TODAY = _context.StationCountPerHours.Where(t => t.STATION_ID == s.ID && t.COUNT_HOUR.Date == DateTime.Now.Date).Select(t => t.COUNT_IN_HOUR).DefaultIfEmpty(0).Min(),
            }).ToList();
            return View( stations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult CreateStation(string title)
        {
            Station station = new Station();
            try
            {
                
                station.STATION_NAME = title;
                

                _context.Stations.Add(station);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(station);
        }

        [HttpPost]
        public JsonResult AddStationCount(int station_id)
        {
            int count = 0;
            StationCountDataToday result = new StationCountDataToday();
            try
            {
                TransStation transStation = new TransStation();

                transStation.STATION_ID = station_id;
                transStation.TIME_STAMP = DateTime.Now;
                _context.TransStations.Add(transStation);
                _context.SaveChanges();
                count = _context.TransStations.Where(t => t.STATION_ID == station_id && t.TIME_STAMP.Date == DateTime.Now.Date).Count();

                var isany = _context.StationCountPerHours.SingleOrDefault(t => t.STATION_ID==station_id && t.COUNT_HOUR.ToString("yyyy-MM-dd HH:00:00") == DateTime.Now.ToString("yyyy-MM-dd HH:00:00"));
                if (isany!=null)
                {
                    isany.COUNT_IN_HOUR = isany.COUNT_IN_HOUR + 1;
                    _context.SaveChanges();
                }
                else
                {
                    StationCountPerHour stationCountPerHour = new StationCountPerHour();
                    stationCountPerHour.STATION_ID = station_id;
                    stationCountPerHour.COUNT_HOUR = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:00:00"));
                    stationCountPerHour.COUNT_IN_HOUR = 1;
                    _context.StationCountPerHours.Add(stationCountPerHour);
                    _context.SaveChanges();
                }

                result = _context.Stations.Select(s => new StationCountDataToday
                {
                    STATION_ID = s.ID,
                    STATION_NAME = s.STATION_NAME,
                    STATION_COUNT_TODAY = _context.TransStations.Where(t => t.STATION_ID == s.ID && t.TIME_STAMP.Date == DateTime.Now.Date).Count(),
                    STATION_AVG_COUNT_TODAY = _context.StationCountPerHours.Where(t => t.STATION_ID == s.ID && t.COUNT_HOUR.Date == DateTime.Now.Date).Select(t => t.COUNT_IN_HOUR).DefaultIfEmpty(0).Average(),
                    STATION_MAX_COUNT_TODAY = _context.StationCountPerHours.Where(t => t.STATION_ID == s.ID && t.COUNT_HOUR.Date == DateTime.Now.Date).Select(t => t.COUNT_IN_HOUR).DefaultIfEmpty(0).Max(),
                    STATION_MIN_COUNT_TODAY = _context.StationCountPerHours.Where(t => t.STATION_ID == s.ID && t.COUNT_HOUR.Date == DateTime.Now.Date).Select(t => t.COUNT_IN_HOUR).DefaultIfEmpty(0).Min(),
                }).Where(t=> t.STATION_ID==station_id).SingleOrDefault();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetStation()
        {
            List<Station> station = new List<Station>();
            try
            {
                station = _context.Stations.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(station);
        }

        [HttpGet]
        public JsonResult GetStationCountToday(int station_id)
        {

            List<int> result = new List<int>();
            result = _context.StationCountPerHours
                               .Where(t => t.STATION_ID == station_id && t.COUNT_HOUR.Date == DateTime.Now.Date)
                               .OrderBy(t => t.COUNT_HOUR)
                               .Select(t => t.COUNT_IN_HOUR)
                               .ToList();
          

            return Json(result);
        }

        [HttpGet]
        public JsonResult GetMaxCountToday()
        {

            var maxCount = _context.StationCountPerHours
                           .Where(t => t.COUNT_HOUR.Date == DateTime.Now.Date)
                           .Select(t => t.COUNT_IN_HOUR)
                           .DefaultIfEmpty(0)
                           .Max();
                           
            //if (maxCount==0)
            //{
            //    return Json(100);
            //}

            return Json(maxCount);
        }
    }
}
