namespace APIDEMO_.Middlewares
{
    public class StatusMiddleWare
    {


        readonly RequestDelegate next;
        /// <summary>
        /// Son componentes que se encargan de manejar las peticiones y respuestas. Permite interceptar la petición, hacer un cambio y generar un resultado diferente.
        /// </summary>
        /// <param name="nextRequest"></param>
        public StatusMiddleWare(RequestDelegate nextRequest)
        {
         next = nextRequest;
        }

        /// <summary>
        /// Metodo que tiene que tener cualquier middleware y donde se va a controlar cualquier peticion
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {

            ///antes
            
            await next(context);
            if (context.Request.Query.FirstOrDefault(p => p.Key == "Status").Key != null)
            {
                await context.Response.WriteAsync("Working");
            }

            ///Despues
        }

    }




    /// <summary>
    /// La clase se realiza asi para usarlo como metodo de extencion.
    /// </summary>

    public static class StatusMiddlewareExtension
    {
        public static IApplicationBuilder UseStatusMiddleware(this IApplicationBuilder builder)
        {
            ///UseMiddleware se utiliza para invocar al middleware
            return builder.UseMiddleware<StatusMiddleWare>();
        }
    }
}
