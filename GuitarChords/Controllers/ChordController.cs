using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuitarChords;
using Microsoft.AspNetCore.Authorization;
using GuitarChords.Repositories.Interfaces;
using GuitarChords.Models.Entities;
using GuitarChords.Models.Dtos;
using GuitarChords.Models.Dtos.Requests;
using GuitarChords.Models.Results;
using System.Drawing.Printing;
using Azure.Core;

namespace GuitarChords.Controllers
{

    public class ChordController : Controller
    {
        private readonly IChordService _chordService;

        public ChordController(IChordService chordService)
        {
            _chordService = chordService;
        }



        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ChordList(int pageNumber = 1)
        {
            ChordListRequest request = new ChordListRequest()
            {
                PageNumber = pageNumber,
                PageSize = 10,
            };
            ChordListResponse chordList = await _chordService.GetAllChords(request);
            return View("ChordListView", chordList);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ShowCreateChord()
        {
            return View("CreateChord");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ProcessCreateChord(CreateChordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateChord", request);
            }
            await _chordService.CreateChord(request);
            return View("Index");
        }

        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> SearchChordResults(string searchName = "A", int pageNumber = 1)
        {
            ChordListRequest request = new ChordListRequest()
            {
                PageNumber = pageNumber,
                PageSize = 10,
                SearchName = searchName,
            };
            ChordListResponse chordList = await _chordService.SearchChord(request);

            return View("ChordListView",chordList);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ShowUpdateChord(Guid id)
        {
            return View("ShowEditChord");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ProcessEditChord(UpdateChordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("ShowEditChord", request);
            }
            await _chordService.UpdateChord(request);
            return View("Index");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteChord(Guid id)
        {
            await _chordService.DeleteChord(id);
            return View("Index");
        }

    }
}
