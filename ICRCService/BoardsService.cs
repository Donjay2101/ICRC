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
    public interface IBoardService
    {
        IEnumerable<Boards> GetBoards();
        Boards GetBoardByID(int ID);        
        IEnumerable<Boards> GetBoard(Expression<Func<Boards,bool>> where);
        void CreateBoard(Boards Board);
        void UpdateBoard(Boards Board);
        void Save();        
    }

    public class BoardsService:IBoardService
    {
        private readonly IBoardRepository boardRepository;
        private readonly IUnitOfWork unitOfWork;

        public BoardsService(IBoardRepository boardRepository,IUnitOfWork unitOfWork)
        {
            this.boardRepository = boardRepository;
            this.unitOfWork = unitOfWork;
        }

        #region Methods

        public IEnumerable<Boards> GetBoards()
        {
            return boardRepository.GetAll().OrderBy(x=>x.Acronym);
        }

        public Boards GetBoardByID(int ID)
        {
            return boardRepository.GetByID(ID);
        }

        public void CreateBoard(Boards board)
        {
            boardRepository.Add(board);
        }

        public void UpdateBoard(Boards board)
        {
            boardRepository.Update(board);
        }

        public IEnumerable<Boards> GetBoard(Expression<Func<Boards,bool>> where)
        {
            return boardRepository.GetMany(where);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        #endregion

    }
}
