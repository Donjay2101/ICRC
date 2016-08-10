using ICRC.Data.Infrastructure;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{
    public class StudentEthicalVoilationRepository:RepositoryBase<StudentVoilations>, IStudentEthicalVoilationRepository
    {

        public StudentEthicalVoilationRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }

        public override IEnumerable<StudentVoilations> GetAll()
        {
            var data=(from voi in DbContext.StudentVoilations join 
                     bc in DbContext.Boards  on voi.Board equals bc.ID
                     join person in DbContext.CertifiedPersons on voi.personID equals person.ID
                     join ethi in DbContext.EthicalVoilations on voi.EthicalVoilationId equals ethi.ID
                     select new
                     {
                         Board=voi.Board,
                         BoardName=bc.Acronym,
                         Comments=voi.Comments,
                         CreatedAt=voi.CreatedAt,
                         CreatedBy=voi.CreatedBy,
                         Date=voi.Date,
                         EthicalVoilationId=voi.EthicalVoilationId,
                         ID=voi.ID,
                         IsLetterSent=voi.IsLetterSent,
                         IsScanned=voi.IsScanned,
                         ISsharable=voi.ISsharable,
                         ModifiedAt=voi.ModifiedAt,
                         ModifiedBy=voi.ModifiedBy,
                         personID=voi.personID,
                         PersonName=person.FirstName+" "+person.MiddleName+" "+person.LastName,
                         EthicalVoilation=ethi.Name,
                         Notes=voi.Notes
                     }).ToList().Select(x=>new StudentVoilations {

                         Board = x.Board,
                         BoardName = x.BoardName,
                         Comments = x.Comments,
                         CreatedAt = x.CreatedAt,
                         CreatedBy = x.CreatedBy,
                         Date = x.Date,
                         EthicalVoilationId=x.EthicalVoilationId,                         
                         ID = x.ID,
                         IsLetterSent = x.IsLetterSent,
                         IsScanned = x.IsScanned,
                         ISsharable = x.ISsharable,
                         ModifiedAt = x.ModifiedAt,
                         ModifiedBy = x.ModifiedBy,
                         personID = x.personID,
                         PersonName = x.PersonName,
                         EthicalVoilation=x.EthicalVoilation,
                         Notes=x.Notes                         
                     }).ToList();

            return data;
        }

        public IEnumerable<StudentVoilations> GetByPersonID(int ID)
        {
            return DbContext.StudentVoilations.Where(x => x.personID == ID);
        }

    }

    public interface IStudentEthicalVoilationRepository:IRepository<StudentVoilations>
    {
        IEnumerable<StudentVoilations> GetByPersonID(int ID);
    }
}
