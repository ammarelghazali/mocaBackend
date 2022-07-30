using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.WifiDtos.Request;
using MOCA.Core.DTOs.MocaSettings.WifiDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class WifisService : IWifisService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WifisService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<WifiDto>> AddWifi(long? lobSpaceTypeId, WifiForCreationDto wifiForCreation)
        {
            //if (lobSpaceTypeId is not null)
            //{
            //    if (!await _unitOfWork.LobSpaceTypes.LobSpaceTypeExists((long)lobSpaceTypeId))
            //    {
            //        return new ResponseDto
            //        {
            //            StatusCode = 400,
            //            Message = "There's no such Lob Space"
            //        };
            //    }
            //}

            var wifi = await _unitOfWork.Wifis.GetWifiByLobSpaceTypeId(lobSpaceTypeId);
            var newWifi = new Wifi();


            if (wifi != null)
            {
                newWifi = _mapper.Map<Wifi>(wifi);

                _unitOfWork.Wifis.Delete(wifi);
            }
            else
            {
                newWifi.LobSpaceTypeId = lobSpaceTypeId;
            }

            newWifi.Description = wifiForCreation.Description;

            _unitOfWork.Wifis.Insert(newWifi);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<WifiDto>
                {
                    Message = "Server Cannot Save Resource Right now",
                };
            }

            return new Response<WifiDto>
            {
                Message = "Wifi Added Successfully",
                Data = _mapper.Map<WifiDto>(newWifi)
            };
        }

        public async Task<Response<bool>> DeleteWifi(long? lobSpaceTypeId)
        {
            //if (lobSpaceTypeId is not null)
            //{
            //    if (!await _unitOfWork.LobSpaceTypes.LobSpaceTypeExists((long)lobSpaceTypeId))
            //    {
            //        return new ResponseDto
            //        {
            //            StatusCode = 400,
            //            Message = "There's no such Lob Space"
            //        };
            //    }
            //}

            var wifi = await _unitOfWork.Wifis.GetWifiByLobSpaceTypeId(lobSpaceTypeId);

            if (wifi == null)
            {
                return new Response<bool>
                {
                    Message = "There's no Wifi Yet"
                };
            }

            _unitOfWork.Wifis.Delete(wifi);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Cannot Save Resource Right now",
                };
            }

            return new Response<bool>
            {
                Data = true,
                Message = "Wifi Deleted Successfully"
            };
        }

        public async Task<Response<WifiDto>> GetWifi(long? lobSpaceTypeId)
        {
            //if (lobSpaceTypeId is not null)
            //{
            //    if (!await _unitOfWork.LobSpaceTypes.LobSpaceTypeExists((long)lobSpaceTypeId))
            //    {
            //        return new ResponseDto
            //        {
            //            StatusCode = 400,
            //            Message = "There's no such Lob Space"
            //        };
            //    }
            //}

            var wifi = await _unitOfWork.Wifis.GetWifiByLobSpaceTypeId(lobSpaceTypeId);

            return new Response<WifiDto>
            {
                Data = _mapper.Map<WifiDto>(wifi)
            };
        }

        public Task<Response<WifiDto>> UpdateWifi(long? lobSpaceTypeId, WifiForCreationDto wifiForCreation)
        {
            return AddWifi(lobSpaceTypeId, wifiForCreation);
        }
    }
}
