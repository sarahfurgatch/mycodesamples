using Microsoft.Practices.Unity;
using Gwig.Data;
using Gwig.Web.Domain;
using Gwig.Web.Models.Requests;
using Gwig.Web.Models.Requests.Events;
using Gwig.Web.Models.Responses;
using Gwig.Web.Services;
using Gwig.Web.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gwig.Web.Controllers.Api
{

    [RoutePrefix("api/missions")]
    public class MissionsApiController : ApiController
    {
        [Dependency]
        public IMissionsService _MissionsService { get; set; }

        //POST
        [Route(), HttpPost]
        public HttpResponseMessage Insert(Mission Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string UserId = UserService.GetCurrentUserId();

            int id = _MissionsService.Insert(UserId, Model);

            ItemResponse<int> Response = new ItemResponse<int>();
            Response.Item = id;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        //GET by Id
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            EditMission retrieved_mission = _MissionsService.Get(id);

            ItemResponse<EditMission> Response = new ItemResponse<EditMission>();
            Response.Item = retrieved_mission;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        //GET all
        [Route, HttpGet]
        public HttpResponseMessage GetListOfMissions()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<Mission> retrieved_mission_list = _MissionsService.GetList();

            ItemsResponse<Mission> Response = new ItemsResponse<Mission>();
            Response.Items = retrieved_mission_list;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        //GET by userId
        [Authorize]
        [Route("byuserid"), HttpGet]
        public HttpResponseMessage GetListOfMissionsByUserId()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string userId = UserService.GetCurrentUserId();
            
            List<Mission> retrieved_mission_list = _MissionsService.GetListWithUserId(userId);

            ItemsResponse<Mission> Response = new ItemsResponse<Mission>();
            Response.Items = retrieved_mission_list;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        //PUT - update event
        [Route, HttpPut]
        public HttpResponseMessage UpdateEventById(UpdateMissionRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string userId = UserService.GetCurrentUserId();

            int updated_mission_id = _MissionsService.Update(userId, model);

            ItemResponse<bool> Response = new ItemResponse<bool>();
            Response.Item = true;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        //PUT - update event media
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateEventMediaById(UpdateMissionMediaRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            int updated_media_mission_id = _MissionsService.UpdateMissionMedia(model);

            ItemResponse<bool> Response = new ItemResponse<bool>();
            Response.Item = true;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        //DELETE
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            int updated_mission_id = _MissionsService.Delete(id);

            ItemResponse<int> Response = new ItemResponse<int>();
            Response.Item = id;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }
    }
}