using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RestDotNet
{
    public static class RequestExtensions
    {
        public static Task SuccessAsync<TResponse>(this IRestRequest<TResponse> request, Action<TResponse> action)
            => SuccessAsync(request, action, CancellationToken.None);

        public static Task SuccessAsync<TResponse>(this IRestRequest<TResponse> request, Action<TResponse> action, CancellationToken cancellationToken)
        {
            request.RegisterCallback(HttpStatusCode.OK, action);
            return request.ExecuteAsync(cancellationToken);
        }

        public static Task SuccessAsync(this IRestRequest request, Action action)
            => SuccessAsync(request, action, CancellationToken.None);

        public static Task SuccessAsync(this IRestRequest request, Action action, CancellationToken cancellationToken)
        {
            request.RegisterCallback(HttpStatusCode.OK, action);
            return request.ExecuteAsync(cancellationToken);
        }


        public static IRestRequest<TResponse> BadRequest<TResponse, TErrorResponse>(this IRestRequest<TResponse> request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.BadRequest, action);
            return request;
        }
        
        public static IRestRequest<TResponse> BadRequest<TResponse>(this IRestRequest<TResponse> request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.BadRequest, action);
            return request;
        }

        public static IRestRequest BadRequest<TErrorResponse>(this IRestRequest request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.BadRequest, action);
            return request;
        }

        public static IRestRequest BadRequest(this IRestRequest request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.BadRequest, action);
            return request;
        }
        

        public static IRestRequest<TResponse> NotFound<TResponse, TErrorResponse>(this IRestRequest<TResponse> request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.NotFound, action);
            return request;
        }

        public static IRestRequest<TResponse> NotFound<TResponse>(this IRestRequest<TResponse> request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.NotFound, action);
            return request;
        }

        public static IRestRequest NotFound<TErrorResponse>(this IRestRequest request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.NotFound, action);
            return request;
        }

        public static IRestRequest NotFound(this IRestRequest request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.NotFound, action);
            return request;
        }
        

        public static IRestRequest<TResponse> InternalServerError<TResponse, TErrorResponse>(this IRestRequest<TResponse> request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return request;
        }

        public static IRestRequest<TResponse> InternalServerError<TResponse>(this IRestRequest<TResponse> request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return request;
        }

        public static IRestRequest InternalServerError<TErrorResponse>(this IRestRequest request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return request;
        }

        public static IRestRequest InternalServerError(this IRestRequest request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.InternalServerError, action);
            return request;
        }
        

        public static IRestRequest<TResponse> Forbidden<TResponse, TErrorResponse>(this IRestRequest<TResponse> request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.Forbidden, action);
            return request;
        }

        public static IRestRequest<TResponse> Forbidden<TResponse>(this IRestRequest<TResponse> request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.Forbidden, action);
            return request;
        }

        public static IRestRequest Forbidden<TErrorResponse>(this IRestRequest request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.Forbidden, action);
            return request;
        }

        public static IRestRequest Forbidden(this IRestRequest request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.Forbidden, action);
            return request;
        }
        

        public static IRestRequest<TResponse> Conflict<TResponse, TErrorResponse>(this IRestRequest<TResponse> request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.Conflict, action);
            return request;
        }

        public static IRestRequest<TResponse> Conflict<TResponse>(this IRestRequest<TResponse> request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.Conflict, action);
            return request;
        }

        public static IRestRequest Conflict<TErrorResponse>(this IRestRequest request, Action<TErrorResponse> action)
        {
            request.RegisterCallback(HttpStatusCode.Conflict, action);
            return request;
        }

        public static IRestRequest Conflict(this IRestRequest request, Action action)
        {
            request.RegisterCallback(HttpStatusCode.Conflict, action);
            return request;
        }
    }
}