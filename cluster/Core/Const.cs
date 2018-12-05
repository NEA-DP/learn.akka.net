namespace Core
{
    public static class Const
    {
        public static class Akka
        {
            public const string DefaultConfigFileName = "akkaConfig.hocon";
            
            public const string ClusterSystemName = "ClusterSystem";

            public static class SharedActorsNames
            {
                public const string ClusterListener = "ClusterListener";
                public const string DeviceEventsReceiver = "DeviceEventsReceiver";
            }
        }
    }
}