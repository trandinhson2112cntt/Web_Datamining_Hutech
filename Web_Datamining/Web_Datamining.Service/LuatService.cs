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
    public interface ILuatService
    {
        Luat Add(Luat Luat);

        void Update(Luat Luat);

        Luat Delete(int id);
        Luat DeleteItem(Luat item);

        IEnumerable<Luat> GetAll();

        IEnumerable<Luat> GetAll(int idLoaiLuat);

        IEnumerable<Luat> GetAll(int idLoaiLuat, string keyword,string dt);
        IEnumerable<Luat> GetAll(int idLoaiLuat, string keyword);

        Luat GetById(int id);

        void Save();
    }
    public class LuatService : ILuatService
    {
        private ILuatRepository _LuatRepository;
        private IUnitOfWork _unitOfWork;

        public LuatService(ILuatRepository LuatRepository, IUnitOfWork unitOfWork)
        {
            ILuatRepository luatRepository = LuatRepository;
            this._LuatRepository = LuatRepository;
            this._unitOfWork = unitOfWork;
        }

        public Luat Add(Luat Luat)
        {
            return _LuatRepository.Add(Luat);
        }

        public Luat Delete(int id)
        {
            return _LuatRepository.Delete(id);
        }

        public Luat DeleteItem(Luat item)
        {
            return _LuatRepository.Delete(item);
        }

        public IEnumerable<Luat> GetAll()
        {
            return _LuatRepository.GetAll();
        }

        public IEnumerable<Luat> GetAll(int idLoaiLuat)
        {
            var listLuat = _LuatRepository.GetMulti(x => x.LuatId == idLoaiLuat);
            if (listLuat == null)
            {
                return _LuatRepository.GetAll();
            }
            else
            {
                return listLuat;
            }
        }

        public IEnumerable<Luat> GetAll(int idLoaiLuat, string keyword,string dt)
        {
            var listLuat = _LuatRepository.GetMulti(x => x.LuatId == idLoaiLuat && (x.X.Contains(keyword) || x.Y.Contains(keyword)) && (x.X.Contains(dt) || x.Y.Contains(dt)));
            if (listLuat == null)
            {
                return _LuatRepository.GetAll();
            }
            else
            {
                return listLuat;
            }
        }
        public IEnumerable<Luat> GetAll(int idLoaiLuat, string keyword)
        {
            var listLuat = _LuatRepository.GetMulti(x => x.LuatId == idLoaiLuat && (x.X.Contains(keyword) || x.Y.Contains(keyword)));
            if (listLuat == null)
            {
                return _LuatRepository.GetAll();
            }
            else
            {
                return listLuat;
            }
        }

        //public IEnumerable<Luat> GetAll(int idLoaiLuat, string keyword)
        //{
        //    throw new NotImplementedException();
        //}

        public Luat GetById(int id)
        {
            return _LuatRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Luat Luat)
        {
            _LuatRepository.Update(Luat);
        }
    }
}
