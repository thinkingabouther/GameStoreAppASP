using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Web;

namespace GameStoreAppCF.Models
{
    public class DBInitializer
    {
        public static void InitializeDB()
        {
            using (var db = new GameStoreDB())
            {
                var genreCard = new Genre
                {
                    Name = "Карточная игра",
                    Description = "Игроки используют карточки"
                };
                var genreRole = new Genre
                {
                    Name = "Ролевая игра",
                    Description = "Игроки исполняют роль персонажа"
                };
                var manufacturerHB = new Manufacturer
                {
                    Name = "Hobby World",
                    Adress = "Москва"
                };
                var authorSomeone = new Author
                {
                    Name = "Неизвестный автор",
                    Adress = "Неизвестно"
                };
                var typeCompetetive = new Type
                {
                    Name = "Соревновательная игра",
                    Description = "Игроки соревнуются друг с другом ради победы"
                };
                var typeCooperative = new Type
                {
                    Name = "Кооперативная игра",
                    Description = "Игроки объединяются для достижения общей цели"
                };
                var clientIvan = new Client
                {
                    Name = "Иванов Иван Иванович",
                    Email = "test@m.r"
                };
                var clientPetr = new Client
                {
                    Name = "Петров Петр Петрович",
                    Email = "Teest1@m.r"
                };
                var order1 = new Order
                {
                    Date = DateTime.Now
                };
                var order2 = new Order
                {
                    Date = DateTime.Now
                };
                var imageConverter = new ImageConverter();
                var game1 = new Game
                {
                    Description = "Дикий запад!",
                    Name = "Бэнг!",
                    Manufacturer = manufacturerHB,
                    Author = authorSomeone,
                    Genre = genreCard,
                    Difficulty = 1,
                    Type = typeCompetetive,
                    Min_Duration = 15,
                    Max_Duration = 60,
                    Min_Players = 2,
                    Max_Players = 10,
                    Quantity = 5,
                    Price = 990.99,
                    //Image = (byte[])imageConverter.ConvertTo(.resources.Bang, typeof(byte[]))
            };
                var game2 = new Game
                {
                    Description = "Война гномов!",
                    Name = "Манчкин",
                    Manufacturer = manufacturerHB,
                    Author = authorSomeone,
                    Genre = genreRole,
                    Difficulty = 2,
                    Type = typeCooperative,
                    Min_Duration = 5,
                    Max_Duration = 180,
                    Min_Players = 3,
                    Max_Players = 10,
                    Quantity = 10,
                    Price = 1990.99,
                    //Image = (byte[])imageConverter.ConvertTo(GameStoreAppCF.Properties.resources.Munchkin, typeof(byte[]))
                };
                order1.Client = clientIvan;
                order1.Game.Add(game1);
                order2.Client = clientPetr;
                order2.Game.Add(game2);

                db.Game.Add(game1);
                db.Game.Add(game2);
                db.Genre.Add(genreCard);
                db.Genre.Add(genreRole);
                db.Author.Add(authorSomeone);
                db.Manufacturer.Add(manufacturerHB);
                db.Type.Add(typeCompetetive);
                db.Type.Add(typeCooperative);
                db.Client.Add(clientIvan);
                db.Client.Add(clientPetr);
                db.Order.Add(order1);
                db.Order.Add(order2);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
        }

        public static void RemoveAllEntites()
        {
            using (var db = new GameStoreDB())
            {
                foreach (var entity in db.Order)
                {
                    db.Order.Remove(entity);
                }
                foreach (var entity in db.Genre)
                {
                    db.Genre.Remove(entity);
                }
                foreach (var entity in db.Type)
                {
                    db.Type.Remove(entity);
                }
                foreach (var entity in db.Manufacturer)
                {
                    db.Manufacturer.Remove(entity);
                }
                foreach (var entity in db.Author)
                {
                    db.Author.Remove(entity);
                }
                foreach (var entity in db.Client)
                {
                    db.Client.Remove(entity);
                }
                foreach (var entity in db.Review)
                {
                    db.Review.Remove(entity);
                }
                foreach (var entity in db.Game)
                {
                    db.Game.Remove(entity);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
        }
    }
}