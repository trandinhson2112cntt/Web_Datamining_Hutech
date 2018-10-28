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
    public interface IMonHocService
    {
        MonHoc Add(MonHoc MonHoc);

        void Update(MonHoc MonHoc);

        MonHoc Delete(int id);
        MonHoc DeleteItem(MonHoc item);

        IEnumerable<MonHoc> GetAll();

        IEnumerable<MonHoc> GetAll(string keyword);

        MonHoc GetById(int id);

        void Save();
    }
    public class MonHocService : IMonHocService
    {
        private IMonHocRepository _MonHocRepository;
        private IUnitOfWork _unitOfWork;

        public MonHocService(IMonHocRepository MonHocRepository, IUnitOfWork unitOfWork)
        {
            this._MonHocRepository = MonHocRepository;
            this._unitOfWork = unitOfWork;
        }

        public MonHoc Add(MonHoc MonHoc)
        {
            return _MonHocRepository.Add(MonHoc);
        }

        public MonHoc Delete(int id)
        {
            return _MonHocRepository.Delete(id);
        }

        public MonHoc DeleteItem(MonHoc item)
        {
            return _MonHocRepository.Delete(item);
        }

        public IEnumerable<MonHoc> GetAll()
        {
            return _MonHocRepository.GetAll();
        }

        public IEnumerable<MonHoc> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _MonHocRepository.GetMulti(x => x.TenMon.Contains(keyword));
            }
            else
            {
                return _MonHocRepository.GetAll();
            }
        }

        public MonHoc GetById(int id)
        {
            return _MonHocRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MonHoc MonHoc)
        {
            _MonHocRepository.Update(MonHoc);
        }
    }
}
