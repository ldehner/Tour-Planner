﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using TourPlanner.API.BL;
using TourPlanner.API.DAL;
using TourPlanner.API.Data;
using TourPlanner.API.Exceptions;
using TourPlanner.API.Mapping;
using TourPlanner.Data;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Converters;

namespace TourPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourPlannerController : ControllerBase
    {
        private readonly ToursDataContext _context;
        private readonly ITourManager _tourManager;
        private readonly ITourLogManager _tourLogManager;
        private readonly IMapQuestManager _mapQuestManager;

        public TourPlannerController(ToursDataContext context, ITourManager tourManager, ITourLogManager tourLogManager, IMapQuestManager mapQuestManager)
        {
            _context = context;
            _tourManager = tourManager;
            _tourLogManager = tourLogManager;
            _mapQuestManager = mapQuestManager;
        }
        [HttpGet("GetTours")]
        public async Task<ActionResult<List<PresentationTour>>> Get()
        {
            return Ok(await _tourManager.GetToursAsync());
        }

        [HttpGet("GetTours/{TourId}")]
        public async Task<ActionResult<PresentationTour>> Get(Guid TourId)
        {
            try
            {
                var result = await _tourManager.GetTourAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
        }

        [HttpPost("AddTour")]
        public async Task<ActionResult<List<PresentationTour>>> AddTour(SimpleTour tour)
        {
            var result = await _mapQuestManager.GetRouteAsync(tour.Start, tour.Destination, "fastest");
            return Ok(await _tourManager.AddTourAsync(tour, result.Distance, result.Time));
        }
        //DeleteTour
        [HttpDelete("DeleteTour/{TourId}")]
        public async Task<ActionResult<List<PresentationTour>>> DeleteTour(Guid TourId)
        {
            try
            {
                var result = await _tourManager.DeleteTourAsync(TourId);
                return Ok(result);
            }catch(TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
        }
        //EditTour
        [HttpPut("UpdateTour/{TourId}")]
        public async Task<ActionResult<PresentationTour>> UpdateTour(Guid TourId, SimpleTour request)
        {
            var mapquest = await _mapQuestManager.GetRouteAsync(request.Start, request.Destination, "fastest");
            try
            {
                var result = await _tourManager.UpdateTourAsync(TourId, request, mapquest.Distance, mapquest.Time);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
        }
        //AddLog
        [HttpPost("AddLog/{TourId}")]
        public async Task<ActionResult<PresentationTour>> AddLog(Guid TourId, SimpleLog log)
        {
            try
            {
                var result = await _tourLogManager.AddLogAsync(TourId, log);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {

                return BadRequest("Tour not found");
            }
        }
        
        //GetLogs
        [HttpGet("GetLogs/{TourId}")]
        public async Task<ActionResult<List<PresentationLog>>> GetLogs(Guid TourId)
        {
            try
            {
                var result = await _tourLogManager.GetLogsAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {

                return BadRequest("Tour not found");
            }

        }
        //GetLogs/{LogId}
        [HttpGet("GetLogs/{TourId}/{LogId}")]
        public async Task<ActionResult<PresentationLog>> GetLog(Guid TourId, Guid LogId)
        {
            try
            {
                var result = await _tourLogManager.GetLogAsync(TourId, LogId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
            catch (LogNotFoundException)
            {
                return BadRequest("Log not found");
            }
        }
        //DeleteLog
        [HttpDelete("DeleteLog/{TourId}/{LogId}")]
        public async Task<ActionResult<List<PresentationLog>>> DeleteLog(Guid TourId, Guid LogId)
        {
            try
            {
                var result = await _tourLogManager.DeleteLogAsync(TourId, LogId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
            catch (LogNotFoundException)
            {
                return BadRequest("Log not found");
            }
        }

    }
}
