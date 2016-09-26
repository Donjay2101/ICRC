using IC_RC.ViewModels;
using ICRC.Data.Infrastructure;
using ICRC.Data.Repositories;
using ICRC.Model;
using ICRC.Model.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ICRCService
{
    public interface ICertificationService
    {
        IEnumerable<Certifications> GetCertifications();
        IEnumerable<Certifications> GetCertificationsForIndex();
        IEnumerable<Certifications> GetCertificationsByBoardID(int ID);
        Certifications GetCertificationByID(int ID);
        IEnumerable<Certifications> QueueForPrint();
        ReportViewModel QueueToPrintByCertificationID(int ID);
        IEnumerable<Certifications> GetCertifications(Expression<Func<Certifications, bool>> where);
        IEnumerable<Certifications> GetCertificationsByPersonID(int ID);
        void CreateCertification(Certifications Board);
        void UpdateCertification(Certifications Board);
        void Save();
        void ClearQueue(string ids);
        void GenerateCertificate(List<int> certifications,string path);
        void UploadCSV(string filePath);
        bool CheckNumber(int number);
        void Delete(int ID);

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

        public void ClearQueue(string ids)
        {
            certificationRepository.ClearQueue(ids);
        }



        public void GenerateCertificate(List<int> certifications,string path)
        {
            string Url;
            string folderName = Path.Combine(path, "Certifications");
            if (Directory.Exists(folderName))
            {
                DirectoryInfo dir=new DirectoryInfo (folderName);

                FileInfo[] files = dir.GetFiles();
                foreach(var file in files)
                {
                    file.Delete();
                }
                Directory.Delete(folderName);
            }
            foreach (var item in certifications)
            {               
                Directory.CreateDirectory(folderName);
                Url = "http://localhost:65147/Certifications/PrintCertificate?id=" + item;
                unitofwork.PrintPDF(Url, path,item.ToString());                    
            }
        }

        public IEnumerable<Certifications> QueueForPrint()
        {
            
            return GetCertificationsForIndex().Where(x => x.AddToPrintQueues == true);
        }

        public ReportViewModel QueueToPrintByCertificationID(int ID)
        {
            return certificationRepository.GetReportDataByCertificationID(ID);

        }

        public IEnumerable<Certifications> GetCertificationsByBoardID(int ID)
        {
            return GetCertificationsForIndex().Where(x => x.IssueBoard == ID);
        }

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
            return certificationRepository.GetCertificationsByID(ID);
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

        public void Delete(int ID)
        {
            var data = certificationRepository.GetByID(ID);
            certificationRepository.Delete(data);
        }

        public void UploadCSV(string filePath)
        {
            certificationRepository.UploadCSV(filePath);
        }

        public void Save()
        {
            unitofwork.Commit();
        }



        #endregion

    }
}
