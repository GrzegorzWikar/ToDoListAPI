﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoAPI.DTO;
using ToDoAPI.Model;

namespace ToDoAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context) 
        {
            var user = (DTOUser?)context.HttpContext.Items["User"];

            if (user == null)
            {
                context.Result = new JsonResult (new {message = "Unauthorized" }) {StatusCode = StatusCodes.Status401Unauthorized};
            }
        }
    }
}
