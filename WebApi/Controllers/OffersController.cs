using DataLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class OffersController : ApiController
    {
        private readonly OfferContext db = new OfferContext();

        public IEnumerable<OfferDto> Get()
        {
            using (db)
            {
                var oldest = DateTime.Now.AddHours(-4);
                var offers = from o in db.Offers
                             select new OfferDto
                             {
                                 Description = o.Description,
                                 Id = o.Id,
                                 PhotoUrl = o.PhotoUrl,
                                 PublishDate = o.PublishDate,
                                 Title = o.Title,
                                 AuthorId = o.AuthorId,
                                 Price = o.Price,
                                 Town = o.Town,
                                 Author = new UserDto
                                 {
                                     Email = o.Author.Email,
                                     FirstName = o.Author.FirstName,
                                     LastName = o.Author.LastName,
                                     PhoneNumber = o.Author.PhoneNumber,
                                 }
                             };
                return offers.Where(o => o.PublishDate.CompareTo(oldest) > 0).ToList();
            }
        }

        public OfferDto Get(int id)
        {
            using (db)
            {
                var offers = from o in db.Offers
                             select new OfferDto
                             {
                                 Description = o.Description,
                                 Id = o.Id,
                                 PhotoUrl = o.PhotoUrl,
                                 PublishDate = o.PublishDate,
                                 Title = o.Title,
                                 AuthorId = o.AuthorId,
                                 Price = o.Price,
                                 Town = o.Town,
                                 Author = new UserDto
                                 {
                                     Email = o.Author.Email,
                                     FirstName = o.Author.FirstName,
                                     LastName = o.Author.LastName,
                                     PhoneNumber = o.Author.PhoneNumber,
                                 }
                             };
                return offers.FirstOrDefault(o => o.Id == id);
            }
        }

        public IEnumerable<OfferDto> Get(string accessToken)
        {
            using (db)
            {
                var offers = from o in db.Offers
                             where o.Author.AccessToken == accessToken
                             select new OfferDto
                             {
                                 Description = o.Description,
                                 Id = o.Id,
                                 PhotoUrl = o.PhotoUrl,
                                 PublishDate = o.PublishDate,
                                 Title = o.Title,
                                 AuthorId = o.AuthorId,
                                 Price = o.Price,
                                 Town = o.Town,
                                 Author = new UserDto
                                 {
                                     Email = o.Author.Email,
                                     FirstName = o.Author.FirstName,
                                     LastName = o.Author.LastName,
                                     PhoneNumber = o.Author.PhoneNumber,
                                 }
                             };
                return offers.ToList();
            }
        }

        public void Post(string accessToken, [FromBody]Offer offer)
        {
            using (db)
            {
                var user = db.Users.FirstOrDefault(u => u.AccessToken == accessToken);
                offer.Author = user;
                offer.PublishDate = DateTime.Now;
                db.Offers.Add(offer);
                offer.PhotoUrl = Dropbox.Upload(Guid.NewGuid().ToString(), offer.PhotoUrl);
                db.SaveChanges();
            }
        }

        public void Put(string accessToken, [FromBody]Offer offer)
        {
            using (db)
            {
                var offerToChange = db.Offers.FirstOrDefault(o => o.Id == offer.Id);
                if (offerToChange != null)
                {
                    if (offerToChange.Author.AccessToken == accessToken)
                    {
                        offerToChange.Description = offer.Description;
                        offerToChange.Title = offer.Title;
                        offerToChange.PhotoUrl = offer.PhotoUrl;
                    }
                }

                db.SaveChanges();
            }
        }

        public void Delete(string accessToken, int id)
        {
            using (db)
            {
                var offerToDelete = db.Offers.FirstOrDefault(o => o.Id == id);
                if (offerToDelete != null)
                {
                    if (offerToDelete.Author.AccessToken == accessToken)
                    {
                        db.Offers.Remove(offerToDelete);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}