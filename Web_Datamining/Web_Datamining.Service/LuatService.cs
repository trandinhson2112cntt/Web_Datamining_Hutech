﻿using System;
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

        IEnumerable<Luat> GetAll(string keyword);

        Luat GetById(int id);

        void Save();
    }
    public class LuatService : ILuatService
    {
        private ILuatRepository _LuatRepository;
        private IUnitOfWork _unitOfWork;

        public LuatService(ILuatRepository LuatRepository, IUnitOfWork unitOfWork)
        {
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

        public IEnumerable<Luat> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _LuatRepository.GetMulti(x => x.X.Contains(keyword) || x.Y.Contains(keyword));
            }
            else
            {
                return _LuatRepository.GetAll();
            }
        }

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