using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;

namespace TouchSystems.Services.Composers
{
    public class RegisterDependencies : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IGoogleCaptchaService, GoogleCaptchaService>();
        }
    }
}
