using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;

namespace GameStore.DB
{
    public class DBSeeder
    {
        public DBSeeder(GameContext dbcontext)
        {
            //Create Genres
            Genre genre1 = new Genre();
            genre1.Id = "FPS";
            genre1.Name = "First Person Shooter";
            dbcontext.Add(genre1);

            Genre genre2 = new Genre();
            genre2.Id = "RTS";
            genre2.Name = "Real Time Strategy";
            dbcontext.Add(genre2);

            Genre genre3 = new Genre();
            genre3.Id = "MOBA";
            genre3.Name = "Multiplayer Online Battle Arena";
            dbcontext.Add(genre3);

            Genre genre4 = new Genre();
            genre4.Id = "RPG";
            genre4.Name = "Role Playing Game";
            dbcontext.Add(genre4);

            Genre genre5 = new Genre();
            genre5.Id = "MMO";
            genre5.Name = "Massively Multiplayer Online";
            dbcontext.Add(genre5);

            Genre genre6 = new Genre();
            genre6.Id = "MMORPG";
            genre6.Name = "Massively Multiplayer Online Role Playing Games";
            dbcontext.Add(genre6);

            //Create Games
            Game game1 = new Game();
            game1.Id = "P001";
            game1.GenreId = "RTS";
            game1.Title = "BorderLands 3";
            game1.Desc = "The original shooter-looter returns, packing bazillions of guns and a mayhem-fueled adventure! Blast through new worlds & enemies and save your home from the most ruthless cult leaders in the galaxy.";
            game1.Price =81.90m;
            game1.ImageURL = "/Product_Images/P001.jpg";
            game1.TrailerURL = "/Product_Videos/P001.mp4";
            dbcontext.Add(game1);

            Game game2 = new Game();
            game2.Id = "P002";
            game2.GenreId = "RPG";
            game2.Title = "Animal Crossing: New Horizons";
            game2.Desc = "Escape to a deserted island and create your own paradise as you explore, create, and customize in the Animal Crossing: New Horizons game. Your island getaway has a wealth of natural resources that can be used to craft everything from tools to creature comforts. You can hunt down insects at the crack of dawn, decorate your paradise throughout the day, or enjoy sunset on the beach while fishing in the ocean. The time of day and season match real life, so each day on your island is a chance to check in and find new surprises all year round.";
            game2.Price = 29.99m;
            game2.ImageURL = "/Product_Images/P002.jpg";
            game2.TrailerURL = "/Product_Videos/P002.mp4";
            dbcontext.Add(game2);

            Game game3 = new Game();
            game3.Id = "P003";
            game3.GenreId = "MMO";
            game3.Title = "Apex Legends";
            game3.Desc = "Conquer with character in Apex Legends, a free-to-play* Battle Royale shooter where legendary characters with powerful abilities team up to battle for fame & fortune on the fringes of the Frontier.";
            game3.Price = 15.99m;
            game3.ImageURL = "/Product_Images/P003.jpg";
            game3.TrailerURL = "/Product_Videos/P003.mp4";
            dbcontext.Add(game3);

            Game game4 = new Game();
            game4.Id = "P004";
            game4.GenreId = "FPS";
            game4.Title = "Call of Duty: Modern Warfare";
            game4.Desc = "The game takes place in a realistic and modern setting. The campaign follows a CIA officer and British SAS forces as they team up with rebels from the fictional country of Urzikstan, combating together against Russian forces who have invaded the country. The game's Special Ops mode features cooperative play missions that follow up the campaign's story. The multiplayer mode supports cross-platform multiplayer and cross-platform progression for the first time in the series. ";
            game4.Price = 16.00m;
            game4.ImageURL = "/Product_Images/P004.jpg";
            game4.TrailerURL = "/Product_Videos/P004.mp4";
            dbcontext.Add(game4);

            Game game5 = new Game();
            game5.Id = "P005";
            game5.GenreId = "RPG";
            game5.Title = "Control";
            game5.Desc = "After a secretive agency in New York is invaded by an otherworldly threat, you become the new Director struggling to regain Control in this supernatural 3rd person action-adventure from Remedy Entertainment and 505 Games";
            game5.Price = 59.99m;
            game5.ImageURL = "/Product_Images/P005.jpg";
            game5.TrailerURL = "/Product_Videos/P005.mp4";
            dbcontext.Add(game5);

            Game game6 = new Game();
            game6.Id = "P006";
            game6.GenreId = "RPG";
            game6.Title = "Days Gone";
            game6.Desc = "Days Gone is an action-adventure survival horror game set in a post-apocalyptic open world, played from a third-person perspective. The player controls Deacon St. John (Sam Witwer), a former U.S. Army 10th Mountain Division Afghanistan War veteran outlaw-turned-drifter and bounty hunter who prefers life on the road to wilderness encampments.";
            game6.Price = 39.99m;
            game6.ImageURL = "/Product_Images/P006.jpg";
            game6.TrailerURL = "/Product_Videos/P006.mp4";
            dbcontext.Add(game6);

            //Create users 
            User user1 = new User();
            user1.Username = "Anil";
            user1.Password = "Anil123";
            dbcontext.Add(user1);

            User user2 = new User();
            user2.Username = "Mike";
            user2.Password = "Mike123";
            dbcontext.Add(user2);

            User user3 = new User();
            user3.Username = "admin";
            user3.Password = "admin";
            dbcontext.Add(user3);

            dbcontext.SaveChanges();
        }
    }
}
