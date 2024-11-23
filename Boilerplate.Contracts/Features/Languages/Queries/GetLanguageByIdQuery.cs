using Boilerplate.Contracts.Bases;
using Boilerplate.Contracts.Interfaces.Custom;
using MediatR;
#nullable disable

namespace Boilerplate.Contracts.Features.Languages.Queries
{
    public class GetLanguageByIdQuery : BaseGetterDTO, IRequest<IHolderOfDTO>
    {

    }
}
