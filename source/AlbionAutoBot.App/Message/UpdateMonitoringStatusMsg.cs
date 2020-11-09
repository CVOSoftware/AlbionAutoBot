namespace AlbionAutoBot.App.Message
{
    internal class UpdateMonitoringStatusMsg
    {
        public UpdateMonitoringStatusMsg(bool monitoringStatus)
        {
            MonitoringStatus = monitoringStatus;
        }

        public bool MonitoringStatus { get; }
    }
}
