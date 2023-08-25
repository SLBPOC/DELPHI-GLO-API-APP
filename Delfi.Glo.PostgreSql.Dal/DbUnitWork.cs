using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;

namespace Delfi.Glo.PostgreSql.Dal
{
    public class DbUnitWork
    {
        private ApplicationContext _dbContext;

        public DbUnitWork(ApplicationContext applicationContext) => _dbContext = applicationContext;

        private Repository<Crew> _crew;
        //private Repository<GeneralInfoDto> _generalInfo;
        private Repository<WellGeneralInfo> _generalInfo;

        public IRepository<Crew> crews => _crew ??= new Repository<Crew>(_dbContext);

        private Repository<Well> _well;

        public IRepository<Well> wells => _well ??= new Repository<Well>(_dbContext);
        private Repository<Alerts> _alert;

        public IRepository<Alerts> alertss => _alert ??= new Repository<Alerts>(_dbContext);

        private Repository<Event> _event;

        public IRepository<Event> events => _event ??= new Repository<Event>(_dbContext);

        public IRepository<WellGeneralInfo> generalInfo => _generalInfo ??= new Repository<WellGeneralInfo>(_dbContext);
        private Repository<CustomAlert> _customalerts;
        public IRepository<CustomAlert> customalerts => _customalerts ??= new Repository<CustomAlert>(_dbContext);

  


        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync(true);
        }
    }
}
