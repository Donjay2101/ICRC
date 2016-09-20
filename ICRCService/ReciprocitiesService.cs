using ICRC.Data.Infrastructure;
using ICRC.Data.Repositories;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ICRCService
{
    public interface IReciprocitiesService
    {
        IEnumerable<Reciprocities> GetReciprocities();
        IEnumerable<Reciprocities> GetReciprocitiesByBoardID(int ID);
        Reciprocities GetReciprocitiesByID(int ID);
        IEnumerable<Reciprocities> GetReciprocities(Expression<Func<Reciprocities, bool>> where);
        IEnumerable<Reciprocities> ReciprocityGetByPersonID(int ID);
        void CreateReciprocity(Reciprocities reciprocity);
        void UpdateReciprocity(Reciprocities reciprocity);
        void Save();
        void Delete(int ID);

    }
    public class ReciprocitiesService:IReciprocitiesService
    {
        public readonly IReciproctiesRepository reciprocityRepository;
        public readonly IUnitOfWork unitOfWork;

        public ReciprocitiesService(IReciproctiesRepository reciprocityRepository, IUnitOfWork unitOfWork)        
        {
            this.reciprocityRepository = reciprocityRepository;
            this.unitOfWork = unitOfWork;
        }


        #region Methods
         
        public IEnumerable<Reciprocities> GetReciprocitiesByBoardID(int ID)
        {
            return GetReciprocities().Where(x => x.OriginatingBoard == ID);
        }
        public IEnumerable<Reciprocities> GetReciprocities()
        {
            return reciprocityRepository.GetAll().OrderBy(x=>x.DateofEntry);
        }


        public Reciprocities GetReciprocitiesByID(int ID)
        {
            return reciprocityRepository.GetByID(ID);
        }


        public IEnumerable<Reciprocities> GetReciprocities(Expression<Func<Reciprocities,bool>>where)
        {
            return reciprocityRepository.GetMany(where);
        }

        public void CreateReciprocity(Reciprocities reciprocity)
        {
            reciprocityRepository.Add(reciprocity);
        }

        public void UpdateReciprocity(Reciprocities Reciprocity)
        {
            reciprocityRepository.Update(Reciprocity);
        }
        public void Delete(int ID)
        {
            var data=reciprocityRepository.GetByID(ID);
            reciprocityRepository.Delete(data);
        }


        public IEnumerable<Reciprocities> ReciprocityGetByPersonID(int ID)
        {
            return reciprocityRepository.GetByPersonID(ID);
        }
        public void Save()
        {
            unitOfWork.Commit();
        }
        #endregion  
    }
}
