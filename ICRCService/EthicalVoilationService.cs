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
        IEnumerable<EthicalVoilation> GetEthicalVoilations();
        EthicalVoilation GetEthicalVoilationByID(int ID);
        IEnumerable<EthicalVoilation> GetEthicalVoilations(Expression<Func<EthicalVoilation, bool>> where);       
        void CreateEthicalVoilation(EthicalVoilation Board);
        void UpdateEthicalVoilation(EthicalVoilation Board);
        void Save();
        void Delete(int ID);
    }
    public class EthicalVoilationService : IEthicalVoliationService
    {
        public readonly IEthicalVoilationsRepository EthicalVoilationRepository;
        public readonly IUnitOfWork unitOfWork;

        public EthicalVoilationService(IEthicalVoilationsRepository EthicalVoilationRepository, IUnitOfWork unitOfWork)
        {
            this.EthicalVoilationRepository = EthicalVoilationRepository;
            this.unitOfWork = unitOfWork;
        }

        #region Methods
        public IEnumerable<EthicalVoilation> GetEthicalVoilations()
        {
            return EthicalVoilationRepository.GetAll().OrderBy(x=>x.Name);
        }

        public EthicalVoilation GetEthicalVoilationByID(int ID)
        {
            return EthicalVoilationRepository.GetByID(ID);
        }

        public IEnumerable<EthicalVoilation> GetEthicalVoilations(Expression<Func<EthicalVoilation, bool>> where)
        {
            return EthicalVoilationRepository.GetMany(where);
        }

        public void CreateEthicalVoilation(EthicalVoilation voilation)
        {
            EthicalVoilationRepository.Add(voilation);
        }

        public void UpdateEthicalVoilation(EthicalVoilation voilation)
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
