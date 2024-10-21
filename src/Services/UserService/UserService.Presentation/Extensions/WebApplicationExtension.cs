﻿using UserService.Domain.Middleware;

namespace UserService.Domain.Extensions;

public static class WebApplicationExtension
{
    public static void AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
    public static void AddApplicationMiddleware(this WebApplication app)
    {
        app.UseHttpsRedirection(); 
        app.UseRouting(); 
        
        app.UseCors(builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500") 
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }); 
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        
        
    }
}