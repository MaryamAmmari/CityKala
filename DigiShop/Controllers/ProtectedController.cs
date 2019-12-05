using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiShop.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigiShop.Controllers
{
    public class ProtectedController : Controller
    {
        [Authorize (Roles = RoleConstants.Admin)]
        public IActionResult AdminArea()
        {
            return View("Index");
        }

        [Authorize (Roles = RoleConstants.User)]
        public IActionResult UserArea()
        {
            return View("Index");
        }
    }
}