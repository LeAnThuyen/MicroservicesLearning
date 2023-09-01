using Microsoft.AspNetCore.HttpOverrides;

namespace Product.API.Extentions
{
    public static class ApplicationExtentions
    {
        public static void UseInfrastucture(this IApplicationBuilder app)
        {
            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });
            // using Microsoft.AspNetCore.HttpOverrides;
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseCors("CorsPolicy");
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();
            //app.UseHttpsRedirection(); for production only



            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });


        }
    }
}
