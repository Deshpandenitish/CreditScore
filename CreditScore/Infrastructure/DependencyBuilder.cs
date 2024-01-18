using CreditLibrary.BL;
using CreditLibrary.DAL;
using CreditScore.BL;
using CreditScore.Models;
using CreditScore.Models.Interface;
using CreditScore.Repository;

namespace CreditScore.Infrastructure
{
    public static class DependencyBuilder
    {
        public static void BuildDependencies(IServiceCollection services)
        {
            BuildTransientDependencies(services);
        }
        public static void BuildTransientDependencies(IServiceCollection services) 
        {
            //Register dependency services here
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IBuisiness, UserBL>();
            services.AddScoped<IDocuments, Documents>();
            
            services.AddScoped<INotification, NotificationRepo>();
            services.AddScoped<IFinanceDetails, FinanceDetailLogic>();
            services.AddScoped<ICreditScoreVal, CreditScoreLogic>();
            services.AddScoped<IAudit, AuditLogic>();
        }
    }
}
