using System.Reflection;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Providers;
using CourseProject.Repositories;
using CourseProject.Services;
using Microsoft.Owin.Security.OAuth;
using Ninject;

namespace CourseProject
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices(kernel);

            return kernel;
        }

        private static void RegisterServices(KernelBase kernel)
        {
            kernel.Bind<IOAuthAuthorizationServerOptions>().To<MyOAuthAuthorizationServerOptions>();
            kernel.Bind<IOAuthAuthorizationServerProvider>().To<SimpleAuthorizationServerProvider>();
            kernel.Bind<IUnitOfWork>().To<EfUnitOfWork>();
            kernel.Bind<ICreativeService>().To<CreativeService>();
            kernel.Bind<IMedalService>().To<MedalService>();
            kernel.Bind<ITagsService>().To<TagsService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<ICommentsService>().To<CommentsService>();
            kernel.Bind<IChaptersService>().To<ChapterService>();
            kernel.Bind<ILikesService>().To<LikesService>();
            kernel.Bind<IRatingService>().To<RatingService>();
            kernel.Bind<ICategoriesService>().To<CategoriesService>();
            kernel.Bind<IAdminService>().To<AdminService>();
        }
    }
}
