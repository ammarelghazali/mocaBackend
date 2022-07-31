using AutoMapper;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class LobSpaceTypesService /*: ILobSpaceTypesService*/
    {
        //private readonly IMapper _mapper;
        //private readonly IUnitOfWork _unitOfWork;
        //public LobSpaceTypesService(IUnitOfWork unitOfWork, IMapper mapper)
        //{
        //    _unitOfWork = unitOfWork;
        //    _mapper = mapper;
        //}

        //public async Task<ResponseDto> GetAll()
        //{
        //    var lobSpaceTypes = await _unitOfWork.LobSpaceTypes.GetAllTypes();

        //    if (lobSpaceTypes == null)
        //        return new ResponseDto { StatusCode = 200, Message = "No types found" };

        //    var AllTypes = _mapper.Map<IList<LobSpaceType>, IList<LobSpaceTypeDto>>(lobSpaceTypes);

        //    return new ResponseDto { StatusCode = 200, Data = AllTypes };

        //}



        //public async Task<ResponseDto> Add(LobSpaceTypeCreationDto lobSpaceTypeCreationDto)
        //{
        //    var lobSpaceType = _mapper.Map<LobSpaceType>(lobSpaceTypeCreationDto);

        //    var LobSpaceTypeByName = await _unitOfWork.LobSpaceTypes.GetByName(lobSpaceType.Name);

        //    if (LobSpaceTypeByName != null)
        //        return new ResponseDto { StatusCode = 400, Message = "this type is already exist" };

        //    var addedlobSpaceType = await _unitOfWork.LobSpaceTypes.AddAsync(lobSpaceType);

        //    var num = await _unitOfWork.SaveChanges();
        //    if (num != 1)
        //        return new ResponseDto { StatusCode = 500 };

        //    return new ResponseDto { StatusCode = 200, Data = addedlobSpaceType.Name , Message = "added successfully"};

        //}

        //public async Task<ResponseDto> UpdateLob(long lobSpaceTypeId, 
        //                                         LobSpaceTypeCreationDto lobSpaceTypeCreationDto)
        //{
        //    var lobSpace = await _unitOfWork.LobSpaceTypes.GetByIdAsync(lobSpaceTypeId);

        //    if (lobSpace == null || lobSpace.IsDeleted)
        //    {
        //        return new ResponseDto
        //        {
        //            StatusCode = 400,
        //            Message = "There's no such Lob Space Type"
        //        };
        //    }

        //    var LobSpaceTypeByName = await _unitOfWork.LobSpaceTypes
        //                                              .GetByName(lobSpaceTypeCreationDto.Name);

        //    if (LobSpaceTypeByName != null)
        //    {
        //        return new ResponseDto
        //        {
        //            StatusCode = 400,
        //            Message = "this type is already exist"
        //        };
        //    }

        //    lobSpace.IsDeleted = true;
        //    _unitOfWork.LobSpaceTypes.Update(lobSpace);

        //    var newLobSpace = new LobSpaceType
        //    {
        //        CreatedAt = DateTime.UtcNow,
        //        CreatedBy = new Guid("5bade43f-9b1b-4734-9f98-09d7407a2783"),
        //        Name = lobSpaceTypeCreationDto.Name
        //    };

        //    var createdLobSpace = await _unitOfWork.LobSpaceTypes.AddAsync(newLobSpace);

        //    if (await _unitOfWork.SaveChanges() < 1)
        //    {
        //        return new ResponseDto
        //        {
        //            StatusCode = 500,
        //            Message = "Server Cannot Save Resource Right now",
        //            Data = null
        //        };
        //    }

        //    await _unitOfWork.LobSpaceTypes.UpdatedRelatedContent(lobSpace.Id, createdLobSpace.Id,
        //                                         new Guid("5bade43f-9b1b-4734-9f98-09d7407a2783"));

        //    await _unitOfWork.SaveChanges();

        //    return new ResponseDto
        //    {
        //        StatusCode = 200,
        //        Data = _mapper.Map<LobSpaceTypeDto>(createdLobSpace)
        //    };
        //}

        //public async Task<ResponseDto> DeleteLob(long lobSpaceTypeId)
        //{
        //    var lobSpace = await _unitOfWork.LobSpaceTypes.GetByIdAsync(lobSpaceTypeId);

        //    if (lobSpace == null || lobSpace.IsDeleted)
        //    {
        //       return new ResponseDto
        //        {
        //            StatusCode = 400,
        //            Message = "There's no such Lob Space Type"
        //        };
        //    }

        //    lobSpace.IsDeleted = true;
        //    _unitOfWork.LobSpaceTypes.Update(lobSpace);

        //    await _unitOfWork.LobSpaceTypes.DeleteRelatedContent(lobSpaceTypeId, 
        //                                             new Guid("5bade43f-9b1b-4734-9f98-09d7407a2783"));

        //    if (await _unitOfWork.SaveChanges() < 1)
        //    {
        //        return new ResponseDto
        //        {
        //            StatusCode = 500,
        //            Message = "Server failed to update the faq"
        //        };
        //    }

        //    return new ResponseDto
        //    {
        //        StatusCode = 204
        //    };
        //}
    }
}
