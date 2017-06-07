using Gwig.Web.Domain;
using Gwig.Web.Models.Requests.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gwig.Web.Services.Interface
{
    public interface IMissionsService
    {
        int Insert(string UserId, Mission model);
        EditMission Get(int id);
        List<Mission> GetList();
        List<Mission> GetListWithUserId(string userId);
        int Update(string UserId, UpdateMissionRequest model);
        int UpdateMissionMedia(UpdateMissionMediaRequest model);
        int Delete(int id);
    }
}