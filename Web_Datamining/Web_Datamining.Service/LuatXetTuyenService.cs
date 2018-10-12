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
    public interface ILuatXetTuyenService
    {
        LuatXetTuyen Add(LuatXetTuyen luatXetTuyen);

        void Update(LuatXetTuyen luatXetTuyen);

        LuatXetTuyen Delete(int id);
        LuatXetTuyen DeleteItem(LuatXetTuyen item);

        IEnumerable<LuatXetTuyen> GetAll();

        IEnumerable<LuatXetTuyen> GetAll(string keyword);

        LuatXetTuyen GetById(int id);

        void Save();
    }
    public class LuatXetTuyenService : ILuatXetTuyenService
    {
        private ILuatXetTuyenRepository _luatXetTuyenRepository;
        private IUnitOfWork _unitOfWork;

        public LuatXetTuyenService(ILuatXetTuyenRepository luatXetTuyenRepository, IUnitOfWork unitOfWork)
        {
            this._luatXetTuyenRepository = luatXetTuyenRepository;
            this._unitOfWork = unitOfWork;
        }

        public LuatXetTuyen Add(LuatXetTuyen luatXetTuyen)
        {
            return _luatXetTuyenRepository.Add(luatXetTuyen);
        }

        public LuatXetTuyen Delete(int id)
        {
            return _luatXetTuyenRepository.Delete(id);
        }

        public LuatXetTuyen DeleteItem(LuatXetTuyen item)
        {
            return _luatXetTuyenRepository.Delete(item);
        }

        public IEnumerable<LuatXetTuyen> GetAll()
        {
            return _luatXetTuyenRepository.GetAll();
        }

        public IEnumerable<LuatXetTuyen> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _luatXetTuyenRepository.GetMulti(x => x.X.Contains(keyword) || x.Y.Contains(keyword));
            }
            else
            {
                return _luatXetTuyenRepository.GetAll();
            }
        }

        public LuatXetTuyen GetById(int id)
        {
            return _luatXetTuyenRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(LuatXetTuyen luatXetTuyen)
        {
            _luatXetTuyenRepository.Update(luatXetTuyen);
        }
    }
}
