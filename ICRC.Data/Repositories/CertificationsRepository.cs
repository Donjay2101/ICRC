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
    public class CertificationsRepository : RepositoryBase<Certifications>, ICertificationsRepository
    {

        public CertificationsRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public override IEnumerable<Certifications> GetAll()
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
            var data = DbContext.Certifications.Where(x => x.certificateNo == Number).ToList();
            if (data.Count > 0)
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
                    AddToPrintQueues = x.AddToPrintQueues,
                    BoardCertificateAcronym = x.BoardCertificateAcronym,
                    BoardCertificateAcronymName = x.BoardCertificateAcronymName,
                    BoardCertificateNumber = x.BoardCertificateNumber,
                    CertID = x.CertID,
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
                }).FirstOrDefault();
        }


        public void UploadCSV(string filePath)
        {
            DataTable dt = MakeTable<Certifications>();
            Boards board;
            CertifiedPersons person;
            string firstName, middlename, lastname, address, email, city, provience, Zip, country, certificateNumber,
                boardCertificateNumber, CertificateIssueDate, RenewalDate, CertificaterequestDate, certificateRequestFee,
                paymentNumber, CertificateNotes,boardName,otherboardName,certificateName,issueBoardName,boardcertificateName,paymentTypeName;
            int personID=0, certifcateID=0, otherboard=0, issueBoard=0, paymentType=0, boardCertificateAcronym=0, count=0;
           
            string CSVData = File.ReadAllText(filePath);
            string[] cells;
            count = 0;         
                foreach (string row in CSVData.Split('\n'))
                {
                    if (count != 0)
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                       
                                dt.Rows.Add();
                                int i = 0;

                                cells = row.Split(',');
                                //board = DbContext.Boards.FirstOrDefault(x => x.Acronym == "ADAD");
                                boardName = cells[9];
                                board = DbContext.Boards.FirstOrDefault(x => x.Acronym == boardName);
                                firstName = cells[0];
                                middlename = cells[1];
                                lastname = cells[2];
                                address = cells[3];
                                city = cells[4];
                                provience = cells[5];
                                country = cells[6];
                                Zip = cells[7];
                                email = cells[8];
                                otherboardName = cells[10];

                                var data = DbContext.Boards.FirstOrDefault(x => x.Acronym == otherboardName);
                                if (data != null)
                                {
                                    otherboard = data.ID;
                                }

                                certificateName = cells[11];
                                var certificate = DbContext.Boards.FirstOrDefault(x => x.Acronym == certificateName);
                                if (certificate != null)
                                {
                                    certifcateID = certificate.ID;
                                }

                                certificateNumber = cells[12];
                                issueBoardName = cells[13];
                                var issueb = DbContext.Boards.FirstOrDefault(x => x.Acronym == issueBoardName);
                                if (issueb != null)
                                {
                                    issueBoard = issueb.ID;
                                }

                                boardCertificateNumber = cells[14];
                                boardcertificateName = cells[15];
                                var BCN = DbContext.Boards.FirstOrDefault(x => x.Acronym == boardcertificateName);
                                if (BCN != null)
                                {
                                    boardCertificateAcronym = BCN.ID;
                                }

                                CertificateIssueDate = cells[16];
                                RenewalDate = cells[17];
                                CertificaterequestDate = cells[18];
                                certificateRequestFee = cells[19];
                                paymentNumber = cells[20];
                                paymentTypeName = cells[21];
                                var paytpe = DbContext.PaymentTypes.FirstOrDefault(x => x.Name == paymentTypeName);
                                if (paytpe != null)
                                {
                                    paymentType = paytpe.ID;
                                }

                                CertificateNotes = cells[22];
                                personID = SearchPerson(firstName, middlename, lastname, board.ID, email, address);

                                dt.Rows[dt.Rows.Count - 1]["CertID"] = certifcateID;
                                dt.Rows[dt.Rows.Count - 1]["CertificateNo"] = certificateNumber;
                                dt.Rows[dt.Rows.Count - 1]["IssueBoard"] = issueBoard;
                                dt.Rows[dt.Rows.Count - 1]["BoardCertificateNumber"] = boardCertificateNumber;

                                dt.Rows[dt.Rows.Count - 1]["CertIssueDate"] = CertificateIssueDate;
                                dt.Rows[dt.Rows.Count - 1]["RecertDate"] = RenewalDate;
                                dt.Rows[dt.Rows.Count - 1]["certRequestDate"] = CertificaterequestDate;
                                dt.Rows[dt.Rows.Count - 1]["CertRequestFee"] = certificateRequestFee;
                                dt.Rows[dt.Rows.Count - 1]["PaymentNumber"] = paymentNumber;
                                dt.Rows[dt.Rows.Count - 1]["PaymentType"] = paymentType;
                                dt.Rows[dt.Rows.Count - 1]["CertNotes"] = CertificateNotes;
                                dt.Rows[dt.Rows.Count - 1]["createdBy"] = -1;
                                dt.Rows[dt.Rows.Count - 1]["createdAt"] = DateTime.Now;
                                dt.Rows[dt.Rows.Count - 1]["ModifiedBy"] = -1;
                                dt.Rows[dt.Rows.Count - 1]["ModifiedAt"] = DateTime.Now;
                                dt.Rows[dt.Rows.Count - 1]["BoardCertificateAcronym"] = boardCertificateAcronym;
                                if (personID <= 0)
                                {
                                    person = new CertifiedPersons();
                                    person.FirstName = firstName;
                                    person.MiddleName = middlename;
                                    person.LastName = lastname;
                                    person.Address = address;
                                    person.City = city;
                                    person.province = provience;
                                    person.Country = country;
                                    person.Zip = Zip;
                                    person.Email = email;
                                    if (board!=null)
                                    {
                                        person.CurrentBoardID = board.ID;
                                    }
                                    person.OtherBoardID = otherboard;
                                    
                                    DbContext.CertifiedPersons.Add(person);
                                    DbContext.SaveChanges();
                                    //DataTable tblPerson = MakeTable<CertifiedPersons>(
                                    personID = person.ID;
                                }
                                dt.Rows[dt.Rows.Count - 1]["PersonID"] = personID;                        
                        }
                    }
                    count++;
                }
                string conn = ConfigurationManager.ConnectionStrings["IRCREntities"].ConnectionString;
                SqlBulkCopy bulkinsert = new SqlBulkCopy(conn);
                bulkinsert.DestinationTableName = "dbo.Certifications";
                bulkinsert.WriteToServer(dt);
                File.Delete(filePath);
            
            

            
        }
       
       


        DataTable MakeTable<T>()
        {
            int count;
            Type upload = typeof(T);
            DataTable dt = new DataTable();
            var properties = upload.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                System.Reflection.PropertyAttributes propAttributes = prop.Attributes;
                count = 0;
                //to not add notmapped columns
                prop.CustomAttributes.ToList().ForEach(x => { if (x.AttributeType.Name.ToUpper() == "NOTMAPPEDATTRIBUTE") count++; });
                //if(propAttributes.)
                if (count <= 0)
                {
                    dt.Columns.Add(new DataColumn(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType));
                }
            }
            return dt;
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
