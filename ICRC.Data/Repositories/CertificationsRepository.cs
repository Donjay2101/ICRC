using IC_RC.ViewModels;
using ICRC.Data.Infrastructure;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{
    public class CertificationsRepository:RepositoryBase<Certifications>,ICertificationsRepository
    {

        public CertificationsRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
        public override  IEnumerable<Certifications> GetAll()
        {

             var data = DbContext.Database.SqlQuery<CertificationViewModel>("exec SP_GetCertifications").ToList().Select(x => new Certifications
             {
                 AddToPrintQueues = x.AddToPrintQueues,
                 BoardCertificateAcronym = x.BoardCertificateAcronym,
                 CertID = x.CertID,
                 BoardCertificateNumber = x.BoardCertificateNumber,
                 BoardCertificateAcronymName = x.BoardCertificateAcronymName,
                 CertificateAcronym = x.CertificateAcronym,
                 certificateNo = x.certificateNo,
                 CertIssueDate = x.CertIssueDate,
                 CertNotes = x.CertNotes,
                 CertRequestDate = x.CertRequestDate,
                 CertRequestFee = x.CertRequestFee,
                 CreatedAt = x.CreatedAt,
                 CreatedBy = x.CreatedBy,
                 ID = x.ID,
                 IssueBoard = x.IssueBoard,
                 IssueBoardAcronym = x.IssueBoardAcronym,
                 ModifiedAt = x.ModifiedAt,
                 ModifiedBy = x.ModifiedBy,
                 PaymentNumber = x.PaymentNumber,
                 PaymentType = x.PaymentType,
                 PersonID = x.PersonID,
                 PersonName = x.PersonName,
                 RecertDate = x.RecertDate
             }).ToList();
            //var data = (from c in DbContext.Certificates
            //            join cr in DbContext.Certifications on c.ID equals cr.CertID
            //            into t
            //            from rt in t.DefaultIfEmpty()
            //            join bc in DbContext.Boards on rt.BoardCertificateAcronym equals bc.ID
            //            into t1
            //            from rt1 in t1.DefaultIfEmpty()
            //            join ib in DbContext.Boards on rt.IssueBoard equals ib.ID
            //            into t2
            //            from rt2 in t2.DefaultIfEmpty()
            //            join cp in DbContext.CertifiedPersons on rt.PersonID equals cp.ID                      
            //            select new
            //            {
            //                AddToPrintQueues = (bool?)rt.AddToPrintQueues,
            //                BoardCertificateAcronym = (int?)rt.BoardCertificateAcronym,
            //                CertID = (int?)rt.CertID,
            //                BoardCertificateNumber = rt.BoardCertificateNumber,
            //                BoardCertificateAcronymName = rt1.Acronym,
            //                CertificateAcronym = c.Name,
            //                certificateNo = rt.certificateNo,
            //                CertIssueDate = rt.CertIssueDate,
            //                CertNotes = rt.CertNotes,
            //                CertRequestDate = rt.CertRequestDate,
            //                CertRequestFee = rt.CertRequestFee,
            //                CreatedAt = rt.CreatedAt,
            //                CreatedBy = (int?)rt.CreatedBy,
            //                ID = rt.ID,
            //                IssueBoard = (int?)rt.IssueBoard,
            //                IssueBoardAcronym = rt2.Acronym,
            //                ModifiedAt = rt.ModifiedAt,
            //                ModifiedBy = rt.ModifiedBy,
            //                PaymentNumber = rt.PaymentNumber,
            //                PaymentType = rt.PaymentType,
            //                PersonID = (int?)rt.PersonID,
            //                PersonName = cp.FirstName + " " + cp.MiddleName + " " + cp.LastName,
            //                RecertDate = rt.RecertDate
            //            }).ToList().Select(x => new Certifications
            //            {
            //                AddToPrintQueues = x.AddToPrintQueues,
            //                BoardCertificateAcronym = x.BoardCertificateAcronym,
            //                CertID = x.CertID,
            //                BoardCertificateNumber = x.BoardCertificateNumber,
            //                BoardCertificateAcronymName = x.BoardCertificateAcronymName,
            //                CertificateAcronym = x.CertificateAcronym,
            //                certificateNo = x.certificateNo,
            //                CertIssueDate = x.CertIssueDate,
            //                CertNotes = x.CertNotes,
            //                CertRequestDate = x.CertRequestDate,
            //                CertRequestFee = x.CertRequestFee,
            //                CreatedAt = x.CreatedAt,
            //                CreatedBy = x.CreatedBy,
            //                ID = x.ID,
            //                IssueBoard = x.IssueBoard ?? x.IssueBoard.Value,
            //                IssueBoardAcronym = x.IssueBoardAcronym,
            //                ModifiedAt = x.ModifiedAt,
            //                ModifiedBy = x.ModifiedBy,
            //                PaymentNumber = x.PaymentNumber,
            //                PaymentType = x.PaymentType,
            //                PersonID = x.PersonID,
            //                PersonName = x.PersonName,
            //                RecertDate = x.RecertDate
            //            }).ToList();

            return data;
        }

        public IEnumerable<Certifications> GetCertificationsByPersonID(int ID)
        {
            
            return GetAll().Where(x => x.PersonID == ID);
        }

        public bool CheckNumber(int Number)
        {
            var data=DbContext.Certifications.Where(x => x.certificateNo == Number).ToList();
            if(data.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        public Certifications GetCertificationsByID(int ID)
        {
            return DbContext.Database.SqlQuery<CertificationViewModel>("exec SP_GetCertificationsByID @ID", new SqlParameter("@ID", ID)).ToList()
                .Select(x => new Certifications
            {
                AddToPrintQueues=x.AddToPrintQueues,
                BoardCertificateAcronym=x.BoardCertificateAcronym,
                BoardCertificateAcronymName=x.BoardCertificateAcronymName,
                BoardCertificateNumber=x.BoardCertificateNumber,
                CertID=x.CertID,
                CertificateAcronym=x.CertificateAcronym,
                certificateNo=x.certificateNo,
                CertIssueDate=x.CertIssueDate,
                CertNotes=x.CertNotes,
                CertRequestDate=x.CertRequestDate,
                CertRequestFee=x.CertRequestFee,
                CreatedAt=x.CreatedAt,
                CreatedBy=x.CreatedBy,
                ID=x.ID,
                IssueBoard=x.IssueBoard,
                 IssueBoardAcronym=x.IssueBoardAcronym,
                 ModifiedAt=x.ModifiedAt,
                 ModifiedBy=x.ModifiedBy,
                 PaymentNumber=x.PaymentNumber,
                 PaymentType=x.PaymentType,
                 PersonID=x.PersonID,
                 PersonName=x.PersonName,
                 RecertDate=x.RecertDate
            }).FirstOrDefault();
        }


        public void UploadCSV(string filePath)
        {
            DataTable dt = new DataTable();
            Boards board;
            string firstName, middlename, lastname, address, email,city,Provience,Zip,country,certificateNumber,
                boardCertificateNumber,CertificateIssueDate,RenewalDate,CertificaterequestDate,certificateRequestFee,
                paymentNumber,CertificateNotes;
            int personID,certifcateID,otherboard,issueBoard,paymentType;
            Type upload = typeof(Certifications);

            var properties = upload.GetProperties();
            foreach(PropertyInfo prop in properties)
            {
                System.Reflection.PropertyAttributes propAttributes= prop.Attributes;
                int count = 0;
                //to not add notmapped columns
                prop.CustomAttributes.ToList().ForEach(x => { if (x.AttributeType.Name.ToUpper() == "NOTMAPPEDATTRIBUTE") count++; });
                //if(propAttributes.)
                if(count<=0)
                {
                    dt.Columns.Add(new DataColumn(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType));
                }

                string CSVData = File.ReadAllText(filePath);
                string []cells;
                foreach (string row in CSVData.Split('\n'))
                {
                    

                    if (count != 0)
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dt.Rows.Add();
                            int i = 0;

                            cells = row.Split(',');
                            
                             board=DbContext.Boards.FirstOrDefault(x => x.Acronym == cells[9]);
                            firstName = cells[0];
                            middlename= cells[1];
                            lastname= cells[2];
                            address = cells[3];
                            city = cells[4];
                            Provience = cells[5];
                            country = cells[6];
                            Zip = cells[7];                            
                            email = cells[8];
                            otherboard = DbContext.Boards.FirstOrDefault(x => x.Acronym == cells[10]).ID;
                            certifcateID = DbContext.Boards.FirstOrDefault(x => x.Acronym == cells[11]).ID;
                            certificateNumber = cells[12];
                            issueBoard = DbContext.Boards.FirstOrDefault(x => x.Acronym == cells[13]).ID;


                            personID =SearchPerson(firstName, middlename, lastname, board.ID, email, address);
                            if(personID>0)
                            {
                                       
                            }

                            //foreach (string cell in row.Split(','))
                            //{
                            //    board = cell[9];
                            //    dt.Rows[dt.Rows.Count - 1][i] = cell;
                            //    i++;
                            //}
                        }
                    }
                    count++;
                }

                string conn = ConfigurationManager.ConnectionStrings["IRCREntities"].ConnectionString;

                SqlBulkCopy bulkinsert = new SqlBulkCopy(conn);
                bulkinsert.DestinationTableName = "dbo.TestScores";
                bulkinsert.WriteToServer(dt);

            }
        }

        int SearchPerson(string firstName,string middleName,string lastName,int boardID,string email,string address)
        {
            var data=DbContext.CertifiedPersons.Where(x => (x.CurrentBoardID==boardID && x.FirstName == firstName && x.MiddleName == middleName && x.LastName == lastName) 
                            ||(x.CurrentBoardID==boardID && x.Email==email)).FirstOrDefault();

            if(data!=null)
            {
                return data.ID;
            }
            return -1;
        }

    }

    public interface ICertificationsRepository:IRepository<Certifications>
    {
        IEnumerable<Certifications> GetCertificationsByPersonID(int ID);
        bool CheckNumber(int number);
        Certifications GetCertificationsByID(int ID);

        void UploadCSV(string filePath);
        //IEnumerable<Certifications> GetAll();


    }
}
