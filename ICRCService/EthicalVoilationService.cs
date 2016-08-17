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
    public interface IEthicalViolationService
    {
        IEnumerable<EthicalViolation> GetEthicalviolations();
        EthicalViolation GetEthicalVoilationByID(int ID);
        IEnumerable<EthicalViolation> GetEthicalviolations(Expression<Func<EthicalViolation, bool>> where);       
        void CreateEthicalVoilation(EthicalViolation Board);
        void UpdateEthicalVoilation(EthicalViolation Board);
        void Save();
        void Delete(int ID);
    }
    public class EthicalviolationService : IEthicalViolationService
    {
        public readonly IEthicalviolationsRepository EthicalVoilationRepository;
        public readonly IUnitOfWork unitOfWork;

        public EthicalviolationService(IEthicalviolationsRepository EthicalVoilationRepository, IUnitOfWork unitOfWork)
        {
            this.EthicalVoilationRepository = EthicalVoilationRepository;
            this.unitOfWork = unitOfWork;
        }

        #region Methods
        public IEnumerable<EthicalViolation> GetEthicalviolations()
        {
            return EthicalVoilationRepository.GetAll().OrderBy(x=>x.Name);
        }

        public EthicalViolation GetEthicalVoilationByID(int ID)
        {
            return EthicalVoilationRepository.GetByID(ID);
        }

        public IEnumerable<EthicalViolation> GetEthicalviolations(Expression<Func<EthicalViolation, bool>> where)
        {
            return EthicalVoilationRepository.GetMany(where);
        }

        public void CreateEthicalVoilation(EthicalViolation voilation)
        {
            EthicalVoilationRepository.Add(voilation);
        }

        public void UpdateEthicalVoilation(EthicalViolation voilation)
        {
            EthicalVoilationRepository.Update(voilation);
        }

        public void Delete(int ID)
        {
            var data = EthicalVoilationRepository.GetByID(ID);
            EthicalVoilationRepository.Delete(data);

        }

     

        public void Save()
        {
            unitOfWork.Commit();
        }


        #endregion  
    }

}
