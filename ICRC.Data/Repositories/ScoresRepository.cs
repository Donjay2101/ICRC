using ICRC.Data.Infrastructure;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{
    public class ScoresRepository : RepositoryBase<Scores>,IScoresRepository
    {
        public ScoresRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public override IEnumerable<Scores> GetAll()
        {

            var data=(from score in DbContext.Scores join 
                     person in DbContext.CertifiedPersons on score.PersonID equals person.ID
                     join company in DbContext.TestingCompanies on score.TestingCompany equals company.ID
                     join board in DbContext.Boards on score.Board equals board.ID
                     join cert in DbContext.Certificates on score.ExamID equals cert.ID
                     select new 
                     {
                         Board=score.Board,
                         BoardName=board.Acronym,
                         CompanyName=company.Name,
                         CreatedAt=score.CreatedAt,
                         CreatedBy=score.CreatedBy,
                         ExamDate=score.ExamDate,
                         ExamID=score.ExamID,
                         ExamName=cert.Name,
                         ID=score.ID,
                         ModifiedAt=score.ModifiedAt,
                         ModifiedBy=score.ModifiedBy,
                         PersonID=score.PersonID,
                         PersonName=person.FirstName+" "+person.MiddleName+" "+person.LastName,
                         ScaledScore=score.ScaledScore,
                         Status=score.Status,
                         TestingCompany=score.TestingCompany,
                     }).ToList().Select(x=>new Scores {

                         Board = x.Board,
                         BoardName = x.BoardName,
                         CompanyName = x.CompanyName,
                         CreatedAt = x.CreatedAt,
                         CreatedBy = x.CreatedBy,
                         ExamDate = x.ExamDate,
                         ExamID = x.ExamID,
                         ExamName = x.ExamName,
                         ID = x.ID,
                         ModifiedAt = x.ModifiedAt,
                         ModifiedBy = x.ModifiedBy,
                         PersonID = x.PersonID,
                         PersonName = x.PersonName,
                         ScaledScore = x.ScaledScore,
                         Status = x.Status,
                         TestingCompany = x.TestingCompany,
                     });


            return data;
            
        }


        public IEnumerable<Scores> ScoresGetByPersonID(int PersonID)
        {
            var data=(from score in DbContext.Scores where score.PersonID== PersonID
                      join person in DbContext.CertifiedPersons on score.PersonID equals person.ID
             join company in DbContext.TestingCompanies on score.TestingCompany equals company.ID
             join board in DbContext.Boards on score.Board equals board.ID
             join cert in DbContext.Certificates on score.ExamID equals cert.ID
             select new
             {
                 Board = score.Board,
                 BoardName = board.Acronym,
                 CompanyName = company.Name,
                 CreatedAt = score.CreatedAt,
                 CreatedBy = score.CreatedBy,
                 ExamDate = score.ExamDate,
                 ExamID = score.ExamID,
                 ExamName = cert.Name,
                 ID = score.ID,
                 ModifiedAt = score.ModifiedAt,
                 ModifiedBy = score.ModifiedBy,
                 PersonID = score.PersonID,
                 PersonName = person.FirstName + " " + person.MiddleName + " " + person.LastName,
                 ScaledScore = score.ScaledScore,
                 Status = score.Status,
                 TestingCompany = score.TestingCompany,
             }).ToList().Select(x => new Scores
             {

                 Board = x.Board,
                 BoardName = x.BoardName,
                 CompanyName = x.CompanyName,
                 CreatedAt = x.CreatedAt,
                 CreatedBy = x.CreatedBy,
                 ExamDate = x.ExamDate,
                 ExamID = x.ExamID,
                 ExamName = x.ExamName,
                 ID = x.ID,
                 ModifiedAt = x.ModifiedAt,
                 ModifiedBy = x.ModifiedBy,
                 PersonID = x.PersonID,
                 PersonName = x.PersonName,
                 ScaledScore = x.ScaledScore,
                 Status = x.Status,
                 TestingCompany = x.TestingCompany,
             });
            return data;
        }
    }


    public interface IScoresRepository : IRepository<Scores>
    {
        IEnumerable<Scores> ScoresGetByPersonID(int ID);
            

    }
}
