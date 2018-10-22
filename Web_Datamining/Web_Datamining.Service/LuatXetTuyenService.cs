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
    //public interface ILoaiLuatService
    //{
    //    LoaiLuat Add(LoaiLuat LoaiLuat);

    //    void Update(LoaiLuat LoaiLuat);

    //    LoaiLuat Delete(int id);
    //    LoaiLuat DeleteItem(LoaiLuat item);

    //    IEnumerable<LoaiLuat> GetAll();

    //    IEnumerable<LoaiLuat> GetAll(string keyword);

    //    LoaiLuat GetById(int id);

    //    void Save();
    //}
    //public class LoaiLuatService : ILoaiLuatService
    //{
    //    private ILoaiLuatnRepository _LoaiLuatRepository;
    //    private IUnitOfWork _unitOfWork;

    //    public LoaiLuatService(ILoaiLuatnRepository LoaiLuatRepository, IUnitOfWork unitOfWork)
    //    {
    //        this._LoaiLuatRepository = LoaiLuatRepository;
    //        this._unitOfWork = unitOfWork;
    //    }

    //    public LoaiLuat Add(LoaiLuat LoaiLuat)
    //    {
    //        return _LoaiLuatRepository.Add(LoaiLuat);
    //    }

    //    public LoaiLuat Delete(int id)
    //    {
    //        return _LoaiLuatRepository.Delete(id);
    //    }

    //    public LoaiLuat DeleteItem(LoaiLuat item)
    //    {
    //        return _LoaiLuatRepository.Delete(item);
    //    }

    //    public IEnumerable<LoaiLuat> GetAll()
    //    {
    //        return _LoaiLuatRepository.GetAll();
    //    }

    //    public IEnumerable<LoaiLuat> GetAll(string keyword)
    //    {
    //        if (!string.IsNullOrEmpty(keyword))
    //        {
    //            return _LoaiLuatRepository.GetMulti(x => x.X.Contains(keyword) || x.Y.Contains(keyword));
    //        }
    //        else
    //        {
    //            return _LoaiLuatRepository.GetAll();
    //        }
    //    }

    //    public LoaiLuat GetById(int id)
    //    {
    //        return _LoaiLuatRepository.GetSingleById(id);
    //    }

    //    public void Save()
    //    {
    //        _unitOfWork.Commit();
    //    }

    //    public void Update(LoaiLuat LoaiLuat)
    //    {
    //        _LoaiLuatRepository.Update(LoaiLuat);
    //    }
    //}
}
