using User.Infastructure;
using Users.Core.Abstractions;

namespace User.Application.Service;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }


    public async Task Register(string userName, string email, string passwordHash)
    {
        var hashedPassword = _passwordHasher.Generate(passwordHash);

        var result = Users.Core.User.Create(Guid.NewGuid(), userName, email, hashedPassword);

        if (result.error != null)
        {
            throw new Exception(result.error);
        }

        await _userRepository.Add(result.user!);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);

        if (user == null)
        {
            throw new Exception("user not found");
        }

        if (!_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new Exception("incorrect password");
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }
}