using AutoMapper;
using DevOne.Security.Cryptography.BCrypt;
using DriveMate.Context;
using DriveMate.Entities;
using DriveMate.HelperClasses;
using DriveMate.HelperClasses.Interfaces;
using DriveMate.Interfaces;
using DriveMate.Interfaces.BaseRepository;
using DriveMate.Requests;
using DriveMate.Requests.UserRequest;
using DriveMate.Responses.UserResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DriveMate.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private IConfiguration _configuration;
        private AppDBContext dBContext;
        private IMapper _mapper;
        private IPasswordHelper passwordHelper;
        private IDocumentService _documentService;

        public UserService(IRepository<User> userrepository,
            IConfiguration configuration,
            AppDBContext dBContext,
            IMapper mapper,
            IPasswordHelper passwordHelper,
            IDocumentService documentService)
        {
            _mapper = mapper;
            _userRepository = userrepository;
            _configuration = configuration;
            this.dBContext = dBContext;
            this.passwordHelper = passwordHelper;
            _documentService = documentService;
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                return await _userRepository.Table.Where(x => x.IsDeleted == false).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoginResponse> SignIn(LoginRequest loginRequest)
        {
            var data =await Authenticate(loginRequest);

            if(data != null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim("Email",data.Email),
                    new Claim("Id",data.Id.ToString()),
                    new Claim("Role",data.Role.ToString()),
                    new Claim("uniqueid",Guid.NewGuid().ToString())
                };
                var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey,SecurityAlgorithms.HmacSha256Signature)
                    );

                return new LoginResponse()
                {
                    Token=new JwtSecurityTokenHandler().WriteToken(token),
                    Message="Success",
                    status = 1
                };
            }
            else
            {
                return new LoginResponse()
                {
                    Token = "",
                    Message = "Authentication failed",
                    status = 0
                };
            }
        }

        
        public async Task<User?> Authenticate(LoginRequest loginRequest) 
        {
            try
            {
                var data = await _userRepository.Table
                            .Where(x => x.Email == loginRequest.Email)
                            .FirstOrDefaultAsync();

                if(passwordHelper.VerifyPassword(loginRequest.Password,data.Password))
                {
                    return data;
                }

                return null;
            }
            catch(Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertDocument(IFormFile formFile, string DocumentNo, Guid Id)
        {
            UploadDocumentRequest userDocument = new UploadDocumentRequest();
            using (var memory = new MemoryStream())
            {
                formFile.CopyTo(memory);
                userDocument.FileData = memory.ToArray();
            }

            userDocument.UserId = Id;
            userDocument.Name = formFile.FileName;

            userDocument.DocumentNo = DocumentNo;
            userDocument.status = true;
            userDocument.IsDeleted = false;
            userDocument.Type = Path.GetExtension(formFile.FileName);
            
            var result = await _documentService.SaveAsync(userDocument);
        }


        public async Task<User?> Save(IFormCollection form)
        {
            try
            {
                var str = form.ToList()[0].Value;
                JsonDocument jsonDocument = JsonDocument.Parse(str);

                //JsonDocument Njson = CapitalizeFirstLetter(jsonDocument);

                SignupRequest signupRequest = JsonSerializer.Deserialize<SignupRequest>(jsonDocument);

                signupRequest.Password = passwordHelper.EncryptPassword(signupRequest.Password);

                if (signupRequest.Id == null)
                {
                    User user = _mapper.Map<User>(signupRequest);
                    await dBContext.Users.AddAsync(user);

                    IFormFile Profilepic = form.Files[0];
                    await this.InsertDocument(Profilepic, user.Id.ToString() , (Guid)user.Id);
                    IFormFile Aadhar = form.Files[1];
                    await this.InsertDocument(Profilepic, signupRequest.AadharNo, (Guid)user.Id);
                    IFormFile Licenece = form.Files[2];
                    await this.InsertDocument(Profilepic, signupRequest.LicenceNo, (Guid)user.Id);

                    await dBContext.SaveChangesAsync();
                    return user;
                }
                else
                {
                    User user = _mapper.Map<User>(signupRequest);
                    await _userRepository.UpdateAsync(user);
                    await dBContext.SaveChangesAsync();
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);        
            }
        }

    }
}
