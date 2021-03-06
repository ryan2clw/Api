﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                //.AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
#if DEBUG
                    options.Authority = "http://127.0.0.1:5002";
                    options.RequireHttpsMetadata = false;
#else
                    options.Authority = "https://127.0.0.1:5002"; 
                    options.RequireHttpsMetadata = false; // MARK TO DO: make staging dir
#endif
                    options.Audience = "api1";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        ValidateAudience = false,
                        ValidateActor = false,
                        ValidateIssuer = false,
                    };
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
