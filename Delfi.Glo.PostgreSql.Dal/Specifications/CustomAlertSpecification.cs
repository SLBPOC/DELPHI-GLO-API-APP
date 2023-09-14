using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System.Linq.Expressions;

namespace Delfi.Glo.PostgreSql.Dal.Specifications
{
    public sealed class CustomAlertSpecification : Specification<CustomAlertDto>
    {
        private readonly int id;
        private readonly IList<CustomAlertDto> _list;

        public CustomAlertSpecification(int _id)
        {
            this.id = _id;
            //this._list = customAlertDtos;
        }
        public override Expression<Func<CustomAlertDto, bool>> ToExpression()
        {
            return x => x.Id == id;
        }
    }
}
