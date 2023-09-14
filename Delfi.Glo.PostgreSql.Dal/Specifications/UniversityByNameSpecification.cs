using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System.Linq.Expressions;

namespace Delfi.Glo.PostgreSql.Dal.Specifications
{
    public sealed class UniversityByNameSpecification : Specification<UniversitiesDto>
    {
        private readonly string universityName;

        public UniversityByNameSpecification(string universityName)
        {
            this.universityName = universityName;
        }

        public override Expression<Func<UniversitiesDto, bool>> ToExpression()
        {
            return university => university.Name.Contains(universityName);
        }
    }
}
