using AutoMapper;
using Digital_Gravity.Models;
using Digital_Gravity.ViewModels;

namespace Digital_Gravity.AutoMappper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define your mappings here
            // Example: Mapping between Subscription and SubscriptionViewModel
            CreateMap<Subscription, UserSubscriptionViewModel>();
        }
    }
}
