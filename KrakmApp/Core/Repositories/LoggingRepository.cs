using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class LoggingRepository : Repository<Error>, ILoggingRepository
    {
        public LoggingRepository(KrakmAppContext context)
            : base(context)
        { }

        public override void Commit()
        {
            try
            {
                base.Commit();
            }
            catch { }
        }
    }
}
