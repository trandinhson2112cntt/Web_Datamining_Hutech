using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Data.Repositories;
using Web_Datamining.Model.Models;

namespace Web_Datamining.Service
{
    public interface IKhaoSatService
    {
        KhaoSat Add(KhaoSat KhaoSat);

        void Update(KhaoSat KhaoSat);

        KhaoSat Delete(int id);
        KhaoSat DeleteItem(KhaoSat item);
        IEnumerable<KhaoSat> GetAll();

        IEnumerable<KhaoSat> GetAll(int idKhaoSat);
        void Save();
    }
    public class KhaoSatService : IKhaoSatService
    {
        private IKhaoSatRepository _KhaoSatRepository;
        private IUnitOfWork _unitOfWork;

        public KhaoSatService(IKhaoSatRepository KhaoSatRepository, IUnitOfWork unitOfWork)
        {
            this._KhaoSatRepository = KhaoSatRepository;
            this._unitOfWork = unitOfWork;
        }

        public KhaoSat Add(KhaoSat KhaoSat)
        {
            return _KhaoSatRepository.Add(KhaoSat);
        }

        public KhaoSat Delete(int id)
        {
            return _KhaoSatRepository.Delete(id);
        }

        public KhaoSat DeleteItem(KhaoSat item)
        {
            return _KhaoSatRepository.Add(item);
        }

        public IEnumerable<KhaoSat> GetAll()
        {
            return _KhaoSatRepository.GetAll();
        }

        public IEnumerable<KhaoSat> GetAll(int idKhaoSat)
        {
            var listKhaoSat = _KhaoSatRepository.GetMulti(x => x.Id == idKhaoSat);
            if (listKhaoSat == null)
            {
                return _KhaoSatRepository.GetAll();
            }
            else
            {
                return listKhaoSat;
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(KhaoSat KhaoSat)
        {
            _KhaoSatRepository.Update(KhaoSat);
        }
    }
}

