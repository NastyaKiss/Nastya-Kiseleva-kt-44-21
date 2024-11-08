using AnastasiaKiselevaKt_44_21.Interfaces.CoursesInterfaces;
using AnastasiaKiselevaKt_44_21.Interfaces.StudentsInterfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using static AnastasiaKiselevaKt_44_21.Interfaces.StudentsInterfaces.IStudentService;

namespace AnastasiaKiselevaKt_44_21.ServiceExtension
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICoursesService, CourseService>();

            return services;
        }
    }
}