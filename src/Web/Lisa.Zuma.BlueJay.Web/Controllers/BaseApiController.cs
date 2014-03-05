﻿using Lisa.Zuma.BlueJay.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        protected BlueJayContext Db;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Db = new BlueJayContext("DefaultConnection");

            base.Initialize(controllerContext);
        }

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }
    }
}