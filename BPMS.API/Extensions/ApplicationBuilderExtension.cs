using Sqids;

namespace BPMS.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSqids(this IApplicationBuilder app)
        {
            SqidsExtensions.Configure(app.ApplicationServices.GetService<SqidsEncoder<int>>());
        }
    }
}
