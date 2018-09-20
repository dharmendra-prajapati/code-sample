using Bicycle.Manager;
using Bicycle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;


namespace Bicycle.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/bicycle")]
    public class BicycleController : ApiController
    {
        private readonly IBicycleManager _bicycleManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bicycleManager"></param>
        public BicycleController(IBicycleManager bicycleManager)
        {
            _bicycleManager = bicycleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        ///// <param name="userId"></param>
        ///// <param name="password"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(PushkarRequestModel model)
        {
            if (string.IsNullOrEmpty(model.userId) && string.IsNullOrEmpty(model.password))
            {
                return Ok(new LoginModel { status = false, message = "Oops, Username or Password missing" });
            }

            var result = _bicycleManager.Login(model.userId,model.password);

            if (result)
            {
                return Ok(new LoginModel
                {
                    status = true,
                    message = "Login Success",
                    token = string.Empty
                });
            }
            else
            {
                return Ok(new LoginModel { status = false, message = "Oops..." });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("stations")]
        [HttpPost]
        public IHttpActionResult Stations()
        {

            var result = _bicycleManager.Stations();

            if (result.Count() > 0)
            {
                return Ok(new StationlistModel
                {
                    status = true,
                    message = "Retrived Success",
                    StationList = result.ToList()
                });
            }
            else
            {
                return Ok(new CommonResponseModel { status = false, message = "Oops..." });
            }
        }

        [Route("assign")]
        [HttpPost]
        public IHttpActionResult AssignCycle(PushkarRequestModel model)
        {
            //ToDo: Code here
            if (string.IsNullOrEmpty(model.userId) && (int)model.cycleNum <= 0)
            {
                return Ok(new CommonResponseModel { status = false, message = "Oops..." });
            }

            var result = _bicycleManager.AssignCycle(model.userId, (int)model.cycleNum);

            if (result)
            {
                return Ok(new CommonResponseModel { status = true, message = "Cycle Assigned Successfully" });
            }
            else
            {
                return Ok(new CommonResponseModel { status = false, message = "Oops..." });
            }

        }

        [Route("unlock")]
        [HttpPost]
        public IHttpActionResult UnlockCycle(PushkarRequestModel model)
        {
            //ToDo: Code here
            if (string.IsNullOrEmpty(model.userId) && model.cycleNum <= 0)
            {
                return Ok(new CommonResponseModel { status = false, message = "Oops..." });
            }

            var result = _bicycleManager.UnlockCycle(model.userId, (int)model.cycleNum);

            if (result)
            {
                return Ok(new CommonResponseModel { status = true, message = "Cycle Unlocked Successfully" });
            }
            else
            {
                return Ok(new CommonResponseModel { status = false, message = "Oops..." });
            }


        }

        [Route("endtrip")]
        [HttpPost]
        public IHttpActionResult EndTrip(PushkarRequestModel model)
        {
            //ToDo: Code here
      
            if (string.IsNullOrEmpty(model.userId) && model.cycleNum <= 0 && model.stationNum <= 0)
            {
                return Ok(new CommonResponseModel { status = false, message = "Oops..." });
            }

            var result = _bicycleManager.EndTrip(model.userId, (int)model.cycleNum, (int)model.stationNum);

            if (result)
            {
                return Ok(new CommonResponseModel { status = true, message = "Cycle Unassigned Successfully" });
            }
            else
            {
                return Ok(new CommonResponseModel { status = false, message = "Oops..." });
            }


        }
    }
}
