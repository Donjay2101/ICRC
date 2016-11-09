using ICRC.Data.Infrastructure;
using ICRC.Data.Repositories;
using ICRC.Model;
using ICRC.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ICRCService
{
    public interface IStudentEthicalViolationService
    {
        IEnumerable<Studentviolations> GetEthicalviolations();

        IQueryable<StudentVoilationForIndex> GetVoilationsForIndex(string board = "", string person = "", string violation = "");
        IQueryable<StudentVoilationForIndex> GetEthicalviolationsByBoardID(int ID, string board = "", string person = "", string violation = "");
        Studentviolations GetEthicalviolationByID(int ID);
        IEnumerable<Studentviolations> GetEthicalviolations(Expression<Func<Studentviolations, bool>> where);
        IEnumerable<StudentVoilationForIndex> GetVoiltaionsByPersonID(int ID);
        void CreateEthicalviolation(Studentviolations Board);
        void UpdateEthicalviolation(Studentviolations Board);
        void Save();
        void Delete(int ID);
    }
    public class StudentEthicalviolationService: IStudentEthicalViolationService
    {
        public readonly IStudentEthicalViolationRepository StudentEthicalViolationRepository;
        public readonly IUnitOfWork unitOfWork;




        public StudentEthicalviolationService(IStudentEthicalViolationRepository StudentEthicalViolationRepository, IUnitOfWork unitOfWork)
        {
            this.StudentEthicalViolationRepository = StudentEthicalViolationRepository;
            this.unitOfWork = unitOfWork;
        }

        #region Methods

        public IQueryable<StudentVoilationForIndex> GetVoilationsForIndex(string board = "", string person = "", string violation = "")
        {
            return StudentEthicalViolationRepository.GetVoilationForIndex(board, person, violation);
        }


        public IQueryable<StudentVoilationForIndex> GetEthicalviolationsByBoardID(int ID, string board = "", string person = "", string violation = "")
        {
            return GetVoilationsForIndex(board, person, violation).Where(x => x.Board == ID).AsQueryable();
        }


        public IEnumerable<Studentviolations> GetEthicalviolations()
        {
            return StudentEthicalViolationRepository.GetAll().OrderBy(x=>x.Date);
        }

        public Studentviolations GetEthicalviolationByID(int ID)
        {
            return StudentEthicalViolationRepository.GetByID(ID);
        }

        public IEnumerable<Studentviolations> GetEthicalviolations(Expression<Func<Studentviolations, bool>>where)
        {
            return StudentEthicalViolationRepository.GetMany(where);
        }

        public void CreateEthicalviolation(Studentviolations violation)
        {
            StudentEthicalViolationRepository.Add(violation);
        }

        public void UpdateEthicalviolation(Studentviolations violation)
        {
            StudentEthicalViolationRepository.Update(violation);
        }

        public IEnumerable<StudentVoilationForIndex> GetVoiltaionsByPersonID(int ID)
        {
            return StudentEthicalViolationRepository.GetByPersonID(ID).AsEnumerable();
        }

        public void Delete(int ID)
        {
            var data = StudentEthicalViolationRepository.GetByID(ID);
            StudentEthicalViolationRepository.Delete(data);
        }

        public void Save()           
        {
            unitOfWork.Commit();
        }


        #endregion  
    }   
}
