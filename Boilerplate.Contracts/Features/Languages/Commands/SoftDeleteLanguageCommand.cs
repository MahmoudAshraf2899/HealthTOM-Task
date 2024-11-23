using Boilerplate.Contracts.Bases;
using Boilerplate.Contracts.Interfaces.Custom;
using MediatR;
#nullable disable

namespace Boilerplate.Contracts.Features.Languages.Commands
{
    public class SoftDeleteLanguageCommand : BaseSetterDTO, IRequest<IHolderOfDTO>
    {
    }
}
