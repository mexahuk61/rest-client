using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RestDotNet
{
    public static class ResponseExtensions
    {
        public static Task SuccessAsync<TResponse>(this IResponse<TResponse> response, Action<TResponse> action)
            => SuccessAsync(response, action, CancellationToken.None);

        public static Task SuccessAsync<TResponse>(this IResponse<TResponse> response, Action<TResponse> action, CancellationToken cancellationToken)
        {
            response.Handler.RegisterCallback(HttpStatusCode.OK, action);
            return response.Handler.HandleAsync(cancellationToken);
        }

        public static Task SuccessAsync(this IResponse response, Action action)
            => SuccessAsync(response, action, CancellationToken.None);

        public static Task SuccessAsync(this IResponse response, Action action, CancellationToken cancellationToken)
        {
            response.Handler.RegisterCallback(HttpStatusCode.OK, action);
            return response.Handler.HandleAsync(cancellationToken);
        }


        public static IResponse<TResponse> BadRequest<TResponse, TErrorResponse>(this IResponse<TResponse> response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return response;
        }
        
        public static IResponse<TResponse> BadRequest<TResponse>(this IResponse<TResponse> response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return response;
        }

        public static IResponse BadRequest<TErrorResponse>(this IResponse response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return response;
        }

        public static IResponse BadRequest(this IResponse response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return response;
        }
        

        public static IResponse<TResponse> NotFound<TResponse, TErrorResponse>(this IResponse<TResponse> response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.NotFound, action);
            return response;
        }

        public static IResponse<TResponse> NotFound<TResponse>(this IResponse<TResponse> response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.NotFound, action);
            return response;
        }

        public static IResponse NotFound<TErrorResponse>(this IResponse response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.NotFound, action);
            return response;
        }

        public static IResponse NotFound(this IResponse response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.NotFound, action);
            return response;
        }
        

        public static IResponse<TResponse> InternalServerError<TResponse, TErrorResponse>(this IResponse<TResponse> response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return response;
        }

        public static IResponse<TResponse> InternalServerError<TResponse>(this IResponse<TResponse> response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return response;
        }

        public static IResponse InternalServerError<TErrorResponse>(this IResponse response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return response;
        }

        public static IResponse InternalServerError(this IResponse response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return response;
        }
        

        public static IResponse<TResponse> Forbidden<TResponse, TErrorResponse>(this IResponse<TResponse> response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return response;
        }

        public static IResponse<TResponse> Forbidden<TResponse>(this IResponse<TResponse> response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return response;
        }

        public static IResponse Forbidden<TErrorResponse>(this IResponse response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return response;
        }

        public static IResponse Forbidden(this IResponse response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return response;
        }
        

        public static IResponse<TResponse> Conflict<TResponse, TErrorResponse>(this IResponse<TResponse> response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.Conflict, action);
            return response;
        }

        public static IResponse<TResponse> Conflict<TResponse>(this IResponse<TResponse> response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.Conflict, action);
            return response;
        }

        public static IResponse Conflict<TErrorResponse>(this IResponse response, Action<TErrorResponse> action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.Conflict, action);
            return response;
        }

        public static IResponse Conflict(this IResponse response, Action action)
        {
            response.Handler.RegisterCallback(HttpStatusCode.Conflict, action);
            return response;
        }
    }
}