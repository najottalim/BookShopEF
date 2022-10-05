using System;
using BookStore.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace BookStore.Service.Helpers;

public class HttpContextHelper
{
    public static IHttpContextAccessor Accessor = null!;
    public static HttpResponse Response => Accessor.HttpContext.Response;
    public static HttpRequest Request => Accessor.HttpContext.Request;
    public static IHeaderDictionary ResponseHeaders => Response.Headers;
    public static IHeaderDictionary RequestHeaders => Request.Headers;

    public static Localization Localization =>
        Enum.TryParse(RequestHeaders["Accept-Language"], true, out Localization result)
            ? result
            : Localization.Uz;

}