using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.BLL.DTO;
using RealEstate.DLL.Entites;
using RealEstate.DLL.Interfaces;

namespace RealEstate.BLL.Helper
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Client, ClientDTO>()
                .ForMember(dest => dest.AppartmentIds, opts => opts.MapFrom(src => src.Apartments.Select(c => c.Id)))
                .ForMember(dest => dest.BookingsIds, opts => opts.MapFrom(src => src.Bookings.Select(b => b.Id)));
			CreateMap<ClientDTO, Client>();
            CreateMap<Apartment, ApartmentDTO>()
                .ForMember(dest => dest.BookingIds, opts => opts.MapFrom(src => src.Bookings.Select(b => b.Id)));
			CreateMap<ApartmentDTO, Apartment>();

            CreateMap<BookingModel, BookingModelDTO>()
                .ForMember(dest => dest.AppartmentTitle, opts => opts.MapFrom(src => src.Appartment.Title));
            CreateMap<BookingModelDTO, BookingModel>();
		}
	}
}
