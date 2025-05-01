using ChamaOSindico.Application.Auth;
using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs.Auth;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Application.Security;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace ChamaOSindico.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly ITransactionService _transactionService;
        private readonly JwtService _jwtService;
        private readonly ITokenBlackListRepository _tokenBlacklistRepository;
        private readonly IUserService _userService;

        public AuthService(
            IUserRepository userRepository, 
            JwtService jwtService, 
            ITokenBlackListRepository tokenBlacklistRepository, 
            IUserService userService,
            IResidentRepository residentRepository,
            ITransactionService transactionService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _tokenBlacklistRepository = tokenBlacklistRepository;
            _userService = userService;
            _residentRepository = residentRepository;
            _transactionService = transactionService;
        }

        public async Task<ApiResponse<AuthResultDto>> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var existing = await _userRepository.GetUserByEmailAsync(registerUserDto.Email);

            if (existing != null)
            {
                return ApiResponse<AuthResultDto>.ErrorResult("Usuário já cadastrado.", HttpStatusCode.Conflict);
            }

            string? token = null;
            string? residentName = null;
            string? userId = null;

            await _transactionService.ExecuteTransactionAsync(async () =>
            {
                var newResident = new Resident
                {
                    Name = registerUserDto.Name,
                    Email = registerUserDto.Email,
                    Phone = registerUserDto.Phone,
                    Rg = registerUserDto.Rg,
                    Cpf = registerUserDto.Cpf,
                    BirthDate = registerUserDto.BirthDate,
                    ApartmentNumber = registerUserDto.ApartmentNumber
                };

                await _residentRepository.CreateResidentAsync(newResident);

                var newUser = new User
                {
                    Email = registerUserDto.Email,
                    PasswordHash = PasswordHasher.Hash(registerUserDto.Password),
                    Role = registerUserDto.Role,
                    PersonId = newResident.Id
                };

                await _userRepository.CreateUserAsync(newUser);

                // Assign the user ID to the resident
                await _residentRepository.AssignUserIdToResidentAsync(newResident.Id, newUser.Id);

                var authDto = new AuthUserDto
                {
                    Id = newUser.Id,
                    Email = registerUserDto.Email,
                    Role = newUser.Role.ToString()
                };

                token = _jwtService.GenerateToken(authDto);
                residentName = newResident.Name;
                userId = newUser.Id;
            });

            var result = new AuthResultDto
            {
                Token = token,
                Name = residentName,
                UserId = userId
            };

            return ApiResponse<AuthResultDto>.SuccessResult(result, "Usuário criado com sucesso!");
        }

        public async Task<ApiResponse<AuthResultDto>> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _userService.GetUserByEmailAsync(loginUserDto.Email);

            if (user == null)
            {
                return ApiResponse<AuthResultDto>.ErrorResult("O e-mail disponibilizado não existe.", HttpStatusCode.Unauthorized);
            }

            var userHashedPassword = await _userRepository.GetUserHashedPassword(user.Id);

            if (!PasswordHasher.Verify(loginUserDto.Password, userHashedPassword))
            {
                return ApiResponse<AuthResultDto>.ErrorResult("Senha incorreta.", HttpStatusCode.Unauthorized);
            }

            userHashedPassword = null; // Clear password hash for security reasons

            var authDto = new AuthUserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            var token = _jwtService.GenerateToken(authDto);

            var resident = await _residentRepository.GetResidentByUserIdAsync(user.Id);

            string? residentName = resident.Name;
            string? userId = resident.UserId;

            var result = new AuthResultDto
            {
                Token = token,
                Name = residentName,
                UserId = userId
            };

            return ApiResponse<AuthResultDto>.SuccessResult(result, "Login realizado com sucesso!");
        }

        public async Task<ApiResponse<string>> LogoutAsync(string token)
        {
            try
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var expiration = jwtToken.ValidTo;

                await _tokenBlacklistRepository.AddTokenToBlackListAsync(token, expiration);
                return ApiResponse<string>.SuccessResult(null, "Successfully logged out.");
            }
            catch
            {
                return ApiResponse<string>.ErrorResult("Invalid token.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<ApiResponse<string>> DeleteUserAsync(string userId)
        {
            bool isDeleted = false;
            
            await _transactionService.ExecuteTransactionAsync(async () =>
            {
                var resident = await _residentRepository.GetResidentByUserIdAsync(userId);

                if (resident != null)
                {
                    await _residentRepository.DeleteResidentAsync(resident.Id);
                    await _userRepository.DeleteUserAsync(userId);
                    isDeleted = true;
                }
            });
            
            if (isDeleted)
            {
                return ApiResponse<string>.SuccessResult(null, "Usuário deletado com sucesso.");
            }
            else
            {
                return ApiResponse<string>.ErrorResult("Usuário não encontrado.", HttpStatusCode.NotFound);
            }
        }
    }
}
