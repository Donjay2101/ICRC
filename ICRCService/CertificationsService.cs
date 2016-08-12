using IC_RC.ViewModels;
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
        IEnumerable<Certifications> GetCertificationsForIndex();
        Certifications GetCertificationByID(int ID);
        IEnumerable<Certifications> GetCertifications(Expression<Func<Certifications, bool>> where);
        IEnumerable<Certifications> GetCertificationsByPersonID(int ID);
        void CreateCertification(Certifications Board);
        void UpdateCertification(Certifications Board);
        void Save();
        bool CheckNumber(int number);
    }

    public class CertificationsService:ICertificationService
    {
        private readonly ICertificationsRepository certificationRepository;
        private readonly  ICertificatesRepository  certificatesRepository;
        private readonly  ICertifiedPersonRepository certifiedPersonRepository;
        private readonly IUnitOfWork unitofwork;


        public CertificationsService(ICertificationsRepository certificationRepository, ICertificatesRepository certificatesRepository, ICertifiedPersonRepository certifiedPersonRepository, IUnitOfWork unitofwork)
        {
            this.certificationRepository = certificationRepository;
            this.certificatesRepository = certificatesRepository;
            this.certifiedPersonRepository = certifiedPersonRepository;
            this.unitofwork = unitofwork;
        }

        #region Methods

        public bool CheckNumber(int number)
        {
            return certificationRepository.CheckNumber(number);
        }

        public IEnumerable<Certifications> GetCertificationsForIndex()
        {
            var certifications = certificationRepository.GetAll().OrderBy(x=>x.CertID);            
            return certifications;
        }

        public IEnumerable<Certifications> GetCertifications()
        {        
            return certificationRepository.GetAll(); ;
        }

        public Certifications GetCertificationByID(int ID)
        {
            return certificationRepository.GetByID(ID);
        }

        public  IEnumerable<Certifications> GetCertifications(Expression<Func<Certifications, bool>>where)
        {
            return certificationRepository.GetMany(where);
        }

        public void CreateCertification(Certifications certification )
        {
            certification.CreatedAt = DateTime.Now;
            //int ID;
            //int.TryParse(System.Web.HttpContext.Current.Session["User"].ToString(), out ID);
            //certification.CreatedBy=ID;
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
