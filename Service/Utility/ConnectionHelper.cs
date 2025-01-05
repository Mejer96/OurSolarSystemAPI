namespace OurSolarSystemAPI.Utility
{
    public abstract class ConnectionHelper
    {
        protected string ConnectionString;
        public IConfiguration Configuration { get; }

        public ConnectionHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

    }
}
