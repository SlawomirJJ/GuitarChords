using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuitarChords;
using GuitarChords.Models;
using GuitarChords.Dtos;
using GuitarChords.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using GuitarChords.Repositories.Interfaces;

namespace GuitarChords.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> ChordList()
        {
            List<ChordDto> chordList = await _chordService.GetAllChords();
            return View("ChordListView", chordList);
        }

        public async Task<IActionResult> ShowCreateChord()
        {
            return View("CreateChord");
        }

        public async Task<IActionResult> ProcessCreateChord(CreateChordRequest request)
        {
            await _chordService.CreateChord(request);
            return View("Index");
        }

        public async Task<IActionResult> SearchChordResults(string chordName)
        {
            List<ChordDto> chordList = await _chordService.SearchChord(chordName);

            return View("ChordListView",chordList);
        }

        public async Task<IActionResult> ShowUpdateChord(Guid id)
        {
            return View("ShowEditChord");
        }

        public async Task<IActionResult> ProcessEditChord(Chord chord)
        {
            await _chordService.UpdateChord(chord);
            return View("Index");
        }

        public async Task<IActionResult> DeleteChord(Guid id)
        {
            await _chordService.DeleteChord(id);
            return View("Index");
        }

    }
}
