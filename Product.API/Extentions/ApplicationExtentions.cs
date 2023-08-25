namespace Product.API.Extentions
{
    public static class ApplicationExtentions
    {
        public static void UseInfrastucture(this IApplicationBuilder app)
        {


            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            //app.UseHttpsRedirection(); for production only

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });


        }
    }
}
