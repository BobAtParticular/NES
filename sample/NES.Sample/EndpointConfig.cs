using EventStore;
using NES.EventStore;
using NES.NServiceBus;
using NServiceBus;

namespace NES.Sample
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Wireup.Init()
                .UsingInMemoryPersistence()
                .NES()
                .Build();

            Configure.With()
                .Log4Net()
                .DefaultBuilder()
                .JsonSerializer()
                .NES();
        }
    }
}