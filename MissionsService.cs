using Gwig.Data;
using Gwig.Web.Domain;
using Gwig.Web.Models.Requests;
using Gwig.Web.Models.Requests.Events;
using Gwig.Web.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Gwig.Web.Services
{
    public class MissionsService : BaseService, IMissionsService
    {
        public int Insert(string UserId, Mission model)
        {
            int uid = 0;//000-0000-0000-0000

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Missions_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    SqlParameter p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                    paramCollection.AddWithValue("@UserId", UserId);
                    paramCollection.AddWithValue("@Title", model.Title);
                    paramCollection.AddWithValue("@Description", model.Description);
                    paramCollection.AddWithValue("@MediaId", model.MediaId);
                    paramCollection.AddWithValue("@PointScore", model.PointScore);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@id"].Value.ToString(), out uid);
                }
                );

            return uid;
        }

        public EditMission Get(int id)
        {
            EditMission retrieved_mission = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Missions_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@id", id);
                }
                , map: delegate (IDataReader reader, short set)
                {
                    retrieved_mission = new EditMission();
                    int startingIndex = 0; //startingOrdinal

                    retrieved_mission.Id = reader.GetSafeInt32(startingIndex++);
                    retrieved_mission.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    retrieved_mission.DateModified = reader.GetSafeDateTime(startingIndex++);
                    retrieved_mission.UserId = reader.GetSafeString(startingIndex++);
                    retrieved_mission.Title = reader.GetSafeString(startingIndex++);
                    retrieved_mission.Description = reader.GetSafeString(startingIndex++);
                    retrieved_mission.MediaId = reader.GetSafeInt32(startingIndex++);
                    retrieved_mission.PointScore = reader.GetSafeInt32(startingIndex++);
                    retrieved_mission.Url = reader.GetSafeString(startingIndex++);
                    retrieved_mission.PlacesCount = reader.GetSafeInt32(startingIndex++);

                }
                );

            return retrieved_mission;
        }

        public List<Mission> GetList()
        {
            List<Mission> newMissionList = new List<Mission>();

            DataProvider.ExecuteCmd(GetConnection, "dbo.Missions_SelectAll"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                }
                , map: delegate (IDataReader reader, short set)
                {
                    Mission newMission = new Mission();
                    int startingIndex = 0; //startingOrdinal

                    newMission.Id = reader.GetSafeInt32(startingIndex++);
                    newMission.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    newMission.DateModified = reader.GetSafeDateTime(startingIndex++);
                    newMission.UserId = reader.GetSafeString(startingIndex++);
                    newMission.Title = reader.GetSafeString(startingIndex++);
                    newMission.Description = reader.GetSafeString(startingIndex++);
                    newMission.MediaId = reader.GetSafeInt32(startingIndex++);
                    newMission.PointScore = reader.GetSafeInt32(startingIndex++);
                    newMission.Url = reader.GetSafeString(startingIndex++);

                    newMissionList.Add(newMission);

                    }
                    );

            return newMissionList;
        }

        public List<Mission> GetListWithUserId(string userId)
        {
            List<Mission> newMissionList = new List<Mission>();

            DataProvider.ExecuteCmd(GetConnection, "dbo.Missions_SelectAllByUserId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", userId);
                }
                , map: delegate (IDataReader reader, short set)
                {
                    Mission newMission = new Mission();
                    int startingIndex = 0; //startingOrdinal

                    newMission.Id = reader.GetSafeInt32(startingIndex++);
                    newMission.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    newMission.DateModified = reader.GetSafeDateTime(startingIndex++);
                    newMission.UserId = reader.GetSafeString(startingIndex++);
                    newMission.Title = reader.GetSafeString(startingIndex++);
                    newMission.Description = reader.GetSafeString(startingIndex++);
                    newMission.MediaId = reader.GetSafeInt32(startingIndex++);
                    newMission.PointScore = reader.GetSafeInt32(startingIndex++);
                    newMission.Url = reader.GetSafeString(startingIndex++);
                    newMission.PlacesCount = reader.GetSafeInt32(startingIndex++);

                    newMissionList.Add(newMission);
                
                }
                );

            return newMissionList;
        }

        public int Update(string UserId, UpdateMissionRequest model)
        {
            int uid = 0;//000-0000-0000-0000

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Missions_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                paramCollection.AddWithValue("@UserId", UserId);
                paramCollection.AddWithValue("@Id", model.Id);
                paramCollection.AddWithValue("@Title", model.Title);
                paramCollection.AddWithValue("@Description", model.Description);
                paramCollection.AddWithValue("@MediaId", model.MediaId);
                paramCollection.AddWithValue("@PointScore", model.PointScore);
                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out uid);
                }
                );

            return uid;
        }

        public int UpdateMissionMedia(UpdateMissionMediaRequest model)
        {
            int uid = 0;//000-0000-0000-0000

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Missions_UpdateMediaId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@MediaId", model.MediaId);
                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out uid);
                }
                );

            return uid;
        }

        public int Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Missions_Delete"
            , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
            }

            );
            
            return id;
        }
    }
}