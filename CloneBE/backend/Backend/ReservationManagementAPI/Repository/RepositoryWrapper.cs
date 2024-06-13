using ReservationManagementAPI.Contracts;
using Entities.Context;

namespace ReservationManagementAPI.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private FamsContext _repoContext;
        private IStudentRepository _student;
        private IClassRepository _class;
        private IModuleRepository _module;

        public IStudentRepository Student
        {
            get
            {
                if (_student == null)
                {
                    _student = new StudentRepository(_repoContext);
                }
                return _student;
            }
        }
        public IModuleRepository Module
        {
            get
            {
                if (_module == null)
                {
                    _module = new ModuleRepository(_repoContext);
                }
                return _module;
            }
        }

        public IClassRepository Class
        {
            get
            {
                if (_class == null)
                {
                    _class = new ClassRepository(_repoContext);
                }
                return _class;
            }
        }
        public RepositoryWrapper(FamsContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
