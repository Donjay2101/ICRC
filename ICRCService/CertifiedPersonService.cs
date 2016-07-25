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
    public interface ICertifiedPersonService 
    {

        IEnumerable<CertifiedPersons> GetCertifiedPersons();
        CertifiedPersons GetCertifiedPersonByID(int ID);
        IEnumerable<CertifiedPersons> GetCertifiedPersons(Expression<Func<CertifiedPersons, bool>> where);
        void CreateCertifiedPerson(CertifiedPersons Board);
        void UpdateCertifiedPerson(CertifiedPersons Board);
        void Save();

    }

    public class CertifiedPersonService:ICertifiedPersonService
    {
        public readonly ICertifiedPersonRepository certifiedRepository;
        public readonly IUnitOfWork unitOfWork;

        public CertifiedPersonService(ICertifiedPersonRepository certifiedRepository,IUnitOfWork unitOfWork  )
        {
            this.certifiedRepository = certifiedRepository;
            this.unitOfWork = unitOfWork;
        }

        #region Methods
        

        public IEnumerable<CertifiedPersons> GetCertifiedPersons()
        {
            return certifiedRepository.GetAll();
        }

        public CertifiedPersons GetCertifiedPersonByID (int ID)
        {
            return certifiedRepository.GetByID(ID);
        }

        public  IEnumerable<CertifiedPersons> GetCertifiedPersons(Expression<Func<CertifiedPersons,bool>>where)
        {
            return certifiedRepository.GetMany(where);
        }

        public void CreateCertifiedPerson(CertifiedPersons person)
        {
            certifiedRepository.Add(person);
        }

        public void UpdateCertifiedPerson(CertifiedPersons person)
        {
            certifiedRepository.Update(person);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        #endregion


    }


}


