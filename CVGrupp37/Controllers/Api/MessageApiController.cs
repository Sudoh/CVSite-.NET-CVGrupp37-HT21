using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using Shared;
using System;
using System.Linq;
using System.Web.Http;

namespace CVGrupp37.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageApiController : ApiController
    {


        [Route("send")]
        [HttpPost]
        public IHttpActionResult SendMessage(CreateMessagesViewModel model)
        {
            try
            {

                using (var context = new ApplicationDbContext())
                {

                    var newMessage = new Messages()
                    {
                        MessageText = model.Message,
                        IsRead = false,
                        DateSent = DateTime.Now,
                        //FromUser = model.From,
                        //ToUserID = model.To.ToString()
                    };


                    //Matchar CvId mot id i databasen
                    var toUserIdfromCV = context.Users
                        .Where(x => x.CVid.ToString() == model.To)
                        .ToList();

                    //Hämtar mottagarens Id
                    newMessage.ToUserID = toUserIdfromCV[0].Id.ToString();

                    string onlineUser = User.Identity.GetUserName();
                    if (!string.Equals(onlineUser, model.From))
                    {
                        newMessage.FromUser = model.From;
                    }
                    else
                    {
                        var FromUser = context.Users
                       .Where(x => x.UserName.ToString() == onlineUser)
                       .ToList();
                        newMessage.FromUser = FromUser[0].Namn;

                    }



                    context.Messages.Add(newMessage);
                    context.SaveChanges();


                }

                return Ok();


            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("setmessagestatus/{id}")]
        [HttpGet]
        public IHttpActionResult ReadMessage(int id)
        {
            string onlineUser = User.Identity.GetUserId();
            if (onlineUser == null)
            {
                onlineUser = "";
            }

            using (var context = new ApplicationDbContext())
            {
                var meddelande = context.Messages
                    .Where(x => x.Id == id)
                    .ToList();

                if (meddelande[0].IsRead)
                {
                    meddelande[0].IsRead = false;
                }
                else
                {
                    meddelande[0].IsRead = true;
                }

                context.SaveChanges();
                return Ok();
            }




        }


        [Route("delete/{id}")]
        [HttpGet]
        public IHttpActionResult DeleteMessage(int id)
        {

            string onlineUser = User.Identity.GetUserId();
            if (onlineUser == null)
            {
                onlineUser = "";
            }


            using (var context = new ApplicationDbContext())
            {
                var ToUser = (from message in context.Messages
                              where message.Id == id
                              select message.ToUserID).ToList();

                if (ToUser == null)
                {
                    ToUser.Clear();
                }

                //userchecker
                if (!string.IsNullOrEmpty(onlineUser) || onlineUser.Equals(ToUser[0]))
                {

                    var mess = context.Messages.FirstOrDefault(x => x.Id == id);
                    if (mess == null)
                    {
                        return BadRequest();
                    }

                    context.Messages.Remove(mess);
                    context.SaveChanges();
                    return Ok();
                }

                return Ok();
            }
        }

    }
}