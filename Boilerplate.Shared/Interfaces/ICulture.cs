using Boilerplate.Shared.Resources;
using Microsoft.Extensions.Localization;

namespace Boilerplate.Shared.Interfaces
{
    public interface ICulture
    {
        public IStringLocalizer<SharedResource> SharedLocalizer { get;}
        public IStringLocalizer<PagesResource> PagesLocalizer { get;}
    }
}
