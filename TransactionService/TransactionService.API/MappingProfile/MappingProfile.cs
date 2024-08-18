using AutoMapper;
using TransactionService.Application.Features.Transactions.Command;
using TransactionService.Domain.Entities;

namespace TransactionService.API.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transaction, TransactionViewModel>().ReverseMap();
    }
   
}