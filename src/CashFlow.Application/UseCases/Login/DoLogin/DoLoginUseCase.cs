using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator
    )
    {
        _repository = repository;
        _passwordEncrypter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email);

        if(user is null)
        {
            throw new InvalidLoginException();
        }

        var passwordMatch = _passwordEncrypter.Verify(request.Password, user.Password);

        if(passwordMatch == false)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }
}