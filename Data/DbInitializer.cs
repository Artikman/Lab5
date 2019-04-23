using Lab_4.Models;
using System;
using System.Linq;

namespace Lab_4.Data
{
    class DbInitializer
    {
        public static void Initialize(CinemaContext db)
        {
            db.Database.EnsureCreated();

            if (db.Genres.Any())
            {
                return;
            }

            int place_number = 35;
            int session_number = 35;
            int genre_number = 35;
            int film_number = 35;
            string name;
            string description;
            string genre;
            string filmCompany;
            string producingCountry;
            string listOfMainActors;
            int ageRestrictions;
            float ticketPrice;
            string employessInvolvedTheSession;
            string session;
            int placeNumber;
            bool employment;

            Random randObj = new Random(1);

            string[] place_voc = { "25 место", "65 место", "100 место", "50 место" };
            int count_place_voc = place_voc.GetLength(0);
            for (int placeID = 1; placeID <= place_number; placeID++)
            {
                session = place_voc[randObj.Next(count_place_voc)] + placeID.ToString();
                placeNumber = randObj.Next();
                employment = randObj.Next(2) == 0 ? false : true;
                db.Places.Add(new Place { Session = session, PlaceNumber = placeNumber, Employment = employment });
            }
            db.SaveChanges();

            string[] session_voc = { "дневной", "вечерний", "утренний", "вечерний" };
            int count_session_voc = session_voc.GetLength(0);
            for (int sessionID = 1; sessionID <= session_number; sessionID++)
            {
                DateTime date = DateTime.Now.Date;
                DateTime dateTime = date.AddDays(-sessionID);
                DateTime timeStarted = DateTime.Now.Date;
                DateTime dateTime1 = timeStarted.AddMinutes(-sessionID);
                DateTime endTime = DateTime.Now.Date;
                DateTime dateTime2 = endTime.AddMinutes(-sessionID);
                ticketPrice = (float)randObj.NextDouble();
                employessInvolvedTheSession = session_voc[randObj.Next(count_session_voc)] + sessionID.ToString();
                int placeID = randObj.Next(1, place_number - 1);
                db.Sessions.Add(new Session { Date = dateTime, TimeStarted = dateTime1, EndTime = dateTime2, TicketPrice = ticketPrice, EmployessInvolvedInTheSession = employessInvolvedTheSession, PlaceId = placeID });
            }
            db.SaveChanges();

            string[] genre_voc = { "Боевик", "Комедия", "Мелодрама", "Фантастика" };
            int count_genre_voc = genre_voc.GetLength(0);
            for (int genreID = 1; genreID <= genre_number; genreID++)
            {
                name = genre_voc[randObj.Next(count_genre_voc)] + genreID.ToString();
                description = genre_voc[randObj.Next(count_genre_voc)] + genreID.ToString();
                db.Genres.Add(new Genre { Name = name, Description = description });
            }
            db.SaveChanges();

            string[] film_voc = { "Терминатор 2", "СуперНянь", "Лаллита", "Гарри Поттер" };
            int count_film_voc = film_voc.GetLength(0);
            for (int filmID = 1; filmID <= film_number; filmID++)
            {
                name = film_voc[randObj.Next(count_film_voc)] + filmID.ToString();
                genre = film_voc[randObj.Next(count_film_voc)] + filmID.ToString();
                DateTime duration = DateTime.Now.Date;
                DateTime dateTime = duration.AddMinutes(-filmID);
                filmCompany = film_voc[randObj.Next(count_film_voc)] + filmID.ToString();
                producingCountry = film_voc[randObj.Next(count_film_voc)] + filmID.ToString();
                listOfMainActors = film_voc[randObj.Next(count_film_voc)] + filmID.ToString();
                ageRestrictions = randObj.Next(30) + 10;
                description = film_voc[randObj.Next(count_film_voc)] + filmID.ToString();
                int genreID = randObj.Next(1, film_number - 1);
                int sessionID = randObj.Next(1, film_number - 1);
            }
            db.SaveChanges();
        }
    }
}