﻿using ICRC.Data.Infrastructure;
using ICRC.Data.Repositories;
using ICRC.Model;
using IRCRC.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ICRCService
{

    public interface ITestScoreService
    {

        IEnumerable<TestScore> GetTestScores();
        TestScore GetTestScoresByID(int ID);
        IEnumerable<TestScore> GetTestScores(Expression<Func<TestScore, bool>> where);
        void CreateTestScore(TestScore Board);
        void UpdateTestScore(TestScore Board);
        IEnumerable<TestScoreViewModel> GetDistinctTestScores();
        IEnumerable<TestScoreViewModel> GetLastNames(int num);
        IEnumerable<TestScoreViewModel> GetTestScoreByPerson(string data);
        IEnumerable<TestScoreViewModel> GetFirstNames(string name);
        IEnumerable<TestScoreViewModel> GetDataByFirstAndLastName(TestScoreViewModel model);
        void UpdateScores(TestScore model);
        void Save();
        void Delete(int ID);

    }
    public  class TestScoreService:ITestScoreService
    {
        public readonly ITestScoreRepository testScoreRepository;
        public readonly IUnitOfWork unitOfWork;

        public TestScoreService(ITestScoreRepository testScoreRepository, IUnitOfWork unitOfWork)
        {
            this.testScoreRepository = testScoreRepository;
            this.unitOfWork = unitOfWork;
        }


        #region Methods


        public void UpdateScores(TestScore model)
        {
            testScoreRepository.UpdateScores(model);
        }

        public IEnumerable<TestScoreViewModel> GetDataByFirstAndLastName(TestScoreViewModel model)
        {
            return testScoreRepository.GetDataByFirstAndLastName(model);
        }
        public IEnumerable<TestScoreViewModel> GetFirstNames(string name)
        {
            return testScoreRepository.GetFirstNames(name);
        }


        public IEnumerable<TestScoreViewModel> GetLastNames(int num)
        {
            return testScoreRepository.GetLastNames(num);
        }

        public IEnumerable<TestScoreViewModel> GetDistinctTestScores()
        {
            var scores = testScoreRepository.GetDistinctTestScores();
            return scores;
        }

        public IEnumerable<TestScore> GetTestScores()
        {
            var Scores= testScoreRepository.GetAll();
            return Scores;
        }

        

        public TestScore GetTestScoresByID(int ID)
        {
            return testScoreRepository.GetByID(ID);
        }

        public IEnumerable<TestScore> GetTestScores(Expression<Func<TestScore, bool>> where)
        {
            return testScoreRepository.GetMany(where);
        }

        public void CreateTestScore(TestScore score)
        {
            //certification.CreatedAt = DateTime.Now;
            //int ID;
            //int.TryParse(System.Web.HttpContext.Current.Session["User"].ToString(), out ID);
            //certification.CreatedBy=ID;
            testScoreRepository.Add(score);
        }

        public void UpdateTestScore(TestScore score)
        {
            testScoreRepository.Update(score);
        }

        public IEnumerable<TestScoreViewModel> GetTestScoreByPerson(string name)
        {
            return testScoreRepository.GetTestScoreByPerson(name);
        }

        public void Delete(int ID)
        {
            var data = testScoreRepository.GetByID(ID);
            testScoreRepository.Delete(data);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }



        #endregion

    }
}
