namespace Cart.Business.Configuration.Settings
{
    public class RabbitMqServerSettings
    {
        public string? ConnectionString { get; set; }
        public string? Queue { get; set; }
    }
}
