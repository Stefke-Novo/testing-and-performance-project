using ServerApp.Models;

namespace ServerApp.Services.OsobaServices
{
    public abstract class OsobaInfoService<T> : Transaction<T>
    {
        protected Osoba _osoba = new();
        protected OsobaInfoService(DBContext context) : base(context)
        {
        }

        public Osoba Osoba { get { return _osoba; } set { _osoba = value; } }
    }
}
