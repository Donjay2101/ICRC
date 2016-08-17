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
    public interface IStudentEthicalViolationService
    {
        IEnumerable<Studentviolations> GetEthicalviolations();
        Studentviolations GetEthicalVoilationByID(int ID);
        IEnumerable<Studentviolations> GetEthicalviolations(Expression<Func<Studentviolations, bool>> where);
        IEnumerable<Studentviolations> GetVoiltaionsByPersonID(int ID);
        void CreateEthicalVoilation(Studentviolations Board);
        void UpdateEthicalVoilation(Studentviolations Board);
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
        public IEnumerable<Studentviolations> GetEthicalviolations()
        {
            return StudentEthicalViolationRepository.GetAll().OrderBy(x=>x.Date);
        }

        public Studentviolations GetEthicalVoilationByID(int ID)
        {
            return StudentEthicalViolationRepository.GetByID(ID);
        }

        public IEnumerable<Studentviolations> GetEthicalviolations(Expression<Func<Studentviolations, bool>>where)
        {
            return StudentEthicalViolationRepository.GetMany(where);
        }

        public void CreateEthicalVoilation(Studentviolations voilation)
        {
            StudentEthicalViolationRepository.Add(voilation);
        }

        public void UpdateEthicalVoilation(Studentviolations voilation)
        {
            StudentEthicalViolationRepository.Update(voilation);
        }

        public IEnumerable<Studentviolations> GetVoiltaionsByPersonID(int ID)
        {
            return StudentEthicalViolationRepository.GetByPersonID(ID);
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
