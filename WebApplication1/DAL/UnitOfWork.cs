using System;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Dal
{
    public class UnitOfWork : IDisposable
    {
        private bool _disposed;
        private readonly context _context;

        public UnitOfWork(context c)
        {
            _context = c;
        }

        public UnitOfWork()
        {
            _context = new context();
        }

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        ///     打开/关闭懒惰加载
        /// </summary>
        /// <param name="open">true:打开懒惰加载，false:反之</param>
        public void SwitchLazyLoad(bool open)
        {
            _context.Configuration.LazyLoadingEnabled = open;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        #region repository
        private AdminRepository _adminRepository;

        public AdminRepository AdminRepository
        {
            get { return _adminRepository ?? (_adminRepository = new AdminRepository(_context)); }
        }
        
        private ArticleRepository _articleRepository;

        public ArticleRepository ArticleRepository
        {
            get { return _articleRepository ?? (_articleRepository = new ArticleRepository(_context)); }
        }

        private UserRepository _userRepository;

        public UserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }
        #endregion
    }
}