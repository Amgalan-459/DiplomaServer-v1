
using CourseProjectServer.Controllers;
using CourseProjectServer.Data.Context;
using CourseProjectServer.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CourseProjectServer {
    public class Program {
        public static async Task Main (string[] args) {       
            await CreateHostBuiler(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuiler (string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseKestrel();
                    webBuilder.UseUrls("http://localhost:5000/", "http://0.0.0.0:80/", "http://0.0.0.0:8080/");
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup {
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<EntetiesController>();
            services.AddDbContext<CourseDbContext>();

            services.AddCors(options =>
            {
                options.AddPolicy("DevCors", builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200") // твой Angular
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });

                options.AddPolicy("ProdCors", builder => {
                    builder
                        .WithOrigins("https://diploma-v1-nout.vercel.app", "http://localhost:4200") // домен продакшн фронтенда
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
        }

        public async void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            if (env.IsDevelopment()) {
                app.UseCors("DevCors");
            }
            else {
                app.UseCors("ProdCors");
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {

                #region MapGet
                endpoints.MapGet("/api/trainee/all", [Authorize] (EntetiesController controller) =>
                    controller.GetAllTrainees());
                endpoints.MapGet("/api/trainee/{id:int}", [Authorize] (EntetiesController controller, int id) =>
                    controller.GetTrainee(id));
                endpoints.MapGet("/api/trainee/{email}", (EntetiesController controller, string email) =>
                    controller.GetTraineeByEmail(email));
                endpoints.MapGet("/api/trainee/trainer/{id:int}", [Authorize] (EntetiesController controller, int id) =>
                    controller.GetTraineesByTrainerId(id));

                //потом смотрим от кого. Если от тренера или админа - пароль надо, а если нет, то нет
                endpoints.MapGet("/api/trainer/all", [Authorize] (EntetiesController controller) =>
                    controller.GetAllTrainers());
                endpoints.MapGet("/api/trainer/{id:int}", [Authorize] (EntetiesController controller, int id) =>
                    controller.GetTrainer(id));
                endpoints.MapGet("/api/trainer/{email}", (EntetiesController controller, string email) =>
                    controller.GetTrainerByEmail(email));

                endpoints.MapGet("/api/workout/all", [Authorize] (EntetiesController controller) =>
                    controller.GetAllWorkouts());
                endpoints.MapGet("/api/workout/{id:int}", [Authorize] (EntetiesController controller, int id) =>
                    controller.GetWorkout(id));
                endpoints.MapGet("/api/workout/trainee/{id:int}", [Authorize] (EntetiesController controller, int id) =>
                    controller.GetWorkoutsByTraineeId(id));

                endpoints.MapGet("/api/exercise/all", [Authorize] (EntetiesController controller) =>
                    controller.GetAllExercises());
                endpoints.MapGet("/api/exercise/{id:int}", [Authorize] (EntetiesController controller, int id) =>
                    controller.GetExercise(id));
                endpoints.MapGet("/api/exercise/workout/{id:int}", [Authorize] (EntetiesController controller, int id) =>
                    controller.GetExerciseByWorkoutId(id));

                endpoints.MapGet("/api/exerciseraw/all", [Authorize] (EntetiesController controller) =>
                    controller.GetAllExerciseRaws());
                endpoints.MapGet("/api/exerciseraw/{id:int}", [Authorize] (EntetiesController controller, int id) =>
                    controller.GetExerciseRaw(id));

                endpoints.MapGet("/api/admin/all", [Authorize] (EntetiesController controller) =>
                    controller.GetAllAdmins());
                endpoints.MapGet("/api/admin/{id:int}", [Authorize] (EntetiesController controller, int id) =>
                    controller.GetAdmin(id));

                endpoints.MapGet("/api/knowladgebase/all", (EntetiesController controller) =>
                    controller.GetAllKnowladgeBase());
                endpoints.MapGet("/api/knowladgebase/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetKnowladgeBase(id));
                endpoints.MapGet("/api/knowledge/trainee/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetKnowledgeBasesByTraineeId(id));

                endpoints.MapGet("/api/course/all", (EntetiesController controller) =>
                    controller.GetAllCourses());
                endpoints.MapGet("/api/available/course/all", (EntetiesController controller) =>
                    controller.GetAllAvailableCourses());
                endpoints.MapGet("/api/course/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetCourse(id));
                endpoints.MapGet("/api/available/course/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetAvailableCourse(id));
                endpoints.MapGet("/api/course/user/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetCoursesByUserId(id));

                endpoints.MapGet("/api/module/all", (EntetiesController controller) =>
                    controller.GetAllModules());
                endpoints.MapGet("/api/available/module/all", (EntetiesController controller) =>
                    controller.GetAllAvailableModules());
                endpoints.MapGet("/api/module/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetModule(id));
                endpoints.MapGet("/api/available/module/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetAvailableModule(id));
                endpoints.MapGet("/api/module/course/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetModulesByCourseId(id));
                endpoints.MapGet("/api/available/module/course/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetAvailableModulesByCourseId(id));

                endpoints.MapGet("/api/lesson/all", (EntetiesController controller) =>
                    controller.GetAllLessons());
                endpoints.MapGet("/api/available/lesson/all", (EntetiesController controller) =>
                    controller.GetAllAvailableLessons());
                endpoints.MapGet("/api/lesson/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetLesson(id));
                endpoints.MapGet("/api/available/lesson/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetAvailableLesson(id));
                endpoints.MapGet("/api/lesson/module/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetLessonsByModuleId(id));
                endpoints.MapGet("/api/available/lesson/module/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetAvailableLessonsByModuleId(id));

                endpoints.MapGet("/api/test/all", (EntetiesController controller) =>
                    controller.GetAllTests());
                endpoints.MapGet("/api/available/test/all", (EntetiesController controller) =>
                    controller.GetAllAvailableTests());
                endpoints.MapGet("/api/test/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetTest(id));
                endpoints.MapGet("/api/available/test/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetAvailableTest(id));
                endpoints.MapGet("/api/test/module/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetTestsByModuleId(id));
                endpoints.MapGet("/api/available/test/module/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetAvailableTestsByModuleId(id));
                #endregion


                #region MapPost
                endpoints.MapPost("/api/trainee",
                    (EntetiesController controller, [FromBody] Trainee trainee) =>
                        controller.AddTrainee(trainee));

                endpoints.MapPost("/api/trainer",
                    [Authorize] (EntetiesController controller, [FromBody] Trainer trainer) =>
                        controller.AddTrainer(trainer));

                endpoints.MapPost("/api/workout",
                    [Authorize] (EntetiesController controller, [FromBody] Workout workout) =>
                        controller.AddWorkout(workout));

                endpoints.MapPost("/api/exercise",
                    [Authorize] (EntetiesController controller, [FromBody] Exercise exercise) =>
                        controller.AddExercise(exercise));

                endpoints.MapPost("/api/exerciseraw",
                    [Authorize] (EntetiesController controller, [FromBody] ExerciseRaw exerciseRaw) =>
                        controller.AddExerciseRaw(exerciseRaw));

                endpoints.MapPost("/api/admin",
                    [Authorize] (EntetiesController controller, [FromBody] Admin admin) =>
                        controller.AddAdmin(admin));

                endpoints.MapPost("/api/knowladgebase",
                    [Authorize] (EntetiesController controller, [FromBody] KnowladgeBase knowladge) =>
                        controller.AddKnowladgeBase(knowladge));

                endpoints.MapPost("/api/course",
                    (EntetiesController controller, [FromBody] Course course) =>
                        controller.AddCourse(course));

                endpoints.MapPost("/api/module",
                    (EntetiesController controller, [FromBody] Module module) =>
                        controller.AddModule(module));

                endpoints.MapPost("/api/lesson",
                    (EntetiesController controller, [FromBody] Lesson lesson) =>
                        controller.AddLesson(lesson));

                endpoints.MapPost("/api/test",
                    (EntetiesController controller, [FromBody] Test test) =>
                        controller.AddTest(test));

                endpoints.MapPost("/api/traineesknowledgebases/{traineeId:int}/{knowledgeId:int}",
                    (EntetiesController controller, int traineeId, int knowledgeId) =>
                        controller.AddTraineesKnowledgeBases(traineeId, knowledgeId));

                //также для подтверждения email. Еще потом добавить в бд isEmailApproved
                endpoints.MapPost("/api/forgetpassword/{email}",
                    (EntetiesController controller, string email) =>
                        controller.ForgotPasswordT(email));

                endpoints.MapPost("/api/user/login", (EntetiesController controller, [FromBody] UserDetails user) =>
                    controller.Login(user));

                endpoints.MapPost("/api/purshuasecourse/{id:int}", (EntetiesController controller, int id, [FromBody] Course course) =>
                    controller.PurshuaseCourse(id, course));

                //endpoints.MapPost("/api/user/exercises")

                /*
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 */
                #endregion


                //delete не пользуемся, а меняем в юзере или админе настройку IsActive. Delete в основном только к exercise
                #region MapDelete
                endpoints.MapDelete("/api/trainee/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteTrainee(id));

                endpoints.MapDelete("/api/trainer/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteTrainer(id));

                endpoints.MapDelete("/api/workout/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteWorkout(id));

                endpoints.MapDelete("/api/exercise/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteExercise(id));

                endpoints.MapDelete("/api/exerciseraw/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteExerciseRaw(id));

                endpoints.MapDelete("/api/admin/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteAdmin(id));

                endpoints.MapDelete("/api/admin/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteKnowladgeBase(id));

                endpoints.MapDelete("/api/course/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteCourse(id));

                endpoints.MapDelete("/api/module/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteModule(id));

                endpoints.MapDelete("/api/lesson/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteLesson(id));

                endpoints.MapDelete("/api/test/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteTest(id));
                #endregion
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
                var context = serviceScope.ServiceProvider.GetRequiredService<CourseDbContext>();
                await context.Database.MigrateAsync();
            }
        }
    }
}
