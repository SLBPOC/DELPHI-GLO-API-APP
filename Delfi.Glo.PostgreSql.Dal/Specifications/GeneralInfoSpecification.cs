using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System.Linq.Expressions;

namespace Delfi.Glo.PostgreSql.Dal.Specifications
{
    public sealed class GeneralInfoSpecification : Specification<WellGeneralInfoDto>
    {
        private readonly int id;

        public GeneralInfoSpecification(int _id)
        {
            this.id = _id;
            //this._list = customAlertDtos;
        }
        public override Expression<Func<WellGeneralInfoDto, bool>> ToExpression()
        {
            return x => x.Id == id;
        }
    }
}
