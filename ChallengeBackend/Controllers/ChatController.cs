using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ChallengeBackend.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            ChallengeBackend.Models.Helpers.DBContextHelper.Instance.JoinRoom( User.Identity.GetUserId<int>(), 1 );
            return View();
        }
    }
}