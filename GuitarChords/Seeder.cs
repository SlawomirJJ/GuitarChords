using Azure.Core;
using GuitarChords.Models.Dtos;
using GuitarChords.Models.Entities;
using GuitarChords.Repositories.Interfaces;
using GuitarChords.Repositories.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GuitarChords
{
    public class Seeder
    {
        private readonly DataContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public Seeder(DataContext dbContext, UserManager<User> userManager, IAuthService authService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _authService = authService;
        }

        public async Task Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Chords.Any())
                {
                    var chords = GetChords();
                    _dbContext.Chords.AddRange(chords);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    RegistrationDto adminModel = new RegistrationDto
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com",
                        Password = "1qa2ws!@QW",
                    };
                    adminModel.Role = Enums.Roles.admin.ToString();
                    await _authService.Registration(adminModel);
                }
            }
            
        }

        private IEnumerable<Chord> GetChords()
        {
            var chords = new List<Chord>()
            {
                new Chord()
                {
                    ChordName = "E Major",
                    FirstString = 0,
                    SecondString = 0,
                    ThirdString = 1,
                    FourthString = 2,
                    FifthString = 2,
                    SixthString = 0,
                },
                new Chord()
                {
                    ChordName = "E Minor",
                    FirstString = 0,
                    SecondString = 0,
                    ThirdString = 0,
                    FourthString = 1,
                    FifthString = 1,
                    SixthString = 0,
                },
                new Chord()
                {
                    ChordName = "E 7",
                    FirstString = 0,
                    SecondString = 0,
                    ThirdString = 1,
                    FourthString = 0,
                    FifthString = 2,
                    SixthString = 0,
                },
                new Chord()
                {
                    ChordName = "E 5",
                    FirstString = null,
                    SecondString = null,
                    ThirdString = null,
                    FourthString = null,
                    FifthString = 2,
                    SixthString = 0,
                },
                new Chord()
                {
                    ChordName = "E sus2",
                    FirstString = 2,
                    SecondString = 5,
                    ThirdString = 4,
                    FourthString = 2,
                    FifthString = null,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "E sus2",
                    FirstString = 7,
                    SecondString = 7,
                    ThirdString = 9,
                    FourthString = 9,
                    FifthString = 7,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "E sus4",
                    FirstString = 0,
                    SecondString = 0,
                    ThirdString = 2,
                    FourthString = 2,
                    FifthString = 2,
                    SixthString = 0,
                },
                new Chord()
                {
                    ChordName = "F sus4",
                    FirstString = 1,
                    SecondString = 1,
                    ThirdString = 3,
                    FourthString = 3,
                    FifthString = 3,
                    SixthString = 1,
                },
                new Chord()
                {
                    ChordName = "F Major",
                    FirstString = 1,
                    SecondString = 1,
                    ThirdString = 2,
                    FourthString = 3,
                    FifthString = 3,
                    SixthString = 1,
                },
                new Chord()
                {
                    ChordName = "F Major",
                    FirstString = 1,
                    SecondString = 1,
                    ThirdString = 2,
                    FourthString = 3,
                    FifthString = null,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "F Major",
                    FirstString = 8,
                    SecondString = 10,
                    ThirdString = 10,
                    FourthString = 10,
                    FifthString = 8,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "F Major",
                    FirstString = 13,
                    SecondString = 10,
                    ThirdString = 10,
                    FourthString = 10,
                    FifthString = null,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "F Major",
                    FirstString = 13,
                    SecondString = 13,
                    ThirdString = 14,
                    FourthString = 15,
                    FifthString = 15,
                    SixthString = 13,
                },
                new Chord()
                {
                    ChordName = "F# Major",
                    FirstString = 2,
                    SecondString = 2,
                    ThirdString = 3,
                    FourthString = 4,
                    FifthString = 4,
                    SixthString = 2,
                },
                new Chord()
                {
                    ChordName = "G Major",
                    FirstString = 3,
                    SecondString = 0,
                    ThirdString = 0,
                    FourthString = 0,
                    FifthString = 2,
                    SixthString = 3,
                },
                new Chord()
                {
                    ChordName = "G Major",
                    FirstString = 3,
                    SecondString = 0,
                    ThirdString = 0,
                    FourthString = 0,
                    FifthString = 2,
                    SixthString = 3,
                },
                new Chord()
                {
                    ChordName = "G Minor",
                    FirstString = 3,
                    SecondString = 3,
                    ThirdString = 3,
                    FourthString = 5,
                    FifthString = 5,
                    SixthString = 3,
                },
                new Chord()
                {
                    ChordName = "G# sus4",
                    FirstString = 4,
                    SecondString = 2,
                    ThirdString = 1,
                    FourthString = 1,
                    FifthString = null,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "A Major",
                    FirstString = 0,
                    SecondString = 2,
                    ThirdString = 2,
                    FourthString = 2,
                    FifthString = 0,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "A Minor",
                    FirstString = 0,
                    SecondString = 1,
                    ThirdString = 2,
                    FourthString = 2,
                    FifthString = 0,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "A 7",
                    FirstString = 0,
                    SecondString = 2,
                    ThirdString = 0,
                    FourthString = 2,
                    FifthString = 0,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "A sus2",
                    FirstString = 0,
                    SecondString = 0,
                    ThirdString = 2,
                    FourthString = 2,
                    FifthString = 0,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "A sus4",
                    FirstString = 0,
                    SecondString = 3,
                    ThirdString = 2,
                    FourthString = 2,
                    FifthString = 0,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "A maj7",
                    FirstString = 0,
                    SecondString = 2,
                    ThirdString = 1,
                    FourthString = 2,
                    FifthString = 0,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "A m7",
                    FirstString = 0,
                    SecondString = 1,
                    ThirdString = 0,
                    FourthString = 2,
                    FifthString = 0,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "A 7sus4",
                    FirstString = 0,
                    SecondString = 3,
                    ThirdString = 0,
                    FourthString = 2,
                    FifthString = 0,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "C Major",
                    FirstString = 0,
                    SecondString = 1,
                    ThirdString = 0,
                    FourthString = 2,
                    FifthString = 3,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "D Major",
                    FirstString = 2,
                    SecondString = 3,
                    ThirdString = 2,
                    FourthString = 0,
                    FifthString = 0,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "D Minor",
                    FirstString = 1,
                    SecondString = 3,
                    ThirdString = 2,
                    FourthString = 0,
                    FifthString = null,
                    SixthString = null,
                },
                new Chord()
                {
                    ChordName = "D sus2",
                    FirstString = 0,
                    SecondString = 3,
                    ThirdString = 2,
                    FourthString = 0,
                    FifthString = null,
                    SixthString = null,
                }

            };

            return chords;
        }
    }
}
