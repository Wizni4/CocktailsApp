/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Domain.Common;
using CocktailsApp.Domain.Users;

using DomainUser = CocktailsApp.Domain.Users.User;

namespace CocktailsApp.Application.Users
{
    public class UserService(
        IUnitOfWork unitOfWork,
        IMapper autoMapper
    ) : IUserService
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
    }
}
