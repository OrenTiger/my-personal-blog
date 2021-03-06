﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using MyPersonalBlog.Repositories;

namespace MyPersonalBlog.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
            _kernel.Bind<IAdminRepository>().To<AdminRepository>();
            _kernel.Bind<IPostRepository>().To<PostRepository>();
            _kernel.Bind<ICommentRepository>().To<CommentRepository>();
            _kernel.Bind<ITagRepository>().To<TagRepository>();
            _kernel.Bind<ISettingsRepository>().To<SettingsRepository>();
            _kernel.Bind<ISettingsProvider>().To<DBSettingsProvider>().InSingletonScope();
            _kernel.Bind<IHashingProvider>().To<HashingProvider>();
            _kernel.Bind<ILikeRepository>().To<LikeRepository>();
        }
    }
}