using Ks_Model;
using Ks_Model.Requst;
using Ks_Model.Requst.Reservation;
using Ks_Model.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WS.SDK
{
    public class WxAPI
    {
      
        //HotelService.ReservationServiceClient test = new HotelService.ReservationServiceClient();
        private YHWxService.ReservationService service = null;
        public WxAPI(string hotelCode)
        {
            if (hotelCode == "YWSJMYHJD")
            {
                service = new YHWxService.ReservationService();
            }
 
        }

        public  RateQueryResponse RateQuery(string transcode, string reqtime, string systype, string username, string password, string openid,
            string ratePlanReservationActionType, string ratePlanCode, string timeUnitType, string startTime, string numberOfTimeUnits,
            string rateReservationActionType, string rateBasisTimeUnitType)
        {
            var obj = new RateQueryRequest()
            {
                Head = new Head()
                {
                    transcode = transcode,
                    reqtime = reqtime,
                    systype = systype,
                    username = username,
                    password = password,
                    openid = openid,
                },
                Body = new RateQueryBody()
                {
                    RatePlans = new RatePlans()
                    {
                        RatePlan = new RatePlan()
                        {
                            reservationActionType = ratePlanReservationActionType,
                            ratePlanCode = ratePlanCode,
                            TimeSpan = new Ks_Model.Requst.TimeSpan()
                            {
                                timeUnitType = timeUnitType,
                                startTime = startTime,
                                numberOfTimeUnits = numberOfTimeUnits,
                            },
                            Rates = new Ks_Model.Requst.Rates()
                            {
                                Rate = new Ks_Model.Requst.Rate()
                                {
                                    reservationActionType = rateReservationActionType,
                                    rateBasisTimeUnitType = rateBasisTimeUnitType,
                                }
                            }
                        }
                    }
                },
            };
            string xmlObj = Common.SerializeToXml(obj, typeof(RateQueryRequest));
            var res = service.RateQuery(xmlObj);
            RateQueryResponse result = Common.Deserialize(typeof(RateQueryResponse), res) as RateQueryResponse;
            return result;
        }

        public  GetRmRateResponse GetRmRate(string transcode, string reqtime, string systype, string username, string password, string openid,
            string stay, string type, string day, string ratecode)
        {
            var obj = new GetRmRateRequest()
            {
                Head = new Head()
                {
                    transcode = transcode,
                    reqtime = reqtime,
                    systype = systype,
                    username = username,
                    password = password,
                    openid = openid,
                },
                Body = new GetRmRateRequrstBody()
                {
                    RoomRate = new RoomRate()
                    {
                        stay = stay,
                        type = type,
                        day = day,
                        ratecode = ratecode,
                    }
                }
            };
            string xmlObj = Common.SerializeToXml(obj, typeof(GetRmRateRequest));
            //var res = service.GetRmrate(xmlObj);
            GetRmRateResponse result = Common.Deserialize(typeof(GetRmRateResponse), xmlObj) as GetRmRateResponse;
            return result;
        }

        public  ReservationRespone Reservation(string transcode, string reqtime, string systype, string username, string password, string openid,
            string ReservationMfShareAction, string ReservationMfReservationAction, string ReservationReservationOriginatorCode, string protocol,
            string ResCommentReservationActionType, string comment, string profileType, string gender, string nameFirst, string nameSur, string primaryLanguageID,
            string RoomStayMfShareAction, string RoomStaymfReservationAction, string RoomStayReservationActionType, string RoomStayReservationStatusType,
            string RoomStayRoomStayRPH, string RoomStayRoomInventoryCode, string RoomStayRoomNo, string timeUnitType, string startTime, string numberOfTimeUnits,
            string arrtime, string ageQualifyingCode, string mfCount, string RatePlanReservationActionType, string ratePlanCode, string mfsourceCode, string mfMarketCode,
            string numRooms, string mfchannelCode, string resCommentRPHs, string resProfileRPHs, string mfsaleid)
        {
            var obj = new ReservationRequest()
            {
                Head = new Head()
                {
                    transcode = transcode,
                    reqtime = reqtime,
                    systype = systype,
                    username = username,
                    password = password,
                    openid = openid,
                },
                Body = new ReservationRequestBody()
                {
                    Reservation = new Ks_Model.Requst.Reservation.Reservation()
                    {
                        mfShareAction = ReservationMfShareAction,
                        mfReservationAction = ReservationMfReservationAction,
                        reservationOriginatorCode = ReservationReservationOriginatorCode,
                        protocol = protocol,
                        ResComments = new ResComments()
                        {
                            ResComment = new ResComment()
                            {
                                reservationActionType = ResCommentReservationActionType,
                                comment = comment,
                            }
                        },
                        ResProfiles = new ResProfiles()
                        {
                            ResProfile = new ResProfile()
                            {
                                Profile = new Profile()
                                {
                                    profileType = profileType,
                                    gender = gender,
                                    IndividualName = new IndividualName()
                                    {
                                        nameFirst = nameFirst,
                                        nameSur = nameSur,
                                    },
                                    primaryLanguageID = primaryLanguageID,
                                }
                            }
                        },
                        RoomStays = new RoomStays()
                        {
                            RoomStay = new RoomStay()
                            {
                                mfShareAction = RoomStayMfShareAction,
                                mfReservationAction = RoomStaymfReservationAction,
                                reservationActionType = RoomStayReservationActionType,
                                reservationStatusType = RoomStayReservationStatusType,
                                roomStayRPH = RoomStayRoomStayRPH,
                                roomInventoryCode = RoomStayRoomInventoryCode,
                                roomNo = RoomStayRoomNo,
                                TimeSpan = new Ks_Model.Requst.Reservation.TimeSpan()
                                {
                                    timeUnitType = timeUnitType,
                                    startTime = startTime,
                                    numberOfTimeUnits = numberOfTimeUnits,
                                    arrtime = arrtime,
                                },
                                GuestCounts = new GuestCounts()
                                {
                                    GuestCount = new List<GuestCount>(){
                                        new GuestCount(){
                                        ageQualifyingCode = ageQualifyingCode,
                                        mfCount = mfCount,
                                        },
                                        new GuestCount(){
                                        ageQualifyingCode = "CHILD",
                                        mfCount = "0",
                                        }
                                    }
                                },
                                RatePlans = new RatePlans_ReservationRequest()
                                {
                                    RatePlan = new RatePlan_ReservationRequest()
                                    {
                                        reservationActionType = RatePlanReservationActionType,
                                        ratePlanCode = ratePlanCode,
                                        mfsourceCode = mfsourceCode,
                                        mfMarketCode = mfMarketCode,
                                        numRooms = numRooms,
                                    }
                                },
                                mfchannelCode = mfchannelCode,
                            }
                        },
                        resCommentRPHs = resCommentRPHs,
                        resProfileRPHs = resProfileRPHs,
                        mfsaleid = mfsaleid,
                    }
                }
            };
            try
            {
                string xmlObj = Common.SerializeToXml(obj, typeof(ReservationRequest));
                var res = service.Reservation(xmlObj);
                ReservationRespone result = Common.Deserialize(typeof(ReservationRespone), res) as ReservationRespone;
                return result;
            }
            catch (Exception)
            {

                return null;
            }
          
        }

        public  RespReservationDetail GetReservationDetail(string transcode, string reqtime, string systype, string username, string password, string openid,
            string confirmationID, string cardno, string mobile)
        {
            var obj = new ReqGetReservationDetail()
            {
                Head = new Head()
                {
                    transcode = transcode,
                    reqtime = reqtime,
                    systype = systype,
                    username = username,
                    password = password,
                    openid = openid,
                },
                Body = new ReqGetReservationDetail_Body()
                {
                    ReservationDetail = new ReservationDetail()
                    {
                        confirmationID = confirmationID,
                        cardno = cardno,
                        mobile = mobile,
                    }
                }
            };
            string xmlObj = Common.SerializeToXml(obj, typeof(ReqGetReservationDetail));
            var res = service.GetReservationDetail(xmlObj);
            RespReservationDetail result = Common.Deserialize(typeof(RespReservationDetail), res) as RespReservationDetail;
            return null;
        }

        public  RespResCancel ResCancel(string transcode, string reqtime, string systype, string username, string password, string openid,
           string confirmationID)
        {
            var obj = new ReqResCancel()
            {
                Head = new Head()
                {
                    transcode = transcode,
                    reqtime = reqtime,
                    systype = systype,
                    username = username,
                    password = password,
                    openid = openid,
                },
                Body = new ReqResCancel_Body()
                {
                    ReservationCancel = new ReservationCancel()
                    {
                        confirmationID = confirmationID,
                    }
                }
            };

            string xmlObj = Common.SerializeToXml(obj, typeof(ReqResCancel));
            var res = service.ResCancel(xmlObj);
            RespResCancel result = Common.Deserialize(typeof(RespResCancel), res) as RespResCancel;
            return result;
        }

        public  RespSearchRoomType SearchRoomType(string transcode, string reqtime, string systype, string username, string password, string openid)
        {
            var obj = new ReqSearchRoomType()
            {
                Head = new Head()
                {
                    transcode = transcode,
                    reqtime = reqtime,
                    systype = systype,
                    username = username,
                    password = password,
                    openid = openid,
                },
                Body = new ReqSearchRoomType_Body()
                {

                }
            };
            string xmlObj = Common.SerializeToXml(obj, typeof(ReqSearchRoomType));
            var res = service.SearchRoomType(xmlObj);

            RespSearchRoomType result = Common.Deserialize(typeof(RespSearchRoomType), res) as RespSearchRoomType;
            return result;
        }
    }
}
