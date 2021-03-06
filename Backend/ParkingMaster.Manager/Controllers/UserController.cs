﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Manager.Managers;
using System.Web.Http.Cors;
using System.Threading.Tasks;

namespace ParkingMaster.Manager.Controllers
{

    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/user/deleteallapps")]
        public async Task<IHttpActionResult> DeleteFromAll([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();
            UserManagementManager userManager = new UserManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                var deleteResponse = await userManager.DeleteUserFromApps(Guid.Parse(response.Data.Id));
                if (deleteResponse.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                    return Content(statusResponse.Data, statusResponse.Error);
                }
            }

            return Content((HttpStatusCode)404, response.Error);
        }

        [HttpPost]
        [Route("api/user/delete")]
        public IHttpActionResult Delete([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            if (request == null)
            {
                return Content((HttpStatusCode)400, "Request is null.");
            }

            UserManagementManager userManager = new UserManagementManager();
            ResponseDTO<bool> managerResponse = userManager.DeleteUser(request);

            if (managerResponse.Data)
            {
                return Ok();
            }
            else
            {
                ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(managerResponse.Error);
                return Content(statusResponse.Data, statusResponse.Error);
            }
        }

        [HttpPost]
        [Route("api/user/session")]
        public IHttpActionResult Session([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                return Ok(response.Data);
            }
            else
            {
                ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                return Content(statusResponse.Data, statusResponse.Error);
            }
        }

        [HttpPost]
        [Route("api/user/setrole")]
        public IHttpActionResult SetRole([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            UserManagementManager _userManager = new UserManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = _userManager.SetRole(request);

            if (response.Data != null)
            {
                return Ok(response.Data);
            }
            else
            {
                ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                return Content(statusResponse.Data, statusResponse.Error);
            }
        }

        [HttpPost]
        [Route("api/user/logout")]
        public IHttpActionResult Logout([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            UserManagementManager _userManager = new UserManagementManager();

            ResponseDTO<bool> response = _userManager.LogoutUser(request);

            if (response.Data)
            {
                return Ok(response.Data);
            }
            else
            {
                ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                return Content(statusResponse.Data, statusResponse.Error);
            }
        }
    }
}