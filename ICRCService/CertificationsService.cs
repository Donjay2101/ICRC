using ICRC.Data.Infrastructure;
using ICRC.Data.Repositories;
using ICRC.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ICRCService
{
    public interface ICertificationService
    {
        IEnumerable<Certifications> GetCertifications();
        Certifications GetCertificationByID(int ID);
        IEnumerable<Certifications> GetCertifications(Expression<Func<Certifications, bool>> where);
        IEnumerable<Certifications> GetCertificationsByPersonID(int ID);
        void CreateCertification(Certifications Board);
        void UpdateCertification(Certifications Board);
        void Save();

    }

    public class CertificationsService:ICertificationService
    {
        private readonly ICertificationsRepository certificationRepository;
        private readonly IUnitOfWork unitofwork;


        public CertificationsService(ICertificationsRepository certificationRepository, IUnitOfWork unitofwork)
        {
            this.certificationRepository = certificationRepository;
            this.unitofwork = unitofwork;
        }

        #region Methods

        public IEnumerable<Certifications> GetCertifications()
        {
            return certificationRepository.GetAll();
        }

        public Certifications GetCertificationByID(int ID)
        {
            return certificationRepository.GetByID(ID);
        }

        public  IEnumerable<Certifications> GetCertifications(Expression<Func<Certifications,bool>>where)
        {
            return certificationRepository.GetMany(where);
        }

        public void CreateCertification(Certifications certification )
        {
            certificationRepository.Add(certification);
        }

        public void UpdateCertification(Certifications certification)
        {
            certificationRepository.Update(certification);
        }

        public IEnumerable<Certifications> GetCertificationsByPersonID(int ID)
        {
            return certificationRepository.GetCertificationsByPersonID(ID);
        }

        public void Save()
        {
            unitofwork.Commit();
        }



        #endregion

    }
}
