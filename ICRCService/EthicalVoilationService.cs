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
    public interface IEthicalVoliationService
    {
        IEnumerable<EthicalVoilations> GetEthicalVoilations();
        EthicalVoilations GetEthicalVoilationByID(int ID);
        IEnumerable<EthicalVoilations> GetEthicalVoilations(Expression<Func<EthicalVoilations, bool>> where);
        IEnumerable<EthicalVoilations> GetVoiltaionsByPersonID(int ID);
        void CreateEthicalVoilation(EthicalVoilations Board);
        void UpdateEthicalVoilation(EthicalVoilations Board);
        void Save();
    }
    public class EthicalVoilationService:IEthicalVoliationService
    {
        public readonly IEthicalVoilationRepository ethicalVoilationRepository;
        public readonly IUnitOfWork unitOfWork;

        public EthicalVoilationService(IEthicalVoilationRepository ethicalVoilationRepository, IUnitOfWork unitOfWork)
        {
            this.ethicalVoilationRepository = ethicalVoilationRepository;
            this.unitOfWork = unitOfWork;
        }

        #region Methods
        public IEnumerable<EthicalVoilations> GetEthicalVoilations()
        {
            return ethicalVoilationRepository.GetAll();
        }

        public EthicalVoilations GetEthicalVoilationByID(int ID)
        {
            return ethicalVoilationRepository.GetByID(ID);
        }

        public IEnumerable<EthicalVoilations> GetEthicalVoilations(Expression<Func<EthicalVoilations,bool>>where)
        {
            return ethicalVoilationRepository.GetMany(where);
        }

        public void CreateEthicalVoilation(EthicalVoilations voilation)
        {
            ethicalVoilationRepository.Add(voilation);
        }

        public void UpdateEthicalVoilation(EthicalVoilations voilation)
        {
            ethicalVoilationRepository.Update(voilation);
        }

        public IEnumerable<EthicalVoilations> GetVoiltaionsByPersonID(int ID)
        {
            return ethicalVoilationRepository.GetByPersonID(ID);
        }

        public void Save()           
        {
            unitOfWork.Commit();
        }


        #endregion  
    }   
}
