using Lab_4.Data;
using Lab_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_4.Services
{
    public class DbService
    {
        private CinemaContext _db;

        public DbService(CinemaContext context)
        {
            _db = context;
        }

        public List<Cinema> GetCinemas()
        {
            var cinema = from genres in _db.Genres
                         join films in _db.Films
                         on genres.GenreId equals films.GenreId
                         select new { name = genres.Name, age = films.AgeRestrictions, date = films.Duration, id = genres.GenreId };
            List<Cinema> list = new List<Cinema>();
            foreach (var pf in cinema) list.Add(new Cinema(pf.name, pf.age, pf.date, pf.id));
            return list;
        }

        public Cinema FindCinemaById(int id)
        {
            IQueryable<Cinema> cinemas = from genres in _db.Genres
                                         join films in _db.Films
                                         on genres.GenreId equals films.GenreId
                                         where genres.GenreId == id
                                         select new Cinema(genres.Name, films.AgeRestrictions, films.Duration, genres.GenreId);
            return cinemas.First();
        }

        public void DeleteCinema(int id)
        {
            var cinema = _db.Genres.First((genres) => genres.GenreId == id);
            _db.Genres.Remove(cinema);
            _db.SaveChanges();
        }

        public void UpdateCinema(Cinema cinema)
        {
            var genre = _db.Genres.First((genres) => genres.GenreId == cinema.Id);
            var film = _db.Films.First((films) => films.GenreId == cinema.Id);

            genre.Name = cinema.Name;
            film.AgeRestrictions = cinema.Age;

            _db.Genres.Update(genre);
            _db.Films.Update(film);
            _db.SaveChanges();
        }

        public IQueryable<Cinema> GetCinemas(string nameCinema, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Cinema> cinemas = from genres in _db.Genres
                                         join films in _db.Films
                                         on genres.GenreId equals films.GenreId
                                         select new Cinema(genres.Name, films.AgeRestrictions, films.Duration, genres.GenreId);

            if (!String.IsNullOrEmpty(nameCinema))
            {
                cinemas = cinemas.Where(p => p.Name.Contains(nameCinema));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    cinemas = cinemas.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeAsc:
                    cinemas = cinemas.OrderBy(s => s.Age);
                    break;
                case SortState.AgeDesc:
                    cinemas = cinemas.OrderByDescending(s => s.Age);
                    break;
                case SortState.DurationAsc:
                    cinemas = cinemas.OrderBy(s => s.Duration);
                    break;
                case SortState.DurationDesc:
                    cinemas = cinemas.OrderByDescending(s => s.Duration);
                    break;
                default:
                    cinemas = cinemas.OrderBy(s => s.Name);
                    break;
            }
            return cinemas;
        }

        public void AddCinema(Cinema cinema)
        {
            Genre genre = new Genre { Name = cinema.Name, Description = "Захватывающий" };
            _db.Genres.Add(genre);
            _db.SaveChanges();

            Film film = new Film { AgeRestrictions = cinema.Age, Duration = cinema.Duration, GenreId = genre.GenreId };
            _db.Films.Add(film);
            _db.SaveChanges();
        }

        public IQueryable<Place> GetPlaces(SortState sortOrder = SortState.SessionAsc)
        {
            var place = _db.Places.Select(v => v);

            switch (sortOrder)
            {
                case SortState.SessionDesc:
                    place = place.OrderByDescending(s => s.Session);
                    break;
                case SortState.PlaceNumberAsc:
                    place = place.OrderBy(s => s.PlaceNumber);
                    break;
                case SortState.PlaceNumberDesc:
                    place = place.OrderByDescending(s => s.PlaceNumber);
                    break;
                default:
                    place = place.OrderBy(s => s.Session);
                    break;
            }
            return place;
        }
    }
}