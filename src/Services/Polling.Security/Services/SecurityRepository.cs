using Polling.Security.Domain.Entities;
using Polling.Security.Dto;

namespace Polling.Security.Services
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly IRefreshRepository _refreshRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtService _jwtService;
        private readonly ITokenStorage _tokenStorage;

        public SecurityRepository(
                    IRefreshRepository refreshRepository,
                    IPasswordManager passwordManager,
                    IUserRepository userRepository,
                    IRoleRepository roleRepository,
                    IJwtService jwtService,
                    ITokenStorage tokenStorage
            )
        {
            _refreshRepository = refreshRepository;
            _passwordManager = passwordManager;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwtService = jwtService;
            _tokenStorage = tokenStorage;
        }

        public async Task Login(LoginDto dto)
        {
            // need validation
            var existUser = await _userRepository.GetOneUserByEmailAsync(dto.Email);

            if (existUser == null)
            {
                throw new Exception("User is not exist");
            }

            var IsValidated = _passwordManager.Validate(dto.Password, existUser.Password);

            if (!IsValidated)
            {
                throw new Exception("Password or login is not correct");
            }

            var accessJwt = _jwtService.CreateToken(existUser.Id.ToString(), existUser.Role.Name);

            accessJwt.Email = existUser.Email;

            _tokenStorage.Set(accessJwt);
        }

        public async Task Register(RegisterDto dto)
        {
            // need validation
            var existUser = await _userRepository.GetOneUserByEmailAsync(dto.Email);

            if (existUser is not null)
            {
                throw new Exception("User has been created yet");
            }

            var mapper = new User(dto.Email, _passwordManager.Secure(dto.Password), dto.RoleId);

            await _userRepository.CreateUserAsync(mapper);

            var accessJwt = _jwtService.CreateToken(mapper.Id.ToString(), mapper.Role.Name);

            accessJwt.Email = existUser.Email;

            _tokenStorage.Set(accessJwt);
        }
    }
}
