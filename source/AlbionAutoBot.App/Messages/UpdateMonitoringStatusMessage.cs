namespace AlbionAutoBot.App.Messages
{
    internal class UpdateMonitoringStatusMessage
    {
        public UpdateMonitoringStatusMessage(bool monitoringStatus)
        {
            MonitoringStatus = monitoringStatus;
        }

        public bool MonitoringStatus { get; }
    }
}
