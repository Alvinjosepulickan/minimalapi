using Microsoft.AspNetCore.Mvc;
using minimal.Data;
using minimal.DTO;
using minimal.Models;
using minimal.Repository.Interfaces;

namespace minimal
{
    public static class AuthEndPoint
    {
        public static async void AuthEndPointMethod(this WebApplication app)
        {
            app.MapPost("api/login", async (IAuthRepository _auth, [FromBody] LoginRequestDTO loginRequest) => {
                var authenticatedResult =await _auth.Authenticate(loginRequest);
                if(authenticatedResult!=null)
                {
                    Results.Ok(authenticatedResult);
                }
                Results.BadRequest();
            }
            );

            app.MapPost("api/createlogin", (IAuthRepository _auth, [FromBody] RegistrationRequestDTO registrationRequest) => {
                if(_auth.IsUniqueUser(registrationRequest.UserName))
                {
                    _auth.Register(registrationRequest);
                }
                else
                {
                    Results.NotFound();
                }

            }
            );
        }
    }
}
