using NServiceBus;
using NServiceBus.MessageInterfaces;
using NServiceBus.ObjectBuilder;

namespace NES.NServiceBus
{
    public static class ConfigureNES
    {
        public static Configure NES(this Configure config)
        {
            config.Configurer.ConfigureComponent<MessageModule>(ComponentCallModelEnum.Singlecall);

            DI.Current.Register(() => config.Builder.Build<IMessageMapper>());
            DI.Current.Register<IEventPublisher, IBus>(bus => new BusAdapter(bus));
            DI.Current.Register(() => config.Builder.Build<IBus>());

            return config;
        }
    }
}