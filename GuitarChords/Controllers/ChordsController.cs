using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuitarChords;
using GuitarChords.Models;

namespace GuitarChords.Controllers
{
    public class ChordsController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}
