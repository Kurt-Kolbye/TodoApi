using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class LabelService : ILabelService
    {
        private IUnitOfWork _unitOfWork;

        public LabelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<Label> GetAll()
        {
            return _unitOfWork.Labels.GetAll();
        }

        public Label Get(long id)
        {
            return _unitOfWork.Labels.Get(id);
        }

        public bool Add(Label label)
        {
            bool result = false;
            try
            {
                _unitOfWork.Labels.Add(label);
                _unitOfWork.Complete();
                result = true;
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return result;
        }

        public bool Update(Label label)
        {
            bool result = false;
            try
            {
                _unitOfWork.Labels.Update(label);
                _unitOfWork.Complete();
                result = true;
            }
            catch (Exception)
            {
               _unitOfWork.Dispose();
            }

            return result;
        }


        public Label Remove(long id)
        {
            var item = _unitOfWork.Labels.Get(id);
            try
            {
                if (item != null)
                {
                    _unitOfWork.Labels.Remove(item);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return item;
        }
    }
}
