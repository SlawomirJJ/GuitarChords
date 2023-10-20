using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuitarChords;
using GuitarChords.Models;
using GuitarChords.Interfaces;
using GuitarChords.Dtos;

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

        public async Task<IActionResult> CreateChord()
        {
            return View();
        }

        public async Task<IActionResult> SearchChordResults(string chordName)
        {
            List<ChordDto> chordList = await _chordService.SearchChord(chordName);

            return View("ChordListView",chordList);
        }

        public async Task<IActionResult> ShowUpdateChord(Guid id)
        {
            //var foundChord = 
            return View("ShowEditChord");
        }

        public async Task<IActionResult> ProcessEditChord(Chord chord)
        {
            await _chordService.UpdateChord(chord);
            return View("Index");
        }

        public async Task<IActionResult> DeleteChord()
        {
            return View("Index");
        }

    }
}
