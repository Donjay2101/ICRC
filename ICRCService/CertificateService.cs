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
    public interface ICertificateService
    {
        IEnumerable<Certificates> GetCertificates();
        Certificates GetCertificateByID(int ID);
        IEnumerable<Certificates> GetCertificate(Expression<Func<Certificates, bool>> where);
        void CreateCertificate(Certificates cetificate);
        void UpdateCertificate(Certificates cetificate);
        void Save();
    }

    public class CertificateService:ICertificateService
    {

        private readonly ICertificatesRepository certificateRepository;
        private readonly IUnitOfWork unitOfWork;

        public CertificateService(ICertificatesRepository certificateRepository,IUnitOfWork unitOfWork)
        {
            this.certificateRepository = certificateRepository;
            this.unitOfWork = unitOfWork;
        }


        #region Methods
        
        public IEnumerable<Certificates> GetCertificates()
        {
            return certificateRepository.GetAll();
        }
        


        public Certificates GetCertificateByID(int ID)
        {
            return certificateRepository.GetByID(ID);
        }
        
        public IEnumerable<Certificates> GetCertificate(Expression<Func<Certificates,bool>>where)
        {
            return certificateRepository.GetMany(where);
        }

        public void CreateCertificate(Certificates certificate)
        {
            certificateRepository.Add(certificate);
        }

        public void UpdateCertificate(Certificates certificate)
        {
            certificateRepository.Update(certificate);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
        
        
        
        #endregion

    }
}
