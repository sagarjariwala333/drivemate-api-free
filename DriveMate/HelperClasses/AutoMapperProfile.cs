using AutoMapper;
using DriveMate.Entities;
using DriveMate.Requests;
using DriveMate.Requests.TripRequest;
using DriveMate.Requests.UserRequest;

namespace DriveMate.HelperClasses
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<AddTripAddressRequest, Trip>();
            CreateMap<Trip, AddTripAddressRequest>();

            CreateMap<SignupRequest, User>();
            CreateMap<User, SignupRequest>();

            CreateMap<AddressModal, Address>();
            CreateMap<Address, AddressModal>();

            CreateMap<UserAddressRequestDto, UserAddress>();
            CreateMap<UserAddress, UserAddressRequestDto>();

            CreateMap<UserDocument, UploadDocumentRequest>();
            CreateMap<UploadDocumentRequest, UserDocument>();

            CreateMap<Trip,TripRequest>();
            CreateMap<TripRequest,Trip>();
        }
    }
}
