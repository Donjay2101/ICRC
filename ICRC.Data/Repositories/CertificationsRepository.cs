using IC_RC.ViewModels;
using ICRC.Data.Infrastructure;
using ICRC.Model;
using ICRC.Model.ViewModel;
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

        public  IQueryable<Certifications> GetAllCertifications()
        {

            var data = DbContext.Database.SqlQuery<CertificationViewModel>("exec SP_GetCertifications").AsQueryable().Select(x => new Certifications
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
            }).AsQueryable();
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

        public IQueryable<CertificationViewModelForIndex> GetCertificationsByPersonID(int ID)
        {

            return GetCertificationsForIndex().Where(x => x.PersonID == ID).AsQueryable();
        }

        public bool CheckNumber(int Number)
        {
            var data = DbContext.Certifications.AsQueryable().Where(x => x.certificateNo == Number).ToList();
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
            return DbContext.Database.SqlQuery<CertificationViewModel>("exec SP_GetCertificationsByID @ID", new SqlParameter("@ID", ID)).AsQueryable()
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

        public IQueryable<CertificationViewModelForIndex> GetCertificationsForIndex(string certID = "", string certNumber = "", string person = "")
        {
            return DbContext.Database.SqlQuery<CertificationViewModelForIndex>("sp_GetCertificationsForIndex @certID,@certNumber,@person",
                new SqlParameter("@certID",certID),
                new SqlParameter("@certNumber", certNumber),
                new SqlParameter("@person", person)).AsQueryable();
        }
        public ReportViewModel GetReportDataByCertificationID(int ID)
        {
            return DbContext.Database.SqlQuery<ReportViewModel>("exec sp_ReportCertificationsByCertificationID @ID", new SqlParameter("@ID", ID)).FirstOrDefault();
        }

        public void UploadCSV(string filePath,Users loggedInUser)
        {
            DataTable dt = MakeTable<Certifications>();
            Boards board;
            CertifiedPersons person;
            string firstName, middlename, lastname, address, email, city, state, Zip, country,
                boardCertificateNumber, CertificateIssueDate, RenewalDate, CertificaterequestDate,certificateName,issueBoardName, boardCertificateAcronym;
            int personID=0, certifcateID=0, otherboard=0, issueBoard=0,count=0, certificateNumber=0;
           
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
                        if(cells.Where(x=>x=="").Count()!=17)
                        {
                            //board = DbContext.Boards.FirstOrDefault(x => x.Acronym == "ADAD");
                           // boardName = cells[9];

                            board = DbContext.Boards.FirstOrDefault(x => x.ID == loggedInUser.BoardID);
                            firstName = cells[0];
                            middlename = cells[1];
                            lastname = cells[2];
                            address = cells[3];
                            city = cells[4];
                            state = cells[5];
                            Zip = cells[6];
                            country = cells[7];                            
                            email = cells[8];

                            //CurrentboardName = cells[9];
                            //otherboardName = cells[10];

                            //var data = DbContext.Boards.FirstOrDefault(x => x.Acronym == CurrentboardName);
                            //if (data != null)
                            //{
                            //    currentBoard= data.ID;
                            //}

                            certificateName = cells[9];
                            var certificate = DbContext.Certificates.FirstOrDefault(x => x.Name== certificateName);
                            if (certificate != null)
                            {
                                certifcateID = certificate.ID;
                            }


                         //   int.TryParse(cells[10], out certificateNumber);
                            //cells[11];
                            issueBoardName = cells[10];
                            var issueb = DbContext.Boards.FirstOrDefault(x => x.Acronym == issueBoardName);
                            if (issueb != null)
                            {
                                issueBoard = issueb.ID;
                            }

                            boardCertificateNumber = cells[11];
                            boardCertificateAcronym = cells[12];
                            //var BCN = DbContext.Boards.FirstOrDefault(x => x.Acronym == boardcertificateName);
                            //if (BCN != null)
                            //{
                            //    boardCertificateAcronym = BCN.ID;
                            //}

                            CertificateIssueDate = cells[13];
                            RenewalDate = cells[14];
                            CertificaterequestDate = cells[15];
                            //certificateRequestFee = cells[17];
                            //paymentNumber = cells[18];
                            //paymentTypeName = cells[19];
                            //var paytpe = DbContext.PaymentTypes.FirstOrDefault(x => x.Name == paymentTypeName);
                            //if (paytpe != null)
                            //{
                            //    paymentType = paytpe.ID;
                            //}

                            //CertificateNotes = cells[20];
                            personID = SearchPerson(lastname, city, state, address, email, board==null?0:board.ID);
                            //int SearchPerson(string lastname, string city, string state, string address, string email, int boardID)
                            dt.Rows[dt.Rows.Count - 1]["CertID"] = certifcateID;
                            dt.Rows[dt.Rows.Count - 1]["CertificateNo"] = certificateNumber;
                            dt.Rows[dt.Rows.Count - 1]["IssueBoard"] = issueBoard;
                            dt.Rows[dt.Rows.Count - 1]["BoardCertificateNumber"] = boardCertificateNumber;

                            dt.Rows[dt.Rows.Count - 1]["CertIssueDate"] = CertificateIssueDate;
                            dt.Rows[dt.Rows.Count - 1]["RecertDate"] = RenewalDate;
                            dt.Rows[dt.Rows.Count - 1]["certRequestDate"] = CertificaterequestDate;
                            //dt.Rows[dt.Rows.Count - 1]["CertRequestFee"] = certificateRequestFee;
                           // dt.Rows[dt.Rows.Count - 1]["PaymentNumber"] = paymentNumber;
                           // dt.Rows[dt.Rows.Count - 1]["PaymentType"] = paymentType;
                          ///  dt.Rows[dt.Rows.Count - 1]["CertNotes"] = CertificateNotes;
                            dt.Rows[dt.Rows.Count - 1]["createdBy"] = -1;
                            dt.Rows[dt.Rows.Count - 1]["createdAt"] = DateTime.Now;
                            dt.Rows[dt.Rows.Count - 1]["ModifiedBy"] = -1;
                            dt.Rows[dt.Rows.Count - 1]["ModifiedAt"] = DateTime.Now;
                            dt.Rows[dt.Rows.Count - 1]["BoardCertificateAcronym"] = boardCertificateAcronym;
                            if (personID <= 0)
                            {
                                person = new CertifiedPersons();
                                person.Suffix = "";
                                person.FirstName = firstName;
                                person.MiddleName = middlename;
                                person.LastName = lastname;
                                person.Address = address;
                                person.City = city;
                                person.State = state;
                              //  person.province = provience;
                                person.Country = country;
                                person.Zip = Zip;
                                person.Email = email;
                                
                                if (board != null)
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

        //int SearchPerson(string firstName,string middleName,string lastName,int boardID,string email,string address)
        //{

        //    var data=DbContext.CertifiedPersons.Where(x => (x.CurrentBoardID==boardID && x.FirstName == firstName && x.MiddleName == middleName && x.LastName == lastName) 
        //                    ||(x.CurrentBoardID==boardID && x.Email==email)).FirstOrDefault();

        //    if(data!=null)
        //    {
        //        return data.ID;
        //    }
        //    return -1;
        //}
        int SearchPerson(string lastname,string city,string state,string address,string email,int boardID)
        {
            var data = DbContext.CertifiedPersons.Where(x => (x.CurrentBoardID == boardID && x.Email==email)
                              || (x.LastName==lastname && x.City==city && x.State==state &&x.Address==address && x.OtherBoardID==boardID)).FirstOrDefault();

            if (data != null)
            {
                return data.ID;
            }
            return -1;

        }

        public void ClearQueue(string ids)
        {
            DbContext.Database.ExecuteSqlCommand("exec SP_ClearCertificatePrintingQueue @ids", new SqlParameter("@ids",ids??(object)DBNull.Value));
        }

    }

    public interface ICertificationsRepository:IRepository<Certifications>
    {
        IQueryable<CertificationViewModelForIndex> GetCertificationsByPersonID(int ID);
        IQueryable<CertificationViewModelForIndex> GetCertificationsForIndex(string certID = "", string certNumber = "", string person = "");
        bool CheckNumber(int number);
        Certifications GetCertificationsByID(int ID);
        void ClearQueue(string ids);
        void UploadCSV(string filePath,Users LoggedInUser);

         IQueryable<Certifications> GetAllCertifications();
        ReportViewModel GetReportDataByCertificationID(int ID);
        //IEnumerable<Certifications> GetAll();


    }
}
