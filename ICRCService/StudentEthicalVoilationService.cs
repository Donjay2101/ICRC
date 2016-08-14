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
    public interface IStudentEthicalVoliationService
    {
        IEnumerable<StudentVoilations> GetEthicalVoilations();
        StudentVoilations GetEthicalVoilationByID(int ID);
        IEnumerable<StudentVoilations> GetEthicalVoilations(Expression<Func<StudentVoilations, bool>> where);
        IEnumerable<StudentVoilations> GetVoiltaionsByPersonID(int ID);
        void CreateEthicalVoilation(StudentVoilations Board);
        void UpdateEthicalVoilation(StudentVoilations Board);
        void Save();
        void Delete(int ID);
    }
    public class StudentEthicalVoilationService:IStudentEthicalVoliationService
    {
        public readonly IStudentEthicalVoilationRepository StudentEthicalVoilationRepository;
        public readonly IUnitOfWork unitOfWork;

        public StudentEthicalVoilationService(IStudentEthicalVoilationRepository StudentEthicalVoilationRepository, IUnitOfWork unitOfWork)
        {
            this.StudentEthicalVoilationRepository = StudentEthicalVoilationRepository;
            this.unitOfWork = unitOfWork;
        }

        #region Methods
        public IEnumerable<StudentVoilations> GetEthicalVoilations()
        {
            return StudentEthicalVoilationRepository.GetAll().OrderBy(x=>x.Date);
        }

        public StudentVoilations GetEthicalVoilationByID(int ID)
        {
            return StudentEthicalVoilationRepository.GetByID(ID);
        }

        public IEnumerable<StudentVoilations> GetEthicalVoilations(Expression<Func<StudentVoilations, bool>>where)
        {
            return StudentEthicalVoilationRepository.GetMany(where);
        }

        public void CreateEthicalVoilation(StudentVoilations voilation)
        {
            StudentEthicalVoilationRepository.Add(voilation);
        }

        public void UpdateEthicalVoilation(StudentVoilations voilation)
        {
            StudentEthicalVoilationRepository.Update(voilation);
        }

        public IEnumerable<StudentVoilations> GetVoiltaionsByPersonID(int ID)
        {
            return StudentEthicalVoilationRepository.GetByPersonID(ID);
        }

        public void Delete(int ID)
        {
            var data = StudentEthicalVoilationRepository.GetByID(ID);
            StudentEthicalVoilationRepository.Delete(data);
        }

        public void Save()           
        {
            unitOfWork.Commit();
        }


        #endregion  
    }   
}
