using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Data.Repositories;
using Web_Datamining.Models;

namespace Web_Datamining.Service
{
    public interface IKhoaService
    {
        Khoa Add(Khoa Khoa);

        void Update(Khoa Khoa);

        Khoa Delete(int id);
        Khoa DeleteItem(Khoa item);

        IEnumerable<Khoa> GetAll();

        IEnumerable<Khoa> GetAll(string keyword);

        Khoa GetById(int id);

        void Save();
    }
    public class KhoaService : IKhoaService
    {
        private IKhoaRepository _KhoaRepository;
        private IUnitOfWork _unitOfWork;

        public KhoaService(IKhoaRepository KhoaRepository, IUnitOfWork unitOfWork)
        {
            this._KhoaRepository = KhoaRepository;
            this._unitOfWork = unitOfWork;
        }

        public Khoa Add(Khoa Khoa)
        {
            return _KhoaRepository.Add(Khoa);
        }

        public Khoa Delete(int id)
        {
            return _KhoaRepository.Delete(id);
        }

        public Khoa DeleteItem(Khoa item)
        {
            return _KhoaRepository.Delete(item);
        }

        public IEnumerable<Khoa> GetAll()
        {
            return _KhoaRepository.GetAll();
        }

        public IEnumerable<Khoa> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _KhoaRepository.GetMulti(x => x.TenKhoa.Contains(keyword));
            }
            else
            {
                return _KhoaRepository.GetAll();
            }
        }

        public Khoa GetById(int id)
        {
            return _KhoaRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Khoa Khoa)
        {
            _KhoaRepository.Update(Khoa);
        }
    }
}
