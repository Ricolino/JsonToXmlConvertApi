namespace JsonToXmlConvertApi.Middleware
{
    public class ApiKeyMiddleware
    {
        //todo Atomic - implement logging
        private readonly ILogger<ApiKeyMiddleware> _logger;

        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";

        public ApiKeyMiddleware(ILogger<ApiKeyMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                _logger.LogInformation("Api Key was not provided. (Using ApiKeyMiddleware) ");

                await context.Response.WriteAsync("Api Key was not provided. (Using ApiKeyMiddleware) ");
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey=appSettings.GetValue<string>(APIKEYNAME);

            if(!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                _logger.LogInformation("Unauthorized client. (Using ApiKeyMiddleware) ");
                await context.Response.WriteAsync("Unauthorized client. (Using ApiKeyMiddleware) ");
                return;
            }

            await _next(context);
        }
    }
}
