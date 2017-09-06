using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RestDotNet
{
    public static class ResponseExtensions
    {
        public static Task SuccessAsync<TResponse>(this IRestRequest<TResponse> restRequest, Action<TResponse> action)
            => SuccessAsync(restRequest, action, CancellationToken.None);

        public static Task SuccessAsync<TResponse>(this IRestRequest<TResponse> restRequest, Action<TResponse> action, CancellationToken cancellationToken)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.OK, action);
            return restRequest.Handler.HandleAsync(cancellationToken);
        }

        public static Task SuccessAsync(this IRestRequest restRequest, Action action)
            => SuccessAsync(restRequest, action, CancellationToken.None);

        public static Task SuccessAsync(this IRestRequest restRequest, Action action, CancellationToken cancellationToken)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.OK, action);
            return restRequest.Handler.HandleAsync(cancellationToken);
        }


        public static IRestRequest<TResponse> BadRequest<TResponse, TErrorResponse>(this IRestRequest<TResponse> restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return restRequest;
        }
        
        public static IRestRequest<TResponse> BadRequest<TResponse>(this IRestRequest<TResponse> restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return restRequest;
        }

        public static IRestRequest BadRequest<TErrorResponse>(this IRestRequest restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return restRequest;
        }

        public static IRestRequest BadRequest(this IRestRequest restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.BadRequest, action);
            return restRequest;
        }
        

        public static IRestRequest<TResponse> NotFound<TResponse, TErrorResponse>(this IRestRequest<TResponse> restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.NotFound, action);
            return restRequest;
        }

        public static IRestRequest<TResponse> NotFound<TResponse>(this IRestRequest<TResponse> restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.NotFound, action);
            return restRequest;
        }

        public static IRestRequest NotFound<TErrorResponse>(this IRestRequest restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.NotFound, action);
            return restRequest;
        }

        public static IRestRequest NotFound(this IRestRequest restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.NotFound, action);
            return restRequest;
        }
        

        public static IRestRequest<TResponse> InternalServerError<TResponse, TErrorResponse>(this IRestRequest<TResponse> restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return restRequest;
        }

        public static IRestRequest<TResponse> InternalServerError<TResponse>(this IRestRequest<TResponse> restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return restRequest;
        }

        public static IRestRequest InternalServerError<TErrorResponse>(this IRestRequest restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return restRequest;
        }

        public static IRestRequest InternalServerError(this IRestRequest restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return restRequest;
        }
        

        public static IRestRequest<TResponse> Forbidden<TResponse, TErrorResponse>(this IRestRequest<TResponse> restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return restRequest;
        }

        public static IRestRequest<TResponse> Forbidden<TResponse>(this IRestRequest<TResponse> restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return restRequest;
        }

        public static IRestRequest Forbidden<TErrorResponse>(this IRestRequest restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return restRequest;
        }

        public static IRestRequest Forbidden(this IRestRequest restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.Forbidden, action);
            return restRequest;
        }
        

        public static IRestRequest<TResponse> Conflict<TResponse, TErrorResponse>(this IRestRequest<TResponse> restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.Conflict, action);
            return restRequest;
        }

        public static IRestRequest<TResponse> Conflict<TResponse>(this IRestRequest<TResponse> restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.Conflict, action);
            return restRequest;
        }

        public static IRestRequest Conflict<TErrorResponse>(this IRestRequest restRequest, Action<TErrorResponse> action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.Conflict, action);
            return restRequest;
        }

        public static IRestRequest Conflict(this IRestRequest restRequest, Action action)
        {
            restRequest.Handler.RegisterCallback(HttpStatusCode.Conflict, action);
            return restRequest;
        }
    }
}