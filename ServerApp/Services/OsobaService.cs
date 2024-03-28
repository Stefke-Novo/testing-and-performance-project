using ServerApp.Models;
using ServerApp.Services.OsobaServices;

namespace ServerApp.Services
{
    public class OsobaService : Service
    {
        private readonly InsertOsobaService _insertOsobaService;
        private readonly UpdateOsobaService _updateOsobaService;
        private readonly DeleteOsobaService _deleteOsobaService;
        private readonly GetAllService _getAllService;
        public OsobaService(DBContext context) : base(context) 
        {
            this._insertOsobaService = new(context);
            this._getAllService = new(context);
            this._updateOsobaService = new(context);
            this._deleteOsobaService = new(context);
        }

        public List<Osoba> GetAll(int index, int size)
        {
            return _getAllService.GetAll(index, size);
        }

        public Osoba Insert(Osoba o)
        {
            _insertOsobaService.Osoba = o;
            return _insertOsobaService.Execute();
        }
        public Osoba Update(Osoba o)
        {
            _updateOsobaService.Osoba = o;
            return _updateOsobaService.Execute();
        }
        public String Delete(Osoba o)
        {
            _deleteOsobaService.Osoba = o;
            _deleteOsobaService.Execute();
            return $"User with id {o.O} is successfully deleted.";
        }
    }
}
