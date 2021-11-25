using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<FilesVersion, FilesVersionDto>();
            CreateMap<Docs, DocsDto>();
            CreateMap<PrescriptionSummary, PrescriptionSummaryDto>();
            CreateMap<Mur, MurDto>();
            CreateMap<SalesSummary, SalesSummaryDto>();
            CreateMap<AddSalesSummaryDto, SalesSummary>();
            CreateMap<ExpenseSummary, ExpenseSummaryDto>();
            CreateMap<AddExpenseSummaryDto, ExpenseSummary>();
        }
    }
}