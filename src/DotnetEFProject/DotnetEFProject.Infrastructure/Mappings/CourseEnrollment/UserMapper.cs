using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DotnetEFProject.Infrastructure.Mappings.CourseEnrollment
{
    public static class UserMapper
    {
        public static User ToUserEntity(this AddUserInput source)
        {
            if (source is null) return null!;
            return new User
            {
                FullName = source.FullName!,
                Email = source.Email!,
                UserProfile = new UserProfile
                {
                    Bio = source.Bio,
                    BirthDate = source.BirthDate
                }
            };
        }

        public static List<User> ToUserEntityList(this IEnumerable<AddUserInput> source)
            => source?.Select(x => x.ToUserEntity()).ToList() ?? [];

        public static GetUserOutput ToDto(this Entities.User source)
        {
            if (source is null) return null!;
            return new GetUserOutput(source.Id, source.FullName, source.Email);
        }

        public static List<GetUserOutput> ToDtoList(this IEnumerable<Entities.User> source)
            => source?.Select(x => x.ToDto()).ToList() ?? [];

        public static Expression<Func<Entities.User, GetUserOutput>> ToExpression()
            => source => new GetUserOutput(source.Id, source.FullName, source.Email);
    }
}
 