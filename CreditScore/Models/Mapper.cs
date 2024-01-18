using AutoMapper;
using CreditScore.Models.Entities;
using CreditScore.Models.Responses;

namespace CreditScore.Models
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<User, UserResponse>();
        }
    }
}
