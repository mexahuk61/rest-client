using System;
using System.Net;

namespace RESTfulClient
{
    public static class RestHandlerExtensions
    {
        public static IRestHandler<TResponse> BadRequest<TResponse, TErrorResponse>(this IRestHandler<TResponse> restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return restHandler;
        }

        public static IRestHandler BadRequest<TErrorResponse>(this IRestHandler restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return restHandler;
        }
        
        public static IRestHandler<TResponse> BadRequest<TResponse>(this IRestHandler<TResponse> restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return restHandler;
        }

        public static IRestHandler BadRequest(this IRestHandler restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return restHandler;
        }

        public static IRestHandler<TResponse> NotFound<TResponse, TErrorResponse>(this IRestHandler<TResponse> restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.NotFound, action);
            return restHandler;
        }

        public static IRestHandler NotFound<TErrorResponse>(this IRestHandler restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.NotFound, action);
            return restHandler;
        }

        public static IRestHandler<TResponse> NotFound<TResponse>(this IRestHandler<TResponse> restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.NotFound, action);
            return restHandler;
        }

        public static IRestHandler NotFound(this IRestHandler restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.NotFound, action);
            return restHandler;
        }

        public static IRestHandler<TResponse> InternalServerError<TResponse, TErrorResponse>(this IRestHandler<TResponse> restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return restHandler;
        }

        public static IRestHandler InternalServerError<TErrorResponse>(this IRestHandler restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return restHandler;
        }

        public static IRestHandler<TResponse> InternalServerError<TResponse>(this IRestHandler<TResponse> restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return restHandler;
        }

        public static IRestHandler InternalServerError(this IRestHandler restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return restHandler;
        }

        public static IRestHandler<TResponse> Forbidden<TResponse, TErrorResponse>(this IRestHandler<TResponse> restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return restHandler;
        }

        public static IRestHandler Forbidden<TErrorResponse>(this IRestHandler restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return restHandler;
        }

        public static IRestHandler<TResponse> Forbidden<TResponse>(this IRestHandler<TResponse> restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return restHandler;
        }

        public static IRestHandler Forbidden(this IRestHandler restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return restHandler;
        }

        public static IRestHandler<TResponse> Conflict<TResponse, TErrorResponse>(this IRestHandler<TResponse> restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.Conflict, action);
            return restHandler;
        }

        public static IRestHandler Conflict<TErrorResponse>(this IRestHandler restHandler, Action<TErrorResponse> action)
        {
            restHandler.RegisterCallback(HttpStatusCode.Conflict, action);
            return restHandler;
        }

        public static IRestHandler<TResponse> Conflict<TResponse>(this IRestHandler<TResponse> restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.Conflict, action);
            return restHandler;
        }

        public static IRestHandler Conflict(this IRestHandler restHandler, Action action)
        {
            restHandler.RegisterCallback(HttpStatusCode.Conflict, action);
            return restHandler;
        }
    }
}
