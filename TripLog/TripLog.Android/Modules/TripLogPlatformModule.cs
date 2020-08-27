using Ninject.Modules;
using TripLog.Droid.Services;
using TripLog.Models;

namespace TripLog.Droid.Modules
{
    public class TripLogPlatformModule: NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationService>()
                .To<LocationService>()
                .InSingletonScope();
        }
    }
}