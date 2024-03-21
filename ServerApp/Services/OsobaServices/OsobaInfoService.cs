using ServerApp.Models;

namespace ServerApp.Services.OsobaServices
{
    public abstract class OsobaInfoService : Service
    {
        protected Osoba _osoba = new();
        protected OsobaInfoService(DBContext context) : base(context)
        {
        }

        public Osoba Osoba { get { return _osoba; } set { _osoba = value; } }
    }
}
